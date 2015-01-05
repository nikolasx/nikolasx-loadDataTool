using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NikolasHelper.GIS
{
    public class LonLatHelper
    {
        /// <summary>
        /// 将ddmmss 形式的经纬度转换到以度为单位的表达形式
        /// 如1212518.91 代表121d 25m 18.91s
        /// </summary>
        /// <param name="d_m_s"></param>
        /// <returns></returns>
        public static double ConvertToDegreeStyle(string d_m_s)
        {
            var fentodu = 1 / 60;
            var miaoTodu = 1 / 3600;
            try
            {
                double[] dms = GetDMS(d_m_s);
                double degree = dms[0] + dms[1] * fentodu + dms[2] * miaoTodu;
                return degree;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static double[] GetDMS(string d_m_s)
        {
            if (!String.IsNullOrEmpty(d_m_s))
            {
                string s;
                string m;
                string d;
                if (!d_m_s.Contains("."))
                {
                    s = d_m_s.Substring(d_m_s.Length - 2, d_m_s.Length);
                    m = d_m_s.Substring(d_m_s.Length - 4, d_m_s.Length);
                    d = d_m_s.Substring(0, d_m_s.Length - 4);
                }
                else
                {
                    // 秒为小数的情况
                    string[] splits = d_m_s.Split('.');

                    //取整数部分后2位
                    s = splits[0].Substring(splits[0].Length - 2, splits[0].Length);
                    s = s + "." + splits[1];
                    m = splits[0].Substring(splits[0].Length - 4, splits[0].Length);
                    d = splits[0].Substring(0, splits.Length - 4);
                }
                double sN = double.Parse(s);
                double mN = double.Parse(m);
                double dN = double.Parse(d);
                return new double[] { dN, mN, sN };
            }
            else
            {
                throw new Exception("输入的度分秒为空值或格式不正确");
            }
        }

        /// <summary>
        /// 将ddmmss 形式的经纬度转换到以度为单位的double型数值
        /// 如121-25-18 代表121d 25m 18s
        /// </summary>
        /// <param name="d_m_s"></param>
        /// <returns></returns>
        public static double ConvertToDegreeStyleFromString(string d_m_s)
        {
            double fentodu = 1.0 / 60;
            double miaoTodu = 1.0 / 3600;
            try
            {
                double[] dms = GetDegreeFromString(d_m_s);
                double degree = dms[2] + dms[1] * fentodu + dms[0] * miaoTodu;
                return degree;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 度分秒的转换 格式为：112-30-19  江西经纬度格式
        /// </summary>
        /// <param name="d_m_s"></param>
        /// <returns></returns>
        public static double[] GetDegreeFromString(string d_m_s)
        {
            if (!String.IsNullOrEmpty(d_m_s))
            {
                string[] tempArr = d_m_s.Split(new Char[] { '-' });
                double sN = double.Parse(tempArr[0]);
                double mN = double.Parse(tempArr[1]);
                double dN = double.Parse(tempArr[2]);
                return new double[] { dN, mN, sN };
            }
            else
            {
                throw new Exception("输入的度分秒为空值或格式不正确");
            }
        }

        /// <summary>
        /// 将dd°mm′ss″形式的经纬度转换到以度为单位的double型数值
        /// </summary>
        /// <param name="d_m_s"></param>
        /// <returns></returns>
        public static double ConvertToDegreeStyleFromDegreeString(string d_m_s)
        {
            double fentodu = 1.0 / 60;
            double miaoTodu = 1.0 / 3600;
            try
            {
                double[] dms = GetDegreeFromDegreeString(d_m_s);
                double degree = dms[2] + dms[1] * fentodu + dms[0] * miaoTodu;
                return degree;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 度分秒格式转换 dd°mm′ss″
        /// </summary>
        /// <param name="d_m_s"></param>
        /// <returns></returns>
        public static double[] GetDegreeFromDegreeString(string d_m_s)
        {
            if (!String.IsNullOrEmpty(d_m_s))
            {
                string[] tempArr = d_m_s.Split(new Char[] { '°','′','″' });
                double sN = double.Parse(tempArr[0]);
                double mN = double.Parse(tempArr[1]);
                double dN = double.Parse(tempArr[2]);
                return new double[] { dN, mN, sN };
            }
            else
            {
                throw new Exception("输入的度分秒为空值或格式不正确");
            }
        }


        /// <summary>
        /// 高斯坐标反算成经纬度
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        public static void GaussToBL(double X, double Y, out double lon, out double lat)
        {

            int ProjNo;
            int ZoneWide; // //带宽
            double[] output = new double[2];
            double longitude1, latitude1, longitude0, X0, Y0, xval, yval;// latitude0,
            float e1, e2, f, a, ee, NN, T, C, M, D, R, u, fai, iPI;
            iPI = 0.0174532925199433F; // //3.1415926535898/180.0;
            //a = 6378245.0; f = 1.0/298.3; //54年北京坐标系参数
            a = 6378140.0F; f = 1 / 298.257F; // 80年西安坐标系参数
            ZoneWide = 6; // //6度带宽
            ProjNo = (int)(X / 1000000L); // 查找带号
            longitude0 = (ProjNo - 1) * ZoneWide + ZoneWide / 2;
            longitude0 = longitude0 * iPI; // 中央经线

            X0 = ProjNo * 1000000L + 500000L;
            Y0 = 0;
            xval = X - X0;
            yval = Y - Y0; // 带内大地坐标
            e2 = 2 * f - f * f;
            e1 = (float)(1.0 - Math.Sqrt(1 - e2)) / (float)(1.0 + Math.Sqrt(1 - e2));
            ee = e2 / (1 - e2);
            M = (float)yval;
            u = M / (a * (1 - e2 / 4 - 3 * e2 * e2 / 64 - 5 * e2 * e2 * e2 / 256));
            fai = u + (3 * e1 / 2 - 27 * e1 * e1 * e1 / 32) * (float)Math.Sin(2 * u)
                    + (21 * e1 * e1 / 16 - 55 * e1 * e1 * e1 * e1 / 32)
                    * (float)Math.Sin(4 * u) + (151 * e1 * e1 * e1 / 96) * (float)Math.Sin(6 * u)
                    + (1097 * e1 * e1 * e1 * e1 / 512) * (float)Math.Sin(8 * u);
            C = ee * (float)Math.Cos(fai) * (float)Math.Cos(fai);
            T = (float)Math.Tan(fai) * (float)Math.Tan(fai);
            NN = a / (float)Math.Sqrt(1.0 - e2 * Math.Sin(fai) * Math.Sin(fai));
            R = a
                    * (1 - e2)
                    / (float)Math.Sqrt((1 - e2 * Math.Sin(fai) * Math.Sin(fai))
                            * (1 - e2 * Math.Sin(fai) * Math.Sin(fai))
                            * (1 - e2 * Math.Sin(fai) * Math.Sin(fai)));
            D = (float)xval / NN;
            // 计算经度(Longitude) 纬度(Latitude)
            longitude1 = longitude0
                    + (D - (1 + 2 * T + C) * D * D * D / 6 + (5 - 2 * C + 28 * T
                            - 3 * C * C + 8 * ee + 24 * T * T)
                            * D * D * D * D * D / 120) / Math.Cos(fai);
            latitude1 = fai
                    - (NN * Math.Tan(fai) / R)
                    * (D * D / 2 - (5 + 3 * T + 10 * C - 4 * C * C - 9 * ee) * D
                            * D * D * D / 24 + (61 + 90 * T + 298 * C + 45 * T * T
                            - 256 * ee - 3 * C * C)
                            * D * D * D * D * D * D / 720);
            // 转换为度 DD
            lon = longitude1 / iPI;
            lat = latitude1 / iPI;
        }
    }
}
