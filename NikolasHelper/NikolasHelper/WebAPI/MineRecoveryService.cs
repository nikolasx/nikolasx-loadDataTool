using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NikolasHelper.HttpPost;
using R2.Disaster.CoreEntities.Domain.MineRecovery;

namespace NikolasHelper.WebAPI
{
    public class MineRecoveryService
    {


        //服务地址
        public static string WebApiUrl = ConfigValues.WebApiUrl;

        /// <summary>
        /// 向数据库中插入矿山复绿档案表
        /// </summary>
        public bool InsertMineArchive(MineArchive mineArchive)
        {

            if (mineArchive == null)
                throw new ArgumentNullException(@"mineArchive");

            string insertUrl = WebApiUrl + "api/MineArchive/New";
            string objStr = JsonConvert.SerializeObject(mineArchive);
            Post.SendPost(insertUrl, objStr);
            return true;
        }

        /// <summary>
        /// 向数据库中插入矿山复绿环境调查表
        /// </summary>
        /// <param name="mineEnvironmentSurvey"></param>
        /// <returns></returns>
        public bool InsertMineEnvironmentSurvey(MineEnvironmentSurvey mineEnvironmentSurvey)
        {

            if (mineEnvironmentSurvey == null)
                throw new ArgumentNullException(@"mineEnvironmentSurvey");

            string insertUrl = WebApiUrl + "api/MineEnvironmentSurvey/New";
            string objStr = JsonConvert.SerializeObject(mineEnvironmentSurvey);
            Post.SendPost(insertUrl, objStr);
            return true;
        }

        /// <summary>
        /// 向数据库中插入矿山复绿遥感解译卡
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool InsertMineRemoteSensingCard(MineRemoteSensingCard card)
        {

            if (card == null)
                throw new ArgumentNullException(@"card");

            string insertUrl = WebApiUrl + "api/MineRemoteSensingCard/New";
            string objStr = JsonConvert.SerializeObject(card);
            Post.SendPost(insertUrl, objStr);
            return true;
        }
    }
}
