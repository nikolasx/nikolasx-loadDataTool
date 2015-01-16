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
    /// 地面沉降
    /// </summary>
    public class LoadLandSubsidenceFS
    {



        public virtual string InsertData(string filePath)
        {
            DataTable landTable = AccessHelper.GetDataTableByAccessFile(filePath, "地面沉降主表");
            DataTable comTable = AccessHelper.GetDataTableByAccessFile(filePath, "汇总表");
            LoadComprehensiveFS loadCom = new LoadComprehensiveFS();
            ComprehensiveFSService compService = new ComprehensiveFSService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < landTable.Rows.Count; i++)
            {
                try
                {
                    LandSubsidenceFS landSlip = GetLandSubsidenceFS(landTable.Rows[i], landTable.Columns);
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
                    comp.LandSubsidenceFS = landSlip;
                    compService.InsertComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，统一编号是：" + landTable.Rows[i]["统一编号"] + ";失败原因是：" + ex.Message + "\n");
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("地面沉降表(5W)", landTable.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }


        public LandSubsidenceFS GetLandSubsidenceFS(DataRow row, DataColumnCollection columns)
        {
            LandSubsidenceFS disaster = new LandSubsidenceFS();

            #region  LandSubsidence 地面沉降
            //disaster.发生时间 = row["发生时间"].ToString();
            disaster.野外编号 = row["野外编号"].ToString();
            disaster.室内编号 = row["室内编号"].ToString();
            disaster.沉降类型 = row["沉降类型"].ToString();
            disaster.沉降中心位置 = row["沉降中心位置"].ToString();
            disaster.沉降中心经度 = row["沉降中心经度"].ToString();
            disaster.沉降中心纬度 = row["沉降中心纬度"].ToString();
            disaster.沉降区面积 = ConvertHelper.GetDoubleValueFromStr(row["沉降区面积"].ToString());
            disaster.年平均沉降量 = ConvertHelper.GetDoubleValueFromStr(row["年平均沉降量"].ToString());
            disaster.历年累计沉降量 = ConvertHelper.GetDoubleValueFromStr(row["历年累计沉降量"].ToString());
            disaster.平均沉降速率 = ConvertHelper.GetDoubleValueFromStr(row["平均沉降速率"].ToString());
            disaster.地形地貌 = row["地形地貌"].ToString();
            disaster.地质构造及活动情况 = row["地质构造及活动情况"].ToString();
            disaster.岩性 = row["岩性"].ToString();
            disaster.厚度 = row["厚度"].ToString();
            disaster.结构 = row["结构"].ToString();
            disaster.空间变化规律 = row["空间变化规律"].ToString();
            disaster.水文地质特征 = row["水文地质特征"].ToString();
            disaster.主要沉降层位 = row["主要沉降层位"].ToString();
            disaster.年开采量 = ConvertHelper.GetDoubleValueFromStr(row["年开采量"].ToString());
            disaster.年补给量 = ConvertHelper.GetDoubleValueFromStr(row["年补给量"].ToString());
            disaster.地下水埋深 = ConvertHelper.GetDoubleValueFromStr(row["地下水埋深"].ToString());
            disaster.年水位变化幅度 = ConvertHelper.GetDoubleValueFromStr(row["年水位变化幅度"].ToString());
            disaster.其它 = row["其它"].ToString();
            disaster.诱发沉降原因 = row["诱发沉降原因"].ToString();
            disaster.变化规律 = row["变化规律"].ToString();
            disaster.沉降现状 = row["沉降现状"].ToString();
            disaster.发展趋势 = row["发展趋势"].ToString();
            disaster.主要危害 = row["主要危害"].ToString();
            disaster.经济损失 = ConvertHelper.GetDoubleValueFromStr(row["经济损失"].ToString());
            disaster.治理措施 = row["治理措施"].ToString();
            disaster.治理效果 = row["治理效果"].ToString();
            disaster.调查负责人 = row["调查负责人"].ToString();
            disaster.填表人 = row["填表人"].ToString();
            disaster.审核人 = row["审核人"].ToString();
            disaster.调查单位 = row["调查单位"].ToString();
            //disaster.填表日期 = row["填表日期"].ToString();
            //landSubsidence.省名 = row["省名"].ToString();
            //landSubsidence.县名 = row["县名"].ToString();
            //landSubsidence.街道 = row["街道"].ToString();
            //landSubsidence.平面示意图路径 = row["平面示意图路径"].ToString();
            //landSubsidence.剖面示意图路径 = row["剖面示意图路径"].ToString();


            //新增字段
            disaster.项目名称 = row["项目名称"].ToString();
            disaster.图幅名 = row["图幅名"].ToString();
            disaster.图幅编号 = row["图幅编号"].ToString();
            disaster.县市编号 = row["县市编号"].ToString();
            disaster.发生时间年 = ConvertHelper.GetIntValueByStr(row["发生时间年"].ToString());
            disaster.发生时间月 = ConvertHelper.GetIntValueByStr(row["发生时间月"].ToString());
            disaster.发生时间日 = ConvertHelper.GetIntValueByStr(row["发生时间日"].ToString());
            disaster.省 = row["省"].ToString();
            disaster.市 = row["市"].ToString();
            disaster.县 = row["县"].ToString();
            disaster.乡 = row["乡"].ToString();
            disaster.村 = row["村"].ToString();
            disaster.组 = row["组"].ToString();
            disaster.地点 = row["地点"].ToString();
            disaster.死亡人数 = ConvertHelper.GetIntValueByStr(row["死亡人数"].ToString());
            disaster.直接损失 = ConvertHelper.GetDoubleValueFromStr(row["直接损失"].ToString());
            disaster.危害对象 = row["危害对象"].ToString();
            disaster.威胁人数 = ConvertHelper.GetIntValueByStr(row["威胁人数"].ToString());
            disaster.威胁财产 = ConvertHelper.GetDoubleValueFromStr(row["威胁财产"].ToString());
            disaster.威胁对象 = row["威胁对象"].ToString();
            disaster.填表日期年 = ConvertHelper.GetIntValueByStr(row["填表日期年"].ToString());
            disaster.填表日期月 = ConvertHelper.GetIntValueByStr(row["填表日期月"].ToString());
            disaster.填表日期日 = ConvertHelper.GetIntValueByStr(row["填表日期日"].ToString());
            disaster.遥感点 = ConvertHelper.GetBooleanByStr(row["遥感点"].ToString());
            disaster.勘查点 = ConvertHelper.GetBooleanByStr(row["勘查点"].ToString());
            disaster.测绘点 = ConvertHelper.GetBooleanByStr(row["测绘点"].ToString());
            disaster.防灾预案 = ConvertHelper.GetBooleanByStr(row["防灾预案"].ToString());
            disaster.照片 = ConvertHelper.GetBooleanByStr(row["照片"].ToString());
            disaster.录像 = ConvertHelper.GetBooleanByStr(row["录像"].ToString());
            disaster.威胁房屋户 = ConvertHelper.GetIntValueByStr(row["威胁房屋户"].ToString());
            disaster.隐患点 = ConvertHelper.GetBooleanByStr(row["隐患点"].ToString());

            #endregion

            return disaster;
        }
    }
}
