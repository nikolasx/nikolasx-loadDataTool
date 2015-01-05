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
    public class LoadMineEnvironmentSurvey
    {

        public string InsertData(string filePath)
        {

            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "环境调查表");
            MineRecoveryService service = new MineRecoveryService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            //TODO 这里先只导入15条数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    MineEnvironmentSurvey survey = GetMineEnvironmentSurvey(dt.Rows[i], dt.Columns);
                    service.InsertMineEnvironmentSurvey(survey);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.AppendLine("第" + (i + 1) + "条数据导入失败，原因是：" + ex.Message);
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("矿山复绿环境调查表", dt.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }



        public MineEnvironmentSurvey GetMineEnvironmentSurvey(DataRow row, DataColumnCollection columns)
        {


            MineEnvironmentSurvey survey = new MineEnvironmentSurvey();


            survey.统一编号 = row["统一编号"].ToString();
            survey.图斑编号 = row["图斑编号"].ToString();
            survey.矿山名称 = row["矿山名称"].ToString();
            survey.设区市 = row["设区市"].ToString();
            survey.县_市_区_ = row["县(市、区)"].ToString();
            survey.乡_镇_ = row["乡(镇)"].ToString();
            survey.村 = row["村"].ToString();
            survey.组 = row["组"].ToString();
            survey.矿山区位 = row["矿山区位"].ToString();
            survey.区位 = row["区位"].ToString();
            survey.区位描述 = row["区位描述"].ToString();
            survey.矿山所处地貌形态 = row["矿山所处地貌形态"].ToString();
            survey.X = ConvertHelper.GetDoubleValueFromStr(row["X"].ToString());
            survey.Y = ConvertHelper.GetDoubleValueFromStr(row["Y"].ToString());
            survey.经度 = string.IsNullOrEmpty(row["经度"].ToString()) ? 0 : LonLatHelper.ConvertToDegreeStyleFromDegreeString(row["经度"].ToString());
            survey.纬度 = string.IsNullOrEmpty(row["纬度"].ToString()) ? 0 : LonLatHelper.ConvertToDegreeStyleFromDegreeString(row["纬度"].ToString());
            survey.矿区拐点坐标 = row["矿区拐点坐标"].ToString();
            survey.破坏区拐点坐标 = row["破坏区拐点坐标"].ToString();
            survey.采矿许可证号 = row["采矿许可证号"].ToString();
            survey.经济类型 = row["经济类型"].ToString();
            survey.开采主矿种 = row["开采主矿种"].ToString();
            survey.矿类 = row["矿类"].ToString();
            survey.矿山规模 = row["矿山规模"].ToString();
            survey.生产现状 = row["生产现状"].ToString();
            //survey.建矿时间 = row["建矿时间"].ToString();
            //survey.闭坑时间 = row["闭坑时间"].ToString();
            survey.矿区面积_公顷_ = ConvertHelper.GetDoubleValueFromStr(row["矿区面积(公顷)"].ToString());
            survey.开采方式 = row["开采方式"].ToString();
            survey.生产能力_万吨_年_ = ConvertHelper.GetDoubleValueFromStr(row["生产能力(万吨/年)"].ToString());
            survey.选矿能力_万吨_年_ = row["选矿能力(万吨/年)"].ToString();
            survey.累计采出矿石量_万吨_ = ConvertHelper.GetDoubleValueFromStr(row["累计采出矿石量(万吨)"].ToString());
            survey.本年度采出矿石量_万吨_ = row["本年度采出矿石量(万吨)"].ToString();
            survey.采空区面积_公顷_ = row["采空区面积(公顷)"].ToString();
            survey.最大采深_米_ = row["最大采深(米)"].ToString();
            survey.采厚_米_ = row["采厚(米)"].ToString();
            survey.保证金建立时间 = row["保证金建立时间"].ToString();
            survey.年度保证金缴纳额_万元_ = ConvertHelper.GetDoubleValueFromStr(row["年度保证金缴纳额(万元)"].ToString());
            survey.本年度保证金缴纳金额_万元_ = ConvertHelper.GetDoubleValueFromStr(row["本年度保证金缴纳金额(万元)"].ToString());
            survey.矿山保证金账户金额_万元_ = ConvertHelper.GetDoubleValueFromStr(row["矿山保证金账户金额(万元)"].ToString());
            survey.耕地 = ConvertHelper.GetDoubleValueFromStr(row["耕地"].ToString());
            survey.林地 = ConvertHelper.GetDoubleValueFromStr(row["林地"].ToString());
            survey.草地 = ConvertHelper.GetDoubleValueFromStr(row["草地"].ToString());
            survey.园地 = ConvertHelper.GetDoubleValueFromStr(row["园地"].ToString());
            survey.其它 = ConvertHelper.GetDoubleValueFromStr(row["其它"].ToString());
            survey.小计 = ConvertHelper.GetDoubleValueFromStr(row["小计"].ToString());
            survey.矿山地质环境恢复治理方案 = row["矿山地质环境恢复治理方案"].ToString();
            survey.应恢复治理面积_公顷_ = ConvertHelper.GetDoubleValueFromStr(row["应恢复治理面积(公顷)"].ToString());
            survey.矿山复绿行动需治理面积_公顷_ = ConvertHelper.GetDoubleValueFromStr(row["矿山复绿行动需治理面积(公顷)"].ToString());
            survey.治理资金渠道 = row["治理资金渠道"].ToString();
            survey.是否列入省级矿山复绿行动实施方案 = row["是否列入省级矿山复绿行动实施方案"].ToString();
            survey.C2013年 = ConvertHelper.GetDoubleValueFromStr(row["2013年"].ToString());
            survey.C2014年 = ConvertHelper.GetDoubleValueFromStr(row["2014年"].ToString());
            survey.C2015年 = ConvertHelper.GetDoubleValueFromStr(row["2015年"].ToString());
            survey.C2016年_2020年 = ConvertHelper.GetDoubleValueFromStr(row["2016年-2020年"].ToString());
            survey.治理年度 = row["治理年度"].ToString();
            survey.治理单价 = ConvertHelper.GetDoubleValueFromStr(row["治理单价"].ToString());
            survey.合计 = ConvertHelper.GetDoubleValueFromStr(row["合计"].ToString());
            survey.验收时间 = row["验收时间"].ToString();

            survey.调查人 = row["调查人"].ToString();
            survey.填表单位 = row["填表单位"].ToString();
            survey.填表 = row["填表"].ToString();

            survey.审核 = row["审核"].ToString();
            survey.填表日期 = ConvertHelper.GetDateTimeValueFromStr(row["填表日期"].ToString());
            survey.备注 = row["备注"].ToString();
            survey.地形地貌景观破坏_ = ConvertHelper.GetDoubleValueFromStr(row["地形地貌景观破坏"].ToString());
            survey.地面塌陷 = row["地面塌陷"].ToString();
            survey.崩塌 = row["崩塌"].ToString();
            survey.滑坡 = row["滑坡"].ToString();
            survey.泥石流 = row["泥石流"].ToString();
            survey.其他 = row["其他"].ToString();
            survey.治理现状 = row["治理现状"].ToString();
            survey.重点治理区名称 = row["重点治理区名称"].ToString();
            survey.治理区编号 = row["治理区编号"].ToString();


            return survey;
        }
    }
}
