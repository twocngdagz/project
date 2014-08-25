using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Data;

using Utilities;

namespace Mashisho
{
    public class TenantViewModel : IBaseViewModel
    {
        IUpdater<BaseObject> updater;

        private ObservableCollection<BaseObject> all_objects;

        public TenantViewModel()
        {
            CombinationUpdater<BaseObject> updater_use = new CombinationUpdater<BaseObject>();
            if (true) // Do DB updates
                updater_use.AddUpdater(new TenantStoreUpdater());
            updater_use.AddUpdater(this);
            this.updater = updater_use;

            all_objects = new ObservableCollection<BaseObject>();

            this.MyPartialObject = new PartialTenant((IUpdater<BaseObject>)this.updater);

            this.MyObjectFilter = new TenantFilter();
            this.MyObjectFilter.PropertyChanged += new PropertyChangedEventHandler(MyObjectFilter_PropertyChanged);

            LoadAll();

            AllObjects = CollectionViewSource.GetDefaultView(all_objects);
            AllObjects.Filter = ((TenantFilter)this.MyObjectFilter).MyPredicate;
            AllObjects.MoveCurrentToFirst();
        }

        void MyObjectFilter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnSomeObjectChange();
        }
        void OnSomeObjectChange()
        {
            AllObjects.Filter = ((TenantFilter)this.MyObjectFilter).MyPredicate;
            if (AllObjects.CurrentItem == null)
                AllObjects.MoveCurrentToFirst();
        }

        public void LoadAll()
        {
            string query = "select * from Tenants;";
            DataTable dt = Utilities.Database.runQuery(query);

            DataTable dt2 = Utilities.Database.runQuery("select * from vwTenancies order by tenancy_id;");
            Dictionary<uint, ObservableCollection<Tenancy>> tenancies = new Dictionary<uint, ObservableCollection<Tenancy>>();
            foreach (DataRow row in dt2.Rows)
            {
                uint tenant_id = (uint)row["tenant_id"];
                string rental_unit_short_name = (string)row["rental_unit_short_name"];
                DateTime start_date = (DateTime)row["start_date"];
                DateTime? end_date = (row["end_date"] is DBNull ? (DateTime?)null : (DateTime)row["end_date"]);
                double rent_pcm = (double)row["latest_rent_pcm"];
                double deposit = (double)row["latest_deposit"];
                if (!tenancies.ContainsKey(tenant_id))
                {
                    tenancies[tenant_id] = new ObservableCollection<Tenancy>();
                }
                ObservableCollection<Tenancy> this_tenancies = tenancies[tenant_id];
                Tenancy t = new Tenancy();
                t.RentalUnit = rental_unit_short_name;
                t.StartDate = start_date;
                t.EndDate = end_date;
                t.RentPCM = rent_pcm;
                t.Deposit = deposit;
                this_tenancies.Add(t);
            }

            all_objects.Clear();
            foreach (DataRow row in dt.Rows)
            {
                Tenant this_tenant = new Tenant();
                this_tenant.Id = (uint)row["tenant_id"];
                this_tenant.Name = (string)row["tenant_short_name"];
                this_tenant.OtherOccupiers = (string)row["other_occupiers"];
                this_tenant.Directory = (string)row["directory"];

                if (tenancies.ContainsKey(this_tenant.Id))
                    this_tenant.Tenancies = tenancies[this_tenant.Id];
                else
                    this_tenant.Tenancies = new ObservableCollection<Tenancy>();

                all_objects.Add(this_tenant);
            }
        }

        public override BaseObject Add(BaseObject ofrom)
        {
            Tenant from = (Tenant)ofrom;
            Tenant to = new Tenant();

            to.SetFrom(from, false);

            if (from.Id == 0)
            {
                uint max_id = from.Id;
                foreach (Tenant t in all_objects)
                {
                    max_id = Math.Max(max_id, t.Id);
                }
                to.Id = max_id + 1;
            }
            else
            {
                to.Id = from.Id;
            }
            all_objects.Add(to);

            OnSomeObjectChange();

            return to;
        }

        public override void Delete(BaseObject o)
        {
            Tenant t = (Tenant)o;
            t.IsDeleted = !t.IsDeleted;

            OnSomeObjectChange();
        }

        public override void Update(BaseObject ofrom, BaseObject oto)
        {
            oto.SetFrom(ofrom, true);

            OnSomeObjectChange();
        }
    }
}