using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corner.Core;
using Newtonsoft.Json;
using NikolasHelper.HttpPost;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;

namespace NikolasHelper.WebAPI.FiveScale
{
    /// <summary>
    /// 导入五万详查数据服务
    /// </summary>
    public class ComprehensiveFSService
    {

        //服务地址
        public static string WebApiUrl = ConfigValues.WebApiUrl;
        /// <summary>
        /// 调用WebAPI服务，插入五万详查综合表
        /// 插入过程：
        /// 1. 根据统一编号查询该灾害点是否存在，若存在则抛出数据重复异常。
        /// 2. 组装数据的物理点，将数据插入到数据库中。
        /// </summary>
        public bool InsertComprehensive(ComprehensiveFS comp)
        {
            string uId = comp.统一编号;

            string queryComUrl = WebApiUrl + "api/InvestigationFS/GetCompleteByUId?uid=" + uId;
            string insertPhyUrl = WebApiUrl + "api/PhyGeoDisaster/New";


            //根据统一编号，判断综合表是否已经存在
            if (!string.IsNullOrEmpty(uId))
            {
                string queryResult = Post.SendPost(queryComUrl);

                ComprehensiveFS queryComp = JsonConvert.DeserializeObject<ComprehensiveFS>(queryResult);
                if (queryComp != null)
                {
                    throw new Exception(@"灾害点已经存在。");
                }

                //构造物理点插入数据
                var phy = new PhyGeoDisaster();
                phy.GBCodeId = comp.GBCodeId;
                phy.Name = comp.名称;
                phy.Location = comp.地理位置;
                phy.DisasterType = comp.灾害类型;
                phy.Lon = comp.经度;
                phy.Lat = comp.纬度;

                List<ComprehensiveFS> list = new List<ComprehensiveFS>();
                list.Add(comp);
                phy.ComprehensiveFSes = list;
                string phyStr = JsonConvert.SerializeObject(phy);
                Post.SendPost(insertPhyUrl, phyStr);
                return true;
            }

            throw new Exception(@"统一编号为null或者空字符串");
        }
    }
}
