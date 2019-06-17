using System.Collections.Generic;
using System.Data;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
namespace XJWZCatering.BLL
{
	/// <summary>
    /// 餐收_点餐订单2业务类
    /// </summary>
    public class bllchoorderdetail : bllBase
    {
		DAL.dalchoorderdetail dal = new DAL.dalchoorderdetail();
        choorderdetailEntity Entity = new choorderdetailEntity();

		/// <summary>
        /// 检验表单数据
        /// </summary>
        /// <returns></returns>
        public string CheckPageInfo(string type, string id, string detailid, string buscode, string stocode, string orderid, string personcount, string opentime, string checkouttime, string alltime, string endtime, string shiftid, string tmcode, string tmname, string tablecodes, string combinenum, string allfoodtime, string pushfoodtime, string pushfoodstate, string closeaccount, string closeCodes, string calltime, string userid, string ordertime, string surchargeamount, string ocostprice, string conmoney, string status, string detailcode, string memcode, string desCode, string desctime, string printcount, string remark, string metname, string pushid, string pushname, string cuser, string ispresented, string presentedcode, string cardcode, string mobile, string coupons, string metcode, string ttcode, string customer, string amanagerid, string amanagername, string olossmoney, string detailtype, string predishemoney, string TerminalType, string depcode, string depname, out string spanids)
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
            CheckValue<choorderdetailEntity>(EName, EValue, ref errorCode, ref ControlName, new choorderdetailEntity());
            //特殊验证写在下面

