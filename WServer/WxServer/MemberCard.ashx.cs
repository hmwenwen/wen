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
    /// MemberCard 的摘要说明
    /// </summary>
    public class MemberCard : ServiceBase
    {
        DataTable dt = new DataTable();
        operatelogEntity logentity = new operatelogEntity();
        bllmemcard bll = new bllmemcard();
        string imgurl = Helper.GetAppSettings("imgUrl");

        public override void ProcessRequest(HttpContext context)
        {
            if (CheckParameters(context))//检测是否合法
            {
                Dictionary<string, object> dicPar = GetParameters();
                if (dicPar != null)
                {
                    logentity.module = "会员卡信息";
                    switch (actionname.ToLower())
                    {
                        case "bindmemcard":
                            BindMemCard(dicPar);
                            break;
                        case "getmemcardlist":
                            GetMemCardList(dicPar);
                            break;
                        case "cancelmemcard":
                            CancelMemCard(dicPar);
                            break;
                        case "memcardisdef":
                            MemCardIsDef(dicPar);
                            break;
                        case "gettradelist":
                            GetMemCardconsumption(dicPar);
                            break;
                        case "openmemcard":
                            OpenMemCard(dicPar);
                            break;
                        case "getmemcarddiscountlist":
                            GetMemCardDisCountList(dicPar);
                            break;
                        case "getmemcardbymp":
                            GetMemCardByMp(dicPar);
                            break;
                        case "getmemcardlevel":
                            GetMemCardLevel(dicPar);
                            break;
                        case "getmemcardbympstandard":
                            GetMemCardByMpStandard(dicPar);
                            break;
                        case "getmemcardlevelstr":
                            GetMemcardlevelStr(dicPar);
                            break;
                    }
                }
            }
        }

        //绑定会员卡
        private void BindMemCard(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "cardcode", "phone", "idno", "paypassword" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string cardcode = dicPar["cardcode"].ToString();  //卡号
            string phone = dicPar["phone"].ToString();        //手机号
            string idno = dicPar["idno"].ToString();          //手机号
            string paypassword = dicPar["paypassword"].ToString();   //支付密码

            string mescode = String.Empty;

            if (cardcode.Substring(0, 1).ToLower() == "e")
            {
                ToCustomerJson("1", "无法绑定电子卡");
            }
            else
            {
                if (!string.IsNullOrEmpty(paypassword))
                {
                    paypassword = OEncryp.Encrypt(paypassword);
                }
                bll.BindMemCard(USER_ID, cardcode, phone, idno, paypassword, ref mescode);

                switch (mescode)
                {
                    case "0":
                        string erqimg = DoWaitProcess(cardcode);
                        new bllPaging().ExecuteNonQueryBySQL("update wx_cardinfo set erqimg='" + erqimg + "' where openid='" + USER_ID + "' and cardcode='" + cardcode + "' and isecard='0';");

                        ToCustomerJson("0", "绑定成功");
                        break;
                    case "1":
                        ToCustomerJson("1", "卡号不存在");
                        break;
                    case "2":
                        ToCustomerJson("1", "卡状态异常");
                        break;
                    case "3":
                        ToCustomerJson("1", "卡资料手机号或证件号码不全");
                        break;
                    case "4":
                        ToCustomerJson("1", "手机号或证件号码与系统资料不匹配");
                        break;
                    case "5":
                        ToCustomerJson("1", "卡密码与系统密码不匹配");
                        break;
                    case "6":
                        ToCustomerJson("1", "卡已绑定其他账号");
                        break;
                    case "7":
                        ToCustomerJson("1", "卡已绑定");
                        break;
                    case "8":
                        ToCustomerJson("1", "卡类型不支持在线购票");
                        break;
                    case "9":
                        ToCustomerJson("1", "此会员已被其他微信用户绑定");
                        break;
                }
            }
        }

        //会员卡列表/详情
        private void GetMemCardList(Dictionary<string, object> dicPar)
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

            var sql = string.Format(@"select mc.cardCode,dbo.f_GetMemcardlevelBycardCode(mc.cardcode) as levelname,mct.cname as typename,dbo.fnGetmemcardconsumptionbalance(mc.cardcode) as balancetotal,wci.isdef,'" + imgurl + "'+wci.erqimg as erqimg,wmw.notpwd,amount,mct.ispay,mc.status from memcard mc left join wx_cardinfo wci on mc.cardcode=wci.cardcode left join wx_members_wx wmw on wci.openid=wmw.openid left join memcardtype mct on mc.ctype=mct.mctcode where mc.cardcode in (select cardcode from wx_cardinfo where openid='{0}' and wci.isecard='0') order by wci.isdef desc,dbo.f_GetMemcardlevelBycardCode(mc.cardcode);", USER_ID);

            dt = new bllPaging().GetDataTableInfoBySQL(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "没有数据");
            }
        }

        //获取已开卡信息及优惠方案信息 调用2.50 接口memberCard/WSMemCard.ashx getokmemcardinfo

        //充值 调用2.50 接口memberCard/WSMemCard.ashx  membercardrecharge

        //会员卡设为（取消）默认
        private void MemCardIsDef(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "cardcode", "type" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string cardcode = dicPar["cardcode"].ToString();
            string type = dicPar["type"].ToString();

            int count = bll.MemCardIsDef(USER_ID, cardcode, type);
            if (count >= 0)
            {
                ToCustomerJson("0", "设置成功");
            }
            else
            {
                ToCustomerJson("1", "设置失败，请稍后再试");
            }
        }

        //解绑会员卡
        private void CancelMemCard(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "cardcode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string cardcode = dicPar["cardcode"].ToString();
            string mescode = String.Empty;

            bll.CancelMemCard(USER_ID, cardcode, ref mescode);
            switch (mescode)
            {
                case "0":
                    ToCustomerJson("0", "解绑成功");
                    break;
                case "1":
                    ToCustomerJson("1", "解绑失败，请稍后再试");
                    break;
            }

        }

        //交易记录
        private void GetMemCardconsumption(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "cardcode", "currentpage", "pagesize" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string cardcode = dicPar["cardcode"].ToString();
            string currentpage = dicPar["currentpage"].ToString();
            string pagesize = dicPar["pagesize"].ToString();
            string mescode = string.Empty;
            string sumcount = string.Empty;

            dt = bll.GetTradeList(USER_ID, cardcode, currentpage, pagesize, ref sumcount, ref mescode);
            if (mescode == "0")
            {
                int scount = Helper.StringToInt(sumcount);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                    foreach (DataRow row in dt.Rows)
                    {
                        jsonStr += "{\"stocode\":\"" + row["stocode"] + "\",\"stoname\":\"" + row["stoname"] + "\",\"income\":\"" + row["income"] + "\",\"expend\":\"" + row["expend"] + "\",\"balancetotal\":\"" + row["balancetotal"] + "\",\"remark\":\"" + row["remark"] + "\",\"ctime\":\"" + row["ctime"] + "\",\"ctype\":\"" + row["ctype"] + "\"},";
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
            else
            {
                ToCustomerJson("2", "信息不匹配，请稍后再试");
            }
        }

        //开卡（电子卡）
        private void OpenMemCard(Dictionary<string, object> dicPar)
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

            string memid = string.Empty;
            string memcode = string.Empty;
            string mescode = string.Empty;

            string count = new bllPaging().ExecuteScalarBySQL("select count(*) from members where wxaccount='" + USER_ID + "';");
            if (Helper.StringToInt(count) > 0)
            {
                ToCustomerJson("1", "已开卡");
            }
            else
            {
                bll.OpenMemcard("weixin", "88888888", "WXXL", USER_ID, "0", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "0", "1", "1", "1", "", "");
                ToCustomerJson("0", "发卡成功");
            }
        }

        //获取用户会员卡折扣信息
        private void GetMemCardDisCountList(Dictionary<string, object> dicPar)
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

            string sql = @"select dbo.f_GetMemcardlevelBycardCode(mc.cardcode) as cardname,mc.cardCode,dp.usdiscountrate as privilegepre,wc.isecard,dp.dispcode,dp.dispname from wx_cardinfo wc left join memcard mc on wc.cardcode = mc.cardcode left join memcardlevel mcl on mc.cracode = mcl.levelcode left join discountpackage dp on CHARINDEX(','+mcl.levelcode+',',','+dp.cracodes+',')>0 where wc.openid='" + USER_ID + "' order by isecard desc,isdef desc;";

            dt = new bllPaging().GetDataTableInfoBySQL(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "没有可用的会员卡信息");
            }
        }

        /// <summary>
        /// 小程序接口（获取会员卡信息）
        /// </summary>
        /// <param name="dicPar"></param>
        private void GetMemCardByMp(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "memcode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            var memcode = dicPar["memcode"].ToString();
            if (!string.IsNullOrEmpty(memcode) && memcode != "undefined")
            {

                var sql = "select mc.cardcode,mc.ctype,dbo.f_GetMemcardtype(mc.ctype) as ctypename,mc.cracode as levelcode,dbo.f_GetMemcardlevel(mc.cracode) as levelname,dbo.fnGetmemcardconsumptionbalance(mc.cardcode) as balancetotal,isnull(mw.amount,0) as amount,(case when isnull(mw.upwd,'')='' then '0' else '1' end) as ispaypwd from memcard mc left join wx_members_wx mw on mc.memcode=mw.memcode left join wx_cardinfo wc on mc.cardcode=wc.cardcode where mc.status='1' and mc.memcode='" + memcode + "' and mc.cardcode in (select cardcode from wx_cardinfo where openid in (select openid from wx_members_wx where memcode='" + memcode + "') and left(cardcode,1)<>'E') order by wc.isdef desc;";
                var dt = SQL.XJWZSQLTool.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    ToJsonStr(ToJson2(dt));
                }
                else
                {
                    ToJsonStr("[{}]");
                }
            }
        }

        /// <summary>
        /// 获取会员卡信息（标准)（小程序用）
        /// </summary>
        /// <param name="dicPar"></param>
        private void GetMemCardByMpStandard(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "memcode", "unionid" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            var memcode = dicPar["memcode"].ToString();
            var unionid = dicPar["unionid"].ToString();

            var sql = string.Empty;
            if (string.IsNullOrEmpty(unionid))
            {
                sql = @" select mc.cardcode,mc.ctype,dbo.f_GetMemcardtype(mc.ctype) as ctypename,mc.cracode as levelcode,dbo.f_GetMemcardlevel(mc.cracode) as levelname,dbo.fnGetmemcardconsumptionbalance(mc.cardcode) as balancetotal,(select isnull(amount,0)  from wx_members_wx where openid in (select openid from wx_members_wx where memcode='" + memcode + @"')) as amount,(select (case when isnull(upwd,'')='' then '0' else '1' end) from wx_members_wx where openid in (select openid from wx_members_wx where memcode='" + memcode + @"')) as ispaypwd,(select levelcodes from mv_goodnbbylevel) as nolevelcodes 
  from memcard mc 
  left join wx_members_wx mw on mc.memcode=mw.memcode 
  left join wx_cardinfo wc on mc.cardcode=wc.cardcode 
  where mc.status='1' and mc.ctype<>'e'  and mc.cardcode in (select cardcode from wx_cardinfo where openid in (select openid from wx_members_wx where memcode='" + memcode + "'))  order by wc.isdef desc;";
            }
            else
            {
                sql = @" select mc.cardcode,mc.ctype,dbo.f_GetMemcardtype(mc.ctype) as ctypename,mc.cracode as levelcode,dbo.f_GetMemcardlevel(mc.cracode) as levelname,dbo.fnGetmemcardconsumptionbalance(mc.cardcode) as balancetotal,(select isnull(amount,0) from wx_members_wx where openid in ('" + unionid + @"')) as amount,(select (case when isnull(upwd,'')='' then '0' else '1' end) from wx_members_wx where openid in ('" + unionid + @"')) as ispaypwd,(select levelcodes from mv_goodnbbylevel) as nolevelcodes 
  from memcard mc 
  left join wx_members_wx mw on mc.memcode=mw.memcode 
  left join wx_cardinfo wc on mc.cardcode=wc.cardcode 
  where mc.status='1' and mc.ctype<>'e'  and mc.cardcode in (select cardcode from wx_cardinfo where openid in ('" + unionid + @"'))  order by wc.isdef desc;";
            }

            var dt = SQL.XJWZSQLTool.ExecuteDataTable(sql);
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
        /// 获取卡等级
        /// </summary>
        /// <param name="dicPar"></param>
        private void GetMemCardLevel(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "levelcode", "levelname", "mctcode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string levelcode = dicPar["levelcode"].ToString();
            string levelname = dicPar["levelname"].ToString();
            string mctcode = dicPar["mctcode"].ToString();

            var dt = SQL.XJWZSQLTool.ExecuteDataTable("select mt.mctcode,mt.cname,ml.levelcode,ml.levelname from memcardlevel ml left join memcardtype mt on ml.mctcode=mt.mctcode where ml.status='1' and (ml.levelcode='" + levelcode + "' or '0'='" + levelcode + "') and (ml.levelname like '%" + levelname + "%' or '0'='" + levelname + "') and (mt.mctcode='" + mctcode + "' or '0'='" + mctcode + "');");
            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }

        //获取卡等级集合（字符串 逗号分隔）
        public void GetMemcardlevelStr(Dictionary<string, object> dicPar)
        {
            //要检测的参数信息
            List<string> pra = new List<string>() { "openid" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string openid = dicPar["openid"].ToString();

            string sql = "declare @cardlevel varchar(max) set @cardlevel='';select @cardlevel=@cardlevel+cracode+',' from memcard where cardcode in(select cardcode from wx_cardinfo where openid='" + openid + "' and isecard<>'1') and status='1';select @cardlevel as cardelvels;";

            var dt = SQL.XJWZSQLTool.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }
    }
}