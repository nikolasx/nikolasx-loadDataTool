using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corner.Core;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI.FiveScale;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.MassPres;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.FiveScale.MassPres
{
    public class LoadWorkingGuideCardFS
    {

        public string InsertData(string filePath)
        {
            DataTable dt = AccessHelper.GetDataTableByAccessFile(filePath, "工作明白卡");
            PrePlanFSService service = new PrePlanFSService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    WorkingGuideCardFS plan = GetWorkingGuideCard(dt.Rows[i], dt.Columns);
                    service.InsertWorkingGuideCardFS(plan);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，统一编号：" + dt.Rows[i]["统一编号"] + "失败原因是：" + ex.Message);
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("工作明白卡（5W）", dt.Rows.Count, successCount,
                failCount, errorStr.ToString());
            return result;
        }


        public WorkingGuideCardFS GetWorkingGuideCard(DataRow row, DataColumnCollection columns)
        {
            WorkingGuideCardFS disaster = new WorkingGuideCardFS();

            disaster.名称 = row["名称"].ToString();
            disaster.野外编号 = row["野外编号"].ToString();
            disaster.统一编号 = row["统一编号"].ToString();
            disaster.灾害位置 = row["灾害位置"].ToString();
            disaster.类型及规模 = row["类型及规模"].ToString();
            disaster.诱发因素 = row["诱发因素"].ToString();
            disaster.威胁对象 = row["威胁对象"].ToString();
            disaster.监测负责人 = row["监测负责人"].ToString();
            disaster.监测的主要迹象 = row["监测的主要迹象"].ToString();
            disaster.监测的主要手段和方法 = row["监测的主要手段和方法"].ToString();
            disaster.临灾预报的判据 = row["临灾预报的判据"].ToString();
            disaster.预定避灾地点 = row["预定避灾地点"].ToString();
            disaster.预定疏散路线 = row["预定疏散路线"].ToString();
            disaster.预定报警信号 = row["预定报警信号"].ToString();
            disaster.疏散命令发布人 = row["疏散命令发布人"].ToString();
            disaster.疏散值班电话 = row["疏散值班电话"].ToString();
            disaster.抢排险单位及负责人 = row["抢排险单位及负责人"].ToString();
            disaster.抢排险值班电话 = row["抢排险值班电话"].ToString();
            disaster.治安保卫单位及负责人 = row["治安保卫单位及负责人"].ToString();
            disaster.治安保卫值班电话 = row["治安保卫值班电话"].ToString();
            disaster.医疗救护单位及负责人 = row["医疗救护单位及负责人"].ToString();
            disaster.医疗救护值班电话 = row["医疗救护值班电话"].ToString();
            disaster.本卡发放单位 = row["本卡发放单位"].ToString();
            disaster.本卡发放单位联系电话 = row["本卡发放单位联系电话"].ToString();
            //card.发放日期 = row["发放日期"].ToString();
            disaster.持卡单位或个人 = row["持卡单位或个人"].ToString();
            disaster.持卡单位或个人联系电话 = row["持卡单位或个人联系电话"].ToString();
            //card.持卡日期 = row["统一编号"].ToString();


            //新增字段

            disaster.省 = row["省"].ToString();
            disaster.市 = row["市"].ToString();
            disaster.县 = row["县"].ToString();
            //disaster.抢排险单位 = row["抢排险单位"].ToString();
            //disaster.抢排险负责人 = row["抢排险负责人"].ToString();
            //disaster.治安保卫单位 = row["治安保卫单位"].ToString();
            //disaster.治安保卫负责人 = row["治安保卫负责人"].ToString();
            //disaster.医疗救护单位 = row["医疗救护单位"].ToString();
            //disaster.医疗救护负责人 = row["医疗救护负责人"].ToString();
            disaster.发放日期年 = ConvertHelper.GetIntValueByStr(row["发放日期年"].ToString());
            disaster.发放日期月 = ConvertHelper.GetIntValueByStr(row["发放日期月"].ToString());
            disaster.发放日期日 = ConvertHelper.GetIntValueByStr(row["发放日期日"].ToString());
            disaster.持卡日期年 = ConvertHelper.GetIntValueByStr(row["持卡日期年"].ToString());
            disaster.持卡日期月 = ConvertHelper.GetIntValueByStr(row["持卡日期月"].ToString());
            disaster.持卡日期日 = ConvertHelper.GetIntValueByStr(row["持卡日期日"].ToString());

            return disaster;
        }

    }
}
