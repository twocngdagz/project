using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Utilities
{
    [TestFixture]
    public class UKCalendarTest
    {
        UKCalendar cal = new UKCalendar();
        [Test]
        public void RulesVsLookup()
        {
            // Compared rule-based isHoliday() vs a lookup in pre-defined tables.
            for (DateTime d = UKCalendar.MinDate; d <= UKCalendar.MaxDate; d = d.AddDays(1))
            {
                Assert.AreEqual(cal.isBankHoliday_lookup(d), cal.isBankHoliday_rules(d), "Error for d=" + d.ToString());
            }
        }
        [Test]
        public void DateLimitsOk()
        {
            cal.isBankHoliday(new DateTime(1901, 1, 1));
            cal.isBankHoliday(new DateTime(2199, 1, 1));
        }
        [Test, ExpectedException(typeof(ApplicationException))]
        public void DateTooSmall()
        {
            cal.isBankHoliday(new DateTime(1900, 1, 1));
        }
        [Test, ExpectedException(typeof(ApplicationException))]
        public void DateTooLarge()
        {
            cal.isBankHoliday(new DateTime(2200, 1, 1));
        }
    }
}