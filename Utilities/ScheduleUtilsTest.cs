using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Utilities
{
    [TestFixture]
    public class ScheduleUtilsTest
    {
        [Test]
        public void getByName()
        {
            IRepaymentSchedule schedule = ScheduleUtils.getScheduleByLoanName(Environment.StandardEnvironment, "ZOPA");

            Assert.AreEqual(36, schedule.Schedule.NPoints);
            Assert.AreEqual(new DateTime(2010, 5, 10), schedule.Schedule.FirstDate.Value);
        }
        [Test]
        public void multipleLoanClassification()
        {
            RepaymentSchedulesByInputLoanName rsbil = ScheduleUtils.GetRepaymentSchedulesByInputLoanName(Environment.StandardEnvironment, "TMW[84bPL]");

            double amount = -641.25e0;
            DateTime date = new DateTime(2013, 3, 1);

            Dictionary<string, LoanSplitResult> result = ScheduleUtils.SplitLoan(date, amount, rsbil);
            foreach (string loan_name in result.Keys)
            {
                double i = result[loan_name].AdjustedInterest;
                double c = result[loan_name].AdjustedCapital;
            }
            //IRepaymentSchedule schedule = ScheduleUtils.getScheduleByLoanName(Environment.StandardEnvironment, "ZOPA");

            //Assert.AreEqual(36, schedule.Schedule.NPoints);
            //Assert.AreEqual(new DateTime(2010, 5, 10), schedule.Schedule.FirstDate.Value);
        }
    }
}
