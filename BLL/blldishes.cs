using System.Collections.Generic;
using System.Data;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
namespace XJWZCatering.BLL
{
	/// <summary>
    /// 业务类
    /// </summary>
    public class blldishes : bllBase
    {
		DAL.daldishes dal = new DAL.daldishes();
        dishesEntity Entity = new dishesEntity();

		/// <summary>
        /// 检验表单数据
        /// </summary>
        /// <returns></returns>
        public string CheckPageInfo(string type, string lsid, string disid, string buscode, string stocode, string discode, string disname, string melcode, string disothername, string distypecode, string quickcode, string customcode, string unit, string price, string memberprice, string ismultiprice, string costprice, string iscostbyingredient, string pushmoney, string matclscode, string matcode, string extcode, string fincode, string dcode, string kitcode, string ecode, string maketime, string qrcode, string dispicture, string remark, string isentity, string entitydefcount, string entityprice, string iscanmodifyprice, string isneedweigh, string isneedmethod, string iscaninventory, string iscancustom, string iscandeposit, string isallowmemberprice, string isattachcalculate, string isclipcoupons, string iscombo, string iscombooptional, string isnonoperating, string status, string busSort, string warcode, string cuser, string uuser, out string spanids)
        {
			string strRetuen = string.Empty;
            spanids = string.Empty;
            //要验证的实体属性
            List<string> EName = new List<string>() {  };
            //要验证的实体属性值
            List<string> EValue = new List<string>() {  };
            //错误信息
            List<string> errorCode = new List<string>();
            List<string> ControlName = new List<string>();
            //验证数据
            CheckValue<dishesEntity>(EName, EValue, ref errorCode, ref ControlName, new dishesEntity());
            //特殊验证写在下面

            if (errorCode.Count > 0)
            {
                strRetuen = ErrMessage.GetMessageInfoByListCode(errorCode);
                spanids = ListTostring(ControlName);
            }
            else//组合对象数据
            {
                Entity = new dishesEntity();
				Entity.lsid = Helper.StringToLong(lsid);
				Entity.disid = Helper.StringToLong(disid);
				Entity.buscode = buscode;
				Entity.stocode = stocode;
				Entity.discode = discode;
				Entity.disname = disname;
				Entity.melcode = melcode;
				Entity.disothername = disothername;
				Entity.distypecode = distypecode;
				Entity.quickcode = quickcode;
				Entity.customcode = customcode;
				Entity.unit = unit;
				Entity.price = Helper.StringToDecimal(price);
				Entity.memberprice = Helper.StringToDecimal(memberprice);
				Entity.ismultiprice = ismultiprice;
				Entity.costprice = Helper.StringToDecimal(costprice);
				Entity.iscostbyingredient = iscostbyingredient;
				Entity.pushmoney = Helper.StringToDecimal(pushmoney);
				Entity.matclscode = matclscode;
				Entity.matcode = matcode;
				Entity.extcode = extcode;
				Entity.fincode = fincode;
				Entity.dcode = dcode;
				Entity.kitcode = kitcode;
				Entity.ecode = ecode;
				Entity.maketime = Helper.StringToInt(maketime);
				Entity.qrcode = qrcode;
				Entity.dispicture = dispicture;
				Entity.remark = remark;
				Entity.isentity = isentity;
				Entity.entitydefcount = Helper.StringToInt(entitydefcount);
				Entity.entityprice = Helper.StringToDecimal(entityprice);
				Entity.iscanmodifyprice = iscanmodifyprice;
				Entity.isneedweigh = isneedweigh;
				Entity.isneedmethod = isneedmethod;
				Entity.iscaninventory = iscaninventory;
				Entity.iscancustom = iscancustom;
				Entity.iscandeposit = iscandeposit;
				Entity.isallowmemberprice = isallowmemberprice;
				Entity.isattachcalculate = isattachcalculate;
				Entity.isclipcoupons = isclipcoupons;
				Entity.iscombo = iscombo;
				Entity.iscombooptional = iscombooptional;
				Entity.isnonoperating = isnonoperating;
				Entity.status = status;
				Entity.busSort = Helper.StringToInt(busSort);
				Entity.warcode = warcode;
				Entity.cuser = Helper.StringToLong(cuser);
				
				Entity.uuser = Helper.StringToLong(uuser);
				
				
            }
            return strRetuen;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public DataTable Add(string GUID, string UID, out  string lsid, string disid, string buscode, string stocode, string discode, string disname, string melcode, string disothername, string distypecode, string quickcode, string customcode, string unit, string price, string memberprice, string ismultiprice, string costprice, string iscostbyingredient, string pushmoney, string matclscode, string matcode, string extcode, string fincode, string dcode, string kitcode, string ecode, string maketime, string qrcode, string dispicture, string remark, string isentity, string entitydefcount, string entityprice, string iscanmodifyprice, string isneedweigh, string isneedmethod, string iscaninventory, string iscancustom, string iscandeposit, string isallowmemberprice, string isattachcalculate, string isclipcoupons, string iscombo, string iscombooptional, string isnonoperating, string status, string busSort, string warcode, string cuser, string uuser, operatelogEntity entity)
        {
			lsid = "0";
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }

            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("add",  lsid, disid, buscode, stocode, discode, disname, melcode, disothername, distypecode, quickcode, customcode, unit, price, memberprice, ismultiprice, costprice, iscostbyingredient, pushmoney, matclscode, matcode, extcode, fincode, dcode, kitcode, ecode, maketime, qrcode, dispicture, remark, isentity, entitydefcount, entityprice, iscanmodifyprice, isneedweigh, isneedmethod, iscaninventory, iscancustom, iscandeposit, isallowmemberprice, isattachcalculate, isclipcoupons, iscombo, iscombooptional, isnonoperating, status, busSort, warcode, cuser, uuser, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
            int result = dal.Add(ref Entity);
            //检测执行结果
            CheckResult(result);
            return dtBase;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public DataTable Update(string GUID, string UID,  string lsid, string disid, string buscode, string stocode, string discode, string disname, string melcode, string disothername, string distypecode, string quickcode, string customcode, string unit, string price, string memberprice, string ismultiprice, string costprice, string iscostbyingredient, string pushmoney, string matclscode, string matcode, string extcode, string fincode, string dcode, string kitcode, string ecode, string maketime, string qrcode, string dispicture, string remark, string isentity, string entitydefcount, string entityprice, string iscanmodifyprice, string isneedweigh, string isneedmethod, string iscaninventory, string iscancustom, string iscandeposit, string isallowmemberprice, string isattachcalculate, string isclipcoupons, string iscombo, string iscombooptional, string isnonoperating, string status, string busSort, string warcode, string cuser, string uuser, operatelogEntity entity)
        {
			
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("update",  lsid, disid, buscode, stocode, discode, disname, melcode, disothername, distypecode, quickcode, customcode, unit, price, memberprice, ismultiprice, costprice, iscostbyingredient, pushmoney, matclscode, matcode, extcode, fincode, dcode, kitcode, ecode, maketime, qrcode, dispicture, remark, isentity, entitydefcount, entityprice, iscanmodifyprice, isneedweigh, isneedmethod, iscaninventory, iscancustom, iscandeposit, isallowmemberprice, isattachcalculate, isclipcoupons, iscombo, iscombooptional, isnonoperating, status, busSort, warcode, cuser, uuser, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
			//获取更新前的数据对象
            dishesEntity OldEntity = new dishesEntity();
            OldEntity = GetEntitySigInfo(" where lsid='" + lsid + "'");
			//更新数据
            int result = dal.Update(Entity);
            //检测执行结果
            if (CheckResult(result))
            {
                //写日志
                if (entity != null)
                {
                    blllog.Add<dishesEntity>(entity, Entity, OldEntity);
                }
            }
            return dtBase;
        }

		/// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="lsid">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public DataTable UpdateStatus(string GUID, string UID, string ids, string Status)
        {
            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            int result = dal.UpdateStatus(ids, Status);
            //检测执行结果
			CheckResult(result);
            return dtBase;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>返回操作结果</returns>
        public DataTable Delete(string GUID, string UID, string lsid, operatelogEntity entity)
        {
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
			string Mescode = string.Empty;
            int result = dal.Delete(lsid, ref Mescode);
            //检测执行结果
            if (CheckDeleteResult(result,Mescode))
            {
                //写日志
                if (entity != null)
                {
                    blllog.Add(entity);
                }

            }
            return dtBase;
        }

        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="filter">指定条件</param>
        /// <returns>返回第一行</returns>
        public DataTable GetPagingSigInfo(string GUID, string UID, string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            return GetPagingListInfo(GUID, UID, 1, 1, filter, string.Empty, out recnums, out pagenums);
        }

		/// <summary>
        /// 获取单条数据实体对象
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public dishesEntity GetEntitySigInfo(string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            DataTable dt = GetPagingListInfo("", "0", 1, 1, filter, string.Empty, out recnums, out pagenums);
            if (dt != null && dt.Rows.Count > 0)
            {
                return SetEntityInfo(dt.Rows[0]);
            }
            return new dishesEntity();
        }

		/// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentpage"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="recnums"></param>
        /// <returns></returns>
        public DataTable GetPagingListInfo(string GUID, string UID, int pageSize, int currentpage, string filter, string order, out int recnums, out int pagenums)
        {
			if (!CheckLogin(GUID, UID))//非法登录
            {
                recnums = -1;
                pagenums = -1;
                return dtBase;
            }
            return new bllPaging().GetPagingInfo("dishes", "lsid", "*,cusername=dbo.fnGetUserName(cuser),uusername=dbo.fnGetUserName(uuser)", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }

		/// <summary>
        /// 单行数据转实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private dishesEntity SetEntityInfo(DataRow dr)
        {
            dishesEntity Entity = new dishesEntity();
			Entity.lsid = Helper.StringToLong(dr["lsid"].ToString());
			Entity.disid = Helper.StringToLong(dr["disid"].ToString());
			Entity.buscode = dr["buscode"].ToString();
			Entity.stocode = dr["stocode"].ToString();
			Entity.discode = dr["discode"].ToString();
			Entity.disname = dr["disname"].ToString();
			Entity.melcode = dr["melcode"].ToString();
			Entity.disothername = dr["disothername"].ToString();
			Entity.distypecode = dr["distypecode"].ToString();
			Entity.quickcode = dr["quickcode"].ToString();
			Entity.customcode = dr["customcode"].ToString();
			Entity.unit = dr["unit"].ToString();
			Entity.price = Helper.StringToDecimal(dr["price"].ToString());
			Entity.memberprice = Helper.StringToDecimal(dr["memberprice"].ToString());
			Entity.ismultiprice = dr["ismultiprice"].ToString();
			Entity.costprice = Helper.StringToDecimal(dr["costprice"].ToString());
			Entity.iscostbyingredient = dr["iscostbyingredient"].ToString();
			Entity.pushmoney = Helper.StringToDecimal(dr["pushmoney"].ToString());
			Entity.matclscode = dr["matclscode"].ToString();
			Entity.matcode = dr["matcode"].ToString();
			Entity.extcode = dr["extcode"].ToString();
			Entity.fincode = dr["fincode"].ToString();
			Entity.dcode = dr["dcode"].ToString();
			Entity.kitcode = dr["kitcode"].ToString();
			Entity.ecode = dr["ecode"].ToString();
			Entity.maketime = Helper.StringToInt(dr["maketime"].ToString());
			Entity.qrcode = dr["qrcode"].ToString();
			Entity.dispicture = dr["dispicture"].ToString();
			Entity.remark = dr["remark"].ToString();
			Entity.isentity = dr["isentity"].ToString();
			Entity.entitydefcount = Helper.StringToInt(dr["entitydefcount"].ToString());
			Entity.entityprice = Helper.StringToDecimal(dr["entityprice"].ToString());
			Entity.iscanmodifyprice = dr["iscanmodifyprice"].ToString();
			Entity.isneedweigh = dr["isneedweigh"].ToString();
			Entity.isneedmethod = dr["isneedmethod"].ToString();
			Entity.iscaninventory = dr["iscaninventory"].ToString();
			Entity.iscancustom = dr["iscancustom"].ToString();
			Entity.iscandeposit = dr["iscandeposit"].ToString();
			Entity.isallowmemberprice = dr["isallowmemberprice"].ToString();
			Entity.isattachcalculate = dr["isattachcalculate"].ToString();
			Entity.isclipcoupons = dr["isclipcoupons"].ToString();
			Entity.iscombo = dr["iscombo"].ToString();
			Entity.iscombooptional = dr["iscombooptional"].ToString();
			Entity.isnonoperating = dr["isnonoperating"].ToString();
			Entity.status = dr["status"].ToString();
			Entity.busSort = Helper.StringToInt(dr["busSort"].ToString());
			Entity.warcode = dr["warcode"].ToString();
			Entity.cuser = Helper.StringToLong(dr["cuser"].ToString());
			
			Entity.uuser = Helper.StringToLong(dr["uuser"].ToString());
			
			
            return Entity;
        }
    }
}