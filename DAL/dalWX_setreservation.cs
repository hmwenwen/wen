using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 设置预定数据访问类
    /// </summary>
    public partial class dalWX_setreservation
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
		int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref WX_setreservationEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@setreservationid", Entity.setreservationid),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@ttcode", Entity.ttcode),
				new SqlParameter("@maxdeposit", Entity.maxdeposit),
				new SqlParameter("@methoddeposit", Entity.methoddeposit),
				new SqlParameter("@nolimitdate", Entity.nolimitdate),
				new SqlParameter("@daydeposit", Entity.daydeposit),
				new SqlParameter("@Duetype", Entity.Duetype),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
				new SqlParameter("@btime", Entity.btime),
				new SqlParameter("@etime", Entity.etime),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_setreservation_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.setreservationid = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(WX_setreservationEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@setreservationid", Entity.setreservationid),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@ttcode", Entity.ttcode),
				new SqlParameter("@maxdeposit", Entity.maxdeposit),
				new SqlParameter("@methoddeposit", Entity.methoddeposit),
				new SqlParameter("@nolimitdate", Entity.nolimitdate),
				new SqlParameter("@daydeposit", Entity.daydeposit),
				new SqlParameter("@Duetype", Entity.Duetype),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
				new SqlParameter("@btime", Entity.btime),
				new SqlParameter("@etime", Entity.etime),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_setreservation_Update", CommandType.StoredProcedure, sqlParameters); 
        }

		/// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="setreservationid">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateStatus(string ids, string Status)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ids", ids),
				new SqlParameter("@status", Status)
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_setreservation_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID，多个用,分隔</param>
        /// <returns>返回操作结果</returns>
        public int Delete(string setreservationid, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@setreservationid", setreservationid),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };
			sqlParameters[1].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_setreservation_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }
    }
}