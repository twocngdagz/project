using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class WorldViewModel
    {
        public IBaseViewModel MyTenantViewModel { get; private set; }
        public IBaseViewModel MyLandlordViewModel { get; private set; }

        public WorldViewModel()
        {
            MyTenantViewModel = new TenantViewModel();
            MyLandlordViewModel = new LandlordViewModel();
        }
    }
}
