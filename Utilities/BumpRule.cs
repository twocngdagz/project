using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    //   See for example : http://www.derivativepricing.com/blog/bid/59824/Business-day-conventions-used-for-interest-rate-swaps-other-derivatives
    //
    //    No Adjustment	
    //    Business Holidays are effectively ignored. Cash flows that fall on holidays are assumed to be distributed on the actual date.
    //    
    //    Previous
    //    Cash flows that fall on a non-business day are assumed to be distributed on the previous business day.
    //    
    //    Following
    //    Cash flows that fall on a non-business day are assumed to be distributed on the following business day.
    //    
    //    Modified Previous
    //    Cash flows that fall on a non-business day are assumed to be distributed on the previous business day. However if the previous business day is in a different month, the following business day is adopted instead.
    //    
    //    Modified Following
    //    Cash flows that fall on a non-business day are assumed to be distributed on the following business day. However if the following business day is in a different month, the previous business day is adopted instead.
    //    
    //    End of Month - No Adjustment
    //    All cash flows are assumed to be distributed on the final day of the month (even if it is a non-business day).
    //    
    //    End of Month - Previous
    //    All cash flows are assumed to be distributed on the final day of the month. If the final day of the month is a non-business day, the previous business day is adopted.
    //    
    //    End of Month - Following
    //    All cash flows are assumed to be distributed on the final day of the month. If the final day of the month is a non-business day, the following business day is adopted.
    public enum BumpRule
    {
        None, Following
    }
}
