using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using XJWZCatering.BLL;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
using XJWZCatering.WServices;
using XJWZCatering.SQL;
using XJWZCatering.LinkPubWx;
using System.Configuration;
using XJWZCatering.Tool;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using System.Text;
using Newtonsoft.Json;


namespace XJWZCatering.WServer.WxServer
{
    /// <summary>
    /// UserCenter 的摘要说明
    /// </summary>
    public class UserCenter : ServiceBase
    {
        DataTable dt = new DataTable();
        operatelogEntity logentity = new operatelogEntity();
        bllWX_members_wx wxuser = new bllWX_members_wx();
        public string sql = string.Empty;
        string imgurl = Helper.GetAppSettings("imgUrl");

        public override void ProcessRequest(HttpContext context)
        {
            if (CheckParameters(context))//检测是否合法
            {
                Dictionary<string, object> dicPar = GetParameters();
                if (dicPar != null)
                {
                    logentity.module = "用户信息";

                    switch (actionname.ToLower())
                    {
                        case "bindmobile": //绑定手机号
                            BindMoble(dicPar);
                            break;
                        case "xemm": //设置小额免密
                            Xemm(dicPar);
                            break;
                        case "setpwd": //设置密码
                            SetPwd(dicPar);
                            break;
                        case "getuserinfo": //获取用户信息
                            GetUserInfo(dicPar);
                            break;
                        case "modifyuserinfo":// 个人信息修改
                            ModifyUserInfo(dicPar);
                            break;
                        case "checkpwd":
                            ValidataPwd(dicPar);
                            break;
                        case "issubscribe":
                            isSubscribe(dicPar);
                            break;
                        case "modifymobile":
                            ModifyMobile(dicPar);
                            break;
                        case "getuserbyuid":
                            GetUserInfoByUid(dicPar);
                            break;
                        case "getuserheadimg":
                            GetUserHeadImg(dicPar);
                            break;
                        case "setpwdnew":
                            SetPwdNew(dicPar);
                            break;
                        case "changememcode":
                            ChangeMemCode(dicPar);
                            break;
                        case "issetpwd":
                            IsSetPwd(dicPar);
                            break;
                        case "getmpuser":
                            GetMPUser(dicPar);
                            break;
                        case "mpdecrypt":
                            MpDecrypt(dicPar);
                            break;
                        case "mpuserinfo":
                            MpUserInfo(dicPar);
                            break;
                    }
                }
                else
                {
                    context.Response.Write("{\"status\":\"2\",\"mes\":\"接口错误\"}");
                }
            }
        }

