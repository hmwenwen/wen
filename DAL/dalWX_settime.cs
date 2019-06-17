using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 设置时间数据访问类
    /// </summary>
    public partial class dalWX_settime
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
		int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref WX_settimeEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@timeid", Entity.timeid),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@timename", Entity.timename),
				new SqlParameter("@timevalue", Entity.timevalue),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
				new SqlParameter("@btime", Entity.btime),
				new SqlParameter("@etime", Entity.etime),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_settime_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.timeid = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(WX_settimeEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@timeid", Entity.timeid),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@timename", Entity.timename),
				new SqlParameter("@timevalue", Entity.timevalue),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
				new SqlParameter("@btime", Entity.btime),
				new SqlParameter("@etime", Entity.etime),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_settime_Update", CommandType.StoredProcedure, sqlParameters); 
        }

		/// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="timeid">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateStatus(string ids, string Status)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ids", ids),
				new SqlParameter("@status", Status)
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_settime_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID，多个用,分隔</param>
        /// <returns>返回操作结果</returns>
        public int Delete(string timeid, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@timeid", timeid),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };
			sqlParameters[1].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_settime_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }
    }
}