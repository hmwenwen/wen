using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    public class dalHelp
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();

        /// <summary>
        /// 帮助中心
        /// </summary>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="keywords"></param>
        /// <param name="sumcount"></param>
        /// <returns></returns>
        public DataTable GetHelpList(string currentpage, string pagesize, string keywords, ref string sumcount)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@currentpage",currentpage),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@keywords",keywords),
                new SqlParameter("@sumcount",SqlDbType.VarChar ,64,sumcount)
            };

            sqlParameters[3].Direction = ParameterDirection.Output;

            DataTable dt = DBHelper.ExecuteDataTable("p_wxhelplist", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[3].Value.ToString();
            return dt;
        }
    }
}
