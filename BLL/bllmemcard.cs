using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XJWZCatering.BLL
{
    public class bllmemcard
    {
        DAL.dalmemcard dal = new DAL.dalmemcard();

        /// <summary>
        /// 绑定会员卡
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="cardcode">卡号</param>
        /// <param name="mobile">手机号</param>
        /// <param name="idno">身份证号</param>
        /// <param name="paypassword">支付密码</param>
        /// <param name="mes"></param>
        public void BindMemCard(string openid, string cardcode, string mobile, string idno, string paypassword, ref string mescode)
        {
            dal.BindMemCard(openid, cardcode, mobile, idno, paypassword, ref mescode);
        }

        //解绑会员卡
        public void CancelMemCard(string openid, string cardcode, ref string mescode)
        {
            dal.CancelMemCard(openid, cardcode, ref mescode);
        }

        //会员卡设为（取消）默认
        public int MemCardIsDef(string openid, string cardcode, string type)
        {
            return dal.MemCardIsDef(openid, cardcode, type);
        }

        //交易记录
        public DataTable GetTradeList(string openid, string cardcode, string currentpage, string pagesize, ref string sumcount, ref string mescode)
        {
            return dal.GetTradeList(openid, cardcode, currentpage, pagesize, ref sumcount, ref mescode);
        }

        //开卡
        public void OpenMemcard(string source, string buscode, string stocode, string wxaccount, string bigcustomer, string cname, string birthday, string sex, string mobile, string email, string tel, string idtype, string idno, string provinceid, string cityid, string areaid, string photo, string signature, string address, string hobby, string remark, string status, string orderno, string cuser, string uuser, string ousercode, string ousername)
        {
            dal.OpenMemcard(source, buscode, stocode, wxaccount, bigcustomer, cname, birthday, sex, mobile, email, tel, idtype, idno, provinceid, cityid, areaid, photo, signature, address, hobby, remark, status, orderno, cuser, uuser, ousercode, ousername);
        }
    }
}
