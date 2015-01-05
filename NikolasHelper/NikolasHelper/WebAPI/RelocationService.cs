using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NikolasHelper.HttpPost;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Investigation;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Relocation;

namespace NikolasHelper.WebAPI
{
    /// <summary>
    /// 移民搬迁数据处理服务
    /// </summary>
    public class RelocationService
    {
        //TODO  完成移民搬迁的基础信息，核查表信息，安置地评价信息的录入
        //TODO  完成矿山复绿（基础档案表，环境调查卡，遥感解译卡）的信息录入


        //服务地址
        public static string WebApiUrl = ConfigValues.WebApiUrl;

        /// <summary>
        /// 调用WebAPI服务，插入移民搬迁信息
        /// 插入过程：
        /// 1：根据用户定义的编号，查询是否存在该编号的物理点。
        /// 2：若存在物理点，根据统一编号查询:移民搬迁综合表是否已经存在，若存在，则为重复插入，异常。
        /// 3：若存在物理点，不存在综合表，则插入该综合表，激活隐患点状态。
        /// 4：若不存在物理点，则构造物理点，插入物理点及综合表。
        /// </summary>
        public bool InsertRelocationComprehensive(RelocationComprehensive comp)
        {
            string customizeId = comp.CustomizeId;
            string uId = comp.统一编号;

            string queryPhyUrl = WebApiUrl + "api/PhyGeoDisaster/GetByCustomizeId?cusolizeId=" + customizeId;
            string queryComUrl = WebApiUrl + "api/RelocationComprehensive/GetCompleteByUid?uid=" + uId;
            string insertComUrl = WebApiUrl + "api/RelocationComprehensive/New";
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";

            PhyGeoDisaster phy = null;
            //根据用户定义编号，获取物理点
            if (!string.IsNullOrEmpty(customizeId))
            {
                string queryResult = Post.SendPost(queryPhyUrl);
                phy = JsonConvert.DeserializeObject<PhyGeoDisaster>(queryResult);
            }
            //根据统一编号，判断综合表是否已经存在
            if (!string.IsNullOrEmpty(uId))
            {
                string queryResult = Post.SendPost(queryComUrl);
                List<Comprehensive> queryComp = JsonConvert.DeserializeObject<List<Comprehensive>>(queryResult);
                if (queryComp.Count > 0)
                {
                    throw new Exception(@"移民搬迁点数据已经存在。");
                }

                //根据物理点是否存在判断
                if (phy != null)
                {
                    //物理点已经存在
                    comp.PhyGeoDisasterId = phy.Id;
                    string compStr = JsonConvert.SerializeObject(comp);
                    Post.SendPost(insertComUrl, compStr);
                }
                else
                {
                    //不存在物理点
                    phy = new PhyGeoDisaster();
                    phy.GBCodeId = comp.GBCodeId;
                    phy.Name = comp.名称;
                    phy.Location = comp.地理位置;
                    phy.DisasterType = comp.灾害类型;
                    phy.Lon = comp.经度;
                    phy.Lat = comp.纬度;

                    List<RelocationComprehensive> list = new List<RelocationComprehensive>();
                    list.Add(comp);
                    phy.RelocationComprehensives = list;
                    string phyStr = JsonConvert.SerializeObject(phy);
                    Post.SendPost(insertPhyUrl, phyStr);
                }

                return true;
            }

            throw new Exception(@"统一编号为null或者空字符串");
        }



