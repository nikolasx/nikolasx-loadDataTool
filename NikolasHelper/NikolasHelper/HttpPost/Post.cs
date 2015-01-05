using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NikolasHelper.HttpPost
{
    /// <summary>
    /// Http请求
    /// </summary>
    public class Post
    {

        public static string SendPost(string url)
        {
            try
            {
                //发送请求
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Timeout = 1000 * 3000;//50分钟
                HttpWebResponse rep = (HttpWebResponse)req.GetResponse();//得到请求结果
                Stream stream = rep.GetResponseStream();
                if (stream != null)
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string responseHtml = reader.ReadToEnd();
                        rep.Close();
                        return responseHtml;
                    }
                rep.Dispose();
                return null;//如果结果流为空，则返回为空
            }
            catch (Exception exp)
            {
                var temp = exp.Message;
                throw exp;//抛出异常
                //return null;//未查询到数据时，404错误
            }
            //return ErrorRestResult;
        }



        public static string SendPost(string url, string dataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                byte[] data = Encoding.UTF8.GetBytes(dataStr);
                request.CookieContainer = new CookieContainer();
                request.AllowAutoRedirect = true;
                request.ContentType = "application/json";
                //request.ContentType = "text/plain";
                request.ContentLength = data.Length;
                request.Timeout = 1000 * 3000;//50分钟
                Stream newstream = request.GetRequestStream();
                newstream.Write(data, 0, data.Length);//传输post的数据
                newstream.Flush();
                newstream.Close();
                newstream.Dispose();

                //获得返回数据
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                if (stream != null)
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string responseHtml = reader.ReadToEnd();
                        response.Close();
                        return responseHtml;
                    }
                response.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return ErrorRestResult;
        }
    }
}
