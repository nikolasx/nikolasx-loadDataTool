using System;
using System.Data;
using System.Text;
using NikolasHelper.GIS;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Investigation;

namespace 隐患点主表导入工具.Investigation
{
    /// <summary>
    /// 导入综合表数据
    /// </summary>
    public class LoadComprehensive
    {


        /// <summary>
        /// 插入十万调查信息
        /// </summary>
        /// <param name="fileMdbPath">主表所在的Access文件路径</param>
        /// <param name="fileComPath">综合表所在的Excel文件路径</param>
        /// <returns></returns>
        public string InsertBaseDisasterInfo(string fileMdbPath, string fileComPath)
        {
            string result = "十万调查导入结果\n\n";

            //崩塌表信息
            LoadLandSlip loadLandSlip = new LoadLandSlip();
            result += loadLandSlip.InsertData(fileMdbPath, fileComPath);

            //滑坡
            LoadLandSlide loadLandSlide = new LoadLandSlide();
            result += loadLandSlide.InsertData(fileMdbPath, fileComPath);

            //泥石流
            LoadDebrisFlow loadDebrisFlow = new LoadDebrisFlow();
            result += loadDebrisFlow.InsertData(fileMdbPath, fileComPath);

            //斜坡
            LoadSlope loadSlope = new LoadSlope();
            result += loadSlope.InsertData(fileMdbPath, fileComPath);

            //地裂缝
            LoadLandFracture loadLandFracture = new LoadLandFracture();
            result += loadLandFracture.InsertData(fileMdbPath, fileComPath);

            //地面塌陷
            LoadLandCollapse loadLandCollapse = new LoadLandCollapse();
            result += loadLandCollapse.InsertData(fileMdbPath, fileComPath);

            //地面沉降
            LoadLandSubsidence loadLandSubsidence = new LoadLandSubsidence();
            result += loadLandSubsidence.InsertData(fileMdbPath, fileComPath);

            return result;
        }


        /// <summary>
        /// 根据综合表Excel文件，获取综合表主要信息
        /// </summary>
        public Comprehensive GetComprehensive(DataRow row, DataColumnCollection columns)
        {
            Comprehensive comp = new Comprehensive();

            comp.CustomizeId = row["隐患点编号"].ToString();

            comp.统一编号 = row["统一编号"].ToString();
            comp.灾害类型 = ConvertHelper.GetEnumGeoDisasterByStr(row["灾害类型"].ToString());
            comp.GBCodeId = row["统一编号"].ToString().Substring(0, 6);
            comp.地理位置 = row["地理位置"].ToString();
            comp.名称 = row["名称"].ToString();
            comp.经度 = LonLatHelper.ConvertToDegreeStyleFromString(row["经度"].ToString());
            comp.纬度 = LonLatHelper.ConvertToDegreeStyleFromString(row["纬度"].ToString());
            comp.死亡人数 = ConvertHelper.GetIntValueByStr(row["死亡人数"].ToString());
            comp.威胁人口 = ConvertHelper.GetIntValueByStr(row["威胁人口"].ToString());
            comp.直接经济损失 = ConvertHelper.GetDoubleValueFromStr(row["直接经济损失"].ToString());
            comp.威胁财产 = ConvertHelper.GetDoubleValueFromStr(row["威胁财产"].ToString());
            comp.灾情等级 = row["灾情等级"].ToString();
            comp.险情等级 = row["险情等级"].ToString();
            comp.X坐标 = ConvertHelper.GetDoubleValueFromStr(row["X坐标"].ToString());
            comp.Y坐标 = ConvertHelper.GetDoubleValueFromStr(row["Y坐标"].ToString());
            comp.灾害体积 = ConvertHelper.GetDoubleValueFromStr(row["灾害体积"].ToString());  //
            comp.灾害面积 = 0;  //


            return comp;
        }

    }
}
