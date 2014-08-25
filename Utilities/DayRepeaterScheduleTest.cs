using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Utilities
{
    [TestFixture]
    public class DayRepeaterScheduleTest
    {
        static DateTime ref_date = new DateTime(2000, 1, 1);
        static DateTime next_date = new DateTime(2000, 1, 29);
        static DateTime ref_date_b = new DateTime(2000, 1, 4);
        static DateTime next_date_b = new DateTime(2000, 1, 31);
        DayRepeaterSchedule schedule = new DayRepeaterSchedule(ref_date, 28, BumpRule.None, Environment.StandardEnvironment);
        DayRepeaterSchedule schedule_b = new DayRepeaterSchedule(ref_date, 28, BumpRule.Following, Environment.StandardEnvironment);

        [Test]
        public void NextExact()
        {
            DateTime next = schedule.advance(ref_date, true, 1);
            Assert.AreEqual(next_date, next);
        }
        [Test]
        public void PrevExact()
        {
            DateTime prev = schedule.advance(next_date, true, -1);
            Assert.AreEqual(ref_date, prev);
        }
        [Test]
        public void NextNotExact()
        {
            for (DateTime date = ref_date; date < next_date; date = date.AddDays(1))
            {
                DateTime next = schedule.advance(date, false, 1);
                Assert.AreEqual(next_date, next, "Date was : " + date.ToString());
            }
        }
        [Test]
        public void PrevNotExact()
        {
            for (DateTime date = ref_date.AddDays(1); date <= next_date; date = date.AddDays(1))
            {
                DateTime prev = schedule.advance(date, false, -1);
                Assert.AreEqual(ref_date, prev, "Date was : " + date.ToString());
            }
        }
        [Test, ExpectedException(typeof(ApplicationException))]
        public void NextExactException()
        {
            schedule.advance(new DateTime(2000, 1, 15), true, 1);
        }
        [Test, ExpectedException(typeof(ApplicationException))]
        public void PrevExactException()
        {
            schedule.advance(new DateTime(2000, 1, 15), true, -1);
        }
        [Test]
        public void NextExact_b()
        {
            DateTime next = schedule_b.advance(ref_date_b, true, 1);
            Assert.AreEqual(next_date_b, next);
        }
        [Test]
        public void PrevExact_b()
        {
            DateTime prev = schedule_b.advance(next_date_b, true, -1);
            Assert.AreEqual(ref_date_b, prev);
        }
        [Test]
        public void NextNotExact_b()
        {
            for (DateTime date = ref_date_b; date < next_date_b; date = date.AddDays(1))
            {
                DateTime next = schedule_b.advance(date, false, 1);
                Assert.AreEqual(next_date_b, next, "Date was : " + date.ToString());
            }
        }
        [Test]
        public void PrevNotExact_b()
        {
            for (DateTime date = ref_date_b.AddDays(1); date <= next_date_b; date = date.AddDays(1))
            {
                DateTime prev = schedule_b.advance(date, false, -1);
                Assert.AreEqual(ref_date_b, prev, "Date was : " + date.ToString());
            }
        }
        [Test, ExpectedException(typeof(ApplicationException))]
        public void NextExactException_b()
        {
            schedule_b.advance(new DateTime(2000, 1, 15), true, 1);
        }
        [Test, ExpectedException(typeof(ApplicationException))]
        public void PrevExactException_b()
        {
            schedule_b.advance(new DateTime(2000, 1, 15), true, -1);
        }
        [Test]
        public void PrevUnbumped()
        {
            Assert.AreEqual(ref_date, schedule.prev_unbumped(new DateTime(2000, 1, 15)));
            Assert.AreEqual(ref_date, schedule.prev_unbumped(new DateTime(2000, 1, 2)));
            Assert.AreEqual(ref_date, schedule.prev_unbumped(new DateTime(2000, 1, 1)));
            Assert.AreEqual(ref_date, schedule.prev_unbumped(new DateTime(2000, 1, 28)));
            Assert.AreEqual(new DateTime(2000, 1, 29), schedule.prev_unbumped(new DateTime(2000, 1, 29)));

            Assert.AreEqual(ref_date, schedule_b.prev_unbumped(new DateTime(2000, 1, 15)));
            Assert.AreEqual(ref_date, schedule_b.prev_unbumped(new DateTime(2000, 1, 2)));
            Assert.AreEqual(ref_date, schedule_b.prev_unbumped(new DateTime(2000, 1, 1)));
            Assert.AreEqual(ref_date, schedule_b.prev_unbumped(new DateTime(2000, 1, 28)));
            Assert.AreEqual(new DateTime(2000, 1, 29), schedule_b.prev_unbumped(new DateTime(2000, 1, 29)));
        }
        [Test]
        public void BumpedPrevUnbumped()
        {
            Assert.AreEqual(ref_date, schedule.bumped_prev_unbumped(new DateTime(2000, 1, 15), false));
            Assert.AreEqual(ref_date, schedule.bumped_prev_unbumped(new DateTime(2000, 1, 2), false));
            Assert.AreEqual(ref_date, schedule.bumped_prev_unbumped(new DateTime(2000, 1, 1), false));
            Assert.AreEqual(ref_date, schedule.bumped_prev_unbumped(new DateTime(2000, 1, 28), false));
            Assert.AreEqual(new DateTime(2000, 1, 29), schedule.bumped_prev_unbumped(new DateTime(2000, 1, 29), false));

            Assert.AreEqual(ref_date, schedule_b.bumped_prev_unbumped(new DateTime(2000, 1, 15), false));
            Assert.AreEqual(ref_date.AddDays(-28), schedule_b.bumped_prev_unbumped(new DateTime(2000, 1, 2), false));
            Assert.AreEqual(ref_date.AddDays(-28), schedule_b.bumped_prev_unbumped(new DateTime(2000, 1, 1), false));
            Assert.AreEqual(ref_date, schedule_b.prev_unbumped(new DateTime(2000, 1, 28)));
            Assert.AreEqual(new DateTime(2000, 1, 1), schedule_b.bumped_prev_unbumped(new DateTime(2000, 1, 29), false));
            Assert.AreEqual(new DateTime(2000, 1, 1), schedule_b.bumped_prev_unbumped(new DateTime(2000, 1, 30), false));
            Assert.AreEqual(new DateTime(2000, 1, 29), schedule_b.bumped_prev_unbumped(new DateTime(2000, 1, 31), false));
        }
    }
}
