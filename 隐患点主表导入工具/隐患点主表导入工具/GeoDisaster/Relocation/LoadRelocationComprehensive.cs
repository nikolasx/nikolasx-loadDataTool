using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikolasHelper.GIS;
using NikolasHelper.Util;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Investigation;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Relocation;
using 隐患点主表导入工具.Investigation;

namespace 隐患点主表导入工具.Relocation
{
    /// <summary>
    /// 移民搬迁基础信息的导入
    /// </summary>
    public class LoadRelocationComprehensive
    {




        /// <summary>
        /// 插入十万移民搬迁信息
        /// </summary>
        /// <param name="fileMdbPath">主表所在的Access文件路径</param>
        /// <param name="fileComPath">综合表所在的Excel文件路径</param>
        /// <returns></returns>
        public string InsertBaseRelocationInfo(string fileMdbPath, string fileComPath)
        {
            string result = "省十万移民搬迁数据导入结果\n\n";

            //崩塌表信息
            LoadRelocationLandSlip loadLandSlip = new LoadRelocationLandSlip();
            result += loadLandSlip.InsertData(fileMdbPath, fileComPath);

            //滑坡
            LoadRelocationLandSlide loadLandSlide = new LoadRelocationLandSlide();
            result += loadLandSlide.InsertData(fileMdbPath, fileComPath);

            //泥石流
            LoadRelocationDebrisFlow loadDebrisFlow = new LoadRelocationDebrisFlow();
            result += loadDebrisFlow.InsertData(fileMdbPath, fileComPath);

            //斜坡
            LoadRelocationSlope loadSlope = new LoadRelocationSlope();
            result += loadSlope.InsertData(fileMdbPath, fileComPath);

            //地裂缝
            LoadRelocationLandFracture loadLandFracture = new LoadRelocationLandFracture();
            result += loadLandFracture.InsertData(fileMdbPath, fileComPath);

            //地面塌陷
            LoadRelocationLandCollapse loadLandCollapse = new LoadRelocationLandCollapse();
            result += loadLandCollapse.InsertData(fileMdbPath, fileComPath);

            //地面沉降
            LoadRelocationLandSubsidence loadLandSubsidence = new LoadRelocationLandSubsidence();
            result += loadLandSubsidence.InsertData(fileMdbPath, fileComPath);

            return result;
        }

        
        public RelocationComprehensive GetComprehensive(DataRow row, DataColumnCollection columns)
        {
            RelocationComprehensive comp = new RelocationComprehensive();

            comp.CustomizeId = "";//row["隐患点编号"].ToString();

            comp.统一编号 = row["统一编号"].ToString();
            comp.灾害类型 = EnumGeoDisasterType.LandSubsidence;
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
            comp.灾害体积 = 0;// ConvertHelper.GetDoubleValueFromStr(row["灾害体积"].ToString());  //
            comp.灾害面积 = 0;  //


            return comp;
        }
    }
}
