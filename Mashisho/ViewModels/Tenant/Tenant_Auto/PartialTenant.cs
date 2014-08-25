using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class PartialTenant : Tenant, IPartialObject
    {
        IUpdater<BaseObject> myupdater;
        BaseObject other;
        BaseObject myprivate;

        public PartialTenant(IUpdater<BaseObject> myupdater)
        {
            this.myupdater = myupdater;

            Clear();
        }

        public BaseObject SetObject
        {
            set
            {
                other = (value == null ? new Tenant() : value);
                if (myprivate != null)
                    myprivate.PropertyChanged -= myprivate_PropertyChanged;
                myprivate = null;
                this.OnPropertyChanged("");
            }
        }

        private BaseObject GetObjectForGet()
        {
            return myprivate == null ? other : myprivate;
        }
        private BaseObject GetObjectForSet()
        {
            if (myprivate == null)
            {
                myprivate = new Tenant();
                myprivate.SetFrom(other, true);
                myprivate.PropertyChanged += myprivate_PropertyChanged;
            }
            return myprivate;
        }

        void myprivate_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(e.PropertyName);
        }

        public override object getPropertyValue(string property_name)
        {
            return GetObjectForGet().getPropertyValue(property_name);
        }

        public override void setPropertyValue(string property_name, object value)
        {
            GetObjectForSet().setPropertyValue(property_name, value);
        }

        public override string ToString()
        {
            return "other: [" + (other == null ? "null" : other.ToString()) + "] myprivate: [" +
                (myprivate == null ? "null" : myprivate.ToString()) + "]";
        }

        public void Add()
        {
            if (other != null && myprivate != null)
            {
                ((Tenant)myprivate).Id = 0;
                myupdater.Add(myprivate);

                myprivate.PropertyChanged -= myprivate_PropertyChanged;
                myprivate = null;
            }
        }

        public void Update()
        {
            if (other != null && myprivate != null)
            {
                myupdater.Update(myprivate, other);

                myprivate.PropertyChanged -= myprivate_PropertyChanged;
                myprivate = null;
            }
        }
        public void Delete()
        {
            if (other != null)
            {
                myupdater.Delete(other);
                
                if (myprivate != null)
                {
                    myprivate.PropertyChanged -= myprivate_PropertyChanged;
                    myprivate = null;
                }
            }
        }
        public void Clear()
        {
            SetObject = null;
        }
    }
}
