using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Emergency;

namespace 隐患点主表导入工具.MassPres
{
    /// <summary>
    /// 应急调查信息录入
    /// </summary>
    public class LoadEmergencySurvey
    {

        public string InsertData(string filePath)
        {
            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "应急调查");

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();
            EmergencySurveyReportService reportService = new EmergencySurveyReportService();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    EmergencySurveyReport report = GetEmergencySurvey(dt.Rows[i], dt.Columns);
                    reportService.InsertEmergencySurveyReport(report);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message + "\n");
                }

            }
            string result = "应急调查报告表导入结果：\n共" + dt.Rows.Count + "条数据。\n" +
                            "导入成功记录数：" + successCount + "条。 失败记录数：" + failCount + "条。 \n";
            result += errorStr.ToString();
            return result;
        }


        /// <summary>
        /// 获取应急调查信息
        /// </summary>
        public EmergencySurveyReport GetEmergencySurvey(DataRow row, DataColumnCollection columns)
        {

            EmergencySurveyReport survey = new EmergencySurveyReport();


            survey.报告年份 = ConvertHelper.GetIntValueByStr(row["报告年份"].ToString());
            survey.报告名称 = row["报告名称"].ToString();
            survey.灾点名称 = row["灾点名称"].ToString();
            survey.灾害类型 = ConvertHelper.GetEnumGeoDisasterByStr(row["灾害类型"].ToString());
            survey.设区市 = row["设区市"].ToString();
            survey.县 = row["县"].ToString();
            survey.乡镇 = row["乡镇"].ToString();
            survey.村 = row["村"].ToString();
            survey.组 = row["组"].ToString();
            //TODO 以后看到实际数据后，根据实际数据转换经纬度值，填入
            //survey.经度 = row["经度"].ToString();
            //survey.纬度 = row["纬度"].ToString();
            survey.X坐标 = ConvertHelper.GetDoubleValueFromStr(row["X"].ToString());
            survey.Y坐标 = ConvertHelper.GetDoubleValueFromStr(row["Y"].ToString());
            survey.调查单位 = row["调查单位"].ToString();
            //survey.调查时间 = row["调查时间"].ToString();
            survey.图件文件夹名称 = row["图件文件夹名称"].ToString();
            survey.是否历史数据 = GetBooleanByStr(row["是否历史数据"].ToString());
            survey.文档位置 = row["文档位置"].ToString();

            survey.GBCodeId = row["市县编码"].ToString();
            survey.CustomizeId = row["隐患点编号"].ToString();


            return survey;
        }


        private bool GetBooleanByStr(string str)
        {
            bool ans = false;
            if (str.Contains("是"))
            {
                ans = true;
            }
            return ans;
        }
    }
}
