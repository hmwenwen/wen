using System;
using System.Collections.Generic;
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
    /// IStore 的摘要说明
    /// </summary>
    public class IStore : ServiceBase
    {
        bllStore bll = new bllStore();
        DataTable dt = new DataTable();
        string imgurl = Helper.GetAppSettings("imgUrl");
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
                        case "findstore"://找店
                            FindStore(dicPar);
                            break;
                        case "getstoredetail"://门店详情
                            GetStoreDetail(dicPar);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 获取门店列表（微信公众号使用，找店）
        /// </summary>
        /// <param name="dicPar"></param>
        private void FindStore(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "pagesize", "currentpage", "citycode", "shopcircle", "keywords" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string pagesize = dicPar["pagesize"].ToString();  //分页记录数
            string currentpage = dicPar["currentpage"].ToString(); //当前页
            string citycode = dicPar["citycode"].ToString();  //城市
            string shopcircle = dicPar["shopcircle"].ToString(); //商圈
            string keywords = dicPar["keywords"].ToString();  //搜索关键字(店名/菜系/菜品名称)

            string sumcount = String.Empty;
            dt = bll.FindStore(pagesize, currentpage, citycode, shopcircle, keywords, ref sumcount);
            int StoCount = Helper.StringToInt(sumcount);

            string jsonStr = String.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    jsonStr += "{\"stocode\":\"" + row["stocode"].ToString() + "\",\"stoname\":\"" + row["cname"].ToString() + "\",\"aveconsume\":\"" + row["jprice"].ToString() + "\",\"classify\":\"" + row["dictype"].ToString() + "\",\"img\":\"" + imgurl + row["logo"].ToString() + "\",\"isyd\":\"" + row["isyd"].ToString() + "\",\"ispd\":\"" + row["ispd"].ToString() + "\",\"isdc\":\"" + row["isdc"].ToString() + "\"},";
                }

                jsonStr = jsonStr.TrimEnd(',') + "],";
                if (StoCount <= Helper.StringToInt(pagesize) * Helper.StringToInt(currentpage))
                {
                    jsonStr += "\"isnextpage\":\"0\",";
                }
                else
                {
                    jsonStr += "\"isnextpage\":\"1\",";
                }

                jsonStr += "\"sumcount\":\"" + StoCount.ToString() + "\"}";
                ToJsonStr(jsonStr);
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }

        /// <summary>
        /// 获取门店详情（微信公众号使用）
        /// </summary>
        /// <param name="dicPar"></param>
        private void GetStoreDetail(Dictionary<string, object> dicPar)
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

            DataSet ds = bll.GetStoDetail(stocode, USER_ID);

            if (ds.Tables.Count == 9)
            {
                //门店基本信息
                DataTable dtStoInfo = ds.Tables[0];
                //门店服务信息
                DataTable dtStoService = ds.Tables[1];
                //推荐菜信息
                DataTable dtDisheInfo = ds.Tables[2];
                //门店设置信息
                DataTable dtStoSetInfo = ds.Tables[3];
                //排队设置信息
                DataTable dtPdSetInfo = ds.Tables[4];
                //排队信息
                DataTable dtPdInfo = ds.Tables[5];
                //预定设置信息
                DataTable dtYdSetInfo = ds.Tables[6];
                //预定时间设置信息
                DataTable dtYdTimeSetInfo = ds.Tables[7];
                //节假日信息
                DataTable dtHolidayInfo = ds.Tables[8];
                //是否节假日接受预定
                string isfest = "1";
                //是否周末接受预定
                string isweek = "1";
                String jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":{";
                if (dtStoInfo != null && dtStoInfo.Rows.Count > 0)
                {
                    string stoimg = dtStoInfo.Rows[0]["stoimg"].ToString();
                    string strimgs = string.Empty;
                    if (!string.IsNullOrEmpty(stoimg))
                    {
                        string[] img_arr = stoimg.Trim(',').Split(',');
                        for (int i = 0; i < img_arr.Length; i++)
                        {
                            strimgs += imgurl + img_arr[i] + ",";
                        }
                    }
                    //门店基本信息
                    jsonStr += "\"stoinfo\":{\"stoimg\":\"" + strimgs.Trim(',') + "\",\"stoname\":\"" + dtStoInfo.Rows[0]["stoname"].ToString() + "\",\"dictype\":\"" + dtStoInfo.Rows[0]["dictype"].ToString() + "\",\"jprice\":\"" + dtStoInfo.Rows[0]["jprice"].ToString() + "\",\"tel\":\"" + dtStoInfo.Rows[0]["tel"].ToString() + "\",\"stoprincipaltel\":\"" + dtStoInfo.Rows[0]["stoprincipaltel"].ToString() + "\",\"saddress\":\"" + dtStoInfo.Rows[0]["saddress"].ToString() + "\",\"busHour\":\"" + dtStoInfo.Rows[0]["busHour"].ToString() + "\",\"ptype\":\"" + dtStoInfo.Rows[0]["ptype"].ToString() + "\",\"stocoordx\":\"" + dtStoInfo.Rows[0]["stocoordx"].ToString() + "\",\"stocoordy\":\"" + dtStoInfo.Rows[0]["stocoordy"].ToString() + "\"";
                    //门店基本信息
                    if (dtStoSetInfo != null && dtStoSetInfo.Rows.Count > 0)
                    {
                        isfest = dtStoSetInfo.Rows[0]["isfest"].ToString().TrimEnd(' ');
                        isweek = dtStoSetInfo.Rows[0]["isweek"].ToString().TrimEnd(' ');
                        jsonStr += ",\"isfest\":\"" + isfest + "\",\"isweek\":\"" + isweek + "\",\"isyd\":\"" + dtStoSetInfo.Rows[0]["isyd"].ToString() + "\",\"ispd\":\"" + dtStoSetInfo.Rows[0]["ispd"].ToString() + "\",\"isdc\":\"" + dtStoSetInfo.Rows[0]["isdc"].ToString() + "\"}";
                    }
                    else
                    {
                        jsonStr += ",\"isfest\":\"1\",\"isweek\":\"1\",\"isyd\":\"0\",\"ispd\":\"0\",\"isdc\":\"0\"}";
                    }
                    //门店服务信息
                    if (dtStoService != null && dtStoService.Rows.Count > 0)
                    {
                        jsonStr += ",\"stoserviceinfo\":[";
                        foreach (DataRow row in dtStoService.Rows)
                        {
                            jsonStr += "{\"sname\":\"" + row["sname"].ToString() + "\",\"spath\":\"" + imgurl + row["spath"].ToString() + "\"},";
                        }
                        jsonStr = jsonStr.TrimEnd(',');
                        jsonStr += "]";
                    }

                    //推荐菜
                    if (dtDisheInfo != null && dtDisheInfo.Rows.Count > 0)
                    {
                        jsonStr += ",\"disheinfo\":[";
                        foreach (DataRow row in dtDisheInfo.Rows)
                        {
                            jsonStr += "{\"discode\":\"" + row["discode"].ToString() + "\",\"disname\":\"" + row["disname"].ToString() + "\",\"price\":\"" + row["price"].ToString() + "\",\"iscombo\":\"" + row["iscombo"].ToString() + "\",\"ismethods\":\"" + row["isdismet"].ToString() + "\",\"dispicture\":\"" + imgurl + row["dispicture"].ToString() + "\",\"unit\":\"" + row["unit"] + "\"},";
                        }
                        jsonStr = jsonStr.TrimEnd(',');
                        jsonStr += "]";
                    }

                    //排队信息
                    if (dtPdSetInfo != null && dtPdSetInfo.Rows.Count > 0)
                    {
                        jsonStr += ",\"pdsetinfo\":[";
                        foreach (DataRow row in dtPdSetInfo.Rows)
                        {
                            jsonStr += "{\"lineid\":\"" + row["lineid"] + "\",\"minperson\":\"" + row["minperosn"].ToString() + "\",\"maxperson\":\"" + row["maxperson"].ToString() + "\",\"linecount\":\"" + row["linecount"].ToString() + "\"},";
                        }
                        jsonStr = jsonStr.TrimEnd(',');
                        jsonStr += "]";
                    }

                    //排队信息
                    if (dtPdInfo != null && dtPdInfo.Rows.Count > 0)
                    {
                        jsonStr += ",\"pdinfo\":[";
                        foreach (DataRow row in dtPdInfo.Rows)
                        {
                            jsonStr += "{\"bwid\":\"" + row["bwid"].ToString() + "\",\"minperson\":\"" + row["minperosn"].ToString() + "\",\"maxperson\":\"" + row["maxperson"].ToString() + "\",\"sortNum\":\"" + row["sortNum"].ToString() + "\",\"waitTime\":\"" + row["waitTime"].ToString() + "\",\"status\":\"" + row["status"].ToString() + "\",\"linecount\":\"" + row["linecount"].ToString() + "\"},";
                        }
                        jsonStr = jsonStr.TrimEnd(',');
                        jsonStr += "]";
                    }

                    //预定设置信息
                    if (dtYdSetInfo != null && dtYdSetInfo.Rows.Count > 0)
                    {
                        //今天 明天 后天 星期几 istq:提前预定不限 isday:提前预定天数
                        //bdate:开始日期 edate:结束日期
                        string day1 = dtYdSetInfo.Rows[0]["day1"].ToString();
                        string day2 = dtYdSetInfo.Rows[0]["day2"].ToString();
                        string day3 = dtYdSetInfo.Rows[0]["day3"].ToString();
                        string istq = dtYdSetInfo.Rows[0]["istq"].ToString().TrimEnd(' ');
                        string isday = dtYdSetInfo.Rows[0]["isday"].ToString().TrimEnd(' ');
                        string bdate = dtYdSetInfo.Rows[0]["bdate"].ToString();
                        string edate = dtYdSetInfo.Rows[0]["edate"].ToString();
                        string ftzq = dtYdSetInfo.Rows[0]["ftzq"].ToString();

                        //今天是否可以预定
                        jsonStr += ",\"ydinfo1\":[";

                        bool istrue = HaveDates(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"), bdate, edate, istq, isday);
                        if (istrue)
                        {
                            if (isweek == "1")
                            {
                                if (isfest == "1")
                                {
                                    if (istq == "1") //提前预定不限
                                    {
                                        jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                        jsonStr += ShowStrToday(dtYdTimeSetInfo, ftzq, "1");
                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                    }
                                    else //提前预定天数
                                    {
                                        //提前预定最早日期
                                        int isdayint = Helper.StringToInt(isday) * -1;
                                        DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                        if (tqDate <= DateTime.Now)
                                        {
                                            jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                            jsonStr += ShowStrToday(dtYdTimeSetInfo, ftzq, "1");
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else  //提前预定天数不符合条件
                                        {
                                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                            jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                    }
                                }
                                else
                                {   //假日不可预定
                                    bool isbool = HaveHoliday(DateTime.Now.ToString(), dtHolidayInfo);
                                    if (!isbool)
                                    {
                                        jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                        jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                    }
                                    else
                                    {
                                        if (istq == "1") //提前预定不限
                                        {
                                            jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                            jsonStr += ShowStrToday(dtYdTimeSetInfo, ftzq, "1");
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else //提前预定天数
                                        {
                                            //提前预定最早日期
                                            int isdayint = Helper.StringToInt(isday) * -1;
                                            DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                            if (tqDate <= DateTime.Now)
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStrToday(dtYdTimeSetInfo, ftzq, "1");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else  //提前预定天数不符合条件
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                        }
                                    }
                                }
                            }
                            else  //周末不可预定
                            {
                                if (day1 == "6" || day1 == "7")
                                {
                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                    jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                }
                                else
                                {
                                    if (isfest == "1") //假日可用
                                    {
                                        if (istq == "1") //提前预定不限
                                        {
                                            jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                            jsonStr += ShowStrToday(dtYdTimeSetInfo, ftzq, "1");
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else //提前预定天数
                                        {
                                            //提前预定最早日期
                                            int isdayint = Helper.StringToInt(isday) * -1;
                                            DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                            if (tqDate <= DateTime.Now)
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStrToday(dtYdTimeSetInfo, ftzq, "1");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else  //提前预定天数不符合条件
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                        }
                                    }
                                    else  //假日不可用
                                    {
                                        bool isbool = HaveHoliday(DateTime.Now.ToString(), dtHolidayInfo);
                                        if (!isbool)
                                        {
                                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                            jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else
                                        {
                                            if (istq == "1") //提前预定不限
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStrToday(dtYdTimeSetInfo, ftzq, "1");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else //提前预定天数
                                            {
                                                //提前预定最早日期
                                                int isdayint = Helper.StringToInt(isday) * -1;
                                                DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                                if (tqDate <= DateTime.Now)
                                                {
                                                    jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                    jsonStr += ShowStrToday(dtYdTimeSetInfo, ftzq, "1");
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                                else  //提前预定天数不符合条件
                                                {
                                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                    jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else //预定日期不可用
                        {
                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                            jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                        }

                        //明天是否可以预定
                        jsonStr += ",\"ydinfo2\":[";

                        istrue = HaveDates(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"), bdate, edate, istq, isday);
                        if (istrue)
                        {
                            if (isweek == "1")
                            {
                                if (isfest == "1")
                                {
                                    if (istq == "1") //提前预定不限
                                    {
                                        jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                        jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "2");
                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                    }
                                    else //提前预定天数
                                    {
                                        //提前预定最早日期
                                        int isdayint = Helper.StringToInt(isday) * -1;
                                        DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                        if (tqDate <= DateTime.Now.AddDays(1))
                                        {
                                            jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                            jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "2");
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else  //提前预定天数不符合条件
                                        {
                                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                            jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                    }
                                }
                                else
                                {   //假日不可预定
                                    bool isbool = HaveHoliday(DateTime.Now.AddDays(1).ToString(), dtHolidayInfo);
                                    if (!isbool)
                                    {
                                        jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                        jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                    }
                                    else
                                    {
                                        if (istq == "1") //提前预定不限
                                        {
                                            jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                            jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "2");
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else //提前预定天数
                                        {
                                            //提前预定最早日期
                                            int isdayint = Helper.StringToInt(isday) * -1;
                                            DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                            if (tqDate <= DateTime.Now.AddDays(1))
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "2");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else  //提前预定天数不符合条件
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                        }
                                    }
                                }
                            }
                            else  //周末不可预定
                            {
                                if (day2 == "6" || day2 == "7")
                                {
                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                    jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                }
                                else
                                {
                                    if (isfest == "1") //假日可用
                                    {
                                        if (istq == "1") //提前预定不限
                                        {
                                            jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                            jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "2");
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else //提前预定天数
                                        {
                                            //提前预定最早日期
                                            int isdayint = Helper.StringToInt(isday) * -1;
                                            DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                            if (tqDate <= DateTime.Now.AddDays(1))
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "2");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else  //提前预定天数不符合条件
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                        }
                                    }
                                    else  //假日不可用
                                    {
                                        bool isbool = HaveHoliday(DateTime.Now.AddDays(1).ToString(), dtHolidayInfo);
                                        if (!isbool)
                                        {
                                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                            jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else
                                        {
                                            if (istq == "1") //提前预定不限
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "2");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else //提前预定天数
                                            {
                                                //提前预定最早日期
                                                int isdayint = Helper.StringToInt(isday) * -1;
                                                DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                                if (tqDate <= DateTime.Now.AddDays(1))
                                                {
                                                    jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                    jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "2");
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                                else  //提前预定天数不符合条件
                                                {
                                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                    jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else //预定日期不可用
                        {
                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                            jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                        }

                        //后天是否可以预定
                        jsonStr += ",\"ydinfo3\":[";

                        istrue = HaveDates(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd 00:00:00"), bdate, edate, istq, isday);
                        if (istrue)
                        {
                            if (isweek == "1")
                            {
                                if (isfest == "1")
                                {
                                    if (istq == "1") //提前预定不限
                                    {
                                        jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                        jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "3");
                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                    }
                                    else //提前预定天数
                                    {
                                        //提前预定最早日期
                                        int isdayint = Helper.StringToInt(isday) * -1;
                                        DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                        if (tqDate <= DateTime.Now.AddDays(2))
                                        {
                                            jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                            jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "3");
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else  //提前预定天数不符合条件
                                        {
                                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                            jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                    }
                                }
                                else
                                {   //假日不可预定
                                    bool isbool = HaveHoliday(DateTime.Now.AddDays(2).ToString(), dtHolidayInfo);
                                    if (!isbool)
                                    {
                                        jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                        jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                    }
                                    else
                                    {
                                        if (istq == "1") //提前预定不限
                                        {
                                            jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                            jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "3");
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else //提前预定天数
                                        {
                                            //提前预定最早日期
                                            int isdayint = Helper.StringToInt(isday) * -1;
                                            DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                            if (tqDate <= DateTime.Now.AddDays(2))
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "3");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else  //提前预定天数不符合条件
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                        }
                                    }
                                }
                            }
                            else  //周末不可预定
                            {
                                if (day3 == "6" || day3 == "7")
                                {
                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                    jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                }
                                else
                                {
                                    if (isfest == "1") //假日可用
                                    {
                                        if (istq == "1") //提前预定不限
                                        {
                                            jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                            jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "3");
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else //提前预定天数
                                        {
                                            //提前预定最早日期
                                            int isdayint = Helper.StringToInt(isday) * -1;
                                            DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                            if (tqDate <= DateTime.Now.AddDays(2))
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "3");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else  //提前预定天数不符合条件
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                        }
                                    }
                                    else  //假日不可用
                                    {
                                        bool isbool = HaveHoliday(DateTime.Now.AddDays(2).ToString(), dtHolidayInfo);
                                        if (!isbool)
                                        {
                                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                            jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else
                                        {
                                            if (istq == "1") //提前预定不限
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "3");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else //提前预定天数
                                            {
                                                //提前预定最早日期
                                                int isdayint = Helper.StringToInt(isday) * -1;
                                                DateTime tqDate = DateTime.Now.AddDays(isdayint);

                                                if (tqDate <= DateTime.Now.AddDays(2))
                                                {
                                                    jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                    jsonStr += ShowStr(dtYdTimeSetInfo, ftzq, "3");
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                                else  //提前预定天数不符合条件
                                                {
                                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                    jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else //预定日期不可用
                        {
                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                            jsonStr += ShowStr1(dtYdTimeSetInfo, ftzq);
                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                        }
                    }

                    jsonStr += "}}";
                    ToJsonStr(jsonStr);
                }
                else
                {
                    ToCustomerJson("2", "网络繁忙，请稍后再试");
                }
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }
    }
}