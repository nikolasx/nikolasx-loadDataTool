﻿

using System.Collections.Generic;
using Newtonsoft.Json;
using NikolasHelper.HttpPost;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Emergency;

namespace NikolasHelper.WebAPI
{
    public class EmergencySurveyReportService
    {
        //服务地址
        public static string WebApiUrl = ConfigValues.WebApiUrl;



        /// <summary>
        /// 调用WebAPI上的服务，将应急调查数据插入到数据库
        /// 1. 通过用户自定义编号查询物理点是否存在。
        /// 2.若存在，则获取物理点编号，将该记录插入到物理点下。
        /// 3. 若不存在，则构造新的物理点，插入物理点及应急调查报告信息。
        /// </summary>
        public bool InsertEmergencySurveyReport(EmergencySurveyReport report)
        {
            string customizeId = report.CustomizeId;
            string queryPhyUrl = WebApiUrl + "api/PhyGeoDisaster/GetByCustomizeId?cusolizeId=" + customizeId;
            string insertReportUrl = WebApiUrl + "api/EmergencySurveyReport/New";
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";

            PhyGeoDisaster phy = null;
            //判断物理点是否存在
            if (!string.IsNullOrEmpty(customizeId))
            {
                string queryReslut = Post.SendPost(queryPhyUrl);
                phy = JsonConvert.DeserializeObject<PhyGeoDisaster>(queryReslut);
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
                phy.Name = report.灾点名称;
                phy.Location = report.设区市 + report.县 + report.乡镇 + report.村 + report.组;
                phy.DisasterType = report.灾害类型;
                phy.CustomizeId = report.CustomizeId;
                phy.Lon = report.经度;
                phy.Lat = report.纬度;

                List<EmergencySurveyReport> list = new List<EmergencySurveyReport>();
                list.Add(report);
                phy.EmergencySurveyReports = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);
            }
            return true;
        }
    }
}
