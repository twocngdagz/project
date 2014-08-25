using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class BaseLandlordFilter : BaseObject, ILandlordFilter
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

        public virtual bool IsActive
        {
            get
            {
                object value = getPropertyValue("IsActive");
                return value == null ? default(bool) : (bool)value;
            }
            set { setPropertyValue("IsActive", value); }
        }

        public virtual int IsFresh
        {
            get
            {
                object value = getPropertyValue("IsFresh");
                return value == null ? default(int) : (int)value;
            }
            set { setPropertyValue("IsFresh", value); }
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

        public override void SetFrom(BaseObject _other, bool full = true)
        {
            LandlordFilter other = (LandlordFilter)_other;
            this.Name = other.Name;
            this.IsActive = other.IsActive;
            if (full)
                this.IsFresh = other.IsFresh;
            this.IsDeleted = other.IsDeleted;
        }

        public override string ToString()
        {
            return "LandlordFilter["
                + (object.Equals(Name, null) ? "null" : Name)
                + "/"
                + (object.Equals(IsActive, null) ? "null" : IsActive.ToString())
                + "/"
                + (object.Equals(IsFresh, null) ? "null" : IsFresh.ToString())
                + "/"
                + (object.Equals(IsDeleted, null) ? "null" : IsDeleted.ToString())
                + "]";
        }
    }
}
