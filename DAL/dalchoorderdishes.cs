using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 餐收_点餐菜品2-1数据访问类
    /// </summary>
    public partial class dalchoorderdishes
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
		int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref choorderdishesEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@lsid", Entity.lsid),
				new SqlParameter("@orderdishesid", Entity.orderdishesid),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@orderid", Entity.orderid),
				new SqlParameter("@detailid", Entity.detailid),
				new SqlParameter("@distypecode", Entity.distypecode),
				new SqlParameter("@dtypecode", Entity.dtypecode),
				new SqlParameter("@melcode", Entity.melcode),
				new SqlParameter("@discode", Entity.discode),
				new SqlParameter("@disname", Entity.disname),
				new SqlParameter("@disothername", Entity.disothername),
				new SqlParameter("@disnum", Entity.disnum),
				new SqlParameter("@disnumconst", Entity.disnumconst),
				new SqlParameter("@addnum", Entity.addnum),
				new SqlParameter("@isneedmethod", Entity.isneedmethod),
				new SqlParameter("@distacode", Entity.distacode),
				new SqlParameter("@distaname", Entity.distaname),
				new SqlParameter("@ordertime", Entity.ordertime),
				new SqlParameter("@addtime", Entity.addtime),
				new SqlParameter("@calltime", Entity.calltime),
				new SqlParameter("@pushfoodtime", Entity.pushfoodtime),
				new SqlParameter("@pushfoodstate", Entity.pushfoodstate),
				new SqlParameter("@isentity", Entity.isentity),
				new SqlParameter("@entitydefcount", Entity.entitydefcount),
				new SqlParameter("@entityprice", Entity.entityprice),
				new SqlParameter("@singlenum", Entity.singlenum),
				new SqlParameter("@singleAllmoney", Entity.singleAllmoney),
				new SqlParameter("@totaladdmoney", Entity.totaladdmoney),
				new SqlParameter("@totaladdmoneydiscount", Entity.totaladdmoneydiscount),
				new SqlParameter("@allmoney", Entity.allmoney),
				new SqlParameter("@allmoneydiscount", Entity.allmoneydiscount),
				new SqlParameter("@memberallmoney", Entity.memberallmoney),
				new SqlParameter("@resultallmoney", Entity.resultallmoney),
				new SqlParameter("@packageaddmoney", Entity.packageaddmoney),
				new SqlParameter("@ispackage", Entity.ispackage),
				new SqlParameter("@iscanout", Entity.iscanout),
				new SqlParameter("@isout", Entity.isout),
				new SqlParameter("@refundNum", Entity.refundNum),
				new SqlParameter("@refundaddnum", Entity.refundaddnum),
				new SqlParameter("@oneprice", Entity.oneprice),
				new SqlParameter("@memberprice", Entity.memberprice),
				new SqlParameter("@costprice", Entity.costprice),
				new SqlParameter("@methodmoney", Entity.methodmoney),
				new SqlParameter("@methodmoneydiscount", Entity.methodmoneydiscount),
				new SqlParameter("@attachmoney", Entity.attachmoney),
				new SqlParameter("@pushmoney", Entity.pushmoney),
				new SqlParameter("@ismember", Entity.ismember),
				new SqlParameter("@ispre", Entity.ispre),
				new SqlParameter("@pretype", Entity.pretype),
				new SqlParameter("@dispcode", Entity.dispcode),
				new SqlParameter("@discountratemax", Entity.discountratemax),
				new SqlParameter("@discountrate", Entity.discountrate),
				new SqlParameter("@premoney", Entity.premoney),
				new SqlParameter("@precheck", Entity.precheck),
				new SqlParameter("@checktime", Entity.checktime),
				new SqlParameter("@ismustconsume", Entity.ismustconsume),
				new SqlParameter("@mustconsumenum", Entity.mustconsumenum),
				new SqlParameter("@iscaninventory", Entity.iscaninventory),
				new SqlParameter("@isallowmemberprice", Entity.isallowmemberprice),
				new SqlParameter("@isattachcalculate", Entity.isattachcalculate),
				new SqlParameter("@isclipcoupons", Entity.isclipcoupons),
				new SqlParameter("@isnonoperating", Entity.isnonoperating),
				new SqlParameter("@iscombooptional", Entity.iscombooptional),
				new SqlParameter("@isneedweigh", Entity.isneedweigh),
				new SqlParameter("@iscanmodifyprice", Entity.iscanmodifyprice),
				new SqlParameter("@matcode", Entity.matcode),
				new SqlParameter("@cguid", Entity.cguid),
				new SqlParameter("@pcguid", Entity.pcguid),
				new SqlParameter("@porderdishesid", Entity.porderdishesid),
				new SqlParameter("@comdiscode", Entity.comdiscode),
				new SqlParameter("@comgcode", Entity.comgcode),
				new SqlParameter("@composetype", Entity.composetype),
				new SqlParameter("@allowkinds", Entity.allowkinds),
				new SqlParameter("@allowcount", Entity.allowcount),
				new SqlParameter("@allowamount", Entity.allowamount),
				new SqlParameter("@usedisdefaultamount", Entity.usedisdefaultamount),
				new SqlParameter("@usedismaxamount", Entity.usedismaxamount),
				new SqlParameter("@unit", Entity.unit),
				new SqlParameter("@extcode", Entity.extcode),
				new SqlParameter("@fincode", Entity.fincode),
				new SqlParameter("@dcode", Entity.dcode),
				new SqlParameter("@kitcode", Entity.kitcode),
				new SqlParameter("@ecode", Entity.ecode),
				new SqlParameter("@warcode", Entity.warcode),
				new SqlParameter("@totmcode", Entity.totmcode),
				new SqlParameter("@totmname", Entity.totmname),
				new SqlParameter("@todetailid", Entity.todetailid),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@makestatus", Entity.makestatus),
				new SqlParameter("@operaretype", Entity.operaretype),
				new SqlParameter("@isdiscount", Entity.isdiscount),
				new SqlParameter("@priceispre", Entity.priceispre),
				new SqlParameter("@ispresented", Entity.ispresented),
				new SqlParameter("@pecode", Entity.pecode),
				new SqlParameter("@pename", Entity.pename),
				new SqlParameter("@prereason", Entity.prereason),
				new SqlParameter("@prereasontype", Entity.prereasontype),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@orderremark", Entity.orderremark),
				new SqlParameter("@gguid", Entity.gguid),
				new SqlParameter("@nopreremark", Entity.nopreremark),
				new SqlParameter("@storeupdated", Entity.storeupdated),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@comprice", Entity.comprice),
				new SqlParameter("@cusername", Entity.cusername),
				new SqlParameter("@tottcode", Entity.tottcode),
				new SqlParameter("@metname", Entity.metname),
				new SqlParameter("@metcode", Entity.metcode),
				new SqlParameter("@detailcode", Entity.detailcode),
				new SqlParameter("@tmopentime", Entity.tmopentime),
				new SqlParameter("@chomac", Entity.chomac),
				new SqlParameter("@orderno", Entity.orderno),
				new SqlParameter("@subtype", Entity.subtype),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_choorderdishes_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.lsid = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(choorderdishesEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@lsid", Entity.lsid),
				new SqlParameter("@orderdishesid", Entity.orderdishesid),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@orderid", Entity.orderid),
				new SqlParameter("@detailid", Entity.detailid),
				new SqlParameter("@distypecode", Entity.distypecode),
				new SqlParameter("@dtypecode", Entity.dtypecode),
				new SqlParameter("@melcode", Entity.melcode),
				new SqlParameter("@discode", Entity.discode),
				new SqlParameter("@disname", Entity.disname),
				new SqlParameter("@disothername", Entity.disothername),
				new SqlParameter("@disnum", Entity.disnum),
				new SqlParameter("@disnumconst", Entity.disnumconst),
				new SqlParameter("@addnum", Entity.addnum),
				new SqlParameter("@isneedmethod", Entity.isneedmethod),
				new SqlParameter("@distacode", Entity.distacode),
				new SqlParameter("@distaname", Entity.distaname),
				new SqlParameter("@ordertime", Entity.ordertime),
				new SqlParameter("@addtime", Entity.addtime),
				new SqlParameter("@calltime", Entity.calltime),
				new SqlParameter("@pushfoodtime", Entity.pushfoodtime),
				new SqlParameter("@pushfoodstate", Entity.pushfoodstate),
				new SqlParameter("@isentity", Entity.isentity),
				new SqlParameter("@entitydefcount", Entity.entitydefcount),
				new SqlParameter("@entityprice", Entity.entityprice),
				new SqlParameter("@singlenum", Entity.singlenum),
				new SqlParameter("@singleAllmoney", Entity.singleAllmoney),
				new SqlParameter("@totaladdmoney", Entity.totaladdmoney),
				new SqlParameter("@totaladdmoneydiscount", Entity.totaladdmoneydiscount),
				new SqlParameter("@allmoney", Entity.allmoney),
				new SqlParameter("@allmoneydiscount", Entity.allmoneydiscount),
				new SqlParameter("@memberallmoney", Entity.memberallmoney),
				new SqlParameter("@resultallmoney", Entity.resultallmoney),
				new SqlParameter("@packageaddmoney", Entity.packageaddmoney),
				new SqlParameter("@ispackage", Entity.ispackage),
				new SqlParameter("@iscanout", Entity.iscanout),
				new SqlParameter("@isout", Entity.isout),
				new SqlParameter("@refundNum", Entity.refundNum),
				new SqlParameter("@refundaddnum", Entity.refundaddnum),
				new SqlParameter("@oneprice", Entity.oneprice),
				new SqlParameter("@memberprice", Entity.memberprice),
				new SqlParameter("@costprice", Entity.costprice),
				new SqlParameter("@methodmoney", Entity.methodmoney),
				new SqlParameter("@methodmoneydiscount", Entity.methodmoneydiscount),
				new SqlParameter("@attachmoney", Entity.attachmoney),
				new SqlParameter("@pushmoney", Entity.pushmoney),
				new SqlParameter("@ismember", Entity.ismember),
				new SqlParameter("@ispre", Entity.ispre),
				new SqlParameter("@pretype", Entity.pretype),
				new SqlParameter("@dispcode", Entity.dispcode),
				new SqlParameter("@discountratemax", Entity.discountratemax),
				new SqlParameter("@discountrate", Entity.discountrate),
				new SqlParameter("@premoney", Entity.premoney),
				new SqlParameter("@precheck", Entity.precheck),
				new SqlParameter("@checktime", Entity.checktime),
				new SqlParameter("@ismustconsume", Entity.ismustconsume),
				new SqlParameter("@mustconsumenum", Entity.mustconsumenum),
				new SqlParameter("@iscaninventory", Entity.iscaninventory),
				new SqlParameter("@isallowmemberprice", Entity.isallowmemberprice),
				new SqlParameter("@isattachcalculate", Entity.isattachcalculate),
				new SqlParameter("@isclipcoupons", Entity.isclipcoupons),
				new SqlParameter("@isnonoperating", Entity.isnonoperating),
				new SqlParameter("@iscombooptional", Entity.iscombooptional),
				new SqlParameter("@isneedweigh", Entity.isneedweigh),
				new SqlParameter("@iscanmodifyprice", Entity.iscanmodifyprice),
				new SqlParameter("@matcode", Entity.matcode),
				new SqlParameter("@cguid", Entity.cguid),
				new SqlParameter("@pcguid", Entity.pcguid),
				new SqlParameter("@porderdishesid", Entity.porderdishesid),
				new SqlParameter("@comdiscode", Entity.comdiscode),
				new SqlParameter("@comgcode", Entity.comgcode),
				new SqlParameter("@composetype", Entity.composetype),
				new SqlParameter("@allowkinds", Entity.allowkinds),
				new SqlParameter("@allowcount", Entity.allowcount),
				new SqlParameter("@allowamount", Entity.allowamount),
				new SqlParameter("@usedisdefaultamount", Entity.usedisdefaultamount),
				new SqlParameter("@usedismaxamount", Entity.usedismaxamount),
				new SqlParameter("@unit", Entity.unit),
				new SqlParameter("@extcode", Entity.extcode),
				new SqlParameter("@fincode", Entity.fincode),
				new SqlParameter("@dcode", Entity.dcode),
				new SqlParameter("@kitcode", Entity.kitcode),
				new SqlParameter("@ecode", Entity.ecode),
				new SqlParameter("@warcode", Entity.warcode),
				new SqlParameter("@totmcode", Entity.totmcode),
				new SqlParameter("@totmname", Entity.totmname),
				new SqlParameter("@todetailid", Entity.todetailid),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@makestatus", Entity.makestatus),
				new SqlParameter("@operaretype", Entity.operaretype),
				new SqlParameter("@isdiscount", Entity.isdiscount),
				new SqlParameter("@priceispre", Entity.priceispre),
				new SqlParameter("@ispresented", Entity.ispresented),
				new SqlParameter("@pecode", Entity.pecode),
				new SqlParameter("@pename", Entity.pename),
				new SqlParameter("@prereason", Entity.prereason),
				new SqlParameter("@prereasontype", Entity.prereasontype),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@orderremark", Entity.orderremark),
				new SqlParameter("@gguid", Entity.gguid),
				new SqlParameter("@nopreremark", Entity.nopreremark),
				new SqlParameter("@storeupdated", Entity.storeupdated),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@comprice", Entity.comprice),
				new SqlParameter("@cusername", Entity.cusername),
				new SqlParameter("@tottcode", Entity.tottcode),
				new SqlParameter("@metname", Entity.metname),
				new SqlParameter("@metcode", Entity.metcode),
				new SqlParameter("@detailcode", Entity.detailcode),
				new SqlParameter("@tmopentime", Entity.tmopentime),
				new SqlParameter("@chomac", Entity.chomac),
				new SqlParameter("@orderno", Entity.orderno),
				new SqlParameter("@subtype", Entity.subtype),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_choorderdishes_Update", CommandType.StoredProcedure, sqlParameters); 
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
            return DBHelper.ExecuteNonQuery("dbo.p_choorderdishes_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
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
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_choorderdishes_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }
    }
}