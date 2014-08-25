using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public abstract class AbstractUnboundedSchedule : IUnboundedSchedule
    {
        public abstract List<DateTime> getDates(DateTime start, DateTime end);
        public abstract DateTime prev_unbumped(DateTime date);
        public abstract DateTime do_bump(DateTime date);
        public abstract DateTime advance_unbumped(DateTime date, int nperiods);
        public abstract int diff_unbumped(DateTime from_unbumped, DateTime to_unbumped);


        // Return the unbumped date, u, for which bump(u) <= date 
        public DateTime bumped_prev_unbumped(DateTime org_date, bool exact)
        {
            DateTime date = prev_unbumped(org_date);
            DateTime bumped_date = do_bump(date);
            if (bumped_date == org_date)
            {
            }
            else if (bumped_date < org_date)
            {
                while (bumped_date <= org_date)
                {
                    date = advance_unbumped(date, 1);
                    bumped_date = do_bump(date);
                }
                date = advance_unbumped(date, -1);
            }
            else if (bumped_date > org_date)
            {
                while (bumped_date > org_date)
                {
                    date = advance_unbumped(date, -1);
                    bumped_date = do_bump(date);
                }
            }
            bumped_date = do_bump(date);
            if (exact && bumped_date != org_date)
                throw new ApplicationException("Could not match date in schedule : " + org_date.ToString());

            return date;
        }


        public DateTime advance(DateTime date, bool exact, int nperiods)
        {
            DateTime unbumped = bumped_prev_unbumped(date, exact);
            bool was_exact = do_bump(unbumped) == date;
            if (exact && !was_exact)
                throw new ApplicationException("Date is not part of schedule : " + date.ToString());

            return do_bump(advance_unbumped(unbumped, nperiods + ((nperiods < 0 && !was_exact) ? 1 : 0)));
        }
        public int diff(DateTime to, DateTime from)
        {
            DateTime to_unbumped = bumped_prev_unbumped(to, true);
            DateTime from_unbumped = bumped_prev_unbumped(from, true);
            return diff_unbumped(to_unbumped, from_unbumped);
        }
    }
}
