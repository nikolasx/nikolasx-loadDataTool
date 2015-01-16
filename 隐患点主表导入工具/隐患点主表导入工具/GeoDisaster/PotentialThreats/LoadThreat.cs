using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NikolasHelper.GIS;
using NikolasHelper.Office;
using NikolasHelper.Util;
using NikolasHelper.WebAPI;
using R2.Disaster.CoreEntities.Domain.GeoDisaster;
using R2.Disaster.CoreEntities.Domain.GeoDisaster.PotentialThreats;

namespace 隐患点主表导入工具.PotentialThreats
{
    /// <summary>
    /// 导入隐患点主表
    /// </summary>
    public class LoadThreat
    {


        public string InsertData(string filePath)
        {
            DataTable dt = ExcelHelper.GetDataTableByExcelFile(filePath, "隐患点主表");

            StringBuilder exceptionStr = new StringBuilder(string.Empty);

            int successCount = 0, failCount = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    PhyGeoDisaster phy = GetPhyGeoDisaster(dt.Rows[i], dt.Columns);
                    Threat threat = GetThreat(dt.Rows[i], dt.Columns);

                    List<Threat> list = new List<Threat>()
                    {
                        threat
                    };

                    phy.Threats = list;
                    PhyGeoDisasterService phyService = new PhyGeoDisasterService();
                    phyService.InsertPhyDisaster(phy, phy.CustomizeId);
                    successCount++;

                }
                catch (Exception ex)
                {
                    failCount++;
                    string errorStr = "第" + (i + 1) + "条记录导入失败" + "失败原因：" + ex.Message + "\n";
                    exceptionStr.Append(errorStr);

                    if (errorStr.Contains("内部服务器错误"))
                    {
                        var error = errorStr;
                    }
                }

            }
            string result = "\n隐患点主表导入结果：\n\n";
            result += "总条数为：" + dt.Rows.Count + "\n";
            result += "导入成功的记录数：" + successCount + "条。" + "失败的记录数是：" + failCount + "条。\n"
                           + exceptionStr;
            return result;
        }


        /// <summary>
        /// 根据隐患点主表的行信息，获取隐患点记录信息
        /// </summary>
        public Threat GetThreat(DataRow row, DataColumnCollection column)
        {
            Threat threat = new Threat();

            threat.搬迁或治理现状 = row["搬迁或治理现状"].ToString();
            threat.报警方法 = row["报警方法（预定报警方式）"].ToString();
            threat.变形特征及活动历史 = row["变形特征及活动历史（历史活动情况）"].ToString();
            threat.村级责任人 = row["村级责任人"].ToString();
            threat.村级责任人手机 = row["村级责任人手机"].ToString();
            threat.地质环境条件 = row["地质环境条件（地质条件）"].ToString();
            threat.防治建议 = row["防治建议（应急防御措施）"].ToString();
            threat.规模 = row["规模"].ToString() == "" ? 0 : (int)double.Parse(row["规模"].ToString());
            threat.监测方法 = row["监测方法（监测手段）"].ToString();
            threat.监测人员手机 = row["监测员手机"].ToString();
            threat.监测人员 = row["监测员"].ToString();
            threat.潜在危害 = row["潜在危害（险情预测）"].ToString();
            threat.人员撤离路线 = row["人员撤离路线（预定疏散路线）"].ToString();
            threat.威胁财产 = row["威胁财产（万元）"].ToString() == "" ? 0 : double.Parse(row["威胁财产（万元）"].ToString());
            threat.威胁人口 = row["威胁人口（人）"].ToString() == "" ? 0 : int.Parse(row["威胁人口（人）"].ToString());
            threat.稳定性分析 = row["稳定性分析（发展趋势预测）"].ToString();
            threat.相关图件照片 = row["相关图件、照片"].ToString();
            threat.诱发因素 = row["引发因素（可能诱发因素）"].ToString();
            threat.预定避灾地点 = row["预定避灾地点"].ToString();

            //string lonStr = row["X"].ToString().Replace("度", "-").Replace("分", "-").Replace("秒", "-");
            //string latStr = row["Y"].ToString().Replace("度", "-").Replace("分", "-").Replace("秒", "-");
            //threat.经度 = LonLatHelper.ConvertToDegreeStyleFromString(lonStr);
            //threat.纬度 = LonLatHelper.ConvertToDegreeStyleFromString(latStr);
            //threat.SourceId = row["来源数据编号"].ToString();


            //这个地方需要对江西省填入的X,Y值做判断。
            //1. dd-mm-ss格式
            int lon;
            if (row["X"].ToString().Contains('-'))
            {
                threat.经度 = LonLatHelper.ConvertToDegreeStyleFromString(row["X"].ToString());
                threat.纬度 = LonLatHelper.ConvertToDegreeStyleFromString(row["Y"].ToString());
            }
            //度分秒格式
            else if (row["X"].ToString().Contains("度"))
            {
                string lonStr = row["X"].ToString().Replace("度", "-").Replace("分", "-").Replace("秒", "-");
                string latStr = row["Y"].ToString().Replace("度", "-").Replace("分", "-").Replace("秒", "-");

                threat.经度 = LonLatHelper.ConvertToDegreeStyleFromString(lonStr);
                threat.纬度 = LonLatHelper.ConvertToDegreeStyleFromString(latStr);
            }
            //大地坐标

            else if (int.TryParse(row["X"].ToString(), out lon) && lon > 10000)
            {
                double lond = double.Parse(row["Y"].ToString());
                double latd = double.Parse(row["X"].ToString());
                double tlon, tlat;
                LonLatHelper.GaussToBL(lond, latd, out tlon, out tlat);
                threat.经度 = tlon;
                threat.纬度 = tlat;
            }
            else if (row["X"].ToString().Contains('.'))
            {
                threat.经度 = double.Parse(row["X"].ToString());
                threat.纬度 = double.Parse(row["Y"].ToString());
            }
            //
            //
            threat.Village = row["村"].ToString();
            threat.Towns = row["乡镇"].ToString();
            threat.Group = row["组"].ToString();

            //获取隐患点编号
            threat.CustomizeId = row["隐患点编号"].ToString();


            threat.Name = row["灾害名称"].ToString();
            threat.GBCodeId = row["市县编码"].ToString();
            threat.ThreatSource = row["隐患点来源"].ToString();
            threat.DisasterType = ConvertHelper.GetEnumGeoDisasterByStr(row["灾害类型 "].ToString());
            threat.IsActive = true;

            return threat;

        }

        /// <summary>
        /// 根据隐患点主表的行信息，获取物理点记录信息
        /// </summary>
        public PhyGeoDisaster GetPhyGeoDisaster(DataRow row, DataColumnCollection column)
        {
            PhyGeoDisaster phy = new PhyGeoDisaster();
            phy.GBCodeId = row["市县编码"].ToString();
            phy.DisasterType = ConvertHelper.GetEnumGeoDisasterByStr(row["灾害类型 "].ToString());
            phy.Name = row["灾害名称"].ToString();

            phy.CustomizeId = row["隐患点编号"].ToString();
            phy.Location = row["设区市"].ToString() +
                row["县（市）"] + row["乡镇"] + row["村"] + row["组"];

            //这种转化方法用于江西数据的特例


            //这个地方需要对江西省填入的X,Y值做判断。
            //1. dd-mm-ss格式
            int lon;
            if (row["X"].ToString().Contains('-'))
            {
                phy.Lon = LonLatHelper.ConvertToDegreeStyleFromString(row["X"].ToString());
                phy.Lat = LonLatHelper.ConvertToDegreeStyleFromString(row["Y"].ToString());
            }
            //度分秒格式
            else if (row["X"].ToString().Contains("度"))
            {
                string lonStr = row["X"].ToString().Replace("度", "-").Replace("分", "-").Replace("秒", "-");
                string latStr = row["Y"].ToString().Replace("度", "-").Replace("分", "-").Replace("秒", "-");

                phy.Lon = LonLatHelper.ConvertToDegreeStyleFromString(lonStr);
                phy.Lat = LonLatHelper.ConvertToDegreeStyleFromString(latStr);
            }
            //大地坐标

            else if (int.TryParse(row["X"].ToString(), out lon) && lon > 10000)
            {
                double lond = double.Parse(row["Y"].ToString());
                double latd = double.Parse(row["X"].ToString());
                double tlon, tlat;
                LonLatHelper.GaussToBL(lond, latd, out tlon, out tlat);
                phy.Lon = tlon;
                phy.Lat = tlat;
            }
            else if (row["X"].ToString().Contains('.'))
            {
                phy.Lon = double.Parse(row["X"].ToString());
                phy.Lat = double.Parse(row["Y"].ToString());
            }




            phy.IsThreat = true;

            return phy;
        }


        public void GetLonlatByStr(string x, string y, out double Lon, out double Lat)
        {
            int lon;
            if (x.Contains('-'))
            {
                Lon = LonLatHelper.ConvertToDegreeStyleFromString(x);
                Lat = LonLatHelper.ConvertToDegreeStyleFromString(y);
            }
            //度分秒格式
            else if (x.Contains("度"))
            {
                string lonStr = x.Replace("度", "-").Replace("分", "-").Replace("秒", "-");
                string latStr = x.Replace("度", "-").Replace("分", "-").Replace("秒", "-");

                Lon = LonLatHelper.ConvertToDegreeStyleFromString(lonStr);
                Lat = LonLatHelper.ConvertToDegreeStyleFromString(latStr);
            }
            //大地坐标

            else if (int.TryParse(x, out lon) && lon > 10000)
            {
                double lond = double.Parse(x);
                double latd = double.Parse(y);
                double tlon, tlat;
                LonLatHelper.GaussToBL(lond, latd, out tlon, out tlat);
                Lon = tlon;
                Lat = tlat;
            }
            else if (x.Contains('.'))
            {
                Lon = double.Parse(x);
                Lat = double.Parse(y);
            }

            Lon = 0;
            Lat = 0;
        }
    }
}
