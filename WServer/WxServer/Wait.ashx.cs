using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using XJWZCatering.BLL;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
using XJWZCatering.SQL;
using XJWZCatering.WServices;

namespace XJWZCatering.WServer.WxServer
{
    /// <summary>
    /// Wait 的摘要说明
    /// </summary>
    public class Wait : ServiceBase
    {

        DataTable dt = new DataTable();
        operatelogEntity logentity = new operatelogEntity();
        bllWX_busWait bll = new bllWX_busWait();

        public override void ProcessRequest(HttpContext context)
        {
            if (CheckParameters(context))//检测是否合法
            {
                Dictionary<string, object> dicPar = GetParameters();
                if (dicPar != null)
                {
                    logentity.module = "客户排队表";
                    switch (actionname.ToLower())
                    {
                        case "referwaitinfo":
                            ReferWaitInfo(dicPar);
                            break;
                        case "addwaitinfo":
                            AddWaitInfo(dicPar);
                            break;
                        case "waitlist":
                            WaitList(dicPar);
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// 刷新当前排队信息
        /// </summary>
        /// <param name="dicPar"></param>
        private void ReferWaitInfo(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "bwid" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            //获取参数信息
            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string bwid = dicPar["bwid"].ToString();

            dt = new bllPaging().GetDataTableInfoBySQL("select wb.bwid,wsl.minperosn as minperson,wsl.maxperson,wb.sortNum,convert(varchar(20),wb.waitTime,120) as waitTime,wb.status,dbo.fngetbuswaitcounts(wsl.lineid,wb.tel,wb.strcode) as linecount from wx_buswait wb left join wx_setlineup wsl on wb.lineid=wsl.lineid where convert(varchar(10),wb.ctime,120)=convert(varchar(10),getdate(),120) and wb.bwid=" + bwid);

            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "没有查询到排队记录，请稍后再试");
            }
        }

        //排队
        private void AddWaitInfo(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "lineid", "stocode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            //获取参数信息
            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string lineid = dicPar["lineid"].ToString();
            string stocode = dicPar["stocode"].ToString();

            string mescode = string.Empty;
            dt = bll.AddWaitInfo(USER_ID, lineid, stocode, ref mescode);
            switch (mescode)
            {
                case "0":
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        try
                        {
                            string stoname = dt.Rows[0]["stoname"].ToString();
                            var host = Helper.GetAppSettings("VipHostUrl");
                            var url = host + "/dist/index.html#/pdjl?GUID=1@USER_ID=" + USER_ID;
                            string waitno = dt.Rows[0]["sortNum"].ToString();
                            string tabletype = dt.Rows[0]["minperson"].ToString() + "-" + dt.Rows[0]["maxperson"].ToString() + "人桌";
                            string linecount = dt.Rows[0]["linecount"].ToString() + "桌";

                            //微信推送
                            string InterfaceUrl = host + "/WServer/WxServer/WxTemplate.ashx";
                            string DetailParameters = "actionname={0}&parameters={{\"USER_ID\":'{1}',\"url\":\"{2}\",\"stoname\":\"{3}\",\"waitno\":\"{4}\",\"tabtype\":\"{5}\",\"tabnum\":\"{6}\"}}";
                            StringBuilder postStr = new StringBuilder();
                            string[] arrPar = new string[] { "queuemsg", USER_ID, url, stoname, waitno, tabletype, linecount };
                            postStr.Append(string.Format(DetailParameters, arrPar));//键值对
                            string jsonStr = Helper.HttpWebRequestByURL(InterfaceUrl, postStr);
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.WriteErrorMessage(ex.ToString());
                        }
                        finally
                        {
                            ReturnListJson(dt);
                        }
                    }
                    else
                    {
                        ToCustomerJson("2", "网络繁忙，请稍后再试");
                    }
                    break;
                case "1":
                    ToCustomerJson("1", "门店未开启排队");
                    break;
                case "3":
                    ToCustomerJson("3", "请先绑定手机号");
                    break;
                default:
                    ToCustomerJson("1", "您当前存在有效排队记录");
                    break;
            }
        }

        //排队记录
        private void WaitList(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "type", "currentpage", "pagesize" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            //获取参数信息
            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string type = dicPar["type"].ToString(); //0:近一个月 1：更早
            string currentpage = dicPar["currentpage"].ToString();
            string pagesize = dicPar["pagesize"].ToString();
            string sumcount = string.Empty;
            dt = bll.GetWaitList(USER_ID, type, currentpage, pagesize, ref sumcount);
            int scount = Helper.StringToInt(sumcount);
            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    jsonStr += "{\"bwid\":\"" + row["bwid"] + "\",\"stocode\":\"" + row["stocode"] + "\",\"stoname\":\"" + row["stoname"] + "\",\"minperson\":\"" + row["minperson"] + "\",\"maxperson\":\"" + row["maxperson"] + "\",\"sortNum\":\"" + row["sortNum"] + "\",\"waitTime\":\"" + row["waitTime"] + "\",\"status\":\"" + row["status"] + "\",\"linecount\":\"" + row["linecount"] + "\",\"ptype\":\"" + row["ptype"] + "\",\"dictype\":\"" + row["firtype"] + "\",\"tel\":\"" + row["tel"] + "\",\"isdc\":\"" + row["isdc"] + "\"},";
                }
                jsonStr = jsonStr.TrimEnd(',');
                jsonStr += "],";
                if (scount <= Helper.StringToInt(pagesize) * Helper.StringToInt(currentpage))
                {
                    jsonStr += "\"isnextpage\":\"0\"}";
                }
                else
                {
                    jsonStr += "\"isnextpage\":\"1\"}";
                }
                ToJsonStr(jsonStr);
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }
    }
}