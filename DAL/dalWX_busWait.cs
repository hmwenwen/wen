using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 客户排队表数据访问类
    /// </summary>
    public partial class dalWX_busWait
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        DataTable dt = new DataTable();
        int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref WX_busWaitEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@bwid", Entity.bwid),
				new SqlParameter("@serialNumber", Entity.serialNumber),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@strcode", Entity.strcode),
				new SqlParameter("@busTop", Entity.busTop),
				new SqlParameter("@sortNum", Entity.sortNum),
				new SqlParameter("@busDate", Entity.busDate),
				new SqlParameter("@waitType", Entity.waitType),
				new SqlParameter("@userName", Entity.userName),
				new SqlParameter("@tel", Entity.tel),
				new SqlParameter("@waitTime", Entity.waitTime),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
				new SqlParameter("@openid", Entity.openid),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_busWait_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.bwid = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(WX_busWaitEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@bwid", Entity.bwid),
				new SqlParameter("@serialNumber", Entity.serialNumber),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@strcode", Entity.strcode),
				new SqlParameter("@busTop", Entity.busTop),
				new SqlParameter("@sortNum", Entity.sortNum),
				new SqlParameter("@busDate", Entity.busDate),
				new SqlParameter("@waitType", Entity.waitType),
				new SqlParameter("@userName", Entity.userName),
				new SqlParameter("@tel", Entity.tel),
				new SqlParameter("@waitTime", Entity.waitTime),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
				new SqlParameter("@openid", Entity.openid),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_busWait_Update", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="bwid">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateStatus(string ids, string Status)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ids", ids),
				new SqlParameter("@status", Status)
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_busWait_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID，多个用,分隔</param>
        /// <returns>返回操作结果</returns>
        public int Delete(string bwid, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@bwid", bwid),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };
            sqlParameters[1].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_busWait_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }

        //排队记录
        public DataTable GetWaitList(string openid, string type, string currentpage, string pagesize, ref string sumcount)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@type",type),
                 new SqlParameter("@currentpage",currentpage),
                 new SqlParameter("@pagesize",pagesize),
                 new SqlParameter("@sumcount",SqlDbType.NVarChar ,256,sumcount)
             };

            sqlParameters[4].Direction = ParameterDirection.Output;
            dt = DBHelper.ExecuteDataTable("p_getwaitlist", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[4].Value.ToString();
            return dt;
        }

        //排队
        public DataTable AddWaitInfo(string openid, string lineid, string stocode, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@lineid",lineid),
                 new SqlParameter("@stocode",stocode),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };

            sqlParameters[3].Direction = ParameterDirection.Output;
            dt = DBHelper.ExecuteDataTable("p_addwait", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[3].Value.ToString();
            return dt;
        }
    }
}