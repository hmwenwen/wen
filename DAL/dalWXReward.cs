using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    public class dalWXReward
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        DataTable dt = new DataTable();

        /// <summary>
        /// 打赏
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="empcode"></param>
        /// <param name="money"></param>
        /// <param name="point"></param>
        /// <param name="rcontent"></param>
        /// <param name="rid"></param>
        public void AddWXReward(string openid, string empcode, string money, string point, string rcontent, ref string rid, ref string orderno)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@openid", openid),
				new SqlParameter("@empcode", empcode),
                new SqlParameter("@money", money),
				new SqlParameter("@point", point),
                new SqlParameter("@rcontent", rcontent),
                new SqlParameter("@rid" ,SqlDbType.VarChar ,64,rid),
                new SqlParameter("@orderno",SqlDbType.VarChar,64,orderno)
             };

            sqlParameters[5].Direction = ParameterDirection.Output;
            sqlParameters[6].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("dbo.p_addwxreward", CommandType.StoredProcedure, sqlParameters);
            rid = sqlParameters[5].Value.ToString();
            orderno = sqlParameters[6].Value.ToString();
        }

        /// <summary>
        /// 更改打赏支付状态
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public int UpdateWXRewardStatus(string empcode, string rid)
        {
            SqlParameter[] sqlParameters = 
            {
                new SqlParameter("@empcode",empcode),
				new SqlParameter("@rid", rid)
             };

            return DBHelper.ExecuteNonQuery("p_UpdateWXRewardStatus", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 根据门店编号及员工编号获取打赏信息
        /// </summary>
        /// <param name="empcode"></param>
        /// <param name="stocode"></param>
        /// <returns></returns>
        public DataTable GetWXRewardInfo(string empcode, string stocode)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@empcode", empcode),
				new SqlParameter("@stocode", stocode)
             };

            return DBHelper.ExecuteDataTable("dbo.p_getwxrewardinfo", CommandType.StoredProcedure, sqlParameters);
        }

    }
}
