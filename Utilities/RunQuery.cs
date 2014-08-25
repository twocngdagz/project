using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace Utilities
{
    public class RunQuery
    {
        public static DataTable execute(string query)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("Database=Accounts;Data Source=localhost;User Id=root;Password="))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter adapt = new MySqlDataAdapter(cmd))
                        {
                            adapt.Fill(dt);
                        }

                        return dt;

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
