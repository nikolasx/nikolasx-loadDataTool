using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NikolasHelper.Office
{
    /// <summary>
    /// Access文件读写操作类
    /// </summary>
    public class AccessHelper
    {
        public const string Con = "Provider=Microsoft.Jet.OleDb.4.0;";//+ "data source=";


        public static DataTable GetDataTableByAccessFile(string filePath,string tableName)
        {
            string conString = Con + "data source=" + filePath + ";";
            var conn = new OleDbConnection(conString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            string sql = "Select * from [" + tableName + "]";
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }
    }
}
