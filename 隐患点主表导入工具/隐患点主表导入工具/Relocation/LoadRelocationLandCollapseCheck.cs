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
    public class LoadRelocationLandCollapseCheck
    {

        public string InsertData(string filePath)
        {


            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "地面塌陷核查表");
            RelocationService service = new RelocationService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    RelocationLandCollapseCheck check = GetCollapseCheck(dt.Rows[i], dt.Columns);
                    service.InsertRelocationLandCollapseCheck(check);
                    successCount++;

                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message);
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("地面塌陷核查表", dt.Rows.Count, successCount, failCount,
                errorStr.ToString());

            return result;

        }


        public RelocationLandCollapseCheck GetCollapseCheck(DataRow row, DataColumnCollection columns)
        {
            RelocationLandCollapseCheck collapse = new RelocationLandCollapseCheck();


            collapse.统一编号 = row["统一编号"].ToString();
            collapse.野外编号 = row["野外编号"].ToString();
            collapse.点类型 = row["点类型"].ToString();
            collapse.设区市 = row["设区市"].ToString();
            collapse.县市区 = row["县市区"].ToString();
            collapse.乡镇场 = row["乡镇场"].ToString();
            collapse.村组及地名 = row["村组及地名"].ToString();
            collapse.X = string.IsNullOrEmpty(row["X"].ToString()) ? 0 : ConvertHelper.GetDoubleValueFromStr(row["X"].ToString());
            collapse.Y = string.IsNullOrEmpty(row["Y"].ToString()) ? 0 : ConvertHelper.GetDoubleValueFromStr(row["Y"].ToString());
            collapse.高程 = row["高程"].ToString();
            collapse.地层代号 = row["地层代号"].ToString();
            collapse.基岩岩性 = row["基岩岩性"].ToString();
            collapse.覆盖层结构 = row["覆盖层结构"].ToString();
            collapse.覆盖层厚度 = row["覆盖层厚度"].ToString();
            collapse.覆盖层上部岩性 = row["覆盖层上部岩性"].ToString();
            collapse.覆盖层下部岩性 = row["覆盖层下部岩性"].ToString();
            collapse.地下水类型 = row["地下水类型"].ToString();
            collapse.地下水埋深 = row["地下水埋深"].ToString();
            collapse.已发生地质灾害发生时间 = row["已发生地质灾害发生时间"].ToString();
            collapse.已发生地质灾害坑数 = row["已发生地质灾害坑数"].ToString();
            collapse.已发生地质灾害面积 = row["已发生地质灾害面积"].ToString();
            collapse.已发生地质灾害规模等级 = row["已发生地质灾害规模等级"].ToString();
            collapse.已发生地质灾害伤人 = row["已发生地质灾害伤人"].ToString();
            collapse.已发生地质灾害亡人 = row["已发生地质灾害亡人"].ToString();
            collapse.已发生地质灾害损失万元 = row["已发生地质灾害损失万元"].ToString();
            collapse.已发生地质灾害灾情等级 = row["已发生地质灾害灾情等级"].ToString();
            collapse.潜在地质灾害稳定等级 = row["潜在地质灾害稳定等级"].ToString();
            collapse.潜在地质灾害面积 = row["潜在地质灾害面积"].ToString();
            collapse.潜在地质灾害规模等级 = row["潜在地质灾害规模等级"].ToString();
            collapse.潜在地质灾害威胁户 = row["潜在地质灾害威胁户"].ToString();
            collapse.潜在地质灾害人 = row["潜在地质灾害人"].ToString();
            collapse.潜在地质灾害威胁资产 = row["潜在地质灾害威胁资产"].ToString();
            collapse.潜在地质灾害危害等级 = row["潜在地质灾害危害等级"].ToString();
            collapse.原来上报搬迁户 = row["原来上报搬迁户"].ToString();
            collapse.原来上报搬迁人 = row["原来上报搬迁人"].ToString();
            collapse.防治工程现状 = row["防治工程现状"].ToString();
            collapse.防治工程建议 = row["防治工程建议"].ToString();


            return collapse;

        }
    }
}
