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
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Investigation;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.FiveScale.Investigation
{

    /// <summary>
    /// 滑坡表
    /// </summary>
    public class LoadLandSlideFS
    {

        public virtual string InsertData(string filePath)
        {
            DataTable landTable = AccessHelper.GetDataTableByAccessFile(filePath, "滑坡主表");
            DataTable comTable = AccessHelper.GetDataTableByAccessFile(filePath, "汇总表");
            LoadComprehensiveFS loadCom = new LoadComprehensiveFS();
            ComprehensiveFSService compService = new ComprehensiveFSService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < landTable.Rows.Count; i++)
            {
                try
                {
                    LandSlideFS landSlip = GetLandSlideFS(landTable.Rows[i], landTable.Columns);
                    string uId = landTable.Rows[i]["统一编号"].ToString();
                    DataRow[] rows = comTable.Select("统一编号=" + uId);
                    if (rows.Length < 1)
                    {
                        throw new Exception(@"不存在与主表对应的综合表信息;");
                    }
                    if (rows.Length > 1)
                    {
                        throw new Exception(@"存在多条与主表对应的综合表信息;");
                    }

                    ComprehensiveFS comp = loadCom.GetComprehensiveFs(rows[0], comTable.Columns);
                    comp.LandSlideFS = landSlip;
                    compService.InsertComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，统一编号是：" + landTable.Rows[i]["统一编号"] + ";失败原因是：" + ex.Message + "\n");
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("滑坡表(5W)", landTable.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }


        public LandSlideFS GetLandSlideFS(DataRow row, DataColumnCollection columns)
        {
            LandSlideFS disaster = new LandSlideFS();

            #region LandSlide
            disaster.野外编号 = row["野外编号"].ToString();
            disaster.室内编号 = row["室内编号"].ToString();
            disaster.滑坡年代 = row["滑坡年代"].ToString();
            //landslide.滑坡时间 = row["滑坡时间年"].ToString() + (row["滑坡时间月"].ToString() == "" ? "" : "年" + row["滑坡时间月"].ToString() + "月") + (row["滑坡时间日"].ToString() == "" ? "" : row["滑坡时间日"].ToString() + "日");
            //disaster.滑坡时间 = row["滑坡时间"].ToString();
            disaster.滑坡类型 = row["滑坡类型"].ToString();
            //disaster.滑体性质 = row["滑体性质"].ToString();   //
            disaster.冠 = float.Parse(row["冠"].ToString() == "" ? "0" : row["冠"].ToString());
            disaster.趾 = float.Parse(row["趾"].ToString() == "" ? "0" : row["趾"].ToString());
            disaster.地层时代 = row["地层时代"].ToString();
            disaster.地层岩性 = row["地层岩性"].ToString();
            disaster.构造部位 = row["构造部位"].ToString();
            disaster.地震烈度 = row["地震烈度"].ToString();
            disaster.地层倾向 = (int)(float.Parse(row["地层倾向"].ToString() == "" ? "0" : row["地层倾向"].ToString()));
            disaster.地层倾角 = (int)(float.Parse(row["地层倾角"].ToString() == "" ? "0" : row["地层倾角"].ToString()));
            disaster.微地貌 = row["微地貌"].ToString();
            disaster.地下水类型 = row["地下水类型"].ToString();
            disaster.年均降雨量 = float.Parse(row["年均降雨量"].ToString() == "" ? "0" : row["年均降雨量"].ToString());
            disaster.日最大降雨量 = float.Parse(row["日最大降雨量"].ToString() == "" ? "0" : row["日最大降雨量"].ToString());
            disaster.时最大降雨量 = float.Parse(row["时最大降雨量"].ToString() == "" ? "0" : row["时最大降雨量"].ToString());
            disaster.洪水位 = float.Parse(row["洪水位"].ToString() == "" ? "0" : row["洪水位"].ToString());
            disaster.枯水位 = float.Parse(row["枯水位"].ToString() == "" ? "0" : row["枯水位"].ToString());
            disaster.相对河流位置 = row["相对河流位置"].ToString();
            disaster.原始坡高 = float.Parse(row["原始坡高"].ToString() == "" ? "0" : row["原始坡高"].ToString());
            disaster.原始坡度 = float.Parse(row["原始坡度"].ToString() == "" ? "0" : row["原始坡度"].ToString());
            disaster.原始坡形 = row["原始坡形"].ToString();
            disaster.斜坡结构类型 = row["斜坡结构类型"].ToString();
            disaster.控滑结构面类型1 = row["控滑结构面类型1"].ToString();
            disaster.控滑结构面倾向1 = (int)(float.Parse(row["控滑结构面倾向1"].ToString() == "" ? "0" : row["控滑结构面倾向1"].ToString()));
            disaster.控滑结构面倾角1 = (int)(float.Parse(row["控滑结构面倾角1"].ToString() == "" ? "0" : row["控滑结构面倾角1"].ToString()));
            disaster.控滑结构面类型2 = row["控滑结构面类型2"].ToString();
            disaster.控滑结构面倾向2 = (int)(float.Parse(row["控滑结构面倾向2"].ToString() == "" ? "0" : row["控滑结构面倾向2"].ToString()));
            disaster.控滑结构面倾角2 = (int)(float.Parse(row["控滑结构面倾角2"].ToString() == "" ? "0" : row["控滑结构面倾角2"].ToString()));
            disaster.控滑结构面类型3 = row["控滑结构面类型3"].ToString();
            disaster.控滑结构面倾向3 = (int)(float.Parse(row["控滑结构面倾向3"].ToString() == "" ? "0" : row["控滑结构面倾向3"].ToString()));
            disaster.控滑结构面倾角3 = (int)(float.Parse(row["控滑结构面倾角3"].ToString() == "" ? "0" : row["控滑结构面倾角3"].ToString()));
            disaster.滑坡长度 = float.Parse(row["滑坡长度"].ToString() == "" ? "0" : row["滑坡长度"].ToString());
            disaster.滑坡宽度 = float.Parse(row["滑坡宽度"].ToString() == "" ? "0" : row["滑坡宽度"].ToString());
            disaster.滑坡厚度 = float.Parse(row["滑坡厚度"].ToString() == "" ? "0" : row["滑坡厚度"].ToString());
            disaster.滑坡坡度 = float.Parse(row["滑坡坡度"].ToString() == "" ? "0" : row["滑坡坡度"].ToString());
            disaster.滑坡坡向 = float.Parse(row["滑坡坡向"].ToString() == "" ? "0" : row["滑坡坡向"].ToString());
            disaster.滑坡面积 = float.Parse(row["滑坡面积"].ToString() == "" ? "0" : row["滑坡面积"].ToString());
            disaster.滑坡体积 = float.Parse(row["滑坡体积"].ToString() == "" ? "0" : row["滑坡体积"].ToString());
            disaster.滑坡平面形态 = row["滑坡平面形态"].ToString();
            disaster.滑坡剖面形态 = row["滑坡剖面形态"].ToString();
            disaster.规模等级 = row["规模等级"].ToString();
            disaster.滑体岩性 = row["滑体岩性"].ToString();
            disaster.滑体结构 = row["滑体结构"].ToString();
            disaster.滑体碎石含量 = float.Parse(row["滑体碎石含量"].ToString() == "" ? "0" : row["滑体碎石含量"].ToString());
            disaster.滑体块度 = row["滑体块度"].ToString();
            disaster.滑床岩性 = row["滑床岩性"].ToString();
            disaster.滑床时代 = row["滑床时代"].ToString();
            disaster.滑床倾向 = (int)(float.Parse(row["滑床倾向"].ToString() == "" ? "0" : row["滑床倾向"].ToString()));
            disaster.滑床倾角 = (int)(float.Parse(row["滑床倾角"].ToString() == "" ? "0" : row["滑床倾角"].ToString()));
            disaster.滑面形态 = row["滑面形态"].ToString();
            disaster.滑面埋深 = float.Parse(row["滑面埋深"].ToString() == "" ? "0" : row["滑面埋深"].ToString());
            disaster.滑面倾向 = (int)(float.Parse(row["滑面倾向"].ToString() == "" ? "0" : row["滑面倾向"].ToString()));
            disaster.滑面倾角 = (int)(float.Parse(row["滑面倾角"].ToString() == "" ? "0" : row["滑面倾角"].ToString()));
            disaster.滑带厚度 = float.Parse(row["滑带厚度"].ToString() == "" ? "0" : row["滑带厚度"].ToString());
            disaster.滑带土名称 = row["滑带土名称"].ToString();
            disaster.滑带土性状 = row["滑带土性状"].ToString();
            disaster.地下水埋深 = float.Parse(row["地下水埋深"].ToString() == "" ? "0" : row["地下水埋深"].ToString());
            disaster.地下水露头 = row["地下水露头"].ToString();
            disaster.地下水补给类型 = row["地下水补给类型"].ToString();
            //disaster.土地使用 = row["土地使用"].ToString();
            disaster.变形迹象名称1 = row["变形迹象名称1"].ToString();
            disaster.变形迹象部位1 = row["变形迹象部位1"].ToString();
            disaster.变形迹象特征1 = row["变形迹象特征1"].ToString();
            //landslide.变形迹象初现时间1 = row["变形迹象初现时间年1"].ToString() + (row["变形迹象初现时间月1"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月1"].ToString() + "月") + (row["变形迹象初现时间日1"].ToString() == "" ? "" : row["变形迹象初现时间日1"].ToString() + "日");
            //disaster.变形迹象初现时间1 = row["变形迹象初现时间1"].ToString();
            disaster.变形迹象名称2 = row["变形迹象名称2"].ToString();
            disaster.变形迹象部位2 = row["变形迹象部位2"].ToString();
            disaster.变形迹象特征2 = row["变形迹象特征2"].ToString();
            //landslide.变形迹象初现时间2 = row["变形迹象初现时间年2"].ToString() + (row["变形迹象初现时间月2"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月2"].ToString() + "月") + (row["变形迹象初现时间日2"].ToString() == "" ? "" : row["变形迹象初现时间日2"].ToString() + "日");
            //disaster.变形迹象初现时间2 = row["变形迹象初现时间2"].ToString();
            disaster.变形迹象名称3 = row["变形迹象名称3"].ToString();
            disaster.变形迹象部位3 = row["变形迹象部位3"].ToString();
            disaster.变形迹象特征3 = row["变形迹象特征3"].ToString();
            //landslide.变形迹象初现时间3 = row["变形迹象初现时间年3"].ToString() + (row["变形迹象初现时间月3"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月3"].ToString() + "月") + (row["变形迹象初现时间日3"].ToString() == "" ? "" : row["变形迹象初现时间日3"].ToString() + "日");
            //disaster.变形迹象初现时间3 = row["变形迹象初现时间3"].ToString();
            disaster.变形迹象名称4 = row["变形迹象名称4"].ToString();
            disaster.变形迹象部位4 = row["变形迹象部位4"].ToString();
            disaster.变形迹象特征4 = row["变形迹象特征4"].ToString();
            //landslide.变形迹象初现时间4 = row["变形迹象初现时间年4"].ToString() + (row["变形迹象初现时间月4"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月4"].ToString() + "月") + (row["变形迹象初现时间日4"].ToString() == "" ? "" : row["变形迹象初现时间日4"].ToString() + "日");
            //disaster.变形迹象初现时间4 = row["变形迹象初现时间4"].ToString();
            disaster.变形迹象名称5 = row["变形迹象名称5"].ToString();
            disaster.变形迹象部位5 = row["变形迹象部位5"].ToString();
            disaster.变形迹象特征5 = row["变形迹象特征5"].ToString();
            //landslide.变形迹象初现时间5 = row["变形迹象初现时间年5"].ToString() + (row["变形迹象初现时间月5"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月5"].ToString() + "月") + (row["变形迹象初现时间日5"].ToString() == "" ? "" : row["变形迹象初现时间日5"].ToString() + "日");
            //disaster.变形迹象初现时间5 = row["变形迹象初现时间5"].ToString();
            disaster.变形迹象名称6 = row["变形迹象名称6"].ToString();
            disaster.变形迹象部位6 = row["变形迹象部位6"].ToString();
            disaster.变形迹象特征6 = row["变形迹象特征6"].ToString();
            //landslide.变形迹象初现时间6 = row["变形迹象初现时间年6"].ToString() + (row["变形迹象初现时间月6"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月6"].ToString() + "月") + (row["变形迹象初现时间日6"].ToString() == "" ? "" : row["变形迹象初现时间日6"].ToString() + "日");
            //disaster.变形迹象初现时间6 = row["变形迹象初现时间6"].ToString();
            disaster.变形迹象名称7 = row["变形迹象名称7"].ToString();
            disaster.变形迹象部位7 = row["变形迹象部位7"].ToString();
            disaster.变形迹象特征7 = row["变形迹象特征7"].ToString();
            //landslide.变形迹象初现时间7 = row["变形迹象初现时间年7"].ToString() + (row["变形迹象初现时间月7"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月7"].ToString() + "月") + (row["变形迹象初现时间日7"].ToString() == "" ? "" : row["变形迹象初现时间日7"].ToString() + "日");
            //disaster.变形迹象初现时间7 = row["变形迹象初现时间7"].ToString();
            disaster.变形迹象名称8 = row["变形迹象名称8"].ToString();
            disaster.变形迹象部位8 = row["变形迹象部位8"].ToString();
            disaster.变形迹象特征8 = row["变形迹象特征8"].ToString();
            //landslide.变形迹象初现时间8 = row["变形迹象初现时间年8"].ToString() + (row["变形迹象初现时间月8"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月8"].ToString() + "月") + (row["变形迹象初现时间日8"].ToString() == "" ? "" : row["变形迹象初现时间日8"].ToString() + "日");
            //disaster.变形迹象初现时间8 = row["变形迹象初现时间8"].ToString();
            disaster.地质因素 = row["地质因素"].ToString();
            disaster.地貌因素 = row["地貌因素"].ToString();
            disaster.物理因素 = row["物理因素"].ToString();
            disaster.人为因素 = row["人为因素"].ToString();
            disaster.主导因素 = row["主导因素"].ToString();
            disaster.复活诱发因素 = row["复活诱发因素"].ToString();
            disaster.目前稳定状态 = row["目前稳定状态"].ToString();
            disaster.今后变化趋势 = row["今后变化趋势"].ToString();
            disaster.隐患点 = bool.Parse(row["隐患点"].ToString() == "" ? "false" : row["隐患点"].ToString());
            //disaster.毁坏房屋 = float.Parse(row["毁坏房屋"].ToString() == "" ? "0" : row["毁坏房屋"].ToString());
            disaster.威胁住户 = float.Parse(row["威胁住户"].ToString() == "" ? "0" : row["威胁住户"].ToString());
            disaster.防灾预案 = bool.Parse(row["防灾预案"].ToString() == "" ? "false" : row["防灾预案"].ToString());
            disaster.多媒体 = bool.Parse(row["多媒体"].ToString() == "" ? "false" : row["多媒体"].ToString());
            disaster.监测建议 = row["监测建议"].ToString();
            disaster.防治建议 = row["防治建议"].ToString();
            disaster.群测人员 = row["群测人员"].ToString();
            disaster.村长 = row["村长"].ToString();
            disaster.电话 = row["电话"].ToString();
            disaster.调查负责人 = row["调查负责人"].ToString();
            disaster.填表人 = row["填表人"].ToString();
            disaster.审核人 = row["审核人"].ToString();
            disaster.调查单位 = row["调查单位"].ToString();
            //landslide.填表日期 = row["填表日期年"].ToString() + (row["填表日期月"].ToString() == "" ? "" : "年" + row["填表日期月"].ToString() + "月") + (row["填表日期日"].ToString() == "" ? "" : row["填表日期日"].ToString() + "日");
            //disaster.填表日期 = row["填表日期"].ToString();
            disaster.平面示意图 = ConvertHelper.GetBooleanByStr(row["平面示意图"].ToString());
            disaster.剖面示意图 = ConvertHelper.GetBooleanByStr(row["剖面示意图"].ToString());
            disaster.滑坡情况 = row["滑坡情况"].ToString();

            //新增字段
            disaster.项目名称 = row["项目名称"].ToString();
            disaster.图幅名 = row["图幅名"].ToString();
            disaster.图幅编号 = row["图幅编号"].ToString();
            disaster.县市编号 = row["县市编号"].ToString();
            disaster.滑坡时间年 = ConvertHelper.GetIntValueByStr(row["滑坡时间年"].ToString());
            disaster.滑坡时间月 = ConvertHelper.GetIntValueByStr(row["滑坡时间月"].ToString());
            disaster.滑坡时间日 = ConvertHelper.GetIntValueByStr(row["滑坡时间日"].ToString());
            disaster.滑坡时间时 = ConvertHelper.GetIntValueByStr(row["滑坡时间时"].ToString());
            disaster.滑坡时间分 = ConvertHelper.GetIntValueByStr(row["滑坡时间分"].ToString());
            disaster.滑坡时间秒 = ConvertHelper.GetIntValueByStr(row["滑坡时间秒"].ToString());
            disaster.滑坡性质 = row["滑坡性质"].ToString();
            disaster.省 = row["省"].ToString();
            disaster.市 = row["市"].ToString();
            disaster.县 = row["县"].ToString();
            disaster.乡 = row["乡"].ToString();
            disaster.村 = row["村"].ToString();
            disaster.组 = row["组"].ToString();
            disaster.地点 = row["地点"].ToString();
            disaster.控滑结构面类型4 = row["控滑结构面类型4"].ToString();
            disaster.控滑结构面倾向4 = ConvertHelper.GetIntValueByStr(row["控滑结构面倾向4"].ToString());
            disaster.控滑结构面倾角4 = ConvertHelper.GetIntValueByStr(row["控滑结构面倾角4"].ToString());
            disaster.土地利用 = row["土地利用"].ToString();
            disaster.变形迹象初现时间年1 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间年1"].ToString());
            disaster.变形迹象初现时间月1 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间月1"].ToString());
            disaster.变形迹象初现时间日1 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间日1"].ToString());
            disaster.变形迹象初现时间年2 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间年2"].ToString());
            disaster.变形迹象初现时间月2 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间月2"].ToString());
            disaster.变形迹象初现时间日2 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间日2"].ToString());
            disaster.变形迹象初现时间年3 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间年3"].ToString());
            disaster.变形迹象初现时间月3 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间月3"].ToString());
            disaster.变形迹象初现时间日3 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间日3"].ToString());
            disaster.变形迹象初现时间年4 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间年4"].ToString());
            disaster.变形迹象初现时间月4 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间月4"].ToString());
            disaster.变形迹象初现时间日4 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间日4"].ToString());
            disaster.变形迹象初现时间年5 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间年5"].ToString());
            disaster.变形迹象初现时间月5 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间月5"].ToString());
            disaster.变形迹象初现时间日5 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间日5"].ToString());
            disaster.变形迹象初现时间年6 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间年6"].ToString());
            disaster.变形迹象初现时间月6 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间月6"].ToString());
            disaster.变形迹象初现时间日6 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间日6"].ToString());
            disaster.变形迹象初现时间年7 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间年7"].ToString());
            disaster.变形迹象初现时间月7 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间月7"].ToString());
            disaster.变形迹象初现时间日7 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间日7"].ToString());
            disaster.变形迹象初现时间年8 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间年8"].ToString());
            disaster.变形迹象初现时间月8 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间月8"].ToString());
            disaster.变形迹象初现时间日8 = ConvertHelper.GetIntValueByStr(row["变形迹象初现时间日8"].ToString());
            disaster.变形活动阶段 = row["变形活动阶段"].ToString();
            disaster.自然诱因 = row["自然诱因"].ToString();
            disaster.毁坏房屋户 = ConvertHelper.GetIntValueByStr(row["毁坏房屋户"].ToString());
            disaster.毁坏房屋间 = ConvertHelper.GetIntValueByStr(row["毁坏房屋间"].ToString());
            disaster.死亡人数 = ConvertHelper.GetIntValueByStr(row["死亡人数"].ToString());
            disaster.毁路 = ConvertHelper.GetFloatValutFromStr(row["毁路"].ToString());
            disaster.毁渠 = ConvertHelper.GetFloatValutFromStr(row["毁渠"].ToString());
            disaster.其它危害 = row["其它危害"].ToString();
            disaster.间接损失 = ConvertHelper.GetDoubleValueFromStr(row["间接损失"].ToString());
            disaster.危害对象 = row["危害对象"].ToString();
            disaster.诱发灾害类型 = row["诱发灾害类型"].ToString();
            disaster.诱发灾害波及范围 = row["诱发灾害波及范围"].ToString();
            disaster.诱发灾害造成损失 = ConvertHelper.GetDoubleValueFromStr(row["诱发灾害造成损失"].ToString());
            disaster.威胁人数 = ConvertHelper.GetIntValueByStr(row["威胁人数"].ToString());
            disaster.威胁对象 = row["威胁对象"].ToString();
            disaster.防治监测 = row["防治监测"].ToString();
            disaster.防治治理 = row["防治治理"].ToString();
            disaster.搬迁避让 = row["搬迁避让"].ToString();
            disaster.群测群防 = row["群测群防"].ToString();
            disaster.遥感点 = ConvertHelper.GetBooleanByStr(row["遥感点"].ToString());
            disaster.勘查点 = ConvertHelper.GetBooleanByStr(row["勘查点"].ToString());
            disaster.测绘点 = ConvertHelper.GetBooleanByStr(row["测绘点"].ToString());
            disaster.录像 = ConvertHelper.GetBooleanByStr(row["录像"].ToString());
            disaster.栅格素描图 = ConvertHelper.GetBooleanByStr(row["栅格素描图"].ToString());
            disaster.矢量平面图 = ConvertHelper.GetBooleanByStr(row["矢量平面图"].ToString());
            disaster.矢量剖面图 = ConvertHelper.GetBooleanByStr(row["矢量剖面图"].ToString());
            disaster.矢量素描图 = ConvertHelper.GetBooleanByStr(row["矢量素描图"].ToString());
            disaster.野外调查记录 = row["野外调查记录"].ToString();
            disaster.填表日期年 = ConvertHelper.GetIntValueByStr(row["填表日期年"].ToString());
            disaster.填表日期月 = ConvertHelper.GetIntValueByStr(row["填表日期月"].ToString());
            disaster.填表日期日 = ConvertHelper.GetIntValueByStr(row["填表日期日"].ToString());
            disaster.威胁房屋户 = ConvertHelper.GetIntValueByStr(row["威胁房屋户"].ToString());
            #endregion

            return disaster;

        }

    }
}