        /// <summary>
        /// 获取微信用户信息
        /// </summary>
        /// <param name="dicPar"></param>
        public void GetUserInfo(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();

            var sql = string.Format(@"declare @mobile varchar(12) set @mobile='';select @mobile=mobile from wx_members_wx where openid='{0}';select isnull(provinceid,'') as proname,isnull(cityid,'') as cityname,isnull(area,'') as areaname,nickname,sex,isnull(Convert(varchar(10),birthday,120),'') as birthday,isnull(addressdetails,'') as addressdetails,isnull(notpwd,'0') as notpwd,isnull(amount,0) as amount,mobile,headimgurl,memcode from WX_members_wx where openid='{0}';select COUNT(ID) from WX_busDestine where tel=@mobile and status in('0','4') union all select COUNT(bwid) from WX_busWait where tel=@mobile and status in('0') union all select COUNT(memid) from WX_orderdetails where openid='{0}' and payType in('0') and status in('0','1') union all select count(id) from wx_cardinfo where openid='{0}' and isecard='0' union all select count(id) from membercoupon where cardcode in (select cardcode from wx_cardinfo where openid='{0}' and status='1') and cid in (select c.cid from coupon c left join N_sumcoupon sc on c.sumcode=sc.sumcode where sc.ctype not in ('2') and c.status in('','0')) union all select COUNT(id) from WX_usermessage where openid='{0}' and status='0' and isdelete='0' union all select COUNT(actId) from WX_Complaints where openid='{0}' and len(opinion)>0 and isread='0' and status='1' and isdelete='0';select cardcode,erqimg from wx_cardinfo where openid='{0}' and isecard='1';", USER_ID);

            DataSet ds = SQLTool.ExecuteDataset(sql);
            if (ds.Tables.Count == 3)
            {
                string jsonStr = "{\"status\":\"0\",\"mes\":\"获取数据成功\",\"data\":[";
                dt = ds.Tables[0]; //用户基本信息
                DataTable dtCount = ds.Tables[1];
                DataTable dtECard = ds.Tables[2]; //电子卡信息
                if (dt != null && dt.Rows.Count > 0)
                {
                    jsonStr += "{\"proname\":\"" + dt.Rows[0]["proname"].ToString() + "\",\"cityname\":\"" + dt.Rows[0]["cityname"].ToString() + "\",\"areaname\":\"" + dt.Rows[0]["areaname"].ToString() + "\",\"nickname\":\"" + dt.Rows[0]["nickname"].ToString() + "\",\"sex\":\"" + dt.Rows[0]["sex"].ToString() + "\",\"birthday\":\"" + dt.Rows[0]["birthday"].ToString() + "\",\"addressdetails\":\"" + dt.Rows[0]["addressdetails"].ToString() + "\",\"notpwd\":\"" + dt.Rows[0]["notpwd"].ToString() + "\",\"amount\":\"" + dt.Rows[0]["amount"].ToString() + "\",\"mobile\":\"" + dt.Rows[0]["mobile"].ToString() + "\",\"headimgurl\":\"" + dt.Rows[0]["headimgurl"].ToString() + "\",\"memcode\":\"" + dt.Rows[0]["memcode"].ToString() + "\"";
                    if (dtCount != null && dtCount.Rows.Count > 0)
                    {
                        jsonStr += ",\"yycount\":\"" + dtCount.Rows[0][0].ToString() + "\",\"pdcount\":\"" + dtCount.Rows[1][0].ToString() + "\",\"dccount\":\"" + dtCount.Rows[2][0].ToString() + "\",\"cardcount\":\"" + dtCount.Rows[3][0].ToString() + "\",\"yhcount\":\"" + dtCount.Rows[4][0].ToString() + "\",\"messcount\":\"" + dtCount.Rows[5][0].ToString() + "\",\"tscount\":\"" + dtCount.Rows[6][0].ToString() + "\"";
                    }
                    else
                    {
                        //（预约记录，排队记录，点餐记录，会员卡数量，优惠券数量，消息数量，投诉建议数量）
                        jsonStr += ",\"yycount\":\"0\",\"pdcount\":\"0\",\"dccount\":\"0\",\"cardcount\":\"0\",\"yhcount\":\"0\",\"messcount\":\"0\",\"tscount\":\"0\"";
                    }

                    if (dtECard != null && dtECard.Rows.Count > 0)
                    {
                        string cardcode = dtECard.Rows[0]["cardcode"].ToString();
                        string erqimg = dtECard.Rows[0]["erqimg"].ToString();
                        //生成二维码保存
                        if (string.IsNullOrEmpty(erqimg))
                        {
                            erqimg = DoWaitProcess(cardcode);

                            new bllPaging().ExecuteNonQueryBySQL("update wx_cardinfo set erqimg='" + erqimg + "' where openid='" + USER_ID + "' and cardcode='" + cardcode + "' and isecard='1';");
                        }

                        jsonStr += ",\"ecardcode\":\"" + cardcode + "\",\"erqimg\":\"" + imgurl + erqimg + "\"";
                    }

                    jsonStr += "}]}";
                    ToJsonStr(jsonStr);
                }
                else
                {
                    ToCustomerJson("1", "未找到用户信息");
                }
            }
            else
            {
                ToCustomerJson("1", "未找到用户信息");
            }
        }

