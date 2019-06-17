using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public partial class daldishes
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
		int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref dishesEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@lsid", Entity.lsid),
				new SqlParameter("@disid", Entity.disid),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@discode", Entity.discode),
				new SqlParameter("@disname", Entity.disname),
				new SqlParameter("@melcode", Entity.melcode),
				new SqlParameter("@disothername", Entity.disothername),
				new SqlParameter("@distypecode", Entity.distypecode),
				new SqlParameter("@quickcode", Entity.quickcode),
				new SqlParameter("@customcode", Entity.customcode),
				new SqlParameter("@unit", Entity.unit),
				new SqlParameter("@price", Entity.price),
				new SqlParameter("@memberprice", Entity.memberprice),
				new SqlParameter("@ismultiprice", Entity.ismultiprice),
				new SqlParameter("@costprice", Entity.costprice),
				new SqlParameter("@iscostbyingredient", Entity.iscostbyingredient),
				new SqlParameter("@pushmoney", Entity.pushmoney),
				new SqlParameter("@matclscode", Entity.matclscode),
				new SqlParameter("@matcode", Entity.matcode),
				new SqlParameter("@extcode", Entity.extcode),
				new SqlParameter("@fincode", Entity.fincode),
				new SqlParameter("@dcode", Entity.dcode),
				new SqlParameter("@kitcode", Entity.kitcode),
				new SqlParameter("@ecode", Entity.ecode),
				new SqlParameter("@maketime", Entity.maketime),
				new SqlParameter("@qrcode", Entity.qrcode),
				new SqlParameter("@dispicture", Entity.dispicture),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@isentity", Entity.isentity),
				new SqlParameter("@entitydefcount", Entity.entitydefcount),
				new SqlParameter("@entityprice", Entity.entityprice),
				new SqlParameter("@iscanmodifyprice", Entity.iscanmodifyprice),
				new SqlParameter("@isneedweigh", Entity.isneedweigh),
				new SqlParameter("@isneedmethod", Entity.isneedmethod),
				new SqlParameter("@iscaninventory", Entity.iscaninventory),
				new SqlParameter("@iscancustom", Entity.iscancustom),
				new SqlParameter("@iscandeposit", Entity.iscandeposit),
				new SqlParameter("@isallowmemberprice", Entity.isallowmemberprice),
				new SqlParameter("@isattachcalculate", Entity.isattachcalculate),
				new SqlParameter("@isclipcoupons", Entity.isclipcoupons),
				new SqlParameter("@iscombo", Entity.iscombo),
				new SqlParameter("@iscombooptional", Entity.iscombooptional),
				new SqlParameter("@isnonoperating", Entity.isnonoperating),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@busSort", Entity.busSort),
				new SqlParameter("@warcode", Entity.warcode),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_dishes_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.lsid = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(dishesEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@lsid", Entity.lsid),
				new SqlParameter("@disid", Entity.disid),
				new SqlParameter("@buscode", Entity.buscode),
				new SqlParameter("@stocode", Entity.stocode),
				new SqlParameter("@discode", Entity.discode),
				new SqlParameter("@disname", Entity.disname),
				new SqlParameter("@melcode", Entity.melcode),
				new SqlParameter("@disothername", Entity.disothername),
				new SqlParameter("@distypecode", Entity.distypecode),
				new SqlParameter("@quickcode", Entity.quickcode),
				new SqlParameter("@customcode", Entity.customcode),
				new SqlParameter("@unit", Entity.unit),
				new SqlParameter("@price", Entity.price),
				new SqlParameter("@memberprice", Entity.memberprice),
				new SqlParameter("@ismultiprice", Entity.ismultiprice),
				new SqlParameter("@costprice", Entity.costprice),
				new SqlParameter("@iscostbyingredient", Entity.iscostbyingredient),
				new SqlParameter("@pushmoney", Entity.pushmoney),
				new SqlParameter("@matclscode", Entity.matclscode),
				new SqlParameter("@matcode", Entity.matcode),
				new SqlParameter("@extcode", Entity.extcode),
				new SqlParameter("@fincode", Entity.fincode),
				new SqlParameter("@dcode", Entity.dcode),
				new SqlParameter("@kitcode", Entity.kitcode),
				new SqlParameter("@ecode", Entity.ecode),
				new SqlParameter("@maketime", Entity.maketime),
				new SqlParameter("@qrcode", Entity.qrcode),
				new SqlParameter("@dispicture", Entity.dispicture),
				new SqlParameter("@remark", Entity.remark),
				new SqlParameter("@isentity", Entity.isentity),
				new SqlParameter("@entitydefcount", Entity.entitydefcount),
				new SqlParameter("@entityprice", Entity.entityprice),
				new SqlParameter("@iscanmodifyprice", Entity.iscanmodifyprice),
				new SqlParameter("@isneedweigh", Entity.isneedweigh),
				new SqlParameter("@isneedmethod", Entity.isneedmethod),
				new SqlParameter("@iscaninventory", Entity.iscaninventory),
				new SqlParameter("@iscancustom", Entity.iscancustom),
				new SqlParameter("@iscandeposit", Entity.iscandeposit),
				new SqlParameter("@isallowmemberprice", Entity.isallowmemberprice),
				new SqlParameter("@isattachcalculate", Entity.isattachcalculate),
				new SqlParameter("@isclipcoupons", Entity.isclipcoupons),
				new SqlParameter("@iscombo", Entity.iscombo),
				new SqlParameter("@iscombooptional", Entity.iscombooptional),
				new SqlParameter("@isnonoperating", Entity.isnonoperating),
				new SqlParameter("@status", Entity.status),
				new SqlParameter("@busSort", Entity.busSort),
				new SqlParameter("@warcode", Entity.warcode),
				new SqlParameter("@cuser", Entity.cuser),
				new SqlParameter("@uuser", Entity.uuser),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_dishes_Update", CommandType.StoredProcedure, sqlParameters); 
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
            return DBHelper.ExecuteNonQuery("dbo.p_dishes_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
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
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_dishes_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }
    }
}