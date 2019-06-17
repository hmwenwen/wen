using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using XJWZCatering.BLL;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
using XJWZCatering.WServices;

namespace XJWZCatering.WServer.WxServer
{
    /// <summary>
    /// IForm 的摘要说明
    /// </summary>
    public class IForm : ServiceBase
    {
        DataTable dt = new DataTable();
        operatelogEntity logentity = new operatelogEntity();
        bllForm bll = new bllForm();
        bllOrder bllo = new bllOrder();

        public override void ProcessRequest(HttpContext context)
        {
            if (CheckParameters(context))//检测是否合法
            {
                Dictionary<string, object> dicPar = GetParameters();
                if (dicPar != null)
                {
                    logentity.module = "Form对接接口信息";
                    switch (actionname.ToLower())
                    {
                        case "getreservelist":
                            GetReserveList(dicPar);
                            break;
                        case "getreservelist2":
                            GetReserveList2(dicPar);
                            break;
                        case "modifyreserveinfo":
                            ModifyReserveInfo(dicPar);
                            break;
                        case "addreserve":
                            AddReserve(dicPar);
                            break;
                        case "modifyreserve":
                            ModifyReserve(dicPar);
                            break;
                        case "cancelreserve":
                            CancelReserve(dicPar);
                            break;
                        case "getwaitlist":
                            GetWaitList(dicPar);
                            break;
                        case "addwaitinfo":
                            AddWaitInfo(dicPar);
                            break;
                        case "modifywaitstatus":
                            ModifyWaitStatus(dicPar);
                            break;
                        case "modifyorderstatus":
                            ModifyOrderStatus(dicPar);
                            break;
                        case "getorderinfo":
                            GetOrderInfo(dicPar);
                            break;
                        case "getwaitsetinfo":
                            GetWaitSetInfo(dicPar);
                            break;
                        case "openorclosewait":
                            OpenOrCloseWait(dicPar);
                            break;
                        case "resetwait":
                            ResetWait(dicPar);
                            break;
                        case "changeprintstatus":
                            ChangePrintStatus(dicPar);
                            break;
                        case "getorderinfobydate":
                            GetOrderInfoByDate(dicPar);
                            break;
                        case "updateorderstatus":
                            UpdateOrderStatus(dicPar);
                            break;
                    }
                }
            }
        }

        //门店关闭/开启排队
        private void OpenOrCloseWait(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "status" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string status = dicPar["status"].ToString();

            int count = new BLL.bllPaging().ExecuteNonQueryBySQL("update WX_stoset set isqueue='" + status + "' where stocode='" + stocode + "'");

            if (count >= 0)
            {
                ToCustomerJson("0", "设置成功");
            }
            else
            {
                ToCustomerJson("2", "设置失败，请稍后再试");
            }
        }