        /// <summary>
        /// 个人信息修改
        /// </summary>
        /// <param name="dicPar"></param>
        public void ModifyUserInfo(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "value", "type" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string value = dicPar["value"].ToString();
            string type = dicPar["type"].ToString();
            //type:1 昵称 2 性别 3 生日 4 常住地 5 详细地址
            if (type == "4")
            {
                string[] value_str = value.Split(',');
                if (value_str.Length == 3)
                {
                    string proname = value_str[0];
                    string cityname = value_str[1];
                    string areaname = value_str[2];

                    int count = wxuser.ModifyCityByOpenid(USER_ID, proname, cityname, areaname);
                    if (count >= 0)
                    {
                        ToCustomerJson("0", "操作成功");
                    }
                    else
                    {
                        ToCustomerJson("2", "修改失败，请稍后再试");
                    }
                }
                else if (value_str.Length == 2)
                {
                    string proname = value_str[0];
                    string cityname = value_str[1];
                    string areaname = string.Empty;

                    int count = wxuser.ModifyCityByOpenid(USER_ID, proname, cityname, areaname);
                    if (count >= 0)
                    {
                        ToCustomerJson("0", "操作成功");
                    }
                    else
                    {
                        ToCustomerJson("2", "修改失败，请稍后再试");
                    }
                }
                else if (value_str.Length == 1)
                {
                    string proname = value_str[0];
                    string cityname = string.Empty;
                    string areaname = string.Empty;

                    int count = wxuser.ModifyCityByOpenid(USER_ID, proname, cityname, areaname);
                    if (count >= 0)
                    {
                        ToCustomerJson("0", "操作成功");
                    }
                    else
                    {
                        ToCustomerJson("2", "修改失败，请稍后再试");
                    }
                }
                else
                {
                    ToCustomerJson("2", "修改失败，请稍后再试");
                }
            }
            else if (type == "1")
            {
                if (value.Length > 16)
                {
                    ToCustomerJson("1", "昵称输入过长，请不要超过16个字");
                }
                else
                {
                    int count = wxuser.ModifyInfoByOpenid(USER_ID, type, value);
                    if (count >= 0)
                    {
                        ToCustomerJson("0", "操作成功");
                    }
                    else
                    {
                        ToCustomerJson("2", "修改失败，请稍后再试");
                    }
                }
            }
            else
            {
                int count = wxuser.ModifyInfoByOpenid(USER_ID, type, value);
                if (count >= 0)
                {
                    ToCustomerJson("0", "操作成功");
                }
                else
                {
                    ToCustomerJson("2", "修改失败，请稍后再试");
                }
            }
        }

