using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public interface IRepaymentSchedule
    {
        double interest(DateTime date);
        double capital(DateTime date);

        double interest(int period);
        double capital(int period);
        
        DateTime DrawdownDate { get; }
        double DrawdownAmount { get; }

        IBoundedSchedule Schedule { get; }

        double ExpectedAccuracy { get; }
    }
}
