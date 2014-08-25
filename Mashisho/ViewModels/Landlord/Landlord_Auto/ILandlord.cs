using System;
using System.Collections.ObjectModel;

namespace Mashisho
{
    interface ILandlord
    {
        uint Id { get; set; }
        string Name { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string Directory { get; set; }
        ObservableCollection<Tenancy> Tenancies { get; set; }
        bool IsDeleted { get; set; }
    }
}
