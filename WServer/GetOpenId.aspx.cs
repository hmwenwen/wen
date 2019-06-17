using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XJWZCatering.CommonBasic;
using XJWZCatering.WServer.HelpTool;

namespace XJWZCatering.WServer
{
    public partial class GetOpenId : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Clear();
                ReGetOpenId();
                //ErrorLog.WriteErrorMessage("走完获取openid的方法之后，当前Session的值是：" + Session["openid"]);
            }
        }

        public void ReGetOpenId()
        {
            string url = Request.Url.AbsoluteUri;//获取当前url
            string appid = Helper.GetAppSettings("appid");
            string secret = Helper.GetAppSettings("secret");
            if (Session["openid"] == "" || Session["openid"] == null)
            {
                //先要判断是否是获取code后跳转过来的
                if (Request.QueryString["code"] == "" || Request.QueryString["code"] == null)
                {
                    //Code为空时，先获取Code
                    string GetCodeUrls = GetCodeUrl(url, appid);
                    Response.Redirect(GetCodeUrls);//先跳转到微信的服务器，取得code后会跳回来这页面的
                }
                else
                {
                    try
                    {
                        //Code非空，已经获取了code后跳回来啦，现在重新获取openid
                        string openid = "";
                        openid = GetOauthAccessOpenId(Request.QueryString["Code"], appid, secret);//重新取得用户的openid
                        Session["openid"] = openid;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteErrorMessage(ex.ToString());
                    }
                    finally
                    {
                        //Response.Write(Session["openid"]);
                        //Response.Redirect("http://wx.link-public.com/dist/index.html#/find?v=" + Guid.NewGuid() + "&openid=" + Session["openid"]);
                        Response.Redirect("aa.html?&openid=" + Session["openid"] + "&" + Guid.NewGuid() + "=" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                        //Page.RegisterStartupScript("gourl", "<script>gourl('" + Session["openid"] + "'); </script>");
                        //ClientScript.RegisterStartupScript(ClientScript.GetType(), "gourl", "<script>gourl('" + Session["openid"] + "');</script>");
                    }
                }
            }
            else
            {
                //Response.Write(Session["openid"]);
                //Response.Redirect("http://wx.link-public.com/dist/index.html#/find?v=" + Guid.NewGuid() + "&openid=" + Session["openid"]);
                Response.Redirect("aa.html?openid=" + Session["openid"] + "&" + Guid.NewGuid() + "=" + DateTime.Now.ToString("mmss"));
                //Page.RegisterStartupScript("gourl", "<script>gourl('" + Session["openid"] + "'); </script>");
                //ClientScript.RegisterStartupScript(ClientScript.GetType(), "gourl", "<script>gourl('" + Session["openid"] + "');</script>");
            }
        }

        #region 重新获取Code的跳转链接(没有用户授权的，只能获取基本信息）
        /// <summary>重新获取Code,以后面实现带着Code重新跳回目标页面(没有用户授权的，只能获取基本信息（openid））</summary>
        /// <param name="url">目标页面</param>
        /// <returns></returns>
        public static string GetCodeUrl(string url, string appid)
        {
            string CodeUrl = "";
            //对url进行编码
            url = System.Web.HttpUtility.UrlEncode(url);
            CodeUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + url + "?action=viewtest&response_type=code&scope=snsapi_base&state=1#wechat_redirect");

            return CodeUrl;
        }
        #endregion

        #region 以Code换取用户的openid、access_token
        /// <summary>根据Code获取用户的openid、access_token</summary>
        public static string GetOauthAccessOpenId(string code, string appid, string secret)
        {
            string Openid = "";
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + secret + "&code=" + code + "&grant_type=authorization_code";
            string gethtml = MyHttpHelper.HttpGet(url);
            //ErrorLog.WriteErrorMessage("拿到的url是：" + url);
            //ErrorLog.WriteErrorMessage("获取到的gethtml是" + gethtml);
            OAuth_Token ac = new OAuth_Token();
            ac = JsonHelper.AnalysisJson<OAuth_Token>(gethtml);
            //ErrorLog.WriteErrorMessage("能否从html里拿到openid=" + ac.openid);
            Openid = ac.openid;
            return Openid;
        }
        #endregion
    }
}