using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class BoundedScheduleAdaptor : IBoundedSchedule
    {
        private IUnboundedSchedule subschedule;

        public BoundedScheduleAdaptor(IUnboundedSchedule subschedule, DateTime? firstdate = null, DateTime? lastsentineldate = null, int? npoints = null)
        {
            this.subschedule = subschedule;
            FirstDate = firstdate;
            LastSentinelDate = lastsentineldate;
            if (npoints != null)
                NPoints = npoints.Value;
        }

        public DateTime? FirstDate
        {
            get;
            private set;
        }

        public DateTime? LastSentinelDate
        {
            get;
            private set;
        }

        public int NPoints
        {
            get
            {
                if (!IsFinite)
                    throw new ApplicationException("Cannot return NPoints in infinite schedule");

                return subschedule.diff(FirstDate.Value, LastSentinelDate.Value);
            }
            set
            {
                if (FirstDate == null && LastSentinelDate == null)
                    throw new ApplicationException("Cannot set # points in doubly unbounded schedule.");

                if (FirstDate != null)
                {
                    DateTime date = subschedule.advance(FirstDate.Value, true, value);
                    if ((LastSentinelDate != null && date > LastSentinelDate.Value))
                        throw new ApplicationException("Cannot expand schedule by setting NPoints");

                    LastSentinelDate = date;
                }
                else
                {
                    FirstDate = subschedule.advance(LastSentinelDate.Value, true, -value); ;
                }
            }
        }

        public bool IsFinite
        {
            get
            {
                return FirstDate != null && LastSentinelDate != null;
            }
        }

        private bool isValid(DateTime date)
        {
            return (FirstDate == null || date >= FirstDate) && (LastSentinelDate == null || date <= LastSentinelDate);
        }

        public List<DateTime> getDates(DateTime start, DateTime end)
        {
            return subschedule.getDates(start, end).FindAll(date => isValid(date));
        }

        public DateTime advance(DateTime date, bool exact, int nperiods)
        {
            if (!isValid(date))
                throw new ApplicationException("Invalid date on input : " + date.ToString());

            DateTime result = subschedule.advance(date, exact, nperiods);
            if (!isValid(result))
                throw new ApplicationException("Invalid date on output : " + result.ToString());
            return result;
        }
        public int diff(DateTime from, DateTime to)
        {
            if (!isValid(from))
                throw new ApplicationException("Invalid date on input : " + from.ToString());
            if (!isValid(to))
                throw new ApplicationException("Invalid date on input : " + to.ToString());

            return subschedule.diff(from, to);
        }
    }
}
