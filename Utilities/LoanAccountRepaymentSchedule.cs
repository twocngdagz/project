using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Utilities
{
    public class LoanAccountRepaymentSchedule : IRepaymentSchedule
    {
        Dictionary<DateTime, double> interests = new Dictionary<DateTime, double>();
        Dictionary<DateTime, double> totals = new Dictionary<DateTime, double>();

        IBoundedSchedule schedule;

        public LoanAccountRepaymentSchedule(int account_id, string repay_regex, string int_regex)
        {
            string query = string.Format("select * from vwClassifiedSplitTransactions where account_id = {0} order by date, transaction_id;", account_id);
            // 28aEHS : account_id  = 12
            // 33RH : account_id  = 11
            // LLD[33RH,F3,110SS,28aEHS,64BHR] : account_id  = 13
            // LLD[25VP] : account_id  = 14
            DataTable dt = Database.runQuery(query);
            List<DateTime> dates = new List<DateTime>();
            Dictionary<DateTime, double> balances = new Dictionary<DateTime, double>();
            foreach (DataRow row in dt.Rows)
            {
                DateTime date = (DateTime)row["date"];
                string description = (string)row["description"];
                double amount = (double)row["amount"];
                double? balance = (double?)(row["balance"] is DBNull ? null : row["balance"]);

                Console.WriteLine("date={0} description='{1}' amount={2}", date, description, amount);

                // 28aEHS/33RH    repay_regex = "PAYT" int_regex = "INTEREST"
                // LLD[33RH,F3,110SS,28aEHS,64BHR] / LLD[25VP]   repay_regex = "Repayment" int_regex = "Interest"

                System.Text.RegularExpressions.Regex rpay = new System.Text.RegularExpressions.Regex("Repayment");
                System.Text.RegularExpressions.Regex rint = new System.Text.RegularExpressions.Regex("Interest");
                if (rpay.Match(description).Success)
                    totals[date] = Math.Abs(amount);
                else if (rint.Match(description).Success)
                    interests[date] = Math.Abs(amount);

                if ((rpay.Match(description).Success || rint.Match(description).Success) && balance.HasValue)
                    balances[date] = Math.Abs(balance.Value);

                dates.Add(date);
            }
            Console.WriteLine("totals={0} interests={1}", totals.Count, interests.Count);

            schedule = new SimpleListSchedule(dates);
            if (schedule.NPoints == 0)
            {
                DrawdownAmount = 0;
            }
            else
            {
                DateTime firstdate = schedule.FirstDate.Value;
                if (balances.ContainsKey(firstdate))
                    DrawdownAmount = balances[firstdate] + totals[firstdate] - interests[firstdate];
                else
                    DrawdownAmount = 0;
            }

            ExpectedAccuracy = 1.05e-2; // Accurate within 1p
        }

        #region IRepaymentSchedule Members

        public double interest(double amount, DateTime date)
        {
            if ((!interests.ContainsKey(date)) || (!totals.ContainsKey(date)))
                throw new Exception("Not found");

            double absamount = Math.Abs(amount);
            // 28aEHS/33RH
            //if (Math.Abs(absamount - totals[date]) > 1e-5)
            if (Math.Abs(absamount - totals[date]) > 0.01 + 1e-5) // We can be 1p off apparently
                throw new Exception(string.Format("Wrong amount got {0} expected {1}", absamount, totals[date]));


            return Math.Sign(amount) * interests[date];
        }

        public double capital(double amount, DateTime date)
        {
            if ((!interests.ContainsKey(date)) || (!totals.ContainsKey(date)))
                throw new Exception("Not found");

            double absamount = Math.Abs(amount);
            if (Math.Abs(absamount - totals[date]) > 1e-5)
                throw new Exception("Wrong amount");

            return Math.Sign(amount) * (totals[date] - interests[date]);
        }

        public double interest(DateTime date)
        {
            if ((!interests.ContainsKey(date)) || (!totals.ContainsKey(date)))
                throw new Exception("Not found");

            return interests[date];
        }

        public double capital(DateTime date)
        {
            if ((!interests.ContainsKey(date)) || (!totals.ContainsKey(date)))
                throw new Exception("Not found");

            return totals[date] - interests[date];
        }

        public double interest(int period)
        {
            return interest(Schedule.advance(Schedule.FirstDate.Value, true, period));
        }

        public double capital(int period)
        {
            return capital(Schedule.advance(Schedule.FirstDate.Value, true, period));
        }

        public IBoundedSchedule Schedule
        {
            get { return schedule; }
        }

        public DateTime DrawdownDate
        {
            get;
            private set;
        }

        public double DrawdownAmount
        {
            get;
            private set;
        }

        public double ExpectedAccuracy
        {
            get;
            private set;
        }

        #endregion
    }
}
