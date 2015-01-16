
using System.IO;
using 隐患点主表导入工具.FiveScale.Investigation;
using 隐患点主表导入工具.FiveScale.MassPres;
using 隐患点主表导入工具.Investigation;
using 隐患点主表导入工具.MassPres;

namespace 隐患点主表导入工具
{
    /// <summary>
    /// 一键导入新疆的数据
    /// </summary>
    public class LoadXinjiangData
    {


        /// <summary>
        /// 一键导入十万调查和群测群防数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string LoadDataByOnekey(string filePath)
        {
            string result = Path.GetFileName(filePath) + ":\n";

            //崩塌表信息
            LoadLandSlip loadLandSlip = new LoadLandSlip();
            result += loadLandSlip.InsertData(filePath);

            //滑坡
            LoadLandSlide loadLandSlide = new LoadLandSlide();
            result += loadLandSlide.InsertData(filePath);

            //泥石流
            LoadDebrisFlow loadDebrisFlow = new LoadDebrisFlow();
            result += loadDebrisFlow.InsertData(filePath);

            //斜坡
            LoadSlope loadSlope = new LoadSlope();
            result += loadSlope.InsertData(filePath);

            //地裂缝
            LoadLandFracture loadLandFracture = new LoadLandFracture();
            result += loadLandFracture.InsertData(filePath);

            //地面塌陷
            LoadLandCollapse loadLandCollapse = new LoadLandCollapse();
            result += loadLandCollapse.InsertData(filePath);

            //地面沉降
            LoadLandSubsidence loadLandSubsidence = new LoadLandSubsidence();
            result += loadLandSubsidence.InsertData(filePath);



            //群测群防数据

            //防灾预案
            LoadPrePlan loadPrePlan = new LoadPrePlan();
            result += loadPrePlan.InsertData(filePath);

            //避灾明白卡
            LoadAvoidRiskCards loadAvoid = new LoadAvoidRiskCards();
            result += loadAvoid.InsertData(filePath);

            //工作明白卡
            LoadWorkingGuideCards loadCards = new LoadWorkingGuideCards();
            result += loadCards.InsertData(filePath);


            return result;

        }

        /// <summary>
        /// 一键导入五万详查数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string LoadDataFSByOnekey(string filePath)
        {
            string result = "\n\n" + Path.GetFileName(filePath) + ":\n";

            //崩塌表信息
            LoadLandSlipFS loadLandSlip = new LoadLandSlipFS();
            result += loadLandSlip.InsertData(filePath);

            //滑坡
            LoadLandSlideFS loadLandSlide = new LoadLandSlideFS();
            result += loadLandSlide.InsertData(filePath);

            //泥石流
            LoadDebrisFlowFS loadDebrisFlow = new LoadDebrisFlowFS();
            result += loadDebrisFlow.InsertData(filePath);

            //斜坡
            LoadSlopeFS loadSlope = new LoadSlopeFS();
            result += loadSlope.InsertData(filePath);

            //地裂缝
            LoadLandFractureFS loadLandFracture = new LoadLandFractureFS();
            result += loadLandFracture.InsertData(filePath);

            //地面塌陷
            LoadLandCollapseFS loadLandCollapse = new LoadLandCollapseFS();
            result += loadLandCollapse.InsertData(filePath);

            //地面沉降
            LoadLandSubsidenceFS loadLandSubsidence = new LoadLandSubsidenceFS();
            result += loadLandSubsidence.InsertData(filePath);



            //群测群防数据

            //防灾预案
            LoadPrePlanFS loadPrePlan = new LoadPrePlanFS();
            result += loadPrePlan.InsertData(filePath);

            //避灾明白卡
            LoadAvoidRiskCardFS loadAvoid = new LoadAvoidRiskCardFS();
            result += loadAvoid.InsertData(filePath);

            //工作明白卡
            LoadWorkingGuideCardFS loadCards = new LoadWorkingGuideCardFS();
            result += loadCards.InsertData(filePath);


            return result;

        }



    }
}
