using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Investigation;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.Investigation
{

    /// <summary>
    /// 崩塌点数据录入
    /// </summary>
    public class LoadLandSlip
    {


        /// <summary>
        /// 插入崩塌点主表数据
        /// </summary>
        /// <param name="landFilePath"></param>
        /// <param name="comFilePath"></param>
        /// <returns></returns>
        public virtual string InsertData(string filePath)
        {
            DataTable landTable = AccessHelper.GetDataTableByAccessFile(filePath, "崩塌主表");
            DataTable comTable = AccessHelper.GetDataTableByAccessFile(filePath, "综合表");
            LoadComprehensive loadCom = new LoadComprehensive();
            ComprehensiveService compService = new ComprehensiveService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < landTable.Rows.Count; i++)
            {
                try
                {
                    LandSlip landSlip = GetLandSlip(landTable.Rows[i], landTable.Columns);
                    string uId = landTable.Rows[i]["统一编号"].ToString();
                    DataRow[] rows = comTable.Select("统一编号=" + uId);
                    if (rows.Length > 1 || rows.Length < 1)
                        throw new Exception(@"不存在综合表或综合表不唯一");

                    Comprehensive comp = loadCom.GetComprehensive(rows[0], comTable.Columns);
                    comp.LandSlip = landSlip;
                    compService.InsertComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，统一编号：" + landTable.Rows[i]["统一编号"] + ";失败原因是：" + ex.Message + "\n");
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("崩塌表", landTable.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }


        /// <summary>
        /// 获取崩塌主表信息
        /// </summary>
        public LandSlip GetLandSlip(DataRow row, DataColumnCollection columns)
        {
            LandSlip landSlip = new LandSlip();

            #region LandSlip
            landSlip.野外编号 = row["野外编号"].ToString();
            landSlip.室内编号 = row["室内编号"].ToString();
            landSlip.斜坡类型 = row["斜坡类型"].ToString();
            landSlip.崩塌类型 = row["崩塌类型"].ToString();
            landSlip.坡顶标高 = float.Parse(row["坡顶标高"].ToString() == "" ? "0" : row["坡顶标高"].ToString());
            landSlip.坡脚标高 = float.Parse(row["坡脚标高"].ToString() == "" ? "0" : row["坡脚标高"].ToString());
            landSlip.地层时代 = row["地层时代"].ToString();
            landSlip.地层岩性 = row["地层岩性"].ToString();
            landSlip.构造部位 = row["构造部位"].ToString();
            landSlip.地震烈度 = row["地震烈度"].ToString();
            landSlip.地层倾向 = (int)(float.Parse(row["地层倾向"].ToString() == "" ? "0" : row["地层倾向"].ToString()));
            landSlip.地层倾角 = (int)(float.Parse(row["地层倾角"].ToString() == "" ? "0" : row["地层倾角"].ToString()));
            landSlip.微地貌 = row["微地貌"].ToString();
            landSlip.地下水类型 = row["地下水类型"].ToString();
            landSlip.年均降雨量 = float.Parse(row["年均降雨量"].ToString() == "" ? "0" : row["年均降雨量"].ToString());
            landSlip.日最大降雨 = float.Parse(row["日最大降雨"].ToString() == "" ? "0" : row["日最大降雨"].ToString());
            landSlip.时最大降雨 = float.Parse(row["时最大降雨"].ToString() == "" ? "0" : row["时最大降雨"].ToString());
            landSlip.洪水位 = float.Parse(row["洪水位"].ToString() == "" ? "0" : row["洪水位"].ToString());
            landSlip.枯水位 = float.Parse(row["枯水位"].ToString() == "" ? "0" : row["枯水位"].ToString());
            landSlip.相对河流位置 = row["相对河流位置"].ToString();
            landSlip.坡高 = float.Parse(row["坡高"].ToString() == "" ? "0" : row["坡高"].ToString());
            landSlip.坡长 = float.Parse(row["坡长"].ToString() == "" ? "0" : row["坡长"].ToString());
            landSlip.坡宽 = float.Parse(row["坡宽"].ToString() == "" ? "0" : row["坡宽"].ToString());
            landSlip.规模 = float.Parse(row["规模"].ToString() == "" ? "0" : row["规模"].ToString());
            landSlip.坡度 = float.Parse(row["坡度"].ToString() == "" ? "0" : row["坡度"].ToString());
            landSlip.坡向 = float.Parse(row["坡向"].ToString() == "" ? "0" : row["坡向"].ToString());
            landSlip.岩体结构类型 = row["岩体结构类型"].ToString();
            landSlip.岩体厚度 = float.Parse(row["岩体厚度"].ToString() == "" ? "0" : row["岩体厚度"].ToString());
            landSlip.岩体裂隙组数 = (int)(float.Parse(row["岩体裂隙组数"].ToString() == "" ? "0" : row["岩体裂隙组数"].ToString()));
            landSlip.岩体块度 = row["岩体块度"].ToString();
            landSlip.斜坡结构类型 = row["斜坡结构类型"].ToString();
            landSlip.控制结构面类型1 = row["控制结构面类型1"].ToString();
            landSlip.控制结构面倾向1 = (int)(float.Parse(row["控制结构面倾向1"].ToString() == "" ? "0" : row["控制结构面倾向1"].ToString()));
            landSlip.控制结构面倾角1 = (int)(float.Parse(row["控制结构面倾角1"].ToString() == "" ? "0" : row["控制结构面倾角1"].ToString()));
            landSlip.控制结构面长度1 = float.Parse(row["控制结构面长度1"].ToString() == "" ? "0" : row["控制结构面长度1"].ToString());
            landSlip.控制结构面间距1 = float.Parse(row["控制结构面间距1"].ToString() == "" ? "0" : row["控制结构面间距1"].ToString());
            landSlip.控制结构面类型2 = row["控制结构面类型2"].ToString();
            landSlip.控制结构面倾向2 = (int)(float.Parse(row["控制结构面倾向2"].ToString() == "" ? "0" : row["控制结构面倾向2"].ToString()));
            landSlip.控制结构面倾角2 = (int)(float.Parse(row["控制结构面倾角2"].ToString() == "" ? "0" : row["控制结构面倾角2"].ToString()));
            landSlip.控制结构面长度2 = float.Parse(row["控制结构面长度2"].ToString() == "" ? "0" : row["控制结构面长度2"].ToString());
            landSlip.控制结构面间距2 = float.Parse(row["控制结构面间距2"].ToString() == "" ? "0" : row["控制结构面间距2"].ToString());
            landSlip.控制结构面类型3 = row["控制结构面类型3"].ToString();
            landSlip.控制结构面倾向3 = (int)(float.Parse(row["控制结构面倾向3"].ToString() == "" ? "0" : row["控制结构面倾向3"].ToString()));
            landSlip.控制结构面倾角3 = (int)(float.Parse(row["控制结构面倾角3"].ToString() == "" ? "0" : row["控制结构面倾角3"].ToString()));
            landSlip.控制结构面长度3 = float.Parse(row["控制结构面长度3"].ToString() == "" ? "0" : row["控制结构面长度3"].ToString());
            landSlip.控制结构面间距3 = float.Parse(row["控制结构面间距3"].ToString() == "" ? "0" : row["控制结构面间距3"].ToString());
            landSlip.全风化带深度 = float.Parse(row["全风化带深度"].ToString() == "" ? "0" : row["全风化带深度"].ToString());
            landSlip.卸荷裂缝深度 = float.Parse(row["卸荷裂缝深度"].ToString() == "" ? "0" : row["卸荷裂缝深度"].ToString());
            landSlip.土体名称 = row["土体名称"].ToString();
            landSlip.土体密实度 = row["土体密实度"].ToString();
            landSlip.土体稠度 = row["土体稠度"].ToString();
            landSlip.下伏基岩时代 = row["下伏基岩时代"].ToString();
            landSlip.下伏基岩岩性 = row["下伏基岩岩性"].ToString();
            landSlip.下伏基岩倾向 = (int)(float.Parse(row["下伏基岩倾向"].ToString() == "" ? "0" : row["下伏基岩倾向"].ToString()));
            landSlip.下伏基岩倾角 = (int)(float.Parse(row["下伏基岩倾角"].ToString() == "" ? "0" : row["下伏基岩倾角"].ToString()));
            landSlip.下伏基岩埋深 = (int)(float.Parse(row["下伏基岩埋深"].ToString() == "" ? "0" : row["下伏基岩埋深"].ToString()));
            landSlip.变形迹象名称1 = row["变形迹象名称1"].ToString();
            landSlip.变形迹象部位1 = row["变形迹象部位1"].ToString();
            landSlip.变形迹象特征1 = row["变形迹象特征1"].ToString();
            //landSlip.变形迹象初现时间1 = row["变形迹象初现时间年1"].ToString() + (row["变形迹象初现时间月1"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月1"].ToString() + "月");
            landSlip.变形迹象初现时间1 = row["变形迹象初现时间1"].ToString();
            landSlip.变形迹象名称2 = row["变形迹象名称2"].ToString();
            landSlip.变形迹象部位2 = row["变形迹象部位2"].ToString();
            landSlip.变形迹象特征2 = row["变形迹象特征2"].ToString();
            //landSlip.变形迹象初现时间2 = row["变形迹象初现时间年2"].ToString() + (row["变形迹象初现时间月2"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月2"].ToString() + "月");
            landSlip.变形迹象初现时间2 = row["变形迹象初现时间2"].ToString();
            landSlip.变形迹象名称3 = row["变形迹象名称3"].ToString();
            landSlip.变形迹象部位3 = row["变形迹象部位3"].ToString();
            landSlip.变形迹象特征3 = row["变形迹象特征3"].ToString();
            //landSlip.变形迹象初现时间3 = row["变形迹象初现时间年3"].ToString() + (row["变形迹象初现时间月3"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月3"].ToString() + "月");
            landSlip.变形迹象初现时间3 = row["变形迹象初现时间3"].ToString();
            landSlip.变形迹象名称4 = row["变形迹象名称4"].ToString();
            landSlip.变形迹象部位4 = row["变形迹象部位4"].ToString();
            landSlip.变形迹象特征4 = row["变形迹象特征4"].ToString();
            //landSlip.变形迹象初现时间4 = row["变形迹象初现时间年4"].ToString() + (row["变形迹象初现时间月4"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月4"].ToString() + "月");
            landSlip.变形迹象初现时间4 = row["变形迹象初现时间4"].ToString();
            landSlip.变形迹象名称5 = row["变形迹象名称5"].ToString();
            landSlip.变形迹象部位5 = row["变形迹象部位5"].ToString();
            landSlip.变形迹象特征5 = row["变形迹象特征5"].ToString();
            //landSlip.变形迹象初现时间5 = row["变形迹象初现时间年5"].ToString() + (row["变形迹象初现时间月5"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月5"].ToString() + "月");
            landSlip.变形迹象初现时间5 = row["变形迹象初现时间5"].ToString();
            landSlip.变形迹象名称6 = row["变形迹象名称6"].ToString();
            landSlip.变形迹象部位6 = row["变形迹象部位6"].ToString();
            landSlip.变形迹象特征6 = row["变形迹象特征6"].ToString();
            //landSlip.变形迹象初现时间6 = row["变形迹象初现时间年6"].ToString() + (row["变形迹象初现时间月6"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月6"].ToString() + "月");
            landSlip.变形迹象初现时间6 = row["变形迹象初现时间6"].ToString();
            landSlip.变形迹象名称7 = row["变形迹象名称7"].ToString();
            landSlip.变形迹象部位7 = row["变形迹象部位7"].ToString();
            landSlip.变形迹象特征7 = row["变形迹象特征7"].ToString();
            //landSlip.变形迹象初现时间7 = row["变形迹象初现时间年7"].ToString() + (row["变形迹象初现时间月7"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月7"].ToString() + "月");
            landSlip.变形迹象初现时间7 = row["变形迹象初现时间7"].ToString();
            landSlip.变形迹象名称8 = row["变形迹象名称8"].ToString();
            landSlip.变形迹象部位8 = row["变形迹象部位8"].ToString();
            landSlip.变形迹象特征8 = row["变形迹象特征8"].ToString();
            //landSlip.变形迹象初现时间8 = row["变形迹象初现时间年8"].ToString() + (row["变形迹象初现时间月8"].ToString() == "" ? "" : "年" + row["变形迹象初现时间月8"].ToString() + "月");
            landSlip.变形迹象初现时间8 = row["变形迹象初现时间8"].ToString();
            landSlip.危岩体可能失稳因素 = row["危岩体可能失稳因素"].ToString();
            landSlip.危岩体目前稳定程度 = row["危岩体目前稳定程度"].ToString();
            landSlip.危岩体今后变化趋势 = row["危岩体今后变化趋势"].ToString();
            landSlip.地下水埋深 = (int)(float.Parse(row["地下水埋深"].ToString() == "" ? "0" : row["地下水埋深"].ToString())); ;
            landSlip.地下水露头 = row["地下水露头"].ToString();
            landSlip.地下水补给类型 = row["地下水补给类型"].ToString();
            landSlip.堆积体长度 = float.Parse(row["堆积体长度"].ToString() == "" ? "0" : row["堆积体长度"].ToString());
            landSlip.堆积体宽度 = float.Parse(row["堆积体宽度"].ToString() == "" ? "0" : row["堆积体宽度"].ToString());
            landSlip.堆积体厚度 = float.Parse(row["堆积体厚度"].ToString() == "" ? "0" : row["堆积体厚度"].ToString());
            landSlip.堆积体体积 = row["堆积体体积"].ToString();
            landSlip.堆积体坡度 = float.Parse(row["堆积体坡度"].ToString() == "" ? "0" : row["堆积体坡度"].ToString());
            landSlip.堆积体坡向 = float.Parse(row["堆积体坡向"].ToString() == "" ? "0" : row["堆积体坡向"].ToString());
            landSlip.堆积体坡面形态 = row["堆积体坡面形态"].ToString();
            landSlip.堆积体稳定性 = row["堆积体稳定性"].ToString();
            landSlip.堆积体可能失稳因素 = row["堆积体可能失稳因素"].ToString();
            landSlip.堆积体目前稳定状态 = row["堆积体目前稳定状态"].ToString();
            landSlip.堆积体今后变化趋势 = row["堆积体今后变化趋势"].ToString();
            landSlip.隐患点 = bool.Parse(row["隐患点"].ToString() == "" ? "false" : row["隐患点"].ToString());
            landSlip.防灾预案 = bool.Parse(row["防灾预案"].ToString() == "" ? "false" : row["防灾预案"].ToString());
            landSlip.多媒体 = bool.Parse(row["多媒体"].ToString() == "" ? "false" : row["多媒体"].ToString());
            landSlip.毁坏房屋 = (int)(float.Parse(row["毁坏房屋"].ToString() == "" ? "0" : row["毁坏房屋"].ToString()));
            landSlip.毁路 = float.Parse(row["毁路"].ToString() == "" ? "0" : row["毁路"].ToString());
            landSlip.毁渠 = float.Parse(row["毁渠"].ToString() == "" ? "0" : row["毁渠"].ToString());
            landSlip.其它危害 = row["其它危害"].ToString();
            landSlip.威胁人口 = (int)(float.Parse(row["威胁人口"].ToString() == "" ? "0" : row["威胁人口"].ToString()));
            landSlip.威胁财产 = Convert.ToDouble(row["威胁财产"].ToString() == "" ? "0" : row["威胁财产"].ToString());
            landSlip.险情等级 = row["险情等级"].ToString();
            landSlip.监测建议 = row["监测建议"].ToString();
            landSlip.防治建议 = row["防治建议"].ToString();
            landSlip.群测人员 = row["群测人员"].ToString();
            landSlip.村长 = row["村长"].ToString();
            landSlip.电话 = row["电话"].ToString();
            landSlip.调查负责人 = row["调查负责人"].ToString();
            landSlip.填表人 = row["填表人"].ToString();
            landSlip.审核人 = row["审核人"].ToString();
            landSlip.调查单位 = row["调查单位"].ToString();
            //landSlip.填表日期 = (row["填表日期年"].ToString() == "" ? "" : row["填表日期年"].ToString() + "年") + (row["填表日期月"].ToString() == "" ? "" : row["填表日期月"].ToString() + "月") + (row["填表日期日"].ToString() == "" ? "" : row["填表日期日"].ToString() + "日");
            landSlip.填表日期 = row["填表日期"].ToString();
            landSlip.土地利用 = row["土地利用"].ToString();
            landSlip.发生时间 = row["发生时间"].ToString();
            landSlip.平面示意图 = (byte[])(row["平面示意图"].ToString() == "" ? null : row["平面示意图"]);
            landSlip.剖面示意图 = (byte[])(row["剖面示意图"].ToString() == "" ? null : row["剖面示意图"]);
            landSlip.崩塌情况 = row["崩塌情况"].ToString();
            //landSlip.省名 = row["省"].ToString();
            //landSlip.县名 = row["县"].ToString();
            //landSlip.街道 = row["省"].ToString() + row["县"].ToString() + row["乡"].ToString() + row["村"].ToString() + row["组"].ToString();
            //landSlip.灾害体积 = float.Parse(row["灾害体积"].ToString() == "" ? "0" : row["灾害体积"].ToString());
            landSlip.平面示意图路径 = row["平面示意图"].ToString();
            landSlip.剖面示意图路径 = row["平面示意图"].ToString();

            #endregion

            return landSlip;
        }



    }
}
