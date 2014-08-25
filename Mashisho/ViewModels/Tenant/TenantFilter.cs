using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class TenantFilter : BaseTenantFilter
    {
        public bool MyPredicate(object otenant)
        {
            Tenant tenant = (Tenant)otenant;
            if (!string.IsNullOrEmpty(this.Name))
                if (!tenant.Name.ToUpper().Contains(this.Name.ToUpper()))
                    return false;

            if (this.IsActive)
            {
                bool found_active = false;
                if (tenant.Tenancies != null)
                {
                    foreach (Tenancy t in tenant.Tenancies)
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

            if (tenant.IsDeleted != this.IsDeleted)
                return false;
            return true;
        }
    }
}
