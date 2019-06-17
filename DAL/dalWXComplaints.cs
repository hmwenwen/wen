using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    public class dalWXComplaints
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        int intReturn;

        /// <summary>
        /// 添加投诉
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="stoname"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int AddComplaints(string openid, string stoname, string content)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@openid", openid),
				new SqlParameter("@stoname", stoname),
				new SqlParameter("@content", content)
             };

            return DBHelper.ExecuteNonQuery("dbo.p_AddComplaints", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 投诉建议列表
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="pagesize"></param>
        /// <param name="currentpage"></param>
        /// <param name="mescode"></param>
        /// <returns></returns>
        public DataTable GetComplaintsList(string openid, string pagesize, string currentpage, ref string sumcount)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@openid",openid),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@currentpage",currentpage),
                new SqlParameter("@sumcount",SqlDbType.VarChar ,64,sumcount)
            };

            sqlParameters[3].Direction = ParameterDirection.Output;

            DataTable dt = DBHelper.ExecuteDataTable("p_ComplaintsList", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[3].Value.ToString();
            return dt;
        }
    }
}
