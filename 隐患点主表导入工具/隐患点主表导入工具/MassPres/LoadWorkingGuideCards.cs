using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikolasHelper.Office;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.MassPres;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.MassPres
{
    public class LoadWorkingGuideCards
    {

        /// <summary>
        /// 工作明白卡
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>

        public string InsertData(string filePath)
        {
            DataTable dt = AccessHelper.GetDataTableByAccessFile(filePath, "工作明白卡");
            PrePlanService service = new PrePlanService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    WorkingGuideCard card = GetWorkingGuideCard(dt.Rows[i], dt.Columns);
                    service.InsertWorkingGuideCards(card);
                }
                catch (Exception ex)
                {
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message);
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("工作明白卡", dt.Rows.Count, successCount,
                failCount, errorStr.ToString());
            return result;
        }



        public WorkingGuideCard GetWorkingGuideCard(DataRow row,DataColumnCollection columns)
        {
            WorkingGuideCard card = new WorkingGuideCard();

            card.名称 = row["名称"].ToString();
            card.野外编号 = row["野外编号"].ToString();
            card.统一编号 = row["统一编号"].ToString();
            card.灾害位置 = row["灾害位置"].ToString();
            card.类型及规模 = row["类型及规模"].ToString();
            card.诱发因素 = row["诱发因素"].ToString();
            card.威胁对象 = row["威胁对象"].ToString();
            card.监测负责人 = row["监测负责人"].ToString();
            card.监测的主要迹象 = row["监测的主要迹象"].ToString();
            card.监测的主要手段和方法 = row["监测的主要手段和方法"].ToString();
            card.临灾预报的判据 = row["临灾预报的判据"].ToString();
            card.预定避灾地点 = row["预定避灾地点"].ToString();
            card.预定疏散路线 = row["预定疏散路线"].ToString();
            card.预定报警信号 = row["预定报警信号"].ToString();
            card.疏散命令发布人 = row["疏散命令发布人"].ToString();
            card.疏散值班电话 = row["疏散值班电话"].ToString();
            card.抢排险单位及负责人 = row["抢排险单位及负责人"].ToString();
            card.抢排险值班电话 = row["抢排险值班电话"].ToString();
            card.治安保卫单位及负责人 = row["治安保卫单位及负责人"].ToString();
            card.治安保卫值班电话 = row["治安保卫值班电话"].ToString();
            card.医疗救护单位及负责人 = row["医疗救护单位及负责人"].ToString();
            card.医疗救护值班电话 = row["医疗救护值班电话"].ToString();
            card.本卡发放单位 = row["本卡发放单位"].ToString();
            card.本卡发放单位联系电话 = row["本卡发放单位联系电话"].ToString();
            //card.发放日期 = row["发放日期"].ToString();
            card.持卡单位或个人 = row["持卡单位或个人"].ToString();
            card.持卡单位或个人联系电话 = row["持卡单位或个人联系电话"].ToString();
            //card.持卡日期 = row["统一编号"].ToString();


            return card;
        }
    }
}
