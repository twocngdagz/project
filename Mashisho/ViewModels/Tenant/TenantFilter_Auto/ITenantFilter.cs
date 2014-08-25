using System;
using System.Collections.ObjectModel;

namespace Mashisho
{
    interface ITenantFilter
    {
        string Name { get; set; }
        string OtherOccupiers { get; set; }
        string Directory { get; set; }
        bool IsActive { get; set; }
        bool IsDeleted { get; set; }
    }
}
