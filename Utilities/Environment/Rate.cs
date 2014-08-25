using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class Rate
    {
        public Rate(string ratetype, double spread, int lag=0)
        {
            this.RateType = ratetype;
            this.Spread = spread;
            this.Lag = lag;
        }

        public string RateType
        {
            get;
            set;
        }
        public double Spread
        {
            get;
            set;
        }
        public int Lag
        {
            get;
            set;
        }
    }
}