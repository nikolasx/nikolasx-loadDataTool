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
    /// <summary>
    /// 斜坡表数据录入
    /// </summary>
    public class LoadSlope
    {

        public virtual string InsertData(string filePath)
        {
            DataTable slopeTable = AccessHelper.GetDataTableByAccessFile(filePath, "斜坡主表");
            DataTable comTable = AccessHelper.GetDataTableByAccessFile(filePath, "综合表");
            LoadComprehensive loadCom = new LoadComprehensive();
            ComprehensiveService compService = new ComprehensiveService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < slopeTable.Rows.Count; i++)
            {
                try
                {
                    Slope slope = GetSlope(slopeTable.Rows[i], slopeTable.Columns);
                    string uId = slopeTable.Rows[i]["统一编号"].ToString();
                    DataRow[] rows = comTable.Select("统一编号=" + uId);
                    if (rows.Length > 1 || rows.Length < 1)
                        throw new Exception(@"不存在综合表或综合表不唯一");

                    Comprehensive comp = loadCom.GetComprehensive(rows[0], comTable.Columns);
                    comp.Slope = slope;
                    compService.InsertComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，统一编号：" + slopeTable.Rows[i]["统一编号"] + ";失败原因是：" + ex.Message + "\n");
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("斜坡表", slopeTable.Rows.Count, successCount, failCount, errorStr.ToString());
            return result;
        }



        public Slope GetSlope(DataRow row, DataColumnCollection columns)
        {
            Slope slope = new Slope();


            #region  斜坡
            slope.野外编号 = row["野外编号"].ToString();
            slope.室内编号 = row["室内编号"].ToString();
            slope.斜坡类型 = row["斜坡类型"].ToString();
            slope.坡顶标高 = double.Parse(row["坡顶标高"].ToString() == "" ? "0" : row["坡顶标高"].ToString());  //
            slope.坡脚标高 = double.Parse(row["坡脚标高"].ToString() == "" ? "0" : row["坡脚标高"].ToString());  //
            slope.地层时代 = row["地层时代"].ToString();
            slope.地层岩性 = row["地层岩性"].ToString();
            slope.地层倾向 = (int)(float.Parse(row["地层倾向"].ToString() == "" ? "0" : row["地层倾向"].ToString()));
            slope.地层倾角 = (int)(float.Parse(row["地层倾角"].ToString() == "" ? "0" : row["地层倾角"].ToString()));
            slope.构造部位 = row["构造部位"].ToString();
            slope.地震烈度 = row["地震烈度"].ToString();
            slope.微地貌 = row["微地貌"].ToString();
            slope.地下水类型 = row["地下水类型"].ToString();
            slope.年均降雨量 = double.Parse(row["年均降雨量"].ToString() == "" ? "0" : row["年均降雨量"].ToString());  //
            slope.日最大降雨 = double.Parse(row["日最大降雨"].ToString() == "" ? "0" : row["日最大降雨"].ToString());  //
            slope.时最大降雨 = double.Parse(row["时最大降雨"].ToString() == "" ? "0" : row["时最大降雨"].ToString());  //
            slope.洪水位 = double.Parse(row["洪水位"].ToString() == "" ? "0" : row["洪水位"].ToString());  //
            slope.枯水位 = double.Parse(row["枯水位"].ToString() == "" ? "0" : row["枯水位"].ToString());  //
            slope.相对河流位置 = row["相对河流位置"].ToString();
            slope.土地利用 = row["土地利用"].ToString();
            slope.最大坡高 = double.Parse(row["最大坡高"].ToString() == "" ? "0" : row["最大坡高"].ToString());  //
            slope.最大坡长 = double.Parse(row["最大坡长"].ToString() == "" ? "0" : row["最大坡长"].ToString());  //
            slope.最大坡宽 = double.Parse(row["最大坡宽"].ToString() == "" ? "0" : row["最大坡宽"].ToString());  //
            slope.平均坡度 = double.Parse(row["平均坡度"].ToString() == "" ? "0" : row["平均坡度"].ToString());  //
            slope.总体坡向 = double.Parse(row["总体坡向"].ToString() == "" ? "0" : row["总体坡向"].ToString());  //
            slope.坡面形态 = row["坡面形态"].ToString();
            slope.岩体结构类型 = row["岩体结构类型"].ToString();
            slope.岩体厚度 = double.Parse(row["岩体厚度"].ToString() == "" ? "0" : row["岩体厚度"].ToString());  //
            slope.岩体裂隙组数 = (int)(float.Parse(row["岩体裂隙组数"].ToString() == "" ? "0" : row["岩体裂隙组数"].ToString()));
            slope.岩体块度 = row["岩体块度"].ToString();
            slope.斜坡结构类型 = row["斜坡结构类型"].ToString();
            slope.控制面结构类型1 = row["控制面结构类型1"].ToString();
            slope.控制面结构倾向1 = (int)(float.Parse(row["控制面结构倾向1"].ToString() == "" ? "0" : row["控制面结构倾向1"].ToString()));
            slope.控制面结构倾角1 = (int)(float.Parse(row["控制面结构倾角1"].ToString() == "" ? "0" : row["控制面结构倾角1"].ToString()));
            slope.控制面结构长度1 = double.Parse(row["控制面结构长度1"].ToString() == "" ? "0" : row["控制面结构长度1"].ToString());  //
            slope.控制面结构间距1 = double.Parse(row["控制面结构间距1"].ToString() == "" ? "0" : row["控制面结构间距1"].ToString());  //
            slope.控制面结构类型2 = row["控制面结构类型2"].ToString();
            slope.控制面结构倾向2 = (int)(float.Parse(row["控制面结构倾向2"].ToString() == "" ? "0" : row["控制面结构倾向2"].ToString()));
            slope.控制面结构倾角2 = (int)(float.Parse(row["控制面结构倾角2"].ToString() == "" ? "0" : row["控制面结构倾角2"].ToString()));
            slope.控制面结构长度2 = double.Parse(row["控制面结构长度2"].ToString() == "" ? "0" : row["控制面结构长度2"].ToString());  //
            slope.控制面结构间距2 = double.Parse(row["控制面结构间距2"].ToString() == "" ? "0" : row["控制面结构间距2"].ToString());  //
            slope.控制面结构类型3 = row["控制面结构类型3"].ToString();
            slope.控制面结构倾向3 = (int)(float.Parse(row["控制面结构倾向3"].ToString() == "" ? "0" : row["控制面结构倾向3"].ToString()));
            slope.控制面结构倾角3 = (int)(float.Parse(row["控制面结构倾角3"].ToString() == "" ? "0" : row["控制面结构倾角3"].ToString()));
            slope.控制面结构长度3 = double.Parse(row["控制面结构长度3"].ToString() == "" ? "0" : row["控制面结构长度3"].ToString());  //
            slope.控制面结构间距3 = double.Parse(row["控制面结构间距3"].ToString() == "" ? "0" : row["控制面结构间距3"].ToString());  //
            slope.全风化带深度 = double.Parse(row["全风化带深度"].ToString() == "" ? "0" : row["全风化带深度"].ToString());  //
            slope.卸荷裂隙深度 = double.Parse(row["卸荷裂隙深度"].ToString() == "" ? "0" : row["卸荷裂隙深度"].ToString());  //
            slope.土体名称 = row["土体名称"].ToString();
            slope.土体密实度 = row["土体密实度"].ToString();
            slope.土体稠度 = row["土体稠度"].ToString();
            slope.下伏基岩岩性 = row["下伏基岩岩性"].ToString();
            slope.下伏基岩时代 = row["下伏基岩时代"].ToString();
            slope.下伏基岩倾向 = (int)(float.Parse(row["下伏基岩倾向"].ToString() == "" ? "0" : row["下伏基岩倾向"].ToString()));
            slope.下伏基岩倾角 = (int)(float.Parse(row["下伏基岩倾角"].ToString() == "" ? "0" : row["下伏基岩倾角"].ToString()));
            slope.下伏基岩埋深 = double.Parse(row["下伏基岩埋深"].ToString() == "" ? "0" : row["下伏基岩埋深"].ToString());  //
            slope.地下水埋深 = double.Parse(row["地下水埋深"].ToString() == "" ? "0" : row["地下水埋深"].ToString());  //
            slope.地下水露头 = row["地下水露头"].ToString();
            slope.地下水补给类型 = row["地下水补给类型"].ToString();
            slope.变形迹象名称1 = row["变形迹象名称1"].ToString();
            slope.变形迹象部位1 = row["变形迹象部位1"].ToString();
            slope.变形迹象特征1 = row["变形迹象特征1"].ToString();
            slope.变形迹象初现时间1 = row["变形迹象初现时间1"].ToString();
            slope.变形迹象名称2 = row["变形迹象名称2"].ToString();
            slope.变形迹象部位2 = row["变形迹象部位2"].ToString();
            slope.变形迹象特征2 = row["变形迹象特征2"].ToString();
            slope.变形迹象初现时间2 = row["变形迹象初现时间2"].ToString();
            slope.变形迹象名称3 = row["变形迹象名称3"].ToString();
            slope.变形迹象部位3 = row["变形迹象部位3"].ToString();
            slope.变形迹象特征3 = row["变形迹象特征3"].ToString();
            slope.变形迹象初现时间3 = row["变形迹象初现时间3"].ToString();
            slope.变形迹象名称4 = row["变形迹象名称4"].ToString();
            slope.变形迹象部位4 = row["变形迹象部位4"].ToString();
            slope.变形迹象特征4 = row["变形迹象特征4"].ToString();
            slope.变形迹象初现时间4 = row["变形迹象初现时间4"].ToString();
            slope.变形迹象名称5 = row["变形迹象名称5"].ToString();
            slope.变形迹象部位5 = row["变形迹象部位5"].ToString();
            slope.变形迹象特征5 = row["变形迹象特征5"].ToString();
            slope.变形迹象初现时间5 = row["变形迹象初现时间5"].ToString();
            slope.变形迹象名称6 = row["变形迹象名称6"].ToString();
            slope.变形迹象部位6 = row["变形迹象部位6"].ToString();
            slope.变形迹象特征6 = row["变形迹象特征6"].ToString();
            slope.变形迹象初现时间6 = row["变形迹象初现时间6"].ToString();
            slope.变形迹象名称7 = row["变形迹象名称7"].ToString();
            slope.变形迹象部位7 = row["变形迹象部位7"].ToString();
            slope.变形迹象特征7 = row["变形迹象特征7"].ToString();
            slope.变形迹象初现时间7 = row["变形迹象初现时间7"].ToString();
            slope.变形迹象名称8 = row["变形迹象名称8"].ToString();
            slope.变形迹象部位8 = row["变形迹象部位8"].ToString();
            slope.变形迹象特征8 = row["变形迹象特征8"].ToString();
            slope.变形迹象初现时间8 = row["变形迹象初现时间8"].ToString();
            slope.可能失稳因素 = row["可能失稳因素"].ToString();
            slope.毁坏房屋 = double.Parse(row["毁坏房屋"].ToString() == "" ? "0" : row["毁坏房屋"].ToString());  //
            slope.毁路 = double.Parse(row["毁路"].ToString() == "" ? "0" : row["毁路"].ToString());  //
            slope.毁渠 = double.Parse(row["毁渠"].ToString() == "" ? "0" : row["毁渠"].ToString());  //
            slope.其它危害 = row["其它危害"].ToString();
            slope.监测建议 = row["监测建议"].ToString();
            slope.防治建议 = row["防治建议"].ToString();
            slope.群测人员 = row["群测人员"].ToString();
            slope.村长 = row["村长"].ToString();
            slope.电话 = row["电话"].ToString();
            slope.隐患点 = bool.Parse(row["隐患点"].ToString() == "" ? "false" : row["隐患点"].ToString());
            slope.防灾预案 = bool.Parse(row["防灾预案"].ToString() == "" ? "false" : row["防灾预案"].ToString());
            slope.多媒体 = bool.Parse(row["多媒体"].ToString() == "" ? "false" : row["多媒体"].ToString());
            slope.调查负责人 = row["调查负责人"].ToString();
            slope.填表人 = row["填表人"].ToString();
            slope.审核人 = row["审核人"].ToString();
            slope.调查单位 = row["调查单位"].ToString();
            slope.填表日期 = row["填表日期"].ToString();
            slope.平面示意图 = (byte[])(row["平面示意图"].ToString() == "" ? null : row["平面示意图"]);
            slope.剖面示意图 = (byte[])(row["剖面示意图"].ToString() == "" ? null : row["剖面示意图"]);
            //slope.省名 = row["省名"].ToString() ;
            //slope.县名 = row["县名"].ToString() ;
            //slope.街道 = row["街道"].ToString() ;
            //slope.平面示意图路径 = row["平面示意图路径"].ToString() ;
            //slope.剖面示意图路径 = row["剖面示意图路径"].ToString();
            #endregion

            return slope;
        }


    }
}