            if (errorCode.Count > 0)
            {
                strRetuen = ErrMessage.GetMessageInfoByListCode(errorCode);
                spanids = ListTostring(ControlName);
            }
            else//组合对象数据
            {
                Entity = new choorderdetailEntity();
				Entity.id = Helper.StringToLong(id);
				Entity.detailid = Helper.StringToLong(detailid);
				Entity.buscode = buscode;
				Entity.stocode = stocode;
				Entity.orderid = Helper.StringToLong(orderid);
				Entity.personcount = Helper.StringToInt(personcount);
				Entity.opentime = Helper.StringToDateTime(opentime);
				Entity.checkouttime = Helper.StringToDateTime(checkouttime);
				Entity.alltime = Helper.StringToInt(alltime);
				Entity.endtime = Helper.StringToDateTime(endtime);
				Entity.shiftid = Helper.StringToLong(shiftid);
				Entity.tmcode = tmcode;
				Entity.tmname = tmname;
				Entity.tablecodes = tablecodes;
				Entity.combinenum = Helper.StringToInt(combinenum);
				Entity.allfoodtime = Helper.StringToDateTime(allfoodtime);
				Entity.pushfoodtime = Helper.StringToDateTime(pushfoodtime);
				Entity.pushfoodstate = pushfoodstate;
				Entity.closeaccount = closeaccount;
				Entity.closeCodes = closeCodes;
				Entity.calltime = Helper.StringToDateTime(calltime);
				Entity.userid = Helper.StringToLong(userid);
				Entity.ordertime = Helper.StringToDateTime(ordertime);
				Entity.surchargeamount = Helper.StringToDecimal(surchargeamount);
				Entity.ocostprice = Helper.StringToDecimal(ocostprice);
				Entity.conmoney = Helper.StringToDecimal(conmoney);
				Entity.status = status;
				Entity.detailcode = detailcode;
				Entity.memcode = memcode;
				Entity.desCode = desCode;
				Entity.desctime = Helper.StringToDateTime(desctime);
				Entity.printcount = Helper.StringToInt(printcount);
				Entity.remark = remark;
				Entity.metname = metname;
				Entity.pushid = pushid;
				Entity.pushname = pushname;
				Entity.cuser = Helper.StringToLong(cuser);
				
				Entity.ispresented = ispresented;
				Entity.presentedcode = presentedcode;
				Entity.cardcode = cardcode;
				Entity.mobile = mobile;
				Entity.coupons = coupons;
				Entity.metcode = metcode;
				Entity.ttcode = ttcode;
				Entity.customer = customer;
				Entity.amanagerid = amanagerid;
				Entity.amanagername = amanagername;
				Entity.olossmoney = Helper.StringToDecimal(olossmoney);
				Entity.detailtype = detailtype;
				Entity.predishemoney = Helper.StringToDecimal(predishemoney);
				Entity.TerminalType = TerminalType;
				Entity.depcode = depcode;
				Entity.depname = depname;
            }
            return strRetuen;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public DataTable Add(string GUID, string UID, out  string id, string detailid, string buscode, string stocode, string orderid, string personcount, string opentime, string checkouttime, string alltime, string endtime, string shiftid, string tmcode, string tmname, string tablecodes, string combinenum, string allfoodtime, string pushfoodtime, string pushfoodstate, string closeaccount, string closeCodes, string calltime, string userid, string ordertime, string surchargeamount, string ocostprice, string conmoney, string status, string detailcode, string memcode, string desCode, string desctime, string printcount, string remark, string metname, string pushid, string pushname, string cuser, string ispresented, string presentedcode, string cardcode, string mobile, string coupons, string metcode, string ttcode, string customer, string amanagerid, string amanagername, string olossmoney, string detailtype, string predishemoney, string TerminalType, string depcode, string depname, operatelogEntity entity)
        {
			id = "0";
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }

            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("add",  id, detailid, buscode, stocode, orderid, personcount, opentime, checkouttime, alltime, endtime, shiftid, tmcode, tmname, tablecodes, combinenum, allfoodtime, pushfoodtime, pushfoodstate, closeaccount, closeCodes, calltime, userid, ordertime, surchargeamount, ocostprice, conmoney, status, detailcode, memcode, desCode, desctime, printcount, remark, metname, pushid, pushname, cuser, ispresented, presentedcode, cardcode, mobile, coupons, metcode, ttcode, customer, amanagerid, amanagername, olossmoney, detailtype, predishemoney, TerminalType, depcode, depname, out spanids);
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
        public DataTable Update(string GUID, string UID,  string id, string detailid, string buscode, string stocode, string orderid, string personcount, string opentime, string checkouttime, string alltime, string endtime, string shiftid, string tmcode, string tmname, string tablecodes, string combinenum, string allfoodtime, string pushfoodtime, string pushfoodstate, string closeaccount, string closeCodes, string calltime, string userid, string ordertime, string surchargeamount, string ocostprice, string conmoney, string status, string detailcode, string memcode, string desCode, string desctime, string printcount, string remark, string metname, string pushid, string pushname, string cuser, string ispresented, string presentedcode, string cardcode, string mobile, string coupons, string metcode, string ttcode, string customer, string amanagerid, string amanagername, string olossmoney, string detailtype, string predishemoney, string TerminalType, string depcode, string depname, operatelogEntity entity)
        {
			
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("update",  id, detailid, buscode, stocode, orderid, personcount, opentime, checkouttime, alltime, endtime, shiftid, tmcode, tmname, tablecodes, combinenum, allfoodtime, pushfoodtime, pushfoodstate, closeaccount, closeCodes, calltime, userid, ordertime, surchargeamount, ocostprice, conmoney, status, detailcode, memcode, desCode, desctime, printcount, remark, metname, pushid, pushname, cuser, ispresented, presentedcode, cardcode, mobile, coupons, metcode, ttcode, customer, amanagerid, amanagername, olossmoney, detailtype, predishemoney, TerminalType, depcode, depname, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
			//获取更新前的数据对象
            choorderdetailEntity OldEntity = new choorderdetailEntity();
            OldEntity = GetEntitySigInfo(" where id='" + id + "'");
			//更新数据
            int result = dal.Update(Entity);
            //检测执行结果
            if (CheckResult(result))
            {
                //写日志
                if (entity != null)
                {
                    blllog.Add<choorderdetailEntity>(entity, Entity, OldEntity);
                }
            }
            return dtBase;
        }

		/// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id">标识</param>
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
        public DataTable Delete(string GUID, string UID, string id, operatelogEntity entity)
        {
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
			string Mescode = string.Empty;
            int result = dal.Delete(id, ref Mescode);
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
        public choorderdetailEntity GetEntitySigInfo(string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            DataTable dt = GetPagingListInfo("", "0", 1, 1, filter, string.Empty, out recnums, out pagenums);
            if (dt != null && dt.Rows.Count > 0)
            {
                return SetEntityInfo(dt.Rows[0]);
            }
            return new choorderdetailEntity();
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
            return new bllPaging().GetPagingInfo("choorderdetail", "id", "*,cusername=dbo.fnGetUserName(cuser)", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }

		/// <summary>
        /// 单行数据转实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private choorderdetailEntity SetEntityInfo(DataRow dr)
        {
            choorderdetailEntity Entity = new choorderdetailEntity();
			Entity.id = Helper.StringToLong(dr["id"].ToString());
			Entity.detailid = Helper.StringToLong(dr["detailid"].ToString());
			Entity.buscode = dr["buscode"].ToString();
			Entity.stocode = dr["stocode"].ToString();
			Entity.orderid = Helper.StringToLong(dr["orderid"].ToString());
			Entity.personcount = Helper.StringToInt(dr["personcount"].ToString());
			Entity.opentime = Helper.StringToDateTime(dr["opentime"].ToString());
			Entity.checkouttime = Helper.StringToDateTime(dr["checkouttime"].ToString());
			Entity.alltime = Helper.StringToInt(dr["alltime"].ToString());
			Entity.endtime = Helper.StringToDateTime(dr["endtime"].ToString());
			Entity.shiftid = Helper.StringToLong(dr["shiftid"].ToString());
			Entity.tmcode = dr["tmcode"].ToString();
			Entity.tmname = dr["tmname"].ToString();
			Entity.tablecodes = dr["tablecodes"].ToString();
			Entity.combinenum = Helper.StringToInt(dr["combinenum"].ToString());
			Entity.allfoodtime = Helper.StringToDateTime(dr["allfoodtime"].ToString());
			Entity.pushfoodtime = Helper.StringToDateTime(dr["pushfoodtime"].ToString());
			Entity.pushfoodstate = dr["pushfoodstate"].ToString();
			Entity.closeaccount = dr["closeaccount"].ToString();
			Entity.closeCodes = dr["closeCodes"].ToString();
			Entity.calltime = Helper.StringToDateTime(dr["calltime"].ToString());
			Entity.userid = Helper.StringToLong(dr["userid"].ToString());
			Entity.ordertime = Helper.StringToDateTime(dr["ordertime"].ToString());
			Entity.surchargeamount = Helper.StringToDecimal(dr["surchargeamount"].ToString());
			Entity.ocostprice = Helper.StringToDecimal(dr["ocostprice"].ToString());
			Entity.conmoney = Helper.StringToDecimal(dr["conmoney"].ToString());
			Entity.status = dr["status"].ToString();
			Entity.detailcode = dr["detailcode"].ToString();
			Entity.memcode = dr["memcode"].ToString();
			Entity.desCode = dr["desCode"].ToString();
			Entity.desctime = Helper.StringToDateTime(dr["desctime"].ToString());
			Entity.printcount = Helper.StringToInt(dr["printcount"].ToString());
			Entity.remark = dr["remark"].ToString();
			Entity.metname = dr["metname"].ToString();
			Entity.pushid = dr["pushid"].ToString();
			Entity.pushname = dr["pushname"].ToString();
			Entity.cuser = Helper.StringToLong(dr["cuser"].ToString());
			
			Entity.ispresented = dr["ispresented"].ToString();
			Entity.presentedcode = dr["presentedcode"].ToString();
			Entity.cardcode = dr["cardcode"].ToString();
			Entity.mobile = dr["mobile"].ToString();
			Entity.coupons = dr["coupons"].ToString();
			Entity.metcode = dr["metcode"].ToString();
			Entity.ttcode = dr["ttcode"].ToString();
			Entity.customer = dr["customer"].ToString();
			Entity.amanagerid = dr["amanagerid"].ToString();
			Entity.amanagername = dr["amanagername"].ToString();
			Entity.olossmoney = Helper.StringToDecimal(dr["olossmoney"].ToString());
			Entity.detailtype = dr["detailtype"].ToString();
			Entity.predishemoney = Helper.StringToDecimal(dr["predishemoney"].ToString());
			Entity.TerminalType = dr["TerminalType"].ToString();
			Entity.depcode = dr["depcode"].ToString();
			Entity.depname = dr["depname"].ToString();
            return Entity;
        }
    }
}