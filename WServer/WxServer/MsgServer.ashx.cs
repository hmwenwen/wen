using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using XJWZCatering.BLL;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
using XJWZCatering.WServices;


namespace XJWZCatering.WServer.WxServer
{
    /// <summary>
    /// MsgServer 的摘要说明
    /// </summary>
    public class MsgServer : ServiceBase
    {
        LoginUniqueness bll = new LoginUniqueness();
        DataTable dt = new DataTable();
        operatelogEntity logentity = new operatelogEntity();
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="context"></param>
        public override void ProcessRequest(HttpContext context)
        {
            if (CheckParameters(context))//检测是否合法
            {
                Dictionary<string, object> dicPar = GetParameters();
                if (dicPar != null)
                {
                    logentity.module = "短信";
                    switch (actionname.ToLower())
                    {
                        case "sendmsg":
                            SendMsg(dicPar);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="dicPar">mobile.手机号  ref vercode.验证码</param>
        /// <returns>0.成功 -1.失败</returns>
        public void SendMsg(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "mobile", "descr" };
            if (!CheckActionParameters(dicPar, pra))
            {
                return;

            }
            string mobile = dicPar["mobile"].ToString();
            string descr = dicPar["descr"].ToString();
            string vercode = dicPar["vercode"].ToString();
            //var flag = LoginUniqueness.MobileMesSendByTemp1(mobile, descr, ref vercode);
            //if (flag)
            //{
            //    ToCustomerJson("0", vercode); //成功
            //}
            //else
            //{
            //    ToCustomerJson("-1", ErrMessage.GetMessageInfoByCode("mobile_fail").Body); //失败
            //}

            var postdata = "actionname=sendmsg&&parameters={'mobile':'" + mobile + "','descr':'" + descr + "','vercode':'66666','buscode':'" + Helper.GetAppSettings("mesBuscode") + "','stocode':'" + ConfigurationManager.AppSettings["mesStocode"].ToString() + "'}";
            var resultStr = Helper.HttpWebRequestByURL2(ConfigurationManager.AppSettings["msgServer"].ToString() + "IsystemSet/WSAliyunSendMsg.ashx", postdata);
            ToCustomerJson(JsonHelper.GetJsonValByKey(resultStr, "status"), JsonHelper.GetJsonValByKey(resultStr, "mes"));

        }
    }
}