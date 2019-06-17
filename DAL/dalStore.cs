using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    public class dalStore
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        int intReturn;
        /// <summary>
        /// 找店
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="currentpage"></param>
        /// <param name="citycode"></param>
        /// <param name="shopcircle"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public DataTable FindStore(string pagesize, string currentpage, string citycode, string shopcircle, string keywords, ref string sumcount)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@citycode",citycode),
                new SqlParameter("@shopcircle",shopcircle),
                new SqlParameter("@keywords",keywords),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@currentpage",currentpage),
                new SqlParameter("@sumcount",SqlDbType.VarChar ,64,sumcount)
            };
            sqlParameters[5].Direction = ParameterDirection.Output;

            DataTable dt = DBHelper.ExecuteDataTable("p_FindStore", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[5].Value.ToString();
            return dt;
        }

        /// <summary>
        /// 门店详情
        /// </summary>
        /// <param name="stocode"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public DataSet GetStoDetail(string stocode, string openid)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@openid",openid)
            };

            return DBHelper.ExecuteDataSet("p_GetStoreDetail", CommandType.StoredProcedure, sqlParameters);
        }
    }
}
