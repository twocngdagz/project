using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Utilities
{
    [TestFixture]
    public class DateBumpTest
    {
        Environment env = Environment.StandardEnvironment;

        [Test]
        public void SimpleWorkingDay()
        {
            DateTime a_random_working_date = new DateTime(2000, 1, 5);

            // BumpRule.None
            DateTime result = DateBump.bump(a_random_working_date, BumpRule.None, env);
            Assert.AreEqual(a_random_working_date, result);

            // BumpRule.Following
            result = DateBump.bump(a_random_working_date, BumpRule.Following, env);
            Assert.AreEqual(a_random_working_date, result);
        }
        [Test]
        public void None()
        {
            DateTime a_holiday = new DateTime(2000, 1, 1);

            // None means don't do anything
            DateTime result = DateBump.bump(a_holiday, BumpRule.None, env);
            Assert.AreEqual(a_holiday, result);
        }
        [Test]
        public void Following()
        {
            DateTime a_holiday = new DateTime(2000, 1, 1);

            // Following business date will be 4/1 as 1/1 was a Sat so the bank holiday shifts
            DateTime result = DateBump.bump(a_holiday, BumpRule.Following, env);
            Assert.AreEqual(new DateTime(2000, 1, 4), result);
        }
        [Test]
        public void EOMSimple()
        {
            // Day is greater than the number of days in month, so default to last day in month
            DateTime result = DateBump.bump(2000, 9, 31, BumpRule.None, env);
            Assert.AreEqual(new DateTime(2000, 9, 30), result);
        }
        [Test]
        public void EOMFollowing()
        {
            // Bump after adjusting the day - 30/9 is a Saturday
            DateTime result = DateBump.bump(2000, 9, 31, BumpRule.Following, env);
            Assert.AreEqual(new DateTime(2000, 10, 2), result);
        }
    }
}
