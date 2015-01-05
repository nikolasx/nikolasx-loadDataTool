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
    /// <summary>
    /// 移民搬迁环境核查表（泥石流）
    /// </summary>
    public class LoadRelocationDebrisFlowCheck
    {

        public string InsertData(string filePath)
        {


            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "泥石流核查表");
            RelocationService service = new RelocationService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count && i < 15; i++)
            {
                try
                {
                    RelocationDebrisFlowCheck check = GetRelocationFlowCheck(dt.Rows[i], dt.Columns);
                    service.InsertRelocationDebrisFlowCheck(check);
                    successCount++;

                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message);
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("泥石流核查表", dt.Rows.Count, successCount, failCount,
                errorStr.ToString());

            return result;

        }

        /// <summary>
        /// 获取移民搬迁环境核查表（泥石流）信息
        /// </summary>
        /// <returns></returns>
        public RelocationDebrisFlowCheck GetRelocationFlowCheck(DataRow row, DataColumnCollection columns)
        {
            RelocationDebrisFlowCheck flow = new RelocationDebrisFlowCheck();

            flow.统一编号 = row["统一编号"].ToString();
            flow.野外编号 = row["野外编号"].ToString();
            flow.设区市 = row["设区市"].ToString();
            flow.县市区 = row["县市区"].ToString();
            flow.乡镇场 = row["乡镇场"].ToString();
            flow.村组及地名 = row["村组及地名"].ToString();
            flow.X = string.IsNullOrEmpty(row["X"].ToString()) ? 0 : ConvertHelper.GetDoubleValueFromStr(row["X"].ToString());
            flow.Y = string.IsNullOrEmpty(row["Y"].ToString()) ? 0 : ConvertHelper.GetDoubleValueFromStr(row["Y"].ToString());
            flow.高程 = row["高程"].ToString();
            flow.已发生地质灾害发生时间 = row["已发生地质灾害发生时间"].ToString();
            flow.已发生地质灾害体积 = row["已发生地质灾害体积"].ToString();
            flow.已发生地质灾害规模等级 = row["已发生地质灾害规模等级"].ToString();
            flow.已发生地质灾害伤人 = row["已发生地质灾害伤人"].ToString();
            flow.已发生地质灾害亡人 = row["已发生地质灾害亡人"].ToString();
            flow.已发生地质灾害损失万元 = row["已发生地质灾害损失万元"].ToString();
            flow.已发生地质灾害灾情等级 = row["已发生地质灾害灾情等级"].ToString();
            flow.主要评价因子不良地质现象 = row["主要评价因子不良地质现象"].ToString();
            flow.主要评价因子沟口扇形地 = row["主要评价因子沟口扇形地"].ToString();
            flow.主要评价因子主沟纵坡 = row["主要评价因子主沟纵坡"].ToString();
            flow.主要评价因子植被覆盖率 = row["主要评价因子植被覆盖率"].ToString();
            flow.主要评价因子冲淤变幅 = row["主要评价因子冲淤变幅"].ToString();
            flow.主要评价因子地层岩浆岩代号 = row["主要评价因子地层岩浆岩代号"].ToString();
            flow.主要评价因子地层岩浆岩岩性 = row["主要评价因子地层岩浆岩岩性"].ToString();
            flow.主要评价因子松散物平均厚 = row["主要评价因子松散物平均厚"].ToString();
            flow.主要评价因子松散物储量 = row["主要评价因子松散物储量"].ToString();
            flow.主要评价因子山坡坡度 = row["主要评价因子山坡坡度"].ToString();
            flow.主要评价因子流域面积 = row["主要评价因子流域面积"].ToString();
            flow.主要评价因子堵塞程度 = row["主要评价因子堵塞程度"].ToString();
            flow.潜在地质灾害易发程度 = row["潜在地质灾害易发程度"].ToString();
            flow.潜在地质灾害体积 = row["潜在地质灾害体积"].ToString();
            flow.潜在地质灾害规模等级 = row["潜在地质灾害规模等级"].ToString();
            flow.潜在地质灾害威胁户 = row["潜在地质灾害威胁户"].ToString();
            flow.潜在地质灾害威胁人 = row["潜在地质灾害威胁人"].ToString();
            flow.潜在地质灾害威胁资产 = row["潜在地质灾害威胁资产"].ToString();
            flow.潜在地质灾害危害等级 = row["潜在地质灾害危害等级"].ToString();
            flow.原来上报搬迁户 = row["原来上报搬迁户"].ToString();
            flow.原来上报搬迁人 = row["原来上报搬迁人"].ToString();
            flow.防治工程现状 = row["防治工程现状"].ToString();
            flow.防治工程建议 = row["防治工程建议"].ToString();


            return flow;
        }
    }
}
