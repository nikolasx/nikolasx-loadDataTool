using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikolasHelper.Office;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Investigation;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.Investigation
{
    public class LoadDebrisFlow
    {



        public virtual string InsertData(string filePath)
        {
            DataTable flowTable = AccessHelper.GetDataTableByAccessFile(filePath, "泥石流主表");
            DataTable comTable = AccessHelper.GetDataTableByAccessFile(filePath, "综合表");
            LoadComprehensive loadCom = new LoadComprehensive();
            ComprehensiveService compService = new ComprehensiveService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < flowTable.Rows.Count; i++)
            {
                try
                {
                    DebrisFlow flow = GetDebrisFlow(flowTable.Rows[i], flowTable.Columns);
                    string uId = flowTable.Rows[i]["统一编号"].ToString();
                    DataRow[] rows = comTable.Select("统一编号=" + uId);
                    if (rows.Length > 1 || rows.Length < 1)
                        throw new Exception(@"不存在综合表或综合表不唯一");

                    Comprehensive comp = loadCom.GetComprehensive(rows[0], comTable.Columns);
                    comp.DebrisFlow = flow;
                    compService.InsertComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，统一编号:" + flowTable.Rows[i]["统一编号"] + ";失败原因是：" + ex.Message + "\n");
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("泥石流表", flowTable.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }



        /// <summary>
        /// 从Excel中获取泥石流信息
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public DebrisFlow GetDebrisFlow(DataRow row, DataColumnCollection columns)
        {
            DebrisFlow flow = new DebrisFlow();



            #region  泥石流
            flow.野外编号 = row["野外编号"].ToString();
            flow.室内编号 = row["室内编号"].ToString();
            flow.最大标高 = float.Parse(row["最大标高"].ToString() == "" ? "0" : row["最大标高"].ToString());
            flow.最小标高 = float.Parse(row["最小标高"].ToString() == "" ? "0" : row["最小标高"].ToString());
            flow.水系名称 = row["水系名称"].ToString();
            flow.主河名称 = row["主河名称"].ToString();
            flow.相对主河位置 = row["相对主河位置"].ToString();
            flow.沟口至主河道距 = row["沟口至主河道距"].ToString();
            flow.流动方向 = float.Parse(row["流动方向"].ToString() == "" ? "0" : row["流动方向"].ToString());
            flow.水动力类型 = row["水动力类型"].ToString();
            flow.沟口巨石A = row["沟口巨石A"].ToString();
            flow.沟口巨石B = row["沟口巨石B"].ToString();
            flow.沟口巨石C = row["沟口巨石C"].ToString();
            flow.泥砂补给途径 = row["泥砂补给途径"].ToString();
            flow.补给区位置 = row["补给区位置"].ToString();
            flow.年最大降雨 = float.Parse(row["年最大降雨"].ToString() == "" ? "0" : row["年最大降雨"].ToString());
            flow.年平均降雨 = float.Parse(row["年平均降雨"].ToString() == "" ? "0" : row["年平均降雨"].ToString());
            flow.日最大降雨 = float.Parse(row["日最大降雨"].ToString() == "" ? "0" : row["日最大降雨"].ToString());
            flow.日平均降雨 = float.Parse(row["日平均降雨"].ToString() == "" ? "0" : row["日平均降雨"].ToString());
            flow.时最大降雨 = float.Parse(row["时最大降雨"].ToString() == "" ? "0" : row["时最大降雨"].ToString());
            flow.时平均降雨 = float.Parse(row["时平均降雨"].ToString() == "" ? "0" : row["时平均降雨"].ToString());
            flow.十分钟最大降雨 = float.Parse(row["十分钟最大降雨"].ToString() == "" ? "0" : row["十分钟最大降雨"].ToString());
            flow.十分钟平均降雨 = float.Parse(row["十分钟平均降雨"].ToString() == "" ? "0" : row["十分钟平均降雨"].ToString());
            flow.沟口扇形地完整性 = (int)(float.Parse(row["沟口扇形地完整性"].ToString() == "" ? "0" : row["沟口扇形地完整性"].ToString()));
            flow.沟口扇形地变幅 = float.Parse(row["沟口扇形地变幅"].ToString() == "" ? "0" : row["沟口扇形地变幅"].ToString());
            flow.沟口扇形地发展趋势 = row["沟口扇形地发展趋势"].ToString();
            flow.沟口扇形地扇长 = float.Parse(row["沟口扇形地扇长"].ToString() == "" ? "0" : row["沟口扇形地扇长"].ToString());
            flow.沟口扇形地扇宽 = float.Parse(row["沟口扇形地扇宽"].ToString() == "" ? "0" : row["沟口扇形地扇宽"].ToString());
            flow.沟口扇形地扩散角 = float.Parse(row["沟口扇形地扩散角"].ToString() == "" ? "0" : row["沟口扇形地扩散角"].ToString());
            flow.沟口扇形地挤压大河 = row["沟口扇形地挤压大河"].ToString();
            flow.地质构造 = row["地质构造"].ToString();
            flow.地震烈度 = row["地震烈度"].ToString();
            flow.滑坡活动程度 = row["滑坡活动程度"].ToString();
            flow.滑坡规模 = row["滑坡规模"].ToString();
            flow.人工弃体活动程度 = row["人工弃体活动程度"].ToString();
            flow.人工弃体规模 = row["人工弃体规模"].ToString();
            flow.自然堆积活动程度 = row["自然堆积活动程度"].ToString();
            flow.自然堆积规模 = row["自然堆积规模"].ToString();
            flow.森林 = float.Parse(row["森林"].ToString() == "" ? "0" : row["森林"].ToString());
            flow.灌丛 = float.Parse(row["灌丛"].ToString() == "" ? "0" : row["灌丛"].ToString());
            flow.草地 = float.Parse(row["草地"].ToString() == "" ? "0" : row["草地"].ToString());
            flow.缓坡耕地 = float.Parse(row["缓坡耕地"].ToString() == "" ? "0" : row["缓坡耕地"].ToString());
            flow.荒地 = float.Parse(row["荒地"].ToString() == "" ? "0" : row["荒地"].ToString());
            flow.陡坡耕地 = float.Parse(row["陡坡耕地"].ToString() == "" ? "0" : row["陡坡耕地"].ToString());
            flow.建筑用地 = float.Parse(row["建筑用地"].ToString() == "" ? "0" : row["建筑用地"].ToString());
            flow.其它用地 = float.Parse(row["其它用地"].ToString() == "" ? "0" : row["其它用地"].ToString());
            flow.防治措施现状 = bool.Parse(row["防治措施现状"].ToString() == "有" ? "true" : "false");
            flow.防治措施类型 = row["防治措施类型"].ToString();
            flow.监测措施 = bool.Parse(row["监测措施"].ToString() == "有" ? "true" : "false");
            flow.监测措施类型 = row["监测措施类型"].ToString();
            flow.威胁危害对象 = row["威胁危害对象"].ToString();
            //flow.灾害史发生时间1 = row["灾害史发生时间年1"].ToString() + (row["灾害史发生时间月1"].ToString() == "" ? "" : "年" + row["灾害史发生时间月1"].ToString() + "月") + (row["灾害史发生时间日1"].ToString() == "" ? "" : row["灾害史发生时间日1"].ToString() + "日");
            flow.灾害史发生时间1 = row["灾害史发生时间1"].ToString();
            flow.灾害史死亡人口1 = (int)(float.Parse(row["灾害史死亡人口1"].ToString() == "" ? "0" : row["灾害史死亡人口1"].ToString()));
            flow.灾害史损失牲畜1 = float.Parse(row["灾害史损失牲畜1"].ToString() == "" ? "0" : row["灾害史损失牲畜1"].ToString());
            flow.灾害史全毁房屋1 = (int)(float.Parse(row["灾害史全毁房屋1"].ToString() == "" ? "0" : row["灾害史全毁房屋1"].ToString()));
            flow.灾害史半毁房屋1 = (int)(float.Parse(row["灾害史半毁房屋1"].ToString() == "" ? "0" : row["灾害史半毁房屋1"].ToString()));
            flow.灾害史全毁农田1 = float.Parse(row["灾害史全毁农田1"].ToString() == "" ? "0" : row["灾害史全毁农田1"].ToString());
            flow.灾害史半毁农田1 = float.Parse(row["灾害史半毁农田1"].ToString() == "" ? "0" : row["灾害史半毁农田1"].ToString());
            flow.灾害史毁坏道路1 = float.Parse(row["灾害史毁坏道路1"].ToString() == "" ? "0" : row["灾害史毁坏道路1"].ToString());
            flow.灾害史毁坏桥梁1 = (int)(float.Parse(row["灾害史毁坏桥梁1"].ToString() == "" ? "0" : row["灾害史毁坏桥梁1"].ToString()));
            flow.灾害史直接损失1 = float.Parse(row["灾害史直接损失1"].ToString() == "" ? "0" : row["灾害史直接损失1"].ToString());
            flow.灾害史灾情等级1 = row["灾害史灾情等级1"].ToString();
            //flow.灾害史发生时间2 = row["灾害史发生时间年2"].ToString() + (row["灾害史发生时间月2"].ToString() == "" ? "" : "年" + row["灾害史发生时间月2"].ToString() + "月") + (row["灾害史发生时间日2"].ToString() == "" ? "" : row["灾害史发生时间日2"].ToString() + "日");
            flow.灾害史发生时间2 = row["灾害史发生时间2"].ToString();
            flow.灾害史死亡人口2 = (int)(float.Parse(row["灾害史死亡人口2"].ToString() == "" ? "0" : row["灾害史死亡人口2"].ToString()));
            flow.灾害史损失牲畜2 = (int)(float.Parse(row["灾害史损失牲畜2"].ToString() == "" ? "0" : row["灾害史损失牲畜2"].ToString()));
            flow.灾害史全毁房屋2 = (int)(float.Parse(row["灾害史全毁房屋2"].ToString() == "" ? "0" : row["灾害史全毁房屋2"].ToString()));
            flow.灾害史半毁房屋2 = (int)(float.Parse(row["灾害史半毁房屋2"].ToString() == "" ? "0" : row["灾害史半毁房屋2"].ToString()));
            flow.灾害史全毁农田2 = float.Parse(row["灾害史全毁农田2"].ToString() == "" ? "0" : row["灾害史全毁农田2"].ToString());
            flow.灾害史半毁农田2 = float.Parse(row["灾害史半毁农田2"].ToString() == "" ? "0" : row["灾害史半毁农田2"].ToString());
            flow.灾害史毁坏道路2 = float.Parse(row["灾害史毁坏道路2"].ToString() == "" ? "0" : row["灾害史毁坏道路2"].ToString());
            flow.灾害史毁坏桥梁2 = (int)(float.Parse(row["灾害史毁坏桥梁2"].ToString() == "" ? "0" : row["灾害史毁坏桥梁2"].ToString()));
            flow.灾害史直接损失2 = float.Parse(row["灾害史直接损失2"].ToString() == "" ? "0" : row["灾害史直接损失2"].ToString());
            flow.灾害史灾情等级2 = row["灾害史灾情等级2"].ToString();
            //flow.灾害史发生时间3 = row["灾害史发生时间年3"].ToString() + (row["灾害史发生时间月3"].ToString() == "" ? "" : "年" + row["灾害史发生时间月3"].ToString() + "月") + (row["灾害史发生时间日3"].ToString() == "" ? "" : row["灾害史发生时间日3"].ToString() + "日");
            flow.灾害史发生时间3 = row["灾害史发生时间3"].ToString();
            flow.灾害史死亡人口3 = (int)(float.Parse(row["灾害史死亡人口3"].ToString() == "" ? "0" : row["灾害史死亡人口3"].ToString()));
            flow.灾害史损失牲畜3 = (int)(float.Parse(row["灾害史损失牲畜3"].ToString() == "" ? "0" : row["灾害史损失牲畜3"].ToString()));
            flow.灾害史全毁房屋3 = (int)(float.Parse(row["灾害史全毁房屋3"].ToString() == "" ? "0" : row["灾害史全毁房屋3"].ToString()));
            flow.灾害史半毁房屋3 = (int)(float.Parse(row["灾害史半毁房屋3"].ToString() == "" ? "0" : row["灾害史半毁房屋3"].ToString()));
            flow.灾害史全毁农田3 = float.Parse(row["灾害史全毁农田3"].ToString() == "" ? "0" : row["灾害史全毁农田3"].ToString());
            flow.灾害史半毁农田3 = float.Parse(row["灾害史半毁农田3"].ToString() == "" ? "0" : row["灾害史半毁农田3"].ToString());
            flow.灾害史毁坏道路3 = float.Parse(row["灾害史毁坏道路3"].ToString() == "" ? "0" : row["灾害史毁坏道路3"].ToString());
            flow.灾害史毁坏桥梁3 = (int)(float.Parse(row["灾害史毁坏桥梁3"].ToString() == "" ? "0" : row["灾害史毁坏桥梁3"].ToString()));
            flow.灾害史直接损失3 = float.Parse(row["灾害史直接损失3"].ToString() == "" ? "0" : row["灾害史直接损失3"].ToString());
            flow.灾害史灾情等级3 = row["灾害史灾情等级3"].ToString();
            //flow.灾害史发生时间4 = row["灾害史发生时间年4"].ToString() + (row["灾害史发生时间月4"].ToString() == "" ? "" : "年" + row["灾害史发生时间月4"].ToString() + "月") + (row["灾害史发生时间日4"].ToString() == "" ? "" : row["灾害史发生时间日4"].ToString() + "日");
            flow.灾害史发生时间4 = row["灾害史发生时间4"].ToString();
            flow.灾害史死亡人口4 = (int)(float.Parse(row["灾害史死亡人口4"].ToString() == "" ? "0" : row["灾害史死亡人口4"].ToString()));
            flow.灾害史损失牲畜4 = (int)(float.Parse(row["灾害史损失牲畜4"].ToString() == "" ? "0" : row["灾害史损失牲畜4"].ToString()));
            flow.灾害史全毁房屋4 = (int)(float.Parse(row["灾害史全毁房屋4"].ToString() == "" ? "0" : row["灾害史全毁房屋4"].ToString()));
            flow.灾害史半毁房屋4 = (int)(float.Parse(row["灾害史半毁房屋4"].ToString() == "" ? "0" : row["灾害史半毁房屋4"].ToString()));
            flow.灾害史全毁农田4 = float.Parse(row["灾害史全毁农田4"].ToString() == "" ? "0" : row["灾害史全毁农田4"].ToString());
            flow.灾害史半毁农田4 = float.Parse(row["灾害史半毁农田4"].ToString() == "" ? "0" : row["灾害史半毁农田4"].ToString());
            flow.灾害史毁坏道路4 = float.Parse(row["灾害史毁坏道路4"].ToString() == "" ? "0" : row["灾害史毁坏道路4"].ToString());
            flow.灾害史毁坏桥梁4 = (int)(float.Parse(row["灾害史毁坏桥梁4"].ToString() == "" ? "0" : row["灾害史毁坏桥梁4"].ToString()));
            flow.灾害史直接损失4 = float.Parse(row["灾害史直接损失4"].ToString() == "" ? "0" : row["灾害史直接损失4"].ToString());
            flow.灾害史灾情等级4 = row["灾害史灾情等级4"].ToString();
            //flow.灾害史发生时间5 = row["灾害史发生时间年5"].ToString() + (row["灾害史发生时间月5"].ToString() == "" ? "" : "年" + row["灾害史发生时间月5"].ToString() + "月") + (row["灾害史发生时间日5"].ToString() == "" ? "" : row["灾害史发生时间日5"].ToString() + "日");
            flow.灾害史发生时间5 = row["灾害史发生时间5"].ToString();
            flow.灾害史死亡人口5 = (int)(float.Parse(row["灾害史死亡人口5"].ToString() == "" ? "0" : row["灾害史死亡人口5"].ToString()));
            flow.灾害史损失牲畜5 = (int)(float.Parse(row["灾害史损失牲畜5"].ToString() == "" ? "0" : row["灾害史损失牲畜5"].ToString()));
            flow.灾害史全毁房屋5 = (int)(float.Parse(row["灾害史全毁房屋5"].ToString() == "" ? "0" : row["灾害史全毁房屋5"].ToString()));
            flow.灾害史半毁房屋5 = (int)(float.Parse(row["灾害史半毁房屋5"].ToString() == "" ? "0" : row["灾害史半毁房屋5"].ToString()));
            flow.灾害史全毁农田5 = float.Parse(row["灾害史全毁农田5"].ToString() == "" ? "0" : row["灾害史全毁农田5"].ToString());
            flow.灾害史半毁农田5 = float.Parse(row["灾害史半毁农田5"].ToString() == "" ? "0" : row["灾害史半毁农田5"].ToString());
            flow.灾害史毁坏道路5 = float.Parse(row["灾害史毁坏道路5"].ToString() == "" ? "0" : row["灾害史毁坏道路5"].ToString());
            flow.灾害史毁坏桥梁5 = (int)(float.Parse(row["灾害史毁坏桥梁5"].ToString() == "" ? "0" : row["灾害史毁坏桥梁5"].ToString()));
            flow.灾害史直接损失5 = float.Parse(row["灾害史直接损失5"].ToString() == "" ? "0" : row["灾害史直接损失5"].ToString());
            flow.灾害史灾情等级5 = row["灾害史灾情等级5"].ToString();
            flow.泥石流冲出方量 = row["泥石流冲出方量"].ToString();
            flow.泥石流规模等级 = row["泥石流规模等级"].ToString();
            flow.泥石流泥位 = row["泥石流泥位"].ToString();
            flow.不良地质现象 = row["不良地质现象"].ToString();
            flow.补给段长度比 = row["补给段长度比"].ToString();
            flow.沟口扇形地 = row["沟口扇形地"].ToString();
            flow.主沟纵坡 = row["主沟纵坡"].ToString();
            flow.新构造影响 = row["新构造影响"].ToString();
            flow.植被覆盖率 = row["植被覆盖率"].ToString();
            flow.冲淤变幅 = row["冲淤变幅"].ToString();
            flow.岩性因素 = row["岩性因素"].ToString();
            flow.松散物储量 = row["松散物储量"].ToString();
            flow.山坡坡度 = row["山坡坡度"].ToString();
            flow.沟槽横断面 = row["沟槽横断面"].ToString();
            flow.松散物平均厚 = row["松散物平均厚"].ToString();
            flow.流域面积 = row["流域面积"].ToString();
            flow.相对高差 = row["相对高差"].ToString();
            flow.堵塞程度 = row["堵塞程度"].ToString();
            flow.评分1 = row["评分1"].ToString();
            flow.评分2 = row["评分2"].ToString();
            flow.评分3 = row["评分3"].ToString();
            flow.评分4 = row["评分4"].ToString();
            flow.评分5 = row["评分5"].ToString();
            flow.评分6 = row["评分6"].ToString();
            flow.评分7 = row["评分7"].ToString();
            flow.评分8 = row["评分8"].ToString();
            flow.评分9 = row["评分9"].ToString();
            flow.评分10 = row["评分10"].ToString();
            flow.评分11 = row["评分11"].ToString();
            flow.评分12 = row["评分12"].ToString();
            flow.评分13 = row["评分13"].ToString();
            flow.评分14 = row["评分14"].ToString();
            flow.评分15 = row["评分15"].ToString();
            flow.总分 = row["总分"].ToString();
            flow.易发程度 = row["易发程度"].ToString();
            flow.泥石流类型 = row["泥石流类型"].ToString();
            flow.发展阶段 = row["发展阶段"].ToString();
            flow.监测建议 = row["监测建议"].ToString();
            flow.防治建议 = row["防治建议"].ToString();
            flow.隐患点 = bool.Parse(row["隐患点"].ToString() == "" ? "false" : row["隐患点"].ToString());
            flow.防灾预案 = bool.Parse(row["防灾预案"].ToString() == "" ? "false" : row["防灾预案"].ToString());
            flow.多媒体 = bool.Parse(row["多媒体"].ToString() == "" ? "false" : row["多媒体"].ToString());
            flow.群测人员 = row["群测人员"].ToString();
            flow.村长 = row["村长"].ToString();
            flow.电话 = row["电话"].ToString();
            flow.调查负责人 = row["调查负责人"].ToString();
            flow.填表人 = row["填表人"].ToString();
            flow.审核人 = row["审核人"].ToString();
            flow.调查单位 = row["调查单位"].ToString();
            //flow.填表日期 = row["填表日期年"].ToString() + (row["填表日期月"].ToString() == "" ? "" : "年" + row["填表日期月"].ToString() + "月") + (row["填表日期日"].ToString() == "" ? "" : row["填表日期日"].ToString() + "日");
            flow.填表日期 = row["填表日期"].ToString();
            flow.xxcs1 = row["xxcs1"].ToString();
            flow.xxcs2 = row["xxcs2"].ToString();
            flow.xxcs3 = row["xxcs3"].ToString();
            flow.xxcs4 = row["xxcs4"].ToString();
            flow.xxcs5 = row["xxcs5"].ToString();
            flow.xxcs6 = row["xxcs6"].ToString();
            flow.xxcs7 = row["xxcs7"].ToString();
            flow.xxcs8 = row["xxcs8"].ToString();
            flow.xxcs9 = row["xxcs9"].ToString();
            flow.xxcs10 = row["xxcs10"].ToString();
            flow.xxcs11 = row["xxcs11"].ToString();
            flow.xxcs12 = row["xxcs12"].ToString();
            flow.xxcs13 = row["xxcs13"].ToString();
            flow.xxcs14 = row["xxcs14"].ToString();
            flow.xxcs15 = row["xxcs15"].ToString();
            flow.示意图 = (byte[])(row["示意图"].ToString() == "" ? null : row["示意图"]);
            flow.泥石流情况 = row["泥石流情况"].ToString();
            //flow.灾害体积 = 0;
            //flow.平面示意图路径 = row["矢量示意图"].ToString();
            //flow.剖面示意图路径 = row["矢量示意图"].ToString();

            #endregion

            return flow;
        }

    }



}
