using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corner.Core;
using NikolasHelper.GIS;
using NikolasHelper.Util;

namespace 隐患点主表导入工具.FiveScale.Investigation
{

    /// <summary>
    /// 导入五万详查综合表
    /// </summary>
    public class LoadComprehensiveFS
    {



        public ComprehensiveFS GetComprehensiveFs(DataRow row, DataColumnCollection columns)
        {
            var comp = new ComprehensiveFS();

            comp.统一编号 = row["统一编号"].ToString();
            comp.野外编号 = row["野外编号"].ToString();
            comp.灾害类型 = ConvertHelper.GetEnumGeoDisasterByStr(row["灾害类型"].ToString());
            comp.GBCodeId = row["国际代码"].ToString();
            comp.地理位置 = row["地理位置"].ToString();
            comp.省 = row["省"].ToString();
            comp.市 = row["市"].ToString();
            comp.县 = row["县"].ToString();
            comp.发生时间年 = ConvertHelper.GetIntValueByStr(row["发生时间年"].ToString());
            comp.发生时间月 = ConvertHelper.GetIntValueByStr(row["发生时间月"].ToString());
            comp.发生时间日 = ConvertHelper.GetIntValueByStr(row["发生时间日"].ToString());
            comp.名称 = row["名称"].ToString();
            comp.经度 = LonLatHelper.ConvertToDegreeStyleFromString(row["经度"].ToString());
            comp.纬度 = LonLatHelper.ConvertToDegreeStyleFromString(row["纬度"].ToString());
            comp.死亡人数 = ConvertHelper.GetIntValueByStr(row["死亡人数"].ToString());
            comp.威胁人口 = ConvertHelper.GetIntValueByStr(row["威胁人口"].ToString());
            comp.直接经济损失 = ConvertHelper.GetFloatValutFromStr(row["直接经济损失"].ToString());
            comp.威胁财产 = ConvertHelper.GetFloatValutFromStr(row["威胁财产"].ToString());
            comp.灾情等级 = row["灾情等级"].ToString();
            comp.险情等级 = row["险情等级"].ToString();
            comp.X坐标 = ConvertHelper.GetIntValueByStr(row["X坐标"].ToString());
            comp.Y坐标 = ConvertHelper.GetIntValueByStr(row["Y坐标"].ToString());
            comp.灾害体积 = ConvertHelper.GetFloatValutFromStr(row["灾害体积"].ToString());
            comp.目前稳定状态 = row["目前稳定状态"].ToString();//
            comp.灾害规模等级 = row["灾害规模等级"].ToString();
            comp.灾害体积 = ConvertHelper.GetFloatValutFromStr(row["灾害体积"].ToString());
            comp.遥感 = Boolean.Parse(row["遥感"].ToString());
            comp.测绘 = Boolean.Parse(row["测绘"].ToString());
            comp.调查 = Boolean.Parse(row["调查"].ToString());
            comp.勘查 = Boolean.Parse(row["勘查"].ToString());
            comp.调查点类型 = row["调查点类型"].ToString();
            comp.方向 = row["方向"].ToString();
            return comp;
        }

    }
}
