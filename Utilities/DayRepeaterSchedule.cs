using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class DayRepeaterSchedule : AbstractUnboundedSchedule
    {
        DateTime reference_date;
        int day_repeat;
        BumpRule bump_rule;
        Environment env;

        public DayRepeaterSchedule(DateTime reference_date, int day_repeat, BumpRule bump_rule, Environment env)
        {
            this.reference_date = reference_date;
            this.day_repeat = day_repeat;
            this.bump_rule = bump_rule;
            this.env = env;
        }

        public override DateTime do_bump(DateTime unbumped_date)
        {
            return DateBump.bump(unbumped_date, bump_rule, env);
        }

        public override List<DateTime> getDates(DateTime start, DateTime end)
        {
            List<DateTime> result = new List<DateTime>();
            foreach (DateTime date in getRawDates(start, end))
            {
                DateTime bumped_date = do_bump(date);

                if (bumped_date >= start && bumped_date <= end)
                    result.Add(bumped_date);
            }
            return result;
        }

        private List<DateTime> getRawDates(DateTime start, DateTime end)
        {
            List<DateTime> result = new List<DateTime>();

            DateTime expanded_start = start.AddMonths(-1);
            DateTime expanded_end = end.AddMonths(1);

            int reference_serial = (int)reference_date.ToOADate();
            int start_serial = (int)expanded_start.ToOADate();

            // Index which will give and indsxate which is on or after expanded_start :
            int index = (int)Math.Floor(((double)(start_serial - reference_serial - 1)) / day_repeat) + 1;
            DateTime indxdate = DateTime.FromOADate((double)(reference_serial + day_repeat * index));

            while (indxdate <= expanded_end)
            {
                result.Add(indxdate);
                ++index;
                indxdate = DateTime.FromOADate((double)(reference_serial + day_repeat * index));
            }

            return result;
        }

        public override DateTime prev_unbumped(DateTime date)
        {
            int diff = ((int)date.ToOADate()) - ((int)reference_date.ToOADate());
            int index = diff / day_repeat;
            return DateTime.FromOADate(((int)reference_date.ToOADate()) + index * day_repeat);
        }

        public override DateTime advance_unbumped(DateTime date, int nperiods)
        {
            return date.AddDays(day_repeat * nperiods);
        }

        public override int diff_unbumped(DateTime from_unbumped, DateTime to_unbumped)
        {
            return ((int)to_unbumped.ToOADate() - (int)from_unbumped.ToOADate()) / day_repeat;
        }

    }
}
