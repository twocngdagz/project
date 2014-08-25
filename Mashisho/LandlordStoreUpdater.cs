using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace Mashisho
{
    public class LandlordStoreUpdater : IUpdater<BaseObject>
    {
        private static string rootdir = @"p:\OurLandlords";
        public BaseObject Add(BaseObject o)
        {
            Landlord l = (Landlord)o;
            string query = "insert into Landlords (landlord_name, address1, address2, directory) values (@landlord_name, @address1, @address2, @directory);";
            Utilities.Database.runQuery(query, "landlord_name", l.Name, "address1", l.Address1, "address2", l.Address2, "directory", l.Directory);
            query = "select landlord_id from Landlords where landlord_name=@landlord_name and address1=@address1 and address2=@address2 and directory=@directory;";
            DataTable dt = Utilities.Database.runQuery(query, "landlord_name", l.Name, "address1", l.Address1, "address2", l.Address2, "directory", l.Directory);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (l.Directory.Trim() != "")
                        Directory.CreateDirectory(Path.Combine(rootdir, l.Directory));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error creating directory : " + e.Message);
                }


                Landlord result = new Landlord();
                result.SetFrom(l, false);
                result.Id = (uint)dt.Rows[0]["landlord_id"];
                return result;
            }
            else
            {
                throw new ApplicationException("Could not find tenant again after insert!");
            }
        }

        public void Delete(BaseObject o)
        {
            Landlord l = (Landlord)o;

            string query = "delete from Landlords where landlord_id = @landlord_id;";
            Utilities.Database.runQuery(query, "landlord_id", l.Id);

            //try
            //{
            //    if (l.Directory.Trim() != "")
            //        Directory.Delete(Path.Combine(rootdir, l.Directory));
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("Error deleting directory : " + e.Message);
            //}
        }

        public void Update(BaseObject ofrom, BaseObject oto)
        {
            Landlord from = (Landlord)ofrom;
            Landlord to = (Landlord)oto;

            string query = "update Landlords set landlord_name=@landlord_name, address1=@address1, address2=@address2, directory=@directory where landlord_id=@landlord_id;";
            Utilities.Database.runQuery(query, "landlord_name", from.Name, "address1", from.Address1, "address2", from.Address2, "directory", from.Directory, "landlord_id", to.Id);

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