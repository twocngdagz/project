using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class SimpleListSchedule : IBoundedSchedule
    {
        List<DateTime> list;
        DateTime sentinel;

        public SimpleListSchedule(List<DateTime> list)
        {
            this.list = list;
            sentinel = list.Count > 0 ? list[list.Count - 1].AddDays(1) : new DateTime(1900, 1, 1);
        }

        public DateTime? FirstDate
        {
            get
            {
                if (list.Count > 0)
                    return list[0];
                else
                    return sentinel;
            }
        }

        public DateTime? LastSentinelDate
        {
            get
            {
                return sentinel;
            }
        }

        public int NPoints
        {
            get
            {
                return list.Count;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsFinite
        {
            get { return true; }
        }

        public List<DateTime> getDates(DateTime start, DateTime end)
        {
            return list.FindAll(date => date >= start && date <= end);
        }

        public DateTime advance(DateTime date, bool exact, int nperiods)
        {
            if (!exact)
            {
                foreach (DateTime mydate in list)
                {
                    if (mydate > date)
                        return advance(mydate, true, nperiods - 1);
                }
                if (nperiods > 1)
                    throw new ApplicationException("Outside schedule");
                return sentinel;
            }

            int indx = getIndex(date);
            if (indx + nperiods < 0 || indx + nperiods > list.Count)
                throw new ApplicationException("Outside schedule");

            return indx + nperiods == list.Count ? sentinel : list[indx + nperiods];
        }

        public int diff(DateTime from, DateTime to)
        {
            int indx_from = getIndex(from);
            int indx_to = getIndex(to);

            return indx_to - indx_from;
        }

        private int getIndex(DateTime date)
        {
            int indx = date == sentinel ? list.Count : list.IndexOf(date);
            if (indx == -1)
                throw new ApplicationException("Date is not part of schedule: " + date.ToString());
            return indx;
        }

    }
}
