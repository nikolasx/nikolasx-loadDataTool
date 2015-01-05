using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.MassPres;
using R2.Disaster.CoreEntities.Domain.MineRecovery;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.MassPres
{
    public class LoadPreventionPlanning
    {

        public string InsertData(string filePath)
        {

            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "防治规划");
            PreventionPlanningService service = new PreventionPlanningService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            //TODO 这里先只导入15条数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    PreventionPlanning plan = GetPreventionPlanning(dt.Rows[i], dt.Columns);
                    service.InsertPreventionPlanningService(plan);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "条数据导入失败，原因是：" + ex.Message);
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("防治规划表", dt.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;

        }




        public PreventionPlanning GetPreventionPlanning(DataRow row, DataColumnCollection columns)
        {
            PreventionPlanning plan = new PreventionPlanning();

            plan.灾害类型 = ConvertHelper.GetEnumGeoDisasterByStr(row["灾害类型"].ToString());
            plan.市 = row["市"].ToString();
            plan.县 = row["县"].ToString();
            plan.乡村组 = row["乡村组"].ToString();
            plan.X = ConvertHelper.GetDoubleValueFromStr(row["X"].ToString());
            plan.Y = ConvertHelper.GetDoubleValueFromStr(row["Y"].ToString());
            plan.稳定性易发性 = row["稳定性易发性"].ToString();
            plan.潜在规模面积 = row["潜在规模面积"].ToString();
            plan.规模等级 = row["规模等级"].ToString();
            plan.主要威胁对象 = row["主要威胁对象"].ToString();
            plan.威胁人口户数 = ConvertHelper.GetIntValueByStr(row["威胁人口户数"].ToString());
            plan.威胁人口人数 = ConvertHelper.GetIntValueByStr(row["威胁人口人数"].ToString());
            plan.经济损失评估万元 = ConvertHelper.GetDoubleValueFromStr(row["经济损失评估万元"].ToString());
            plan.险情等级 = row["险情等级"].ToString();
            plan.防治分级 = row["防治分级"].ToString();
            plan.防治分期 = row["防治分期"].ToString();
            plan.防治措施 = row["防治措施"].ToString();
            plan.防治经费万元 = ConvertHelper.GetDoubleValueFromStr(row["防治经费万元"].ToString());
            plan.备注 = row["备注"].ToString();
            plan.GBCodeId = "360105";
            return plan;
        }
    }




}
