using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class RateResolver
    {
        private Dictionary<string, Dictionary<DateTime, double>> rates = new Dictionary<string, Dictionary<DateTime, double>>();
        private Dictionary<string, List<DateTime>> ratelists = new Dictionary<string, List<DateTime>>();

        public void AddRate(string ratetype, double rate, DateTime date)
        {
            if (!rates.ContainsKey(ratetype))
            {
                rates.Add(ratetype, new Dictionary<DateTime, double>());
            }
            rates[ratetype].Add(date, rate);
            ratelists[ratetype] = null;
        }

        public double GetRate(string ratetype, double spread, DateTime date,  int lag=0)
        {
            date = date.AddDays(-lag);

            if (!rates.ContainsKey(ratetype))
            {
                throw new ApplicationException("Unknown rate type in GetRate() : " + ratetype);
            }
            Dictionary<DateTime, double> these_rates = rates[ratetype];
            if (ratelists[ratetype] == null)
            {
                ratelists[ratetype] = new List<DateTime>(these_rates.Keys);
            }
            List<DateTime> ratelist = ratelists[ratetype];

            int index = ratelist.BinarySearch(date);
            if (index >= 0) // We found an exact match
            {
                return spread + these_rates[ratelist[index]];
            }
            else // We found an upper bound
            {
                index = ~index;
                if (index == 0)
                {
                    throw new ApplicationException("Date before smallest rate date defined!");
                }
                return spread + these_rates[ratelist[index-1]];
            }
        }

        public double GetRate(Rate r, DateTime date)
        {
            return GetRate(r.RateType, r.Spread, date, r.Lag);
        }

        private static RateResolver dbresolver = null;

        public static RateResolver DBResolver
        {
            get
            {
                if (dbresolver == null)
                {
                    dbresolver = new RateResolver();
                    foreach (DataRow row in Database.runQuery("select rate_type_name, when_date, rate from vwRateHistory order by rate_type_name, when_date;").Rows)
                    {
                        string ratetype = (string)row["rate_type_name"];
                        double rate = (double)row["rate"];
                        DateTime date = (DateTime)row["when_date"];

                        dbresolver.AddRate(ratetype, rate, date);
                    }
                }
                return dbresolver;
            }
        }

    }
}
