using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using XJWZCatering.BLL;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
using XJWZCatering.WServices;

namespace XJWZCatering.WServer.WxServer
{
    /// <summary>
    /// Coupon 的摘要说明
    /// </summary>
    public class Coupon : ServiceBase
    {
        DataTable dt = new DataTable();
        operatelogEntity logentity = new operatelogEntity();
        bllCoupon bll = new bllCoupon();
        System.Web.HttpServerUtility server = System.Web.HttpContext.Current.Server;
        string imgUrl = Helper.GetAppSettings("imgUrl");

        public override void ProcessRequest(HttpContext context)
        {
            if (CheckParameters(context))//检测是否合法
            {
                Dictionary<string, object> dicPar = GetParameters();
                if (dicPar != null)
                {
                    logentity.module = "优惠券信息";
                    switch (actionname.ToLower())
                    {
                        case "getcouponlist":
                            GetCouponList(dicPar);
                            break;
                        case "getcouponlistbymoney":
                            GetCouponListByMoney(dicPar);
                            break;
                        case "getcoupondetail":
                            GetCouponDetail(dicPar);
                            break;
                        case "getmaincouponinfo":
                            GetMaincouponInfo(dicPar);
                            break;
                        case "receivecoupon":
                            ReceiveCoupon(dicPar);
                            break;
                        case "getmoviecouponlist":
                            GetMovieCouponList(dicPar);
                            break;
                        case "getmoviemaincouponlist":
                            GetMovieMainCouponList(dicPar);
                            break;
                        case "getmoviemaincouponbymoney":
                            GetMovieMainCouponByMoney(dicPar);
                            break;
                        case "getcouponnumber":
                            GetCouponNumber(dicPar);
                            break;
                        case "returnorusecoupon":
                            ReturnOrUseCoupon(dicPar);
                            break;
                        case "getpushcoupon":
                            GetPushCoupon(dicPar);
                            break;
                        case "sharecoupon"://发起分享优惠券
                            ShareCoupon(dicPar);
                            break;
                        case "getsharecoupon"://领取优惠券
                            GetshareCoupon(dicPar);
                            break;
                        case "getbirthsent"://生日赠送提醒
                            GetBirthsent(dicPar);
                            break;
                    }
                }
            }
        }

        //优惠券信息
        private void GetCouponList(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "currentpage", "pagesize", "type" };
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
            dt = bll.GetCouponList(USER_ID, type, currentpage, pagesize, ref sumcount);
            int scount = Helper.StringToInt(sumcount);

            if (dt != null && dt.Rows.Count > 0)
            {
                string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                foreach (DataRow row in dt.Rows)
                {
                    var imgpath = row["couponimg"].ToString().Length > 0 ? imgUrl + row["couponimg"] : string.Empty;
                    jsonStr += "{\"couname\":\"" + row["couname"] + "\",\"sdate\":\"" + row["sdate"] + "\",\"edate\":\"" + row["edate"] + "\",\"checkcode\":\"" + row["checkcode"] + "\",\"status\":\"" + row["status"] + "\",\"couponimg\":\"" + imgpath + "\",\"dicname\":\"" + row["dicname"] + "\",\"singlemoney\":\"" + row["singlemoney"] + "\"},";
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

        //优惠券明细
        private void GetCouponDetail(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "couponcode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string couponcode = dicPar["couponcode"].ToString();

            dt = bll.GetCouponDetail(couponcode);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("couponimg", typeof(string));
                string path = string.Empty;
                if (!File.Exists(server.MapPath("~/erqimg/" + couponcode + ".jpg")))
                {
                    path = DoWaitProcess(couponcode);
                }
                else
                {
                    path = "/erqimg/" + couponcode + ".jpg";
                }
                dt.Rows[0]["couponimg"] = Helper.GetAppSettings("imgUrl") + path;
                dt.AcceptChanges();
                ReturnListJson(dt);
                //string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";

                //jsonStr += "{\"sdate\":\"" + dt.Rows[0]["sdate"].ToString() + "\",\"edate\":\"" + dt.Rows[0]["edate"].ToString() + "\",\"checkcode\":\"" + dt.Rows[0]["checkcode"].ToString() + "\",\"couname\":\"" + dt.Rows[0]["couname"].ToString() + "\",\"storename\":\"" + dt.Rows[0]["storename"].ToString() + "\",\"singlemoney\":\"" + dt.Rows[0]["singlemoney"].ToString() + "\",\"maxmoney\":\"" + dt.Rows[0]["maxmoney"].ToString() + "\",\"uselimit\":\"" + dt.Rows[0]["uselimit"].ToString() + "\",\"istodayuse\":\"" + dt.Rows[0]["istodayuse"].ToString() + "\",\"goodsname\":\"" + dt.Rows[0]["goodsname"].ToString() + "\",\"couponimg\":\"" + Helper.GetAppSettings("imgUrl") + path + "\",\"mvtype\":\"" + dt.Rows[0]["mvtype"].ToString() + "\",\"descr\":\"" + dt.Rows[0]["descr"].ToString() + "\"}]}";

                //ToJsonStr(jsonStr);
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        //根据订单消费金额获取满足条件的优惠券信息
        private void GetCouponListByMoney(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "money" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string money = dicPar["money"].ToString();

            dt = bll.GetCouponListByMoney(USER_ID, stocode, money);

            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "无可用的优惠券");
            }
        }

        //获取有效的优惠券活动信息(影城券)（小程序使用）
        private void GetMaincouponInfo(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "mccode", "couname" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string mccode = dicPar["mccode"].ToString();
            string couname = dicPar["couname"].ToString();

            dt = new bllPaging().GetDataTableInfoBySQL("select mccode,couname,dbo.fnGetCouponStoNames(mccode) as stonames from N_maincoupon where getdate() between btime and etime and firtype='2' and status='1' and ('0'='" + mccode + "' or mccode='" + mccode + "') and ('0'='" + couname + "' or couname like '%" + couname + "%')");
            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "暂无优惠券活动信息");
            }
        }

