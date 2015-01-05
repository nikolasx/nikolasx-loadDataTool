using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikolasHelper.GIS;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.MineRecovery;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.MineRecovery
{
    public class LoadMineRemoteSensingCard
    {


        public string InsertData(string filePath)
        {

            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "遥感解译卡");
            MineRecoveryService service = new MineRecoveryService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            //TODO 这里先只导入15条数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    MineRemoteSensingCard card = GetMineRemoteSensingCard(dt.Rows[i], dt.Columns);
                    service.InsertMineRemoteSensingCard(card);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "条数据导入失败，原因是：" + ex.Message);
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("矿山复绿遥感解译卡", dt.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }


        public MineRemoteSensingCard GetMineRemoteSensingCard(DataRow row, DataColumnCollection columns)
        {


            MineRemoteSensingCard card = new MineRemoteSensingCard();

            card.编号 = row["编号"].ToString();
            card.图斑编号 = row["图斑编号"].ToString();
            card.设区市 = row["设区市"].ToString();
            card.县 = row["县"].ToString();
            card.乡镇 = row["乡镇"].ToString();
            card.村 = row["村"].ToString();
            card.组 = row["组"].ToString();
            card.经度 = LonLatHelper.ConvertToDegreeStyleFromDegreeString(row["经度"].ToString());
            card.纬度 = LonLatHelper.ConvertToDegreeStyleFromDegreeString(row["纬度"].ToString());
            card.三区两线区位 = row["三区两线区位"].ToString();
            card.矿山名称 = row["矿山名称"].ToString();
            card.遥感影像 = row["遥感影像"].ToString();
            card.拐点坐标 = row["拐点坐标"].ToString();
            card.开采矿种 = row["开采矿种"].ToString();
            card.地貌类型 = row["地貌类型"].ToString();
            card.开采方式 = row["开采方式"].ToString();
            card.采场破坏面积 = row["采场破坏面积"].ToString();
            card.工业广场破坏面积 = row["工业广场破坏面积"].ToString();
            card.排土场破坏面积 = row["排土场破坏面积"].ToString();
            card.其它破坏面积 = row["其它破坏面积"].ToString();
            card.尾矿库破坏面积 = row["尾矿库破坏面积"].ToString();
            card.已治理面积 = row["已治理面积"].ToString();
            card.总破坏面积 = row["总破坏面积"].ToString();
            card.解译人 = row["解译人"].ToString();
            card.治理方式 = row["治理方式"].ToString();
            card.技术负责 = row["技术负责"].ToString();
            card.校核 = row["校核"].ToString();
            card.核查人 = row["核查人"].ToString();
            card.解译时间 = DateTime.Parse(row["解译时间"].ToString());
            card.核查单位 = row["核查单位"].ToString();
            card.核查结果 = row["核查结果"].ToString();
            card.核查时间 = DateTime.Parse(row["核查时间"].ToString());

            return card;


        }
    }
}
