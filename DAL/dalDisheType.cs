using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public partial class dalDisheType
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
		int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref DisheTypeEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@lsid", Entity.lsid),
				new SqlParameter("@distypeid", Entity.distypeid),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@pdistypecode", Entity.pdistypecode),
				new SqlParameter("@distypecode", Entity.distypecode),
				new SqlParameter("@dispath", Entity.dispath),
				new SqlParameter("@distypename", Entity.distypename),
				new SqlParameter("@metcode", Entity.metcode),
				new SqlParameter("@fincode", Entity.fincode),
				new SqlParameter("@maxdiscount", Entity.maxdiscount),
				new SqlParameter("@busSort", Entity.busSort),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_DisheType_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.lsid = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(DisheTypeEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@lsid", Entity.lsid),
				new SqlParameter("@distypeid", Entity.distypeid),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@pdistypecode", Entity.pdistypecode),
				new SqlParameter("@distypecode", Entity.distypecode),
				new SqlParameter("@dispath", Entity.dispath),
				new SqlParameter("@distypename", Entity.distypename),
				new SqlParameter("@metcode", Entity.metcode),
				new SqlParameter("@fincode", Entity.fincode),
				new SqlParameter("@maxdiscount", Entity.maxdiscount),
				new SqlParameter("@busSort", Entity.busSort),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_DisheType_Update", CommandType.StoredProcedure, sqlParameters); 
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
            return DBHelper.ExecuteNonQuery("dbo.p_DisheType_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
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
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_DisheType_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }
    }
}