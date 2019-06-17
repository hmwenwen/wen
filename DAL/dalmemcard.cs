using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    public class dalmemcard
    {
        DataTable dt = new DataTable();
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        int intReturn;

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
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@openid",openid),
                new SqlParameter("@cardcode",cardcode),
                new SqlParameter("@mobile",mobile),
                new SqlParameter("@IDNO",idno),
                new SqlParameter("@CardPassWord",paypassword),
                new SqlParameter("@mes",SqlDbType.VarChar ,64,mescode)
            };
            sqlParameters[5].Direction = ParameterDirection.Output;

            DBHelper.ExecuteDataTable("p_checkbindmemcardtrue", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[5].Value.ToString();
        }

        //解绑会员卡
        public void CancelMemCard(string openid, string cardcode, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                new SqlParameter("@openid",openid),
                new SqlParameter("@cardcode",cardcode),
                new SqlParameter("@mescode",SqlDbType.VarChar ,64,mescode)
            };

            sqlParameters[2].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("p_CancelMemCard", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[2].Value.ToString();
        }

        //会员卡设为（取消）默认
        public int MemCardIsDef(string openid, string cardcode, string type)
        {
            SqlParameter[] sqlParameters = 
            {
                new SqlParameter("@openid",openid),
                new SqlParameter("@cardcode",cardcode),
                new SqlParameter("@type",type)
            };

            return DBHelper.ExecuteNonQuery("p_MemCardIsDef", CommandType.StoredProcedure, sqlParameters);
        }

        //交易记录
        public DataTable GetTradeList(string openid, string cardcode, string currentpage, string pagesize, ref string sumcount, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                new SqlParameter("@openid",openid),
                new SqlParameter("@cardcode",cardcode),
                new SqlParameter("@currentpage",currentpage),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@sumcount",SqlDbType.VarChar ,64,sumcount),
                new SqlParameter("@mescode",SqlDbType.VarChar ,64,mescode)
            };

            sqlParameters[4].Direction = ParameterDirection.Output;
            sqlParameters[5].Direction = ParameterDirection.Output;
            dt = DBHelper.ExecuteDataTable("p_gettradelist", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[4].Value.ToString();
            mescode = sqlParameters[5].Value.ToString();
            return dt;
        }

        //开卡
        public void OpenMemcard(string source, string buscode, string stocode, string wxaccount, string bigcustomer, string cname, string birthday, string sex, string mobile, string email, string tel, string idtype, string idno, string provinceid, string cityid, string areaid, string photo, string signature, string address, string hobby, string remark, string status, string orderno, string cuser, string uuser, string ousercode, string ousername)
        {
            SqlParameter[] sqlParameters = 
            {
                new SqlParameter("@source",source),
                new SqlParameter("@buscode",buscode),
                new SqlParameter("@strcode",stocode),
                new SqlParameter("@wxaccount",wxaccount),
                new SqlParameter("@bigcustomer",bigcustomer),
                new SqlParameter("@cname",cname),
                new SqlParameter("@birthday",birthday),
                new SqlParameter("@sex",sex),
                new SqlParameter("@mobile",mobile),
                new SqlParameter("@email",email),
                new SqlParameter("@tel",tel),
                new SqlParameter("@idtype",idtype),
                new SqlParameter("@IDNO",idno),
                new SqlParameter("@provinceid",provinceid),
                new SqlParameter("@cityid",cityid),
                new SqlParameter("@areaid",areaid),
                new SqlParameter("@photo",photo),
                new SqlParameter("@signature",signature),
                new SqlParameter("@address",address),
                new SqlParameter("@hobby",hobby),
                new SqlParameter("@remark",remark),
                new SqlParameter("@status",status),
                new SqlParameter("@orderno",orderno),
                new SqlParameter("@cuser",cuser),
                new SqlParameter("@uuser",uuser),
                new SqlParameter("@ousercode",ousercode),
                new SqlParameter("@ousername",ousername)
            };

            DBHelper.ExecuteNonQuery("p_members_wx_Add_true", CommandType.StoredProcedure, sqlParameters);
        }
    }
}