        /// <summary>
        /// 插入移民搬迁环境核查表（泥石流）
        /// 插入过程：
        /// 1. 根据统一编号，查询移民搬迁综合表信息是否存在，若不存在，则构造物理点，插入。
        /// 2. 若存在，则获取物理点编号，插在该物理点下。
        /// </summary>
        /// <returns></returns>
        public bool InsertRelocationDebrisFlowCheck(RelocationDebrisFlowCheck flow)
        {

            string uid = flow.统一编号;
            string queryComUrl = WebApiUrl + "api/RelocationComprehensive/GetCompleteByUid?uid=" + uid;
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";
            string insertCheckUrl = WebApiUrl + "api/RelocationDebrisFlowCheck/New";

            int phyId = 0;

            //查询是否存在该点的移民搬迁综合表信息
            if (!string.IsNullOrEmpty(uid))
            {
                string resultStr = Post.SendPost(queryComUrl);
                var resultList = JsonConvert.DeserializeObject<List<RelocationDebrisFlowCheck>>(resultStr);
                if (resultList.Count > 0)
                {
                    phyId = resultList[0].PhyGeoDisasterId;
                }
            }

            if (phyId != 0)
            {
                //物理点存在
                flow.PhyGeoDisasterId = phyId;
                string flowStr = JsonConvert.SerializeObject(flow);
                Post.SendPost(insertCheckUrl, flowStr);
            }
            else
            {
                //不存在物理点
                PhyGeoDisaster phy = new PhyGeoDisaster();
                phy.Location = flow.设区市 + flow.县市区 + flow.乡镇场 + flow.村组及地名;

                List<RelocationDebrisFlowCheck> list = new List<RelocationDebrisFlowCheck>();
                list.Add(flow);
                phy.RelocationDebrisFlowChecks = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);

            }

            return true;

        }


        public bool InsertRelocationLandSlipCheck(RelocationLandSlipCheck landSlip)
        {

            string uid = landSlip.统一编号;
            string queryComUrl = WebApiUrl + "api/RelocationComprehensive/GetCompleteByUid?uid=" + uid;
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";
            string insertCheckUrl = WebApiUrl + "api/RelocationLandSlipCheck/New";

            int phyId = 0;

            //查询是否存在该点的移民搬迁综合表信息
            if (!string.IsNullOrEmpty(uid))
            {
                string resultStr = Post.SendPost(queryComUrl);
                var resultList = JsonConvert.DeserializeObject<List<RelocationComprehensive>>(resultStr);
                if (resultList.Count > 0)
                {
                    phyId = resultList[0].PhyGeoDisasterId;
                }
            }

            if (phyId != 0)
            {
                //物理点存在
                landSlip.PhyGeoDisasterId = phyId;
                string landSlipStr = JsonConvert.SerializeObject(landSlip);
                Post.SendPost(insertCheckUrl, landSlipStr);
            }
            else
            {
                //不存在物理点
                PhyGeoDisaster phy = new PhyGeoDisaster();
                phy.Location = landSlip.设区市 + landSlip.县市区 + landSlip.乡镇场 + landSlip.村组及地名;

                List<RelocationLandSlipCheck> list = new List<RelocationLandSlipCheck>();
                list.Add(landSlip);
                phy.RelocationLandSlipChecks = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);

            }

