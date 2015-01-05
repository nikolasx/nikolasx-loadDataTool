using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NikolasHelper.SQL
{
    public class SqlHelper
    {
        private const string Con = "server=192.168.83.122;database=JXDZDataCenter;uid=sa;pwd=guest";

        public static DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();

            using (var sqlCon = new SqlConnection(Con))
            {
                sqlCon.Open();
                using (var adapter = new SqlDataAdapter(sql, sqlCon))
                {
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
