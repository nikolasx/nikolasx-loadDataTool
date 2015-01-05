
namespace 隐患点主表导入工具.Util
{
    public class UtilMethod
    {



        /// <summary>
        /// 获得插入结果打印信息
        /// </summary>
        public static string GetPrintErrorInfo(string tableName, int count, int successCount, int failCount, string errorString)
        {
            string ans;

            ans = "\n" + tableName + "导入结果：\n"
                  + "总记录数：" + count + "\t成功记录数：" + successCount + "失败记录数：" + failCount + "\n"
                  + errorString;
            return ans;

        }
    }
}
