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
    public class LoadMineArchive
    {

        public string InsertData(string filePath)
        {

            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "基础档案表");
            MineRecoveryService service = new MineRecoveryService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            //
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    MineArchive archive = GetMineArchive(dt.Rows[i], dt.Columns);
                    service.InsertMineArchive(archive);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "条数据导入失败，原因是：" + ex.Message);
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("矿山复绿档案表", dt.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }




        public MineArchive GetMineArchive(DataRow row, DataColumnCollection columns)
        {


            MineArchive archive = new MineArchive();

            archive.编号 = row["编号"].ToString();
            archive.矿山名称 = row["矿山名称"].ToString();
            archive.市 = row["市"].ToString();
            archive.县 = row["县"].ToString();
            archive.乡镇 = row["乡镇"].ToString();
            archive.村 = row["村"].ToString();
            archive.组 = row["组"].ToString();
            archive.区位 = row["区位"].ToString();
            archive.自然保护区 = row["自然保护区"].ToString();
            archive.景观 = row["景观"].ToString();
            archive.居民区 = row["居民区"].ToString();
            archive.交通干线 = row["交通干线"].ToString();
            archive.河流 = row["河流"].ToString();
            archive.位置 = row["位置"].ToString();
            archive.地质形态 = row["地质形态"].ToString();
            archive.山区 = row["山区"].ToString();
            archive.丘陵 = row["丘陵"].ToString();
            archive.平原 = row["平原"].ToString();
            archive.其他地貌 = row["其他地貌"].ToString();
            archive.经度 = string.IsNullOrEmpty(row["经度"].ToString()) ? 0 : LonLatHelper.ConvertToDegreeStyleFromDegreeString(row["经度"].ToString());
            archive.纬度 = string.IsNullOrEmpty(row["纬度"].ToString()) ? 0 : LonLatHelper.ConvertToDegreeStyleFromDegreeString(row["纬度"].ToString());
            archive.拐点坐标 = row["拐点坐标"].ToString();
            archive.企业类型 = row["企业类型"].ToString();
            archive.国有经济 = row["国有经济"].ToString();
            archive.集体经济 = row["集体经济"].ToString();
            archive.联营经济 = row["联营经济"].ToString();
            archive.私营经济 = row["私营经济"].ToString();
            archive.股份合作 = row["股份合作"].ToString();
            archive.股份制企业 = row["股份制企业"].ToString();
            archive.外商投资 = row["外商投资"].ToString();
            archive.其他经济 = row["其他经济"].ToString();
            archive.开采矿种 = row["开采矿种"].ToString();
            archive.JBQK38 = row["JBQK38"].ToString();
            archive.矿山规模 = row["矿山规模"].ToString();
            archive.DXKS = row["DXKS"].ToString();
            archive.ZXKS = row["ZXKS"].ToString();
            archive.XXKS = row["XXKS"].ToString();
            archive.生产现状 = row["生产现状"].ToString();
            archive.ZJ = row["ZJ"].ToString();
            archive.KC = row["KC"].ToString();
            archive.GB = row["GB"].ToString();
            archive.TC = row["TC"].ToString();
            //archive.建矿时间 = row[""].ToString();
            //archive.闭坑时间 = row[""].ToString();
            archive.JBQK24 = ConvertHelper.GetDoubleValueFromStr(row["JBQK24"].ToString());
            archive.开采方式 = row["开采方式"].ToString();
            archive.LTKC = row["LTKC"].ToString();
            archive.JGKC = row["JGKC"].ToString();
            archive.HHKC = row["HHKC"].ToString();
            archive.QTKC = row["QTKC"].ToString();
            archive.TDPH108 = ConvertHelper.GetDoubleValueFromStr(row["TDPH108"].ToString());
            archive.ZLLS10 = ConvertHelper.GetDoubleValueFromStr(row["ZLLS10"].ToString());
            archive.ZLGH02 = ConvertHelper.GetDoubleValueFromStr(row["ZLGH02"].ToString());
            archive.ZLGH03 = ConvertHelper.GetDoubleValueFromStr(row["ZLGH03"].ToString());
            archive.威胁对象 = row["威胁对象"].ToString();
            archive.威胁景观 = row["威胁景观"].ToString();
            archive.威胁公路 = row["威胁公路"].ToString();
            archive.威胁学校 = row["威胁学校"].ToString();
            archive.威胁铁路 = row["威胁铁路"].ToString();
            archive.威胁居民地 = row["威胁居民地"].ToString();
            archive.威胁航道 = row["威胁航道"].ToString();
            archive.威胁厂矿企业 = row["威胁厂矿企业"].ToString();
            archive.威胁其他 = row["威胁其他"].ToString();
            archive.处理措施 = row["处理措施"].ToString();
            archive.地灾防治 = row["地灾防治"].ToString();
            archive.地貌景观修复 = row["地貌景观修复"].ToString();

            archive.植被绿化 = row["植被绿化"].ToString();
            archive.人造景观 = row["人造景观"].ToString();
            archive.其他措施 = row["其他措施"].ToString();
            archive.治理责任主体 = row["治理责任主体"].ToString();
            archive.地方政府 = row["地方政府"].ToString();
            archive.矿山企业 = row["矿山企业"].ToString();
            archive.中央补助 = row["中央补助"].ToString();
            archive.地方财政拨款 = row["地方财政拨款"].ToString();
            archive.公益投资 = row["公益投资"].ToString();
            archive.社会投资 = row["社会投资"].ToString();
            archive.其他资金 = row["其他资金"].ToString();
            archive.保证金 = row["保证金"].ToString();
            archive.企业自筹 = row["企业自筹"].ToString();
            archive.其他 = row["其他"].ToString();
            archive.ZLGH08 = row["ZLGH08"].ToString();
            archive.ZLGH09 = row["ZLGH09"].ToString();
            archive.ZLGH10 = row["ZLGH10"].ToString();
            archive.ZLGH11 = row["ZLGH11"].ToString();
            archive.ZLGH15 = row["ZLGH15"].ToString();
            archive.验收时间 = row["验收时间"].ToString();
            archive.填表人 = row["填表人"].ToString();
            archive.填表单位 = row["填表单位"].ToString();
            archive.DC03 = row["DC03"].ToString();
            archive.审核人 = row["审核人"].ToString();
            archive.备注 = row["备注"].ToString();
            archive.地质环境问题 = row["地质环境问题"].ToString();
            archive.WT01 = row["WT01"].ToString();
            archive.WT02 = row["WT02"].ToString();
            archive.WT03 = row["WT02"].ToString();
            archive.WT04 = row["WT04"].ToString();
            archive.WT05 = row["WT05"].ToString();
            archive.WT06 = row["WT06"].ToString();
            archive.JKND = row["JKND"].ToString();


            return archive;
        }
    }
}
