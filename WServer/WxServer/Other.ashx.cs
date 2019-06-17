using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XJWZCatering.WServices;
using XJWZCatering.SQL;
using XJWZCatering.LinkPubWx;
using XJWZCatering.BLL;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Net;

namespace XJWZCatering.WServer.WxServer
{
    /// <summary>
    /// Other 的摘要说明
    /// </summary>
    public class Other : ServiceBase
    {
        bllWXComplaints bll = new bllWXComplaints();
        bllReward bllr = new bllReward();
        bllHelp bllh = new bllHelp();
        bllWX_usermessage bllum = new bllWX_usermessage();
        bllCoupon bllc = new bllCoupon();
        bllOrder bllo = new bllOrder();
        DataTable dt = new DataTable();
        operatelogEntity logentity = new operatelogEntity();

        public override void ProcessRequest(HttpContext context)
        {
            if (CheckParameters(context))//检测是否合法
            {
                Dictionary<string, object> dicPar = GetParameters();
                if (dicPar != null)
                {
                    logentity.module = "其他信息";
                    switch (actionname.ToLower())
                    {
                        case "getlocationinfo":
                            GetLocationInfo(dicPar);
                            break;
                        case "usercomplain":
                            UserComplain(dicPar);
                            break;
                        case "complainlist":
                            ComplainList(dicPar);
                            break;
                        case "getstorelist":
                            GetStoreList(dicPar);
                            break;
                        case "userrewardinfo":
                            UserRewardInfo(dicPar);
                            break;
                        case "userreward":
                            UserReward(dicPar);
                            break;
                        case "updateuserreward":
                            UPdateUserReward(dicPar);
                            break;
                        case "helplist":
                            HelpList(dicPar);
                            break;
                        case "mymesinfo":
                            MyMesInfo(dicPar);
                            break;
                        case "mymeslist":
                            MyMesList(dicPar);
                            break;
                        case "delmymes":
                            DelMyMes(dicPar);
                            break;
                        case "updatemymesstatus":
                            UpdateMyMesStatus(dicPar);
                            break;
                        case "getcityandshopcircles":
                            GetCityAndShopCircles(dicPar);
                            break;
                        case "getcardandcouponlist":
                            GetCardAndCouponList(dicPar);
                            break;
                        case "getorderlist":
                            GetOrderList(dicPar);
                            break;
                        case "sendmsg":
                            SendMsg(dicPar);
                            break;
                        case "readcomplain":
                            ReadComplain(dicPar);
                            break;
                        case "getsqinfo":
                            GetSqInfo(dicPar);
                            break;
                        case "iscombo":
                            GetIsCombo(dicPar);
                            break;
                        case "webinfo":
                            GetWebInfo(dicPar);
                            break;
                    }
                }
            }
        }

        //获取城市及所属商圈
        private void GetCityAndShopCircles(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            DataSet ds = new bllPaging().GetDataSetInfoBySQL("select distinct cityid,dbo.fnGetCity(cityid) as ctiyname from store where status='1' and stocode not in ('12','18','zhcc001','LZKJ','PLAYH5','WXXL') and status='1';select sqcode,sqname,city from sqinfo where status='1' and isdelete='0';");

            if (ds.Tables.Count == 2)
            {
                DataTable dtCityInfo = ds.Tables[0];
                DataTable dtSqInfo = ds.Tables[1];

                if (dtCityInfo != null && dtCityInfo.Rows.Count > 0)
                {
                    String jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                    foreach (DataRow dr in dtCityInfo.Rows)
                    {
                        string cityStr = dr["cityid"].ToString();
                        jsonStr += "{\"citycode\":\"" + cityStr + "\",\"cityname\":\"" + dr["ctiyname"].ToString() + "\",\"shopcityinfo\":[";
                        DataRow[] dr_arr = dtSqInfo.Select(" city='" + cityStr + "'");
                        for (int i = 0; i < dr_arr.Length; i++)
                        {
                            jsonStr += "{\"code\":\"" + dr_arr[i]["sqcode"] + "\",\"name\":\"" + dr_arr[i]["sqname"] + "\"},";
                        }
                        jsonStr = jsonStr.TrimEnd(',');
                        jsonStr += "]},";
                    }
                    jsonStr = jsonStr.TrimEnd(',');
                    jsonStr += "]}";
                    ToJsonStr(jsonStr);
                }
                else
                {
                    ToCustomerJson("1", "暂无数据");
                }
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }

        }

