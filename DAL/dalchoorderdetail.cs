using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 餐收_点餐订单2数据访问类
    /// </summary>
    public partial class dalchoorderdetail
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
		int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref choorderdetailEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@id", Entity.id),
				new SqlParameter("@detailid", Entity.detailid),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@orderid", Entity.orderid),
				new SqlParameter("@personcount", Entity.personcount),
				new SqlParameter("@opentime", Entity.opentime),
				new SqlParameter("@checkouttime", Entity.checkouttime),
				new SqlParameter("@alltime", Entity.alltime),
				new SqlParameter("@endtime", Entity.endtime),
				new SqlParameter("@shiftid", Entity.shiftid),
				new SqlParameter("@tmcode", Entity.tmcode),
				new SqlParameter("@tmname", Entity.tmname),
				new SqlParameter("@tablecodes", Entity.tablecodes),
				new SqlParameter("@combinenum", Entity.combinenum),
				new SqlParameter("@allfoodtime", Entity.allfoodtime),
				new SqlParameter("@pushfoodtime", Entity.pushfoodtime),
				new SqlParameter("@pushfoodstate", Entity.pushfoodstate),
				new SqlParameter("@closeaccount", Entity.closeaccount),
				new SqlParameter("@closeCodes", Entity.closeCodes),
				new SqlParameter("@calltime", Entity.calltime),
				new SqlParameter("@userid", Entity.userid),
				new SqlParameter("@ordertime", Entity.ordertime),
				new SqlParameter("@surchargeamount", Entity.surchargeamount),
				new SqlParameter("@ocostprice", Entity.ocostprice),
				new SqlParameter("@conmoney", Entity.conmoney),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@detailcode", Entity.detailcode),
				new SqlParameter("@memcode", Entity.memcode),
				new SqlParameter("@desCode", Entity.desCode),
				new SqlParameter("@desctime", Entity.desctime),
				new SqlParameter("@printcount", Entity.printcount),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@metname", Entity.metname),
				new SqlParameter("@pushid", Entity.pushid),
				new SqlParameter("@pushname", Entity.pushname),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@ispresented", Entity.ispresented),
				new SqlParameter("@presentedcode", Entity.presentedcode),
				new SqlParameter("@cardcode", Entity.cardcode),
				new SqlParameter("@mobile", Entity.mobile),
				new SqlParameter("@coupons", Entity.coupons),
				new SqlParameter("@metcode", Entity.metcode),
				new SqlParameter("@ttcode", Entity.ttcode),
				new SqlParameter("@customer", Entity.customer),
				new SqlParameter("@amanagerid", Entity.amanagerid),
				new SqlParameter("@amanagername", Entity.amanagername),
				new SqlParameter("@olossmoney", Entity.olossmoney),
				new SqlParameter("@detailtype", Entity.detailtype),
				new SqlParameter("@predishemoney", Entity.predishemoney),
				new SqlParameter("@TerminalType", Entity.TerminalType),
				new SqlParameter("@depcode", Entity.depcode),
				new SqlParameter("@depname", Entity.depname),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_choorderdetail_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.id = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(choorderdetailEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@id", Entity.id),
				new SqlParameter("@detailid", Entity.detailid),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@orderid", Entity.orderid),
				new SqlParameter("@personcount", Entity.personcount),
				new SqlParameter("@opentime", Entity.opentime),
				new SqlParameter("@checkouttime", Entity.checkouttime),
				new SqlParameter("@alltime", Entity.alltime),
				new SqlParameter("@endtime", Entity.endtime),
				new SqlParameter("@shiftid", Entity.shiftid),
				new SqlParameter("@tmcode", Entity.tmcode),
				new SqlParameter("@tmname", Entity.tmname),
				new SqlParameter("@tablecodes", Entity.tablecodes),
				new SqlParameter("@combinenum", Entity.combinenum),
				new SqlParameter("@allfoodtime", Entity.allfoodtime),
				new SqlParameter("@pushfoodtime", Entity.pushfoodtime),
				new SqlParameter("@pushfoodstate", Entity.pushfoodstate),
				new SqlParameter("@closeaccount", Entity.closeaccount),
				new SqlParameter("@closeCodes", Entity.closeCodes),
				new SqlParameter("@calltime", Entity.calltime),
				new SqlParameter("@userid", Entity.userid),
				new SqlParameter("@ordertime", Entity.ordertime),
				new SqlParameter("@surchargeamount", Entity.surchargeamount),
				new SqlParameter("@ocostprice", Entity.ocostprice),
				new SqlParameter("@conmoney", Entity.conmoney),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@detailcode", Entity.detailcode),
				new SqlParameter("@memcode", Entity.memcode),
				new SqlParameter("@desCode", Entity.desCode),
				new SqlParameter("@desctime", Entity.desctime),
				new SqlParameter("@printcount", Entity.printcount),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@metname", Entity.metname),
				new SqlParameter("@pushid", Entity.pushid),
				new SqlParameter("@pushname", Entity.pushname),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@ispresented", Entity.ispresented),
				new SqlParameter("@presentedcode", Entity.presentedcode),
				new SqlParameter("@cardcode", Entity.cardcode),
				new SqlParameter("@mobile", Entity.mobile),
				new SqlParameter("@coupons", Entity.coupons),
				new SqlParameter("@metcode", Entity.metcode),
				new SqlParameter("@ttcode", Entity.ttcode),
				new SqlParameter("@customer", Entity.customer),
				new SqlParameter("@amanagerid", Entity.amanagerid),
				new SqlParameter("@amanagername", Entity.amanagername),
				new SqlParameter("@olossmoney", Entity.olossmoney),
				new SqlParameter("@detailtype", Entity.detailtype),
				new SqlParameter("@predishemoney", Entity.predishemoney),
				new SqlParameter("@TerminalType", Entity.TerminalType),
				new SqlParameter("@depcode", Entity.depcode),
				new SqlParameter("@depname", Entity.depname),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_choorderdetail_Update", CommandType.StoredProcedure, sqlParameters); 
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
            return DBHelper.ExecuteNonQuery("dbo.p_choorderdetail_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
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
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_choorderdetail_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }
    }
}