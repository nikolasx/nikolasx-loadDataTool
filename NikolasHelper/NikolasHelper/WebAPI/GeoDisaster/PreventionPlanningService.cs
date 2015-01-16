using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NikolasHelper.HttpPost;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.MassPres;

namespace NikolasHelper.WebAPI
{
    /// <summary>
    /// 防治规划服务
    /// </summary>
    public class PreventionPlanningService
    {
        public static string WebApiUrl = ConfigValues.WebApiUrl;

        /// <summary>
        /// 调用服务，向数据库中插入防治规划信息
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        public bool InsertPreventionPlanningService(PreventionPlanning plan)
        {
            if (plan == null)
                throw new ArgumentNullException("plan");

            string insertUrl = WebApiUrl + "api/PreventionPlanning/New";

            string objStr = JsonConvert.SerializeObject(plan);
            Post.SendPost(insertUrl, objStr);
            return true;
        }
    }
}
