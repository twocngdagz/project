using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class Environment
    {
        private static Environment standard_environment = null;
        public static Environment StandardEnvironment
        {
            get
            {
                if (standard_environment == null)
                {
                    standard_environment = new Environment();
                    standard_environment.Calendar = new UKCalendar();
                    standard_environment.RateResolver = RateResolver.DBResolver;
                }
                return standard_environment;
            }
        }
        private Environment()
        {
        }

        public Calendar Calendar
        {
            get;
            private set;
        }

        public RateResolver RateResolver
        {
            get;
            private set;
        }
    }
}
