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
    /// 地裂缝主表信息录入
    /// </summary>
    public class LoadLandFracture
    {

        public virtual string InsertData(string landFilePath, string comFilePath)
        {
            DataTable landTable = AccessHelper.GetDataTableByAccessFile(landFilePath, "地裂缝主表");
            DataTable comTable = ExcelHelper.GetDataTableByExcelFile(comFilePath, "十万调查");
            LoadComprehensive loadCom = new LoadComprehensive();
            ComprehensiveService compService = new ComprehensiveService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < landTable.Rows.Count; i++)
            {
                try
                {
                    LandFracture landFracture = GetLandFracture(landTable.Rows[i], landTable.Columns);
                    string uId = landTable.Rows[i]["统一编号"].ToString();
                    DataRow[] rows = comTable.Select("统一编号=" + uId);
                    if (rows.Length > 1 || rows.Length < 1)
                        throw new Exception(@"不存在综合表或综合表不唯一");

                    Comprehensive comp = loadCom.GetComprehensive(rows[0], comTable.Columns);
                    comp.LandFracture = landFracture;
                    compService.InsertComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message + "\n");
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("地裂缝表", landTable.Rows.Count, successCount,
                failCount, errorStr.ToString());
            return result;
        }





        public LandFracture GetLandFracture(DataRow row, DataColumnCollection columns)
        {
            LandFracture landFracture = new LandFracture();

            #region LandFracture 地裂缝
            landFracture.野外编号 = row["野外编号"].ToString();
            landFracture.室内编号 = row["室内编号"].ToString();
            landFracture.单缝缝号1 = (int)(float.Parse(row["单缝缝号1"].ToString() == "" ? "0" : row["单缝缝号1"].ToString()));
            landFracture.单缝形态1 = row["单缝形态1"].ToString();
            landFracture.单缝延伸方向1 = row["单缝延伸方向1"].ToString();
            landFracture.单缝倾向1 = (int)(float.Parse(row["单缝倾向1"].ToString() == "" ? "0" : row["单缝倾向1"].ToString()));
            landFracture.单缝倾角1 = (int)(float.Parse(row["单缝倾角1"].ToString() == "" ? "0" : row["单缝倾角1"].ToString()));
            landFracture.单缝长度1 = row["单缝长度1"].ToString();
            landFracture.单缝宽度1 = row["单缝宽度1"].ToString();
            landFracture.单缝深度1 = row["单缝深度1"].ToString();
            landFracture.单缝规模等级1 = row["单缝规模等级1"].ToString();
            landFracture.单缝性质1 = row["单缝性质1"].ToString();
            landFracture.单缝位移方向1 = row["单缝位移方向1"].ToString();
            landFracture.单缝位移距离1 = row["单缝位移距离1"].ToString();
            landFracture.单缝填充物1 = row["单缝填充物1"].ToString();
            //landFracture.单缝出现时间1 = row["单缝出现时间年1"].ToString() + (row["单缝出现时间月1"].ToString() == "" ? "" : "年" + row["单缝出现时间月1"].ToString() + "月") + (row["单缝出现时间日1"].ToString() == "" ? "" : row["单缝出现时间日1"].ToString() + "日");
            landFracture.单缝出现时间1 = row["单缝出现时间1"].ToString();
            landFracture.单缝活动性1 = row["单缝活动性1"].ToString();
            landFracture.单缝缝号2 = row["单缝缝号2"].ToString();
            landFracture.单缝形态2 = row["单缝形态2"].ToString();
            landFracture.单缝延伸方向2 = row["单缝延伸方向2"].ToString();
            landFracture.单缝倾向2 = (int)(float.Parse(row["单缝倾向2"].ToString() == "" ? "0" : row["单缝倾向2"].ToString()));
            landFracture.单缝倾角2 = (int)(float.Parse(row["单缝倾角2"].ToString() == "" ? "0" : row["单缝倾角2"].ToString()));
            landFracture.单缝长度2 = row["单缝长度2"].ToString();
            landFracture.单缝宽度2 = row["单缝宽度2"].ToString();
            landFracture.单缝深度2 = row["单缝深度2"].ToString();
            landFracture.单缝规模等级2 = row["单缝规模等级2"].ToString();
            landFracture.单缝性质2 = row["单缝性质2"].ToString();
            landFracture.单缝位移方向2 = row["单缝位移方向2"].ToString();
            landFracture.单缝位移距离2 = row["单缝位移距离2"].ToString();
            landFracture.单缝填充物2 = row["单缝填充物2"].ToString();
            //landFracture.单缝出现时间2 = row["单缝出现时间年2"].ToString() + (row["单缝出现时间月2"].ToString() == "" ? "" : "年" + row["单缝出现时间月2"].ToString() + "月") + (row["单缝出现时间日2"].ToString() == "" ? "" : row["单缝出现时间日2"].ToString() + "日");
            landFracture.单缝出现时间2 = row["单缝出现时间2"].ToString();
            landFracture.单缝活动性2 = row["单缝活动性2"].ToString();
            landFracture.单缝缝号3 = row["单缝缝号3"].ToString();
            landFracture.单缝形态3 = row["单缝形态3"].ToString();
            landFracture.单缝延伸方向3 = row["单缝延伸方向3"].ToString();
            landFracture.单缝倾向3 = (int)(float.Parse(row["单缝倾向3"].ToString() == "" ? "0" : row["单缝倾向3"].ToString()));
            landFracture.单缝倾角3 = float.Parse(row["单缝倾角3"].ToString() == "" ? "0" : row["单缝倾角3"].ToString());
            landFracture.单缝长度3 = row["单缝长度3"].ToString();
            landFracture.单缝宽度3 = row["单缝宽度3"].ToString();
            landFracture.单缝深度3 = row["单缝深度3"].ToString();
            landFracture.单缝规模等级3 = row["单缝规模等级3"].ToString();
            landFracture.单缝性质3 = row["单缝性质3"].ToString();
            landFracture.单缝位移方向3 = row["单缝位移方向3"].ToString();
            landFracture.单缝位移距离3 = row["单缝位移距离3"].ToString();
            landFracture.单缝填充物3 = row["单缝填充物3"].ToString();
            //landFracture.单缝出现时间3 = row["单缝出现时间年3"].ToString() + (row["单缝出现时间月3"].ToString() == "" ? "" : "年" + row["单缝出现时间月3"].ToString() + "月") + (row["单缝出现时间日3"].ToString() == "" ? "" : row["单缝出现时间日3"].ToString() + "日");
            landFracture.单缝出现时间3 = row["单缝出现时间3"].ToString();
            landFracture.单缝活动性3 = row["单缝活动性3"].ToString();
            landFracture.群缝缝数 = (int)(float.Parse(row["群缝缝数"].ToString() == "" ? "0" : row["群缝缝数"].ToString()));
            landFracture.群缝分布面积 = float.Parse(row["群缝分布面积"].ToString() == "" ? "0" : row["群缝分布面积"].ToString());
            landFracture.群缝发育间距 = float.Parse(row["群缝发育间距"].ToString() == "" ? "0" : row["群缝发育间距"].ToString());
            landFracture.群缝排列形式 = row["群缝排列形式"].ToString();
            landFracture.裂缝长度max = float.Parse(row["裂缝长度max"].ToString() == "" ? "0" : row["裂缝长度max"].ToString());
            landFracture.裂缝长度min = float.Parse(row["裂缝长度min"].ToString() == "" ? "0" : row["裂缝长度min"].ToString());
            landFracture.裂缝宽度max = float.Parse(row["裂缝宽度max"].ToString() == "" ? "0" : row["裂缝长度min"].ToString());
            landFracture.裂缝宽度min = float.Parse(row["裂缝宽度min"].ToString() == "" ? "0" : row["裂缝宽度min"].ToString());
            landFracture.裂缝深度max = float.Parse(row["裂缝深度max"].ToString() == "" ? "0" : row["裂缝深度max"].ToString());
            landFracture.裂缝深度min = float.Parse(row["裂缝深度min"].ToString() == "" ? "0" : row["裂缝深度min"].ToString());
            //landFracture.始发时间 = row["始发时间年"].ToString() + (row["始发时间月"].ToString() == "" ? "" : "年" + row["始发时间月"].ToString() + "月") + (row["始发时间日"].ToString() == "" ? "" : row["始发时间日"].ToString() + "日");
            //landFracture.盛发开始时间 = row["盛发开始时间年"].ToString() + (row["盛发开始时间月"].ToString() == "" ? "" : "年" + row["盛发开始时间月"].ToString() + "月") + (row["盛发开始时间日"].ToString() == "" ? "" : row["盛发开始时间日"].ToString() + "日");
            //landFracture.盛发截止时间 = row["盛发截止时间年"].ToString() + (row["盛发截止时间月"].ToString() == "" ? "" : "年" + row["盛发截止时间月"].ToString() + "月") + (row["盛发截止时间日"].ToString() == "" ? "" : row["盛发截止时间日"].ToString() + "日");
            //landFracture.停止时间 = row["停止时间年"].ToString() + (row["停止时间月"].ToString() == "" ? "" : "年" + row["停止时间月"].ToString() + "月") + (row["停止时间日"].ToString() == "" ? "" : row["停止时间日"].ToString() + "日");

            landFracture.始发时间 = row["始发时间"].ToString();
            landFracture.盛发开始时间 = row["盛发开始时间"].ToString();
            landFracture.盛发截止时间 = row["盛发截止时间"].ToString();
            landFracture.停止时间 = row["停止时间"].ToString();

            landFracture.目前发展情况 = row["目前发展情况"].ToString();
            landFracture.成因类型 = row["成因类型"].ToString();
            landFracture.裂缝区地貌特征 = row["裂缝区地貌特征"].ToString();
            landFracture.裂缝与地貌走向关系 = row["裂缝与地貌走向关系"].ToString();
            landFracture.裂缝巨岩土层时代 = row["裂缝巨岩土层时代"].ToString();
            landFracture.裂缝巨岩土层岩性 = row["裂缝巨岩土层岩性"].ToString();
            landFracture.受裂土层时间 = row["受裂土层时间"].ToString();
            landFracture.受裂土层土性 = row["受裂土层土性"].ToString();
            landFracture.受裂土下伏层时间 = row["受裂土下伏层时间"].ToString();
            landFracture.受裂土下伏层岩性 = row["受裂土下伏层岩性"].ToString();
            landFracture.受裂岩土层时代 = row["受裂岩土层时代"].ToString();
            landFracture.受裂岩土层岩性 = row["受裂岩土层岩性"].ToString();
            landFracture.胀缩土特征 = row["胀缩土特征"].ToString();
            landFracture.胀缩土膨胀性 = row["胀缩土膨胀性"].ToString();
            landFracture.胀缩土含水量 = float.Parse(row["胀缩土含水量"].ToString() == "" ? "0" : row["胀缩土含水量"].ToString());
            landFracture.裂缝区构造断裂走向1 = row["裂缝区构造断裂走向1"].ToString();
            landFracture.裂缝区构造断裂倾向1 = (int)(float.Parse(row["裂缝区构造断裂倾向1"].ToString() == "" ? "0" : row["裂缝区构造断裂倾向1"].ToString()));
            landFracture.裂缝区构造断裂倾角1 = (int)(float.Parse(row["裂缝区构造断裂倾角1"].ToString() == "" ? "0" : row["裂缝区构造断裂倾角1"].ToString()));
            landFracture.裂缝区构造断裂走向2 = row["裂缝区构造断裂走向2"].ToString();
            landFracture.裂缝区构造断裂倾向2 = (int)(float.Parse(row["裂缝区构造断裂倾向2"].ToString() == "" ? "0" : row["裂缝区构造断裂倾向2"].ToString()));
            landFracture.裂缝区构造断裂倾角2 = (int)(float.Parse(row["裂缝区构造断裂倾角2"].ToString() == "" ? "0" : row["裂缝区构造断裂倾角2"].ToString()));
            landFracture.岩层中断裂倾向 = (int)(float.Parse(row["岩层中断裂倾向"].ToString() == "" ? "0" : row["岩层中断裂倾向"].ToString()));
            landFracture.岩层中断裂倾角 = (int)(float.Parse(row["岩层中断裂倾角"].ToString() == "" ? "0" : row["岩层中断裂倾角"].ToString()));
            landFracture.土层中有无新断裂 = bool.Parse(row["土层中有无新断裂"].ToString() == "" ? "false" : row["土层中有无新断裂"].ToString());
            landFracture.土层中新断裂倾向 = (int)(float.Parse(row["土层中新断裂倾向"].ToString() == "" ? "0" : row["土层中新断裂倾向"].ToString()));
            landFracture.土层中新断裂倾角 = (int)(float.Parse(row["土层中新断裂倾角"].ToString() == "" ? "0" : row["土层中新断裂倾角"].ToString()));
            landFracture.主要构造断裂走向1 = row["主要构造断裂走向1"].ToString();
            landFracture.主要构造断裂倾向1 = (int)(float.Parse(row["主要构造断裂倾向1"].ToString() == "" ? "0" : row["主要构造断裂倾向1"].ToString()));
            landFracture.主要构造断裂倾角1 = (int)(float.Parse(row["主要构造断裂倾角1"].ToString() == "" ? "0" : row["主要构造断裂倾角1"].ToString()));
            landFracture.主要构造断裂走向2 = row["主要构造断裂走向2"].ToString();
            landFracture.主要构造断裂倾向2 = (int)(float.Parse(row["主要构造断裂倾向2"].ToString() == "" ? "0" : row["主要构造断裂倾向2"].ToString()));
            landFracture.主要构造断裂倾角2 = (int)(float.Parse(row["主要构造断裂倾角2"].ToString() == "" ? "0" : row["主要构造断裂倾角2"].ToString()));
            landFracture.胀缩土中有无新断裂 = bool.Parse(row["胀缩土中有无新断裂"].ToString() == "" ? "false" : row["胀缩土中有无新断裂"].ToString());
            landFracture.胀缩土中新断裂倾向 = (int)(float.Parse(row["胀缩土中新断裂倾向"].ToString() == "" ? "0" : row["胀缩土中新断裂倾向"].ToString()));
            landFracture.胀缩土中新断裂倾角 = (int)(float.Parse(row["胀缩土中新断裂倾角"].ToString() == "" ? "0" : row["胀缩土中新断裂倾角"].ToString()));
            landFracture.洞室埋深 = float.Parse(row["洞室埋深"].ToString() == "" ? "0" : row["洞室埋深"].ToString());
            landFracture.洞室规模 = row["洞室规模"].ToString();
            landFracture.洞室长 = float.Parse(row["洞室长"].ToString() == "" ? "0" : row["洞室长"].ToString());
            landFracture.洞室宽 = float.Parse(row["洞室宽"].ToString() == "" ? "0" : row["洞室宽"].ToString());
            landFracture.洞室高 = float.Parse(row["洞室高"].ToString() == "" ? "0" : row["洞室高"].ToString());
            landFracture.洞室与裂缝区位置关系 = row["洞室与裂缝区位置关系"].ToString();
            //landFracture.洞室开挖时间 = row["洞室开挖时间年"].ToString() + (row["洞室开挖时间月"].ToString() == "" ? "" : "年" + row["洞室开挖时间月"].ToString() + "月") + (row["洞室开挖时间日"].ToString() == "" ? "" : row["洞室开挖时间日"].ToString() + "日");
            landFracture.洞室开挖时间 = row["洞室开挖时间"].ToString();
            landFracture.洞室开挖方式 = row["洞室开挖方式"].ToString();
            landFracture.洞室开挖强度 = row["洞室开挖强度"].ToString();
            landFracture.抽排地下水类型 = row["抽排地下水类型"].ToString();
            landFracture.抽排井埋深 = float.Parse(row["抽排井埋深"].ToString() == "" ? "0" : row["抽排井埋深"].ToString());
            landFracture.抽排水位水量 = float.Parse(row["抽排水位水量"].ToString() == "" ? "0" : row["抽排水位水量"].ToString());
            landFracture.抽排日出水量 = float.Parse(row["抽排日出水量"].ToString() == "" ? "0" : row["抽排日出水量"].ToString());
            //landFracture.抽排水开始时间 = row["抽排水开始时间年"].ToString() + (row["抽排水开始时间月"].ToString() == "" ? "" : "年" + row["抽排水开始时间月"].ToString() + "月") + (row["抽排水开始时间日"].ToString() == "" ? "" : row["抽排水开始时间日"].ToString() + "日");
            //landFracture.抽排水停止时间 = row["抽排水停止时间年"].ToString() + (row["抽排水停止时间月"].ToString() == "" ? "" : "年" + row["抽排水停止时间月"].ToString() + "月") + (row["抽排水停止时间日"].ToString() == "" ? "" : row["抽排水停止时间日"].ToString() + "日");

            landFracture.抽排水开始时间 = row["抽排水开始时间"].ToString();
            landFracture.抽排水停止时间 = row["抽排水停止时间"].ToString();

            landFracture.抽排水状态 = bool.Parse(row["抽排水状态"].ToString() == "" ? "false" : row["抽排水状态"].ToString());
            landFracture.地震烈度 = row["地震烈度"].ToString();
            //landFracture.地震发生时间 = row["地震发生时间年"].ToString() + (row["地震发生时间月"].ToString() == "" ? "" : "年" + row["地震发生时间月"].ToString() + "月") + (row["地震发生时间日"].ToString() == "" ? "" : row["地震发生时间日"].ToString() + "日");
            landFracture.地震发生时间 = row["地震发生时间"].ToString();
            landFracture.活动断层位置 = row["活动断层位置"].ToString();
            landFracture.活动断层倾向 = (int)(float.Parse(row["活动断层倾向"].ToString() == "" ? "0" : row["活动断层倾向"].ToString()));
            landFracture.活动断层倾角 = (int)(float.Parse(row["活动断层倾角"].ToString() == "" ? "0" : row["活动断层倾角"].ToString()));
            landFracture.活动断层长度 = float.Parse(row["活动断层长度"].ToString() == "" ? "0" : row["活动断层长度"].ToString());
            landFracture.活动断层性质 = row["活动断层性质"].ToString();
            //landFracture.活动断层活动时间 = row["活动断层活动时间年"].ToString() + (row["活动断层活动时间月"].ToString() == "" ? "" : "年" + row["活动断层活动时间月"].ToString() + "月") + (row["活动断层活动时间日"].ToString() == "" ? "" : row["活动断层活动时间日"].ToString() + "日");
            landFracture.活动断层活动时间 = row["活动断层活动时间"].ToString();
            landFracture.活动断层活动速率 = row["活动断层活动速率"].ToString();
            landFracture.活动断层断距 = row["活动断层断距"].ToString();
            landFracture.水理作用水源 = row["水理作用水源"].ToString();
            //landFracture.水理作用时间 = row["水理作用时间年"].ToString() + (row["水理作用时间月"].ToString() == "" ? "" : "年" + row["水理作用开挖时间月"].ToString() + "月") + (row["水理作用开挖时间日"].ToString() == "" ? "" : row["水理作用开挖时间日"].ToString() + "日");
            landFracture.水理作用时间 = row["水理作用时间"].ToString();
            landFracture.水理作用水质 = row["水理作用水质"].ToString();
            landFracture.水理作用类型 = row["水理作用类型"].ToString();
            //landFracture.水理作用开挖时间 = row["水理作用开挖时间年"].ToString() + (row["水理作用开挖时间月"].ToString() == "" ? "" : "年" + row["活动断层活动时间月"].ToString() + "月") + (row["活动断层活动时间日"].ToString() == "" ? "" : row["活动断层活动时间日"].ToString() + "日");
            landFracture.水理作用开挖时间 = row["水理作用开挖时间"].ToString();
            landFracture.水理作用开挖方式 = row["水理作用开挖方式"].ToString();
            landFracture.水理作用开挖深度 = float.Parse(row["水理作用开挖深度"].ToString() == "" ? "0" : row["水理作用开挖深度"].ToString());
            landFracture.毁坏房屋 = float.Parse(row["毁坏房屋"].ToString() == "" ? "0" : row["毁坏房屋"].ToString());
            landFracture.阻断交通 = row["阻断交通"].ToString();
            landFracture.隐患点 = bool.Parse(row["隐患点"].ToString() == "" ? "false" : row["隐患点"].ToString());
            landFracture.威胁房屋 = float.Parse(row["威胁房屋"].ToString() == "" ? "0" : row["威胁房屋"].ToString());
            landFracture.交通隐患 = row["交通隐患"].ToString();
            landFracture.发展预测 = row["发展预测"].ToString();
            landFracture.防灾预案 = bool.Parse(row["防灾预案"].ToString() == "" ? "false" : row["防灾预案"].ToString());
            landFracture.多媒体 = bool.Parse(row["多媒体"].ToString() == "" ? "false" : row["多媒体"].ToString());
            landFracture.防治措施及效果 = row["防治措施及效果"].ToString();
            landFracture.防治建议 = row["防治建议"].ToString();
            landFracture.调查负责人 = row["调查负责人"].ToString();
            landFracture.填表人 = row["填表人"].ToString();
            landFracture.审核人 = row["审核人"].ToString();
            landFracture.调查单位 = row["调查单位"].ToString();
            //landFracture.填表日期 = row["填表日期年"].ToString() + (row["填表日期月"].ToString() == "" ? "" : "年" + row["填表日期月"].ToString() + "月") + (row["填表日期日"].ToString() == "" ? "" : row["填表日期日"].ToString() + "日");
            landFracture.填表日期 = row["填表日期"].ToString();
            landFracture.抽排水位置关系 = row["抽排水位置关系"].ToString();
            landFracture.平面示意图 = (byte[])(row["平面示意图"].ToString() == "" ? null : row["平面示意图"]);
            landFracture.剖面示意图 = (byte[])(row["剖面示意图"].ToString() == "" ? null : row["剖面示意图"]);
            //landFracture.省名 = row["省"].ToString();
            //landFracture.县名 = row["县"].ToString();
            //landFracture.街道 = row["省"].ToString() + row["县"].ToString() + row["乡"].ToString() + row["村"].ToString() + row["组"].ToString();
            //landFracture.平面示意图路径 = row["平面示意图"].ToString();
            //landFracture.剖面示意图路径 = row["剖面示意图"].ToString();
            #endregion


            return landFracture;
        }
    }
}
