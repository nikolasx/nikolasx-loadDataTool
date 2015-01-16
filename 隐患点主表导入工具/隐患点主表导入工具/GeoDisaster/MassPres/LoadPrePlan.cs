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
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.MassPres
{
    /// <summary>
    /// 防灾预案
    /// </summary>
    public class LoadPrePlan
    {

        public string InsertData(string filePath)
        {
            DataTable dt = AccessHelper.GetDataTableByAccessFile(filePath, "地质灾害隐患点防灾预案表");
            PrePlanService service = new PrePlanService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    PrePlan plan = GetPrePlan(dt.Rows[i], dt.Columns);
                    service.InsertPrePlan(plan);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，统一编号:" + dt.Rows[i]["统一编号"] + ";失败原因是：" + ex.Message);
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("防灾预案", dt.Rows.Count, successCount,
                failCount, errorStr.ToString());
            return result;
        }


        public PrePlan GetPrePlan(DataRow row, DataColumnCollection columns)
        {
            PrePlan plan = new PrePlan();

            plan.统一编号 = row["统一编号"].ToString();
            plan.名称 = row["名称"].ToString();
            plan.野外编号 = row["野外编号"].ToString();
            plan.地理位置 = row["地理位置"].ToString();
            //plan.室内编号 = row["室内编号"].ToString();
            plan.X坐标 = ConvertHelper.GetDoubleValueFromStr(row["X坐标"].ToString());
            plan.Y坐标 = ConvertHelper.GetDoubleValueFromStr(row["Y坐标"].ToString());
            plan.经度 = row["经度"].ToString();
            plan.纬度 = row["纬度"].ToString();
            plan.隐患点类型 = ConvertHelper.GetEnumGeoDisasterByStr(row["隐患点类型"].ToString());
            plan.规模等级 = row["规模等级"].ToString();
            plan.威胁人口 = ConvertHelper.GetIntValueByStr(row["威胁人口"].ToString());
            plan.威胁财产 = ConvertHelper.GetDoubleValueFromStr(row["威胁财产"].ToString());
            plan.险情等级 = row["险情等级"].ToString();
            plan.曾经发生灾害时间 = row["曾经发生灾害时间"].ToString();
            plan.地质环境条件 = row["地质环境条件"].ToString();
            plan.变形特征及历史活动情况 = row["变形特征及历史活动情况"].ToString();
            plan.稳定性分析 = row["稳定性分析"].ToString();
            plan.引发因素 = row["引发因素"].ToString();
            plan.潜在危害 = row["潜在危害"].ToString();
            plan.临灾状态预测 = row["临灾状态预测"].ToString();
            //plan.监测的主要迹象 = row["监测的主要迹象"].ToString();
            plan.监测方法 = row["监测方法"].ToString();
            plan.监测周期 = row["监测周期"].ToString();
            plan.监测责任人 = row["监测责任人"].ToString();
            plan.监测责任人电话 = row["监测责任人电话"].ToString();
            plan.群测群防人员 = row["群测群防人员"].ToString();
            plan.群测群防人员电话 = row["群测群防人员电话"].ToString();
            plan.报警方法 = row["报警方法"].ToString();
            plan.报警信号 = row["报警信号"].ToString();
            plan.报警人 = row["报警人"].ToString();
            plan.报警人电话 = row["报警人电话"].ToString();
            plan.预定避灾地点 = row["预定避灾地点"].ToString();
            plan.人员撤离路线 = row["人员撤离路线"].ToString();
            plan.防治建议 = row["防治建议"].ToString();
            plan.示意图 = (byte[])(row["示意图"].ToString() == "" ? null : row["示意图"]);
            //plan.疏散命令发布人 = row["疏散命令发布人"].ToString();
            //plan.疏散值班电话 = row["疏散值班电话"].ToString();
            //plan.排险负责人 = row["排险负责人"].ToString();
            //plan.排险值班电话 = row["排险值班电话"].ToString();
            //plan.治安保卫单位 = row["治安保卫单位"].ToString();
            //plan.治安保卫负责人 = row["治安保卫负责人"].ToString();
            //plan.治安保卫值班电话 = row["治安保卫值班电话"].ToString();
            //plan.医疗救护单位 = row["医疗救护单位"].ToString();
            //plan.医疗救护负责人 = row["医疗救护负责人"].ToString();
            //plan.医疗救护值班电话 = row["医疗救护值班电话"].ToString();
            //plan.撤离路线图 = (byte[])row["撤离路线图"];




            return plan;
        }
    }
}
