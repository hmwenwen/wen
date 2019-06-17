using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XJWZCatering.CommonBasic;
using XJWZCatering.LinkPubWx;
using XJWZCatering.WServices;
using XJWZCatering.WXHelper;

namespace XJWZCatering.WServer.WxServer
{
    /// <summary>
    /// WxTemplate 的摘要说明
    /// </summary>
    public class WxTemplate : ServiceBase
    {

        public override void ProcessRequest(HttpContext context)
        {
            if (CheckParameters(context))//检测是否合法
            {
                Dictionary<string, object> dicPar = GetParameters();
                if (dicPar != null)
                {
                    switch (actionname.ToLower())
                    {
                        case "sendmsg":
                            ReserveMsg(dicPar);
                            break;
                        case "servicemsg":
                            serviceMsg(dicPar);
                            break;
                        case "queuemsg":
                            QueueMsg(dicPar);
                            break;
                        case "creserve":
                            CancelReserve(dicPar);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 模版消息发送
        /// </summary>
        /// <param name="pData"></param>
        public void SenMsg(string postUrl, string pData)
        {
            BaseWeb bw = new BaseWeb();
            string access_token = Tools.getDbtoken();
            string url = postUrl + access_token;
            var result = bw.WXWebRequest(url, pData);
            var msg = JsonHelper.GetJsonValByKey(result, "errmsg");
            Log.WriteLog("msg", "wxtemp", msg);
            if (msg == "ok")
            {
                ToCustomerJson("0", "success");
            }
            else
            {
                ToCustomerJson("1", result);
            }
        }

        /// <summary>
        /// 预约消息提醒
        /// </summary>
        /// <param name="openid"></param>
        public void ReserveMsg(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "USER_ID", "title", "url", "stoname", "time", "location", "address", "tel" };

            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            var openid = SQL.SQLTool.GetFirststringField("SELECT wxopenid FROM WX_members_wx WHERE openid='" + dicPar["USER_ID"].ToString() + "'");

            var postData = "{\"touser\":\"" + openid + "\",\"template_id\":\"rn1ZCbNy6IjuydPCDTfENpxah6bpgRB-LRC9Ztpujyw\",\"url\":\"" + dicPar["url"].ToString() + "\",\"topcolor\":\"#FF0000\",\"data\":{\"first\":{\"value\":\"" + dicPar["title"].ToString() + "\",\"color\":\"#000000\"},\"keyword1\":{\"value\":\"" + dicPar["stoname"].ToString() + "\",\"color\":\"#FF0000\"},\"keyword2\":{\"value\":\"" + dicPar["time"].ToString() + "\",\"color\":\"#FF0000\"},\"keyword3\":{\"value\":\"" + dicPar["location"].ToString() + "\",\"color\":\"#FF0000\"},\"keyword4\":{\"value\":\"" + dicPar["address"].ToString() + "\",\"color\":\"#FF0000\"},\"remark\":{\"value\":\"我们期待您的光临，您的预订如有变更，请联系我们，" + dicPar["tel"].ToString() + "\",\"color\":\"#000000\"}}}";
            SenMsg("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=", postData);
        }

        /// <summary>
        /// 排队信息
        /// </summary>
        /// <param name="dicPar"></param>
        public void QueueMsg(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "USER_ID", "url", "stoname", "waitno", "tabtype", "tabnum" };

            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            var openid = SQL.SQLTool.GetFirststringField("SELECT wxopenid FROM WX_members_wx WHERE openid='" + dicPar["USER_ID"].ToString() + "'");
            var postData = "{\"touser\":\"" + openid + "\",\"template_id\":\"-VSPQzPMSKXrXJb9q0rcV3SVfNIctb5g6TyRsgNw81c\",\"url\":\"" + dicPar["url"].ToString() + "\",\"topcolor\":\"#FF0000\",\"data\":{\"first\":{\"value\":\"您好，领号成功，请留意！\",\"color\":\"#000000\"},\"keyword1\":{\"value\":\"" + dicPar["stoname"].ToString() + "\",\"color\":\"#FF0000\"},\"keyword2\":{\"value\":\"" + dicPar["waitno"].ToString() + "\",\"color\":\"#FF0000\"},\"keyword3\":{\"value\":\"" + dicPar["tabtype"].ToString() + "\",\"color\":\"#FF0000\"},\"keyword4\":{\"value\":\"" + dicPar["tabnum"].ToString() + "\",\"color\":\"#FF0000\"},\"remark\":{\"value\":\"***微信排队，可以随时查询哟！***\",\"color\":\"#000000\"}}}";
            SenMsg("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=", postData);
        }

        /// <summary>
        /// 客服消息（必须满足该用户在48小时内与公众号有交互）
        /// </summary>
        /// <param name="dicPar"></param>
        public void serviceMsg(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "USER_ID", "content", "type" };

            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            var type = dicPar["type"].ToString();
            var postData = string.Empty;
            if (type == "text")
            {
                postData = "{\"touser\":\"" + dicPar["USER_ID"].ToString() + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + dicPar["content"].ToString() + "\"}}";
            }

            SenMsg("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=", postData);
        }

        /// <summary>
        /// 取消预约消息提醒
        /// </summary>
        /// <param name="openid"></param>
        public void CancelReserve(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "USER_ID", "title", "url", "project", "time", "remark" };

            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            var openid = SQL.SQLTool.GetFirststringField("SELECT wxopenid FROM WX_members_wx WHERE openid='" + dicPar["USER_ID"].ToString() + "'");
            try
            {
                var postData = "{\"touser\":\"" + openid + "\",\"template_id\":\"G81Vx_mWmJ4JhtlaSjDCJqUTA7OogmhD-qD49eOzMsc\",\"url\":\"" + dicPar["url"].ToString() + "\",\"topcolor\":\"#FF0000\",\"data\":{\"first\":{\"value\":\"" + dicPar["title"].ToString() + "\",\"color\":\"#000000\"},\"keyword1\":{\"value\":\"" + dicPar["project"].ToString() + "\",\"color\":\"#FF0000\"},\"keyword2\":{\"value\":\"" + dicPar["time"].ToString() + "\",\"color\":\"#FF0000\"},\"keyword3\":{\"value\":\"已取消\",\"color\":\"#FF0000\"},\"remark\":{\"value\":\"" + dicPar["remark"].ToString() + "\",\"color\":\"#000000\"}}}";
                SenMsg("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=", postData);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}