using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NikolasHelper.HttpPost;
using NikolasHelper.LINQ;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;

namespace NikolasHelper.WebAPI
{
    public class ExpressionService
    {

        public static string WebApiUrl = ConfigValues.WebApiUrl;

        public Expression<Func<PhyGeoDisaster, Boolean>> GetExpressionByGBCode(List<string> gbcodes)
        {
            //如果为null，表示忽略此条件
            var eps = DynamicLinqExpressions.True<PhyGeoDisaster>();
            if (gbcodes != null && gbcodes.Count != 0)
            {
                //如果不为null，则初始条件为False，多个gbcodes间为“OR”关系，有一个满足则，此条件成立
                eps = DynamicLinqExpressions.False<PhyGeoDisaster>();
                foreach (var regionCode in gbcodes)
                {
                    if (!String.IsNullOrEmpty(regionCode))
                    {
                        eps = eps.Or(p => p.GBCodeId == regionCode);
                    }
                }
            }
            return eps;
        }



        public string ExpressionTest()
        {
            string url = WebApiUrl + "api/PhyGeoDisaster/GetByExpression";

            List<string> list = new List<string>();
            list.Add("360829");
            var eps = GetExpressionByGBCode(list);

            string condition = JsonConvert.SerializeObject(eps);

            var aa = JsonConvert.DeserializeObject(condition, typeof(Expression<Func<PhyGeoDisaster, Boolean>>));
            string result = Post.SendPost(url, condition);
            return result;

        }
    }
}
