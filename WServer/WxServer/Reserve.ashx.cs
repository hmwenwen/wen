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
    /// Reserve 的摘要说明
    /// </summary>
    public class Reserve : ServiceBase
    {
        DataTable dt = new DataTable();
        operatelogEntity logentity = new operatelogEntity();
        bllWX_busDestine bll = new bllWX_busDestine();
        bllWX_stoset bllsto = new bllWX_stoset();
        bllWX_settime blltime = new bllWX_settime();
        string sql = string.Empty;

        public override void ProcessRequest(HttpContext context)
        {
            if (CheckParameters(context))//检测是否合法
            {
                Dictionary<string, object> dicPar = GetParameters();
                if (dicPar != null)
                {
                    logentity.module = "客户预定表";
                    switch (actionname.ToLower())
                    {
                        case "getreservetimes":
                            GetReserveTimes(dicPar);
                            break;
                        case "getreservetime":
                            GetReserveTime(dicPar);
                            break;
                        case "cancelreserve":
                            CancelReserve(dicPar);
                            break;
                        case "addreserve":
                            AddReserve(dicPar);
                            break;
                        case "reserverecord":
                            ReserveRecord(dicPar);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 获取指定日期预约时间及预定状态
        /// </summary>
        /// <param name="dicPar"></param>
        private void GetReserveTime(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "currentdate" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string currentdate = dicPar["currentdate"].ToString();

            DataSet ds = bll.GetReserveTime(stocode, currentdate);
            if (ds.Tables.Count == 5)
            {
                //指定日期星期几
                int weeks = GetDayOfWeek(Helper.StringToDateTime(currentdate));
                //isfest:节假日是否接受预定 isweek 周末是否接受预定 isyd:是否预定
                DataTable dt1 = ds.Tables[0];
                //istq:提前预定不限 isday:提前预定天数  bdate:开始日期 edate:结束日期
                DataTable dt2 = ds.Tables[1];
                //预约 code:预定编号 btime:开始时间 etime:结束时间 maxdep:预定上限 count1:指定日期已经预定数量
                DataTable dt3 = ds.Tables[2];
                //节假日设置信息
                DataTable dt4 = ds.Tables[3];
                //备注信息
                DataTable dt5 = ds.Tables[4];
                //是否节假日接受预定
                string isfest = "1";
                //是否周末接受预定
                string isweek = "1";
                String jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":{";
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    string isyd = dt1.Rows[0]["isyd"].ToString();
                    if (isyd == "1")
                    {
                        isfest = dt1.Rows[0]["isfest"].ToString().TrimEnd(' ');
                        isweek = dt1.Rows[0]["isweek"].ToString().TrimEnd(' ');
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            //istq:提前预定不限 isday:提前预定天数
                            //bdate:开始日期 edate:结束日期
                            string istq = dt2.Rows[0]["istq"].ToString().TrimEnd(' ');
                            string isday = dt2.Rows[0]["isday"].ToString().TrimEnd(' ');
                            string bdate = dt2.Rows[0]["bdate"].ToString();
                            string edate = dt2.Rows[0]["edate"].ToString();
                            string ftzq = dt2.Rows[0]["ftzq"].ToString();

                            //指定日期是否可以预定
                            jsonStr += "\"oydinfo\":[";

                            bool istrue = HaveDates(currentdate, bdate, edate, istq, isday);
                            if (istrue)
                            {
                                if (isweek == "1")
                                {
                                    if (isfest == "1")
                                    {
                                        if (istq == "1") //提前预定不限
                                        {
                                            jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                            if (Helper.StringToDateTime(currentdate).ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                                            {
                                                jsonStr += ShowStrToday(dt3, ftzq, "1");
                                            }
                                            else
                                            {
                                                jsonStr += ShowStr(dt3, ftzq, "1");
                                            }
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
                                                if (Helper.StringToDateTime(currentdate).ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                                                {
                                                    jsonStr += ShowStrToday(dt3, ftzq, "1");
                                                }
                                                else
                                                {
                                                    jsonStr += ShowStr(dt3, ftzq, "1");
                                                }
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else  //提前预定天数不符合条件
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dt3, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                        }
                                    }
                                    else
                                    {   //假日不可预定
                                        bool isbool = HaveHoliday(currentdate, dt4);
                                        if (!isbool)
                                        {
                                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                            jsonStr += ShowStr1(dt3, ftzq);
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else
                                        {
                                            if (istq == "1") //提前预定不限
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                if (Helper.StringToDateTime(currentdate).ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                                                {
                                                    jsonStr += ShowStrToday(dt3, ftzq, "1");
                                                }
                                                else
                                                {
                                                    jsonStr += ShowStr(dt3, ftzq, "1");
                                                }
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
                                                    if (Helper.StringToDateTime(currentdate).ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                                                    {
                                                        jsonStr += ShowStrToday(dt3, ftzq, "1");
                                                    }
                                                    else
                                                    {
                                                        jsonStr += ShowStr(dt3, ftzq, "1");
                                                    }
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                                else  //提前预定天数不符合条件
                                                {
                                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                    jsonStr += ShowStr1(dt3, ftzq);
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                            }
                                        }
                                    }
                                }
                                else  //周末不可预定
                                {
                                    if (weeks == 6 || weeks == 7)
                                    {
                                        jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                        jsonStr += ShowStr1(dt3, ftzq);
                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                    }
                                    else
                                    {
                                        if (isfest == "1") //假日可用
                                        {
                                            if (istq == "1") //提前预定不限
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                if (Helper.StringToDateTime(currentdate).ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                                                {
                                                    jsonStr += ShowStrToday(dt3, ftzq, "1");
                                                }
                                                else
                                                {
                                                    jsonStr += ShowStr(dt3, ftzq, "1");
                                                }
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
                                                    if (Helper.StringToDateTime(currentdate).ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                                                    {
                                                        jsonStr += ShowStrToday(dt3, ftzq, "1");
                                                    }
                                                    else
                                                    {
                                                        jsonStr += ShowStr(dt3, ftzq, "1");
                                                    }
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                                else  //提前预定天数不符合条件
                                                {
                                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                    jsonStr += ShowStr1(dt3, ftzq);
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                            }
                                        }
                                        else  //假日不可用
                                        {
                                            bool isbool = HaveHoliday(currentdate, dt4);
                                            if (!isbool)
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dt3, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else
                                            {
                                                if (istq == "1") //提前预定不限
                                                {
                                                    jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                    if (Helper.StringToDateTime(currentdate).ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                                                    {
                                                        jsonStr += ShowStrToday(dt3, ftzq, "1");
                                                    }
                                                    else
                                                    {
                                                        jsonStr += ShowStr(dt3, ftzq, "1");
                                                    }
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
                                                        if (Helper.StringToDateTime(currentdate).ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                                                        {
                                                            jsonStr += ShowStrToday(dt3, ftzq, "1");
                                                        }
                                                        else
                                                        {
                                                            jsonStr += ShowStr(dt3, ftzq, "1");
                                                        }
                                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                    }
                                                    else  //提前预定天数不符合条件
                                                    {
                                                        jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                        jsonStr += ShowStr1(dt3, ftzq);
                                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else  //预定日期不可用
                            {
                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                jsonStr += ShowStr1(dt3, ftzq);
                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                            }

                            if (dt5 != null && dt5.Rows.Count > 0)
                            {
                                jsonStr += ",\"remarkinfo\":[";
                                foreach (DataRow row in dt5.Rows)
                                {
                                    jsonStr += "{\"rcontent\":\"" + row["dicname"].ToString() + "\"},";
                                }
                                jsonStr = jsonStr.TrimEnd(',') + "]";
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
                        ToCustomerJson("1", "当前门店未开启预约");
                    }
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

        /// <summary>
        /// 获取预约今天 明天 后台预约详细信息及备注信息
        /// </summary>
        /// <param name="dicPar"></param>
        private void GetReserveTimes(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();

            DataSet ds = bll.GetReserveTimes(stocode);
            if (ds.Tables.Count == 5)
            {
                //isfest:节假日是否接受预定 isweek 周末是否接受预定 isyd:是否预定
                DataTable dt1 = ds.Tables[0];
                //今天 明天 后天 星期几 istq:提前预定不限 isday:提前预定天数  bdate:开始日期 edate:结束日期
                DataTable dt2 = ds.Tables[1];
                //预约 code:预定编号 btime:开始时间 etime:结束时间 maxdep:预定上限 count1:今天已经预定数量 count2:明天已经预定数量 count3:后天已经预定数量
                DataTable dt3 = ds.Tables[2];
                //节假日设置信息
                DataTable dt4 = ds.Tables[3];
                //备注信息
                DataTable dt5 = ds.Tables[4];
                //是否节假日接受预定
                string isfest = "1";
                //是否周末接受预定
                string isweek = "1";
                String jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":{";
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    string isyd = dt1.Rows[0]["isyd"].ToString();
                    if (isyd == "1")
                    {
                        isfest = dt1.Rows[0]["isfest"].ToString().TrimEnd(' ');
                        isweek = dt1.Rows[0]["isweek"].ToString().TrimEnd(' ');

                        if (dt2 != null && dt2.Rows.Count > 0)
                        {
                            //今天 明天 后天 星期几 istq:提前预定不限 isday:提前预定天数
                            //bdate:开始日期 edate:结束日期
                            string day1 = dt2.Rows[0]["day1"].ToString();
                            string day2 = dt2.Rows[0]["day2"].ToString();
                            string day3 = dt2.Rows[0]["day3"].ToString();
                            string istq = dt2.Rows[0]["istq"].ToString().TrimEnd(' ');
                            string isday = dt2.Rows[0]["isday"].ToString().TrimEnd(' ');
                            string bdate = dt2.Rows[0]["bdate"].ToString();
                            string edate = dt2.Rows[0]["edate"].ToString();
                            string ftzq = dt2.Rows[0]["ftzq"].ToString();

                            //今天是否可以预定
                            jsonStr += "\"ydinfo1\":[";

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
                                            jsonStr += ShowStrToday(dt3, ftzq, "1");
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
                                                jsonStr += ShowStrToday(dt3, ftzq, "1");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else  //提前预定天数不符合条件
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dt3, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                        }
                                    }
                                    else
                                    {   //假日不可预定
                                        bool isbool = HaveHoliday(DateTime.Now.ToString(), dt4);
                                        if (!isbool)
                                        {
                                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                            jsonStr += ShowStr1(dt3, ftzq);
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else
                                        {
                                            if (istq == "1") //提前预定不限
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStrToday(dt3, ftzq, "1");
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
                                                    jsonStr += ShowStrToday(dt3, ftzq, "1");
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                                else  //提前预定天数不符合条件
                                                {
                                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                    jsonStr += ShowStr1(dt3, ftzq);
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
                                        jsonStr += ShowStr1(dt3, ftzq);
                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                    }
                                    else
                                    {
                                        if (isfest == "1") //假日可用
                                        {
                                            if (istq == "1") //提前预定不限
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStrToday(dt3, ftzq, "1");
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
                                                    jsonStr += ShowStrToday(dt3, ftzq, "1");
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                                else  //提前预定天数不符合条件
                                                {
                                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                    jsonStr += ShowStr1(dt3, ftzq);
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                            }
                                        }
                                        else  //假日不可用
                                        {
                                            bool isbool = HaveHoliday(DateTime.Now.ToString(), dt4);
                                            if (!isbool)
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dt3, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else
                                            {
                                                if (istq == "1") //提前预定不限
                                                {
                                                    jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                    jsonStr += ShowStrToday(dt3, ftzq, "1");
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
                                                        jsonStr += ShowStrToday(dt3, ftzq, "1");
                                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                    }
                                                    else  //提前预定天数不符合条件
                                                    {
                                                        jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                        jsonStr += ShowStr1(dt3, ftzq);
                                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else  //预定日期不可用
                            {
                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                jsonStr += ShowStr1(dt3, ftzq);
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
                                            jsonStr += ShowStr(dt3, ftzq, "2");
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
                                                jsonStr += ShowStr(dt3, ftzq, "2");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else  //提前预定天数不符合条件
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dt3, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                        }
                                    }
                                    else
                                    {   //假日不可预定
                                        bool isbool = HaveHoliday(DateTime.Now.AddDays(1).ToString(), dt4);
                                        if (!isbool)
                                        {
                                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                            jsonStr += ShowStr1(dt3, ftzq);
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else
                                        {
                                            if (istq == "1") //提前预定不限
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStr(dt3, ftzq, "2");
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
                                                    jsonStr += ShowStr(dt3, ftzq, "2");
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                                else  //提前预定天数不符合条件
                                                {
                                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                    jsonStr += ShowStr1(dt3, ftzq);
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
                                        jsonStr += ShowStr1(dt3, ftzq);
                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                    }
                                    else
                                    {
                                        if (isfest == "1") //假日可用
                                        {
                                            if (istq == "1") //提前预定不限
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStr(dt3, ftzq, "2");
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
                                                    jsonStr += ShowStr(dt3, ftzq, "2");
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                                else  //提前预定天数不符合条件
                                                {
                                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                    jsonStr += ShowStr1(dt3, ftzq);
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                            }
                                        }
                                        else  //假日不可用
                                        {
                                            bool isbool = HaveHoliday(DateTime.Now.AddDays(1).ToString(), dt4);
                                            if (!isbool)
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dt3, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else
                                            {
                                                if (istq == "1") //提前预定不限
                                                {
                                                    jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                    jsonStr += ShowStr(dt3, ftzq, "2");
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
                                                        jsonStr += ShowStr(dt3, ftzq, "2");
                                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                    }
                                                    else  //提前预定天数不符合条件
                                                    {
                                                        jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                        jsonStr += ShowStr1(dt3, ftzq);
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
                                jsonStr += ShowStr1(dt3, ftzq);
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
                                            jsonStr += ShowStr(dt3, ftzq, "3");
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
                                                jsonStr += ShowStr(dt3, ftzq, "3");
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else  //提前预定天数不符合条件
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dt3, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                        }
                                    }
                                    else
                                    {   //假日不可预定
                                        bool isbool = HaveHoliday(DateTime.Now.AddDays(2).ToString(), dt4);
                                        if (!isbool)
                                        {
                                            jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                            jsonStr += ShowStr1(dt3, ftzq);
                                            jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                        }
                                        else
                                        {
                                            if (istq == "1") //提前预定不限
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStr(dt3, ftzq, "3");
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
                                                    jsonStr += ShowStr(dt3, ftzq, "3");
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                                else  //提前预定天数不符合条件
                                                {
                                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                    jsonStr += ShowStr1(dt3, ftzq);
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
                                        jsonStr += ShowStr1(dt3, ftzq);
                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                    }
                                    else
                                    {
                                        if (isfest == "1") //假日可用
                                        {
                                            if (istq == "1") //提前预定不限
                                            {
                                                jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                jsonStr += ShowStr(dt3, ftzq, "3");
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
                                                    jsonStr += ShowStr(dt3, ftzq, "3");
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                                else  //提前预定天数不符合条件
                                                {
                                                    jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                    jsonStr += ShowStr1(dt3, ftzq);
                                                    jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                }
                                            }
                                        }
                                        else  //假日不可用
                                        {
                                            bool isbool = HaveHoliday(DateTime.Now.AddDays(2).ToString(), dt4);
                                            if (!isbool)
                                            {
                                                jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                jsonStr += ShowStr1(dt3, ftzq);
                                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                            }
                                            else
                                            {
                                                if (istq == "1") //提前预定不限
                                                {
                                                    jsonStr += "{\"isyd\":\"1\",\"yddetail\":[";
                                                    jsonStr += ShowStr(dt3, ftzq, "3");
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
                                                        jsonStr += ShowStr(dt3, ftzq, "3");
                                                        jsonStr = jsonStr.TrimEnd(',') + "]}]";
                                                    }
                                                    else  //提前预定天数不符合条件
                                                    {
                                                        jsonStr += "{\"isyd\":\"0\",\"yddetail\":[";
                                                        jsonStr += ShowStr1(dt3, ftzq);
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
                                jsonStr += ShowStr1(dt3, ftzq);
                                jsonStr = jsonStr.TrimEnd(',') + "]}]";
                            }

                            if (dt5 != null && dt5.Rows.Count > 0)
                            {
                                jsonStr += ",\"remarkinfo\":[";
                                foreach (DataRow row in dt5.Rows)
                                {
                                    jsonStr += "{\"rcontent\":\"" + row["dicname"].ToString() + "\"},";
                                }
                                jsonStr = jsonStr.TrimEnd(',') + "]";
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
                        ToCustomerJson("1", "当前门店未开启预约");
                    }
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

        /// <summary>
        /// 添加用户预定信息
        /// </summary>
        /// <param name="dicPar"></param>
        private void AddReserve(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "rdate", "rtime", "usernum", "remark" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string rdate = dicPar["rdate"].ToString();
            string rtime = dicPar["rtime"].ToString();
            string usernum = dicPar["usernum"].ToString();
            string remark = dicPar["remark"].ToString();
            //手机号是否已绑定
            //是否已经预约过(选定日期有已预约未处理的数据)
            //当前预定时间类型预定数量是否已达上限
            string mescode = string.Empty;
            bll.CheckReserve(USER_ID, stocode, rdate, rtime, ref mescode);
            switch (mescode)
            {
                case "0":
                    dt = bll.AddReserve(USER_ID, stocode, rdate, rtime, Helper.StringToInt(usernum), ToJsonString(remark));
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        try
                        {
                            string stoname = dt.Rows[0]["stoname"].ToString();
                            var host = Helper.GetAppSettings("VipHostUrl");
                            var url = host + "/dist/index.html#/yyjl?GUID=1@USER_ID=" + USER_ID;
                            string desDate = dt.Rows[0]["desDate"].ToString();
                            string desTime = dt.Rows[0]["desTime"].ToString();
                            string address = dt.Rows[0]["address"].ToString();
                            string tel = dt.Rows[0]["tel"].ToString();
                            string desDateTime = Helper.StringToDateTime(desDate).ToString("yyyy-MM-dd") + " " + desTime;

                            //微信推送
                            string InterfaceUrl = host + "/WServer/WxServer/WxTemplate.ashx";
                            string DetailParameters = "actionname={0}&parameters={{\"USER_ID\":'{1}',\"url\":\"{2}\",\"stoname\":\"{3}\",\"time\":\"{4}\",\"location\":\"{5}\",\"address\":\"{6}\",\"tel\":\"{7}\",\"title\":\"{8}\"}}";
                            StringBuilder postStr = new StringBuilder();
                            string[] arrPar = new string[] { "sendmsg", USER_ID, url, stoname, desDateTime, "待分配", address, tel, "您好，您的预定正在处理中，请耐心等待" };
                            postStr.Append(string.Format(DetailParameters, arrPar));//键值对
                            string jsonStr = Helper.HttpWebRequestByURL(InterfaceUrl, postStr);
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
                        ToCustomerJson("2", "网络繁忙，请稍后再试");
                    }
                    break;
                case "1":
                    ToCustomerJson("3", "手机号未绑定");
                    break;
                case "2":
                    ToCustomerJson("1", "选定日期有已预约未处理的预约记录");
                    break;
                case "3":
                    ToCustomerJson("2", "网络繁忙，请稍后再试");
                    break;
                case "4":
                    ToCustomerJson("1", "当前预定时间段已达上限");
                    break;
            }
        }

        /// <summary>
        /// 取消预定
        /// </summary>
        /// <param name="dicPar"></param>
        private void CancelReserve(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "orderid" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string orderid = dicPar["orderid"].ToString();

            //是否有此预定，当前预定是否已处理
            string mescode = string.Empty;
            bll.CancelReserve(USER_ID, stocode, orderid, ref mescode);
            switch (mescode)
            {
                case "0":
                    //发微信推送
                    try
                    {
                        dt = new bllPaging().GetDataTableInfoBySQL("select wb.buscode,wb.stocode,dbo.fngetstorename(wb.stocode) as stoname,wb.userName,wb.tel,wb.desDate,wb.desTime,wb.metname,s.tel as stel,wb.openid from WX_busDestine wb left join store s on wb.stocode=s.stocode where wb.id=" + orderid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string buscode = dt.Rows[0]["buscode"].ToString();//商户编号
                            stocode = dt.Rows[0]["stocode"].ToString();//门店编号
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
                    break;
                case "1":
                    ToCustomerJson("1", "无此预定信息");
                    break;
                case "2":
                    ToCustomerJson("1", "此预定信息已处理");
                    break;
            }
        }

        /// <summary>
        /// 预约记录
        /// </summary>
        /// <param name="dicPar"></param>
        private void ReserveRecord(Dictionary<string, object> dicPar)
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
            dt = bll.GetReserveRecordlist(USER_ID, type, currentpage, pagesize, ref sumcount);
            int scount = Helper.StringToInt(sumcount);
            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    jsonStr += "{\"id\":\"" + row["ID"] + "\",\"stocode\":\"" + row["stocode"] + "\",\"stoname\":\"" + row["stoname"] + "\",\"desDate\":\"" + row["desDate"] + "\",\"desTime\":\"" + row["desTime"] + "\",\"personNum\":\"" + row["personNum"] + "\",\"status\":\"" + row["status"] + "\",\"ctime\":\"" + row["ctime"] + "\",\"remark\":\"" + row["remark"] + "\"},";
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