        //预定列表
        private void GetReserveList(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "rid", "resdate", "restime", "metcode", "restatus", "mobile", "name", "currentpage", "pagesize", "stocode", "dicid", "ttcode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string rid = dicPar["rid"].ToString();
            string resdate = dicPar["resdate"].ToString();
            string restime = dicPar["restime"].ToString();
            string metcode = dicPar["metcode"].ToString();
            string restatus = dicPar["restatus"].ToString();
            string mobile = dicPar["mobile"].ToString();
            string name = dicPar["name"].ToString();
            string currentpage = dicPar["currentpage"].ToString();
            string pagesize = dicPar["pagesize"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string dicid = dicPar["dicid"].ToString();
            string ttcode = dicPar["ttcode"].ToString();
            //string cuser = dicPar["cuser"].ToString();
            //string cname = dicPar["cname"].ToString();
            if (!string.IsNullOrEmpty(restime))
            {
                restime = restime.Split(':')[0] + ":" + restime.Split(':')[1];
            }
            string sumcount = string.Empty;
            dt = bll.GetReserveList(rid, resdate, restime, metcode, restatus, mobile, name, currentpage, pagesize, stocode, dicid, ttcode, ref sumcount);
            int scount = Helper.StringToInt(sumcount);
            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    jsonStr += "{\"id\":\"" + row["id"] + "\",\"desDate\":\"" + row["desDate"] + "\",\"desTime\":\"" + row["desTime"] + "\",\"personNum\":\"" + row["personNum"] + "\",\"dicid\":\"" + row["dicid"] + "\",\"metcode\":\"" + row["metcode"] + "\",\"metname\":\"" + row["metname"] + "\",\"userName\":\"" + row["userName"] + "\",\"tel\":\"" + row["tel"] + "\",\"remark\":\"" + row["remark"] + "\",\"status\":\"" + row["status"] + "\",\"ctime\":\"" + row["ctime"] + "\",\"dishesremark\":\"" + row["dishesremark"] + "\",\"sex\":\"" + row["sex"] + "\",\"isvip\":\"" + row["isvip"] + "\",\"cancelcount\":\"" + row["cancelcount"] + "\",\"yqcount\":\"" + row["yqcount"] + "\",\"ydcount\":\"" + row["ydcount"] + "\",\"ttcode\":\"" + row["ttcode"] + "\",\"cuser\":\"" + row["cuser"] + "\",\"cname\":\"" + row["cname"] + "\",\"tcid\":\"" + row["tcid"] + "\",\"tcname\":\"" + row["tcname"] + "\"},";
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

        //预定列表（按开始时间 结束时间 状态 查询）
        private void GetReserveList2(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "resdate", "resstime", "resetime", "restatus", "currentpage", "pagesize" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string resdate = dicPar["resdate"].ToString();
            string resstime = dicPar["resstime"].ToString();
            string resetime = dicPar["resetime"].ToString();
            string restatus = dicPar["restatus"].ToString();
            string currentpage = dicPar["currentpage"].ToString();
            string pagesize = dicPar["pagesize"].ToString();

            if (!string.IsNullOrEmpty(resstime))
            {
                resstime = resstime.Split(':')[0] + ":" + resstime.Split(':')[1];
            }

            if (!string.IsNullOrEmpty(resetime))
            {
                resetime = resetime.Split(':')[0] + ":" + resetime.Split(':')[1];
            }

            string sumcount = string.Empty;
            dt = bll.GetReserveList2(stocode, resdate, resstime, resetime, restatus, currentpage, pagesize, ref sumcount);
            int scount = Helper.StringToInt(sumcount);
            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    jsonStr += "{\"id\":\"" + row["id"] + "\",\"desDate\":\"" + row["desDate"] + "\",\"desTime\":\"" + row["desTime"] + "\",\"personNum\":\"" + row["personNum"] + "\",\"dicid\":\"" + row["dicid"] + "\",\"metcode\":\"" + row["metcode"] + "\",\"metname\":\"" + row["metname"] + "\",\"userName\":\"" + row["userName"] + "\",\"tel\":\"" + row["tel"] + "\",\"remark\":\"" + row["remark"] + "\",\"status\":\"" + row["status"] + "\",\"ctime\":\"" + row["ctime"] + "\",\"dishesremark\":\"" + row["dishesremark"] + "\",\"sex\":\"" + row["sex"] + "\",\"isvip\":\"" + row["isvip"] + "\",\"cancelcount\":\"" + row["cancelcount"] + "\",\"yqcount\":\"" + row["yqcount"] + "\",\"ydcount\":\"" + row["ydcount"] + "\",\"ttcode\":\"" + row["ttcode"] + "\",\"cuser\":\"" + row["cuser"] + "\",\"cname\":\"" + row["cname"] + "\",\"tcid\":\"" + row["tcid"] + "\",\"tcname\":\"" + row["tcname"] + "\"},";
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

        //更改客户预定状态及绑定桌台号
        private void ModifyReserveInfo(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "rid", "restatus", "metcode", "metname", "ttcode", "cuser", "cname" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string rid = dicPar["rid"].ToString();
            string status = dicPar["restatus"].ToString();
            string metcode = dicPar["metcode"].ToString();
            string metname = dicPar["metname"].ToString();
            string ttcode = dicPar["ttcode"].ToString();
            string cuser = dicPar["cuser"].ToString();
            string cname = dicPar["cname"].ToString();

            int count = bll.ModifyReserveInfo(rid, status, metcode, metname, ttcode, cuser, cname);

            if (count >= 0)
            {
                //发短信(商家接受预定)
                if (status == "0")
                {
                    //接受预定且绑定了桌台信息
                    try
                    {
                        dt = new bllPaging().GetDataTableInfoBySQL("select wb.buscode,wb.stocode,dbo.fngetstorename(wb.stocode) as stoname,wb.userName,wb.tel,wb.desDate,wb.desTime,wb.metname,s.tel as stel,wb.openid,s.address from WX_busDestine wb left join store s on wb.stocode=s.stocode where wb.id=" + rid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string buscode = dt.Rows[0]["buscode"].ToString();//商户编号
                            string stocode = dt.Rows[0]["stocode"].ToString();//门店编号
                            string stoname = dt.Rows[0]["stoname"].ToString();//门店名称
                            string username = dt.Rows[0]["userName"].ToString();//预定人名称
                            string tel = dt.Rows[0]["tel"].ToString(); //预定人电话
                            string metname1 = dt.Rows[0]["metname"].ToString();//桌台名称
                            string desDate = dt.Rows[0]["desDate"].ToString();//预定日期
                            string desTime = dt.Rows[0]["desTime"].ToString();//预定时间
                            string stel = dt.Rows[0]["stel"].ToString(); //门店联系电话
                            //具体预定时间（日期+时间）
                            string desDateTime = Helper.StringToDateTime(dt.Rows[0]["desDate"].ToString()).ToString("yyyy-MM-dd") + " " + desTime + ":00";
                            string openid = dt.Rows[0]["openid"].ToString(); //微信openid 餐收端预定的没有值
                            string address = dt.Rows[0]["address"].ToString();//门店地址

                            string url = Helper.GetAppSettings("VipPostUrl");

                            string InterfaceUrl = url + "/IsystemSet/WSsendMsg.ashx";

                            string DetailParameters = "actionname={0}&parameters={{\"mobile\":\"{1}\", \"username\":\"{2}\",\"resdatetime\":\"{3}\",\"metname\":\"{4}\",\"stoname\":\"{5}\",\"buscode\":\"{6}\",\"stocode\":\"{7}\",\"tel\":\"{8}\"}}";
                            StringBuilder postStr = new StringBuilder();
                            string[] arrPar = new string[] { "bookingInfo", tel, username, desDateTime, metname1, stoname, buscode, stocode, stel };
                            postStr.Append(string.Format(DetailParameters, arrPar));//键值对
                            string jsonStr = Helper.HttpWebRequestByURL(InterfaceUrl, postStr);

                            //微信推送
                            if (!string.IsNullOrEmpty(openid))
                            {
                                var host = Helper.GetAppSettings("VipHostUrl");
                                url = host + "/dist/index.html#/yyjl?GUID=1@USER_ID=" + openid;
                                //微信推送
                                InterfaceUrl = host + "/WServer/WxServer/WxTemplate.ashx";

                                DetailParameters = "actionname={0}&parameters={{\"USER_ID\":'{1}',\"url\":\"{2}\",\"stoname\":\"{3}\",\"time\":\"{4}\",\"location\":\"{5}\",\"address\":\"{6}\",\"tel\":\"{7}\",\"title\":\"{8}\"}}";
                                postStr.Clear();
                                arrPar = new string[] { "sendmsg", openid, url, stoname, desDateTime, metname1, address, tel, "您好，您的预定已成功" };
                                postStr.Append(string.Format(DetailParameters, arrPar));//键值对
                                jsonStr = Helper.HttpWebRequestByURL(InterfaceUrl, postStr);
                            }
                            //ErrorLog.WriteErrorMessage(jsonStr);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        ToCustomerJson("0", "更改信息成功");
                    }
                }
                else
                {
                    ToCustomerJson("0", "更改信息成功");
                }
            }
            else
            {
                ToCustomerJson("2", "未知错误，请稍后再试");
            }
        }

        //预定
        private void AddReserve(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "resdatetime", "personNum", "metcode", "metname", "phone", "name", "remark", "status", "TerminalType", "sex", "isvip", "ttcode", "cuser", "cname", "dicid", "tcid", "tcname" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string resdatetime = dicPar["resdatetime"].ToString();
            string personNum = dicPar["personNum"].ToString();
            string metcode = dicPar["metcode"].ToString();
            string metname = dicPar["metname"].ToString();
            string phone = dicPar["phone"].ToString();
            string name = dicPar["name"].ToString();
            string remark = dicPar["remark"].ToString();
            string status = dicPar["status"].ToString();
            string TerminalType = dicPar["TerminalType"].ToString();
            string sex = dicPar["sex"].ToString();
            string isvip = dicPar["isvip"].ToString();
            string ttcode = dicPar["ttcode"].ToString();
            string cuser = dicPar["cuser"].ToString();
            string cname = dicPar["cname"].ToString();
            string dicid = dicPar["dicid"].ToString();
            string tcid = dicPar["tcid"].ToString();
            string tcname = dicPar["tcname"].ToString();

            string resdate = resdatetime.Split(' ')[0];
            string restime = resdatetime.Split(' ')[1].Split(':')[0] + ":" + resdatetime.Split(' ')[1].Split(':')[1];
            string mescode = string.Empty;
            string rid = string.Empty;
            int count = bll.AddReserve(stocode, resdate, restime, personNum, metcode, metname, phone, name, ToJsonString(remark), status, TerminalType, sex, isvip, ttcode, cuser, cname, dicid, tcid, tcname, ref rid, ref mescode);

            if (count >= 0)
            {
                switch (mescode)
                {
                    case "0":
                        //发短信
                        if (status == "0")
                        {
                            try
                            {
                                dt = new bllPaging().GetDataTableInfoBySQL("select wb.buscode,wb.stocode,dbo.fngetstorename(wb.stocode) as stoname,wb.userName,wb.tel,wb.desDate,wb.desTime,wb.metname,s.tel as stel,wb.sex,wb.openid,s.address from WX_busDestine wb left join store s on wb.stocode=s.stocode where wb.id=" + rid);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    string buscode = dt.Rows[0]["buscode"].ToString();//商户编号
                                    string stocode1 = dt.Rows[0]["stocode"].ToString();//门店编号
                                    string stoname = dt.Rows[0]["stoname"].ToString();//门店名称
                                    string username = dt.Rows[0]["userName"].ToString();//预定人名称
                                    string tel = dt.Rows[0]["tel"].ToString(); //预定人电话
                                    string metname1 = dt.Rows[0]["metname"].ToString();//桌台名称
                                    string desDate = dt.Rows[0]["desDate"].ToString();//预定日期
                                    string desTime = dt.Rows[0]["desTime"].ToString();//预定时间
                                    string stel = dt.Rows[0]["stel"].ToString(); //门店联系电话
                                    string sexname = string.Empty;
                                    if (sex == "1")
                                    {
                                        sexname = "先生";
                                    }
                                    else
                                    {
                                        sexname = "女士";
                                    }
                                    //具体预定时间（日期+时间）
                                    string desDateTime = Helper.StringToDateTime(dt.Rows[0]["desDate"].ToString()).ToString("yyyy-MM-dd") + " " + desTime + ":00";
                                    string openid = dt.Rows[0]["openid"].ToString(); //微信openid 餐收端预定的没有值
                                    string address = dt.Rows[0]["address"].ToString();//门店地址

                                    string url = Helper.GetAppSettings("VipPostUrl");

                                    string InterfaceUrl = url + "/IsystemSet/WSsendMsg.ashx";

                                    string DetailParameters = "actionname={0}&parameters={{\"mobile\":\"{1}\", \"username\":\"{2}\",\"resdatetime\":\"{3}\",\"metname\":\"{4}\",\"stoname\":\"{5}\",\"buscode\":\"{6}\",\"stocode\":\"{7}\",\"tel\":\"{8}\"}}";
                                    StringBuilder postStr = new StringBuilder();
                                    string[] arrPar = new string[] { "bookingInfo", tel, username + sexname, desDateTime, metname1, stoname, buscode, stocode1, stel };
                                    postStr.Append(string.Format(DetailParameters, arrPar));//键值对
                                    string jsonStr = Helper.HttpWebRequestByURL(InterfaceUrl, postStr);

                                    //微信推送
                                    if (!string.IsNullOrEmpty(openid))
                                    {
                                        var host = Helper.GetAppSettings("VipHostUrl");
                                        url = host + "/dist/index.html#/yyjl?GUID=1@USER_ID=" + openid;
                                        //微信推送
                                        InterfaceUrl = host + "/WServer/WxServer/WxTemplate.ashx";

                                        DetailParameters = "actionname={0}&parameters={{\"USER_ID\":'{1}',\"url\":\"{2}\",\"stoname\":\"{3}\",\"time\":\"{4}\",\"location\":\"{5}\",\"address\":\"{6}\",\"tel\":\"{7}\",\"title\":\"{8}\"}}";
                                        postStr.Clear();
                                        arrPar = new string[] { "sendmsg", openid, url, stoname, desDateTime, metname1, address, tel, "您好，您的预定已成功" };
                                        postStr.Append(string.Format(DetailParameters, arrPar));//键值对
                                        jsonStr = Helper.HttpWebRequestByURL(InterfaceUrl, postStr);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteErrorMessage(ex.ToString());
                            }
                            finally
                            {
                                ToCustomerJson("0", "预定成功");
                            }
                        }
                        else
                        {
                            ToCustomerJson("0", "预定成功");
                        }
                        break;
                    case "3":
                        ToCustomerJson("1", "预定失败，没有查询到当前预定时间段信息");
                        break;
                    case "4":
                        ToCustomerJson("1", "预定失败，当前预定时间段已达上限");
                        break;
                }
            }
            else
            {
                ToCustomerJson("2", "预定失败，请稍后再试");
            }
        }

        //修改预定信息
        private void ModifyReserve(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "rid", "resdatetime", "personNum", "metcode", "metname", "phone", "name", "remark", "status", "TerminalType", "sex", "isvip", "ttcode", "dicid", "cuser", "cname", "tcid", "tcname" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string rid = dicPar["rid"].ToString();
            string resdatetime = dicPar["resdatetime"].ToString();
            string personNum = dicPar["personNum"].ToString();
            string metcode = dicPar["metcode"].ToString();
            string metname = dicPar["metname"].ToString();
            string phone = dicPar["phone"].ToString();
            string name = dicPar["name"].ToString();
            string remark = dicPar["remark"].ToString();
            string status = dicPar["status"].ToString();
            string TerminalType = dicPar["TerminalType"].ToString();
            string sex = dicPar["sex"].ToString();
            string isvip = dicPar["isvip"].ToString();
            string ttcode = dicPar["ttcode"].ToString();
            string dicid = dicPar["dicid"].ToString();
            string cuser = dicPar["cuser"].ToString();
            string cname = dicPar["cname"].ToString();
            string tcid = dicPar["tcid"].ToString();
            string tcname = dicPar["tcname"].ToString();

            string resdate = resdatetime.Split(' ')[0];
            string restime = resdatetime.Split(' ')[1].Split(':')[0] + ":" + resdatetime.Split(' ')[1].Split(':')[1];
            string sql = @" update WX_busDestine set desDate='" + resdate + "',desTime='" + FormatTime(restime) + "',personNum=" + personNum + ",metcode='" + metcode + "',metname='" + metname + "',userName='" + name + "',tel='" + phone + "',remark='" + ToJsonString(remark) + "',status='" + status + "',TerminalType='" + TerminalType + "',sex='" + sex + "',isvip='" + isvip + "',ttcode='" + ttcode + "',dicid=" + dicid + ",cuser=" + cuser + ",cname='" + cname + "',tcid='" + tcid + "',tcname='" + tcname + "' where id=" + rid;

            int count = new BLL.bllPaging().ExecuteNonQueryBySQL(sql);
            if (count >= 0)
            {
                //发短信
                if (status == "0")
                {

                    try
                    {
                        dt = new bllPaging().GetDataTableInfoBySQL("select wb.buscode,wb.stocode,dbo.fngetstorename(wb.stocode) as stoname,wb.userName,wb.tel,wb.desDate,wb.desTime,wb.metname,s.tel as stel,wb.openid,s.address from WX_busDestine wb left join store s on wb.stocode=s.stocode where wb.id=" + rid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string buscode = dt.Rows[0]["buscode"].ToString();//商户编号
                            string stocode = dt.Rows[0]["stocode"].ToString();//门店编号
                            string stoname = dt.Rows[0]["stoname"].ToString();//门店名称
                            string username = dt.Rows[0]["userName"].ToString();//预定人名称
                            string tel = dt.Rows[0]["tel"].ToString(); //预定人电话
                            string metname1 = dt.Rows[0]["metname"].ToString();//桌台名称
                            string desDate = dt.Rows[0]["desDate"].ToString();//预定日期
                            string desTime = dt.Rows[0]["desTime"].ToString();//预定时间
                            string stel = dt.Rows[0]["stel"].ToString(); //门店联系电话
                            string sexname = string.Empty;
                            if (sex == "1")
                            {
                                sexname = "先生/女士";
                            }
                            else
                            {
                                sexname = "先生/女士";
                            }
                            //具体预定时间（日期+时间）
                            string desDateTime = Helper.StringToDateTime(dt.Rows[0]["desDate"].ToString()).ToString("yyyy-MM-dd") + " " + desTime + ":00";
                            string openid = dt.Rows[0]["openid"].ToString(); //微信openid 餐收端预定的没有值
                            string address = dt.Rows[0]["address"].ToString();//门店地址

                            string url = Helper.GetAppSettings("msgServer");

                            string InterfaceUrl = url + "/IsystemSet/WSAliyunSendMsg.ashx";

                            string DetailParameters = "actionname={0}&parameters={{\"mobile\":\"{1}\", \"username\":\"{2}\",\"resdatetime\":\"{3}\",\"metname\":\"{4}\",\"stoname\":\"{5}\",\"buscode\":\"{6}\",\"stocode\":\"{7}\",\"tel\":\"{8}\"}}";
                            StringBuilder postStr = new StringBuilder();
                            string[] arrPar = new string[] { "bookinginfo", tel, username + sexname, desDateTime, metname1, stoname, buscode, stocode, stel };
                            postStr.Append(string.Format(DetailParameters, arrPar));//键值对
                            string jsonStr = Helper.HttpWebRequestByURL(InterfaceUrl, postStr);
                            ErrorLog.WriteErrorMessage("短信发送结果:" + jsonStr);
                            //微信推送
                            if (!string.IsNullOrEmpty(openid))
                            {
                                var host = Helper.GetAppSettings("VipHostUrl");
                                url = host + "/dist/index.html#/yyjl?GUID=1@USER_ID=" + openid;
                                //微信推送
                                InterfaceUrl = host + "/WServer/WxServer/WxTemplate.ashx";

                                DetailParameters = "actionname={0}&parameters={{\"USER_ID\":'{1}',\"url\":\"{2}\",\"stoname\":\"{3}\",\"time\":\"{4}\",\"location\":\"{5}\",\"address\":\"{6}\",\"tel\":\"{7}\",\"title\":\"{8}\"}}";
                                postStr.Clear();
                                arrPar = new string[] { "sendmsg", openid, url, stoname, desDateTime, metname1, address, stel, "您好，您的预定已成功" };
                                postStr.Append(string.Format(DetailParameters, arrPar));//键值对
                                jsonStr = Helper.HttpWebRequestByURL(InterfaceUrl, postStr);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteErrorMessage(ex.ToString());
                    }
                    finally
                    {
                        ToCustomerJson("0", "修改成功");
                    }
                }
                else
                {
                    ToCustomerJson("0", "修改成功");
                }
            }
            else
            {
                ToCustomerJson("2", "操作失败，请稍后再试");
            }
        }

        public string FormatTime(string restime)
        {
            string[] restime_arr = restime.Split(':');
            return restime_arr[0] + ":" + restime_arr[1];
        }

        //取消预定
        private void CancelReserve(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "rid", "cancelremark" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string rid = dicPar["rid"].ToString();
            string cancelremark = dicPar["cancelremark"].ToString();

            string sql = "update WX_busDestine set status='3',dishesremark='" + cancelremark + "' where id=" + rid;
            int count = new BLL.bllPaging().ExecuteNonQueryBySQL(sql);
            if (count >= 0)
            {
                //发微信推送
                try
                {
                    dt = new bllPaging().GetDataTableInfoBySQL("select wb.buscode,wb.stocode,dbo.fngetstorename(wb.stocode) as stoname,wb.userName,wb.tel,wb.desDate,wb.desTime,wb.metname,s.tel as stel,wb.openid,s.address from WX_busDestine wb left join store s on wb.stocode=s.stocode where wb.id=" + rid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string buscode = dt.Rows[0]["buscode"].ToString();//商户编号
                        string stocode = dt.Rows[0]["stocode"].ToString();//门店编号
                        string stoname = dt.Rows[0]["stoname"].ToString();//门店名称
                        string username = dt.Rows[0]["userName"].ToString();//预定人名称
                        string tel = dt.Rows[0]["tel"].ToString(); //预定人电话
                        string metname1 = dt.Rows[0]["metname"].ToString();//桌台名称
                        string desDate = dt.Rows[0]["desDate"].ToString();//预定日期
                        string desTime = dt.Rows[0]["desTime"].ToString();//预定时间
                        string stel = dt.Rows[0]["stel"].ToString(); //门店联系电话

                        //具体预定时间（日期+时间）
                        string desDateTime = Helper.StringToDateTime(dt.Rows[0]["desDate"].ToString()).ToString("yyyy-MM-dd") + " " + desTime + ":00";
                        string openid = dt.Rows[0]["openid"].ToString(); //微信openid 餐收端预定的没有值
                        string address = dt.Rows[0]["address"].ToString();//门店地址

                        //微信推送
                        if (!string.IsNullOrEmpty(openid))
                        {
                            var host = Helper.GetAppSettings("VipHostUrl");
                            string url = host + "/dist/index.html#/yyjl?GUID=1@USER_ID=" + openid;
                            //微信推送
                            string InterfaceUrl = host + "/WServer/WxServer/WxTemplate.ashx";

                            string DetailParameters = "actionname={0}&parameters={{\"USER_ID\":'{1}',\"url\":\"{2}\",\"title\":\"{3}\",\"project\":\"{4}\",\"time\":\"{5}\",\"remark\":\"{6}\"}}";
                            StringBuilder postStr = new StringBuilder();
                            string[] arrPar = new string[] { "creserve", openid, url, "您的预约已取消", "餐厅桌台预定", desDateTime, "本次预约已被取消，如有需要请重新预约" };
                            postStr.Append(string.Format(DetailParameters, arrPar));//键值对
                            string jsonStr = Helper.HttpWebRequestByURL(InterfaceUrl, postStr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorMessage(ex.ToString());
                }
                finally
                {
                    ToCustomerJson("0", "取消成功");
                }
            }
            else
            {
                ToCustomerJson("2", "操作失败，请稍后再试");
            }
        }

        //门店排队信息
        private void GetWaitList(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "lineid", "stocode", "mobile", "status", "currentpage", "pagesize" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string lineid = dicPar["lineid"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string mobile = dicPar["mobile"].ToString();
            string status = dicPar["status"].ToString();
            string currentpage = dicPar["currentpage"].ToString();
            string pagesize = dicPar["pagesize"].ToString();

            string sumcount = string.Empty;
            dt = bll.GetWaitList(stocode, mobile, lineid, status, currentpage, pagesize, ref sumcount);
            int scount = Helper.StringToInt(sumcount);
            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    jsonStr += "{\"rid\":\"" + row["bwid"] + "\",\"sortNum\":\"" + row["sortNum"] + "\",\"busDate\":\"" + row["busDate"] + "\",\"waitType\":\"" + row["waitType"] + "\",\"userName\":\"" + row["userName"] + "\",\"tel\":\"" + row["tel"] + "\",\"waitTime\":\"" + row["waitTime"] + "\",\"remark\":\"" + row["remark"] + "\",\"status\":\"" + row["status"] + "\",\"strcode\":\"" + row["strcode"] + "\",\"stoname\":\"" + row["stoname"] + "\",\"linecount\":\"" + row["linecount"] + "\",\"serialNumber\":\"" + row["serialNumber"] + "\",\"maxperson\":\"" + row["maxperson"] + "\",\"minperson\":\"" + row["minperson"] + "\",\"lineid\":\"" + row["lineid"] + "\"},";
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

        //排队设置信息
        public void GetWaitSetInfo(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();

            dt = new BLL.bllPaging().GetDataTableInfoBySQL("select minperosn as minperson,lineid,maxperson,dbo.fngetbuswaitcounts(lineid,'','" + stocode + "') as linecount from wx_setlineUp where status='1' and isdelete='0' and stocode='" + stocode + "';");

            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }

        //排队
        private void AddWaitInfo(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "lineid", "stocode", "mobile" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string lineid = dicPar["lineid"].ToString();
            string mobile = dicPar["mobile"].ToString();

            string mescode = string.Empty;
            dt = bll.AddWaitInfo(stocode, mobile, lineid, ref mescode);

            switch (mescode)
            {
                case "0":
                    if (dt != null && dt.Rows.Count >= 0)
                    {
                        //发短信
                        ReturnListJson(dt);
                    }
                    else
                    {
                        ToCustomerJson("2", "操作失败，请稍后再试");
                    }
                    break;
                default:
                    ToCustomerJson("1", "门店未开启排队");
                    break;
            }
        }

        //重新排队
        private void ResetWait(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "rid" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string rid = dicPar["rid"].ToString();

            string mescode = string.Empty;
            dt = bll.ResetWait(rid, ref mescode);
            switch (mescode)
            {
                case "0":
                    if (dt != null && dt.Rows.Count >= 0)
                    {
                        //发短信
                        ReturnListJson(dt);
                    }
                    else
                    {
                        ToCustomerJson("2", "操作失败，请稍后再试");
                    }
                    break;
                case "1":
                    ToCustomerJson("1", "门店未开启排队");
                    break;
            }
        }

        //修改排队状态
        private void ModifyWaitStatus(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "rid", "status" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string rid = dicPar["rid"].ToString();
            string status = dicPar["status"].ToString();

            int count = new BLL.bllPaging().ExecuteNonQueryBySQL("update wx_buswait set status='" + status + "' where bwid=" + rid);
            if (count >= 0)
            {
                //需要发短信不？
                ToCustomerJson("0", "修改成功");
            }
            else
            {
                ToCustomerJson("2", "操作失败，请稍后再试");
            }
        }

        //更改点餐状态（后支付门店挂单状态更改为下单状态，或取消）
        private void ModifyOrderStatus(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "orderno", "status" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string orderno = dicPar["orderno"].ToString();
            string status = dicPar["status"].ToString();

            int count = new BLL.bllPaging().ExecuteNonQueryBySQL("update wx_orderdetails set status='" + status + "' where orderno='" + orderno + "'");
            if (count >= 0)
            {
                //需要发短信不？
                ToCustomerJson("0", "修改成功");
            }
            else
            {
                ToCustomerJson("2", "操作失败，请稍后再试");
            }
        }

        //根据订单号获取即时订单信息
        private void GetOrderInfo(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "buscode", "orderno" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string buscode = dicPar["buscode"].ToString();
            string orderno = dicPar["orderno"].ToString();

            dt = bllo.GetOrderListByForm(stocode, orderno);

            if (dt != null && dt.Rows.Count > 0)
            {
                //套餐订单
                DataRow[] drPackage = dt.Select(" ispackage='1' ");
                DataRow[] drNoPackage = dt.Select(" porderdishesid=0 and ispackage='' ");

                string orderdishesid = string.Empty;
                string packageaddmoney = string.Empty; //套餐做法加价
                string comprice = string.Empty;//套餐价格
                string tccode = string.Empty;//套餐编号
                string tcname = string.Empty;//套餐名称
                string tcnum = string.Empty; //套餐数量
                string patypename = string.Empty;//支付方式名称
                string statusname = string.Empty;//支付状态名称
                string jsonstr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"orders\":[";
                //套餐订单
                if (drPackage.Length > 0)
                {
                    for (int i = 0; i < drPackage.Length; i++)
                    {
                        //套餐信息
                        orderdishesid = drPackage[i]["orderdishesid"].ToString();
                        tccode = drPackage[i]["discode"].ToString();
                        tcname = drPackage[i]["disname"].ToString();
                        tcnum = drPackage[i]["disnum"].ToString();
                        comprice = drPackage[i]["comprice"].ToString();
                        packageaddmoney = drPackage[i]["packageaddmoney"].ToString();
                        //单品信息
                        DataRow[] drPackageDetail = dt.Select(" porderdishesid=" + orderdishesid);
                        for (int j = 0; j < drPackageDetail.Length; j++)
                        {
                            if (drPackageDetail[j]["patype"].ToString() == "1")
                            {
                                patypename = "会员卡支付";
                            }
                            else if (drPackageDetail[j]["patype"].ToString() == "2")
                            {
                                patypename = "微信支付";
                            }
                            else
                            {
                                patypename = "未知";
                            }

                            if (drPackageDetail[j]["status"].ToString() == "0")
                            {
                                statusname = "待支付";
                            }
                            else if (drPackageDetail[j]["status"].ToString() == "1")
                            {
                                statusname = "待确认";
                            }
                            else if (drPackageDetail[j]["status"].ToString() == "2")
                            {
                                statusname = "待取餐";
                            }
                            else if (drPackageDetail[j]["status"].ToString() == "3")
                            {
                                statusname = "取消";
                            }
                            else if (drPackageDetail[j]["status"].ToString() == "4")
                            {
                                statusname = "已取餐";
                            }
                            else
                            {
                                statusname = "已退款";
                            }

                            jsonstr += "{\"orderno\":\"" + drPackageDetail[j]["orderno"].ToString() + "\",\"sumprice\":\"" + drPackageDetail[j]["sumprice"].ToString() + "\",\"discountprice\":\"" + drPackageDetail[j]["discountprice"].ToString() + "\",\"singlemoney\":\"" + drPackageDetail[j]["singlemoney"].ToString() + "\",\"privilegepre\":\"" + drPackageDetail[j]["privilegepre"].ToString() + "\",\"couname\":\"" + drPackageDetail[j]["couname"].ToString() + "\",\"checkcode\":\"" + drPackageDetail[j]["checkcode"].ToString() + "\",\"out_trade_no\":\"" + drPackageDetail[j]["out_trade_no"].ToString() + "\",\"qcCode\":\"" + drPackageDetail[j]["qcCode"].ToString() + "\",\"ctime\":\"" + drPackageDetail[j]["ctime"].ToString() + "\",\"paytime\":\"" + drPackageDetail[j]["paytime"].ToString() + "\",\"personcount\":\"" + drPackageDetail[j]["personcount"].ToString() + "\",\"discode\":\"" + drPackageDetail[j]["discode"].ToString() + "\",\"disname\":\"" + drPackageDetail[j]["disname"].ToString() + "\",\"distypecode\":\"" + drPackageDetail[j]["distypecode"].ToString() + "\",\"distypename\":\"" + drPackageDetail[j]["distypename"].ToString() + "\",\"disnum\":\"" + drPackageDetail[j]["disnum"].ToString() + "\",\"oneprice\":\"" + drPackageDetail[j]["oneprice"].ToString() + "\",\"comprice\":\"" + comprice + "\",\"ispackage\":\"1\",\"userName\":\"" + drPackageDetail[j]["nickname"].ToString() + "\",\"mobile\":\"" + drPackageDetail[j]["mobile"].ToString() + "\",\"packageaddmoney\":\"" + packageaddmoney + "\",\"methodmoney\":\"0\",\"tccode\":\"" + tccode + "\",\"tcname\":\"" + tcname + "\",\"tcnum\":\"" + tcnum + "\",\"status\":\"" + drPackageDetail[j]["status"].ToString() + "\",\"patypecode\":\"" + drPackageDetail[j]["patype"].ToString() + "\",\"patypename\":\"" + patypename + "\",\"unit\":\"" + drPackageDetail[j]["unit"].ToString() + "\",\"statusname\":\"" + statusname + "\",\"orderdishesid\":\"" + drPackageDetail[j]["orderdishesid"].ToString() + "\",\"porderdishesid\":\"" + drPackageDetail[j]["porderdishesid"].ToString() + "\",\"remark\":\"" + drPackageDetail[j]["remark"].ToString() + "\"},";
                        }
                    }
                }

                //单品
                if (drNoPackage.Length > 0)
                {
                    for (int j = 0; j < drNoPackage.Length; j++)
                    {
                        if (drNoPackage[j]["patype"].ToString() == "1")
                        {
                            patypename = "会员卡支付";
                        }
                        else if (drNoPackage[j]["patype"].ToString() == "2")
                        {
                            patypename = "微信支付";
                        }
                        else
                        {
                            patypename = "未知";
                        }

                        if (drNoPackage[j]["status"].ToString() == "0")
                        {
                            statusname = "待支付";
                        }
                        else if (drNoPackage[j]["status"].ToString() == "1")
                        {
                            statusname = "待确认";
                        }
                        else if (drNoPackage[j]["status"].ToString() == "2")
                        {
                            statusname = "待取餐";
                        }
                        else if (drNoPackage[j]["status"].ToString() == "3")
                        {
                            statusname = "取消";
                        }
                        else if (drNoPackage[j]["status"].ToString() == "4")
                        {
                            statusname = "已取餐";
                        }
                        else
                        {
                            statusname = "已退款";
                        }

                        jsonstr += "{\"orderno\":\"" + drNoPackage[j]["orderno"].ToString() + "\",\"sumprice\":\"" + drNoPackage[j]["sumprice"].ToString() + "\",\"discountprice\":\"" + drNoPackage[j]["discountprice"].ToString() + "\",\"singlemoney\":\"" + drNoPackage[j]["singlemoney"].ToString() + "\",\"privilegepre\":\"" + drNoPackage[j]["privilegepre"].ToString() + "\",\"couname\":\"" + drNoPackage[j]["couname"].ToString() + "\",\"checkcode\":\"" + drNoPackage[j]["checkcode"].ToString() + "\",\"out_trade_no\":\"" + drNoPackage[j]["out_trade_no"].ToString() + "\",\"qcCode\":\"" + drNoPackage[j]["qcCode"].ToString() + "\",\"ctime\":\"" + drNoPackage[j]["ctime"].ToString() + "\",\"paytime\":\"" + drNoPackage[j]["paytime"].ToString() + "\",\"personcount\":\"" + drNoPackage[j]["personcount"].ToString() + "\",\"discode\":\"" + drNoPackage[j]["discode"].ToString() + "\",\"disname\":\"" + drNoPackage[j]["disname"].ToString() + "\",\"distypecode\":\"" + drNoPackage[j]["distypecode"].ToString() + "\",\"distypename\":\"" + drNoPackage[j]["distypename"].ToString() + "\",\"disnum\":\"" + drNoPackage[j]["disnum"].ToString() + "\",\"oneprice\":\"" + drNoPackage[j]["oneprice"].ToString() + "\",\"comprice\":\"" + drNoPackage[j]["comprice"].ToString() + "\",\"ispackage\":\"0\",\"userName\":\"" + drNoPackage[j]["nickname"].ToString() + "\",\"mobile\":\"" + drNoPackage[j]["mobile"].ToString() + "\",\"packageaddmoney\":\"0\",\"methodmoney\":\"" + drNoPackage[j]["methodmoney"].ToString() + "\",\"tccode\":\"\",\"tcname\":\"\",\"tcnum\":\"0\",\"status\":\"" + drNoPackage[j]["status"].ToString() + "\",\"patypecode\":\"" + drNoPackage[j]["patype"].ToString() + "\",\"patypename\":\"" + patypename + "\",\"unit\":\"" + drNoPackage[j]["unit"].ToString() + "\",\"statusname\":\"" + statusname + "\",\"orderdishesid\":\"" + drNoPackage[j]["orderdishesid"].ToString() + "\",\"porderdishesid\":\"" + drNoPackage[j]["porderdishesid"].ToString() + "\",\"remark\":\"" + drNoPackage[j]["remark"].ToString() + "\"},";
                    }
                }

                jsonstr = jsonstr.TrimEnd(',');
                jsonstr += "]}";

                ToJsonStr(jsonstr);
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }

        //改变订单打印状态
        private void ChangePrintStatus(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "orderno", "stocode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string orderno = dicPar["orderno"].ToString();
            string stocode = dicPar["stocode"].ToString();

            string mescode = string.Empty;
            int count = bllo.ChangePrintStatusByForm(stocode, orderno, ref mescode);
            if (count >= 0)
            {
                switch (mescode)
                {
                    case "0":
                        ToSucessJson();
                        break;
                    case "1":
                        ToCustomerJson("1", "订单状态异常");
                        break;
                }
            }
            else
            {
                ToErrorJson();
            }
        }

        //根据时间区间及处理状态获取订单信息
        private void GetOrderInfoByDate(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "buscode", "startdate", "enddate", "currentpage", "pagesize", "isprint" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string buscode = dicPar["buscode"].ToString();
            string startdate = dicPar["startdate"].ToString();
            string enddate = dicPar["enddate"].ToString();
            string currentpage = dicPar["currentpage"].ToString();
            string pagesize = dicPar["pagesize"].ToString();
            string isprint = dicPar["isprint"].ToString();

            string sumcount = string.Empty;
            string patypename = string.Empty;//支付方式名称
            string statusname = string.Empty;//支付状态名称
            dt = bllo.GetOrderListDataByForm(stocode, startdate, enddate, currentpage, pagesize, isprint, ref sumcount);
            int scount = Helper.StringToInt(sumcount);

            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    if (row["patype"].ToString() == "1")
                    {
                        patypename = "会员卡支付";
                    }
                    else if (row["patype"].ToString() == "2")
                    {
                        patypename = "微信支付";
                    }
                    else
                    {
                        patypename = "未知";
                    }

                    if (row["status"].ToString() == "0")
                    {
                        statusname = "待支付";
                    }
                    else if (row["status"].ToString() == "1")
                    {
                        statusname = "待确认";
                    }
                    else if (row["status"].ToString() == "2")
                    {
                        statusname = "待取餐";
                    }
                    else if (row["status"].ToString() == "3")
                    {
                        statusname = "取消";
                    }
                    else if (row["status"].ToString() == "4")
                    {
                        statusname = "已取餐";
                    }
                    else
                    {
                        statusname = "已退款";
                    }
                    jsonStr += "{\"orderno\":\"" + row["orderno"] + "\",\"sumprice\":\"" + row["sumprice"] + "\",\"discountprice\":\"" + row["discountprice"] + "\",\"singlemoney\":\"" + row["singlemoney"] + "\",\"privilegepre\":\"" + row["privilegepre"] + "\",\"couname\":\"" + row["couname"] + "\",\"checkcode\":\"" + row["checkcode"] + "\",\"out_trade_no\":\"" + row["out_trade_no"] + "\",\"qcCode\":\"" + row["qcCode"] + "\",\"ctime\":\"" + row["ctime"] + "\",\"paytime\":\"" + row["paytime"] + "\",\"personcount\":\"" + row["personcount"] + "\",\"nickname\":\"" + row["nickname"] + "\",\"mobile\":\"" + row["mobile"] + "\",\"status\":\"" + row["status"] + "\",\"patypecode\":\"" + row["patype"].ToString() + "\",\"patypename\":\"" + patypename + "\",\"statusname\":\"" + statusname + "\"},";
                }
                jsonStr = jsonStr.TrimEnd(',');
                jsonStr += "],";
                if (scount <= Helper.StringToInt(pagesize) * Helper.StringToInt(currentpage))
                {
                    jsonStr += "\"isnextpage\":\"0\",";
                }
                else
                {
                    jsonStr += "\"isnextpage\":\"1\",";
                }

                jsonStr += "\"recordcount\":\"" + sumcount + "\"}";
                ToJsonStr(jsonStr);
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }

        //修改订单状态
        private void UpdateOrderStatus(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "orderno", "status", "repaycode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string orderno = dicPar["orderno"].ToString();
            string status = dicPar["status"].ToString();
            string repaycode = dicPar["repaycode"].ToString();

            int count = new bllPaging().ExecuteNonQueryBySQL(" update wx_orderdetails set status='" + status + "',out_refund_no='" + repaycode + "' where orderno='" + orderno + "';");

            if (count >= 0)
            {
                ToCustomerJson("0", "修改成功");
            }
            else
            {
                ToCustomerJson("2", "未知错误，请稍后再试");
            }
        }
    }
}