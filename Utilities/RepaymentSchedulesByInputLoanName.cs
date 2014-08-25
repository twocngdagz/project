using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class RepaymentSchedulesByInputLoanName
    {
        public RepaymentSchedulesByInputLoanName()
        {
            RepaymentSchedules = new Dictionary<string, IRepaymentSchedule>();
        }
        public string InputLoanName
        {
            get;
            set;
        }
        public Dictionary<string, IRepaymentSchedule> RepaymentSchedules
        {
            get;
            set;
        }
    }
}
