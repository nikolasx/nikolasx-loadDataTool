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
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.FiveScale.MassPres
{
    public class LoadAvoidRiskCardFS
    {

        public string InsertData(string filePath)
        {
            DataTable dt = AccessHelper.GetDataTableByAccessFile(filePath, "避险明白卡");
            PrePlanFSService service = new PrePlanFSService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    AvoidRiskCardFS plan = GetAvoidRiskCard(dt.Rows[i], dt.Columns);
                    service.InsertAvoidRiskCardFS(plan);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，统一编号：" + dt.Rows[i]["统一编号"] + "失败原因是：" + ex.Message);
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("避险明白卡(5W)", dt.Rows.Count, successCount,
                failCount, errorStr.ToString());
            return result;
        }
        public AvoidRiskCardFS GetAvoidRiskCard(DataRow row, DataColumnCollection columns)
        {
            AvoidRiskCardFS disaster = new AvoidRiskCardFS();

            disaster.名称 = row["名称"].ToString();
            disaster.野外编号 = row["野外编号"].ToString();
            disaster.灾害类型 = row["灾害类型"].ToString();
            disaster.规模 = row["规模"].ToString();
            disaster.规模等级 = row["规模等级"].ToString();
            disaster.位置关系 = row["位置关系"].ToString();
            disaster.本住户注意事项 = row["本住户注意事项"].ToString();
            disaster.诱发因素 = row["诱发因素"].ToString();
            disaster.监测人 = row["监测人"].ToString();
            disaster.监测人联系电话 = row["监测人联系电话"].ToString();
            disaster.预警信号 = row["预警信号"].ToString();
            disaster.预警信号发布人 = row["预警信号发布人"].ToString();
            disaster.预警信号发布人联系电话 = row["预警信号发布人联系电话"].ToString();
            disaster.撤离路线 = row["撤离路线"].ToString();
            disaster.安置单位地点 = row["安置单位地点"].ToString();
            disaster.安置单位负责人 = row["安置单位负责人"].ToString();
            disaster.安置单位联系电话 = row["安置单位联系电话"].ToString();
            disaster.救护单位 = row["救护单位"].ToString();
            disaster.救护单位负责人 = row["救护单位负责人"].ToString();
            disaster.救护单位联系电话 = row["救护单位联系电话"].ToString();
            disaster.本卡发放单位 = row["本卡发放单位"].ToString();
            disaster.本卡发放单位负责人 = row["本卡发放单位负责人"].ToString();
            disaster.本卡发放单位联系电话 = row["本卡发放单位联系电话"].ToString();
            disaster.统一编号 = row["统一编号"].ToString();


            //新增字段
            disaster.省 = row["省"].ToString();
            disaster.市 = row["市"].ToString();
            disaster.县 = row["县"].ToString();
            disaster.户主姓名 = row["户主姓名"].ToString();
            disaster.家庭人数 = row["家庭人数"].ToString();
            disaster.房屋类型 = row["房屋类型"].ToString();
            disaster.家庭住址 = row["家庭住址"].ToString();
            disaster.姓名1 = row["姓名1"].ToString();
            disaster.性别1 = row["性别1"].ToString();
            disaster.年龄1 = row["年龄1"].ToString();
            disaster.姓名2 = row["姓名2"].ToString();
            disaster.性别2 = row["性别2"].ToString();
            disaster.年龄2 = row["年龄2"].ToString();
            disaster.姓名3 = row["姓名3"].ToString();
            disaster.性别3 = row["性别3"].ToString();
            disaster.年龄3 = row["年龄3"].ToString();
            disaster.姓名4 = row["姓名4"].ToString();
            disaster.性别4 = row["性别4"].ToString();
            disaster.年龄4 = row["年龄4"].ToString();
            disaster.姓名5 = row["姓名5"].ToString();
            disaster.性别5 = row["性别5"].ToString();
            disaster.年龄5 = row["年龄5"].ToString();
            disaster.姓名6 = row["姓名6"].ToString();
            disaster.性别6 = row["性别6"].ToString();
            disaster.年龄6 = row["年龄6"].ToString();
            disaster.姓名7 = row["姓名7"].ToString();
            disaster.性别7 = row["性别7"].ToString();
            disaster.年龄7 = row["年龄7"].ToString();
            disaster.姓名8 = row["姓名8"].ToString();
            disaster.性别8 = row["性别8"].ToString();
            disaster.年龄8 = row["年龄8"].ToString();
            disaster.户主签名 = row["户主签名"].ToString();
            disaster.联系电话 = row["联系电话"].ToString();
            disaster.日期年 = ConvertHelper.GetIntValueByStr(row["日期年"].ToString());
            disaster.日期月 = ConvertHelper.GetIntValueByStr(row["日期月"].ToString());
            disaster.日期日 = ConvertHelper.GetIntValueByStr(row["日期日"].ToString());
            disaster.编号 = ConvertHelper.GetIntValueByStr(row["编号"].ToString());


            return disaster;
        }
    }
}
