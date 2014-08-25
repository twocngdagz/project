using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class LandlordFilter : BaseLandlordFilter
    {
        public bool MyPredicate(object olandlord)
        {
            Landlord landlord = (Landlord)olandlord;
            if (!string.IsNullOrEmpty(this.Name))
                if (!landlord.Name.ToUpper().Contains(this.Name.ToUpper()))
                    return false;

            if (this.IsActive)
            {
                bool found_active = false;
                if (landlord.Tenancies != null)
                {
                    foreach (Tenancy t in landlord.Tenancies)
                    {
                        if (t.EndDate == null || t.EndDate >= DateTime.Today)
                        {
                            found_active = true;
                            break;
                        }
                    }
                    if (!found_active)
                        return false;
                }
            }

            if (landlord.IsDeleted != this.IsDeleted)
                return false;
            return true;
        }
    }
}
