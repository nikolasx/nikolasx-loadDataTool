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
    public class LoadRelocationLandSlideCheck
    {
        public string InsertData(string filePath)
        {
            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "滑坡核查表");
            RelocationService service = new RelocationService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count && i < 15; i++)
            {
                try
                {
                    RelocationLandSlideCheck check = GetRelocationSlideCheck(dt.Rows[i], dt.Columns);
                    service.InsertRelocationLandSlideCheck(check);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message);
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("滑坡核查表", dt.Rows.Count, successCount, failCount,
                errorStr.ToString());

            return result;

        }


        public RelocationLandSlideCheck GetRelocationSlideCheck(DataRow row, DataColumnCollection columns)
        {
            RelocationLandSlideCheck slide = new RelocationLandSlideCheck();

            slide.统一编号 = row["统一编号"].ToString();
            slide.野外编号 = row["野外编号"].ToString();
            slide.设区市 = row["设区市"].ToString();
            slide.县市区 = row["县市区"].ToString();
            slide.乡镇场 = row["乡镇场"].ToString();
            slide.村组及地名 = row["村组及地名"].ToString();
            slide.X = ConvertHelper.GetDoubleValueFromStr(row["X"].ToString());
            slide.Y = ConvertHelper.GetDoubleValueFromStr(row["Y"].ToString());
            slide.高程 = row["高程"].ToString();
            slide.地层岩浆岩代号 = row["地层岩浆岩代号"].ToString();
            slide.地层岩浆岩岩性 = row["地层岩浆岩岩性"].ToString();
            slide.覆盖层岩性 = row["覆盖层岩性"].ToString();
            slide.覆盖层厚度 = row["覆盖层厚度"].ToString();
            slide.原始斜坡坡高 = row["原始斜坡坡高"].ToString();
            slide.原始斜坡坡度 = row["原始斜坡坡度"].ToString();
            slide.人工切坡坡度 = row["人工切坡坡度"].ToString();
            slide.人工切坡坡高 = row["人工切坡坡高"].ToString();
            slide.已发生地质灾害_发生日期 = row["已发生地质灾害-发生日期"].ToString();
            slide.已发生地质灾害_滑体性质 = row["已发生地质灾害-滑体性质"].ToString();
            slide.已发生地质灾害_体积 = row["已发生地质灾害-体积"].ToString();
            slide.已发生地质灾害_规模等级 = row["已发生地质灾害-规模等级"].ToString();
            slide.已发生地质灾害_伤人 = row["已发生地质灾害-伤人"].ToString();
            slide.已发生地质灾害_亡人 = row["已发生地质灾害-亡人"].ToString();
            slide.已发生地质灾害_损失万元 = row["已发生地质灾害-损失万元"].ToString();
            slide.已发生地质灾害_灾情等级 = row["已发生地质灾害-灾情等级"].ToString();
            slide.潜在地质灾害_稳定等级 = row["潜在地质灾害-稳定等级"].ToString();
            slide.潜在地质灾害_体积 = row["潜在地质灾害-体积"].ToString();
            slide.潜在地质灾害_规模等级 = row["潜在地质灾害-规模等级"].ToString();
            slide.潜在地质灾害_威胁户数 = row["潜在地质灾害-威胁户数"].ToString();
            slide.潜在地质灾害_威胁人数 = row["潜在地质灾害-威胁人数"].ToString();
            slide.潜在地质灾害_威胁资产 = row["潜在地质灾害-威胁资产"].ToString();
            slide.潜在地质灾害_危害等级 = row["潜在地质灾害-危害等级"].ToString();
            slide.原来上报搬迁_户 = row["原来上报搬迁-户"].ToString();
            slide.原来上报搬迁_人 = row["原来上报搬迁-人"].ToString();
            slide.防治工程_现状 = row["防治工程-现状"].ToString();
            slide.防治工程_建议 = row["防治工程-建议"].ToString();


            return slide;
        }
    }
}
