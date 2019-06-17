using System;
using System.Collections;
using System.Data;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.BLL
{
    /// <summary>
    /// 单点登录操作
    /// </summary>
    public class LoginUniqueness : bllBase
    {
        /// <summary>
        /// 判断用户登录是否合法性
        /// </summary>
        /// <param name="GUID">唯一标识</param>
        /// <param name="UserID">用户ID</param>
        /// <returns>返回：""空字符合法，否则非法</returns>
        public static DataTable LoginedCheckFromPage(string GUID, string UserID)
        {
            DataTable dt = new DataTable("error");
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("mes", typeof(string));
            dt.Columns.Add("spanids", typeof(string));
            dt.AcceptChanges();
            if (UserID != "0")
            {
                //验证用户合法性
                if (!LoginedCheck(GUID, UserID))
                {
                    DataRow LoginVerify = dt.NewRow();
                    LoginVerify["type"] = "-1";
                    LoginVerify["mes"] = "用户已在其他位置登录，请重新登录！";
                    dt.Rows.Add(LoginVerify);
                }
            }
            return dt;
        }

        /// <summary>
        /// 登录身份验证
        /// </summary>
        /// <param name="GUID">登录GUID</param>
        /// <param name="UserID">用户ID</param>
        /// <returns>返回是否合法</returns>
        public static bool LoginedCheck(string GUID, string UserID)
        {
            bool Flag = true;
            Hashtable hOnline = MemCached.GetCache<Hashtable>("LoginOnline");
            if (hOnline != null)
            {
                object Val = hOnline[UserID];
                if (Val != null && Val.ToString() == GUID)
                {
                    Flag = true;
                }
            }
            return Flag;
        }

        /// <summary>
        /// 获取在线人数
        /// </summary>
        /// <returns></returns>
        public static int GetOnlinePerson()
        {
            int Flag = 0;
            Hashtable hOnline = MemCached.GetCache<Hashtable>("LoginOnline");
            if (hOnline != null)
            {
                Flag = hOnline.Count;
            }
            return Flag;
        }

        /// <summary>
        /// 登录系统
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns>返回：唯一GUID</returns>
        public static string LoginedSetKey(string UserID)
        {
            //以guid作为用户的唯一标识
            string guid = Guid.NewGuid().ToString();
            //
            Hashtable hOnline = MemCached.GetCache<Hashtable>("LoginOnline");
            //Hashtable hOnline = (Hashtable)WebCache.GetCache("LoginOnline");
            if (hOnline == null)
            {
                hOnline = new Hashtable();
            }

            hOnline[UserID] = guid;
            //WebCache.Insert("LoginOnline", hOnline,0);
            //MemCached.AddOrReplaceCache<Hashtable>("LoginOnline", hOnline, DateTime.Now.AddYears(1));
            return guid;
        }

        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="vercode">返回验证码</param>
        /// <returns></returns>
        public static bool MobileMesSendByTemp1(string mobile, string descr, ref string vercode)
        {
            bool Flag = false;
            string SendKey = "mob_" + mobile;
            Random rd = new Random();
            vercode = rd.Next(100001, 999999).ToString();
            Flag = NetEaseNoteInterface.SendTemplateByNetEase("temp1", new string[1] { mobile }, new string[2] { descr, vercode });
            if (Flag)
            {
                WebCache.Insert(SendKey, vercode, 5);
                //MemCached.AddOrReplaceCache<string>(SendKey, vercode, DateTime.Now.AddMinutes(5));
            }
            return Flag;
        }
        /// <summary>
        /// 存酒短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="descr"></param>
        /// <param name="vercode"></param>
        /// <returns></returns>
        public static bool MobileMesSendByTemp3(string mobile, string savetime, string stoname, string descr, string endtime, ref string vercode)
        {
            bool Flag = false;
            string SendKey = "mob_" + mobile;
            if (vercode.Length == 0)
            {
                Random rd = new Random();
                vercode = rd.Next(100001, 999999).ToString();
            }
            Flag = NetEaseNoteInterface.SendTemplateByNetEase("temp3", new string[1] { mobile }, new string[5] { savetime, stoname, descr, endtime, vercode });
            if (Flag)
            {
                WebCache.Insert(SendKey, vercode, 5);
            }
            return Flag;
        }

        /// <summary>
        /// 取酒短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="descr"></param>
        /// <param name="vercode"></param>
        /// <returns></returns>
        public static bool MobileMesSendByTemp4(string mobile, string nowdate, string stoname, string descr, string otelphone, string vercode)
        {
            bool Flag = false;
            string SendKey = "mob_" + mobile;
            Flag = NetEaseNoteInterface.SendTemplateByNetEase("temp4", new string[1] { mobile }, new string[5] { nowdate, stoname, descr, otelphone, vercode });
            if (Flag)
            {
                WebCache.Insert(SendKey, vercode, 5);
            }
            return Flag;
        }

        /// <summary>
        /// 校验验证码
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="vercode">验证码</param>
        /// <returns>返回 0.成功 1.过期 2.验证码错误</returns>
        public static int MobileMesCheck(string mobile, string vercode)
        {
            string SendKey = "mob_" + mobile;
            //string objMes = MemCached.GetCache<string>(SendKey);
            object objMes = WebCache.GetCache(SendKey);
            if (objMes == null)
            {
                return 1;//过期
            }
            if (objMes.ToString() != vercode)
            {
                return 2; //验证码错误
            }
            return 0;
        }


        /// <summary>
        /// 登出系统，清除系统缓存
        /// </summary>
        /// <param name="UserID">用户ID</param>
        public static bool LogoutSystem(string UserID)
        {
            Hashtable hOnline = MemCached.GetCache<Hashtable>("LoginOnline");
            if (hOnline != null)
            {
                hOnline.Remove(UserID);
                MemCached.AddOrReplaceCache<Hashtable>("LoginOnline", hOnline, DateTime.Now.AddYears(1));
                return true;
            }
            return false;
        }
    }
}