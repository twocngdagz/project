using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public interface IBoundedSchedule : IUnboundedSchedule
    {
        // First date in bounded schedule
        DateTime? FirstDate { get; }
        // *One past* the last date in bounded schedule
        DateTime? LastSentinelDate { get; }

        int NPoints { get; set; }
        bool IsFinite { get; }
    }
}
