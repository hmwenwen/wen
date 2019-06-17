using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 客户预订表数据访问类
    /// </summary>
    public partial class dalWX_busDestine
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref WX_busDestineEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ID", Entity.ID),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@desDate", Entity.desDate),
				new SqlParameter("@desTime", Entity.desTime),
				new SqlParameter("@dicid", Entity.dicid),
				new SqlParameter("@personNum", Entity.personNum),
				new SqlParameter("@metcode", Entity.metcode),
				new SqlParameter("@userName", Entity.userName),
				new SqlParameter("@tel", Entity.tel),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@dishesremark", Entity.dishesremark),
				new SqlParameter("@TerminalType", Entity.TerminalType),
				new SqlParameter("@openid", Entity.openid),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_busDestine_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.ID = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(WX_busDestineEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ID", Entity.ID),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@desDate", Entity.desDate),
				new SqlParameter("@desTime", Entity.desTime),
				new SqlParameter("@dicid", Entity.dicid),
				new SqlParameter("@personNum", Entity.personNum),
				new SqlParameter("@metcode", Entity.metcode),
				new SqlParameter("@userName", Entity.userName),
				new SqlParameter("@tel", Entity.tel),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@dishesremark", Entity.dishesremark),
				new SqlParameter("@TerminalType", Entity.TerminalType),
				new SqlParameter("@openid", Entity.openid),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_busDestine_Update", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="ID">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateStatus(string ids, string Status)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ids", ids),
				new SqlParameter("@status", Status)
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_busDestine_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID，多个用,分隔</param>
        /// <returns>返回操作结果</returns>
        public int Delete(string ID, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@ID", ID),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };
            sqlParameters[1].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_busDestine_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }

        //获取预约今天 明天 后台预约详细信息及备注信息
        public DataSet GetReserveTimes(string stocode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@stocode", stocode)
             };

            return DBHelper.ExecuteDataSet("p_GetReserveTimes", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        ///获取指定日期预约时间及预定状态
        /// </summary>
        /// <param name="stocode"></param>
        /// <param name="currentdate"></param>
        /// <returns></returns>
        public DataSet GetReserveTime(string stocode, string currentdate)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@stocode", stocode),
                 new SqlParameter("@currentdate",currentdate)
             };

            return DBHelper.ExecuteDataSet("p_GetReserveTime", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 取消预定
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="stocode"></param>
        /// <param name="orderid"></param>
        /// <param name="mescode"></param>
        public void CancelReserve(string openid, string stocode, string orderid, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@stocode",stocode),
                 new SqlParameter("@orderid",orderid),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };
            sqlParameters[3].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("dbo.p_CancelReserve", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[3].Value.ToString();
        }

        //预定 检测
        public void CheckReserve(string openid, string stocode, string sdate, string stime, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@stocode",stocode),
                 new SqlParameter("@sdate",sdate),
                 new SqlParameter("@stime",stime),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };
            sqlParameters[4].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("dbo.p_CheckReserve", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[4].Value.ToString();
        }

        //预约
        public DataTable AddReserve(string openid, string stocode, string rdate, string rtime, int usernum, string remark)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@stocode",stocode),
                 new SqlParameter("@rdate",rdate),
                 new SqlParameter("@rtime",rtime),
                 new SqlParameter("@usernum",usernum),
                 new SqlParameter("@remark",remark)
             };

            return DBHelper.ExecuteDataTable("dbo.p_AddReserve", CommandType.StoredProcedure, sqlParameters);
        }

        //预约记录
        public DataTable GetReserveRecordlist(string openid, string type, string currentpage, string pagesize, ref string sumcount)
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
            DataTable dt = DBHelper.ExecuteDataTable("dbo.p_getReserveRecordlist", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[4].Value.ToString();

            return dt;
        }
    }
}