        //获取商圈信息（小程序使用）
        public void GetSqInfo(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            dt = new bllPaging().GetDataTableInfoBySQL("select sqcode,sqname,city,dbo.fnGetCity(city) as cityname from sqinfo where status='1' and isdelete='0' order by city asc;");

            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }

        /// <summary>
        /// 获取地理信息
        /// </summary>
        public void GetLocationInfo(Dictionary<string, object> dicPar)
        {
            var strJson = string.Empty;
            if (Tools.GetCache("locationinfo") == null)
            {
                var sql = "select provinceid,province from provinces";
                var dt = SQLTool.ExecuteDataTable(sql);
                strJson = "[";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var province = dt.Rows[i]["province"].ToString();
                    province = province.TrimEnd('省');
                    province = province.TrimEnd('市');
                    strJson += "{\"province\":\"" + province + "\",\"city\":[";
                    sql = "select cityid,city from citys where parentid='" + dt.Rows[i]["provinceid"] + "'";
                    var cityDt = SQLTool.ExecuteDataTable(sql);
                    for (int j = 0; j < cityDt.Rows.Count; j++)
                    {
                        strJson += "{\"city\":\"" + cityDt.Rows[j]["city"] + "\",\"area\":[";
                        sql = "SELECT areaid,area,letter FROM areas where parentid='" + cityDt.Rows[j]["cityid"] + "'";
                        var areaDt = SQLTool.ExecuteDataTable(sql);
                        for (int x = 0; x < areaDt.Rows.Count; x++)
                        {
                            strJson += "\"" + areaDt.Rows[x]["area"] + "\",";
                        }
                        strJson = strJson.TrimEnd(',');
                        strJson += "]},";
                    }
                    strJson = strJson.TrimEnd(',');
                    strJson += "]},";
                }
                strJson = strJson.TrimEnd(',');
                strJson += "]";
                Tools.AddCacheLasting("locationinfo", strJson);
            }
            else
            {
                strJson = Convert.ToString(Tools.GetCache("locationinfo"));
            }

            HttpContext hc = HttpContext.Current;
            hc.Response.Clear();
            hc.Response.Write(strJson);
            hc.Response.End();
        }

        /// <summary>
        /// 投诉
        /// </summary>
        public void UserComplain(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stoname", "complaincontent" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stoname = dicPar["stoname"].ToString();
            string complaincontent = dicPar["complaincontent"].ToString();

            int count = bll.AddComplaints(USER_ID, stoname, Helper.ReplaceString(complaincontent));
            if (count >= 0)
            {
                ToCustomerJson("0", "投诉成功");
            }
            else
            {
                ToCustomerJson("1", "投诉失败，请稍后再试");
            }
        }

        /// <summary>
        /// 投诉列表
        /// </summary>
        /// <param name="dicPar"></param>
        public void ComplainList(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "pagesize", "currentpage" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string pagesize = dicPar["pagesize"].ToString();
            string currentpage = dicPar["currentpage"].ToString();

            string sumcount = String.Empty;
            dt = bll.GetComplaintsList(USER_ID, pagesize, currentpage, ref sumcount);
            int StoCount = Helper.StringToInt(sumcount);

            string jsonStr = String.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    jsonStr += "{\"id\":\"" + row["id"] + "\",\"stocode\":\"" + row["stocode"].ToString() + "\",\"stoname\":\"" + row["stoname"].ToString() + "\",\"comdate\":\"" + row["comdate"].ToString() + "\",\"aContent\":\"" + ToJsonString(row["aContent"].ToString()) + "\",\"enddate\":\"" + row["enddate"].ToString() + "\",\"opinion\":\"" + ToJsonString(row["opinion"].ToString()) + "\",\"releasePerson\":\"" + ToJsonString(row["releasePerson"].ToString()) + "\",\"isread\":\"" + row["isread"] + "\"},";
                }

