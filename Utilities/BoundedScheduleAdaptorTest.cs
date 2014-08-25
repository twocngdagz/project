using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Utilities
{
    [TestFixture]
    public class BoundedScheduleAdaptorTest
    {
        BoundedScheduleAdaptor bsa;
        [Test]
        public void simple()
        {
            bsa = new BoundedScheduleAdaptor(new MonthlySchedule(15, BumpRule.None, Environment.StandardEnvironment));

            Assert.AreEqual(null, bsa.FirstDate);
            Assert.AreEqual(null, bsa.LastSentinelDate);
            Assert.AreEqual(false, bsa.IsFinite);
        }
        [Test]
        public void firstDate()
        {
            bsa = new BoundedScheduleAdaptor(new MonthlySchedule(15, BumpRule.None, Environment.StandardEnvironment), firstdate:new DateTime(2000, 1, 15));

            bsa.NPoints = 12;

            Assert.AreEqual(new DateTime(2000, 1, 15), bsa.FirstDate);
            Assert.AreEqual(new DateTime(2001, 1, 15), bsa.LastSentinelDate);
            Assert.AreEqual(true, bsa.IsFinite);
            Assert.AreEqual(12, bsa.NPoints);
        }
        [Test]
        public void lastDate()
        {
            bsa = new BoundedScheduleAdaptor(new MonthlySchedule(15, BumpRule.None, Environment.StandardEnvironment), lastsentineldate: new DateTime(2001, 1, 15));

            bsa.NPoints = 12;

            Assert.AreEqual(new DateTime(2000, 1, 15), bsa.FirstDate);
            Assert.AreEqual(new DateTime(2001, 1, 15), bsa.LastSentinelDate);
            Assert.AreEqual(true, bsa.IsFinite);
            Assert.AreEqual(12, bsa.NPoints);
        }
        [Test]
        public void firstAndLastDate()
        {
            bsa = new BoundedScheduleAdaptor(new MonthlySchedule(15, BumpRule.None, Environment.StandardEnvironment), firstdate: new DateTime(2000, 1, 15), lastsentineldate: new DateTime(2001, 1, 15));

            Assert.AreEqual(new DateTime(2000, 1, 15), bsa.FirstDate);
            Assert.AreEqual(new DateTime(2001, 1, 15), bsa.LastSentinelDate);
            Assert.AreEqual(true, bsa.IsFinite);
            Assert.AreEqual(12, bsa.NPoints);
        }
    }
}
