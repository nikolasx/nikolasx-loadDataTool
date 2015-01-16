using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Relocation;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.Relocation
{
    public class LoadRelocationPlaceEvaluation
    {

        public string InsertData(string filePath)
        {
            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "安置地评价表");
            RelocationService service = new RelocationService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    RelocationPlaceEvaluation place = GetRelocationPlaceEvaluation(dt.Rows[i], dt.Columns);
                    service.InsertRelocationPlaceEvaluation(place);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message);
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("崩塌核查表", dt.Rows.Count, successCount, failCount,
                errorStr.ToString());

            return result;

        }


        public RelocationPlaceEvaluation GetRelocationPlaceEvaluation(DataRow row, DataColumnCollection columns)
        {

            RelocationPlaceEvaluation evaluation = new RelocationPlaceEvaluation();


            evaluation.统一编号 = row["统一编号"].ToString();
            evaluation.野外编号 = row["野外编号"].ToString();
            evaluation.区市 = row["区市"].ToString();
            evaluation.县市区 = row["县市区"].ToString();
            evaluation.乡镇场 = row["乡镇场"].ToString();
            evaluation.村组及地名 = row["村组及地名"].ToString();
            evaluation.X = ConvertHelper.GetDoubleValueFromStr(row["X"].ToString());
            evaluation.Y = ConvertHelper.GetDoubleValueFromStr(row["Y"].ToString());
            evaluation.面积公顷 = row["面积公顷"].ToString();
            evaluation.安置规模户 = row["安置规模户"].ToString();
            evaluation.安置规模人 = ConvertHelper.GetDoubleValueFromStr(row["安置规模人"].ToString());
            evaluation.主要环境地质问题 = row["主要环境地质问题"].ToString();
            evaluation.防治建议 = row["防治建议"].ToString();
            evaluation.适宜性 = row["适宜性"].ToString();
            evaluation.F16 = row["F16"].ToString();
            evaluation.F17 = row["F17"].ToString();
            evaluation.F18 = row["F18"].ToString();
            evaluation.F19 = row["F19"].ToString();
            evaluation.F20 = row["F20"].ToString();


            return evaluation;
        }
    }
}
