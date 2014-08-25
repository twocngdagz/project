using System;
using System.Collections.ObjectModel;

namespace Mashisho
{
    interface ITenant
    {
        uint Id { get; set; }
        string Name { get; set; }
        string OtherOccupiers { get; set; }
        string Directory { get; set; }
        ObservableCollection<Tenancy> Tenancies { get; set; }
        bool IsDeleted { get; set; }
    }
}
