using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Relocation;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.Relocation
{
    public class LoadRelocationLandSlipCheck
    {

        public string InsertData(string filePath)
        {
            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "崩塌核查表");
            RelocationService service = new RelocationService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count && i < 15; i++)
            {
                try
                {
                    RelocationLandSlipCheck check = GetRelocationSlipCheck(dt.Rows[i], dt.Columns);
                    service.InsertRelocationLandSlipCheck(check);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message);
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("崩塌核查表", dt.Rows.Count, successCount, failCount,
                errorStr.ToString());

            return result;

        }


        public RelocationLandSlipCheck GetRelocationSlipCheck(DataRow row, DataColumnCollection columns)
        {


            RelocationLandSlipCheck slip = new RelocationLandSlipCheck();

            slip.统一编号 = row["统一编号"].ToString();
            slip.野外编号 = row["野外编号"].ToString();
            slip.设区市 = row["设区市"].ToString();
            slip.县市区 = row["县市区"].ToString();
            slip.乡镇场 = row["乡镇场"].ToString();
            slip.村组及地名 = row["村组及地名"].ToString();
            slip.X = ConvertHelper.GetDoubleValueFromStr(row["X"].ToString());
            slip.Y = ConvertHelper.GetDoubleValueFromStr(row["Y"].ToString());
            slip.高程 = row["高程"].ToString();
            slip.地层_岩浆岩代号 = row["地层、岩浆岩代号"].ToString();
            slip.地层_岩浆岩代号岩性 = row["地层、岩浆岩代号岩性"].ToString();
            slip.覆盖层岩性 = row["覆盖层岩性"].ToString();
            slip.覆盖层厚度 = row["覆盖层厚度"].ToString();
            slip.原始斜坡坡高 = row["原始斜坡坡高"].ToString();
            slip.原始斜坡坡度 = row["原始斜坡坡度"].ToString();
            slip.已发生地质灾害发生时间 = row["已发生地质灾害发生时间"].ToString();
            slip.已发生地质灾害崩体性质 = row["已发生地质灾害崩体性质"].ToString();
            slip.已发生地质灾害体积 = row["已发生地质灾害体积"].ToString();
            slip.已发生地质灾害规模等级 = row["已发生地质灾害规模等级"].ToString();
            slip.已发生地质灾害伤人 = row["已发生地质灾害伤人"].ToString();
            slip.已发生地质灾害亡人 = row["已发生地质灾害亡人"].ToString();
            slip.已发生地质灾害损失 = row["已发生地质灾害损失"].ToString();
            slip.已发生地质灾害灾情等级 = row["已发生地质灾害灾情等级"].ToString();
            slip.潜在地质灾害稳定等级 = row["潜在地质灾害稳定等级"].ToString();
            slip.潜在地质灾害体积 = row["潜在地质灾害体积"].ToString();
            slip.潜在地质灾害规模等级 = row["潜在地质灾害规模等级"].ToString();
            slip.潜在地质灾害威胁户 = row["潜在地质灾害威胁户"].ToString();
            slip.潜在地质灾害威胁人 = row["潜在地质灾害威胁人"].ToString();
            slip.潜在地质灾害威胁资产 = row["潜在地质灾害威胁资产"].ToString();
            slip.潜在地质灾害危害等级 = row["潜在地质灾害危害等级"].ToString();
            slip.原来上报搬迁户 = row["原来上报搬迁户"].ToString();
            slip.原来上报搬迁人 = row["原来上报搬迁人"].ToString();
            slip.防治工程现状 = row["防治工程现状"].ToString();
            slip.防治工程建议 = row["防治工程建议"].ToString();


            return slip;

        }
    }
}
