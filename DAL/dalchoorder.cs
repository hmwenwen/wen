using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 餐收_桌台点餐1数据访问类
    /// </summary>
    public partial class dalchoorder
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
		int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref choorderEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@lsid", Entity.lsid),
				new SqlParameter("@orderid", Entity.orderid),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@strcode", Entity.strcode),
				new SqlParameter("@shiftid", Entity.shiftid),
				new SqlParameter("@tmcode", Entity.tmcode),
				new SqlParameter("@personnum", Entity.personnum),
				new SqlParameter("@username", Entity.username),
				new SqlParameter("@userphone", Entity.userphone),
				new SqlParameter("@arrivetime", Entity.arrivetime),
				new SqlParameter("@opentime", Entity.opentime),
				new SqlParameter("@restime", Entity.restime),
				new SqlParameter("@checkouttime", Entity.checkouttime),
				new SqlParameter("@gusetleavetime", Entity.gusetleavetime),
				new SqlParameter("@alltime", Entity.alltime),
				new SqlParameter("@allfoodtime", Entity.allfoodtime),
				new SqlParameter("@conmoney", Entity.conmoney),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_choorder_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.lsid = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(choorderEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@lsid", Entity.lsid),
				new SqlParameter("@orderid", Entity.orderid),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@strcode", Entity.strcode),
				new SqlParameter("@shiftid", Entity.shiftid),
				new SqlParameter("@tmcode", Entity.tmcode),
				new SqlParameter("@personnum", Entity.personnum),
				new SqlParameter("@username", Entity.username),
				new SqlParameter("@userphone", Entity.userphone),
				new SqlParameter("@arrivetime", Entity.arrivetime),
				new SqlParameter("@opentime", Entity.opentime),
				new SqlParameter("@restime", Entity.restime),
				new SqlParameter("@checkouttime", Entity.checkouttime),
				new SqlParameter("@gusetleavetime", Entity.gusetleavetime),
				new SqlParameter("@alltime", Entity.alltime),
				new SqlParameter("@allfoodtime", Entity.allfoodtime),
				new SqlParameter("@conmoney", Entity.conmoney),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_choorder_Update", CommandType.StoredProcedure, sqlParameters); 
        }

		/// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="lsid">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateStatus(string ids, string Status)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ids", ids),
				new SqlParameter("@status", Status)
             };
            return DBHelper.ExecuteNonQuery("dbo.p_choorder_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID，多个用,分隔</param>
        /// <returns>返回操作结果</returns>
        public int Delete(string lsid, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@lsid", lsid),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };
			sqlParameters[1].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_choorder_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }
    }
}