                jsonStr = jsonStr.TrimEnd(',') + "],";
                if (StoCount <= Helper.StringToInt(pagesize) * Helper.StringToInt(currentpage))
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

        //读取投诉信息
        public void ReadComplain(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "id" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string id = dicPar["id"].ToString();

            var sql = "update WX_Complaints set isread='1' where actId=" + id;

            int count = new bllPaging().ExecuteNonQueryBySQL(sql);

            if (count >= 0)
            {
                ToCustomerJson("0", "操作成功");
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="dicPar"></param>
        public void GetStoreList(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "keywords" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string keywords = dicPar["keywords"].ToString();

            if (string.IsNullOrEmpty(keywords))
            {
                keywords = "##########";
            }

            DataTable dt = new bllPaging().GetDataTableInfoBySQL("select cname as stoname from store where cname like '%" + keywords + "%' and stocode not in ('12','18','zhcc001','LZKJ','WXXL','PLAYH5');");

            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }

        /// <summary>
        /// 打赏(返回打赏记录的id,用于更新打赏状态)
        /// </summary>
        /// <param name="dicPar"></param>
        public void UserReward(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "empcode", "point", "money", "rcontent" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string empcode = dicPar["empcode"].ToString();
            string point = dicPar["point"].ToString();
            string money = dicPar["money"].ToString();
            string rcontent = dicPar["rcontent"].ToString();

            string rid = string.Empty;
            string orderno = string.Empty;
            bllr.AddWXReward(USER_ID, empcode, money, point, rcontent, ref rid, ref orderno);
            if (!string.IsNullOrEmpty(rid) && rid != "0")
            {
                string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                jsonStr += "{\"rid\":\"" + rid + "\",\"orderno\":\"" + orderno + "\"}";
                jsonStr += "]}";
                ToJsonStr(jsonStr);
            }
            else
            {
                ToCustomerJson("1", "网络繁忙，请稍后再试");
            }
        }

        /// <summary>
        /// 更改打赏支付状态
        /// </summary>
        /// <param name="dicPar"></param>
        public void UPdateUserReward(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "empcode", "rid" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string rid = dicPar["rid"].ToString();
            string empcode = dicPar["empcode"].ToString();

            int count = bllr.UpdateWXRewardStatus(empcode, rid);
            if (count >= 0)
            {
                ToCustomerJson("0", "支付成功");
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        /// <summary>
        /// 获取用户打赏信息（展示用户打赏基本信息）
        /// </summary>
        /// <param name="dicPar"></param>
        public void UserRewardInfo(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "empcode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string empcode = dicPar["empcode"].ToString();
            int count = 0;

            DataTable dtUserInfo = new bllPaging().GetDataTableInfoBySQL("select strcode,cname from employee where ecode='" + empcode + "' and status='1';");
            if (dtUserInfo != null && dtUserInfo.Rows.Count > 0)
            {
                string stocode = dtUserInfo.Rows[0]["strcode"].ToString();
                string cname = dtUserInfo.Rows[0]["cname"].ToString();

                dt = bllr.GetWXRewardInfo(empcode, stocode);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string jsonStr1 = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                    foreach (DataRow row in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(row["moneyset"].ToString()))
                        {
                            count = 1;
                            break;
                        }
                        else
                        {
                            jsonStr1 += "{\"mcount\":\"" + row["moneys"] + "\",\"points\":\"" + row["points"] + "\",\"moneyset\":\"" + row["moneyset"] + "\",\"cname\":\"" + cname + "\"}";
                        }
                    }

                    if (count == 0)
                    {
                        jsonStr1 += "]}";
                        ToJsonStr(jsonStr1);
                    }
                    else
                    {
                        ToCustomerJson("1", "当前门店未开启打赏功能");
                    }
                }
                else
                {
                    ToCustomerJson("1", "当前门店未开启打赏功能");
                }
            }
            else
            {
                ToCustomerJson("1", "员工信息异常");
            }
        }

        /// <summary>
        /// 帮助中心
        /// </summary>
        public void HelpList(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "currentpage", "pagesize", "keywords" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string currentpage = dicPar["currentpage"].ToString();
            string pagesize = dicPar["pagesize"].ToString();
            string keywords = dicPar["keywords"].ToString();

            string sumcount = string.Empty;
            dt = bllh.GetHelpList(currentpage, pagesize, keywords, ref sumcount);
            int scount = Helper.StringToInt(sumcount);
            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    jsonStr += "{\"id\":\"" + row["id"] + "\",\"title\":\"" + ToJsonString(row["title"].ToString()) + "\",\"content\":\"" + ToJsonString(row["hcontent"].ToString()) + "\"},";
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

        /// <summary>
        /// 消息中心
        /// </summary>
        /// <param name="dicPar"></param>
        public void MyMesInfo(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();

            dt = bllum.MyMesInfo(USER_ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }

        //发送消息
        public void SendMsg(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "type", "msgtitle", "msgcontent" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string type = dicPar["type"].ToString();
            string msgtitle = dicPar["msgtitle"].ToString();
            string msgcontent = dicPar["msgcontent"].ToString();

            var sql = "insert into WX_usermessage values('" + USER_ID + "','" + type + "','0','" + msgcontent + "','" + msgtitle + "',getdate(),'0');";

            int count = new bllPaging().ExecuteNonQueryBySQL(sql);

            if (count >= 0)
            {
                ToCustomerJson("0", "操作成功");
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        /// <summary>
        /// 消息中心详情列表
        /// </summary>
        /// <param name="dicPar"></param>
        public void MyMesList(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "currentpage", "pagesize", "types" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string currentpage = dicPar["currentpage"].ToString();
            string pagesize = dicPar["pagesize"].ToString();
            string types = dicPar["types"].ToString();

            string sumcount = string.Empty;
            dt = bllum.MyMesList(currentpage, pagesize, USER_ID, types, ref sumcount);
            int scount = Helper.StringToInt(sumcount);

            string jsonStr = String.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    jsonStr += "{\"id\":\"" + row["id"] + "\",\"sid\":\"" + row["sid"].ToString() + "\",\"title\":\"" + ToJsonString(row["title"].ToString()) + "\",\"msgdetails\":\"" + ToJsonString(row["msgdetails"].ToString()) + "\",\"status\":\"" + row["status"].ToString() + "\",\"ctime\":\"" + row["ctime"].ToString() + "\"},";
                }

                jsonStr = jsonStr.TrimEnd(',') + "],";
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

        /// <summary>
        /// 批量删除消息
        /// </summary>
        /// <param name="dicPar"></param>
        public void DelMyMes(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "ids" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string ids = dicPar["ids"].ToString();

            int count = bllum.DelMesList(USER_ID, ids);
            if (count >= 0)
            {
                ToCustomerJson("0", "删除成功");
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        /// <summary>
        /// 将消息置为已读
        /// </summary>
        /// <param name="dicPar"></param>
        public void UpdateMyMesStatus(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "id" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string id = dicPar["id"].ToString();

            int count = new bllPaging().ExecuteNonQueryBySQL("update WX_usermessage set status='1' where id='" + id + "';");
            if (count >= 0)
            {
                ToCustomerJson("0", "操作成功");
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        //获取用户会员卡折扣信息及优惠券信息
        public void GetCardAndCouponList(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "money", "discodes" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string money = dicPar["money"].ToString();
            string discodes = dicPar["discodes"].ToString();

            DataSet ds = bllc.GetCardAndCouponList(USER_ID, stocode, money, discodes.Trim(','));

            if (ds.Tables.Count == 2)
            {
                DataTable dtCoupon = ds.Tables[0];
                DataTable dtCard = ds.Tables[1];

                if (dtCoupon == null && dtCoupon.Rows.Count == 0 && dtCard == null && dtCard.Rows.Count == 0)
                {
                    ToCustomerJson("1", "暂无可用的会员卡信息及优惠券信息");
                }
                else
                {
                    String jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":{";
                    if (dtCoupon != null && dtCoupon.Rows.Count > 0)
                    {
                        jsonStr += "\"couponinfo\":[";
                        foreach (DataRow row in dtCoupon.Rows)
                        {
                            jsonStr += "{\"couname\":\"" + row["couname"].ToString() + "\",\"checkcode\":\"" + row["checkcode"].ToString() + "\",\"singlemoney\":\"" + row["singlemoney"] + "\",\"maxmoney\":\"" + row["maxmoney"] + "\",\"discode\":\"" + row["discode"] + "\",\"ctype\":\"" + row["ctype"] + "\",\"disprice\":\"" + row["disprice"] + "\"},";//ctype:1 商品券
                        }
                        jsonStr = jsonStr.TrimEnd(',');
                        jsonStr += "],";
                    }

                    if (dtCard != null && dtCard.Rows.Count > 0)
                    {
                        jsonStr += "\"cardinfo\":[";
                        foreach (DataRow row in dtCard.Rows)
                        {
                            jsonStr += "{\"cardname\":\"" + row["cardlevel"].ToString() + "\",\"cardCode\":\"" + row["cardCode"].ToString() + "\",\"privilegepre\":\"" + row["privilegepre"].ToString() + "\",\"isecard\":\"" + row["isecard"] + "\",\"dispcode\":\"" + row["dispcode"].ToString() + "\",\"dispname\":\"" + row["dispname"].ToString() + "\",\"status\":\"" + row["status"].ToString() + "\"},";
                        }
                        jsonStr = jsonStr.TrimEnd(',');
                        jsonStr += "]";
                    }
                    jsonStr = jsonStr.TrimEnd(',') + "}}";
                    ToJsonStr(jsonStr);
                }
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        //点餐记录
        public void GetOrderList(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "type", "currentpage", "pagesize" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string type = dicPar["type"].ToString();
            string currentpage = dicPar["currentpage"].ToString();
            string pagesize = dicPar["pagesize"].ToString();

            string sumcount = string.Empty;
            dt = bllo.GetOrderList(USER_ID, type, currentpage, pagesize, ref sumcount);
            int scount = Helper.StringToInt(sumcount);

            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    jsonStr += "{\"orderno\":\"" + row["orderno"] + "\",\"stocode\":\"" + row["stocode"] + "\",\"stoname\":\"" + row["stoname"] + "\",\"payType\":\"" + row["payType"] + "\",\"paymoney\":\"" + row["paymoney"] + "\",\"ctime\":\"" + row["ctime"] + "\",\"status\":\"" + row["status"] + "\",\"ptype\":\"" + row["ptype"] + "\",\"tel\":\"" + row["tel"] + "\",\"dictype\":\"" + row["firtype"] + "\",\"patype\":\"" + row["patype"] + "\"},";
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

        /// <summary>
        /// 判断是否是套餐
        /// </summary>
        /// <param name="dicPar"></param>
        public void GetIsCombo(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "discode", "stocode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string discode = dicPar["discode"].ToString();
            string stocode = dicPar["stocode"].ToString();

            var sql = "SELECT iscombo FROM [dishes] WHERE discode='" + discode + "' AND stocode='" + stocode + "'";
            var result = SQL.XJWZSQLTool.GetFirststringField(sql);
            if (result == "1")
            {
                ToJsonStr("Y");
            }
            else
            {
                ToJsonStr("N");
            }

        }

        /// <summary>
        /// 获取官网信息
        /// </summary>
        /// <param name="dicPar"></param>
        public void GetWebInfo(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "btype", "stype" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            string btype = dicPar["btype"].ToString();
            string stype = dicPar["stype"].ToString();

            using (StreamReader file = File.OpenText(Pagcontext.Server.MapPath("~/config.json")))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject json = (JObject)JToken.ReadFrom(reader);
                    var obj = json[btype]["Info"];
                    if (!string.IsNullOrEmpty(stype))
                    {
                        foreach (JObject o in obj)
                        {
                            if (o["name"].ToString() == stype)
                            {
                                ToJsonStr(o.ToString());
                            }
                        }
                    }
                    else
                    {
                        ToJsonStr(obj.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string idcard(string path)
        {
            //https://ai.baidu.com/docs#/OCR-API/7e4792c7 接口参考
            Tools Tool = new Tools();
            var token = "24.96358732ba89f3aa6e5aefc14e51086f.2592000.1548589815.282335-15296391"; //Tool.GetCache("atoken");
            if (token == null)
            {
                token = Tools.getAccessToken();
                Tools.AddCache("atoken", token);
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
            return result;
        }
    }
}