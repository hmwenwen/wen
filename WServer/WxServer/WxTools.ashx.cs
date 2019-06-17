using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using XJWZCatering.LinkPubWx;
using XJWZCatering.CommonBasic;
using XJWZCatering.WXHelper;
namespace XJWZCatering.WServer.WxServer
{
    /// <summary>
    /// WxTools 的摘要说明
    /// </summary>
    public class WxTools : IHttpHandler
    {
        public static readonly string appId = ConfigurationManager.AppSettings["appid"];
        public static readonly string secret = ConfigurationManager.AppSettings["secret"];

        public void ProcessRequest(HttpContext context)
        {
            var funName = context.Request["FunName"];
            var result = string.Empty;
            switch (funName)
            {
                case "getOpenid":
                    result = getOpenid(context.Request["code"]);
                    break;
            }

            context.Response.Clear();
            context.Response.Write(result);
            context.Response.End();
        }

        public string getOpenid(string code)
        {
            var openid = string.Empty;
            BaseWeb bw = new BaseWeb();
            var userbaseurl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appId + "&secret=" + secret + "&code=" + code + "&grant_type=authorization_code";
            var userinfo = bw.WXWebRequest(userbaseurl, string.Empty);
            openid = JsonHelper.GetJsonValByKey(userinfo, "openid");
            var sql = "select openid from WX_members_wx where wxopenid='" + openid + "'";
            openid = Convert.ToString(SQL.SQLTool.ExecuteScalar(sql));
            return openid;
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