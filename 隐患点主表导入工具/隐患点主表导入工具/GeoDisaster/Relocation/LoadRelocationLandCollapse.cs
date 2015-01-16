using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NikolasHelper.Office;
using NikolasHelper.SQL;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Investigation;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.Relocation;
using 隐患点主表导入工具.Investigation;
using 隐患点主表导入工具.Util;

namespace 隐患点主表导入工具.Relocation
{
    public class LoadRelocationLandCollapse
    {


        public string InsertData(string landFilePath, string comFilePath)
        {
            //DataTable landTable = AccessHelper.GetDataTableByAccessFile(landFilePath, "地面塌陷主表");

            //TODO 临时修改
            DataTable landTable = SqlHelper.GetDataTable("select * from [dbo].[TheRelocationGroundCollapse]");
            DataTable comTable = ExcelHelper.GetDataTableByExcelFile(comFilePath, "省十万人避灾搬迁工程");
            LoadRelocationComprehensive loadCom = new LoadRelocationComprehensive();
            RelocationService compService = new RelocationService();

            int successCount = 0, failCount = 0;
            StringBuilder errorStr = new StringBuilder();

            for (int i = 0; i < landTable.Rows.Count; i++)
            {
                try
                {
                    RelocationLandCollapse landCollapse = GetRelocationLandCollapse(landTable.Rows[i], landTable.Columns);
                    string uId = landTable.Rows[i]["统一编号"].ToString();
                    DataRow[] rows = comTable.Select("统一编号=" + uId);
                    if (rows.Length > 1 || rows.Length < 1)
                        throw new Exception(@"不存在综合表或综合表不唯一");

                    RelocationComprehensive comp = loadCom.GetComprehensive(rows[0], comTable.Columns);
                    comp.LandCollapse = landCollapse;
                    compService.InsertRelocationComprehensive(comp);
                    successCount++;
                }
                catch (Exception ex)
                {
                    failCount++;
                    errorStr.Append("第" + (i + 1) + "导入失败，失败原因是：" + ex.Message + "\n");
                }
            }

            string result = UtilMethod.GetPrintErrorInfo("地面塌陷", landTable.Rows.Count, successCount, failCount,
                errorStr.ToString());
            return result;
        }


