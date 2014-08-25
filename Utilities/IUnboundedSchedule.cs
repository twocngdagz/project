using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    // An IUnboundedSchedule extends in principle infinitely backwards and forwards, to look at its realisation provide start and end dates.
    //
    // Example of schedule: "the 25th of every month"
    //                      "every four weeks"
    public interface IUnboundedSchedule
    {
        List<DateTime> getDates(DateTime start, DateTime end);

        //
        // Sample schedule:   10/02/2013 10/03/2013 07/04/2013 05/05/2013 02/06/2013
        //
        //  Called with 10/03 -> advance(-1) will return 10/02.
        //                       advance(+1) will return 07/04.
        //
        //  exact:
        //       * if true, input needs to be exactly 10/03.
        //       * if false,  11/02 through to 09/03 will return 10/02 as the previous day (advance(-1)).
        //                    10/03 through to 06/04 will return 07/04 as the next day (advance(+1)).
        //
        DateTime advance(DateTime date, bool exact, int nperiods);

        // Reverse of advance() - both dates should be part of the schedule :
        int diff(DateTime from, DateTime to);
    }
}
