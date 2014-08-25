using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public abstract class Calendar
    {
        public enum Month : int { January = 1, February = 2, March = 3, April = 4, May = 5, June = 6, July = 7, August = 8, September = 9, October = 10, November = 11, December = 12 };

        protected Dictionary<DateTime, bool> holidays = new Dictionary<DateTime, bool>();
        protected Dictionary<DayOfWeek, bool> weekend_days = new Dictionary<DayOfWeek, bool>();

        public bool isBankHoliday(DateTime date)
        {
            try
            {
                return isBankHoliday_lookup(date);
            }
            catch (Exception) // Could not look up  - out of range
            {
            }
            return isBankHoliday_rules(date);
        }
        public bool isBankHoliday_lookup(DateTime date)
        {
            if (date < MinDate || date > MaxDate)
                throw new ApplicationException("Date out of range in isBankHoliday: " + date.ToString());

            return weekend_days.ContainsKey(date.DayOfWeek) || holidays.ContainsKey(date);
        }
        public virtual bool isBankHoliday_rules(DateTime date)
        {
            throw new NotImplementedException("This calendar has not got a rule-based holiday implementation!");
        }

        public static DateTime MinDate
        {
            get;
            protected set;
        }
        public static DateTime MaxDate
        {
            get;
            protected set;
        }
    }
}
