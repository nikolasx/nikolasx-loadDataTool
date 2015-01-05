
using System.Collections.Generic;
using Newtonsoft.Json;
using NikolasHelper.GIS;
using NikolasHelper.HttpPost;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Investigation;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.MassPres;

namespace NikolasHelper.WebAPI
{
    public class MonthlyReportService
    {
        //WebAPI地址
        public static string WebApiUrl = ConfigValues.WebApiUrl;



        /// <summary>
        /// 向数据库中插入一条月报速报记录
        /// 1.根据用户定义编号，查询是否存在物理点。
        /// 2.若存在则获取物理点编号，将月报记录插入到物理点下。
        /// 3.若不存在，则构造新的物理点，包含该月报信息，将物理点插入到数据库中。
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public bool InsertMonthlyReport(MonthlyReport report)
        {
            string customizeId = report.CustomizeId;
            string queryPhyUrl = WebApiUrl + "api/PhyGeoDisaster/GetByCustomizeId?cusolizeId=" + customizeId;
            string insertReportUrl = WebApiUrl + "api/MonthlyReport/New";
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";

            PhyGeoDisaster phy = null;
            //根据用户定义编号获取物理点
            if (!string.IsNullOrEmpty(customizeId))
            {
                string queryResult = Post.SendPost(queryPhyUrl);
                phy = JsonConvert.DeserializeObject<PhyGeoDisaster>(queryResult);
            }
            if (phy != null)
            {
                //物理点已经存在
                report.PhyGeoDisasterId = phy.Id;
                string reportStr = JsonConvert.SerializeObject(report);
                Post.SendPost(insertReportUrl, reportStr);
            }
            else
            {
                //不存在物理点
                phy = new PhyGeoDisaster();
                phy.GBCodeId = report.GBCodeId;
                phy.Name = report.地理位置;
                phy.Location = report.地理位置;
                phy.DisasterType = report.灾害类型;
                phy.CustomizeId = report.CustomizeId;

                //经纬度转换
                phy.Lon = report.经度;
                phy.Lon = report.纬度;

                List<MonthlyReport> list = new List<MonthlyReport>();
                list.Add(report);
                phy.MonthlyReports = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);
            }
            return true;
        }


    }
}
