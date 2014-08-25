using System;
using System.Collections.ObjectModel;

namespace Mashisho
{
    interface ILandlordFilter
    {
        string Name { get; set; }
        bool IsActive { get; set; }
        int IsFresh { get; set; }
        bool IsDeleted { get; set; }
    }
}
