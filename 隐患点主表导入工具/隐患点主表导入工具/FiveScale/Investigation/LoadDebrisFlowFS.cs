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
using NPOI.SS.Formula.Functions;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.FiveScale.Investigation
{
    public class LoadDebrisFlowFS
    {

        public virtual string InsertData(string filePath)
        {
            DataTable landTable = AccessHelper.GetDataTableByAccessFile(filePath, "泥石流主表");
            DataTable comTable = AccessHelper.GetDataTableByAccessFile(filePath, "汇总表");
            LoadComprehensiveFS loadCom = new LoadComprehensiveFS();
            ComprehensiveFSService compService = new ComprehensiveFSService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < landTable.Rows.Count; i++)
            {
                try
                {
                    DebrisFlowFS landSlip = GetDebrisFlowFs(landTable.Rows[i], landTable.Columns);
                    string uId = landTable.Rows[i]["统一编号"].ToString();
                    DataRow[] rows = comTable.Select("统一编号=" + uId);
                    //if (rows.Length > 1 || rows.Length < 1)
                    //    throw new Exception(@"不存在综合表或综合表不唯一");
                    if (rows.Length < 1)
                    {
                        throw new Exception(@"不存在与主表对应的综合表信息;");
                    }
                    if (rows.Length > 1)
                    {
                        throw new Exception(@"存在多条与主表对应的综合表信息;");
                    }


                    ComprehensiveFS comp = loadCom.GetComprehensiveFs(rows[0], comTable.Columns);
                    comp.DebrisFlowFS = landSlip;
                    compService.InsertComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，统一编号是：" + landTable.Rows[i]["统一编号"] + ";失败原因是：" + ex.Message + "\n");
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("泥石流表(5W)", landTable.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }

        public DebrisFlowFS GetDebrisFlowFs(DataRow row, DataColumnCollection columns)
        {
            DebrisFlowFS disaster = new DebrisFlowFS();

            #region  泥石流

            disaster.项目名称 = row["项目名称"].ToString();
            disaster.图幅名 = row["图幅名"].ToString();
            disaster.野外编号 = row["野外编号"].ToString();
            disaster.室内编号 = row["室内编号"].ToString();
            disaster.最大标高 = float.Parse(row["最大标高"].ToString() == "" ? "0" : row["最大标高"].ToString());
            disaster.最小标高 = float.Parse(row["最小标高"].ToString() == "" ? "0" : row["最小标高"].ToString());
            disaster.水系名称 = row["水系名称"].ToString();
            disaster.主河名称 = row["主河名称"].ToString();
            disaster.相对主河位置 = row["相对主河位置"].ToString();
            disaster.沟口至主河道距 = ConvertHelper.GetFloatValutFromStr(row["沟口至主河道距"].ToString());
            disaster.流动方向 = float.Parse(row["流动方向"].ToString() == "" ? "0" : row["流动方向"].ToString());
            disaster.水动力类型 = row["水动力类型"].ToString();
            disaster.沟口巨石A = ConvertHelper.GetFloatValutFromStr(row["沟口巨石A"].ToString());
            disaster.沟口巨石B = ConvertHelper.GetFloatValutFromStr(row["沟口巨石B"].ToString());
            disaster.沟口巨石C = ConvertHelper.GetFloatValutFromStr(row["沟口巨石C"].ToString());
            disaster.泥砂补给途径 = row["泥砂补给途径"].ToString();
            disaster.补给区位置 = row["补给区位置"].ToString();
            disaster.年最大降雨 = float.Parse(row["年最大降雨"].ToString() == "" ? "0" : row["年最大降雨"].ToString());
            disaster.年平均降雨 = float.Parse(row["年平均降雨"].ToString() == "" ? "0" : row["年平均降雨"].ToString());
            disaster.日最大降雨 = float.Parse(row["日最大降雨"].ToString() == "" ? "0" : row["日最大降雨"].ToString());
            disaster.日平均降雨 = float.Parse(row["日平均降雨"].ToString() == "" ? "0" : row["日平均降雨"].ToString());
            disaster.时最大降雨 = float.Parse(row["时最大降雨"].ToString() == "" ? "0" : row["时最大降雨"].ToString());
            disaster.时平均降雨 = float.Parse(row["时平均降雨"].ToString() == "" ? "0" : row["时平均降雨"].ToString());
            disaster.十分钟最大降雨 = float.Parse(row["十分钟最大降雨"].ToString() == "" ? "0" : row["十分钟最大降雨"].ToString());
            disaster.十分钟平均降雨 = float.Parse(row["十分钟平均降雨"].ToString() == "" ? "0" : row["十分钟平均降雨"].ToString());
            disaster.沟口扇形地完整性 = (int)(float.Parse(row["沟口扇形地完整性"].ToString() == "" ? "0" : row["沟口扇形地完整性"].ToString()));
            disaster.沟口扇形地变幅 = float.Parse(row["沟口扇形地变幅"].ToString() == "" ? "0" : row["沟口扇形地变幅"].ToString());
            disaster.沟口扇形地发展趋势 = row["沟口扇形地发展趋势"].ToString();
            disaster.沟口扇形地扇长 = float.Parse(row["沟口扇形地扇长"].ToString() == "" ? "0" : row["沟口扇形地扇长"].ToString());
            disaster.沟口扇形地扇宽 = float.Parse(row["沟口扇形地扇宽"].ToString() == "" ? "0" : row["沟口扇形地扇宽"].ToString());
            disaster.沟口扇形地扩散角 = float.Parse(row["沟口扇形地扩散角"].ToString() == "" ? "0" : row["沟口扇形地扩散角"].ToString());
            disaster.沟口扇形地挤压大河 = row["沟口扇形地挤压大河"].ToString();
            disaster.地质构造 = row["地质构造"].ToString();
            disaster.地震烈度 = row["地震烈度"].ToString();
            disaster.滑坡活动程度 = row["滑坡活动程度"].ToString();
            disaster.滑坡规模 = row["滑坡规模"].ToString();
            disaster.人工弃体活动程度 = row["人工弃体活动程度"].ToString();
            disaster.人工弃体规模 = row["人工弃体规模"].ToString();
            disaster.自然堆积活动程度 = row["自然堆积活动程度"].ToString();
            disaster.自然堆积规模 = row["自然堆积规模"].ToString();
            disaster.森林 = float.Parse(row["森林"].ToString() == "" ? "0" : row["森林"].ToString());
            disaster.灌丛 = float.Parse(row["灌丛"].ToString() == "" ? "0" : row["灌丛"].ToString());
            disaster.草地 = float.Parse(row["草地"].ToString() == "" ? "0" : row["草地"].ToString());
            disaster.缓坡耕地 = float.Parse(row["缓坡耕地"].ToString() == "" ? "0" : row["缓坡耕地"].ToString());
            disaster.荒地 = float.Parse(row["荒地"].ToString() == "" ? "0" : row["荒地"].ToString());
            disaster.陡坡耕地 = float.Parse(row["陡坡耕地"].ToString() == "" ? "0" : row["陡坡耕地"].ToString());
            disaster.建筑用地 = float.Parse(row["建筑用地"].ToString() == "" ? "0" : row["建筑用地"].ToString());
            disaster.其它用地 = float.Parse(row["其它用地"].ToString() == "" ? "0" : row["其它用地"].ToString());
            disaster.防治措施现状 = row["防治措施现状"].ToString();
            disaster.防治措施类型 = row["防治措施类型"].ToString();
            disaster.监测措施 = row["监测措施"].ToString();
            disaster.监测措施类型 = row["监测措施类型"].ToString();
            disaster.威胁危害对象 = row["威胁危害对象"].ToString();
            //flow.灾害史发生时间1 = row["灾害史发生时间年1"].ToString() + (row["灾害史发生时间月1"].ToString() == "" ? "" : "年" + row["灾害史发生时间月1"].ToString() + "月") + (row["灾害史发生时间日1"].ToString() == "" ? "" : row["灾害史发生时间日1"].ToString() + "日");
            disaster.灾害史发生时间年1 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间年1"].ToString());
            disaster.灾害史死亡人口1 = (int)(float.Parse(row["灾害史死亡人口1"].ToString() == "" ? "0" : row["灾害史死亡人口1"].ToString()));
            disaster.灾害史损失牲畜1 = int.Parse(row["灾害史损失牲畜1"].ToString() == "" ? "0" : row["灾害史损失牲畜1"].ToString());
            disaster.灾害史全毁房屋1 = (int)(float.Parse(row["灾害史全毁房屋1"].ToString() == "" ? "0" : row["灾害史全毁房屋1"].ToString()));
            disaster.灾害史半毁房屋1 = (int)(float.Parse(row["灾害史半毁房屋1"].ToString() == "" ? "0" : row["灾害史半毁房屋1"].ToString()));
            disaster.灾害史全毁农田1 = float.Parse(row["灾害史全毁农田1"].ToString() == "" ? "0" : row["灾害史全毁农田1"].ToString());
            disaster.灾害史半毁农田1 = float.Parse(row["灾害史半毁农田1"].ToString() == "" ? "0" : row["灾害史半毁农田1"].ToString());
            disaster.灾害史毁坏道路1 = float.Parse(row["灾害史毁坏道路1"].ToString() == "" ? "0" : row["灾害史毁坏道路1"].ToString());
            disaster.灾害史毁坏桥梁1 = (int)(float.Parse(row["灾害史毁坏桥梁1"].ToString() == "" ? "0" : row["灾害史毁坏桥梁1"].ToString()));
            disaster.灾害史直接损失1 = float.Parse(row["灾害史直接损失1"].ToString() == "" ? "0" : row["灾害史直接损失1"].ToString());
            disaster.灾害史灾情等级1 = row["灾害史灾情等级1"].ToString();
            //flow.灾害史发生时间2 = row["灾害史发生时间年2"].ToString() + (row["灾害史发生时间月2"].ToString() == "" ? "" : "年" + row["灾害史发生时间月2"].ToString() + "月") + (row["灾害史发生时间日2"].ToString() == "" ? "" : row["灾害史发生时间日2"].ToString() + "日");
            disaster.灾害史发生时间年2 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间年2"].ToString());
            disaster.灾害史死亡人口2 = (int)(float.Parse(row["灾害史死亡人口2"].ToString() == "" ? "0" : row["灾害史死亡人口2"].ToString()));
            disaster.灾害史损失牲畜2 = (int)(float.Parse(row["灾害史损失牲畜2"].ToString() == "" ? "0" : row["灾害史损失牲畜2"].ToString()));
            disaster.灾害史全毁房屋2 = (int)(float.Parse(row["灾害史全毁房屋2"].ToString() == "" ? "0" : row["灾害史全毁房屋2"].ToString()));
            disaster.灾害史半毁房屋2 = (int)(float.Parse(row["灾害史半毁房屋2"].ToString() == "" ? "0" : row["灾害史半毁房屋2"].ToString()));
            disaster.灾害史全毁农田2 = float.Parse(row["灾害史全毁农田2"].ToString() == "" ? "0" : row["灾害史全毁农田2"].ToString());
            disaster.灾害史半毁农田2 = float.Parse(row["灾害史半毁农田2"].ToString() == "" ? "0" : row["灾害史半毁农田2"].ToString());
            disaster.灾害史毁坏道路2 = float.Parse(row["灾害史毁坏道路2"].ToString() == "" ? "0" : row["灾害史毁坏道路2"].ToString());
            disaster.灾害史毁坏桥梁2 = (int)(float.Parse(row["灾害史毁坏桥梁2"].ToString() == "" ? "0" : row["灾害史毁坏桥梁2"].ToString()));
            disaster.灾害史直接损失2 = float.Parse(row["灾害史直接损失2"].ToString() == "" ? "0" : row["灾害史直接损失2"].ToString());
            disaster.灾害史灾情等级2 = row["灾害史灾情等级2"].ToString();
            //flow.灾害史发生时间3 = row["灾害史发生时间年3"].ToString() + (row["灾害史发生时间月3"].ToString() == "" ? "" : "年" + row["灾害史发生时间月3"].ToString() + "月") + (row["灾害史发生时间日3"].ToString() == "" ? "" : row["灾害史发生时间日3"].ToString() + "日");
            disaster.灾害史发生时间年3 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间年3"].ToString());
            disaster.灾害史死亡人口3 = (int)(float.Parse(row["灾害史死亡人口3"].ToString() == "" ? "0" : row["灾害史死亡人口3"].ToString()));
            disaster.灾害史损失牲畜3 = (int)(float.Parse(row["灾害史损失牲畜3"].ToString() == "" ? "0" : row["灾害史损失牲畜3"].ToString()));
            disaster.灾害史全毁房屋3 = (int)(float.Parse(row["灾害史全毁房屋3"].ToString() == "" ? "0" : row["灾害史全毁房屋3"].ToString()));
            disaster.灾害史半毁房屋3 = (int)(float.Parse(row["灾害史半毁房屋3"].ToString() == "" ? "0" : row["灾害史半毁房屋3"].ToString()));
            disaster.灾害史全毁农田3 = float.Parse(row["灾害史全毁农田3"].ToString() == "" ? "0" : row["灾害史全毁农田3"].ToString());
            disaster.灾害史半毁农田3 = float.Parse(row["灾害史半毁农田3"].ToString() == "" ? "0" : row["灾害史半毁农田3"].ToString());
            disaster.灾害史毁坏道路3 = float.Parse(row["灾害史毁坏道路3"].ToString() == "" ? "0" : row["灾害史毁坏道路3"].ToString());
            disaster.灾害史毁坏桥梁3 = (int)(float.Parse(row["灾害史毁坏桥梁3"].ToString() == "" ? "0" : row["灾害史毁坏桥梁3"].ToString()));
            disaster.灾害史直接损失3 = float.Parse(row["灾害史直接损失3"].ToString() == "" ? "0" : row["灾害史直接损失3"].ToString());
            disaster.灾害史灾情等级3 = row["灾害史灾情等级3"].ToString();
            //flow.灾害史发生时间4 = row["灾害史发生时间年4"].ToString() + (row["灾害史发生时间月4"].ToString() == "" ? "" : "年" + row["灾害史发生时间月4"].ToString() + "月") + (row["灾害史发生时间日4"].ToString() == "" ? "" : row["灾害史发生时间日4"].ToString() + "日");
            disaster.灾害史发生时间年4 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间年4"].ToString());
            disaster.灾害史死亡人口4 = (int)(float.Parse(row["灾害史死亡人口4"].ToString() == "" ? "0" : row["灾害史死亡人口4"].ToString()));
            disaster.灾害史损失牲畜4 = (int)(float.Parse(row["灾害史损失牲畜4"].ToString() == "" ? "0" : row["灾害史损失牲畜4"].ToString()));
            disaster.灾害史全毁房屋4 = (int)(float.Parse(row["灾害史全毁房屋4"].ToString() == "" ? "0" : row["灾害史全毁房屋4"].ToString()));
            disaster.灾害史半毁房屋4 = (int)(float.Parse(row["灾害史半毁房屋4"].ToString() == "" ? "0" : row["灾害史半毁房屋4"].ToString()));
            disaster.灾害史全毁农田4 = float.Parse(row["灾害史全毁农田4"].ToString() == "" ? "0" : row["灾害史全毁农田4"].ToString());
            disaster.灾害史半毁农田4 = float.Parse(row["灾害史半毁农田4"].ToString() == "" ? "0" : row["灾害史半毁农田4"].ToString());
            disaster.灾害史毁坏道路4 = float.Parse(row["灾害史毁坏道路4"].ToString() == "" ? "0" : row["灾害史毁坏道路4"].ToString());
            disaster.灾害史毁坏桥梁4 = (int)(float.Parse(row["灾害史毁坏桥梁4"].ToString() == "" ? "0" : row["灾害史毁坏桥梁4"].ToString()));
            disaster.灾害史直接损失4 = float.Parse(row["灾害史直接损失4"].ToString() == "" ? "0" : row["灾害史直接损失4"].ToString());
            disaster.灾害史灾情等级4 = row["灾害史灾情等级4"].ToString();
            //flow.灾害史发生时间5 = row["灾害史发生时间年5"].ToString() + (row["灾害史发生时间月5"].ToString() == "" ? "" : "年" + row["灾害史发生时间月5"].ToString() + "月") + (row["灾害史发生时间日5"].ToString() == "" ? "" : row["灾害史发生时间日5"].ToString() + "日");
            disaster.灾害史发生时间年5 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间年5"].ToString());
            disaster.灾害史死亡人口5 = (int)(float.Parse(row["灾害史死亡人口5"].ToString() == "" ? "0" : row["灾害史死亡人口5"].ToString()));
            disaster.灾害史损失牲畜5 = (int)(float.Parse(row["灾害史损失牲畜5"].ToString() == "" ? "0" : row["灾害史损失牲畜5"].ToString()));
            disaster.灾害史全毁房屋5 = (int)(float.Parse(row["灾害史全毁房屋5"].ToString() == "" ? "0" : row["灾害史全毁房屋5"].ToString()));
            disaster.灾害史半毁房屋5 = (int)(float.Parse(row["灾害史半毁房屋5"].ToString() == "" ? "0" : row["灾害史半毁房屋5"].ToString()));
            disaster.灾害史全毁农田5 = float.Parse(row["灾害史全毁农田5"].ToString() == "" ? "0" : row["灾害史全毁农田5"].ToString());
            disaster.灾害史半毁农田5 = float.Parse(row["灾害史半毁农田5"].ToString() == "" ? "0" : row["灾害史半毁农田5"].ToString());
            disaster.灾害史毁坏道路5 = float.Parse(row["灾害史毁坏道路5"].ToString() == "" ? "0" : row["灾害史毁坏道路5"].ToString());
            disaster.灾害史毁坏桥梁5 = (int)(float.Parse(row["灾害史毁坏桥梁5"].ToString() == "" ? "0" : row["灾害史毁坏桥梁5"].ToString()));
            disaster.灾害史直接损失5 = float.Parse(row["灾害史直接损失5"].ToString() == "" ? "0" : row["灾害史直接损失5"].ToString());
            disaster.灾害史灾情等级5 = row["灾害史灾情等级5"].ToString();
            disaster.泥石流冲出方量 = ConvertHelper.GetDoubleValueFromStr(row["泥石流冲出方量"].ToString());
            disaster.泥石流规模等级 = row["泥石流规模等级"].ToString();
            disaster.泥石流泥位 = ConvertHelper.GetFloatValutFromStr(row["泥石流泥位"].ToString());
            disaster.不良地质现象 = row["不良地质现象"].ToString();
            disaster.补给段长度比 = row["补给段长度比"].ToString();
            disaster.沟口扇形地 = row["沟口扇形地"].ToString();
            disaster.主沟纵坡 = row["主沟纵坡"].ToString();
            disaster.新构造影响 = row["新构造影响"].ToString();
            disaster.植被覆盖率 = row["植被覆盖率"].ToString();
            disaster.冲淤变幅 = row["冲淤变幅"].ToString();
            disaster.岩性因素 = row["岩性因素"].ToString();
            disaster.松散物储量 = row["松散物储量"].ToString();
            disaster.山坡坡度 = row["山坡坡度"].ToString();
            disaster.沟槽横断面 = row["沟槽横断面"].ToString();
            disaster.松散物平均厚 = row["松散物平均厚"].ToString();
            disaster.流域面积 = row["流域面积"].ToString();
            disaster.相对高差 = row["相对高差"].ToString();
            disaster.堵塞程度 = row["堵塞程度"].ToString();
            disaster.评分1 = ConvertHelper.GetIntValueByStr(row["评分1"].ToString());
            disaster.评分2 = ConvertHelper.GetIntValueByStr(row["评分2"].ToString());
            disaster.评分3 = ConvertHelper.GetIntValueByStr(row["评分3"].ToString());
            disaster.评分4 = ConvertHelper.GetIntValueByStr(row["评分4"].ToString());
            disaster.评分5 = ConvertHelper.GetIntValueByStr(row["评分5"].ToString());
            disaster.评分6 = ConvertHelper.GetIntValueByStr(row["评分6"].ToString());
            disaster.评分7 = ConvertHelper.GetIntValueByStr(row["评分7"].ToString());
            disaster.评分8 = ConvertHelper.GetIntValueByStr(row["评分8"].ToString());
            disaster.评分9 = ConvertHelper.GetIntValueByStr(row["评分9"].ToString());
            disaster.评分10 = ConvertHelper.GetIntValueByStr(row["评分10"].ToString());
            disaster.评分11 = ConvertHelper.GetIntValueByStr(row["评分11"].ToString());
            disaster.评分12 = ConvertHelper.GetIntValueByStr(row["评分12"].ToString());
            disaster.评分13 = ConvertHelper.GetIntValueByStr(row["评分13"].ToString());
            disaster.评分14 = ConvertHelper.GetIntValueByStr(row["评分14"].ToString());
            disaster.评分15 = ConvertHelper.GetIntValueByStr(row["评分15"].ToString());
            disaster.总分 = row["总分"].ToString();
            disaster.易发程度 = row["易发程度"].ToString();
            disaster.泥石流类型 = row["泥石流类型"].ToString();
            disaster.发展阶段 = row["发展阶段"].ToString();
            disaster.监测建议 = row["监测建议"].ToString();
            disaster.防治建议 = row["防治建议"].ToString();
            disaster.隐患点 = bool.Parse(row["隐患点"].ToString() == "" ? "false" : row["隐患点"].ToString());
            disaster.防灾预案 = bool.Parse(row["防灾预案"].ToString() == "" ? "false" : row["防灾预案"].ToString());
            disaster.多媒体 = bool.Parse(row["多媒体"].ToString() == "" ? "false" : row["多媒体"].ToString());
            disaster.群测人员 = row["群测人员"].ToString();
            disaster.村长 = row["村长"].ToString();
            disaster.电话 = row["电话"].ToString();
            disaster.调查负责人 = row["调查负责人"].ToString();
            disaster.填表人 = row["填表人"].ToString();
            disaster.审核人 = row["审核人"].ToString();
            disaster.调查单位 = row["调查单位"].ToString();
            //flow.填表日期 = row["填表日期年"].ToString() + (row["填表日期月"].ToString() == "" ? "" : "年" + row["填表日期月"].ToString() + "月") + (row["填表日期日"].ToString() == "" ? "" : row["填表日期日"].ToString() + "日");
            disaster.填表日期年 = ConvertHelper.GetIntValueByStr(row["填表日期年"].ToString());
            disaster.填表日期月 = ConvertHelper.GetIntValueByStr(row["填表日期月"].ToString());
            disaster.填表日期日 = ConvertHelper.GetIntValueByStr(row["填表日期日"].ToString());
            disaster.xxcs1 = row["xxcs1"].ToString();
            disaster.xxcs2 = row["xxcs2"].ToString();
            disaster.xxcs3 = row["xxcs3"].ToString();
            disaster.xxcs4 = row["xxcs4"].ToString();
            disaster.xxcs5 = row["xxcs5"].ToString();
            disaster.xxcs6 = row["xxcs6"].ToString();
            disaster.xxcs7 = row["xxcs7"].ToString();
            disaster.xxcs8 = row["xxcs8"].ToString();
            disaster.xxcs9 = row["xxcs9"].ToString();
            disaster.xxcs10 = row["xxcs10"].ToString();
            disaster.xxcs11 = row["xxcs11"].ToString();
            disaster.xxcs12 = row["xxcs12"].ToString();
            disaster.xxcs13 = row["xxcs13"].ToString();
            disaster.xxcs14 = row["xxcs14"].ToString();
            disaster.xxcs15 = row["xxcs15"].ToString();
            disaster.示意图 = bool.Parse(row["示意图"].ToString());
            disaster.泥石流情况 = row["泥石流情况"].ToString();
            //flow.灾害体积 = 0;
            //flow.平面示意图路径 = row["矢量示意图"].ToString();
            //flow.剖面示意图路径 = row["矢量示意图"].ToString();

            //新增字段
            disaster.项目名称 = row["项目名称"].ToString();
            disaster.图幅名 = row["图幅名"].ToString();
            disaster.图幅编号 = row["图幅编号"].ToString();
            disaster.县市编号 = row["县市编号"].ToString();
            disaster.省 = row["省"].ToString();
            disaster.市 = row["市"].ToString();
            disaster.县 = row["县"].ToString();
            disaster.乡 = row["乡"].ToString();
            disaster.村 = row["村"].ToString();
            disaster.组 = row["组"].ToString();
            disaster.地点 = row["地点"].ToString();
            disaster.其它 = row["其它"].ToString();
            disaster.危害对象 = row["危害对象"].ToString();
            disaster.死亡人数 = ConvertHelper.GetIntValueByStr(row["死亡人数"].ToString());
            disaster.直接经济损失 = ConvertHelper.GetDoubleValueFromStr(row["直接经济损失"].ToString());
            disaster.灾情等级 = row["灾情等级"].ToString();
            disaster.灾害史发生时间年1 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间年1"].ToString());
            disaster.灾害史发生时间月1 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间月1"].ToString());
            disaster.灾害史发生时间日1 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间日1"].ToString());
            disaster.灾害史发生时间年2 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间年2"].ToString());
            disaster.灾害史发生时间月2 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间月2"].ToString());
            disaster.灾害史发生时间日2 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间日2"].ToString());
            disaster.灾害史发生时间年3 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间年3"].ToString());
            disaster.灾害史发生时间月3 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间月3"].ToString());
            disaster.灾害史发生时间日3 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间日3"].ToString());
            disaster.灾害史发生时间年4 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间年4"].ToString());
            disaster.灾害史发生时间月4 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间月4"].ToString());
            disaster.灾害史发生时间日4 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间日4"].ToString());
            disaster.灾害史发生时间年5 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间年5"].ToString());
            disaster.灾害史发生时间月5 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间月5"].ToString());
            disaster.灾害史发生时间日5 = ConvertHelper.GetIntValueByStr(row["灾害史发生时间日5"].ToString());
            disaster.暴发频率 = ConvertHelper.GetFloatValutFromStr(row["暴发频率"].ToString());
            disaster.防治监测 = row["防治监测"].ToString();
            disaster.防治治理 = row["防治治理"].ToString();
            disaster.搬迁避让 = row["搬迁避让"].ToString();
            disaster.群测群防 = row["群测群防"].ToString();
            disaster.遥感点 = ConvertHelper.GetBooleanByStr(row["遥感点"].ToString());
            disaster.勘查点 = ConvertHelper.GetBooleanByStr(row["勘查点"].ToString());
            disaster.测绘点 = ConvertHelper.GetBooleanByStr(row["测绘点"].ToString());
            disaster.录像 = ConvertHelper.GetBooleanByStr(row["录像"].ToString());
            disaster.填表日期年 = ConvertHelper.GetIntValueByStr(row["填表日期年"].ToString());
            disaster.填表日期月 = ConvertHelper.GetIntValueByStr(row["填表日期月"].ToString());
            disaster.填表日期日 = ConvertHelper.GetIntValueByStr(row["填表日期日"].ToString());
            disaster.矢量示意图 = ConvertHelper.GetBooleanByStr(row["矢量示意图"].ToString());
            disaster.野外记录信息 = row["野外记录信息"].ToString();
            disaster.威胁房屋户 = ConvertHelper.GetIntValueByStr(row["威胁房屋户"].ToString());
            disaster.补给段长度值 = ConvertHelper.GetFloatValutFromStr(row["补给段长度值"].ToString());
            disaster.主沟纵坡值 = ConvertHelper.GetFloatValutFromStr(row["主沟纵坡值"].ToString());
            disaster.植被覆盖值 = ConvertHelper.GetFloatValutFromStr(row["植被覆盖值"].ToString());
            disaster.冲淤变幅值 = ConvertHelper.GetFloatValutFromStr(row["冲淤变幅值"].ToString());
            disaster.松散物储量值 = ConvertHelper.GetFloatValutFromStr(row["松散物储量值"].ToString());
            disaster.山坡坡度值 = ConvertHelper.GetFloatValutFromStr(row["山坡坡度值"].ToString());
            disaster.松散物平均厚值 = ConvertHelper.GetFloatValutFromStr(row["松散物平均厚值"].ToString());
            disaster.流域面积值 = ConvertHelper.GetFloatValutFromStr(row["流域面积值"].ToString());
            disaster.相对高差值 = ConvertHelper.GetFloatValutFromStr(row["相对高差值"].ToString());

            #endregion

            return disaster;
        }
    }
}
