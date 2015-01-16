using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corner.Core;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI;
using NikolasHelper.WebAPI.FiveScale;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.MassPres;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.FiveScale.MassPres
{
    public class LoadPrePlanFS
    {



        public string InsertData(string filePath)
        {
            DataTable dt = AccessHelper.GetDataTableByAccessFile(filePath, "防灾预案表");
            PrePlanFSService service = new PrePlanFSService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    PrePlanFS plan = GetPrePlanFS(dt.Rows[i], dt.Columns);
                    service.InsertPrePlanFS(plan);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，统一编号：" + dt.Rows[i]["统一编号"] + "失败原因是：" + ex.Message);
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("防灾预案表(5W)", dt.Rows.Count, successCount,
                failCount, errorStr.ToString());
            return result;
        }

        public PrePlanFS GetPrePlanFS(DataRow row, DataColumnCollection columns)
        {
            PrePlanFS disaster = new PrePlanFS();

            disaster.统一编号 = row["统一编号"].ToString();
            disaster.名称 = row["名称"].ToString();
            disaster.野外编号 = row["野外编号"].ToString();
            //disaster.地理位置 = row["地理位置"].ToString();
            //plan.室内编号 = row["室内编号"].ToString();
            disaster.X坐标 = ConvertHelper.GetIntValueByStr(row["X坐标"].ToString());
            disaster.Y坐标 = ConvertHelper.GetIntValueByStr(row["Y坐标"].ToString());
            disaster.经度 = row["经度"].ToString();
            disaster.纬度 = row["纬度"].ToString();
            disaster.隐患点类型 = row["隐患点类型"].ToString();
            disaster.规模等级 = row["规模等级"].ToString();
            disaster.威胁人口 = ConvertHelper.GetIntValueByStr(row["威胁人口"].ToString());
            disaster.威胁财产 = ConvertHelper.GetDoubleValueFromStr(row["威胁财产"].ToString());
            disaster.险情等级 = row["险情等级"].ToString();
            disaster.曾经发生灾害时间 = row["曾经发生灾害时间"].ToString();
            disaster.地质环境条件 = row["地质环境条件"].ToString();
            disaster.变形特征及历史活动情况 = row["变形特征及历史活动情况"].ToString();
            disaster.稳定性分析 = row["稳定性分析"].ToString();
            disaster.引发因素 = row["引发因素"].ToString();
            disaster.潜在危害 = row["潜在危害"].ToString();
            disaster.临灾状态预测 = row["临灾状态预测"].ToString();
            //plan.监测的主要迹象 = row["监测的主要迹象"].ToString();
            disaster.监测方法 = row["监测方法"].ToString();
            disaster.监测周期 = row["监测周期"].ToString();
            disaster.监测责任人 = row["监测责任人"].ToString();
            disaster.监测责任人电话 = row["监测责任人电话"].ToString();
            disaster.群测群防人员 = row["群测群防人员"].ToString();
            disaster.群测群防人员电话 = row["群测群防人员电话"].ToString();
            disaster.报警方法 = row["报警方法"].ToString();
            disaster.报警信号 = row["报警信号"].ToString();
            disaster.报警人 = row["报警人"].ToString();
            disaster.报警人电话 = row["报警人电话"].ToString();
            disaster.预定避灾地点 = row["预定避灾地点"].ToString();
            disaster.人员撤离路线 = row["人员撤离路线"].ToString();
            disaster.防治建议 = row["防治建议"].ToString();
            //disaster.示意图 = (byte[])(row["示意图"].ToString() == "" ? null : row["示意图"]);
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

            //新增字段

            disaster.省 = row["省"].ToString();
            disaster.市 = row["市"].ToString();
            disaster.县 = row["县"].ToString();
            disaster.乡 = row["乡"].ToString();
            disaster.村 = row["村"].ToString();
            disaster.组 = row["组"].ToString();
            disaster.地点 = row["地点"].ToString();
            disaster.规模 = row["规模"].ToString();
            disaster.监测仪器 = row["监测仪器"].ToString();
            disaster.监测责任人手机 = row["监测责任人手机"].ToString();
            disaster.群测群防人员手机 = row["群测群防人员手机"].ToString();
            disaster.栅格撤离路线图 = ConvertHelper.GetBooleanByStr(row["栅格撤离路线图"].ToString());
            disaster.多媒体 = ConvertHelper.GetBooleanByStr(row["多媒体"].ToString());
            disaster.监测信息 = ConvertHelper.GetBooleanByStr(row["监测信息"].ToString());
            disaster.矢量撤离路线图 = ConvertHelper.GetBooleanByStr(row["矢量撤离路线图"].ToString());


            return disaster;
        }
    }
}
