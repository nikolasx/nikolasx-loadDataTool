using System.Data;
using System.Data.OleDb;


namespace NikolasHelper.Office
{
    public class ExcelHelper
    {
        private const string Con = "Provider=Microsoft.ACE.OleDb.12.0;Extended Properties='Excel 8.0;HDR=YES;IMEX=0;'";

        /// <summary>
        /// 根据Excel文件，获取该文件的DataTable表格数据
        /// 隐患点主表：tableName=隐患点主表
        /// </summary>
        public static DataTable GetDataTableByExcelFile(string filePath, string tableName)
        {
            string conString = Con + "; Data Source=" + filePath + ";";
            var conn = new OleDbConnection(conString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            string sql = "Select * from [" + tableName + "$]";
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }




        public static void UpdateExcelValue(string filePath, string yhd, string bh, string tableName, string column1, string column2)
        {
            string conString = Con + "; Data Source=" + filePath + ";";
            var conn = new OleDbConnection(conString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            string sql = "update [" + tableName + "$] set " + column1 + " = '" + yhd + "' where " + column2 +
                " = '" + bh + "'";

            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}
