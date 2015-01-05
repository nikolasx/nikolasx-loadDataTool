using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;

namespace NikolasHelper.Util
{
    public class ConvertHelper
    {
        /// <summary>
        /// 根据灾害类型返回其枚举值，首先精确匹配，其次在模糊匹配
        /// </summary>
        /// <param name="disType"></param>
        /// <returns></returns>
        public static EnumGeoDisasterType GetEnumGeoDisasterByStr(string disType)
        {
            EnumGeoDisasterType enumType;
            try
            {
                enumType = GetEnumGeoDisasterByExactTypeStr(disType);
            }
            catch (Exception ex)
            {
                enumType = GetEnumDisTypeByFuzzyTypeStr(disType);
            }
            return enumType;
        }

        /// <summary>
        /// 根据灾害类型精确中文字符串，返回其枚举类型
        /// (针对雷磊WebApi定义的灾害类型)
        /// </summary>
        /// <param name="disType"></param>
        /// <returns></returns>
        public static EnumGeoDisasterType GetEnumGeoDisasterByExactTypeStr(string disType)
        {
            EnumGeoDisasterType enumType;
            switch (disType)
            {
                case "崩塌":
                    enumType = EnumGeoDisasterType.LandSlip;
                    break;
                case "滑坡":
                    enumType = EnumGeoDisasterType.LandSlide;
                    break;
                case "泥石流":
                    enumType = EnumGeoDisasterType.DebrisFlow;
                    break;
                case "斜坡":
                    enumType = EnumGeoDisasterType.Slope;
                    break;
                case "地裂缝":
                    enumType = EnumGeoDisasterType.LandFracture;
                    break;
                case "地面塌陷":
                    enumType = EnumGeoDisasterType.LandCollapse;
                    break;
                case "地面沉降":
                    enumType = EnumGeoDisasterType.LandSubsidence;
                    break;
                default:
                    throw new Exception(@"灾害类型不存在");
            }
            return enumType;
        }

        /// <summary>
        /// 根据灾害类型模糊字符串，返回其枚举类型
        /// </summary>
        /// <returns></returns>
        public static EnumGeoDisasterType GetEnumDisTypeByFuzzyTypeStr(string typeStr)
        {
            if (string.IsNullOrWhiteSpace(typeStr))
                throw new Exception(@"灾害类型字符串为null或空或空白字符串；");

            if (typeStr.Contains("崩塌"))
                return EnumGeoDisasterType.LandSlip;
            if (typeStr.Contains("滑坡"))
                return EnumGeoDisasterType.LandSlide;
            if (typeStr.Contains("斜坡"))
                return EnumGeoDisasterType.Slope;
            if (typeStr.Contains("泥石流"))
                return EnumGeoDisasterType.DebrisFlow;
            if (typeStr.Contains("塌陷"))
                return EnumGeoDisasterType.LandCollapse;
            if (typeStr.Contains("裂缝"))
                return EnumGeoDisasterType.LandFracture;
            if (typeStr.Contains("沉降"))
                return EnumGeoDisasterType.LandSubsidence;

            throw new Exception(@"未知灾害类型");

        }







        /// <summary>
        /// 将double字符串转化为double值
        /// </summary>
        /// <returns></returns>
        public static double GetDoubleValueFromStr(string str)
        {
            double result;
            if (!double.TryParse(str, out result))
            {
                result = 0.0;
            }
            return result;
        }
        /// <summary>
        /// 将int字符串转化为int值
        /// </summary>
        /// <returns></returns>
        public static int GetIntValueByStr(string str)
        {
            int result;
            if (!int.TryParse(str, out result))
            {
                result = 0;
            }
            return result;
        }


        /// <summary>
        /// 将“2014/8/15”字符串格式的时间转化为DateTime,不能转化则默认为“1990/1/1”
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeValueFromStr(string str)
        {
            DateTime time;
            if (!DateTime.TryParse(str,out time))
            {
                time = new DateTime(1900, 1, 1);
            }
            return time;
        }
    }

}
