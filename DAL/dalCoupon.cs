using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    public class dalCoupon
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();

        int intReturn;

        //获取用户优惠券列表
        public DataTable GetCouponList(string openid, string type, string currentpage, string pagesize, ref string sumcount)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@openid",openid),
                new SqlParameter("@type",type),
                new SqlParameter("@currentpage",currentpage),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@sumcount",SqlDbType.VarChar ,64,sumcount)
            };

            sqlParameters[4].Direction = ParameterDirection.Output;

            DataTable dt = DBHelper.ExecuteDataTable("p_getcouponlist", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[4].Value.ToString();
            return dt;
        }

        //根据优惠券号获取优惠券明细
        public DataTable GetCouponDetail(string couponcode)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@couponcode",couponcode)
            };

            return DBHelper.ExecuteDataTable("p_getcoupondetail", CommandType.StoredProcedure, sqlParameters);
        }

        //根据订单消费金额获取满足条件的优惠券信息
        public DataTable GetCouponListByMoney(string openid, string stocode, string money)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@openid",openid),
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@money",money)
            };

            return DBHelper.ExecuteDataTable("p_getcouponlistbymoney", CommandType.StoredProcedure, sqlParameters);
        }

        //获取用户会员卡折扣信息及优惠券信息
        public DataSet GetCardAndCouponList(string openid, string stocode, string money, string discodes)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@openid",openid),
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@money",money),
                new SqlParameter("@discodes",discodes)
            };

            return DBHelper.ExecuteDataSet("p_getcardandcouponlist", CommandType.StoredProcedure, sqlParameters);
        }

        //优惠券发放
        //优惠券活动code ,会员code,发放门店code
        public int ReceiveCoupon(string mccode, string memcode, string ffstocode, ref string mescode)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@mccode",mccode),
                new SqlParameter("@memcode",memcode),
                new SqlParameter("@ffstocode",ffstocode),
                new SqlParameter("@mescode",SqlDbType.VarChar ,64,mescode)
            };

            sqlParameters[3].Direction = ParameterDirection.Output;

            int count = DBHelper.ExecuteNonQuery("p_ReceiveCoupon", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[3].Value.ToString();
            return count;
        }

        //获取用户优惠券列表(影城）（小程序使用）
        public DataTable GetMVCouponList(string openid, string type, string currentpage, string pagesize, ref string sumcount)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@openid",openid),
                new SqlParameter("@type",type),
                new SqlParameter("@currentpage",currentpage),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@sumcount",SqlDbType.VarChar ,64,sumcount)
            };

            sqlParameters[4].Direction = ParameterDirection.Output;

            DataTable dt = DBHelper.ExecuteDataTable("p_getmvcouponlist", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[4].Value.ToString();
            return dt;
        }

        //根据消费的金额获取满足条件的用户优惠券信息（影城券）（小程序使用）
        public DataTable GetMovieMainCouponByMoney(string stocode, string memcode, string money, string discode, string discodes)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@memcode",memcode),
                new SqlParameter("@money", money),
                new SqlParameter("@discode",discode),
                new SqlParameter("@discodes",discodes)
            };

            return DBHelper.ExecuteDataTable("p_getfiltercoupon", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 分享优惠券
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="memcode"></param>
        /// <param name="checkcode"></param>
        /// <param name="mescode"></param>
        /// <returns></returns>
        public int ShareCoupon(string openid, string memcode, string checkcode, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                new SqlParameter("@memcode", memcode),
				new SqlParameter("@checkcode", checkcode),
				new SqlParameter("@mescode", SqlDbType.NVarChar ,64,mescode)
             };
            sqlParameters[3].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_MemberCardShareCoupon", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[3].Value.ToString();
            return intReturn;
        }

        /// <summary>
        /// 领取优惠券
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="memcode"></param>
        /// <param name="checkcode"></param>
        /// <param name="mescode"></param>
        /// <returns></returns>
        public int GetShareCoupon(string openid, string memcode, string checkcode, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                new SqlParameter("@openid", openid),
                new SqlParameter("@memcode", memcode),
				new SqlParameter("@checkcode", checkcode),
				new SqlParameter("@mescode", SqlDbType.NVarChar ,64,mescode)
             };
            sqlParameters[3].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_MemberCardGetShareCoupon", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[3].Value.ToString();
            return intReturn;
        }

        /// <summary>
        /// 获取需要推送消息的优惠券用户
        /// </summary>
        /// <param name="tipsCycle"></param>
        /// <returns></returns>
        public DataTable GetPushCoupon(string tipsCycle)
        {
            SqlParameter[] sqlParameters = 
            {
                new SqlParameter("@tipsCycle", tipsCycle),
             };
            //sqlParameters[0].Direction = ParameterDirection.Output;
            return DBHelper.ExecuteDataTable("dbo.p_GetPushCoupon", CommandType.StoredProcedure, sqlParameters);
        }
    }
}
