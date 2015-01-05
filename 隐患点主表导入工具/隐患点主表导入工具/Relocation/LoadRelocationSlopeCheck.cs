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
    public class LoadRelocationSlopeCheck
    {

        public string InsertData(string filePath)
        {
            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "斜坡核查表");
            RelocationService service = new RelocationService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count && i < 15; i++)
            {
                try
                {
                    RelocationSlopeCheck check = GetRelocationSlopeCheck(dt.Rows[i], dt.Columns);
                    service.InsertRelocationSlopeCheck(check);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message);
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("斜坡核查表", dt.Rows.Count, successCount, failCount,
                errorStr.ToString());

            return result;

        }


        public RelocationSlopeCheck GetRelocationSlopeCheck(DataRow row, DataColumnCollection columns)
        {


            RelocationSlopeCheck slope = new RelocationSlopeCheck();


            slope.统一编号 = row["统一编号"].ToString();
            slope.野外编号 = row["野外编号"].ToString();
            slope.设区市 = row["设区市"].ToString();
            slope.县市区 = row["县市区"].ToString();
            slope.乡镇场 = row["乡镇场"].ToString();
            slope.村组及地名 = row["村组及地名"].ToString();
            slope.x = string.IsNullOrEmpty(row["x"].ToString()) ? 0 : ConvertHelper.GetDoubleValueFromStr(row["x"].ToString());
            slope.y = string.IsNullOrEmpty(row["y"].ToString()) ? 0 : ConvertHelper.GetDoubleValueFromStr(row["y"].ToString());
            slope.高程 = row["高程"].ToString();
            slope.斜坡类型 = row["斜坡类型"].ToString();
            slope.地层岩浆岩代号 = row["地层岩浆岩代号"].ToString();
            slope.地层岩浆岩岩性 = row["地层岩浆岩岩性"].ToString();
            slope.覆盖层岩性 = row["覆盖层岩性"].ToString();
            slope.覆盖层厚度 = row["覆盖层厚度"].ToString();
            slope.原始斜坡坡高 = row["原始斜坡坡高"].ToString();
            slope.原始斜坡坡度 = row["原始斜坡坡度"].ToString();
            slope.人工切坡坡度 = row["人工切坡坡度"].ToString();
            slope.人工切坡坡高 = row["人工切坡坡高"].ToString();
            slope.潜在地质灾害灾害类型 = row["潜在地质灾害灾害类型"].ToString();
            slope.潜在地质灾害稳定等级 = row["潜在地质灾害稳定等级"].ToString();
            slope.潜在地质灾害体积 = row["潜在地质灾害体积"].ToString();
            slope.潜在地质灾害规模等级 = row["潜在地质灾害规模等级"].ToString();
            slope.潜在地质灾害威胁户 = row["潜在地质灾害威胁户"].ToString();
            slope.潜在地质灾害威胁人 = row["潜在地质灾害威胁人"].ToString();
            slope.潜在地质灾害威胁财产 = row["潜在地质灾害威胁财产"].ToString();
            slope.潜在地质灾害危害等级 = row["潜在地质灾害危害等级"].ToString();
            slope.原来上报搬迁户 = row["原来上报搬迁户"].ToString();
            slope.原来上报搬迁人 = row["原来上报搬迁人"].ToString();
            slope.防治工程现状 = row["防治工程现状"].ToString();
            slope.防治工程建议 = row["防治工程建议"].ToString();




            return slope;


        }
    }
}
