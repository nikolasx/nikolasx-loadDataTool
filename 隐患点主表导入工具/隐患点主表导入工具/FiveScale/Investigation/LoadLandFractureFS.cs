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
    /// 地裂缝
    /// </summary>
    public class LoadLandFractureFS
    {
        public virtual string InsertData(string filePath)
        {
            DataTable landTable = AccessHelper.GetDataTableByAccessFile(filePath, "地裂缝主表");
            DataTable comTable = AccessHelper.GetDataTableByAccessFile(filePath, "汇总表");
            LoadComprehensiveFS loadCom = new LoadComprehensiveFS();
            ComprehensiveFSService compService = new ComprehensiveFSService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < landTable.Rows.Count; i++)
            {
                try
                {
                    LandFractureFS landSlip = GetLandFractureFS(landTable.Rows[i], landTable.Columns);
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
                    comp.LandFractureFS = landSlip;
                    compService.InsertComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，统一编号是：" + landTable.Rows[i]["统一编号"] + ";失败原因是：" + ex.Message + "\n");
                }
            }
            string result = UtilMethod.GetPrintErrorInfo("地裂缝表(5W)", landTable.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }

        public LandFractureFS GetLandFractureFS(DataRow row, DataColumnCollection columns)
        {
            LandFractureFS disaster = new LandFractureFS();

            #region LandFracture 地裂缝
            disaster.野外编号 = row["野外编号"].ToString();
            disaster.室内编号 = row["室内编号"].ToString();
            disaster.单缝缝号1 = (int)(float.Parse(row["单缝缝号1"].ToString() == "" ? "0" : row["单缝缝号1"].ToString()));
            disaster.单缝形态1 = row["单缝形态1"].ToString();
            disaster.单缝延伸方向1 = ConvertHelper.GetFloatValutFromStr(row["单缝延伸方向1"].ToString());
            disaster.单缝倾向1 = (int)(float.Parse(row["单缝倾向1"].ToString() == "" ? "0" : row["单缝倾向1"].ToString()));
            disaster.单缝倾角1 = (int)(float.Parse(row["单缝倾角1"].ToString() == "" ? "0" : row["单缝倾角1"].ToString()));
            disaster.单缝长度1 = ConvertHelper.GetFloatValutFromStr(row["单缝长度1"].ToString());
            disaster.单缝宽度1 = ConvertHelper.GetFloatValutFromStr(row["单缝宽度1"].ToString());
            disaster.单缝深度1 = ConvertHelper.GetFloatValutFromStr(row["单缝深度1"].ToString());
            disaster.单缝规模等级1 = row["单缝规模等级1"].ToString();
            disaster.单缝性质1 = row["单缝性质1"].ToString();
            disaster.单缝位移方向1 = ConvertHelper.GetFloatValutFromStr(row["单缝位移方向1"].ToString());
            disaster.单缝位移距离1 = ConvertHelper.GetFloatValutFromStr(row["单缝位移距离1"].ToString());
            disaster.单缝填充物1 = row["单缝填充物1"].ToString();
            //landFracture.单缝出现时间1 = row["单缝出现时间年1"].ToString() + (row["单缝出现时间月1"].ToString() == "" ? "" : "年" + row["单缝出现时间月1"].ToString() + "月") + (row["单缝出现时间日1"].ToString() == "" ? "" : row["单缝出现时间日1"].ToString() + "日");
            //disaster.单缝出现时间1 = row["单缝出现时间1"].ToString();
            disaster.单缝活动性1 = row["单缝活动性1"].ToString();
            disaster.单缝缝号2 = ConvertHelper.GetIntValueByStr(row["单缝缝号2"].ToString());
            disaster.单缝形态2 = row["单缝形态2"].ToString();
            disaster.单缝延伸方向2 = ConvertHelper.GetFloatValutFromStr(row["单缝延伸方向2"].ToString());
            disaster.单缝倾向2 = (int)(float.Parse(row["单缝倾向2"].ToString() == "" ? "0" : row["单缝倾向2"].ToString()));
            disaster.单缝倾角2 = (int)(float.Parse(row["单缝倾角2"].ToString() == "" ? "0" : row["单缝倾角2"].ToString()));
            disaster.单缝长度2 = ConvertHelper.GetFloatValutFromStr(row["单缝长度2"].ToString());
            disaster.单缝宽度2 = ConvertHelper.GetFloatValutFromStr(row["单缝宽度2"].ToString());
            disaster.单缝深度2 = ConvertHelper.GetFloatValutFromStr(row["单缝深度2"].ToString());
            disaster.单缝规模等级2 = row["单缝规模等级2"].ToString();
            disaster.单缝性质2 = row["单缝性质2"].ToString();
            disaster.单缝位移方向2 = ConvertHelper.GetFloatValutFromStr(row["单缝位移方向2"].ToString());
            disaster.单缝位移距离2 = ConvertHelper.GetFloatValutFromStr(row["单缝位移距离2"].ToString());
            disaster.单缝填充物2 = row["单缝填充物2"].ToString();
            //landFracture.单缝出现时间2 = row["单缝出现时间年2"].ToString() + (row["单缝出现时间月2"].ToString() == "" ? "" : "年" + row["单缝出现时间月2"].ToString() + "月") + (row["单缝出现时间日2"].ToString() == "" ? "" : row["单缝出现时间日2"].ToString() + "日");
            //disaster.单缝出现时间2 = row["单缝出现时间2"].ToString();
            disaster.单缝活动性2 = row["单缝活动性2"].ToString();
            disaster.单缝缝号3 = ConvertHelper.GetIntValueByStr(row["单缝缝号3"].ToString());
            disaster.单缝形态3 = row["单缝形态3"].ToString();
            disaster.单缝延伸方向3 = ConvertHelper.GetFloatValutFromStr(row["单缝延伸方向3"].ToString());
            disaster.单缝倾向3 = (int)(float.Parse(row["单缝倾向3"].ToString() == "" ? "0" : row["单缝倾向3"].ToString()));
            disaster.单缝倾角3 = ConvertHelper.GetIntValueByStr(row["单缝倾角3"].ToString() == "" ? "0" : row["单缝倾角3"].ToString());
            disaster.单缝长度3 = ConvertHelper.GetFloatValutFromStr(row["单缝长度3"].ToString());
            disaster.单缝宽度3 = ConvertHelper.GetFloatValutFromStr(row["单缝宽度3"].ToString());
            disaster.单缝深度3 = ConvertHelper.GetFloatValutFromStr(row["单缝深度3"].ToString());
            disaster.单缝规模等级3 = row["单缝规模等级3"].ToString();
            disaster.单缝性质3 = row["单缝性质3"].ToString();
            disaster.单缝位移方向3 = ConvertHelper.GetFloatValutFromStr(row["单缝位移方向3"].ToString());
            disaster.单缝位移距离3 = ConvertHelper.GetFloatValutFromStr(row["单缝位移距离3"].ToString());
            disaster.单缝填充物3 = row["单缝填充物3"].ToString();
            //landFracture.单缝出现时间3 = row["单缝出现时间年3"].ToString() + (row["单缝出现时间月3"].ToString() == "" ? "" : "年" + row["单缝出现时间月3"].ToString() + "月") + (row["单缝出现时间日3"].ToString() == "" ? "" : row["单缝出现时间日3"].ToString() + "日");
            //disaster.单缝出现时间3 = row["单缝出现时间3"].ToString();
            disaster.单缝活动性3 = row["单缝活动性3"].ToString();
            disaster.群缝缝数 = (int)(float.Parse(row["群缝缝数"].ToString() == "" ? "0" : row["群缝缝数"].ToString()));
            disaster.群缝分布面积 = float.Parse(row["群缝分布面积"].ToString() == "" ? "0" : row["群缝分布面积"].ToString());
            disaster.群缝发育间距 = float.Parse(row["群缝发育间距"].ToString() == "" ? "0" : row["群缝发育间距"].ToString());
            disaster.群缝排列形式 = row["群缝排列形式"].ToString();
            disaster.裂缝长度max = float.Parse(row["裂缝长度max"].ToString() == "" ? "0" : row["裂缝长度max"].ToString());
            disaster.裂缝长度min = float.Parse(row["裂缝长度min"].ToString() == "" ? "0" : row["裂缝长度min"].ToString());
            disaster.裂缝宽度max = float.Parse(row["裂缝宽度max"].ToString() == "" ? "0" : row["裂缝长度min"].ToString());
            disaster.裂缝宽度min = float.Parse(row["裂缝宽度min"].ToString() == "" ? "0" : row["裂缝宽度min"].ToString());
            disaster.裂缝深度max = float.Parse(row["裂缝深度max"].ToString() == "" ? "0" : row["裂缝深度max"].ToString());
            disaster.裂缝深度min = float.Parse(row["裂缝深度min"].ToString() == "" ? "0" : row["裂缝深度min"].ToString());
            //landFracture.始发时间 = row["始发时间年"].ToString() + (row["始发时间月"].ToString() == "" ? "" : "年" + row["始发时间月"].ToString() + "月") + (row["始发时间日"].ToString() == "" ? "" : row["始发时间日"].ToString() + "日");
            //landFracture.盛发开始时间 = row["盛发开始时间年"].ToString() + (row["盛发开始时间月"].ToString() == "" ? "" : "年" + row["盛发开始时间月"].ToString() + "月") + (row["盛发开始时间日"].ToString() == "" ? "" : row["盛发开始时间日"].ToString() + "日");
            //landFracture.盛发截止时间 = row["盛发截止时间年"].ToString() + (row["盛发截止时间月"].ToString() == "" ? "" : "年" + row["盛发截止时间月"].ToString() + "月") + (row["盛发截止时间日"].ToString() == "" ? "" : row["盛发截止时间日"].ToString() + "日");
            //landFracture.停止时间 = row["停止时间年"].ToString() + (row["停止时间月"].ToString() == "" ? "" : "年" + row["停止时间月"].ToString() + "月") + (row["停止时间日"].ToString() == "" ? "" : row["停止时间日"].ToString() + "日");

            //disaster.始发时间 = row["始发时间"].ToString();
            //disaster.盛发开始时间 = row["盛发开始时间"].ToString();
            //disaster.盛发截止时间 = row["盛发截止时间"].ToString();
            //disaster.停止时间 = row["停止时间"].ToString();

            disaster.目前发展情况 = row["目前发展情况"].ToString();
            disaster.成因类型 = row["成因类型"].ToString();
            disaster.裂缝区地貌特征 = row["裂缝区地貌特征"].ToString();
            disaster.裂缝与地貌走向关系 = row["裂缝与地貌走向关系"].ToString();
            disaster.裂缝巨岩土层时代 = row["裂缝巨岩土层时代"].ToString();
            disaster.裂缝巨岩土层岩性 = row["裂缝巨岩土层岩性"].ToString();
            disaster.受裂土层时间 = row["受裂土层时间"].ToString();
            disaster.受裂土层土性 = row["受裂土层土性"].ToString();
            disaster.受裂土下伏层时间 = row["受裂土下伏层时间"].ToString();
            disaster.受裂土下伏层岩性 = row["受裂土下伏层岩性"].ToString();
            disaster.受裂岩土层时代 = row["受裂岩土层时代"].ToString();
            disaster.受裂岩土层岩性 = row["受裂岩土层岩性"].ToString();
            disaster.胀缩土特征 = row["胀缩土特征"].ToString();
            disaster.胀缩土膨胀性 = row["胀缩土膨胀性"].ToString();
            disaster.胀缩土含水量 = float.Parse(row["胀缩土含水量"].ToString() == "" ? "0" : row["胀缩土含水量"].ToString());
            disaster.裂缝区构造断裂走向1 = row["裂缝区构造断裂走向1"].ToString();
            disaster.裂缝区构造断裂倾向1 = (int)(float.Parse(row["裂缝区构造断裂倾向1"].ToString() == "" ? "0" : row["裂缝区构造断裂倾向1"].ToString()));
            disaster.裂缝区构造断裂倾角1 = (int)(float.Parse(row["裂缝区构造断裂倾角1"].ToString() == "" ? "0" : row["裂缝区构造断裂倾角1"].ToString()));
            disaster.裂缝区构造断裂走向2 = row["裂缝区构造断裂走向2"].ToString();
            disaster.裂缝区构造断裂倾向2 = (int)(float.Parse(row["裂缝区构造断裂倾向2"].ToString() == "" ? "0" : row["裂缝区构造断裂倾向2"].ToString()));
            disaster.裂缝区构造断裂倾角2 = (int)(float.Parse(row["裂缝区构造断裂倾角2"].ToString() == "" ? "0" : row["裂缝区构造断裂倾角2"].ToString()));
            disaster.岩层中断裂倾向 = (int)(float.Parse(row["岩层中断裂倾向"].ToString() == "" ? "0" : row["岩层中断裂倾向"].ToString()));
            disaster.岩层中断裂倾角 = (int)(float.Parse(row["岩层中断裂倾角"].ToString() == "" ? "0" : row["岩层中断裂倾角"].ToString()));
            disaster.土层中有无新断裂 = bool.Parse(row["土层中有无新断裂"].ToString() == "" ? "false" : row["土层中有无新断裂"].ToString());
            disaster.土层中新断裂倾向 = (int)(float.Parse(row["土层中新断裂倾向"].ToString() == "" ? "0" : row["土层中新断裂倾向"].ToString()));
            disaster.土层中新断裂倾角 = (int)(float.Parse(row["土层中新断裂倾角"].ToString() == "" ? "0" : row["土层中新断裂倾角"].ToString()));
            disaster.主要构造断裂走向1 = row["主要构造断裂走向1"].ToString();
            disaster.主要构造断裂倾向1 = (int)(float.Parse(row["主要构造断裂倾向1"].ToString() == "" ? "0" : row["主要构造断裂倾向1"].ToString()));
            disaster.主要构造断裂倾角1 = (int)(float.Parse(row["主要构造断裂倾角1"].ToString() == "" ? "0" : row["主要构造断裂倾角1"].ToString()));
            disaster.主要构造断裂走向2 = row["主要构造断裂走向2"].ToString();
            disaster.主要构造断裂倾向2 = (int)(float.Parse(row["主要构造断裂倾向2"].ToString() == "" ? "0" : row["主要构造断裂倾向2"].ToString()));
            disaster.主要构造断裂倾角2 = (int)(float.Parse(row["主要构造断裂倾角2"].ToString() == "" ? "0" : row["主要构造断裂倾角2"].ToString()));
            disaster.胀缩土中有无新断裂 = bool.Parse(row["胀缩土中有无新断裂"].ToString() == "" ? "false" : row["胀缩土中有无新断裂"].ToString());
            disaster.胀缩土中新断裂倾向 = (int)(float.Parse(row["胀缩土中新断裂倾向"].ToString() == "" ? "0" : row["胀缩土中新断裂倾向"].ToString()));
            disaster.胀缩土中新断裂倾角 = (int)(float.Parse(row["胀缩土中新断裂倾角"].ToString() == "" ? "0" : row["胀缩土中新断裂倾角"].ToString()));
            disaster.洞室埋深 = float.Parse(row["洞室埋深"].ToString() == "" ? "0" : row["洞室埋深"].ToString());
            disaster.洞室规模 = row["洞室规模"].ToString();
            disaster.洞室长 = float.Parse(row["洞室长"].ToString() == "" ? "0" : row["洞室长"].ToString());
            disaster.洞室宽 = float.Parse(row["洞室宽"].ToString() == "" ? "0" : row["洞室宽"].ToString());
            disaster.洞室高 = float.Parse(row["洞室高"].ToString() == "" ? "0" : row["洞室高"].ToString());
            disaster.洞室与裂缝区位置关系 = row["洞室与裂缝区位置关系"].ToString();
            //landFracture.洞室开挖时间 = row["洞室开挖时间年"].ToString() + (row["洞室开挖时间月"].ToString() == "" ? "" : "年" + row["洞室开挖时间月"].ToString() + "月") + (row["洞室开挖时间日"].ToString() == "" ? "" : row["洞室开挖时间日"].ToString() + "日");
            //disaster.洞室开挖时间 = row["洞室开挖时间"].ToString();
            disaster.洞室开挖方式 = row["洞室开挖方式"].ToString();
            disaster.洞室开挖强度 = row["洞室开挖强度"].ToString();
            disaster.抽排地下水类型 = row["抽排地下水类型"].ToString();
            disaster.抽排井埋深 = float.Parse(row["抽排井埋深"].ToString() == "" ? "0" : row["抽排井埋深"].ToString());
            //disaster.抽排水位水量 = float.Parse(row["抽排水位水量"].ToString() == "" ? "0" : row["抽排水位水量"].ToString());
            disaster.抽排日出水量 = float.Parse(row["抽排日出水量"].ToString() == "" ? "0" : row["抽排日出水量"].ToString());
            //landFracture.抽排水开始时间 = row["抽排水开始时间年"].ToString() + (row["抽排水开始时间月"].ToString() == "" ? "" : "年" + row["抽排水开始时间月"].ToString() + "月") + (row["抽排水开始时间日"].ToString() == "" ? "" : row["抽排水开始时间日"].ToString() + "日");
            //landFracture.抽排水停止时间 = row["抽排水停止时间年"].ToString() + (row["抽排水停止时间月"].ToString() == "" ? "" : "年" + row["抽排水停止时间月"].ToString() + "月") + (row["抽排水停止时间日"].ToString() == "" ? "" : row["抽排水停止时间日"].ToString() + "日");

            //disaster.抽排水开始时间 = row["抽排水开始时间"].ToString();
            //disaster.抽排水停止时间 = row["抽排水停止时间"].ToString();

            disaster.抽排水状态 = bool.Parse(row["抽排水状态"].ToString() == "" ? "false" : row["抽排水状态"].ToString());
            disaster.地震烈度 = row["地震烈度"].ToString();
            //landFracture.地震发生时间 = row["地震发生时间年"].ToString() + (row["地震发生时间月"].ToString() == "" ? "" : "年" + row["地震发生时间月"].ToString() + "月") + (row["地震发生时间日"].ToString() == "" ? "" : row["地震发生时间日"].ToString() + "日");
            //disaster.地震发生时间 = row["地震发生时间"].ToString();
            disaster.活动断层位置 = row["活动断层位置"].ToString();
            disaster.活动断层倾向 = (int)(float.Parse(row["活动断层倾向"].ToString() == "" ? "0" : row["活动断层倾向"].ToString()));
            disaster.活动断层倾角 = (int)(float.Parse(row["活动断层倾角"].ToString() == "" ? "0" : row["活动断层倾角"].ToString()));
            disaster.活动断层长度 = float.Parse(row["活动断层长度"].ToString() == "" ? "0" : row["活动断层长度"].ToString());
            disaster.活动断层性质 = row["活动断层性质"].ToString();
            //landFracture.活动断层活动时间 = row["活动断层活动时间年"].ToString() + (row["活动断层活动时间月"].ToString() == "" ? "" : "年" + row["活动断层活动时间月"].ToString() + "月") + (row["活动断层活动时间日"].ToString() == "" ? "" : row["活动断层活动时间日"].ToString() + "日");
            //disaster.活动断层活动时间 = row["活动断层活动时间"].ToString();
            disaster.活动断层活动速率 = ConvertHelper.GetFloatValutFromStr(row["活动断层活动速率"].ToString());
            disaster.活动断层断距 = ConvertHelper.GetFloatValutFromStr(row["活动断层断距"].ToString());
            disaster.水理作用水源 = row["水理作用水源"].ToString();
            //landFracture.水理作用时间 = row["水理作用时间年"].ToString() + (row["水理作用时间月"].ToString() == "" ? "" : "年" + row["水理作用开挖时间月"].ToString() + "月") + (row["水理作用开挖时间日"].ToString() == "" ? "" : row["水理作用开挖时间日"].ToString() + "日");
            //disaster.水理作用时间 = row["水理作用时间"].ToString();
            disaster.水理作用水质 = ConvertHelper.GetFloatValutFromStr(row["水理作用水质"].ToString());
            disaster.水理作用类型 = row["水理作用类型"].ToString();
            //landFracture.水理作用开挖时间 = row["水理作用开挖时间年"].ToString() + (row["水理作用开挖时间月"].ToString() == "" ? "" : "年" + row["活动断层活动时间月"].ToString() + "月") + (row["活动断层活动时间日"].ToString() == "" ? "" : row["活动断层活动时间日"].ToString() + "日");
            //disaster.水理作用开挖时间 = row["水理作用开挖时间"].ToString();
            disaster.水理作用开挖方式 = row["水理作用开挖方式"].ToString();
            disaster.水理作用开挖深度 = float.Parse(row["水理作用开挖深度"].ToString() == "" ? "0" : row["水理作用开挖深度"].ToString());
            disaster.毁坏房屋 = float.Parse(row["毁坏房屋"].ToString() == "" ? "0" : row["毁坏房屋"].ToString());
            disaster.阻断交通 = ConvertHelper.GetFloatValutFromStr(row["阻断交通"].ToString());
            disaster.隐患点 = bool.Parse(row["隐患点"].ToString() == "" ? "false" : row["隐患点"].ToString());
            disaster.威胁房屋 = float.Parse(row["威胁房屋"].ToString() == "" ? "0" : row["威胁房屋"].ToString());
            disaster.交通隐患 = ConvertHelper.GetFloatValutFromStr(row["交通隐患"].ToString());
            disaster.发展预测 = row["发展预测"].ToString();
            disaster.防灾预案 = bool.Parse(row["防灾预案"].ToString() == "" ? "false" : row["防灾预案"].ToString());
            disaster.多媒体 = bool.Parse(row["多媒体"].ToString() == "" ? "false" : row["多媒体"].ToString());
            disaster.防治措施及效果 = row["防治措施及效果"].ToString();
            disaster.防治建议 = row["防治建议"].ToString();
            disaster.调查负责人 = row["调查负责人"].ToString();
            disaster.填表人 = row["填表人"].ToString();
            disaster.审核人 = row["审核人"].ToString();
            disaster.调查单位 = row["调查单位"].ToString();
            //landFracture.填表日期 = row["填表日期年"].ToString() + (row["填表日期月"].ToString() == "" ? "" : "年" + row["填表日期月"].ToString() + "月") + (row["填表日期日"].ToString() == "" ? "" : row["填表日期日"].ToString() + "日");
            //disaster.填表日期 = row["填表日期"].ToString();
            disaster.抽排水位置关系 = row["抽排水位置关系"].ToString();
            disaster.平面示意图 = ConvertHelper.GetBooleanByStr(row["平面示意图"].ToString());
            disaster.剖面示意图 = ConvertHelper.GetBooleanByStr(row["剖面示意图"].ToString());
            //landFracture.省名 = row["省"].ToString();
            //landFracture.县名 = row["县"].ToString();
            //landFracture.街道 = row["省"].ToString() + row["县"].ToString() + row["乡"].ToString() + row["村"].ToString() + row["组"].ToString();
            //landFracture.平面示意图路径 = row["平面示意图"].ToString();
            //landFracture.剖面示意图路径 = row["剖面示意图"].ToString();

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
            disaster.单缝出现时间年1 = ConvertHelper.GetIntValueByStr(row["单缝出现时间年1"].ToString());
            disaster.单缝出现时间月1 = ConvertHelper.GetIntValueByStr(row["单缝出现时间月1"].ToString());
            disaster.单缝出现时间日1 = ConvertHelper.GetIntValueByStr(row["单缝出现时间日1"].ToString());
            disaster.单缝出现时间年2 = ConvertHelper.GetIntValueByStr(row["单缝出现时间年2"].ToString());
            disaster.单缝出现时间月2 = ConvertHelper.GetIntValueByStr(row["单缝出现时间月2"].ToString());
            disaster.单缝出现时间日2 = ConvertHelper.GetIntValueByStr(row["单缝出现时间日2"].ToString());
            disaster.单缝出现时间年3 = ConvertHelper.GetIntValueByStr(row["单缝出现时间年3"].ToString());
            disaster.单缝出现时间月3 = ConvertHelper.GetIntValueByStr(row["单缝出现时间月3"].ToString());
            disaster.单缝出现时间日3 = ConvertHelper.GetIntValueByStr(row["单缝出现时间日3"].ToString());
            disaster.群缝平行排列倾向 = ConvertHelper.GetIntValueByStr(row["群缝平行排列倾向"].ToString());
            disaster.群缝平行排列倾角 = ConvertHelper.GetIntValueByStr(row["群缝平行排列倾角"].ToString());
            disaster.群缝平行排列阶步指向 = ConvertHelper.GetIntValueByStr(row["群缝平行排列阶步指向"].ToString());
            disaster.群缝斜列排列倾向 = ConvertHelper.GetIntValueByStr(row["群缝斜列排列倾向"].ToString());
            disaster.群缝斜列排列倾角 = ConvertHelper.GetIntValueByStr(row["群缝斜列排列倾角"].ToString());
            disaster.群缝斜列排列阶步指向 = ConvertHelper.GetIntValueByStr(row["群缝斜列排列阶步指向"].ToString());
            disaster.群缝环围排列圆心位置经度 = row["群缝环围排列圆心位置经度"].ToString();
            disaster.群缝环围排列圆心位置纬度 = row["群缝环围排列圆心位置纬度"].ToString();
            disaster.始发时间年 = ConvertHelper.GetIntValueByStr(row["始发时间年"].ToString());
            disaster.始发时间月 = ConvertHelper.GetIntValueByStr(row["始发时间月"].ToString());
            disaster.始发时间日 = ConvertHelper.GetIntValueByStr(row["始发时间日"].ToString());
            disaster.盛发开始时间年 = ConvertHelper.GetIntValueByStr(row["盛发开始时间年"].ToString());
            disaster.盛发开始时间月 = ConvertHelper.GetIntValueByStr(row["盛发开始时间月"].ToString());
            disaster.盛发开始时间日 = ConvertHelper.GetIntValueByStr(row["盛发开始时间日"].ToString());
            disaster.盛发截止时间年 = ConvertHelper.GetIntValueByStr(row["盛发截止时间年"].ToString());
            disaster.盛发截止时间月 = ConvertHelper.GetIntValueByStr(row["盛发截止时间月"].ToString());
            disaster.盛发截止时间日 = ConvertHelper.GetIntValueByStr(row["盛发截止时间日"].ToString());
            disaster.停止时间年 = ConvertHelper.GetIntValueByStr(row["停止时间年"].ToString());
            disaster.停止时间月 = ConvertHelper.GetIntValueByStr(row["停止时间月"].ToString());
            disaster.停止时间日 = ConvertHelper.GetIntValueByStr(row["停止时间日"].ToString());
            disaster.引发动力因素 = row["引发动力因素"].ToString();
            disaster.洞室开挖时间年 = ConvertHelper.GetIntValueByStr(row["洞室开挖时间年"].ToString());
            disaster.洞室开挖时间月 = ConvertHelper.GetIntValueByStr(row["洞室开挖时间月"].ToString());
            disaster.洞室开挖时间日 = ConvertHelper.GetIntValueByStr(row["洞室开挖时间日"].ToString());
            disaster.抽排水位 = ConvertHelper.GetFloatValutFromStr(row["抽排水位"].ToString());
            disaster.抽排水量 = ConvertHelper.GetFloatValutFromStr(row["抽排水量"].ToString());
            disaster.抽排水开始时间年 = ConvertHelper.GetIntValueByStr(row["抽排水开始时间年"].ToString());
            disaster.抽排水开始时间月 = ConvertHelper.GetIntValueByStr(row["抽排水开始时间月"].ToString());
            disaster.抽排水开始时间日 = ConvertHelper.GetIntValueByStr(row["抽排水开始时间日"].ToString());
            disaster.抽排水停止时间年 = ConvertHelper.GetIntValueByStr(row["抽排水停止时间年"].ToString());
            disaster.抽排水停止时间月 = ConvertHelper.GetIntValueByStr(row["抽排水停止时间月"].ToString());
            disaster.抽排水停止时间日 = ConvertHelper.GetIntValueByStr(row["抽排水停止时间日"].ToString());
            disaster.地震发生时间年 = ConvertHelper.GetIntValueByStr(row["地震发生时间年"].ToString());
            disaster.地震发生时间月 = ConvertHelper.GetIntValueByStr(row["地震发生时间月"].ToString());
            disaster.地震发生时间日 = ConvertHelper.GetIntValueByStr(row["地震发生时间日"].ToString());
            disaster.断层活动 = ConvertHelper.GetBooleanByStr(row["断层活动"].ToString());
            disaster.活动断层活动时间年 = ConvertHelper.GetIntValueByStr(row["活动断层活动时间年"].ToString());
            disaster.活动断层活动时间月 = ConvertHelper.GetIntValueByStr(row["活动断层活动时间月"].ToString());
            disaster.活动断层活动时间日 = ConvertHelper.GetIntValueByStr(row["活动断层活动时间日"].ToString());
            disaster.水理作用时间年 = ConvertHelper.GetIntValueByStr(row["水理作用时间年"].ToString());
            disaster.水理作用时间月 = ConvertHelper.GetIntValueByStr(row["水理作用时间月"].ToString());
            disaster.水理作用时间日 = ConvertHelper.GetIntValueByStr(row["水理作用时间日"].ToString());
            disaster.开挖卸荷作用 = ConvertHelper.GetBooleanByStr(row["开挖卸荷作用"].ToString());
            disaster.其它作用引起的干湿变化 = ConvertHelper.GetBooleanByStr(row["其它作用引起的干湿变化"].ToString());
            disaster.水理作用开挖时间年 = ConvertHelper.GetIntValueByStr(row["水理作用开挖时间年"].ToString());
            disaster.水理作用开挖时间月 = ConvertHelper.GetIntValueByStr(row["水理作用开挖时间月"].ToString());
            disaster.水理作用开挖时间日 = ConvertHelper.GetIntValueByStr(row["水理作用开挖时间日"].ToString());
            disaster.小时 = ConvertHelper.GetIntValueByStr(row["小时"].ToString());
            disaster.危害对象 = row["危害对象"].ToString();
            disaster.威胁对象 = row["威胁对象"].ToString();
            disaster.遥感点 = ConvertHelper.GetBooleanByStr(row["遥感点"].ToString());
            disaster.勘查点 = ConvertHelper.GetBooleanByStr(row["勘查点"].ToString());
            disaster.测绘点 = ConvertHelper.GetBooleanByStr(row["测绘点"].ToString());
            disaster.录像 = ConvertHelper.GetBooleanByStr(row["录像"].ToString());
            disaster.矢量平面图 = ConvertHelper.GetBooleanByStr(row["矢量平面图"].ToString());
            disaster.矢量剖面图 = ConvertHelper.GetBooleanByStr(row["矢量剖面图"].ToString());
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
