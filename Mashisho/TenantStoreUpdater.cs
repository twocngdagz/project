using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class TenantStoreUpdater : IUpdater<BaseObject>
    {
        private static string rootdir = @"p:\Tenancies";
        public BaseObject Add(BaseObject o)
        {
            Tenant t = (Tenant)o;
            string query = "insert into Tenants (tenant_short_name, other_occupiers, directory) values (@tenant_short_name, @other_occupiers, @directory);";
            Utilities.Database.runQuery(query, "tenant_short_name", t.Name, "other_occupiers", t.OtherOccupiers, "directory", t.Directory);
            query = "select tenant_id from Tenants where tenant_short_name=@tenant_short_name and other_occupiers=@other_occupiers and directory=@directory;";
            DataTable dt = Utilities.Database.runQuery(query, "tenant_short_name", t.Name, "other_occupiers", t.OtherOccupiers, "directory", t.Directory);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (t.Directory.Trim() != "")
                        Directory.CreateDirectory(Path.Combine(rootdir, t.Directory));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error creating directory : " + e.Message);
                }


                Tenant result = new Tenant();
                result.SetFrom(t, false);
                result.Id = (uint)dt.Rows[0]["tenant_id"];
                return result;
            }
            else
            {
                throw new ApplicationException("Could not find tenant again after insert!");
            }

        }

        public void Delete(BaseObject o)
        {
            Tenant t = (Tenant)o;

            string query = "delete from Tenants where tenant_id = @tenant_id;";
            Utilities.Database.runQuery(query, "tenant_id", t.Id);

            //try
            //{
            //    if (t.Directory.Trim() != "")
            //        Directory.Delete(Path.Combine(rootdir, t.Directory));
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("Error deleting directory : " + e.Message);
            //}
        }

        public void Update(BaseObject ofrom, BaseObject oto)
        {
            Tenant from = (Tenant)ofrom;
            Tenant to = (Tenant)oto;

            string query = "update Tenants set tenant_short_name=@tenant_short_name, other_occupiers=@other_occupiers, directory=@directory where tenant_id=@tenant_id;";
            Utilities.Database.runQuery(query, "tenant_short_name", from.Name, "other_occupiers", from.OtherOccupiers, "directory", from.Directory, "tenant_id", to.Id);

            try
            {
                if (from.Directory != to.Directory && from.Directory.Trim() != "" && to.Directory.Trim() != "")
                    Directory.Move(Path.Combine(rootdir, to.Directory), Path.Combine(rootdir, from.Directory));
            }
            catch (Exception e)
            {
                MessageBox.Show("Error moving directory : " + e.Message);
            }
        }
    }
}