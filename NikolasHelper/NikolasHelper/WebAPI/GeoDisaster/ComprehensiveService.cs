

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NikolasHelper.GIS;
using NikolasHelper.HttpPost;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Investigation;

namespace NikolasHelper.WebAPI
{
    public class ComprehensiveService
    {
        //服务地址
        public static string WebApiUrl = ConfigValues.WebApiUrl;


        /// <summary>
        /// 调用WebAPI服务，插入十万调查综合表
        /// 插入过程：
        /// 1：根据用户定义的编号，查询是否存在该编号的物理点。
        /// 2：若存在物理点，根据统一编号查询:综合表是否已经存在，若存在，则为重复插入，异常。
        /// 3：若存在物理点，不存在综合表，则插入该综合表，激活隐患点状态。
        /// 4：若不存在物理点，则构造物理点，插入物理点及综合表。
        /// </summary>
        public bool InsertComprehensive(Comprehensive comp)
        {
            string customizeId = comp.CustomizeId;
            string uId = comp.统一编号;

            string queryPhyUrl = WebApiUrl + "api/PhyGeoDisaster/GetByCustomizeId?cusolizeId=" + customizeId;
            string queryComUrl = WebApiUrl + "api/Investigation/GetCompleteByUId?uid=" + uId;
            string insertComUrl = WebApiUrl + "api/Investigation/New";
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

                Comprehensive queryComp = JsonConvert.DeserializeObject<Comprehensive>(queryResult);
                if (queryComp != null)
                {
                    throw new Exception(@"灾害点已经存在。");
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

                    List<Comprehensive> list = new List<Comprehensive>();
                    list.Add(comp);
                    phy.Comprehensives = list;
                    string phyStr = JsonConvert.SerializeObject(phy);
                    Post.SendPost(insertPhyUrl, phyStr);
                }

                return true;
            }

            throw new Exception(@"统一编号为null或者空字符串");
        }
    }
}
