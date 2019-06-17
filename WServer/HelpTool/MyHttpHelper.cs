using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace XJWZCatering.WServer.HelpTool
{
    /// <summary>HttpHelper的2次封装函数 作者：
    /// </summary>
    public class MyHttpHelper
    {
        #region 公共函数
        /// <summary>返回 HTML 字符串的编码结果</summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            return str.Length > 0 ? HttpUtility.HtmlEncode(str) : "";
        }

        /// <summary>返回 HTML 字符串的解码结果</summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            return str.Length > 0 ? HttpUtility.HtmlDecode(str) : "";
        }

        /// <summary>
        /// 根据指定的编码对RUl进行解码
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        /// <param name="encoding">要进行解码的编码方式</param>
        /// <returns></returns>
        public static string UrlDecode(string str, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            if (str.Length == 0)
            {
                return "";
            }

            if (encoding == null)
            {
                return HttpUtility.UrlDecode(str);
            }
            else
            {
                return HttpUtility.UrlDecode(str, encoding);
            }
        }

        /// <summary>根据指定的编码对URL进行编码</summary>
        /// <param name="str">要编码的URL</param>
        /// <param name="encoding">要进行编码的编码方式</param>
        /// <returns></returns>
        public static string UrlEncode(string str, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            if (str.Length == 0)
            {
                return "";
            }

            if (encoding == null)
            {
                return HttpUtility.UrlEncode(str);
            }
            else
            {
                return HttpUtility.UrlEncode(str, encoding);
            }
        }

        /// <summary>
        /// 根据 charSet 返回 Encoding
        /// </summary>
        /// <param name="charSet">"gb2312" or "utf-8",默认: "" ==  "utf-8"</param>
        /// <returns></returns>
        public static Encoding GetEncoding(string charSet)
        {
            Encoding en = Encoding.Default;

            if (charSet == "gb2312")
            {
                en = Encoding.GetEncoding("gb2312");
            }
            else if (charSet == "utf-8")
            {
                en = Encoding.UTF8;
            }
            return en;
        }


        #endregion

        #region Post

        /// <summary>HTTP Get方式请求数据</summary>
        /// <param name="url">URL</param>
        /// <param name="param">user=123123 & pwd=1231313"</param>
        /// <param name="charSet">"gb2312" or "utf-8",默认: "" ==  "utf-8"</param>
        /// <returns></returns>
        public static string HttpPost(string url, string param, string charSet = "utf-8")
        {
            HttpHelpers http = new HttpHelpers();
            HttpItem item = new HttpItem()
            {
                URL = url,
                Encoding = GetEncoding(charSet),//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                Method = "post",//URL     可选项 默认为Get
                Postdata = param
            };


            //得到HTML代码
            HttpResult result = http.GetHtml(item);

            //取出返回的Cookie
            //string cookie = result.Cookie;

            //返回的Html内容
            string html = result.Html;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return html;
            }

            //string statusCodeDescription = result.StatusDescription;
            return "";
        }
        #endregion

        #region Get
        /// <summary>HTTP Get方式请求数据</summary>
        /// <param name="url">URL</param>
        /// <param name="charSet">"gb2312" or "utf-8",默认: "" ==  "utf-8"</param>
        /// <returns></returns>
        public static string HttpGet(string url, string charSet = "utf-8")
        {
            HttpHelpers http = new HttpHelpers();
            HttpItem item = new HttpItem()
            {
                URL = url,
                Encoding = GetEncoding(charSet),
                Method = "get"
            };


            //得到HTML代码
            HttpResult result = http.GetHtml(item);

            //取出返回的Cookie
            //string cookie = result.Cookie;

            //返回的Html内容
            string html = result.Html;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return html;
            }
            //string statusCodeDescription = result.StatusDescription;
            return "";
        }
        /// <summary>POST客服消息/summary>
        /// <param name="url">URL</param>
        /// <param name="postData">内容</param>
        /// <returns>消息状态</returns>
        public static string GetPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...  
            try
            {
                // 设置参数  
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据  
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求  
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码  
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return err;
            }
        }
        #endregion

    }
}