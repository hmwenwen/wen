using System.Data;
using System.Data.SqlClient;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 用户消息表数据访问类
    /// </summary>
    public partial class dalWX_usermessage
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref WX_usermessageEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@id", Entity.id),
				new SqlParameter("@openid", Entity.openid),
				new SqlParameter("@msgtype", Entity.msgtype),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@msgdetails", Entity.msgdetails),
				new SqlParameter("@title", Entity.title),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_usermessage_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.id = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(WX_usermessageEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@id", Entity.id),
				new SqlParameter("@openid", Entity.openid),
				new SqlParameter("@msgtype", Entity.msgtype),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@msgdetails", Entity.msgdetails),
				new SqlParameter("@title", Entity.title),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_usermessage_Update", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateStatus(string ids, string Status)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ids", ids),
				new SqlParameter("@status", Status)
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_usermessage_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID，多个用,分隔</param>
        /// <returns>返回操作结果</returns>
        public int Delete(string id, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@id", id),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };
            sqlParameters[1].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_usermessage_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }

        /// <summary>
        /// 消息中心
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public DataTable MyMesInfo(string openid)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid)
             };

            return DBHelper.ExecuteDataTable("p_mymesinfo", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 消息中心列表
        /// </summary>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="openid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable MyMesList(string currentpage, string pagesize, string openid, string type, ref string sumcount)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@currentpage", currentpage),
                 new SqlParameter("@pagesize", pagesize),
                 new SqlParameter("@type", type),
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@sumcount",SqlDbType.VarChar ,64, sumcount)
             };

            sqlParameters[4].Direction = ParameterDirection.Output;
            DataTable dt = DBHelper.ExecuteDataTable("p_mymeslist", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[4].Value.ToString();
            return dt;
        }

        /// <summary>
        /// 批量删除消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="ids"></param>
        public int DelMesList(string openid, string ids)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@ids",ids),
                new SqlParameter("@openid",openid)
            };

            return DBHelper.ExecuteNonQuery("p_delete_mymes", CommandType.StoredProcedure, sqlParameters);
        }
    }
}