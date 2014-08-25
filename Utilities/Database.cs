using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;

namespace Utilities
{
    public class Database
    {
        public static DataTable runQuery(string query, params object[] args)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            for (int i = 0; i + 1 < args.Length; i += 2)
            {
                string name = "@" + (string)args[i];
                object value = args[i + 1];

                parameters.Add(new MySqlParameter(name, value));
            }
            return runParamQuery(query, parameters);
        }

        public static DataTable runParamQuery(string query, List<MySqlParameter> parameters)
        {
            DataTable result = new DataTable();
            using (MySqlConnection conn = new MySqlConnection("Database=Accounts;Data Source=erwin;User Id=<user>;Password=<password>"))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    foreach (MySqlParameter this_param in parameters)
                    {
                        cmd.Parameters.Add(this_param);
                    }
                    using (MySqlDataAdapter adapt = new MySqlDataAdapter(cmd))
                    {
                        adapt.Fill(result);
                    }
                }
            }
            return result;
        }
    }
}
