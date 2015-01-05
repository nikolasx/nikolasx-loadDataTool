

using System;
using Newtonsoft.Json;
using NikolasHelper.HttpPost;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;

namespace NikolasHelper.WebAPI
{
    /// <summary>
    /// 调用WebAPI 物理点的服务
    /// </summary>
    public class PhyGeoDisasterService
    {
        //public static string WebApiUrl = ConfigurationManager.ConnectionStrings["WebApiUrl"].ConnectionString;

        public static string WebApiUrl = ConfigValues.WebApiUrl;

        /// <summary>
        /// 调用WebAPI服务，插入物理点
        /// 插入前，判断该物理点编号是否存在
        /// </summary>
        public bool InsertPhyDisaster(PhyGeoDisaster phy, string customizeId)
        {

            string queryUrl = WebApiUrl + "api/PhyGeoDisaster/GetByCustomizeId?cusolizeId=" + customizeId;
            string insertUrl = WebApiUrl + "api/PhyGeoDisaster/New";

            //查询该物理点是否存在
            string queryResult = Post.SendPost(queryUrl);
            PhyGeoDisaster queryPhy = JsonConvert.DeserializeObject<PhyGeoDisaster>(queryResult);
            if (queryPhy != null)
                throw new Exception(@"物理点已经存在");
            //插入物理点
            string phyStr = JsonConvert.SerializeObject(phy);
            Post.SendPost(insertUrl, phyStr);

            return true;
        }
    }
}
