using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class BaseTenantFilter : HTObservableObject, ITenantFilter
    {
        public virtual string Name
        {
            get
            {
                object value = getPropertyValue("Name");
                return value == null ? default(string) : (string)value;
            }
            set { setPropertyValue("Name", value); }
        }

        public virtual string OtherOccupiers
        {
            get
            {
                object value = getPropertyValue("OtherOccupiers");
                return value == null ? default(string) : (string)value;
            }
            set { setPropertyValue("OtherOccupiers", value); }
        }

        public virtual string Directory
        {
            get
            {
                object value = getPropertyValue("Directory");
                return value == null ? default(string) : (string)value;
            }
            set { setPropertyValue("Directory", value); }
        }

        public virtual bool IsActive
        {
            get
            {
                object value = getPropertyValue("IsActive");
                return value == null ? default(bool) : (bool)value;
            }
            set { setPropertyValue("IsActive", value); }
        }

        public virtual bool IsDeleted
        {
            get
            {
                object value = getPropertyValue("IsDeleted");
                return value == null ? default(bool) : (bool)value;
            }
            set { setPropertyValue("IsDeleted", value); }
        }

        public void SetFrom(TenantFilter other, bool full = true)
        {
            this.Name = other.Name;
            this.OtherOccupiers = other.OtherOccupiers;
            this.Directory = other.Directory;
            this.IsActive = other.IsActive;
            this.IsDeleted = other.IsDeleted;
        }

        public override string ToString()
        {
            return "TenantFilter["
                + (object.Equals(Name, null) ? "null" : Name)
                + "/"
                + (object.Equals(OtherOccupiers, null) ? "null" : OtherOccupiers)
                + "/"
                + (object.Equals(Directory, null) ? "null" : Directory)
                + "/"
                + (object.Equals(IsActive, null) ? "null" : IsActive.ToString())
                + "/"
                + (object.Equals(IsDeleted, null) ? "null" : IsDeleted.ToString())
                + "]";
        }
    }
}
