using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace Utilities
{
    public class ScheduleUtils
    {
        public static DateTime findClose(IUnboundedSchedule schedule, DateTime date)
        {
            // Check logic if date is outside range of schedule XXX
            DateTime upper = schedule.advance(date, false, 1);
            DateTime lower = schedule.advance(upper, true, -1);

            int diff_upper = Math.Abs((int)upper.ToOADate() - (int)date.ToOADate());
            int diff_lower = Math.Abs((int)lower.ToOADate() - (int)date.ToOADate());

            if (diff_lower <= diff_upper)
                return lower;
            else
                return upper;
        }

        public static double capital(IRepaymentSchedule schedule, double amount, DateTime date)
        {
            DateTime date_use = findClose(schedule.Schedule, date);
            double raw_capital = schedule.capital(date);
            double raw_interest = schedule.interest(date);
            double raw_total = raw_capital + raw_interest;

            if (schedule.ExpectedAccuracy >=0 && (Math.Abs(Math.Abs(raw_total)-Math.Abs(amount)) > schedule.ExpectedAccuracy))
                throw new ApplicationException("Wrong amount in repayment-schedule interest/capital query");

            // Depending on how "amount" comes in, we may have to adjust signs. I.e., the raw_total will generally be
            // positive, but if amount is something debited from our account it will be negative.
            int sign_factor = Math.Sign(amount) * Math.Sign(raw_total);

            // We keep the captial repayment as is (ensuring full amortization at loan termination), any small variations
            // in the amount will be attributed to differences in the interest calculation:
            return raw_capital * sign_factor;
        }

        public static double interest(IRepaymentSchedule schedule, double amount, DateTime date)
        {
            // As per above, capital repayment is fixed, rest is deemed to be interest :
            return amount - capital(schedule, amount, date);
        }

        public static IRepaymentSchedule getScheduleByLoanName(Environment env, string loan_name)
        {
            string query = "select * from FormulaSchedule where loan_name = @loan_name;";
            DataTable dt = Database.runQuery(query, "loan_name", loan_name);
            if (dt.Rows.Count != 1)
                throw new ApplicationException("Loan name not found in getScheduleByLoanName() : " + loan_name);

            DataRow this_row = dt.Rows[0];
            uint fs_id = (uint)this_row["fs_id"];
            bool is_interest_only = (sbyte)this_row["is_interest_only"] != (sbyte)0;
            DateTime first_date = (DateTime)this_row["first_date"];
            int day_of_month = (int)this_row["day_of_month"];
            DateTime drawdown_date = (DateTime)this_row["drawdown_date"];
            double drawdown_amount = (double)this_row["drawdown_amount"];

            query = "select * from FormulaSchedule_rates where fs_id = @fs_id order by rate_order;";
            dt = Database.runQuery(query, "fs_id", fs_id);
            List<int> nperiods = new List<int>();
            List<Rate> rates = new List<Rate>();
            foreach (DataRow row in dt.Rows)
            {
                nperiods.Add((int)row["nperiods"]);
                rates.Add(new Rate((string)row["rate_type_name"], (double)row["rate"], (int)row["rate_lag"]));
            }

            if (is_interest_only)
                return new FormulaInterestOnlySchedule(
                     env,
                     nperiods,
                     rates,
                     first_date,
                     day_of_month,
                     drawdown_date,
                     drawdown_amount);
            else
                return new FormulaRepaymentSchedule(
                    env,
                    nperiods,
                    rates,
                    first_date,
                    day_of_month,
                    drawdown_date,
                    drawdown_amount);
        }

        public static DataTable ToDataTable(IRepaymentSchedule schedule)
        {
            DataTable result = new DataTable("Transactions");
            result.Columns.Add("date", typeof(DateTime));
            result.Columns.Add("amount", typeof(double));
            result.Columns.Add("capital", typeof(double));
            result.Columns.Add("interest", typeof(double));
            result.Columns.Add("balance", typeof(double));

            result.Rows.Add(schedule.DrawdownDate, -schedule.DrawdownAmount, -schedule.DrawdownAmount, 0e0, schedule.DrawdownAmount);

            double balance = schedule.DrawdownAmount;
            int period = 0;
            for (DateTime date = schedule.Schedule.FirstDate.Value;
                 date < schedule.Schedule.LastSentinelDate.Value;
                 date = schedule.Schedule.advance(date, true, 1))
            {
                double capital = schedule.capital(period);
                double interest = schedule.interest(period);
                balance -= capital;
                result.Rows.Add(date, capital + interest, capital, interest, balance);
                ++period;
            }

            return result;
        }

        public static RepaymentSchedulesByInputLoanName GetRepaymentSchedulesByInputLoanName(Environment env, string input_loan_name, bool is_current = true)
        {
            RepaymentSchedulesByInputLoanName result = new RepaymentSchedulesByInputLoanName();
            result.InputLoanName = input_loan_name;
            string query = "select * from FormulaSchedule where coalesce(input_loan_name, loan_name) = @input_loan_name";
            if (is_current)
                query += " and is_current = 1";
            DataTable dt = Database.runQuery(query, "input_loan_name", input_loan_name);

            foreach (DataRow row in dt.Rows)
            {
                string loan_name = (string)row["loan_name"];
                result.RepaymentSchedules[loan_name] = getScheduleByLoanName(env, loan_name);
            }

            return result;
        }
        public static List<RepaymentSchedulesByInputLoanName> GetAllRepaymentSchedulesByInputLoanName(Environment env, bool is_current = true)
        {
            List<RepaymentSchedulesByInputLoanName> result = new List<RepaymentSchedulesByInputLoanName>();
            string query = "select distinct coalesce(input_loan_name, loan_name) as input_loan_name from FormulaSchedule";
            if (is_current)
                query += " where is_current = 1";
            query += " order by coalesce(input_loan_name, loan_name);";
            DataTable dt = Database.runQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                string input_loan_name = (string)row["input_loan_name"];
                result.Add(GetRepaymentSchedulesByInputLoanName(env, input_loan_name, is_current));
            }

            return result;
        }
        public static Dictionary<string, LoanSplitResult> SplitLoan(DateTime date, double amount, RepaymentSchedulesByInputLoanName rsbil)
        {
            Dictionary<string, LoanSplitResult> result = new Dictionary<string, LoanSplitResult>();
            double sum_interest = 0e0;
            double sum_capital = 0e0;
            foreach (string loan_name in rsbil.RepaymentSchedules.Keys)
            {
                LoanSplitResult this_result = new LoanSplitResult();
                IRepaymentSchedule ir = rsbil.RepaymentSchedules[loan_name];
                this_result.OriginalInterest = ir.interest(date);
                this_result.OriginalCapital = ir.capital(date);
                sum_interest += this_result.OriginalInterest;
                sum_capital += this_result.OriginalCapital;
                result[loan_name] = this_result;
            }

            int sign = Math.Sign(amount);
            double abs_amount = Math.Abs(amount);
            // We do not adjust the capital repayments (this allows repayment to follow the usual schedule)
            // but the remaining interest amount is split amongst the loans proportionally
            double interest_factor = sum_interest / (abs_amount - sum_capital);
            foreach (string loan_name in result.Keys)
            {
                result[loan_name].AdjustedInterest = sign * interest_factor * result[loan_name].OriginalInterest;
                result[loan_name].AdjustedCapital = sign * result[loan_name].OriginalCapital;
            }

            return result;
        }
    }
}
