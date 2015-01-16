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
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.FiveScale.Investigation
{

    /// <summary>
    /// 斜坡
    /// </summary>
    public class LoadSlopeFS
    {


        public virtual string InsertData(string filePath)
        {
            DataTable landTable = AccessHelper.GetDataTableByAccessFile(filePath, "不稳定斜坡主表");
            DataTable comTable = AccessHelper.GetDataTableByAccessFile(filePath, "汇总表");
            LoadComprehensiveFS loadCom = new LoadComprehensiveFS();
            ComprehensiveFSService compService = new ComprehensiveFSService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < landTable.Rows.Count; i++)
            {
                try
                {
                    SlopeFS landSlip = GetSlopeFS(landTable.Rows[i], landTable.Columns);
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
                    comp.SlopeFS = landSlip;
                    compService.InsertComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，统一编号是：" + landTable.Rows[i]["统一编号"] + ";失败原因是：" + ex.Message + "\n");
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("斜坡表(5W)", landTable.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }

        public SlopeFS GetSlopeFS(DataRow row, DataColumnCollection columns)
        {
            SlopeFS disaster = new SlopeFS();


            #region  斜坡
            disaster.野外编号 = row["野外编号"].ToString();
            disaster.室内编号 = row["室内编号"].ToString();
            disaster.斜坡类型 = row["斜坡类型"].ToString();
            disaster.坡顶标高 = float.Parse(row["坡顶标高"].ToString() == "" ? "0" : row["坡顶标高"].ToString());  //
            disaster.坡脚标高 = float.Parse(row["坡脚标高"].ToString() == "" ? "0" : row["坡脚标高"].ToString());  //
            disaster.地层时代 = row["地层时代"].ToString();
            disaster.地层岩性 = row["地层岩性"].ToString();
            disaster.地层倾向 = (int)(float.Parse(row["地层倾向"].ToString() == "" ? "0" : row["地层倾向"].ToString()));
            disaster.地层倾角 = (int)(float.Parse(row["地层倾角"].ToString() == "" ? "0" : row["地层倾角"].ToString()));
            disaster.构造部位 = row["构造部位"].ToString();
            disaster.地震烈度 = row["地震烈度"].ToString();
            disaster.微地貌 = row["微地貌"].ToString();
            disaster.地下水类型 = row["地下水类型"].ToString();
            disaster.年均降雨量 = float.Parse(row["年均降雨量"].ToString() == "" ? "0" : row["年均降雨量"].ToString());  //
            disaster.日最大降雨 = float.Parse(row["日最大降雨"].ToString() == "" ? "0" : row["日最大降雨"].ToString());  //
            disaster.时最大降雨 = float.Parse(row["时最大降雨"].ToString() == "" ? "0" : row["时最大降雨"].ToString());  //
            disaster.洪水位 = float.Parse(row["洪水位"].ToString() == "" ? "0" : row["洪水位"].ToString());  //
            disaster.枯水位 = float.Parse(row["枯水位"].ToString() == "" ? "0" : row["枯水位"].ToString());  //
            disaster.相对河流位置 = row["相对河流位置"].ToString();
            disaster.土地利用 = row["土地利用"].ToString();
            disaster.最大坡高 = float.Parse(row["最大坡高"].ToString() == "" ? "0" : row["最大坡高"].ToString());  //
            disaster.最大坡长 = float.Parse(row["最大坡长"].ToString() == "" ? "0" : row["最大坡长"].ToString());  //
            disaster.最大坡宽 = float.Parse(row["最大坡宽"].ToString() == "" ? "0" : row["最大坡宽"].ToString());  //
            disaster.平均坡度 = float.Parse(row["平均坡度"].ToString() == "" ? "0" : row["平均坡度"].ToString());  //
            disaster.总体坡向 = float.Parse(row["总体坡向"].ToString() == "" ? "0" : row["总体坡向"].ToString());  //
            disaster.坡面形态 = row["坡面形态"].ToString();
            disaster.岩体结构类型 = row["岩体结构类型"].ToString();
            disaster.岩体厚度 = float.Parse(row["岩体厚度"].ToString() == "" ? "0" : row["岩体厚度"].ToString());  //
            disaster.岩体裂隙组数 = (int)(float.Parse(row["岩体裂隙组数"].ToString() == "" ? "0" : row["岩体裂隙组数"].ToString()));
            disaster.岩体块度 = row["岩体块度"].ToString();
            disaster.斜坡结构类型 = row["斜坡结构类型"].ToString();
            disaster.控制面结构类型1 = row["控制面结构类型1"].ToString();
            disaster.控制面结构倾向1 = (int)(float.Parse(row["控制面结构倾向1"].ToString() == "" ? "0" : row["控制面结构倾向1"].ToString()));
            disaster.控制面结构倾角1 = (int)(float.Parse(row["控制面结构倾角1"].ToString() == "" ? "0" : row["控制面结构倾角1"].ToString()));
            disaster.控制面结构长度1 = float.Parse(row["控制面结构长度1"].ToString() == "" ? "0" : row["控制面结构长度1"].ToString());  //
            disaster.控制面结构间距1 = float.Parse(row["控制面结构间距1"].ToString() == "" ? "0" : row["控制面结构间距1"].ToString());  //
            disaster.控制面结构类型2 = row["控制面结构类型2"].ToString();
            disaster.控制面结构倾向2 = (int)(float.Parse(row["控制面结构倾向2"].ToString() == "" ? "0" : row["控制面结构倾向2"].ToString()));
            disaster.控制面结构倾角2 = (int)(float.Parse(row["控制面结构倾角2"].ToString() == "" ? "0" : row["控制面结构倾角2"].ToString()));
            disaster.控制面结构长度2 = float.Parse(row["控制面结构长度2"].ToString() == "" ? "0" : row["控制面结构长度2"].ToString());  //
            disaster.控制面结构间距2 = float.Parse(row["控制面结构间距2"].ToString() == "" ? "0" : row["控制面结构间距2"].ToString());  //
            disaster.控制面结构类型3 = row["控制面结构类型3"].ToString();
            disaster.控制面结构倾向3 = (int)(float.Parse(row["控制面结构倾向3"].ToString() == "" ? "0" : row["控制面结构倾向3"].ToString()));
            disaster.控制面结构倾角3 = (int)(float.Parse(row["控制面结构倾角3"].ToString() == "" ? "0" : row["控制面结构倾角3"].ToString()));
            disaster.控制面结构长度3 = float.Parse(row["控制面结构长度3"].ToString() == "" ? "0" : row["控制面结构长度3"].ToString());  //
            disaster.控制面结构间距3 = float.Parse(row["控制面结构间距3"].ToString() == "" ? "0" : row["控制面结构间距3"].ToString());  //
            disaster.全风化带深度 = float.Parse(row["全风化带深度"].ToString() == "" ? "0" : row["全风化带深度"].ToString());  //
            disaster.卸荷裂隙深度 = float.Parse(row["卸荷裂隙深度"].ToString() == "" ? "0" : row["卸荷裂隙深度"].ToString());  //
            disaster.土体名称 = row["土体名称"].ToString();
            disaster.土体密实度 = row["土体密实度"].ToString();
            disaster.土体稠度 = row["土体稠度"].ToString();
            disaster.下伏基岩岩性 = row["下伏基岩岩性"].ToString();
            disaster.下伏基岩时代 = row["下伏基岩时代"].ToString();
            disaster.下伏基岩倾向 = (int)(float.Parse(row["下伏基岩倾向"].ToString() == "" ? "0" : row["下伏基岩倾向"].ToString()));
            disaster.下伏基岩倾角 = (int)(float.Parse(row["下伏基岩倾角"].ToString() == "" ? "0" : row["下伏基岩倾角"].ToString()));
            disaster.下伏基岩埋深 = float.Parse(row["下伏基岩埋深"].ToString() == "" ? "0" : row["下伏基岩埋深"].ToString());  //
            disaster.地下水埋深 = float.Parse(row["地下水埋深"].ToString() == "" ? "0" : row["地下水埋深"].ToString());  //
            disaster.地下水露头 = row["地下水露头"].ToString();
            disaster.地下水补给类型 = row["地下水补给类型"].ToString();
            disaster.变形迹象名称1 = row["变形迹象名称1"].ToString();
            disaster.变形迹象部位1 = row["变形迹象部位1"].ToString();
            disaster.变形迹象特征1 = row["变形迹象特征1"].ToString();
            //disaster.变形迹象初现时间1 = row["变形迹象初现时间1"].ToString();
            disaster.变形迹象名称2 = row["变形迹象名称2"].ToString();
            disaster.变形迹象部位2 = row["变形迹象部位2"].ToString();
            disaster.变形迹象特征2 = row["变形迹象特征2"].ToString();
            //disaster.变形迹象初现时间2 = row["变形迹象初现时间2"].ToString();
            disaster.变形迹象名称3 = row["变形迹象名称3"].ToString();
            disaster.变形迹象部位3 = row["变形迹象部位3"].ToString();
            disaster.变形迹象特征3 = row["变形迹象特征3"].ToString();
            //disaster.变形迹象初现时间3 = row["变形迹象初现时间3"].ToString();
            disaster.变形迹象名称4 = row["变形迹象名称4"].ToString();
            disaster.变形迹象部位4 = row["变形迹象部位4"].ToString();
            disaster.变形迹象特征4 = row["变形迹象特征4"].ToString();
            //disaster.变形迹象初现时间4 = row["变形迹象初现时间4"].ToString();
            disaster.变形迹象名称5 = row["变形迹象名称5"].ToString();
            disaster.变形迹象部位5 = row["变形迹象部位5"].ToString();
            disaster.变形迹象特征5 = row["变形迹象特征5"].ToString();
            //disaster.变形迹象初现时间5 = row["变形迹象初现时间5"].ToString();
            disaster.变形迹象名称6 = row["变形迹象名称6"].ToString();
            disaster.变形迹象部位6 = row["变形迹象部位6"].ToString();
            disaster.变形迹象特征6 = row["变形迹象特征6"].ToString();
            //disaster.变形迹象初现时间6 = row["变形迹象初现时间6"].ToString();
            disaster.变形迹象名称7 = row["变形迹象名称7"].ToString();
            disaster.变形迹象部位7 = row["变形迹象部位7"].ToString();
            disaster.变形迹象特征7 = row["变形迹象特征7"].ToString();
            //disaster.变形迹象初现时间7 = row["变形迹象初现时间7"].ToString();
            disaster.变形迹象名称8 = row["变形迹象名称8"].ToString();
            disaster.变形迹象部位8 = row["变形迹象部位8"].ToString();
            disaster.变形迹象特征8 = row["变形迹象特征8"].ToString();
            //disaster.变形迹象初现时间8 = row["变形迹象初现时间8"].ToString();
            disaster.可能失稳因素 = row["可能失稳因素"].ToString();
            //disaster.毁坏房屋 = double.Parse(row["毁坏房屋"].ToString() == "" ? "0" : row["毁坏房屋"].ToString());  //
            disaster.毁路 = float.Parse(row["毁路"].ToString() == "" ? "0" : row["毁路"].ToString());  //
            disaster.毁渠 = float.Parse(row["毁渠"].ToString() == "" ? "0" : row["毁渠"].ToString());  //
            disaster.其它危害 = row["其它危害"].ToString();
            disaster.监测建议 = row["监测建议"].ToString();
            disaster.防治建议 = row["防治建议"].ToString();
            disaster.群测人员 = row["群测人员"].ToString();
            disaster.村长 = row["村长"].ToString();
            disaster.电话 = row["电话"].ToString();
            disaster.隐患点 = bool.Parse(row["隐患点"].ToString() == "" ? "false" : row["隐患点"].ToString());
            disaster.防灾预案 = bool.Parse(row["防灾预案"].ToString() == "" ? "false" : row["防灾预案"].ToString());
            disaster.多媒体 = bool.Parse(row["多媒体"].ToString() == "" ? "false" : row["多媒体"].ToString());
            disaster.调查负责人 = row["调查负责人"].ToString();
            disaster.填表人 = row["填表人"].ToString();
            disaster.审核人 = row["审核人"].ToString();
            disaster.调查单位 = row["调查单位"].ToString();
            //disaster.填表日期 = row["填表日期"].ToString();
            disaster.平面示意图 = ConvertHelper.GetBooleanByStr(row["平面示意图"].ToString()) ;
            disaster.剖面示意图 = ConvertHelper.GetBooleanByStr(row["剖面示意图"].ToString());
            //slope.省名 = row["省名"].ToString() ;
            //slope.县名 = row["县名"].ToString() ;
            //slope.街道 = row["街道"].ToString() ;
            //slope.平面示意图路径 = row["平面示意图路径"].ToString() ;
            //slope.剖面示意图路径 = row["剖面示意图路径"].ToString();

            //新增字段
            disaster.项目名称 = row["项目名称"].ToString();
            disaster.图幅名 = row["图幅名"].ToString();
            disaster.图幅编号 = row["图幅编号"].ToString();
            disaster.县市编号 = row["县市编号"].ToString();
            disaster.斜坡变形趋势 = row["斜坡变形趋势"].ToString();
            disaster.省 = row["省"].ToString();
            disaster.市 = row["市"].ToString();
            disaster.县 = row["县"].ToString();
            disaster.乡 = row["乡"].ToString();
            disaster.村 = row["村"].ToString();
            disaster.组 = row["组"].ToString();
            disaster.地点 = row["地点"].ToString();
            disaster.最大厚度 = ConvertHelper.GetFloatValutFromStr(row["最大厚度"].ToString());
            disaster.预测体积 = ConvertHelper.GetDoubleValueFromStr(row["预测体积"].ToString());
            disaster.预测规模等级 = row["预测规模等级"].ToString();
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
            disaster.毁坏房屋户 = ConvertHelper.GetIntValueByStr(row["毁坏房屋户"].ToString());
            disaster.毁坏房屋间 = ConvertHelper.GetIntValueByStr(row["毁坏房屋间"].ToString());
            disaster.威胁对象 = row["威胁对象"].ToString();
            disaster.防治监测 = row["防治监测"].ToString();
            disaster.防治治理 = row["防治治理"].ToString();
            disaster.群测群防 = row["群测群防"].ToString();
            disaster.搬迁避让 = row["搬迁避让"].ToString();
            disaster.遥感点 = ConvertHelper.GetBooleanByStr(row["遥感点"].ToString());
            disaster.勘查点 = ConvertHelper.GetBooleanByStr(row["勘查点"].ToString());
            disaster.测绘点 = ConvertHelper.GetBooleanByStr(row["测绘点"].ToString());
            disaster.填表日期年 = ConvertHelper.GetIntValueByStr(row["填表日期年"].ToString());
            disaster.填表日期月 = ConvertHelper.GetIntValueByStr(row["填表日期月"].ToString());
            disaster.填表日期日 = ConvertHelper.GetIntValueByStr(row["填表日期日"].ToString());
            disaster.野外记录信息 = row["野外记录信息"].ToString();
            disaster.矢量平面图 = ConvertHelper.GetBooleanByStr(row["矢量平面图"].ToString());
            disaster.矢量剖面图 = ConvertHelper.GetBooleanByStr(row["矢量剖面图"].ToString());
            disaster.录像 = ConvertHelper.GetBooleanByStr(row["录像"].ToString());
            disaster.威胁房屋户 = ConvertHelper.GetIntValueByStr(row["威胁房屋户"].ToString());


            #endregion

            return disaster;
        }
    }
}
