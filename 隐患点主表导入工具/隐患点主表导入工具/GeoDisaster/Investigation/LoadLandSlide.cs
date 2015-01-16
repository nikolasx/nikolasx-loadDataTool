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
    /// 滑坡主表信息录入
    /// </summary>
    public class LoadLandSlide
    {


        /// <summary>
        /// 插入滑坡主表信息
        /// </summary>
        /// <param name="landFilePath"></param>
        /// <param name="comFilePath"></param>
        /// <returns></returns>
        public virtual string InsertData(string filePath)
        {
            DataTable landTable = AccessHelper.GetDataTableByAccessFile(filePath, "滑坡主表");
            DataTable comTable = AccessHelper.GetDataTableByAccessFile(filePath, "综合表");
            LoadComprehensive loadCom = new LoadComprehensive();
            ComprehensiveService compService = new ComprehensiveService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < landTable.Rows.Count; i++)
            {
                try
                {
                    LandSlide landSlide = GetLandSlide(landTable.Rows[i], landTable.Columns);
                    string uId = landTable.Rows[i]["统一编号"].ToString();
                    DataRow[] rows = comTable.Select("统一编号=" + uId);
                    if (rows.Length > 1 || rows.Length < 1)
                        throw new Exception(@"不存在综合表或综合表不唯一");

                    Comprehensive comp = loadCom.GetComprehensive(rows[0], comTable.Columns);
                    comp.LandSlide = landSlide;
                    compService.InsertComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，统一编号:" + landTable.Rows[i]["统一编号"] + ";失败原因是：" + ex.Message + "\n");
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("滑坡表", landTable.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }


        public LandSlide GetLandSlide(DataRow row,DataColumnCollection columns)
        {
            LandSlide landslide = new LandSlide();

            #region LandSlide
            landslide.野外编号 = row["野外编号"].ToString();
            landslide.室内编号 = row["室内编号"].ToString();
            landslide.滑坡年代 = row["滑坡年代"].ToString();
            //landslide.滑坡时间 = row["滑坡时间年"].ToString() + (row["滑坡时间月"].ToString() == "" ? "" : "年" + row["滑坡时间月"].ToString() + "月") + (row["滑坡时间日"].ToString() == "" ? "" : row["滑坡时间日"].ToString() + "日");
            landslide.滑坡时间 = row["滑坡时间"].ToString();
            landslide.滑坡类型 = row["滑坡类型"].ToString();
            landslide.滑体性质 = row["滑体性质"].ToString();   //
            landslide.冠 = float.Parse(row["冠"].ToString() == "" ? "0" : row["冠"].ToString());
            landslide.趾 = float.Parse(row["趾"].ToString() == "" ? "0" : row["趾"].ToString());
            landslide.地层时代 = row["地层时代"].ToString();
            landslide.地层岩性 = row["地层岩性"].ToString();
            landslide.构造部位 = row["构造部位"].ToString();
            landslide.地震烈度 = row["地震烈度"].ToString();
            landslide.地层倾向 = (int)(float.Parse(row["地层倾向"].ToString() == "" ? "0" : row["地层倾向"].ToString()));
            landslide.地层倾角 = (int)(float.Parse(row["地层倾角"].ToString() == "" ? "0" : row["地层倾角"].ToString()));
            landslide.微地貌 = row["微地貌"].ToString();
            landslide.地下水类型 = row["地下水类型"].ToString();
            landslide.年均降雨量 = float.Parse(row["年均降雨量"].ToString() == "" ? "0" : row["年均降雨量"].ToString());
            landslide.日最大降雨量 = float.Parse(row["日最大降雨量"].ToString() == "" ? "0" : row["日最大降雨量"].ToString());
            landslide.时最大降雨量 = float.Parse(row["时最大降雨量"].ToString() == "" ? "0" : row["时最大降雨量"].ToString());
            landslide.洪水位 = float.Parse(row["洪水位"].ToString() == "" ? "0" : row["洪水位"].ToString());
            landslide.枯水位 = float.Parse(row["枯水位"].ToString() == "" ? "0" : row["枯水位"].ToString());
            landslide.相对河流位置 = row["相对河流位置"].ToString();
            landslide.原始坡高 = float.Parse(row["原始坡高"].ToString() == "" ? "0" : row["原始坡高"].ToString());
            landslide.原始坡度 = float.Parse(row["原始坡度"].ToString() == "" ? "0" : row["原始坡度"].ToString());
            landslide.原始坡形 = row["原始坡形"].ToString();
            landslide.斜坡结构类型 = row["斜坡结构类型"].ToString();
            landslide.控滑结构面类型1 = row["控滑结构面类型1"].ToString();
            landslide.控滑结构面倾向1 = (int)(float.Parse(row["控滑结构面倾向1"].ToString() == "" ? "0" : row["控滑结构面倾向1"].ToString()));
            landslide.控滑结构面倾角1 = (int)(float.Parse(row["控滑结构面倾角1"].ToString() == "" ? "0" : row["控滑结构面倾角1"].ToString()));
            landslide.控滑结构面类型2 = row["控滑结构面类型2"].ToString();
            landslide.控滑结构面倾向2 = (int)(float.Parse(row["控滑结构面倾向2"].ToString() == "" ? "0" : row["控滑结构面倾向2"].ToString()));
            landslide.控滑结构面倾角2 = (int)(float.Parse(row["控滑结构面倾角2"].ToString() == "" ? "0" : row["控滑结构面倾角2"].ToString()));
            landslide.控滑结构面类型3 = row["控滑结构面类型3"].ToString();
            landslide.控滑结构面倾向3 = (int)(float.Parse(row["控滑结构面倾向3"].ToString() == "" ? "0" : row["控滑结构面倾向3"].ToString()));
            landslide.控滑结构面倾角3 = (int)(float.Parse(row["控滑结构面倾角3"].ToString() == "" ? "0" : row["控滑结构面倾角3"].ToString()));
            landslide.滑坡长度 = float.Parse(row["滑坡长度"].ToString() == "" ? "0" : row["滑坡长度"].ToString());
            landslide.滑坡宽度 = float.Parse(row["滑坡宽度"].ToString() == "" ? "0" : row["滑坡宽度"].ToString());
            landslide.滑坡厚度 = float.Parse(row["滑坡厚度"].ToString() == "" ? "0" : row["滑坡厚度"].ToString());
            landslide.滑坡坡度 = float.Parse(row["滑坡坡度"].ToString() == "" ? "0" : row["滑坡坡度"].ToString());
            landslide.滑坡坡向 = float.Parse(row["滑坡坡向"].ToString() == "" ? "0" : row["滑坡坡向"].ToString());
            landslide.滑坡面积 = float.Parse(row["滑坡面积"].ToString() == "" ? "0" : row["滑坡面积"].ToString());
            landslide.滑坡体积 = float.Parse(row["滑坡体积"].ToString() == "" ? "0" : row["滑坡体积"].ToString());
            landslide.滑坡平面形态 = row["滑坡平面形态"].ToString();
            landslide.滑坡剖面形态 = row["滑坡剖面形态"].ToString();
            landslide.规模等级 = row["规模等级"].ToString();
            landslide.滑体岩性 = row["滑体岩性"].ToString();
            landslide.滑体结构 = row["滑体结构"].ToString();
            landslide.滑体碎石含量 = float.Parse(row["滑体碎石含量"].ToString() == "" ? "0" : row["滑体碎石含量"].ToString());
            landslide.滑体块度 = row["滑体块度"].ToString();
            landslide.滑床岩性 = row["滑床岩性"].ToString();
            landslide.滑床时代 = row["滑床时代"].ToString();
            landslide.滑床倾向 = (int)(float.Parse(row["滑床倾向"].ToString() == "" ? "0" : row["滑床倾向"].ToString()));
            landslide.滑床倾角 = (int)(float.Parse(row["滑床倾角"].ToString() == "" ? "0" : row["滑床倾角"].ToString()));
            landslide.滑面形态 = row["滑面形态"].ToString();
            landslide.滑面埋深 = float.Parse(row["滑面埋深"].ToString() == "" ? "0" : row["滑面埋深"].ToString());
            landslide.滑面倾向 = (int)(float.Parse(row["滑面倾向"].ToString() == "" ? "0" : row["滑面倾向"].ToString()));
            landslide.滑面倾角 = (int)(float.Parse(row["滑面倾角"].ToString() == "" ? "0" : row["滑面倾角"].ToString()));
            landslide.滑带厚度 = float.Parse(row["滑带厚度"].ToString() == "" ? "0" : row["滑带厚度"].ToString());
            landslide.滑带土名称 = row["滑带土名称"].ToString();
            landslide.滑带土性状 = row["滑带土性状"].ToString();
            landslide.地下水埋深 = float.Parse(row["地下水埋深"].ToString() == "" ? "0" : row["地下水埋深"].ToString());
            landslide.地下水露头 = row["地下水露头"].ToString();
            landslide.地下水补给类型 = row["地下水补给类型"].ToString();
            landslide.土地使用 = row["土地使用"].ToString();
            landslide.变形迹象名称1 = row["变形迹象名称1"].ToString();
            landslide.变形迹象部位1 = row["变形迹象部位1"].ToString();
            landslide.变形迹象特征1 = row["变形迹象特征1"].ToString();
            //landslide.变形迹象初现时间1 = row["变形迹象初现时间年1"].ToString() + (row["变形迹象初现时间月1"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月1"].ToString() + "月") + (row["变形迹象初现时间日1"].ToString() == "" ? "" : row["变形迹象初现时间日1"].ToString() + "日");
            landslide.变形迹象初现时间1 = row["变形迹象初现时间1"].ToString();
            landslide.变形迹象名称2 = row["变形迹象名称2"].ToString();
            landslide.变形迹象部位2 = row["变形迹象部位2"].ToString();
            landslide.变形迹象特征2 = row["变形迹象特征2"].ToString();
            //landslide.变形迹象初现时间2 = row["变形迹象初现时间年2"].ToString() + (row["变形迹象初现时间月2"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月2"].ToString() + "月") + (row["变形迹象初现时间日2"].ToString() == "" ? "" : row["变形迹象初现时间日2"].ToString() + "日");
            landslide.变形迹象初现时间2 = row["变形迹象初现时间2"].ToString();
            landslide.变形迹象名称3 = row["变形迹象名称3"].ToString();
            landslide.变形迹象部位3 = row["变形迹象部位3"].ToString();
            landslide.变形迹象特征3 = row["变形迹象特征3"].ToString();
            //landslide.变形迹象初现时间3 = row["变形迹象初现时间年3"].ToString() + (row["变形迹象初现时间月3"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月3"].ToString() + "月") + (row["变形迹象初现时间日3"].ToString() == "" ? "" : row["变形迹象初现时间日3"].ToString() + "日");
            landslide.变形迹象初现时间3 = row["变形迹象初现时间3"].ToString();
            landslide.变形迹象名称4 = row["变形迹象名称4"].ToString();
            landslide.变形迹象部位4 = row["变形迹象部位4"].ToString();
            landslide.变形迹象特征4 = row["变形迹象特征4"].ToString();
            //landslide.变形迹象初现时间4 = row["变形迹象初现时间年4"].ToString() + (row["变形迹象初现时间月4"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月4"].ToString() + "月") + (row["变形迹象初现时间日4"].ToString() == "" ? "" : row["变形迹象初现时间日4"].ToString() + "日");
            landslide.变形迹象初现时间4 = row["变形迹象初现时间4"].ToString();
            landslide.变形迹象名称5 = row["变形迹象名称5"].ToString();
            landslide.变形迹象部位5 = row["变形迹象部位5"].ToString();
            landslide.变形迹象特征5 = row["变形迹象特征5"].ToString();
            //landslide.变形迹象初现时间5 = row["变形迹象初现时间年5"].ToString() + (row["变形迹象初现时间月5"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月5"].ToString() + "月") + (row["变形迹象初现时间日5"].ToString() == "" ? "" : row["变形迹象初现时间日5"].ToString() + "日");
            landslide.变形迹象初现时间5 = row["变形迹象初现时间5"].ToString();
            landslide.变形迹象名称6 = row["变形迹象名称6"].ToString();
            landslide.变形迹象部位6 = row["变形迹象部位6"].ToString();
            landslide.变形迹象特征6 = row["变形迹象特征6"].ToString();
            //landslide.变形迹象初现时间6 = row["变形迹象初现时间年6"].ToString() + (row["变形迹象初现时间月6"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月6"].ToString() + "月") + (row["变形迹象初现时间日6"].ToString() == "" ? "" : row["变形迹象初现时间日6"].ToString() + "日");
            landslide.变形迹象初现时间6 = row["变形迹象初现时间6"].ToString();
            landslide.变形迹象名称7 = row["变形迹象名称7"].ToString();
            landslide.变形迹象部位7 = row["变形迹象部位7"].ToString();
            landslide.变形迹象特征7 = row["变形迹象特征7"].ToString();
            //landslide.变形迹象初现时间7 = row["变形迹象初现时间年7"].ToString() + (row["变形迹象初现时间月7"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月7"].ToString() + "月") + (row["变形迹象初现时间日7"].ToString() == "" ? "" : row["变形迹象初现时间日7"].ToString() + "日");
            landslide.变形迹象初现时间7 = row["变形迹象初现时间7"].ToString();
            landslide.变形迹象名称8 = row["变形迹象名称8"].ToString();
            landslide.变形迹象部位8 = row["变形迹象部位8"].ToString();
            landslide.变形迹象特征8 = row["变形迹象特征8"].ToString();
            //landslide.变形迹象初现时间8 = row["变形迹象初现时间年8"].ToString() + (row["变形迹象初现时间月8"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月8"].ToString() + "月") + (row["变形迹象初现时间日8"].ToString() == "" ? "" : row["变形迹象初现时间日8"].ToString() + "日");
            landslide.变形迹象初现时间8 = row["变形迹象初现时间8"].ToString();
            landslide.地质因素 = row["地质因素"].ToString();
            landslide.地貌因素 = row["地貌因素"].ToString();
            landslide.物理因素 = row["物理因素"].ToString();
            landslide.人为因素 = row["人为因素"].ToString();
            landslide.主导因素 = row["主导因素"].ToString();
            landslide.复活诱发因素 = row["复活诱发因素"].ToString();
            landslide.目前稳定状态 = row["目前稳定状态"].ToString();
            landslide.今后变化趋势 = row["今后变化趋势"].ToString();
            landslide.隐患点 = bool.Parse(row["隐患点"].ToString() == "" ? "false" : row["隐患点"].ToString());
            landslide.毁坏房屋 = float.Parse(row["毁坏房屋"].ToString() == "" ? "0" : row["毁坏房屋"].ToString());
            landslide.威胁住户 = float.Parse(row["威胁住户"].ToString() == "" ? "0" : row["威胁住户"].ToString());
            landslide.防灾预案 = bool.Parse(row["防灾预案"].ToString() == "" ? "false" : row["防灾预案"].ToString());
            landslide.多媒体 = bool.Parse(row["多媒体"].ToString() == "" ? "false" : row["多媒体"].ToString());
            landslide.监测建议 = row["监测建议"].ToString();
            landslide.防治建议 = row["防治建议"].ToString();
            landslide.群测人员 = row["群测人员"].ToString();
            landslide.村长 = row["村长"].ToString();
            landslide.电话 = row["电话"].ToString();
            landslide.调查负责人 = row["调查负责人"].ToString();
            landslide.填表人 = row["填表人"].ToString();
            landslide.审核人 = row["审核人"].ToString();
            landslide.调查单位 = row["调查单位"].ToString();
            //landslide.填表日期 = row["填表日期年"].ToString() + (row["填表日期月"].ToString() == "" ? "" : "年" + row["填表日期月"].ToString() + "月") + (row["填表日期日"].ToString() == "" ? "" : row["填表日期日"].ToString() + "日");
            landslide.填表日期 = row["填表日期"].ToString();
            landslide.平面示意图 = (byte[])(row["平面示意图"].ToString() == "" ? null : row["平面示意图"]);
            landslide.剖面示意图 = (byte[])(row["剖面示意图"].ToString() == "" ? null : row["剖面示意图"]);
            landslide.滑坡情况 = row["滑坡情况"].ToString();
            #endregion

            return landslide;

        }
    }
}
