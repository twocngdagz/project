using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class LoanSplitResult
    {
        public double OriginalInterest
        {
            get;
            set;
        }
        public double OriginalCapital
        {
            get;
            set;
        }
        public double AdjustedInterest
        {
            get;
            set;
        }
        public double AdjustedCapital
        {
            get;
            set;
        }
    }
}