            return true;

        }


        public bool InsertRelocationLandSlideCheck(RelocationLandSlideCheck landSlide)
        {

            string uid = landSlide.统一编号;
            string queryComUrl = WebApiUrl + "api/RelocationComprehensive/GetCompleteByUid?uid=" + uid;
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";
            string insertCheckUrl = WebApiUrl + "api/RelocationLandSlideCheck/New";

            int phyId = 0;

            //查询是否存在该点的移民搬迁综合表信息
            if (!string.IsNullOrEmpty(uid))
            {
                string resultStr = Post.SendPost(queryComUrl);
                var resultList = JsonConvert.DeserializeObject<List<RelocationComprehensive>>(resultStr);
                if (resultList.Count > 0)
                {
                    phyId = resultList[0].PhyGeoDisasterId;
                }
            }

            if (phyId != 0)
            {
                //物理点存在
                landSlide.PhyGeoDisasterId = phyId;
                string landSlideStr = JsonConvert.SerializeObject(landSlide);
                Post.SendPost(insertCheckUrl, landSlideStr);
            }
            else
            {
                //不存在物理点
                PhyGeoDisaster phy = new PhyGeoDisaster();
                phy.Location = landSlide.设区市 + landSlide.县市区 + landSlide.乡镇场 + landSlide.村组及地名;

                List<RelocationLandSlideCheck> list = new List<RelocationLandSlideCheck>();
                list.Add(landSlide);
                phy.RelocationLandSlideChecks = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);

            }

            return true;

        }


        public bool InsertRelocationSlopeCheck(RelocationSlopeCheck slope)
        {

            string uid = slope.统一编号;
            string queryComUrl = WebApiUrl + "api/RelocationComprehensive/GetCompleteByUid?uid=" + uid;
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";
            string insertCheckUrl = WebApiUrl + "api/RelocationSlopeCheck/New";

            int phyId = 0;

            //查询是否存在该点的移民搬迁综合表信息
            if (!string.IsNullOrEmpty(uid))
            {
                string resultStr = Post.SendPost(queryComUrl);
                var resultList = JsonConvert.DeserializeObject<List<RelocationComprehensive>>(resultStr);
                if (resultList.Count > 0)
                {
                    phyId = resultList[0].PhyGeoDisasterId;
                }
            }

            if (phyId != 0)
            {
                //物理点存在
                slope.PhyGeoDisasterId = phyId;
                string flowStr = JsonConvert.SerializeObject(slope);
                Post.SendPost(insertCheckUrl, flowStr);
            }
            else
            {
                //不存在物理点
                PhyGeoDisaster phy = new PhyGeoDisaster();
                phy.Location = slope.设区市 + slope.县市区 + slope.乡镇场 + slope.村组及地名;

                List<RelocationSlopeCheck> list = new List<RelocationSlopeCheck>();
                list.Add(slope);
                phy.RelocationSlopeChecks = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);

            }

            return true;

        }


        public bool InsertRelocationLandCollapseCheck(RelocationLandCollapseCheck landCollapse)
        {

            string uid = landCollapse.统一编号;
            string queryComUrl = WebApiUrl + "api/RelocationComprehensive/GetCompleteByUid?uid=" + uid;
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";
            string insertCheckUrl = WebApiUrl + "api/RelocationLandCollapseCheck/New";

            int phyId = 0;

            //查询是否存在该点的移民搬迁综合表信息
            if (!string.IsNullOrEmpty(uid))
            {
                string resultStr = Post.SendPost(queryComUrl);
                var resultList = JsonConvert.DeserializeObject<List<RelocationComprehensive>>(resultStr);
                if (resultList.Count > 0)
                {
                    phyId = resultList[0].PhyGeoDisasterId;
                }
            }

            if (phyId != 0)
            {
                //物理点存在
                landCollapse.PhyGeoDisasterId = phyId;
                string flowStr = JsonConvert.SerializeObject(landCollapse);
                Post.SendPost(insertCheckUrl, flowStr);
            }
            else
            {
                //不存在物理点
                PhyGeoDisaster phy = new PhyGeoDisaster();
                phy.Location = landCollapse.设区市 + landCollapse.县市区 + landCollapse.乡镇场 + landCollapse.村组及地名;

                List<RelocationLandCollapseCheck> list = new List<RelocationLandCollapseCheck>();
                list.Add(landCollapse);
                phy.RelocationLandCollapseChecks = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);

            }

            return true;

        }


        public bool InsertRelocationPlaceEvaluation(RelocationPlaceEvaluation evaluation)
        {

            string uid = evaluation.统一编号;
            string queryComUrl = WebApiUrl + "api/RelocationComprehensive/GetCompleteByUid?uid=" + uid;
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";
            string insertCheckUrl = WebApiUrl + "api/RelocationPlaceEvaluation/New";

            int phyId = 0;

            //查询是否存在该点的移民搬迁综合表信息
            if (!string.IsNullOrEmpty(uid))
            {
                string resultStr = Post.SendPost(queryComUrl);
                var resultList = JsonConvert.DeserializeObject<List<RelocationComprehensive>>(resultStr);
                if (resultList.Count > 0)
                {
                    phyId = resultList[0].PhyGeoDisasterId;
                }
            }

            if (phyId != 0)
            {
                //物理点存在
                evaluation.PhyGeoDisasterId = phyId;
                string flowStr = JsonConvert.SerializeObject(evaluation);
                Post.SendPost(insertCheckUrl, flowStr);
            }
            else
            {
                //不存在物理点
                PhyGeoDisaster phy = new PhyGeoDisaster();
                phy.Location = evaluation.区市 + evaluation.县市区 + evaluation.乡镇场 + evaluation.村组及地名;

                List<RelocationPlaceEvaluation> list = new List<RelocationPlaceEvaluation>();
                list.Add(evaluation);
                phy.RelocationPlaceEvaluations = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);

            }

            return true;

        }



    }



}