        /// <summary>
        /// 用户绑定手机号
        /// </summary>
        /// <param name="dicPar"></param>
        public void BindMoble(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "mobile" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string mobile = dicPar["mobile"].ToString();

            string mescode = String.Empty;
            int count = wxuser.SetPhone(USER_ID, mobile, ref mescode);

            if (count >= 0)
            {
                switch (mescode)
                {
                    case "0":
                        ToCustomerJson("0", "设置成功");
                        break;
                    case "1":
                        ToCustomerJson("1", "当前用户已绑定手机");
                        break;
                    case "2":
                        ToCustomerJson("1", "当前手机已绑定其他用户");
                        break;
                    case "3":
                        ToCustomerJson("1", "会员已被其他微信用户绑定，请联系会员中心");
                        break;
                }
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        //修改手机号
        public void ModifyMobile(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "newmobile" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string newmobile = dicPar["newmobile"].ToString();

            int count = new bllPaging().ExecuteNonQueryBySQL("update wx_members_wx set mobile='' where mobile='" + newmobile + "';update wx_members_wx set mobile='" + newmobile + "' where openid='" + USER_ID + "';declare @memcode varchar(32); select top 1 @memcode=memcode from members WHERE wxaccount='" + USER_ID + "'; exec dbo.p_members_changemobile_wx @memcode,'" + newmobile + "','','用户自己';--delete from wx_cardinfo where openid='" + USER_ID + "' and left(cardcode,1)<>'E';");

            if (count >= 0)
            {
                ToCustomerJson("0", "更改成功");
            }
            else
            {
                ToCustomerJson("1", "更改失败");
            }
        }

        /// <summary>
        /// 设置小额免密
        /// </summary>
        /// <param name="dicPar"></param>
        public void Xemm(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "amount", "status" }; //status 1:免密,0 需要密码
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string amount = dicPar["amount"].ToString();
            string status = dicPar["status"].ToString();

            string mescode = String.Empty;
            int count = wxuser.SetnotPwd(USER_ID, status, amount, ref mescode);

            if (count >= 0)
            {
                switch (mescode)
                {
                    case "0":
                        ToCustomerJson("0", "设置成功");
                        break;
                    case "1":
                        ToCustomerJson("1", "请先绑定手机号");
                        break;
                }
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        /// <summary>
        /// 设置支付密码
        /// </summary>
        /// <param name="dicPar"></param>
        public void SetPwd(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "pwd" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string pwd = dicPar["pwd"].ToString();

            string mescode = String.Empty;
            int count = wxuser.SetUPwd(USER_ID, Tools.DesEncrypt(pwd), ref mescode);

            if (count >= 0)
            {
                switch (mescode)
                {
                    case "0":
                        ToCustomerJson("0", "设置成功");
                        break;
                    case "1":
                        ToCustomerJson("1", "请先绑定手机号");
                        break;
                }
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        /// <summary>
        /// 设置支付密码(新 加入身份证、手机号验证）
        /// </summary>
        /// <param name="dicPar"></param>
        public void SetPwdNew(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "mobile", "idno", "pwd" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string GUID = dicPar["GUID"].ToString();
            string USER_ID = dicPar["USER_ID"].ToString();
            string mobile = dicPar["mobile"].ToString();
            string idno = dicPar["idno"].ToString();
            string pwd = dicPar["pwd"].ToString();

            string mescode = String.Empty;
            int count = wxuser.SetUPwdNew(USER_ID, mobile, idno, Tools.DesEncrypt(pwd), ref mescode);

            if (count >= 0)
            {
                switch (mescode)
                {
                    case "0":
                        ToCustomerJson("0", "设置成功");
                        break;
                    case "1":
                        ToCustomerJson("1", "请先绑定手机号");
                        break;
                    case "2":
                        ToCustomerJson("1", "输入手机号与绑定手机号不一致");
                        break;
                    case "3":
                        ToCustomerJson("1", "手机号与输入身份证号不匹配");
                        break;
                    case "4":
                        ToCustomerJson("1", "请补全身份证信息");
                        break;
                }
            }
            else
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        /// <summary>
        /// 密码验证
        /// </summary>
        /// <param name="dicPar"></param>
        public void ValidataPwd(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "pwd" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            sql = "select upwd from WX_members_wx where openid='" + dicPar["USER_ID"].ToString() + "'";
            var pwd = Convert.ToString(SQLTool.ExecuteScalar(sql));
            if (!string.IsNullOrEmpty(pwd))
            {
                if (pwd == Tools.DesEncrypt(Convert.ToString(dicPar["pwd"])))
                {
                    ToCustomerJson("0", "验证成功");
                }
                else
                {
                    ToCustomerJson("2", "密码错误");
                }
            }
            else
            {
                ToCustomerJson("1", "您尚未设置密码");
            }
        }

        /// <summary>
        /// 判断用户是否设置密码
        /// </summary>
        /// <param name="dicPar"></param>
        public void IsSetPwd(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "openid" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            try
            {
                string openid = dicPar["openid"].ToString();
                string sql = "SELECT TOP 1 ISNULL(upwd,'0') AS upwd  FROM dbo.WX_members_wx where openid='" + openid + "'";
                var result = SQL.SQLTool.ExecuteScalar(sql).ToString();
                if (string.IsNullOrEmpty(result) || result == "0")
                {
                    ToCustomerJson("0", "暂未设置密码");
                }
                else
                {
                    ToCustomerJson("1", "密码已设置");
                }
            }
            catch (Exception)
            {

                ToCustomerJson("-1", "暂未查找到该用户");
            }


        }

        /// <summary>
        /// 是否关注
        /// </summary>
        /// <param name="dicPar"></param>
        public void isSubscribe(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            var sql = "select subscribe from WX_members_wx where openid='" + Tools.SafeSql(Convert.ToString(dicPar["USER_ID"])) + "'";
            var isSubscribe = Convert.ToString(SQLTool.ExecuteScalar(sql));
            if (isSubscribe == "0")
            {
                ToCustomerJson("0", "未关注");
            }
            else
            {
                ToCustomerJson("1", "已关注");
            }
        }

        /// <summary>
        /// 微信小程序获取信息
        /// </summary>
        /// <param name="dicPar"></param>
        public void GetUserInfoByUid(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "openid", "unionid", "mobile" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            var uid = dicPar["unionid"].ToString();
            var openid = dicPar["openid"].ToString();
            var mobile = dicPar["mobile"].ToString();
            if (string.IsNullOrEmpty(mobile))
            {
                ToCustomerJson("2", "手机号为空，请在微信中绑定手机号");
                return;
            }

            if (uid == "undefined" || uid == "")
            {
                ToCustomerJson("2", "网络繁忙，请稍后再试");
                return;
            }
            var sql = "SELECT isnull(mobile,'')as mobile,isnull(memcode,'')as memcode FROM WX_members_wx where openid='" + uid + "'";
            var dt = SQL.XJWZSQLTool.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["memcode"].ToString() == "")
                {
                    sql = "declare @memcode varchar(32) set @memcode='';declare @count int set @count=0;declare @mobile varchar(12) set @mobile='" + mobile + "';update WX_members_wx set mobile='' where openid<>'" + uid + "' and mobile='" + mobile + "';update WX_members_wx set mobile='" + mobile + "' where openid='" + uid + "'; exec p_members_wx_Add_true 'weixin','88888888','WXXL','" + uid + "','0','','','','" + string.Empty + "','','','IDNO','',0,0,0,'','','','','','0',1,1,1,'',''; SELECT mobile,memcode FROM WX_members_wx where openid='" + uid + "'";

                    //sql = "declare @memcode varchar(32) set @memcode='';declare @count int set @count=0;declare @mobile varchar(12) set @mobile='" + mobile + "';update WX_members_wx set mobile='" + mobile + "' where openid='" + uid + "';select @count=count(*) from members where mobile=@mobile;if(@count>0)begin select @memcode=memcode from members where mobile=@mobile;update members set wxaccount='" + uid + "' where memcode=@memcode;update wx_members_wx set memcode=@memcode where openid='" + uid + "';exec p_MemCard_WXOpen_true '88888888',@memcode,@mobile,'" + uid + "'; end else begin exec p_members_wx_Add_true 'weixin','88888888','WXXL','" + uid + "','0','','','','" + string.Empty + "','','','IDNO','',0,0,0,'','','','','','0',1,1,1,'',''; end SELECT mobile,memcode FROM WX_members_wx where openid='" + uid + "'";
                    var dtInfo = SQL.XJWZSQLTool.ExecuteDataTable(sql);
                    if (dtInfo != null && dtInfo.Rows.Count > 0)
                    {
                        ReturnListJson(dtInfo);
                    }
                    else
                    {
                        ToCustomerJson("2", "网络繁忙，请稍后再试");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(uid))
                    {
                        //更新用户小程序信息
                        dt = SQL.XJWZSQLTool.ExecuteDataTable("update wx_members_wx set mobile='" + mobile + "',mpopenid='" + openid + "' where openid='" + uid + "';update wx_members_wx set mobile='' where openid<>'" + uid + "' and mobile='" + mobile + "';UPDATE dbo.WX_members_wx SET type=0 WHERE wxopenid IS NOT NULL AND mpopenid IS NOT NULL;SELECT '" + mobile + "' as mobile,'" + dt.Rows[0]["memcode"].ToString() + "'as memcode;");
                    }
                    ReturnListJson(dt);
                }
            }
            else
            {
                sql = "declare @memcode varchar(32) set @memcode='';declare @count int set @count=0;declare @mobile varchar(12) set @mobile='" + mobile + "';insert into WX_members_wx(openid,mobile,mpopenid,type) values ('" + uid + "','" + mobile + "','" + openid + "','2');update WX_members_wx set mobile='' where openid<>'" + uid + "' and mobile='" + mobile + "';exec p_members_wx_Add_true 'weixin','88888888','WXXL','" + uid + "','0','','','','" + string.Empty + "','','','IDNO','',0,0,0,'','','','','','0',1,1,1,'','';SELECT mobile,memcode FROM WX_members_wx where openid='" + uid + "'";

                //sql = "declare @memcode varchar(32) set @memcode='';declare @count int set @count=0;declare @mobile varchar(12) set @mobile='" + mobile + "';insert into WX_members_wx(openid,mobile,mpopenid,type) values ('" + uid + "','" + mobile + "','" + openid + "','2');select @count=count(*) from members where mobile=@mobile;if(@count>0)begin select @memcode=memcode from members where mobile=@mobile;update members set wxaccount='" + uid + "' where memcode=@memcode;update wx_members_wx set memcode=@memcode where openid='" + uid + "';exec p_MemCard_WXOpen_true '88888888',@memcode,@mobile,'" + uid + "'; end else begin exec p_members_wx_Add_true 'weixin','88888888','WXXL','" + uid + "','0','','','','" + string.Empty + "','','','IDNO','',0,0,0,'','','','','','0',1,1,1,'',''; end SELECT mobile,memcode FROM WX_members_wx where openid='" + uid + "'";
                var dtInfo = SQL.XJWZSQLTool.ExecuteDataTable(sql);
                if (dtInfo != null && dtInfo.Rows.Count > 0)
                {
                    ReturnListJson(dtInfo);
                }
                else
                {
                    ToCustomerJson("2", "网络繁忙，请稍后再试");
                }
            }
        }

        //获取用户头像
        public void GetUserHeadImg(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "memcode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            string memcode = dicPar["memcode"].ToString();
            dt = new bllPaging().GetDataTableInfoBySQL("select top 1 headimgurl from WX_members_wx where memcode='" + memcode + "';");
            if (dt != null && dt.Rows.Count > 0)
            {
                ReturnListJson(dt);
            }
            else
            {
                ToCustomerJson("1", "获取用户头像失败");
            }
        }

        /// <summary>
        /// 重新获取会员编号（memcode)
        /// </summary>
        public void ChangeMemCode(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "GUID", "USER_ID" };

            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            var unionid = dicPar["USER_ID"].ToString();

            DataTable dt = new bllPaging().GetDataTableInfoBySQL("select isnull(memcode,'') as memcode,isnull(mobile,'') as mobile from WX_members_wx where openid='" + unionid + "';");
            if (dt != null && dt.Rows.Count > 0)
            {
                string memcode = dt.Rows[0]["memcode"].ToString();
                string mobile = dt.Rows[0]["mobile"].ToString();
                if (string.IsNullOrEmpty(memcode) || string.IsNullOrEmpty(mobile))
                {
                    ToCustomerJson("1", "暂无数据");
                }
                else
                {
                    ReturnListJson(dt);
                }
            }
            else
            {
                ToCustomerJson("1", "暂无数据");
            }
        }

        /// <summary>
        /// 获取用户状态
        /// </summary>
        /// <param name="dicPar"></param>
        public void GetMPUser(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "code" };

            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            var code = dicPar["code"].ToString();
            WXHelper.WeChatAppDecrypt wxh = new WXHelper.WeChatAppDecrypt();
            var result = wxh.GetOpenIdAndSessionKeyString(code);
            ToJsonStr(result);
        }

        /// <summary>
        /// 微信加密字符串解密
        /// </summary>
        /// <param name="dicPar"></param>
        public void MpDecrypt(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "encryptedData", "iv", "sessionKey" };

            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            var encryptedData = dicPar["encryptedData"].ToString();
            var iv = dicPar["iv"].ToString();
            var sessionKey = dicPar["sessionKey"].ToString();
            WXHelper.WeChatAppDecrypt wxh = new WXHelper.WeChatAppDecrypt();
            var result = wxh.Decrypt(encryptedData.Replace(" ", "+"), iv, sessionKey);
            if (result != "fail")
            {
                ToJsonStr(result);
            }
            else
            {
                ToCustomerJson("-1", "网络错误,请稍后重试！");
            }

        }

        /// <summary>
        /// 获取用户信息（手机号、身份证号、证件类型）
        /// </summary>
        /// <param name="dicPar"></param>
        public void MpUserInfo(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "GUID", "USER_ID" };

            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            var unionid = dicPar["USER_ID"].ToString();
            var sql = "select top 1 mobile,IDNO,idtype from members where wxaccount='" + Tools.SafeSql(unionid) + "'";
            var dt = SQLTool.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                ToJsonStr(ToJson2(dt));
            }
            else
            {
                ToCustomerJson("-1", "暂无数据");
            }

        }

    }
}