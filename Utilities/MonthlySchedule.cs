using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class MonthlySchedule : AbstractUnboundedSchedule
    {
        int day_of_month;
        BumpRule bump_rule;
        Environment env;

        public MonthlySchedule(int day_of_month, BumpRule bump_rule, Environment env)
        {
            this.day_of_month = day_of_month;
            this.bump_rule = bump_rule;
            this.env = env;
        }

        public override DateTime do_bump(DateTime unbumped_date)
        {
            return DateBump.bump(unbumped_date.Year, unbumped_date.Month, day_of_month, bump_rule, env);
        }

        public override List<DateTime> getDates(DateTime start, DateTime end)
        {
            List<DateTime> result = new List<DateTime>();
            foreach (DateTime month in getMonths(start, end))
            {
                int days_in_month = (int)month.AddMonths(1).ToOADate() - (int)month.ToOADate();
                DateTime bumped_date = do_bump(month);

                if (bumped_date >= start && bumped_date <= end)
                    result.Add(bumped_date);
            }
            return result;
        }

        // This will return the months that are *possibly* in scope (as dates always on the 1st of the month) :
        private List<DateTime> getMonths(DateTime start, DateTime end)
        {
            List<DateTime> result = new List<DateTime>();
            if (end < start)
                return result;

            int istart_year = start.Year;
            int istart_month = start.Month;
            int iend_year = end.Year;
            int iend_month = end.Month;

            // First the month *before* we start, as bump rules may take us within the range:
            result.Add(new DateTime(istart_year, istart_month, 1).AddMonths(-1));

            while (istart_year < iend_year || (istart_year == iend_year && istart_month <= iend_month))
            {
                result.Add(new DateTime(istart_year, istart_month, 1));

                ++istart_month;
                if (istart_month == 13)
                {
                    istart_month = 1;
                    ++istart_year;
                }
            }

            // Finally this will be the month *after* we end, as bump rules may take us within the range:
            result.Add(new DateTime(istart_year, istart_month, 1));

            return result;
        }

        public override DateTime prev_unbumped(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public override DateTime advance_unbumped(DateTime date, int nperiods)
        {
            // We do a "monthly serial" to be able to add/subtract nperiods easily :
            int monthly_serial = date.Year * 12 + date.Month - 1;
            monthly_serial += nperiods;
            int year = monthly_serial / 12;
            int month = monthly_serial % 12 + 1;
            return new DateTime(year, month, 1);
        }

        public override int diff_unbumped(DateTime from_unbumped, DateTime to_unbumped)
        {
            // We do a "monthly serial" to be able to add/subtract nperiods easily :
            int monthly_serial_from = from_unbumped.Year * 12 + from_unbumped.Month - 1;
            int monthly_serial_to = to_unbumped.Year * 12 + to_unbumped.Month - 1;

            return monthly_serial_to - monthly_serial_from;
        }
    }
}