using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NikolasHelper.GIS;
using NikolasHelper.HttpPost;
using NikolasHelper.Util;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Investigation;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.MassPres;

namespace NikolasHelper.WebAPI
{
    /// <summary>
    /// 防灾预案表
    /// </summary>
    public class PrePlanService
    {

        //服务地址
        public static string WebApiUrl = ConfigValues.WebApiUrl;


        /// <summary>
        /// 调用webapi服务，插入一条防灾预案表
        /// 插入过程：
        /// 1. 根据统一编号查询该防灾预案点是否存在，若存在，则不插入
        /// 2. 根据统一编号查询，是否存在该防灾预案点的基础灾害点
        /// 3. 若存在，则将该点插入到基础灾害点下。
        /// 4. 若不存在，则将该点插入为游离的点，给出异常提示。
        /// </summary>
        /// <param name="pre"></param>
        public void InsertPrePlan(PrePlan pre)
        {
            string uId = pre.统一编号;
            string queryComUrl = WebApiUrl + "api/Investigation/GetCompleteByUId?uid=" + uId;
            string queryPreUrl = WebApiUrl + "api/PrePlan/GetByUId?uid=" + uId;
            string insertPreUrl = WebApiUrl + "api/PrePlan/New";
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";


            string queryResult = Post.SendPost(queryPreUrl);
            List<PrePlan> prePlan = JsonConvert.DeserializeObject<List<PrePlan>>(queryResult);
            if (prePlan != null)
            {
                throw new Exception(@"该防灾预案点已存在");
            }

            queryResult = Post.SendPost(queryComUrl);
            List<Comprehensive> queryComp = JsonConvert.DeserializeObject<List<Comprehensive>>(queryResult);

            if (queryComp != null)
            {
                pre.PhyGeoDisasterId = queryComp[0].PhyGeoDisasterId;
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
                phy.Location = pre.地理位置;
                phy.DisasterType = pre.隐患点类型;
                phy.Lon = LonLatHelper.ConvertToDegreeStyleFromString(pre.经度);
                phy.Lat = LonLatHelper.ConvertToDegreeStyleFromString(pre.纬度);

                List<PrePlan> list = new List<PrePlan>();
                list.Add(pre);
                phy.PrePlans = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);
                throw new Exception(@"插入成功，但是为游离点");
            }
        }

        /// <summary>
        /// 调用webapi服务，插入一条避灾明白卡信息
        /// 插入过程：
        /// 1. 根据统一编号查询该避灾明白卡点是否存在，若存在，则不插入
        /// 2. 根据统一编号查询，是否存在该避灾明白卡的基础灾害点
        /// 3. 若存在，则将该点插入到基础灾害点下。
        /// 4. 若不存在，则将该点插入为游离的点，给出异常提示。
        /// </summary>
        /// <param name="pre"></param>
        public void InsertAvoidRiskCards(AvoidRiskCard card)
        {
            string uId = card.统一编号;
            string queryComUrl = WebApiUrl + "api/Investigation/GetCompleteByUId?uid=" + uId;
            string queryCardUrl = WebApiUrl + "api/AvoidRiskCard/GetByUid?uid=" + uId;
            string insertPreUrl = WebApiUrl + "api/AvoidRiskCard/New";
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";


            string queryResult = Post.SendPost(queryCardUrl);
            List<AvoidRiskCard> prePlan = JsonConvert.DeserializeObject<List<AvoidRiskCard>>(queryResult);
            if (prePlan != null)
            {
                throw new Exception(@"该避灾明白卡点已存在");
            }

            queryResult = Post.SendPost(queryComUrl);
            List<Comprehensive> queryComp = JsonConvert.DeserializeObject<List<Comprehensive>>(queryResult);

            if (queryComp != null)
            {
                card.PhyGeoDisasterId = queryComp[0].PhyGeoDisasterId;
                string carStr = JsonConvert.SerializeObject(card);
                Post.SendPost(insertPreUrl, carStr);
            }
            else
            {
                PhyGeoDisaster phy = new PhyGeoDisaster();
                //不存在物理点
                phy = new PhyGeoDisaster();
                phy.GBCodeId = card.统一编号.Substring(0, 6);
                phy.Name = card.名称;
                phy.Location = card.位置关系;
                phy.DisasterType = card.灾害类型;

                List<AvoidRiskCard> list = new List<AvoidRiskCard>();
                list.Add(card);
                phy.AvoidRiskCards = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);
                throw new Exception(@"插入成功，但是为游离点");
            }
        }



        /// <summary>
        /// 调用webapi服务，插入一条避灾明白卡信息
        /// 插入过程：
        /// 1. 根据统一编号查询该避灾明白卡点是否存在，若存在，则不插入
        /// 2. 根据统一编号查询，是否存在该避灾明白卡的基础灾害点
        /// 3. 若存在，则将该点插入到基础灾害点下。
        /// 4. 若不存在，则将该点插入为游离的点，给出异常提示。
        /// </summary>
        /// <param name="pre"></param>
        public void InsertWorkingGuideCards(WorkingGuideCard card)
        {
            string uId = card.统一编号;
            string queryComUrl = WebApiUrl + "api/Investigation/GetCompleteByUId?uid=" + uId;
            string queryCardUrl = WebApiUrl + "api/WorkingGuideCard/GetByUid?uid=" + uId;
            string insertCardUrl = WebApiUrl + "api/WorkingGuideCard/New";
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";


            string queryResult = Post.SendPost(queryCardUrl);
            List<AvoidRiskCard> prePlan = JsonConvert.DeserializeObject<List<AvoidRiskCard>>(queryResult);
            if (prePlan != null)
            {
                throw new Exception(@"该工作明白卡已存在");
            }

            queryResult = Post.SendPost(queryComUrl);
            List<Comprehensive> queryComp = JsonConvert.DeserializeObject<List<Comprehensive>>(queryResult);

            if (queryComp != null)
            {
                card.PhyGeoDisasterId = queryComp[0].PhyGeoDisasterId;
                string carStr = JsonConvert.SerializeObject(card);
                Post.SendPost(insertCardUrl, carStr);
            }
            else
            {
                PhyGeoDisaster phy = new PhyGeoDisaster();
                //不存在物理点
                phy = new PhyGeoDisaster();
                phy.GBCodeId = card.统一编号.Substring(0, 6);
                phy.Name = card.名称;
                phy.Location = card.灾害位置;
                phy.DisasterType = ConvertHelper.GetEnumGeoDisasterByStr(card.类型及规模);

                List<WorkingGuideCard> list = new List<WorkingGuideCard>();
                list.Add(card);
                phy.WorkingGuideCards = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);
                throw new Exception(@"插入成功，但是为游离点");
            }
        }
    }
}
