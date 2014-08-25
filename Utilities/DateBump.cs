using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class DateBump
    {
        // Main function, take (year,month,day) as we cannot be sure to have a valid day of month:
        public static DateTime bump(int year, int month, int day, BumpRule bump_rule, Environment env)
        {
            // For now resolve end-of-month issues as simply as possible:
            DateTime input = new DateTime(year, month, 1);
            int days_in_month = (int)input.AddMonths(1).ToOADate() - (int)input.ToOADate();
            input = new DateTime(year, month, Math.Min(day, days_in_month));

            return bump(input, bump_rule, env);
        }
        // Convenience function if we know unbumped is already a valid date:
        public static DateTime bump(DateTime input, BumpRule bump_rule, Environment env)
        {
            DateTime bumped_date = input;

            if (bump_rule == BumpRule.Following)
            {
                while (env.Calendar.isBankHoliday(bumped_date))
                {
                    bumped_date = bumped_date.AddDays(1);
                }
            }

            return bumped_date;
        }
    }
}
