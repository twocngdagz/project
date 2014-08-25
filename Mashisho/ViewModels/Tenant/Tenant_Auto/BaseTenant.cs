using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class BaseTenant : BaseObject, ITenant
    {
        public virtual uint Id
        {
            get
            {
                object value = getPropertyValue("Id");
                return value == null ? default(uint) : (uint)value;
            }
            set { setPropertyValue("Id", value); }
        }

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

        public virtual ObservableCollection<Tenancy> Tenancies
        {
            get
            {
                object value = getPropertyValue("Tenancies");
                return value == null ? default(ObservableCollection<Tenancy>) : (ObservableCollection<Tenancy>)value;
            }
            set { setPropertyValue("Tenancies", value); }
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
            Tenant other = (Tenant)_other;
            if (full)
                this.Id = other.Id;
            this.Name = other.Name;
            this.OtherOccupiers = other.OtherOccupiers;
            this.Directory = other.Directory;
            if (full)
                this.Tenancies = other.Tenancies;
            this.IsDeleted = other.IsDeleted;
        }

        public override string ToString()
        {
            return "Tenant["
                + (object.Equals(Id, null) ? "null" : Id.ToString())
                + "/"
                + (object.Equals(Name, null) ? "null" : Name)
                + "/"
                + (object.Equals(OtherOccupiers, null) ? "null" : OtherOccupiers)
                + "/"
                + (object.Equals(Directory, null) ? "null" : Directory)
                + "/"
                + (object.Equals(Tenancies, null) ? "null" : Tenancies.Count.ToString())
                + "/"
                + (object.Equals(IsDeleted, null) ? "null" : IsDeleted.ToString())
                + "]";
        }
    }
}
