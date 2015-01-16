
using System;
using System.Data;
using System.Text;
using NikolasHelper.GIS;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.MassPres;

namespace 隐患点主表导入工具.MassPres
{
    /// <summary>
    /// 月报速报信息录入
    /// </summary>
    public class LoadMonthlyReport
    {

        public string InsertData(string excelPath)
        {
            DataTable dt = ExcelHelper.GetDataTableByExcelFile(excelPath, "月报速报");

            MonthlyReportService reportService = new MonthlyReportService();
            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    MonthlyReport report = GetMonthlyReport(dt.Rows[i], dt.Columns);
                    reportService.InsertMonthlyReport(report);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message);
                }
            }
            string result = "月报速报表导入结果：\n共" + dt.Rows.Count + "条数据。\n" +
                            "导入成功记录数：" + successCount + "条。 失败记录数：" + failCount + "条。 \n";
            result += errorStr.ToString();
            return result;
        }



        public MonthlyReport GetMonthlyReport(DataRow row, DataColumnCollection columns)
        {
            MonthlyReport report = new MonthlyReport();

            report.灾害类型 = ConvertHelper.GetEnumGeoDisasterByStr(row["灾害类型"].ToString());
            //report.上报时间 = Convert.ToDateTime(row["上报时间"]);
            report.地理位置 = row["乡镇、村小组具体地点"].ToString();
            //report.初始发现时间 = Convert.ToDateTime(row["初始发现时间"]);
            report.市 = row["市"].ToString();
            report.县 = row["县"].ToString();

            //经度，纬度的处理

            string lon = row["经度"].ToString();
            string lat = row["纬度"].ToString();

            if (string.IsNullOrEmpty(lon) || string.IsNullOrEmpty(lat))
            {
                report.经度 = 0;
                report.纬度 = 0;
            }
            else if (lon.Contains("-"))
            {
                report.经度 = LonLatHelper.ConvertToDegreeStyleFromString(lon);
                report.纬度 = LonLatHelper.ConvertToDegreeStyleFromString(lat);
            }
            else if (lon.Contains("°"))
            {
                report.经度 = LonLatHelper.ConvertToDegreeStyleFromDegreeString(lon);
                report.纬度 = LonLatHelper.ConvertToDegreeStyleFromDegreeString(lat);
            }
            //report.经度 = string.IsNullOrEmpty(row["经度"].ToString()) ? 0 : LonLatHelper.ConvertToDegreeStyleFromDegreeString(row["经度"].ToString());
            //report.纬度 = string.IsNullOrEmpty(row["纬度"].ToString()) ? 0 : LonLatHelper.ConvertToDegreeStyleFromDegreeString(row["纬度"].ToString());
            


            report.灾情险情类型 = row["灾情险情类型"].ToString();
            report.达标情况 = row["达标情况"].ToString();
            report.灾害级别 = row["灾害级别"].ToString();
            report.灾害规模 = row["灾害规模"].ToString();
            report.死亡人数 = ConvertHelper.GetIntValueByStr(row["死亡人数"].ToString());
            report.失踪人数 = ConvertHelper.GetIntValueByStr(row["失踪人数"].ToString());
            report.受伤人数 = ConvertHelper.GetIntValueByStr(row["受伤人数"].ToString());
            report.直接经济损失万元 = ConvertHelper.GetDoubleValueFromStr(row["直接经济损失万元"].ToString());
            report.威胁户数 = ConvertHelper.GetIntValueByStr(row["威胁户数"].ToString());
            report.威胁人数 = ConvertHelper.GetIntValueByStr(row["威胁人数"].ToString());
            report.撤离户数 = ConvertHelper.GetIntValueByStr(row["撤离户数"].ToString());
            report.撤离人数 = ConvertHelper.GetIntValueByStr(row["撤离人数"].ToString());
            report.潜在经济损失万元 = ConvertHelper.GetDoubleValueFromStr(row["潜在经济损失万元"].ToString());
            report.地质灾害发生情况 = row["地质灾害发生情况"].ToString();
            report.成因及发展趋势 = row["成因及发展趋势"].ToString();
            report.防灾措施 = row["防灾措施"].ToString();
            report.是否已列入防灾预案 = row["是否已列入防灾预案"].ToString();
            report.备注 = row["备注"].ToString();

            report.GBCodeId = row["市县编码"].ToString();
            report.CustomizeId = row["隐患点编号"].ToString();

            return report;
        }
    }
}