        public RelocationLandCollapse GetRelocationLandCollapse(DataRow row, DataColumnCollection columns)
        {

            RelocationLandCollapse landCollapse = new RelocationLandCollapse();

            #region LandCollapse 地面塌陷
            landCollapse.野外编号 = row["野外编号"].ToString();
            landCollapse.室内编号 = row["室内编号"].ToString();
            landCollapse.单体陷坑坑号1 = (int)(float.Parse(row["单体陷坑坑号1"].ToString() == "" ? "0" : row["单体陷坑坑号1"].ToString()));
            landCollapse.单体陷坑形状1 = row["单体陷坑形状1"].ToString();
            landCollapse.单体陷坑坑口规模1 = float.Parse(row["单体陷坑坑口规模1"].ToString() == "" ? "0" : row["单体陷坑坑口规模1"].ToString());
            landCollapse.单体陷坑深度1 = float.Parse(row["单体陷坑深度1"].ToString() == "" ? "0" : row["单体陷坑深度1"].ToString());
            landCollapse.单体陷坑变形面积1 = float.Parse(row["单体陷坑变形面积1"].ToString() == "" ? "0" : row["单体陷坑变形面积1"].ToString());
            landCollapse.单体陷坑规模等级1 = row["单体陷坑规模等级1"].ToString();
            landCollapse.单体陷坑长轴方向1 = row["单体陷坑长轴方向1"].ToString();
            landCollapse.单体陷坑充水水位深1 = float.Parse(row["单体陷坑充水水位深1"].ToString() == "" ? "0" : row["单体陷坑充水水位深1"].ToString());
            landCollapse.单体陷坑水位变动1 = float.Parse(row["单体陷坑水位变动1"].ToString() == "" ? "0" : row["单体陷坑水位变动1"].ToString());
            //landCollapse.单体陷坑发生时间1 = row["单体陷坑发生时间年1"].ToString() + (row["单体陷坑发生时间月1"].ToString() == "" ? "" : "年" + row["单体陷坑发生时间月1"].ToString() + "月") + (row["单体陷坑发生时间日1"].ToString() == "" ? "" : row["单体陷坑发生时间日1"].ToString() + "日");
            landCollapse.单体陷坑发生时间1 = row["单体陷坑发生时间1"].ToString();
            landCollapse.单体陷坑发展变化1 = row["单体陷坑发展变化1"].ToString();
            landCollapse.单体陷坑坑号2 = (int)(float.Parse(row["单体陷坑坑号2"].ToString() == "" ? "0" : row["单体陷坑坑号2"].ToString()));
            landCollapse.单体陷坑形状2 = row["单体陷坑形状2"].ToString();
            landCollapse.单体陷坑坑口规模2 = float.Parse(row["单体陷坑坑口规模2"].ToString() == "" ? "0" : row["单体陷坑坑口规模2"].ToString());
            landCollapse.单体陷坑深度2 = float.Parse(row["单体陷坑深度2"].ToString() == "" ? "0" : row["单体陷坑深度2"].ToString());
            landCollapse.单体陷坑变形面积2 = float.Parse(row["单体陷坑变形面积2"].ToString() == "" ? "0" : row["单体陷坑变形面积2"].ToString());
            landCollapse.单体陷坑规模等级2 = row["单体陷坑规模等级2"].ToString();
            landCollapse.单体陷坑长轴方向2 = row["单体陷坑长轴方向2"].ToString();
            landCollapse.单体陷坑充水水位深2 = float.Parse(row["单体陷坑充水水位深2"].ToString() == "" ? "0" : row["单体陷坑充水水位深2"].ToString());
            landCollapse.单体陷坑水位变动2 = float.Parse(row["单体陷坑水位变动2"].ToString() == "" ? "0" : row["单体陷坑水位变动2"].ToString());
            //landCollapse.单体陷坑发生时间2 = row["单体陷坑发生时间年2"].ToString() + (row["单体陷坑发生时间月2"].ToString() == "" ? "" : "年" + row["单体陷坑发生时间月2"].ToString() + "月") + (row["单体陷坑发生时间日2"].ToString() == "" ? "" : row["单体陷坑发生时间日2"].ToString() + "日");
            landCollapse.单体陷坑发生时间2 = row["单体陷坑发生时间2"].ToString();
            landCollapse.单体陷坑发展变化2 = row["单体陷坑发展变化2"].ToString();
            landCollapse.单体陷坑坑号3 = (int)(float.Parse(row["单体陷坑坑号3"].ToString() == "" ? "0" : row["单体陷坑坑号3"].ToString()));
            landCollapse.单体陷坑形状3 = row["单体陷坑形状3"].ToString();
            landCollapse.单体陷坑坑口规模3 = float.Parse(row["单体陷坑坑口规模3"].ToString() == "" ? "0" : row["单体陷坑坑口规模3"].ToString());
            landCollapse.单体陷坑深度3 = float.Parse(row["单体陷坑深度3"].ToString() == "" ? "0" : row["单体陷坑深度3"].ToString());
            landCollapse.单体陷坑变形面积3 = float.Parse(row["单体陷坑变形面积3"].ToString() == "" ? "0" : row["单体陷坑变形面积3"].ToString());
            landCollapse.单体陷坑规模等级3 = row["单体陷坑规模等级3"].ToString();
            landCollapse.单体陷坑长轴方向3 = row["单体陷坑长轴方向3"].ToString();
            landCollapse.单体陷坑充水水位深3 = float.Parse(row["单体陷坑充水水位深3"].ToString() == "" ? "0" : row["单体陷坑充水水位深3"].ToString());
            landCollapse.单体陷坑水位变动3 = float.Parse(row["单体陷坑水位变动3"].ToString() == "" ? "0" : row["单体陷坑水位变动3"].ToString());
            //landCollapse.单体陷坑发生时间3 = row["单体陷坑发生时间年3"].ToString() + (row["单体陷坑发生时间月3"].ToString() == "" ? "" : "年" + row["单体陷坑发生时间月3"].ToString() + "月") + (row["单体陷坑发生时间日3"].ToString() == "" ? "" : row["单体陷坑发生时间日3"].ToString() + "日");
            landCollapse.单体陷坑发生时间3 = row["单体陷坑发生时间3"].ToString();
            landCollapse.单体陷坑发展变化3 = row["单体陷坑发展变化3"].ToString();
            landCollapse.陷坑坑数 = (int)(float.Parse(row["陷坑坑数"].ToString() == "" ? "0" : row["陷坑坑数"].ToString()));
            landCollapse.陷坑分布面积 = row["陷坑分布面积"].ToString();
            landCollapse.排列形式 = row["排列形式"].ToString();
            landCollapse.长列方向 = row["长列方向"].ToString();
            landCollapse.最小陷坑口径 = float.Parse(row["最小陷坑口径"].ToString() == "" ? "0" : row["最小陷坑口径"].ToString());
            landCollapse.最大陷坑口径 = float.Parse(row["最大陷坑口径"].ToString() == "" ? "0" : row["最大陷坑口径"].ToString());
            landCollapse.最小陷坑深度 = float.Parse(row["最小陷坑深度"].ToString() == "" ? "0" : row["最小陷坑深度"].ToString());
            landCollapse.最大陷坑深度 = float.Parse(row["最大陷坑深度"].ToString() == "" ? "0" : row["最大陷坑深度"].ToString());
            //landCollapse.始发时间 = row["始发时间年"].ToString() + (row["始发时间月"].ToString() == "" ? "" : "年" + row["始发时间月"].ToString() + "月") + (row["始发时间日"].ToString() == "" ? "" : row["始发时间日"].ToString() + "日");
            //landCollapse.盛发开始时间 = row["盛发开始时间年"].ToString() + (row["盛发开始时间月"].ToString() == "" ? "" : "年" + row["盛发开始时间月"].ToString() + "月") + (row["盛发开始时间日"].ToString() == "" ? "" : row["盛发开始时间日"].ToString() + "日");
            //landCollapse.盛发截止时间 = row["盛发截止时间年"].ToString() + (row["盛发截止时间月"].ToString() == "" ? "" : "年" + row["盛发截止时间月"].ToString() + "月") + (row["盛发截止时间日"].ToString() == "" ? "" : row["盛发截止时间日"].ToString() + "日");
            //landCollapse.停止时间 = row["停止时间年"].ToString() + (row["停止时间月"].ToString() == "" ? "" : "年" + row["停止时间月"].ToString() + "月") + (row["停止时间日"].ToString() == "" ? "" : row["停止时间日"].ToString() + "日");

            landCollapse.始发时间 = row["始发时间"].ToString();
            landCollapse.盛发开始时间 = row["盛发开始时间"].ToString();
            landCollapse.盛发截止时间 = row["盛发截止时间"].ToString();
            landCollapse.停止时间 = row["停止时间"].ToString();

            landCollapse.尚在发展 = row["尚在发展"].ToString();
            landCollapse.单缝缝号1 = (int)(float.Parse(row["单缝缝号1"].ToString() == "" ? "0" : row["单缝缝号1"].ToString()));
            landCollapse.单缝形态1 = row["单缝形态1"].ToString();
            landCollapse.单缝延伸方向1 = row["单缝延伸方向1"].ToString();
            landCollapse.单缝倾向1 = (int)(float.Parse(row["单缝倾向1"].ToString() == "" ? "0" : row["单缝倾向1"].ToString()));
            landCollapse.单缝倾角1 = (int)(float.Parse(row["单缝倾角1"].ToString() == "" ? "0" : row["单缝倾角1"].ToString()));
            landCollapse.单缝长度1 = float.Parse(row["单缝长度1"].ToString() == "" ? "0" : row["单缝长度1"].ToString());
            landCollapse.单缝宽度1 = float.Parse(row["单缝宽度1"].ToString() == "" ? "0" : row["单缝宽度1"].ToString());
            landCollapse.单缝深度1 = float.Parse(row["单缝深度1"].ToString() == "" ? "0" : row["单缝深度1"].ToString());
            landCollapse.单缝性质1 = row["单缝性质1"].ToString();
            landCollapse.单缝缝号2 = (int)(float.Parse(row["单缝缝号2"].ToString() == "" ? "0" : row["单缝缝号2"].ToString()));
            landCollapse.单缝形态2 = row["单缝形态2"].ToString();
            landCollapse.单缝延伸方向2 = row["单缝延伸方向2"].ToString();
            landCollapse.单缝倾向2 = (int)(float.Parse(row["单缝倾向2"].ToString() == "" ? "0" : row["单缝倾向2"].ToString()));
            landCollapse.单缝倾角2 = (int)(float.Parse(row["单缝倾角2"].ToString() == "" ? "0" : row["单缝倾角2"].ToString()));
            landCollapse.单缝长度2 = float.Parse(row["单缝长度2"].ToString() == "" ? "0" : row["单缝长度2"].ToString());
            landCollapse.单缝宽度2 = float.Parse(row["单缝宽度2"].ToString() == "" ? "0" : row["单缝宽度2"].ToString());
            landCollapse.单缝深度2 = float.Parse(row["单缝深度2"].ToString() == "" ? "0" : row["单缝深度2"].ToString());
            landCollapse.单缝性质2 = row["单缝性质2"].ToString();
            landCollapse.单缝缝号3 = (int)(float.Parse(row["单缝缝号3"].ToString() == "" ? "0" : row["单缝缝号3"].ToString()));
            landCollapse.单缝形态3 = row["单缝形态3"].ToString();
            landCollapse.单缝延伸方向3 = row["单缝延伸方向3"].ToString();
            landCollapse.单缝倾向3 = (int)(float.Parse(row["单缝倾向3"].ToString() == "" ? "0" : row["单缝倾向3"].ToString()));
            landCollapse.单缝倾角3 = (int)(float.Parse(row["单缝倾角3"].ToString() == "" ? "0" : row["单缝倾角3"].ToString()));
            landCollapse.单缝长度3 = float.Parse(row["单缝长度3"].ToString() == "" ? "0" : row["单缝长度3"].ToString());
            landCollapse.单缝宽度3 = float.Parse(row["单缝宽度3"].ToString() == "" ? "0" : row["单缝宽度3"].ToString());
            landCollapse.单缝深度3 = float.Parse(row["单缝深度3"].ToString() == "" ? "0" : row["单缝深度3"].ToString());
            landCollapse.单缝性质3 = row["单缝性质3"].ToString();
            landCollapse.缝数 = (int)(float.Parse(row["缝数"].ToString() == "" ? "0" : row["缝数"].ToString()));
            landCollapse.裂缝分布面积 = float.Parse(row["裂缝分布面积"].ToString() == "" ? "0" : row["裂缝分布面积"].ToString());
            landCollapse.裂缝间距 = float.Parse(row["裂缝间距"].ToString() == "" ? "0" : row["裂缝间距"].ToString());
            landCollapse.裂缝排列形式 = row["裂缝排列形式"].ToString();
            landCollapse.裂缝倾向 = (int)(float.Parse(row["裂缝倾向"].ToString() == "" ? "0" : row["裂缝倾向"].ToString()));
            landCollapse.裂缝倾角 = (int)(float.Parse(row["裂缝倾角"].ToString() == "" ? "0" : row["裂缝倾角"].ToString()));
            landCollapse.裂缝长max = float.Parse(row["裂缝长max"].ToString() == "" ? "0" : row["裂缝长max"].ToString());
            landCollapse.裂缝长min = float.Parse(row["裂缝长min"].ToString() == "" ? "0" : row["裂缝长min"].ToString());
            landCollapse.裂缝宽max = float.Parse(row["裂缝宽max"].ToString() == "" ? "0" : row["裂缝宽max"].ToString());
            landCollapse.裂缝宽min = float.Parse(row["裂缝宽min"].ToString() == "" ? "0" : row["裂缝宽min"].ToString());
            landCollapse.裂缝深max = float.Parse(row["裂缝深max"].ToString() == "" ? "0" : row["裂缝深max"].ToString());
            landCollapse.裂缝深min = float.Parse(row["裂缝深min"].ToString() == "" ? "0" : row["裂缝深min"].ToString());
            landCollapse.塌陷区地貌特征 = row["塌陷区地貌特征"].ToString();
            landCollapse.成因类型 = row["成因类型"].ToString();
            landCollapse.岩溶塌陷地层时代 = row["岩溶塌陷地层时代"].ToString();
            landCollapse.岩溶塌陷地层岩性 = row["岩溶塌陷地层岩性"].ToString();
            landCollapse.岩溶塌陷岩层倾向 = (int)(float.Parse(row["岩溶塌陷岩层倾向"].ToString() == "" ? "0" : row["岩溶塌陷岩层倾向"].ToString()));
            landCollapse.岩溶塌陷岩层倾角 = (int)(float.Parse(row["岩溶塌陷岩层倾角"].ToString() == "" ? "0" : row["岩溶塌陷岩层倾角"].ToString()));
            landCollapse.岩溶塌陷断裂情况 = row["岩溶塌陷断裂情况"].ToString();
            landCollapse.岩溶塌陷溶洞发育情况 = row["岩溶塌陷溶洞发育情况"].ToString();
            landCollapse.岩溶塌陷岩层发育程度 = row["岩溶塌陷岩层发育程度"].ToString();
            landCollapse.岩溶塌陷塌顶溶洞埋深 = float.Parse(row["岩溶塌陷塌顶溶洞埋深"].ToString() == "" ? "0" : row["岩溶塌陷塌顶溶洞埋深"].ToString());
            landCollapse.岩溶塌陷地下水位埋深 = float.Parse(row["岩溶塌陷地下水位埋深"].ToString() == "" ? "0" : row["岩溶塌陷地下水位埋深"].ToString());
            landCollapse.岩溶塌陷诱发动力因素 = row["岩溶塌陷诱发动力因素"].ToString();
            landCollapse.土洞塌陷单层土性 = row["土洞塌陷单层土性"].ToString();
            landCollapse.土洞塌陷单层土厚 = float.Parse(row["土洞塌陷单层土厚"].ToString() == "" ? "0" : row["土洞塌陷单层土厚"].ToString());
            landCollapse.土洞塌陷双层上部土性 = row["土洞塌陷双层上部土性"].ToString();
            landCollapse.土洞塌陷双层上部土厚 = float.Parse(row["土洞塌陷双层上部土厚"].ToString() == "" ? "0" : row["土洞塌陷双层上部土厚"].ToString());
            landCollapse.土洞塌陷双层下部土性 = row["土洞塌陷双层下部土性"].ToString();
            landCollapse.土洞塌陷双层下部土厚 = float.Parse(row["土洞塌陷双层下部土厚"].ToString() == "" ? "0" : row["土洞塌陷双层下部土厚"].ToString());
            landCollapse.土洞塌陷下伏基岩时代 = row["土洞塌陷下伏基岩时代"].ToString();
            landCollapse.土洞塌陷下伏基岩岩性 = row["土洞塌陷下伏基岩岩性"].ToString();
            landCollapse.土洞塌陷地下水位埋深 = float.Parse(row["土洞塌陷地下水位埋深"].ToString() == "" ? "0" : row["土洞塌陷地下水位埋深"].ToString());
            landCollapse.土洞塌陷诱发动力因素 = row["土洞塌陷诱发动力因素"].ToString();
            landCollapse.井位塌陷区方向 = row["井位塌陷区方向"].ToString();
            landCollapse.井位塌陷区距离 = float.Parse(row["井位塌陷区距离"].ToString() == "" ? "0" : row["井位塌陷区距离"].ToString());
            landCollapse.井位塌陷区抽水降深 = float.Parse(row["井位塌陷区抽水降深"].ToString() == "" ? "0" : row["井位塌陷区抽水降深"].ToString());
            landCollapse.井位塌陷区日出水量 = float.Parse(row["井位塌陷区日出水量"].ToString() == "" ? "0" : row["井位塌陷区日出水量"].ToString());
            landCollapse.江河水位塌陷区方向 = row["江河水位塌陷区方向"].ToString();
            landCollapse.江河水位塌陷区距离 = float.Parse(row["江河水位塌陷区距离"].ToString() == "" ? "0" : row["江河水位塌陷区距离"].ToString());
            landCollapse.江河水位塌陷区水位变幅 = float.Parse(row["江河水位塌陷区水位变幅"].ToString() == "" ? "0" : row["江河水位塌陷区水位变幅"].ToString());
            landCollapse.江河水位塌陷区变化类型 = row["江河水位塌陷区变化类型"].ToString();
            landCollapse.冒顶塌陷土层时代 = row["冒顶塌陷土层时代"].ToString();
            landCollapse.冒顶塌陷土层土性 = row["冒顶塌陷土层土性"].ToString();
            landCollapse.冒顶塌陷土层厚度 = float.Parse(row["冒顶塌陷土层厚度"].ToString() == "" ? "0" : row["冒顶塌陷土层厚度"].ToString());
            landCollapse.冒顶塌陷岩层时代 = row["冒顶塌陷岩层时代"].ToString();
            landCollapse.冒顶塌陷岩层岩性 = row["冒顶塌陷岩层岩性"].ToString();
            landCollapse.冒顶塌陷岩层厚度 = float.Parse(row["冒顶塌陷岩层厚度"].ToString() == "" ? "0" : row["冒顶塌陷岩层厚度"].ToString());
            landCollapse.冒顶塌陷地下水位埋深 = float.Parse(row["冒顶塌陷地下水位埋深"].ToString() == "" ? "0" : row["冒顶塌陷地下水位埋深"].ToString());
            landCollapse.冒顶塌陷诱发动力因素 = row["冒顶塌陷诱发动力因素"].ToString();
            landCollapse.冒顶塌陷矿层厚度 = float.Parse(row["冒顶塌陷矿层厚度"].ToString() == "" ? "0" : row["冒顶塌陷矿层厚度"].ToString());
            //landCollapse.冒顶塌陷开采时间 = row["冒顶塌陷开采时间年"].ToString() + (row["冒顶塌陷开采时间月"].ToString() == "" ? "" : "年" + row["冒顶塌陷开采时间月"].ToString() + "月") + (row["冒顶塌陷开采时间日"].ToString() == "" ? "" : row["冒顶塌陷开采时间日"].ToString() + "日");
            landCollapse.冒顶塌陷开采时间 = row["冒顶塌陷开采时间"].ToString();
            landCollapse.冒顶塌陷开采厚度 = float.Parse(row["冒顶塌陷开采厚度"].ToString() == "" ? "0" : row["冒顶塌陷开采厚度"].ToString());
            landCollapse.冒顶塌陷开采深度 = float.Parse(row["冒顶塌陷开采深度"].ToString() == "" ? "0" : row["冒顶塌陷开采深度"].ToString());
            landCollapse.冒顶塌陷开采方法 = row["冒顶塌陷开采方法"].ToString();
            landCollapse.冒顶塌陷工作面推进速度 = float.Parse(row["冒顶塌陷工作面推进速度"].ToString() == "" ? "0" : row["冒顶塌陷工作面推进速度"].ToString());
            landCollapse.冒顶塌陷采出量 = float.Parse(row["冒顶塌陷采出量"].ToString() == "" ? "0" : row["冒顶塌陷采出量"].ToString());
            landCollapse.冒顶塌陷顶板管理方法 = row["冒顶塌陷顶板管理方法"].ToString();
            landCollapse.冒顶塌陷重复采动 = bool.Parse(row["冒顶塌陷重复采动"].ToString() == "" ? "false" : row["冒顶塌陷重复采动"].ToString());
            landCollapse.冒顶塌陷采空区形态 = row["冒顶塌陷采空区形态"].ToString();
            landCollapse.冒顶塌陷采空区规模 = row["冒顶塌陷采空区规模"].ToString();
            landCollapse.毁坏田地 = float.Parse(row["毁坏田地"].ToString() == "" ? "0" : row["毁坏田地"].ToString());
            landCollapse.毁坏房屋 = float.Parse(row["毁坏房屋"].ToString() == "" ? "0" : row["毁坏房屋"].ToString());
            landCollapse.阻断交通 = row["阻断交通"].ToString();
            landCollapse.地下水源枯竭 = row["地下水源枯竭"].ToString();
            landCollapse.地下水井突水 = row["地下水井突水"].ToString();
            landCollapse.掩埋地面物资 = row["掩埋地面物资"].ToString();
            landCollapse.新增陷坑 = (int)(float.Parse(row["新增陷坑"].ToString() == "" ? "0" : row["新增陷坑"].ToString()));
            landCollapse.扩大陷区 = float.Parse(row["扩大陷区"].ToString() == "" ? "0" : row["扩大陷区"].ToString());
            landCollapse.潜在毁田 = float.Parse(row["潜在毁田"].ToString() == "" ? "0" : row["潜在毁田"].ToString());
            landCollapse.潜在毁房 = (int)(float.Parse(row["潜在毁房"].ToString() == "" ? "0" : row["潜在毁房"].ToString()));
            landCollapse.出现新陷区 = (int)(float.Parse(row["出现新陷区"].ToString() == "" ? "0" : row["出现新陷区"].ToString()));
            landCollapse.新陷区面积 = float.Parse(row["新陷区面积"].ToString() == "" ? "0" : row["新陷区面积"].ToString());
            landCollapse.断路 = float.Parse(row["断路"].ToString() == "" ? "0" : row["断路"].ToString());
            landCollapse.其他危害 = row["其他危害"].ToString();
            landCollapse.隐患点 = bool.Parse(row["隐患点"].ToString() == "" ? "false" : row["隐患点"].ToString());
            landCollapse.防灾预案 = bool.Parse(row["防灾预案"].ToString() == "" ? "false" : row["防灾预案"].ToString());
            landCollapse.多媒体 = bool.Parse(row["多媒体"].ToString() == "" ? "false" : row["多媒体"].ToString());
            landCollapse.防治措施 = row["防治措施"].ToString();
            landCollapse.防治建议 = row["防治建议"].ToString();
            landCollapse.群测人员 = row["群测人员"].ToString();
            landCollapse.村长 = row["村长"].ToString();
            landCollapse.电话 = row["电话"].ToString();
            landCollapse.调查负责人 = row["调查负责人"].ToString();
            landCollapse.填表人 = row["填表人"].ToString();
            landCollapse.审核人 = row["审核人"].ToString();
            landCollapse.调查单位 = row["调查单位"].ToString();
            //landCollapse.填表日期 = row["填表日期年"].ToString() + (row["填表日期月"].ToString() == "" ? "" : "年" + row["填表日期月"].ToString() + "月") + (row["填表日期日"].ToString() == "" ? "" : row["填表日期日"].ToString() + "日");
            landCollapse.填表日期 = row["填表日期"].ToString();
            landCollapse.阶步指向 = row["阶步指向"].ToString();
            landCollapse.平面示意图 = (byte[])(row["平面示意图"].ToString() == "" ? null : row["平面示意图"]);
            landCollapse.剖面示意图 = (byte[])(row["平面示意图"].ToString() == "" ? null : row["平面示意图"]);
            landCollapse.地面塌陷情况 = row["地面塌陷情况"].ToString();
            //landCollapse.省名 = row["省"].ToString();
            //landCollapse.县名 = row["县"].ToString();
            //landCollapse.街道 = row["省"].ToString() + row["县"].ToString() + row["乡"].ToString() + row["村"].ToString() + row["组"].ToString();
            //landCollapse.平面示意图路径 = row["平面示意图"].ToString();
            //landCollapse.剖面示意图路径 = row["剖面示意图"].ToString();
            #endregion


            return landCollapse;

        }
    }
}
