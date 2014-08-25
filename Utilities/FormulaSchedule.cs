using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public abstract class FormulaSchedule : IRepaymentSchedule
    {
        protected Environment env;
        protected DateTime firstdate;
        protected int nper;

        protected IBoundedSchedule schedule;

        protected List<int> nperiods = new List<int>();
        protected List<Rate> rates = new List<Rate>();

        protected class ReducingBalanceCalc
        {
            public double interest;
            public double capital;
        }
        protected List<ReducingBalanceCalc> rbcs = new List<ReducingBalanceCalc>();

        public FormulaSchedule(Environment env, List<int> nperiods, List<Rate> rates, DateTime firstdate, int day_of_month, DateTime drawdowndate, double drawdownamount)
        {
            this.env = env;
            this.nperiods = nperiods;
            this.rates = rates;
            this.firstdate = firstdate;
            this.DrawdownDate = drawdowndate;
            this.DrawdownAmount = drawdownamount;

            nper = 0;
            foreach (int n in nperiods)
            {
                nper += n;
            }

            schedule = new BoundedScheduleAdaptor(new MonthlySchedule(day_of_month, BumpRule.Following, env), firstdate: firstdate, npoints: nper);

            DrawdownAmount = drawdownamount;

            ExpectedAccuracy = -1e0; // Not accurate at all
        }

        #region IRepaymentSchedule Members


        public double interest(DateTime date)
        {
            return interest(findPeriod(date));
        }

        public double capital(DateTime date)
        {
            return capital(findPeriod(date));
        }

        public double interest(int period)
        {
            if (period < 0)
                return 0e0;
            else
            {
                return rbcs[period].interest;
            }
        }

        public double capital(int period)
        {
            if (period < 0)
                return 0e0;
            else
            {
                return rbcs[period].capital;
            }
        }

        #endregion

        protected double rate(int period)
        {
            int n = 0;
            for (int i = 0; i < nperiods.Count; ++i)
            {
                n += nperiods[i];
                if (period < n)
                    return env.RateResolver.GetRate(rates[i], Schedule.advance(this.firstdate, true, period));
            }
            return 0;
        }

        int findPeriod(DateTime date)
        {
            return schedule.diff(schedule.FirstDate.Value, date);
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
    }
}
