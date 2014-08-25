using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class BaseLandlord : BaseObject, ILandlord
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

        public virtual string Address1
        {
            get
            {
                object value = getPropertyValue("Address1");
                return value == null ? default(string) : (string)value;
            }
            set { setPropertyValue("Address1", value); }
        }

        public virtual string Address2
        {
            get
            {
                object value = getPropertyValue("Address2");
                return value == null ? default(string) : (string)value;
            }
            set { setPropertyValue("Address2", value); }
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
            Landlord other = (Landlord)_other;
            if (full)
                this.Id = other.Id;
            this.Name = other.Name;
            this.Address1 = other.Address1;
            this.Address2 = other.Address2;
            this.Directory = other.Directory;
            if (full)
                this.Tenancies = other.Tenancies;
            this.IsDeleted = other.IsDeleted;
        }

        public override string ToString()
        {
            return "Landlord["
                + (object.Equals(Id, null) ? "null" : Id.ToString())
                + "/"
                + (object.Equals(Name, null) ? "null" : Name)
                + "/"
                + (object.Equals(Address1, null) ? "null" : Address1)
                + "/"
                + (object.Equals(Address2, null) ? "null" : Address2)
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
