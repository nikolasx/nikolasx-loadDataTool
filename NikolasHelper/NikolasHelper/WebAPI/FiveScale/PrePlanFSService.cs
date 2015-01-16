using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corner.Core;
using Newtonsoft.Json;
using NikolasHelper.GIS;
using NikolasHelper.HttpPost;
using NikolasHelper.Util;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.MassPres;

namespace NikolasHelper.WebAPI.FiveScale
{
    public class PrePlanFSService
    {



        //服务地址
        public static string WebApiUrl = ConfigValues.WebApiUrl;


        /// <summary>
        /// 插入五万详查的防灾预案表
        /// 1. 根据统一编号查询该防灾预案表是否存在，存在则不插入。
        /// 2. 根据统一编号查询所对应的五万调查综合表是否存在，存在则插入到该综合表对应的物理点下，否则作为游离点插入
        /// </summary>
        /// <param name="pre"></param>
        public void InsertPrePlanFS(PrePlanFS pre)
        {
            string uId = pre.统一编号;
            string queryComUrl = WebApiUrl + "api/InvestigationFS/GetCompleteByUId?uid=" + uId;
            string queryPreUrl = WebApiUrl + "api/PrePlanFS/GetByUId?uid=" + uId;
            string insertPreUrl = WebApiUrl + "api/PrePlanFS/New";
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";


            string queryResult = Post.SendPost(queryPreUrl);
            List<PrePlanFS> prePlan = JsonConvert.DeserializeObject<List<PrePlanFS>>(queryResult);
            if (prePlan.Count != 0)
            {
                throw new Exception(@"该防灾预案点已存在");
            }

            queryResult = Post.SendPost(queryComUrl);
            ComprehensiveFS queryComp = JsonConvert.DeserializeObject<ComprehensiveFS>(queryResult);

            if (queryComp != null)
            {
                pre.PhyGeoDisasterId = queryComp.PhyGeoDisasterId;
                string preStr = JsonConvert.SerializeObject(pre);
                Post.SendPost(insertPreUrl, preStr);
            }
            else
            {
                PhyGeoDisaster phy = new PhyGeoDisaster();
                //不存在物理点
                phy = new PhyGeoDisaster();
                phy.GBCodeId = pre.统一编号.Substring(0, 6);
                phy.Name = pre.名称;
                phy.Location = pre.地点;
                phy.DisasterType = ConvertHelper.GetEnumGeoDisasterByStr(pre.隐患点类型);
                phy.Lon = LonLatHelper.ConvertToDegreeStyleFromString(pre.经度);
                phy.Lat = LonLatHelper.ConvertToDegreeStyleFromString(pre.纬度);

                List<PrePlanFS> list = new List<PrePlanFS>();
                list.Add(pre);
                phy.PrePlanFSes = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);
                throw new Exception(@"插入成功，但是为游离点");
            }
        }


        public void InsertWorkingGuideCardFS(WorkingGuideCardFS pre)
        {
            string uId = pre.统一编号;
            string queryComUrl = WebApiUrl + "api/InvestigationFS/GetCompleteByUId?uid=" + uId;
            string queryPreUrl = WebApiUrl + "api/WorkingGuideCardFS/GetByUid?uid=" + uId;
            string insertPreUrl = WebApiUrl + "api/WorkingGuideCardFS/New";
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";


            string queryResult = Post.SendPost(queryPreUrl);
            List<PrePlanFS> prePlan = JsonConvert.DeserializeObject<List<PrePlanFS>>(queryResult);
            if (prePlan.Count != 0)
            {
                throw new Exception(@"该工作明白卡已存在");
            }

            queryResult = Post.SendPost(queryComUrl);
            ComprehensiveFS queryComp = JsonConvert.DeserializeObject<ComprehensiveFS>(queryResult);

            if (queryComp != null)
            {
                pre.PhyGeoDisasterId = queryComp.PhyGeoDisasterId;
                string preStr = JsonConvert.SerializeObject(pre);
                Post.SendPost(insertPreUrl, preStr);
            }
            else
            {
                PhyGeoDisaster phy = new PhyGeoDisaster();
                //不存在物理点
                phy = new PhyGeoDisaster();
                phy.GBCodeId = pre.统一编号.Substring(0, 6);
                phy.Name = pre.名称;
                phy.Location = pre.灾害位置;
                phy.DisasterType = ConvertHelper.GetEnumGeoDisasterByStr(pre.类型及规模);
                //phy.Lon = LonLatHelper.ConvertToDegreeStyleFromString(pre.经度);
                //phy.Lat = LonLatHelper.ConvertToDegreeStyleFromString(pre.纬度);

                List<WorkingGuideCardFS> list = new List<WorkingGuideCardFS>();
                list.Add(pre);
                phy.WorkingGuideCardFSes = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);
                throw new Exception(@"插入成功，但是为游离点");
            }
        }


        public void InsertAvoidRiskCardFS(AvoidRiskCardFS pre)
        {
            string uId = pre.统一编号;
            string queryComUrl = WebApiUrl + "api/InvestigationFS/GetCompleteByUId?uid=" + uId;
            string queryPreUrl = WebApiUrl + "api/AvoidRiskCardFS/GetByUid?uid=" + uId;
            string insertPreUrl = WebApiUrl + "api/AvoidRiskCardFS/New";
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";


            string queryResult = Post.SendPost(queryPreUrl);
            List<AvoidRiskCardFS> prePlan = JsonConvert.DeserializeObject<List<AvoidRiskCardFS>>(queryResult);
            if (prePlan.Count != 0)
            {
                throw new Exception(@"该避险明白卡已存在");
            }

            queryResult = Post.SendPost(queryComUrl);
            ComprehensiveFS queryComp = JsonConvert.DeserializeObject<ComprehensiveFS>(queryResult);

            if (queryComp != null)
            {
                pre.PhyGeoDisasterId = queryComp.PhyGeoDisasterId;
                string preStr = JsonConvert.SerializeObject(pre);
                Post.SendPost(insertPreUrl, preStr);
            }
            else
            {
                PhyGeoDisaster phy = new PhyGeoDisaster();
                //不存在物理点
                phy = new PhyGeoDisaster();
                phy.GBCodeId = pre.统一编号.Substring(0, 6);
                phy.Name = pre.名称;
                phy.Location = pre.位置关系;
                phy.DisasterType = ConvertHelper.GetEnumGeoDisasterByStr(pre.灾害类型);
                //phy.Lon = LonLatHelper.ConvertToDegreeStyleFromString(pre.经度);
                //phy.Lat = LonLatHelper.ConvertToDegreeStyleFromString(pre.纬度);

                List<AvoidRiskCardFS> list = new List<AvoidRiskCardFS>();
                list.Add(pre);
                phy.AvoidRiskCardFSes = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);
                throw new Exception(@"插入成功，但是为游离点");
            }
        }



        
        
    }
}
