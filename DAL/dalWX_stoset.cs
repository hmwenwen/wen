using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 门店设置数据访问类
    /// </summary>
    public partial class dalWX_stoset
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
		int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref WX_stosetEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@setreservationid", Entity.setreservationid),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@isnetwork", Entity.isnetwork),
				new SqlParameter("@isqueue", Entity.isqueue),
				new SqlParameter("@isaddfood", Entity.isaddfood),
				new SqlParameter("@festival", Entity.festival),
				new SqlParameter("@weekend", Entity.weekend),
				new SqlParameter("@ntime", Entity.ntime),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_stoset_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.setreservationid = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(WX_stosetEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@setreservationid", Entity.setreservationid),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@isnetwork", Entity.isnetwork),
				new SqlParameter("@isqueue", Entity.isqueue),
				new SqlParameter("@isaddfood", Entity.isaddfood),
				new SqlParameter("@festival", Entity.festival),
				new SqlParameter("@weekend", Entity.weekend),
				new SqlParameter("@ntime", Entity.ntime),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_stoset_Update", CommandType.StoredProcedure, sqlParameters); 
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
            return DBHelper.ExecuteNonQuery("dbo.p_WX_stoset_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
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
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_stoset_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }
    }
}