        //发放优惠券（小程序使用）
        private void ReceiveCoupon(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "mccode", "memcode", "ffstocode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string mccode = dicPar["mccode"].ToString();
            string memcode = dicPar["memcode"].ToString();
            string ffstocode = dicPar["ffstocode"].ToString();

            string mescode = string.Empty;
            int count = bll.ReceiveCoupon(mccode, memcode, ffstocode, ref mescode);

            if (count >= 0)
            {
                switch (mescode)
                {
                    case "0":
                        ToCustomerJson("0", "发放成功");
                        break;
                    case "1":
                        ToCustomerJson("1", "发放失败，优惠券发放数量已超出限制");
                        break;
                }
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        //优惠券列表（可领取的优惠券）（影城券）（小程序使用）
        private void GetMovieMainCouponList(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "memcode", "stocode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string memcode = dicPar["memcode"].ToString();
            string stocode = dicPar["stocode"].ToString();

            if (memcode == "0")
            {
                dt = new bllPaging().GetDataTableInfoBySQL("select mc.mccode,dbo.fnGetCouponStoNames(mccode) as stonames,mc.couname,convert(varchar(10),mc.btime,102) as btime,convert(varchar(10),mc.etime,102) as etime,mc.singlemoney from N_maincoupon mc left join N_sumcoupon sm on mc.sumcode=sm.sumcode left join mv_couponset mcs on mc.mccode=mcs.couponcode where sm.ctype='2' and getdate() between mc.btime and mc.etime and dbo.fngetCouponCanStore(mccode,'" + stocode + "')='1' and sm.audstatus='1' and mc.number>dbo.fngetreceivecouponnum(mc.mccode) and mc.status='1' and mcs.limits<>'0';");
            }
            else
            {
                //函数 fngetisreceivecoupon 需优化 暂时屏蔽 lcl
                //dt = new bllPaging().GetDataTableInfoBySQL("select mc.mccode,dbo.fnGetStoNames(mc.stocode) as stonames,mc.couname,convert(varchar(10),mc.btime,102) as btime,convert(varchar(10),mc.etime,102) as etime,mc.singlemoney from N_maincoupon mc left join N_sumcoupon sm on mc.sumcode=sm.sumcode left join mv_couponset mcs on mc.mccode=mcs.couponcode where sm.ctype='2' and getdate() between mc.btime and mc.etime and dbo.fngetisreceivecoupon(mc.mccode,'" + memcode + "')='0' and (''=mc.stocode or '" + stocode + "' in (select col from dbo.fn_StringSplit(mc.stocode,','))) and sm.audstatus='1' and mc.number>dbo.fngetreceivecouponnum(mc.mccode) and mc.status='1' and mcs.limits<>'0';");
                dt = new bllPaging().GetDataTableInfoBySQL("select '' as mccode,'' as stonames,'' as couname,'' as btime,'' as etime,0 as singlemoney where 1=2;");
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "暂无符合条件的优惠券信息");
            }
        }

        //我的优惠券（已领取的）(已使用和未使用)（影城券）（小程序使用）
        private void GetMovieCouponList(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "memcode", "currentpage", "pagesize", "type" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string memcode = dicPar["memcode"].ToString();
            string currentpage = dicPar["currentpage"].ToString();
            string pagesize = dicPar["pagesize"].ToString();
            string type = dicPar["type"].ToString();
            if (string.IsNullOrEmpty(USER_ID) || USER_ID == "0")
            {
                ToCustomerJson("1", "获取微信信息失败，请重新登录");
            }
            else
            {
                string sumcount = string.Empty;
                dt = bll.GetMVCouponList(USER_ID, type, currentpage, pagesize, ref sumcount);
                int scount = Helper.StringToInt(sumcount);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                    foreach (DataRow row in dt.Rows)
                    {
                        var couponimg = row["couponimg"].ToString().Length > 0 ? imgUrl + row["couponimg"] : string.Empty;
                        jsonStr += "{\"couname\":\"" + row["couname"] + "\",\"sdate\":\"" + row["sdate"] + "\",\"edate\":\"" + row["edate"] + "\",\"checkcode\":\"" + row["checkcode"] + "\",\"status\":\"" + row["status"] + "\",\"couponimg\":\"" + couponimg + "\",\"dicname\":\"" + row["dicname"] + "\",\"singlemoney\":\"" + row["singlemoney"] + "\"},";
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

        //根据消费的金额获取满足条件的用户优惠券信息（影城券）（小程序使用）
        private void GetMovieMainCouponByMoney(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "memcode", "money", "discodes", "discode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string stocode = dicPar["stocode"].ToString();
            string memcode = dicPar["memcode"].ToString();
            string money = dicPar["money"].ToString();
            string discodes = dicPar["discodes"].ToString();
            string discode = dicPar["discode"].ToString();
            dt = bll.GetMovieMainCouponByMoney(stocode, memcode, money, discode, discodes);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dicPar.ContainsKey("filmcode") && dicPar["filmcode"] != null && !string.IsNullOrWhiteSpace(dicPar["filmcode"].ToString()))
                {
                    List<DataRow> drs = new List<DataRow>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        string filmcodes = dr["filmcodes"].ToString();
                        if (!string.IsNullOrWhiteSpace(filmcodes) && !filmcodes.Contains(dicPar["filmcode"].ToString()))
                        {
                            drs.Add(dr);
                        }
                    }
                    foreach (DataRow dr in drs)
                    {
                        dt.Rows.Remove(dr);
                    }
                    if (dt.Rows.Count <= 0)
                    {
                        ToCustomerJson("1", "暂无数据");
                    }
                }
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }

        //获取用户优惠券数量
        private void GetCouponNumber(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "memcode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            //string memcode = dicPar["memcode"].ToString();

            string count = new bllPaging().ExecuteScalarBySQL("select count(membercoupon.cid) from membercoupon left join dbo.coupon on membercoupon.cid=coupon.cid left join N_maincoupon mc on coupon.mccode = mc.mccode left join N_sumcoupon sc on mc.sumcode=sc.sumcode where sc.ctype='0' and cardcode in(select cardcode from wx_cardinfo where openid='" + USER_ID + "' and status='1') and  dbo.coupon.status ='0';");
            ToCustomerJson(count, "获取数据成功");
        }

        //优惠券退回(使用)(0:退回 1：使用)
        private void ReturnOrUseCoupon(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "couponcodes", "type" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string couponcodes = dicPar["couponcodes"].ToString();
            string type = dicPar["type"].ToString();

            int count = 0;
            try
            {
                string strSql = "update coupon set status='" + type + "',checkstocode='WXXL',checkucode='线上用户',checkperson='线上用户',checktime=getdate() where checkcode in (select col from dbo.fn_StringSplit('" + couponcodes.Trim(',') + "',','));if ('" + type + "'='1') begin declare @id varchar(max);set @id=''; select @id=@id+convert(varchar(20),cid)+',' from coupon where checkcode in (select col from dbo.fn_StringSplit('" + couponcodes.Trim(',') + "',',')); exec [dbo].[p_coupon_transDrawCoupon] @id;   end";
                //ErrorLog.WriteLogMessage("1111", strSql);
                count = new bllPaging().ExecuteNonQueryBySQL(strSql);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
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
        /// 获取需要推送消息的优惠券用户
        /// </summary>
        /// <param name="dicPar"></param>
        private void GetPushCoupon(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "TipsCycle" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string tipsCycle = dicPar["TipsCycle"].ToString();

            var dt = bll.GetPushCoupon(tipsCycle);
            if (dt != null && dt.Rows.Count > 0)
            {
                ToJsonStr(Helper.DataTable2Json(dt));
            }
            else
            {
                ToCustomerJson("-1", "暂无数据");
            }
        }

        /// <summary>
        /// 获取需要提醒的生日赠送信息
        /// </summary>
        /// <param name="dicPar"></param>
        private void GetBirthsent(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();

            dt = new bllPaging().GetDataTableInfoBySQL("select distinct wmw.mpopenid,[dbo].[f_GetBirthsentRecord](mcc.cardcode,mcc.memcode,mcc.pcode) as [sid] from coupon c left join membercoupon mcc on c.cid=mcc.cid left join members me on me.memcode=mcc.memcode LEFT JOIN dbo.WX_members_wx wmw ON  wmw.openid=me.wxaccount where c.puser='生日赠送' and isnull(mcc.memcode,'')<>'' and [dbo].[f_GetBirthsentIsTrip](mcc.cardcode,mcc.memcode,mcc.pcode)=0");
            if (dt != null && dt.Rows.Count > 0)
            {
                ToJsonStr(Helper.DataTable2Json(dt));
            }
            else
            {
                ToCustomerJson("-1", "暂无数据");
            }
        }

        /// <summary>
        /// 发起分享优惠券
        /// </summary>
        /// <param name="dicPar"></param>
        public void ShareCoupon(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息string , string ,string 
            List<string> pra = new List<string>() { "GUID", "USER_ID", "couponcode", "memcode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            //获取参数信息
            string GUID = dicPar["GUID"].ToString();
            string openid = dicPar["USER_ID"].ToString();
            string checkcode = dicPar["couponcode"].ToString();
            string memcode = dicPar["memcode"].ToString();
            if (!checkcode.Contains("-") && checkcode.Length == 16)
            {
                string newCode = string.Format("{0}-{1}-{2}-{3}", checkcode.Substring(0, 4), checkcode.Substring(4, 4), checkcode.Substring(8, 4), checkcode.Substring(12, 4));
                checkcode = newCode;
            }
            string mescode = "";
            if (bll.ShareCoupon(GUID, openid, memcode, checkcode, ref mescode) == 0)
            {
                ToCustomerJson("0", "分享成功");
            }
            else
            {
                if (mescode == "Error1")
                {
                    mescode = "参数错误";
                }
                else if (mescode == "Error2")
                {
                    mescode = "未绑定会员";
                }
                else if (mescode == "Error3")
                {
                    mescode = "优惠券码无效";
                }
                ToCustomerJson("1", mescode);
            }
        }

        /// <summary>
        /// 领取优惠券
        /// </summary>
        /// <param name="dicPar"></param>
        public void GetshareCoupon(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息string , string ,string 
            List<string> pra = new List<string>() { "GUID", "USER_ID", "couponcode", "memcode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            //获取参数信息
            string GUID = dicPar["GUID"].ToString();
            string openid = dicPar["USER_ID"].ToString();
            string checkcode = dicPar["couponcode"].ToString();
            string memcode = dicPar["memcode"].ToString();
            if (!checkcode.Contains("-") && checkcode.Length == 16)
            {
                string newCode = string.Format("{0}-{1}-{2}-{3}", checkcode.Substring(0, 4), checkcode.Substring(4, 4), checkcode.Substring(8, 4), checkcode.Substring(12, 4));
                checkcode = newCode; 
            }
            string mescode = "";
            if (bll.GetShareCoupon(GUID, openid, memcode, checkcode, ref mescode) == 0)
            {
                ToCustomerJson("0", "领取成功");
            }
            else
            {
                if (mescode == "Error1")
                {
                    mescode = "参数错误";
                }
                else if (mescode == "Error2")
                {
                    mescode = "未绑定会员";
                }
                else if (mescode == "Error3")
                {
                    mescode = "优惠券已使用";
                }
                else if (mescode == "Error4")
                {
                    mescode = "优惠券已被领取";
                }
                else if (mescode == "Error5")
                {
                    mescode = "领取失败";
                }
                ToCustomerJson("1", mescode);
            }
        }
    }
}