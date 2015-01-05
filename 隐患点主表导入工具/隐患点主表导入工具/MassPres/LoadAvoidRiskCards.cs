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
    public class LoadAvoidRiskCards
    {

        /// <summary>
        /// 避灾明白卡
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>

        public string InsertData(string filePath)
        {
            DataTable dt = AccessHelper.GetDataTableByAccessFile(filePath, "避灾明白卡");
            PrePlanService service = new PrePlanService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    AvoidRiskCard card = GetAvoidRiskCard(dt.Rows[i], dt.Columns);
                    service.InsertAvoidRiskCards(card);
                }
                catch (Exception ex)
                {
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message);
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("避灾明白卡", dt.Rows.Count, successCount,
                failCount, errorStr.ToString());
            return result;
        }


        public AvoidRiskCard GetAvoidRiskCard(DataRow row, DataColumnCollection columns)
        {
            AvoidRiskCard card = new AvoidRiskCard();

            card.名称 = row["名称"].ToString();
            card.野外编号 = row["野外编号"].ToString();
            card.灾害类型 = ConvertHelper.GetEnumGeoDisasterByStr(row["灾害类型"].ToString());
            card.规模 = row["规模"].ToString();
            card.规模等级 = row["规模等级"].ToString();
            card.位置关系 = row["位置关系"].ToString();
            card.本住户注意事项 = row["本住户注意事项"].ToString();
            card.诱发因素 = row["诱发因素"].ToString();
            card.监测人 = row["监测人"].ToString();
            card.监测人联系电话 = row["监测人联系电话"].ToString();
            card.预警信号 = row["预警信号"].ToString();
            card.预警信号发布人 = row["预警信号发布人"].ToString();
            card.预警信号发布人联系电话 = row["预警信号发布人联系电话"].ToString();
            card.撤离路线 = row["撤离路线"].ToString();
            card.安置单位地点 = row["安置单位地点"].ToString();
            card.安置单位负责人 = row["安置单位负责人"].ToString();
            card.安置单位联系电话 = row["安置单位联系电话"].ToString();
            card.救护单位 = row["救护单位"].ToString();
            card.救护单位负责人 = row["救护单位负责人"].ToString();
            card.救护单位联系电话 = row["救护单位联系电话"].ToString();
            card.本卡发放单位 = row["本卡发放单位"].ToString();
            card.本卡发放单位负责人 = row["本卡发放单位负责人"].ToString();
            card.本卡发放单位联系电话 = row["本卡发放单位联系电话"].ToString();
            card.统一编号 = row["统一编号"].ToString();




            return card;
        }
    }
}
