using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
using XJWZCatering.LinkPubWx;
using System.Configuration;
using XJWZCatering.Tool;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using XJWZCatering.WServer.WxServer;

namespace XJWZCatering.WServices
{
    public class ServiceBase : IHttpHandler
    {
        public HttpContext Pagcontext = null;
        public string actionname = string.Empty;
        public string usercode = string.Empty; //授权用户账号
        public operatelogEntity entity = null;
        System.Web.HttpServerUtility server = System.Web.HttpContext.Current.Server;

        public virtual void ProcessRequest(HttpContext context)
        {
        }

        public string GetPriKey(DataTable dt, string key)
        {
            string strReturn = string.Empty;
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strReturn += dt.Rows[i][key].ToString() + ",";
                }
            }

            return "'" + strReturn.TrimEnd(',').Replace(",", "','") + "'";
        }

        /// <summary>
        /// 检测接口必要参数
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected bool CheckParameters(HttpContext context)
        {
            Pagcontext = context;
            bool Flag = true;
            string mes = string.Empty;
            actionname = context.Request["actionname"];
            //if (CheckAuthorization() == true)
            //{
            if (actionname == null)
            {
                mes += "actionname,";
                Flag = false;
            }
            string parameters = context.Request["parameters"];
            if (parameters == null)
            {
                mes += "parameters,";
                Flag = false;
            }
            if (!Flag)
            {
                context.Response.Write("{\"status\":\"2\",\"mes\":\"缺少" + mes.TrimEnd(',') + "参数\"}");
            }
            //}
            //else
            //{
            //    context.Response.Write("{\"status\":\"2\",\"mes\":\"授权已过期或总控程序未启动\"}");
            //}
            return Flag;
        }

        /// <summary>
        /// 检测授权程序是否开启
        /// </summary>
        /// <returns></returns>
        private bool CheckAuthorization()
        {
            bool result = false;
            if (Process.GetProcessesByName("CateringServerForm").Length > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 检测接口必要参数
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected bool CheckCouponParameters(HttpContext context)
        {
            Pagcontext = context;
            bool Flag = true;
            string mes = string.Empty;
            actionname = context.Request["actionname"];
            if (actionname == null)
            {
                mes += "actionname,";
                Flag = false;
            }
            string parameters = context.Request["parameters"];
            if (parameters == null)
            {
                mes += "parameters,";
                Flag = false;
            }
            ////授权用户账号
            //usercode = context.Request["usercode"];
            //if (usercode == null)
            //{
            //    mes += "usercode,";
            //    Flag = false;
            //}

            if (!Flag)
            {
                context.Response.Write("{\"status\":\"2\",\"mes\":\"缺少" + mes.TrimEnd(',') + "参数\"}");
            }
            return Flag;
        }

        /// <summary>
        /// 获取json参数信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> dicPar = new Dictionary<string, object>();
            string parameters = Pagcontext.Request["parameters"];
            //Log.WriteLog("para", "ServiceBase", "传递过来的参数为:" + parameters);
            if (parameters.Length > 0)
            {
                try
                {
                    //string decparameters = OEncryp.Decrypt(parameters);
                    string decparameters = parameters;
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    object obj = jss.DeserializeObject(decparameters);
                    dicPar = (Dictionary<string, object>)obj;
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorMessage(ex);
                    Pagcontext.Response.Write("{\"status\":\"2\",\"mes\":\"参数解析错误\"}");
                    return null;
                }
            }
            return dicPar;
        }

        /// <summary>
        /// 检测调用参数是否合法
        /// </summary>
        /// <param name="dicPar"></param>
        /// <param name="liPra"></param>
        /// <returns></returns>
        protected bool CheckActionParameters(Dictionary<string, object> dicPar, List<string> liPra)
        {
            string mes = string.Empty;
            bool Flag = true;
            if (liPra == null)
            {
                Pagcontext.Response.Write("{\"status\":\"2\",\"mes\":\"List参数不合法\"}");
                return false;
            }
            if (dicPar == null)
            {
                Pagcontext.Response.Write("{\"status\":\"2\",\"mes\":\"parameters参数解析错误\"}");
                return false;
            }
            foreach (string str in liPra)
            {
                if (!dicPar.ContainsKey(str))
                {
                    mes += str + ",";
                    Flag = false;
                }
            }
            if (!Flag)
            {
                Pagcontext.Response.Write("{\"status\":\"2\",\"mes\":\"缺少" + mes.TrimEnd(',') + "参数\"}");
            }
            return Flag;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected void ToCustomerJson(string status, string mes)
        {
            Pagcontext.Response.Write(JsonHelper.ToJson(status, mes));
        }

        protected void ToErrorJson()
        {
            string mes = ErrMessage.GetMessageInfoByCode("Err_004").Body;
            Pagcontext.Response.Write(JsonHelper.ToErrorJson("1", mes));
        }

        protected void ToTipsJson()
        {
            Pagcontext.Response.Write("{\"status\":\"2\",\"mes\":\"审核成功\"}");
        }

        protected void ToSucessJson()
        {
            Pagcontext.Response.Write("{\"status\":\"0\",\"mes\":\"操作成功\"}");
        }

        protected void ToNullJson()
        {
            string mes = ErrMessage.GetMessageInfoByCode("Err_003").Body;
            Pagcontext.Response.Write(JsonHelper.ToErrorJson("1", mes));
        }

        protected void ToJsonStr(string jsonstr)
        {
            Pagcontext.Response.Write(jsonstr);
        }

        /// <summary>
        /// 返回执行json
        /// </summary>
        /// <param name="dt"></param>
        protected void ReturnJson(DataTable dt)
        {
            string type;
            string mes;
            Helper.GetDataTableToResult(dt, out type, out mes);
            Pagcontext.Response.Write(JsonHelper.ToJson(type, mes));
        }

        /// <summary>
        /// 返回单条json
        /// </summary>
        /// <param name="dt"></param>
        protected void ReturnListJson(DataTable dt)
        {
            ReturnListJson(dt, null, null, null, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="status"></param>
        /// <param name="mes"></param>
        protected void ReturnJsonByT<T>(T t)
        {
            Pagcontext.Response.Write(JsonHelper.ObjectToJson<T>(t));
        }

        /// <summary>
        /// 返回列表json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="currentPage"></param>
        /// <param name="totalPage"></param>
        protected void ReturnListJson(DataTable dt, int? pageSize, long? recordCount, int? currentPage, int? totalPage)
        {
            string type;
            string mes;
            Helper.GetDataTableToResult(dt, out type, out mes);
            if (type != "0")
            {
                Pagcontext.Response.Write(JsonHelper.ToJson(type, mes));
            }
            else
            {
                Pagcontext.Response.Write(JsonHelper.ToJson(type, mes, new ArrayList() { dt }, new string[] { "data" }, pageSize, recordCount, currentPage, totalPage));
            }
        }


        /// <summary>
        /// Json 返回多对象数据
        /// </summary>
        /// <param name="type">是否成功标识</param>
        /// <param name="mes">提示信息</param>
        /// <param name="list">多对象</param>
        /// <param name="Names">多对象返回名称</param>
        /// <param name="pageSize">页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="currentPage">当前页数</param>
        /// <param name="totalPage">总页数</param>
        protected void ReturnListJson(string type, string mes, ArrayList list, string[] Names, int? pageSize, long? recordCount, int? currentPage, int? totalPage)
        {
            if (type != "0")
            {
                Pagcontext.Response.Write(JsonHelper.ToJson(type, mes));
            }
            else
            {
                Pagcontext.Response.Write(JsonHelper.ToJson(type, mes, list, Names, pageSize, recordCount, currentPage, totalPage));
            }
        }

        #region  DataSet 转json 李超
        public void DataSetToJson(DataSet dataSet, string status, string mes)
        {
            string jsonString = "{\"status\":\"" + status + "\",\"mes\":\"" + mes + "\",\"data\":{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + ToJson2(table) + ",";
            }
            jsonString = jsonString.TrimEnd(',');
            Pagcontext.Response.Write(jsonString + "}}");
        }


        public string ToJson2(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                if (i != 0)
                {
                    jsonString.Append(",");
                }
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":\"");
                    strValue = string.Format(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + "\",");
                    }
                    else
                    {
                        jsonString.Append(strValue + "\"");
                    }
                }
                jsonString.Append("}");
            }
            jsonString.Append("]");
            return jsonString.ToString();
        }
        #endregion

        public void DataTableToJson(DataTable dt, string status, string mes)
        {
            string jsonString = "{\"status\":\"" + status + "\",\"mes\":\"" + mes + "\",\"data\":{";
            jsonString += "\"" + dt.TableName + "\":" + ToJson2(dt) + ",";
            jsonString = jsonString.TrimEnd(',');
            Pagcontext.Response.Write(jsonString + "}}");
        }

        //判断当前日期是否在指定日期区间内
        public bool HaveDates(string currdate, string startdate, string enddate, string istq, string isday)
        {
            bool istrue = false;
            DateTime currdatetime = Helper.StringToDateTime(currdate);
            DateTime startdatetime = Helper.StringToDateTime(startdate);
            DateTime enddatetime = Helper.StringToDateTime(enddate);

            if (istq == "1")
            {
                if (currdatetime <= enddatetime)
                {
                    istrue = true;
                }
                else
                {
                    istrue = false;
                }
            }
            else
            {
                int isdayint = Helper.StringToInt(isday) * -1;
                startdatetime = Helper.StringToDateTime(startdate).AddDays(isdayint);
                if (currdatetime >= startdatetime && currdatetime <= enddatetime)
                {
                    istrue = true;
                }
            }

            return istrue;
        }

        //判断当前日期是否在节假日设置日期里
        public bool HaveHoliday(string currentdate, DataTable dt)
        {
            bool istrue = true;
            DateTime currdatetime = Helper.StringToDateTime(currentdate);
            foreach (DataRow row in dt.Rows)
            {
                DateTime startdatetime = Helper.StringToDateTime(row["bdate"].ToString());
                DateTime enddatetime = Helper.StringToDateTime(row["edate"].ToString());
                if (currdatetime >= startdatetime && currdatetime <= enddatetime)
                {
                    istrue = false;
                    break;
                }
            }

            return istrue;
        }

        //今天预约 拼接字符串（与当前日期判断）
        public string ShowStrToday(DataTable dt, string ftzq, string type)
        {
            string jsonStr = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                string stime = row["btime"].ToString();
                string etime = row["etime"].ToString();
                string maxdep = row["maxdep"].ToString();
                //string count1 = string.Empty;
                //if (type == "1")
                //{
                //    count1 = row["count1"].ToString();
                //}
                //else if (type == "2")
                //{
                //    count1 = row["count2"].ToString();
                //}
                //else
                //{
                //    count1 = row["count3"].ToString();
                //}

                DateTime CurrSDate = Helper.StringToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + stime);
                DateTime CurrEDate = Helper.StringToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + etime);
                DateTime CurrDate = DateTime.Now;
                int count = 0;
                while (CurrSDate < CurrEDate)
                {
                    if (count == 0)
                    {
                        //if (Helper.StringToInt(count1) >= Helper.StringToInt(maxdep))
                        //{
                        //    jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"0\"},";
                        //}
                        //else
                        //{
                        if (CurrSDate <= CurrDate)
                        {
                            jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"0\"},";
                        }
                        else
                        {
                            jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"1\"},";
                        }
                        //}
                    }
                    else
                    {
                        CurrSDate = CurrSDate.AddMinutes(Helper.StringToInt(ftzq));
                        //if (Helper.StringToInt(count1) >= Helper.StringToInt(maxdep))
                        //{
                        //    jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"0\"},";
                        //}
                        //else
                        //{
                        if (CurrSDate <= CurrDate)
                        {
                            jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"0\"},";
                        }
                        else
                        {
                            jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"1\"},";
                        }
                        //}
                    }
                    count++;
                }
            }
            return jsonStr;
        }

        //拼接时间段字符串
        public string ShowStr(DataTable dt, string ftzq, string type)
        {
            string jsonStr = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                string stime = row["btime"].ToString();
                string etime = row["etime"].ToString();
                string maxdep = row["maxdep"].ToString();
                //string count1 = string.Empty;
                //if (type == "1")
                //{
                //    count1 = row["count1"].ToString();
                //}
                //else if (type == "2")
                //{
                //    count1 = row["count2"].ToString();
                //}
                //else
                //{
                //    count1 = row["count3"].ToString();
                //}

                DateTime CurrSDate = Helper.StringToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + stime);
                DateTime CurrEDate = Helper.StringToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + etime);
                int count = 0;
                while (CurrSDate < CurrEDate)
                {
                    if (count == 0)
                    {
                        //if (Helper.StringToInt(count1) >= Helper.StringToInt(maxdep))
                        //{
                        //    jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"0\"},";
                        //}
                        //else
                        //{
                        jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"1\"},";
                        //}
                    }
                    else
                    {
                        CurrSDate = CurrSDate.AddMinutes(Helper.StringToInt(ftzq));
                        //if (Helper.StringToInt(count1) >= Helper.StringToInt(maxdep))
                        //{
                        //    jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"0\"},";
                        //}
                        //else
                        //{
                        jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"1\"},";
                        //}
                    }
                    count++;
                }
            }
            return jsonStr;
        }

        //拼接时间段字符串（门店不可预定）
        public string ShowStr1(DataTable dt, string ftzq)
        {
            string jsonStr = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                string stime = row["btime"].ToString();
                string etime = row["etime"].ToString();
                string maxdep = row["maxdep"].ToString();
                //string count1 = row["count1"].ToString();

                DateTime CurrSDate = Helper.StringToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + stime);
                DateTime CurrEDate = Helper.StringToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + etime);
                int count = 0;
                while (CurrSDate < CurrEDate)
                {
                    if (count == 0)
                    {
                        jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"0\"},";
                    }
                    else
                    {
                        CurrSDate = CurrSDate.AddMinutes(Helper.StringToInt(ftzq));

                        jsonStr += "{\"time\":\"" + CurrSDate.ToString("HH:mm") + "\",\"isyd\":\"0\"},";
                    }
                    count++;
                }
            }
            return jsonStr;
        }

        //获取日期是星期几
        public int GetDayOfWeek(DateTime dtime)
        {
            int intStr = 0;
            string dStr = dtime.DayOfWeek.ToString();
            switch (dStr)
            {
                case "Monday":
                    intStr = 1;
                    break;
                case "Tuesday":
                    intStr = 2;
                    break;
                case "Wednesday":
                    intStr = 3;
                    break;
                case "Thursday":
                    intStr = 4;
                    break;
                case "Friday":
                    intStr = 5;
                    break;
                case "Saturday":
                    intStr = 6;
                    break;
                case "Sunday":
                    intStr = 7;
                    break;
            }

            return intStr;
        }

        /// <summary>
        /// 生成二维码并展示到页面上
        /// </summary>
        /// <param name="precreateResult">二维码串</param>
        public string DoWaitProcess(string ecard)
        {
            //打印出 preResponse.QrCode 对应的条码
            Bitmap bt;

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            qrCodeEncoder.QRCodeScale = 3;
            qrCodeEncoder.QRCodeVersion = 8;
            bt = qrCodeEncoder.Encode(ecard, Encoding.UTF8);
            string filename = ecard + ".jpg";
            string path = "/erqimg/" + filename;
            bt.Save(server.MapPath("~" + path));

            return path;
        }

        public string ToJsonString(string s)
        {
            return s.Replace("\"", "&quot;").Replace("\'", "&#39;").Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\r", "\\r");
            //return s.Replace(">", "&gt;").Replace("<", "&lt;").Replace(" ", "&nbsp;").Replace("\"", "&quot;").Replace("\'", "&#39;").Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\r", "\\r");
        }
    }
}