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
    public class LandlordViewModel : IBaseViewModel
    {
        IUpdater<BaseObject> updater;

        private ObservableCollection<Landlord> all_objects;

        public LandlordViewModel()
        {
            CombinationUpdater<BaseObject> updater_use = new CombinationUpdater<BaseObject>();
            if (true) // Do DB updates
                updater_use.AddUpdater(new LandlordStoreUpdater());
            updater_use.AddUpdater(this);
            this.updater = updater_use;

            all_objects = new ObservableCollection<Landlord>();

            this.MyPartialObject = new PartialLandlord((IUpdater<BaseObject>)this.updater);

            this.MyObjectFilter = new LandlordFilter();
            this.MyObjectFilter.PropertyChanged += new PropertyChangedEventHandler(MyObjectFilter_PropertyChanged);

            LoadAll();

            AllObjects = CollectionViewSource.GetDefaultView(all_objects);
            AllObjects.Filter = ((LandlordFilter)this.MyObjectFilter).MyPredicate;
            AllObjects.MoveCurrentToFirst();
        }

        void MyObjectFilter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnSomeObjectChange();
        }
        void OnSomeObjectChange()
        {
            AllObjects.Filter = ((LandlordFilter)this.MyObjectFilter).MyPredicate;
            if (AllObjects.CurrentItem == null)
                AllObjects.MoveCurrentToFirst();
        }

        public void LoadAll()
        {
            string query = "select * from Landlords;";
            DataTable dt = Utilities.Database.runQuery(query);

            //DataTable dt2 = Utilities.Database.runQuery("select * from vwTenancies order by tenancy_id;");
            //Dictionary<uint, ObservableCollection<Tenancy>> tenancies = new Dictionary<uint, ObservableCollection<Tenancy>>();
            //foreach (DataRow row in dt2.Rows)
            //{
            //    uint tenant_id = (uint)row["tenant_id"];
            //    string rental_unit_short_name = (string)row["rental_unit_short_name"];
            //    DateTime start_date = (DateTime)row["start_date"];
            //    DateTime? end_date = (row["end_date"] is DBNull ? (DateTime?)null : (DateTime)row["end_date"]);
            //    double rent_pcm = (double)row["latest_rent_pcm"];
            //    double deposit = (double)row["latest_deposit"];
            //    if (!tenancies.ContainsKey(tenant_id))
            //    {
            //        tenancies[tenant_id] = new ObservableCollection<Tenancy>();
            //    }
            //    ObservableCollection<Tenancy> this_tenancies = tenancies[tenant_id];
            //    Tenancy t = new Tenancy();
            //    t.RentalUnit = rental_unit_short_name;
            //    t.StartDate = start_date;
            //    t.EndDate = end_date;
            //    t.RentPCM = rent_pcm;
            //    t.Deposit = deposit;
            //    this_tenancies.Add(t);
            //}

            all_objects.Clear();
            foreach (DataRow row in dt.Rows)
            {
                Landlord this_landlord = new Landlord();
                this_landlord.Id = (uint)row["landlord_id"];
                this_landlord.Name = (string)row["landlord_name"];
                this_landlord.Address1 = (string)row["address1"];
                this_landlord.Address2 = (string)row["address2"];
                this_landlord.Directory = (string)row["directory"];

                //if (tenancies.ContainsKey(this_tenant.Id))
                //    this_tenant.Tenancies = tenancies[this_tenant.Id];
                //else
                //    this_tenant.Tenancies = new ObservableCollection<Tenancy>();

                all_objects.Add(this_landlord);
            }
        }

        public override BaseObject Add(BaseObject ofrom)
        {
            Landlord from = (Landlord)ofrom;
            Landlord to = new Landlord();

            to.SetFrom(from, false);

            if (from.Id == 0)
            {
                uint max_id = from.Id;
                foreach (Landlord l in all_objects)
                {
                    max_id = Math.Max(max_id, l.Id);
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
            Landlord l = (Landlord)o;
            l.IsDeleted = !l.IsDeleted;

            OnSomeObjectChange();
        }

        public override void Update(BaseObject ofrom, BaseObject oto)
        {
            Landlord from = (Landlord)ofrom;
            Landlord to = (Landlord)oto;
            to.SetFrom(from, true);

            OnSomeObjectChange();
        }
    }
}