using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using XJWZCatering.LinkPubWx;

namespace XJWZCatering.WServer.WxServer
{
    /// <summary>
    /// MPImgLoad 的摘要说明
    /// </summary>
    public class MPImgLoad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpFileCollection files = HttpContext.Current.Request.Files;
            var path = string.Empty;
            foreach (string key in files.AllKeys)
            {
                HttpPostedFile file = files[key];//file.ContentLength文件长度
                if (string.IsNullOrEmpty(file.FileName) == false)
                {
                    path = HttpContext.Current.Server.MapPath("~/imgcoll/") + file.FileName;
                    file.SaveAs(path);
                }
            }


            if (!string.IsNullOrEmpty(path))
            {
                var token = Tools.GetCache("atoken_2018");
                if (token == null)
                {
                    var tokens = (JObject)JsonConvert.DeserializeObject(Tools.getAccessToken());
                    token = tokens["access_token"];
                    Tools.AddCache("atoken_2018", token, 480);
                }
                string strbaser64 = Tools.ImgToBase64String(path); // 图片的base64编码
                string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/idcard?access_token=" + token;
                Encoding encoding = Encoding.Default;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
                request.Method = "post";
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;
                String str = "id_card_side=front&image=" + HttpUtility.UrlEncode(strbaser64);
                byte[] buffer = encoding.GetBytes(str);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                context.Response.Clear();
                context.Response.Write(result);

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}