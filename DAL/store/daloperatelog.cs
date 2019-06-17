using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 后台用户操作日志数据访问类
    /// </summary>
    public partial class daloperatelog
    {
        XJWZCatering.CommonBasic.MSSqlDataAccess DBHelper = new XJWZCatering.CommonBasic.MSSqlDataAccess();
        int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(operatelogEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@module", Entity.module),
				new SqlParameter("@pageurl", Entity.pageurl),
				new SqlParameter("@otype", Entity.otype),
				new SqlParameter("@logcontent", Entity.logcontent),
				new SqlParameter("@ip", Entity.ip),
				new SqlParameter("@cuser", Entity.cuser)
             };
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_operatelog_Add", CommandType.StoredProcedure, sqlParameters);
            return intReturn;
        }
    }
}