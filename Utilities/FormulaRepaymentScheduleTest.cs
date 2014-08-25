using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Utilities
{
    [TestFixture]
    public class FormulaRepaymentScheduleTest
    {
        private IRepaymentSchedule zopa_schedule;
        public FormulaRepaymentScheduleTest()
        {
            Environment env = Environment.StandardEnvironment;
            List<int> nperiods = new List<int>();
            List<Rate> rates = new List<Rate>();
            List<string> ratetypes = new List<string>();
            nperiods.Add(36);
            rates.Add(new Rate("FIXED", .0902e0)); // Officially 9.4% but Zopa has a weird calculation method (daily amortization of capital?)
            DateTime firstdate = new DateTime(2010, 5, 10);
            int day_of_month = 10;
            DateTime drawdown_date = new DateTime(2010, 4, 6);
            double drawdown_amount = 15120;

            zopa_schedule = new FormulaRepaymentSchedule(env, nperiods, rates, firstdate, day_of_month, drawdown_date, drawdown_amount);
        }
        [Test]
        public void ZopaRegression()
        {
            List<DateTime> dates = new List<DateTime>();
            List<double> interests = new List<double>();
            List<double> capitals = new List<double>();

            dates.Add(new DateTime(2010, 5, 10));
            dates.Add(new DateTime(2010, 6, 10));
            dates.Add(new DateTime(2010, 7, 12));
            dates.Add(new DateTime(2010, 8, 10));
            dates.Add(new DateTime(2010, 9, 10));
            dates.Add(new DateTime(2010, 10, 11));
            dates.Add(new DateTime(2010, 11, 10));
            dates.Add(new DateTime(2010, 12, 10));
            dates.Add(new DateTime(2011, 1, 10));
            dates.Add(new DateTime(2011, 2, 10));
            dates.Add(new DateTime(2011, 3, 10));
            dates.Add(new DateTime(2011, 4, 11));
            dates.Add(new DateTime(2011, 5, 10));
            dates.Add(new DateTime(2011, 6, 10));
            dates.Add(new DateTime(2011, 7, 11));
            dates.Add(new DateTime(2011, 8, 10));
            dates.Add(new DateTime(2011, 9, 12));
            dates.Add(new DateTime(2011, 10, 10));
            dates.Add(new DateTime(2011, 11, 10));
            dates.Add(new DateTime(2011, 12, 12));
            dates.Add(new DateTime(2012, 1, 10));
            dates.Add(new DateTime(2012, 2, 10));
            dates.Add(new DateTime(2012, 3, 12));
            dates.Add(new DateTime(2012, 4, 10));
            dates.Add(new DateTime(2012, 5, 10));
            dates.Add(new DateTime(2012, 6, 11));
            dates.Add(new DateTime(2012, 7, 10));
            dates.Add(new DateTime(2012, 8, 10));
            dates.Add(new DateTime(2012, 9, 10));
            dates.Add(new DateTime(2012, 10, 10));
            dates.Add(new DateTime(2012, 11, 12));
            dates.Add(new DateTime(2012, 12, 10));
            dates.Add(new DateTime(2013, 1, 10));
            dates.Add(new DateTime(2013, 2, 11));
            dates.Add(new DateTime(2013, 3, 11));
            dates.Add(new DateTime(2013, 4, 10));

            interests.Add(113.65e0);
            interests.Add(110.89e0);
            interests.Add(108.11e0);
            interests.Add(105.31e0);
            interests.Add(102.48e0);
            interests.Add(99.64e0);
            interests.Add(96.77e0);
            interests.Add(93.88e0);
            interests.Add(90.98e0);
            interests.Add(88.04e0);
            interests.Add(85.09e0);
            interests.Add(82.11e0);
            interests.Add(79.12e0);
            interests.Add(76.1e0);
            interests.Add(73.05e0);
            interests.Add(69.99e0);
            interests.Add(66.9e0);
            interests.Add(63.79e0);
            interests.Add(60.65e0);
            interests.Add(57.49e0);
            interests.Add(54.31e0);
            interests.Add(51.1e0);
            interests.Add(47.87e0);
            interests.Add(44.61e0);
            interests.Add(41.33e0);
            interests.Add(38.03e0);
            interests.Add(34.7e0);
            interests.Add(31.35e0);
            interests.Add(27.97e0);
            interests.Add(24.56e0);
            interests.Add(21.13e0);
            interests.Add(17.68e0);
            interests.Add(14.19e0);
            interests.Add(10.68e0);
            interests.Add(7.15e0);
            interests.Add(3.59e0);

            capitals.Add(367.3e0);
            capitals.Add(370.06e0);
            capitals.Add(372.84e0);
            capitals.Add(375.65e0);
            capitals.Add(378.47e0);
            capitals.Add(381.31e0);
            capitals.Add(384.18e0);
            capitals.Add(387.07e0);
            capitals.Add(389.98e0);
            capitals.Add(392.91e0);
            capitals.Add(395.86e0);
            capitals.Add(398.84e0);
            capitals.Add(401.84e0);
            capitals.Add(404.86e0);
            capitals.Add(407.9e0);
            capitals.Add(410.97e0);
            capitals.Add(414.05e0);
            capitals.Add(417.17e0);
            capitals.Add(420.3e0);
            capitals.Add(423.46e0);
            capitals.Add(426.64e0);
            capitals.Add(429.85e0);
            capitals.Add(433.08e0);
            capitals.Add(436.34e0);
            capitals.Add(439.62e0);
            capitals.Add(442.92e0);
            capitals.Add(446.25e0);
            capitals.Add(449.61e0);
            capitals.Add(452.99e0);
            capitals.Add(456.39e0);
            capitals.Add(459.82e0);
            capitals.Add(463.28e0);
            capitals.Add(466.76e0);
            capitals.Add(470.27e0);
            capitals.Add(473.8e0);
            capitals.Add(477.36e0);

            for (int i = 0; i < dates.Count; ++i)
            {
                DateTime date = dates[i];
                double interest_exp = interests[i];
                double capital_exp = capitals[i];
                Assert.AreEqual(interest_exp, zopa_schedule.interest(date), 6e-3);
                Assert.AreEqual(capital_exp, zopa_schedule.capital(date), 6e-3);
            }
        //06-Apr-10	-£15,120.00	-£15,120.00	£0.00	£15,120.00
        //10-May-10	£480.95	£367.30	£113.65	£14,752.70
        //10-Jun-10	£480.95	£370.06	£110.89	£14,382.64
        //12-Jul-10	£480.95	£372.84	£108.11	£14,009.79
        //10-Aug-10	£480.95	£375.65	£105.31	£13,634.15
        //10-Sep-10	£480.95	£378.47	£102.48	£13,255.68
        //11-Oct-10	£480.95	£381.31	£99.64	£12,874.37
        //10-Nov-10	£480.95	£384.18	£96.77	£12,490.18
        //10-Dec-10	£480.95	£387.07	£93.88	£12,103.12
        //10-Jan-11	£480.95	£389.98	£90.98	£11,713.14
        //10-Feb-11	£480.95	£392.91	£88.04	£11,320.23
        //10-Mar-11	£480.95	£395.86	£85.09	£10,924.37
        //11-Apr-11	£480.95	£398.84	£82.11	£10,525.53
        //10-May-11	£480.95	£401.84	£79.12	£10,123.69
        //10-Jun-11	£480.95	£404.86	£76.10	£9,718.84
        //11-Jul-11	£480.95	£407.90	£73.05	£9,310.94
        //10-Aug-11	£480.95	£410.97	£69.99	£8,899.97
        //12-Sep-11	£480.95	£414.05	£66.90	£8,485.92
        //10-Oct-11	£480.95	£417.17	£63.79	£8,068.75
        //10-Nov-11	£480.95	£420.30	£60.65	£7,648.45
        //12-Dec-11	£480.95	£423.46	£57.49	£7,224.99
        //10-Jan-12	£480.95	£426.64	£54.31	£6,798.34
        //10-Feb-12	£480.95	£429.85	£51.10	£6,368.49
        //12-Mar-12	£480.95	£433.08	£47.87	£5,935.41
        //10-Apr-12	£480.95	£436.34	£44.61	£5,499.07
        //10-May-12	£480.95	£439.62	£41.33	£5,059.45
        //11-Jun-12	£480.95	£442.92	£38.03	£4,616.53
        //10-Jul-12	£480.95	£446.25	£34.70	£4,170.28
        //10-Aug-12	£480.95	£449.61	£31.35	£3,720.67
        //10-Sep-12	£480.95	£452.99	£27.97	£3,267.68
        //10-Oct-12	£480.95	£456.39	£24.56	£2,811.29
        //12-Nov-12	£480.95	£459.82	£21.13	£2,351.47
        //10-Dec-12	£480.95	£463.28	£17.68	£1,888.20
        //10-Jan-13	£480.95	£466.76	£14.19	£1,421.44
        //11-Feb-13	£480.95	£470.27	£10.68	£951.17
        //11-Mar-13	£480.95	£473.80	£7.15	£477.36
        //10-Apr-13	£480.95	£477.36	£3.59	-£0.00
        }
        [Test, ExpectedException(typeof(ApplicationException))]
        public void WrongDateException()
        {
            // 10 March 11 was taken on 11 March 11 for some reason, ensure we have some tolerance on the exact date :
            DateTime wrong_date = new DateTime(2011, 3, 11);
            Assert.AreEqual(85.09e0, zopa_schedule.interest(wrong_date), 6e-3);
        }

        [Test]
        public void DateVariation()
        {
            // 10 March 11 was taken on 11 March 11 for some reason, ensure we have some tolerance on the exact date :
            DateTime wrong_date = new DateTime(2011, 3, 11);
            DateTime right_date = ScheduleUtils.findClose(zopa_schedule.Schedule, wrong_date);
            Assert.AreEqual(85.09e0, zopa_schedule.interest(right_date), 6e-3);
            Assert.AreEqual(395.86e0, zopa_schedule.capital(right_date), 6e-3);
        }
    }
}
