using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Investigation;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Relocation;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.Investigation
{
    public class LoadLandSubsidence
    {


        public virtual string InsertData(string filePath)
        {
            DataTable landTable = AccessHelper.GetDataTableByAccessFile(filePath, "地面沉降主表");
            DataTable comTable = AccessHelper.GetDataTableByAccessFile(filePath, "综合表");
            LoadComprehensive loadCom = new LoadComprehensive();
            ComprehensiveService compService = new ComprehensiveService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < landTable.Rows.Count; i++)
            {
                try
                {
                    LandSubsidence landSubsidence = GetLandSubsidence(landTable.Rows[i], landTable.Columns);
                    string uId = landTable.Rows[i]["统一编号"].ToString();
                    DataRow[] rows = comTable.Select("统一编号=" + uId);
                    if (rows.Length > 1 || rows.Length < 1)
                        throw new Exception(@"不存在综合表或综合表不唯一");

                    Comprehensive comp = loadCom.GetComprehensive(rows[0], comTable.Columns);
                    comp.LandSubsidence = landSubsidence;
                    compService.InsertComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，统一编号：" + landTable.Rows[i]["统一编号"] + ";失败原因是：" + ex.Message + "\n");
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("地面沉降", landTable.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }




        public LandSubsidence GetLandSubsidence(DataRow row, DataColumnCollection columns)
        {
            LandSubsidence landSubsidence = new LandSubsidence();

            #region  LandSubsidence 地面沉降
            landSubsidence.发生时间 = row["发生时间"].ToString();
            landSubsidence.野外编号 = row["野外编号"].ToString();
            landSubsidence.室内编号 = row["室内编号"].ToString();
            landSubsidence.沉降类型 = row["沉降类型"].ToString();
            landSubsidence.沉降中心位置 = row["沉降中心位置"].ToString();
            landSubsidence.沉降中心经度 = row["沉降中心经度"].ToString();
            landSubsidence.沉降中心纬度 = row["沉降中心纬度"].ToString();
            landSubsidence.沉降区面积 = ConvertHelper.GetDoubleValueFromStr(row["沉降区面积"].ToString());
            landSubsidence.年平均沉降量 = ConvertHelper.GetDoubleValueFromStr(row["年平均沉降量"].ToString());
            landSubsidence.历年累计沉降量 = ConvertHelper.GetDoubleValueFromStr(row["历年累计沉降量"].ToString());
            landSubsidence.平均沉降速率 = ConvertHelper.GetDoubleValueFromStr(row["平均沉降速率"].ToString());
            landSubsidence.地形地貌 = row["地形地貌"].ToString();
            landSubsidence.地质构造及活动情况 = row["地质构造及活动情况"].ToString();
            landSubsidence.岩性 = row["岩性"].ToString();
            landSubsidence.厚度 = row["厚度"].ToString();
            landSubsidence.结构 = row["结构"].ToString();
            landSubsidence.空间变化规律 = row["空间变化规律"].ToString();
            landSubsidence.水文地质特征 = row["水文地质特征"].ToString();
            landSubsidence.主要沉降层位 = row["主要沉降层位"].ToString();
            landSubsidence.年开采量 = ConvertHelper.GetDoubleValueFromStr(row["年开采量"].ToString());
            landSubsidence.年补给量 = ConvertHelper.GetDoubleValueFromStr(row["年补给量"].ToString());
            landSubsidence.地下水埋深 = ConvertHelper.GetDoubleValueFromStr(row["地下水埋深"].ToString());
            landSubsidence.年水位变化幅度 = ConvertHelper.GetDoubleValueFromStr(row["年水位变化幅度"].ToString());
            landSubsidence.其它 = row["其它"].ToString();
            landSubsidence.诱发沉降原因 = row["诱发沉降原因"].ToString();
            landSubsidence.变化规律 = row["变化规律"].ToString();
            landSubsidence.沉降现状 = row["沉降现状"].ToString();
            landSubsidence.发展趋势 = row["发展趋势"].ToString();
            landSubsidence.主要危害 = row["主要危害"].ToString();
            landSubsidence.经济损失 = ConvertHelper.GetDoubleValueFromStr(row["经济损失"].ToString());
            landSubsidence.治理措施 = row["治理措施"].ToString();
            landSubsidence.治理效果 = row["治理效果"].ToString();
            landSubsidence.调查负责人 = row["调查负责人"].ToString();
            landSubsidence.填表人 = row["填表人"].ToString();
            landSubsidence.审核人 = row["审核人"].ToString();
            landSubsidence.调查单位 = row["调查单位"].ToString();
            landSubsidence.填表日期 = row["填表日期"].ToString();
            //landSubsidence.省名 = row["省名"].ToString();
            //landSubsidence.县名 = row["县名"].ToString();
            //landSubsidence.街道 = row["街道"].ToString();
            //landSubsidence.平面示意图路径 = row["平面示意图路径"].ToString();
            //landSubsidence.剖面示意图路径 = row["剖面示意图路径"].ToString();
            #endregion

            return landSubsidence;
        }
    }
}
