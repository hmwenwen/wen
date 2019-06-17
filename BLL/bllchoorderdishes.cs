using System.Collections.Generic;
using System.Data;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
namespace XJWZCatering.BLL
{
	/// <summary>
    /// 餐收_点餐菜品2-1业务类
    /// </summary>
    public class bllchoorderdishes : bllBase
    {
		DAL.dalchoorderdishes dal = new DAL.dalchoorderdishes();
        choorderdishesEntity Entity = new choorderdishesEntity();

		/// <summary>
        /// 检验表单数据
        /// </summary>
        /// <returns></returns>
        public string CheckPageInfo(string type, string lsid, string orderdishesid, string buscode, string stocode, string orderid, string detailid, string distypecode, string dtypecode, string melcode, string discode, string disname, string disothername, string disnum, string disnumconst, string addnum, string isneedmethod, string distacode, string distaname, string ordertime, string addtime, string calltime, string pushfoodtime, string pushfoodstate, string isentity, string entitydefcount, string entityprice, string singlenum, string singleAllmoney, string totaladdmoney, string totaladdmoneydiscount, string allmoney, string allmoneydiscount, string memberallmoney, string resultallmoney, string packageaddmoney, string ispackage, string iscanout, string isout, string refundNum, string refundaddnum, string oneprice, string memberprice, string costprice, string methodmoney, string methodmoneydiscount, string attachmoney, string pushmoney, string ismember, string ispre, string pretype, string dispcode, string discountratemax, string discountrate, string premoney, string precheck, string checktime, string ismustconsume, string mustconsumenum, string iscaninventory, string isallowmemberprice, string isattachcalculate, string isclipcoupons, string isnonoperating, string iscombooptional, string isneedweigh, string iscanmodifyprice, string matcode, string cguid, string pcguid, string porderdishesid, string comdiscode, string comgcode, string composetype, string allowkinds, string allowcount, string allowamount, string usedisdefaultamount, string usedismaxamount, string unit, string extcode, string fincode, string dcode, string kitcode, string ecode, string warcode, string totmcode, string totmname, string todetailid, string status, string makestatus, string operaretype, string isdiscount, string priceispre, string ispresented, string pecode, string pename, string prereason, string prereasontype, string remark, string orderremark, string gguid, string nopreremark, string storeupdated, string cuser, string comprice, string cusername, string tottcode, string metname, string metcode, string detailcode, string tmopentime, string chomac, string orderno, string subtype, out string spanids)
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
            CheckValue<choorderdishesEntity>(EName, EValue, ref errorCode, ref ControlName, new choorderdishesEntity());
            //特殊验证写在下面

            if (errorCode.Count > 0)
            {
                strRetuen = ErrMessage.GetMessageInfoByListCode(errorCode);
                spanids = ListTostring(ControlName);
            }
            else//组合对象数据
            {
                Entity = new choorderdishesEntity();
				Entity.lsid = Helper.StringToLong(lsid);
				Entity.orderdishesid = Helper.StringToLong(orderdishesid);
				Entity.buscode = buscode;
				Entity.stocode = stocode;
				Entity.orderid = Helper.StringToLong(orderid);
				Entity.detailid = Helper.StringToLong(detailid);
				Entity.distypecode = distypecode;
				Entity.dtypecode = dtypecode;
				Entity.melcode = melcode;
				Entity.discode = discode;
				Entity.disname = disname;
				Entity.disothername = disothername;
				Entity.disnum = Helper.StringToDecimal(disnum);
				Entity.disnumconst = Helper.StringToDecimal(disnumconst);
				Entity.addnum = Helper.StringToDecimal(addnum);
				Entity.isneedmethod = isneedmethod;
				Entity.distacode = distacode;
				Entity.distaname = distaname;
				Entity.ordertime = Helper.StringToDateTime(ordertime);
				Entity.addtime = Helper.StringToDateTime(addtime);
				Entity.calltime = Helper.StringToDateTime(calltime);
				Entity.pushfoodtime = Helper.StringToDateTime(pushfoodtime);
				Entity.pushfoodstate = Helper.StringToInt(pushfoodstate);
				Entity.isentity = isentity;
				Entity.entitydefcount = Helper.StringToInt(entitydefcount);
				Entity.entityprice = Helper.StringToDecimal(entityprice);
				Entity.singlenum = Helper.StringToInt(singlenum);
				Entity.singleAllmoney = Helper.StringToDecimal(singleAllmoney);
				Entity.totaladdmoney = Helper.StringToDecimal(totaladdmoney);
				Entity.totaladdmoneydiscount = Helper.StringToDecimal(totaladdmoneydiscount);
				Entity.allmoney = Helper.StringToDecimal(allmoney);
				Entity.allmoneydiscount = Helper.StringToDecimal(allmoneydiscount);
				Entity.memberallmoney = Helper.StringToDecimal(memberallmoney);
				Entity.resultallmoney = Helper.StringToDecimal(resultallmoney);
				Entity.packageaddmoney = Helper.StringToDecimal(packageaddmoney);
				Entity.ispackage = ispackage;
				Entity.iscanout = iscanout;
				Entity.isout = isout;
				Entity.refundNum = Helper.StringToDecimal(refundNum);
				Entity.refundaddnum = Helper.StringToDecimal(refundaddnum);
				Entity.oneprice = Helper.StringToDecimal(oneprice);
				Entity.memberprice = Helper.StringToDecimal(memberprice);
				Entity.costprice = Helper.StringToDecimal(costprice);
				Entity.methodmoney = Helper.StringToDecimal(methodmoney);
				Entity.methodmoneydiscount = Helper.StringToDecimal(methodmoneydiscount);
				Entity.attachmoney = Helper.StringToDecimal(attachmoney);
				Entity.pushmoney = Helper.StringToDecimal(pushmoney);
				Entity.ismember = ismember;
				Entity.ispre = ispre;
				Entity.pretype = pretype;
				Entity.dispcode = dispcode;
				Entity.discountratemax = Helper.StringToDecimal(discountratemax);
				Entity.discountrate = Helper.StringToDecimal(discountrate);
				Entity.premoney = Helper.StringToDecimal(premoney);
				Entity.precheck = precheck;
				Entity.checktime = Helper.StringToDateTime(checktime);
				Entity.ismustconsume = ismustconsume;
				Entity.mustconsumenum = Helper.StringToDecimal(mustconsumenum);
				Entity.iscaninventory = iscaninventory;
				Entity.isallowmemberprice = isallowmemberprice;
				Entity.isattachcalculate = isattachcalculate;
				Entity.isclipcoupons = isclipcoupons;
				Entity.isnonoperating = isnonoperating;
				Entity.iscombooptional = iscombooptional;
				Entity.isneedweigh = isneedweigh;
				Entity.iscanmodifyprice = iscanmodifyprice;
				Entity.matcode = matcode;
				Entity.cguid = cguid;
				Entity.pcguid = pcguid;
				Entity.porderdishesid = Helper.StringToLong(porderdishesid);
				Entity.comdiscode = comdiscode;
				Entity.comgcode = comgcode;
				Entity.composetype = composetype;
				Entity.allowkinds = Helper.StringToDecimal(allowkinds);
				Entity.allowcount = Helper.StringToDecimal(allowcount);
				Entity.allowamount = Helper.StringToDecimal(allowamount);
				Entity.usedisdefaultamount = Helper.StringToDecimal(usedisdefaultamount);
				Entity.usedismaxamount = Helper.StringToDecimal(usedismaxamount);
				Entity.unit = unit;
				Entity.extcode = extcode;
				Entity.fincode = fincode;
				Entity.dcode = dcode;
				Entity.kitcode = kitcode;
				Entity.ecode = ecode;
				Entity.warcode = warcode;
				Entity.totmcode = totmcode;
				Entity.totmname = totmname;
				Entity.todetailid = Helper.StringToLong(todetailid);
				Entity.status = status;
				Entity.makestatus = makestatus;
				Entity.operaretype = operaretype;
				Entity.isdiscount = isdiscount;
				Entity.priceispre = priceispre;
				Entity.ispresented = ispresented;
				Entity.pecode = pecode;
				Entity.pename = pename;
				Entity.prereason = prereason;
				Entity.prereasontype = prereasontype;
				Entity.remark = remark;
				Entity.orderremark = orderremark;
				Entity.gguid = gguid;
				Entity.nopreremark = nopreremark;
				Entity.storeupdated = storeupdated;
				Entity.cuser = Helper.StringToLong(cuser);
				
				Entity.comprice = Helper.StringToDecimal(comprice);
				Entity.cusername = cusername;
				Entity.tottcode = tottcode;
				Entity.metname = metname;
				Entity.metcode = metcode;
				Entity.detailcode = detailcode;
				Entity.tmopentime = Helper.StringToDateTime(tmopentime);
				Entity.chomac = chomac;
				Entity.orderno = orderno;
				Entity.subtype = subtype;
            }
            return strRetuen;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public DataTable Add(string GUID, string UID, out  string lsid, string orderdishesid, string buscode, string stocode, string orderid, string detailid, string distypecode, string dtypecode, string melcode, string discode, string disname, string disothername, string disnum, string disnumconst, string addnum, string isneedmethod, string distacode, string distaname, string ordertime, string addtime, string calltime, string pushfoodtime, string pushfoodstate, string isentity, string entitydefcount, string entityprice, string singlenum, string singleAllmoney, string totaladdmoney, string totaladdmoneydiscount, string allmoney, string allmoneydiscount, string memberallmoney, string resultallmoney, string packageaddmoney, string ispackage, string iscanout, string isout, string refundNum, string refundaddnum, string oneprice, string memberprice, string costprice, string methodmoney, string methodmoneydiscount, string attachmoney, string pushmoney, string ismember, string ispre, string pretype, string dispcode, string discountratemax, string discountrate, string premoney, string precheck, string checktime, string ismustconsume, string mustconsumenum, string iscaninventory, string isallowmemberprice, string isattachcalculate, string isclipcoupons, string isnonoperating, string iscombooptional, string isneedweigh, string iscanmodifyprice, string matcode, string cguid, string pcguid, string porderdishesid, string comdiscode, string comgcode, string composetype, string allowkinds, string allowcount, string allowamount, string usedisdefaultamount, string usedismaxamount, string unit, string extcode, string fincode, string dcode, string kitcode, string ecode, string warcode, string totmcode, string totmname, string todetailid, string status, string makestatus, string operaretype, string isdiscount, string priceispre, string ispresented, string pecode, string pename, string prereason, string prereasontype, string remark, string orderremark, string gguid, string nopreremark, string storeupdated, string cuser, string comprice, string cusername, string tottcode, string metname, string metcode, string detailcode, string tmopentime, string chomac, string orderno, string subtype, operatelogEntity entity)
        {
			lsid = "0";
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }

            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("add",  lsid, orderdishesid, buscode, stocode, orderid, detailid, distypecode, dtypecode, melcode, discode, disname, disothername, disnum, disnumconst, addnum, isneedmethod, distacode, distaname, ordertime, addtime, calltime, pushfoodtime, pushfoodstate, isentity, entitydefcount, entityprice, singlenum, singleAllmoney, totaladdmoney, totaladdmoneydiscount, allmoney, allmoneydiscount, memberallmoney, resultallmoney, packageaddmoney, ispackage, iscanout, isout, refundNum, refundaddnum, oneprice, memberprice, costprice, methodmoney, methodmoneydiscount, attachmoney, pushmoney, ismember, ispre, pretype, dispcode, discountratemax, discountrate, premoney, precheck, checktime, ismustconsume, mustconsumenum, iscaninventory, isallowmemberprice, isattachcalculate, isclipcoupons, isnonoperating, iscombooptional, isneedweigh, iscanmodifyprice, matcode, cguid, pcguid, porderdishesid, comdiscode, comgcode, composetype, allowkinds, allowcount, allowamount, usedisdefaultamount, usedismaxamount, unit, extcode, fincode, dcode, kitcode, ecode, warcode, totmcode, totmname, todetailid, status, makestatus, operaretype, isdiscount, priceispre, ispresented, pecode, pename, prereason, prereasontype, remark, orderremark, gguid, nopreremark, storeupdated, cuser, comprice, cusername, tottcode, metname, metcode, detailcode, tmopentime, chomac, orderno, subtype, out spanids);
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
        public DataTable Update(string GUID, string UID,  string lsid, string orderdishesid, string buscode, string stocode, string orderid, string detailid, string distypecode, string dtypecode, string melcode, string discode, string disname, string disothername, string disnum, string disnumconst, string addnum, string isneedmethod, string distacode, string distaname, string ordertime, string addtime, string calltime, string pushfoodtime, string pushfoodstate, string isentity, string entitydefcount, string entityprice, string singlenum, string singleAllmoney, string totaladdmoney, string totaladdmoneydiscount, string allmoney, string allmoneydiscount, string memberallmoney, string resultallmoney, string packageaddmoney, string ispackage, string iscanout, string isout, string refundNum, string refundaddnum, string oneprice, string memberprice, string costprice, string methodmoney, string methodmoneydiscount, string attachmoney, string pushmoney, string ismember, string ispre, string pretype, string dispcode, string discountratemax, string discountrate, string premoney, string precheck, string checktime, string ismustconsume, string mustconsumenum, string iscaninventory, string isallowmemberprice, string isattachcalculate, string isclipcoupons, string isnonoperating, string iscombooptional, string isneedweigh, string iscanmodifyprice, string matcode, string cguid, string pcguid, string porderdishesid, string comdiscode, string comgcode, string composetype, string allowkinds, string allowcount, string allowamount, string usedisdefaultamount, string usedismaxamount, string unit, string extcode, string fincode, string dcode, string kitcode, string ecode, string warcode, string totmcode, string totmname, string todetailid, string status, string makestatus, string operaretype, string isdiscount, string priceispre, string ispresented, string pecode, string pename, string prereason, string prereasontype, string remark, string orderremark, string gguid, string nopreremark, string storeupdated, string cuser, string comprice, string cusername, string tottcode, string metname, string metcode, string detailcode, string tmopentime, string chomac, string orderno, string subtype, operatelogEntity entity)
        {
			
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("update",  lsid, orderdishesid, buscode, stocode, orderid, detailid, distypecode, dtypecode, melcode, discode, disname, disothername, disnum, disnumconst, addnum, isneedmethod, distacode, distaname, ordertime, addtime, calltime, pushfoodtime, pushfoodstate, isentity, entitydefcount, entityprice, singlenum, singleAllmoney, totaladdmoney, totaladdmoneydiscount, allmoney, allmoneydiscount, memberallmoney, resultallmoney, packageaddmoney, ispackage, iscanout, isout, refundNum, refundaddnum, oneprice, memberprice, costprice, methodmoney, methodmoneydiscount, attachmoney, pushmoney, ismember, ispre, pretype, dispcode, discountratemax, discountrate, premoney, precheck, checktime, ismustconsume, mustconsumenum, iscaninventory, isallowmemberprice, isattachcalculate, isclipcoupons, isnonoperating, iscombooptional, isneedweigh, iscanmodifyprice, matcode, cguid, pcguid, porderdishesid, comdiscode, comgcode, composetype, allowkinds, allowcount, allowamount, usedisdefaultamount, usedismaxamount, unit, extcode, fincode, dcode, kitcode, ecode, warcode, totmcode, totmname, todetailid, status, makestatus, operaretype, isdiscount, priceispre, ispresented, pecode, pename, prereason, prereasontype, remark, orderremark, gguid, nopreremark, storeupdated, cuser, comprice, cusername, tottcode, metname, metcode, detailcode, tmopentime, chomac, orderno, subtype, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
			//获取更新前的数据对象
            choorderdishesEntity OldEntity = new choorderdishesEntity();
            OldEntity = GetEntitySigInfo(" where lsid='" + lsid + "'");
			//更新数据
            int result = dal.Update(Entity);
            //检测执行结果
            if (CheckResult(result))
            {
                //写日志
                if (entity != null)
                {
                    blllog.Add<choorderdishesEntity>(entity, Entity, OldEntity);
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
        public choorderdishesEntity GetEntitySigInfo(string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            DataTable dt = GetPagingListInfo("", "0", 1, 1, filter, string.Empty, out recnums, out pagenums);
            if (dt != null && dt.Rows.Count > 0)
            {
                return SetEntityInfo(dt.Rows[0]);
            }
            return new choorderdishesEntity();
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
            return new bllPaging().GetPagingInfo("choorderdishes", "lsid", "*,cusername=dbo.fnGetUserName(cuser)", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }

		/// <summary>
        /// 单行数据转实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private choorderdishesEntity SetEntityInfo(DataRow dr)
        {
            choorderdishesEntity Entity = new choorderdishesEntity();
			Entity.lsid = Helper.StringToLong(dr["lsid"].ToString());
			Entity.orderdishesid = Helper.StringToLong(dr["orderdishesid"].ToString());
			Entity.buscode = dr["buscode"].ToString();
			Entity.stocode = dr["stocode"].ToString();
			Entity.orderid = Helper.StringToLong(dr["orderid"].ToString());
			Entity.detailid = Helper.StringToLong(dr["detailid"].ToString());
			Entity.distypecode = dr["distypecode"].ToString();
			Entity.dtypecode = dr["dtypecode"].ToString();
			Entity.melcode = dr["melcode"].ToString();
			Entity.discode = dr["discode"].ToString();
			Entity.disname = dr["disname"].ToString();
			Entity.disothername = dr["disothername"].ToString();
			Entity.disnum = Helper.StringToDecimal(dr["disnum"].ToString());
			Entity.disnumconst = Helper.StringToDecimal(dr["disnumconst"].ToString());
			Entity.addnum = Helper.StringToDecimal(dr["addnum"].ToString());
			Entity.isneedmethod = dr["isneedmethod"].ToString();
			Entity.distacode = dr["distacode"].ToString();
			Entity.distaname = dr["distaname"].ToString();
			Entity.ordertime = Helper.StringToDateTime(dr["ordertime"].ToString());
			Entity.addtime = Helper.StringToDateTime(dr["addtime"].ToString());
			Entity.calltime = Helper.StringToDateTime(dr["calltime"].ToString());
			Entity.pushfoodtime = Helper.StringToDateTime(dr["pushfoodtime"].ToString());
			Entity.pushfoodstate = Helper.StringToInt(dr["pushfoodstate"].ToString());
			Entity.isentity = dr["isentity"].ToString();
			Entity.entitydefcount = Helper.StringToInt(dr["entitydefcount"].ToString());
			Entity.entityprice = Helper.StringToDecimal(dr["entityprice"].ToString());
			Entity.singlenum = Helper.StringToInt(dr["singlenum"].ToString());
			Entity.singleAllmoney = Helper.StringToDecimal(dr["singleAllmoney"].ToString());
			Entity.totaladdmoney = Helper.StringToDecimal(dr["totaladdmoney"].ToString());
			Entity.totaladdmoneydiscount = Helper.StringToDecimal(dr["totaladdmoneydiscount"].ToString());
			Entity.allmoney = Helper.StringToDecimal(dr["allmoney"].ToString());
			Entity.allmoneydiscount = Helper.StringToDecimal(dr["allmoneydiscount"].ToString());
			Entity.memberallmoney = Helper.StringToDecimal(dr["memberallmoney"].ToString());
			Entity.resultallmoney = Helper.StringToDecimal(dr["resultallmoney"].ToString());
			Entity.packageaddmoney = Helper.StringToDecimal(dr["packageaddmoney"].ToString());
			Entity.ispackage = dr["ispackage"].ToString();
			Entity.iscanout = dr["iscanout"].ToString();
			Entity.isout = dr["isout"].ToString();
			Entity.refundNum = Helper.StringToDecimal(dr["refundNum"].ToString());
			Entity.refundaddnum = Helper.StringToDecimal(dr["refundaddnum"].ToString());
			Entity.oneprice = Helper.StringToDecimal(dr["oneprice"].ToString());
			Entity.memberprice = Helper.StringToDecimal(dr["memberprice"].ToString());
			Entity.costprice = Helper.StringToDecimal(dr["costprice"].ToString());
			Entity.methodmoney = Helper.StringToDecimal(dr["methodmoney"].ToString());
			Entity.methodmoneydiscount = Helper.StringToDecimal(dr["methodmoneydiscount"].ToString());
			Entity.attachmoney = Helper.StringToDecimal(dr["attachmoney"].ToString());
			Entity.pushmoney = Helper.StringToDecimal(dr["pushmoney"].ToString());
			Entity.ismember = dr["ismember"].ToString();
			Entity.ispre = dr["ispre"].ToString();
			Entity.pretype = dr["pretype"].ToString();
			Entity.dispcode = dr["dispcode"].ToString();
			Entity.discountratemax = Helper.StringToDecimal(dr["discountratemax"].ToString());
			Entity.discountrate = Helper.StringToDecimal(dr["discountrate"].ToString());
			Entity.premoney = Helper.StringToDecimal(dr["premoney"].ToString());
			Entity.precheck = dr["precheck"].ToString();
			Entity.checktime = Helper.StringToDateTime(dr["checktime"].ToString());
			Entity.ismustconsume = dr["ismustconsume"].ToString();
			Entity.mustconsumenum = Helper.StringToDecimal(dr["mustconsumenum"].ToString());
			Entity.iscaninventory = dr["iscaninventory"].ToString();
			Entity.isallowmemberprice = dr["isallowmemberprice"].ToString();
			Entity.isattachcalculate = dr["isattachcalculate"].ToString();
			Entity.isclipcoupons = dr["isclipcoupons"].ToString();
			Entity.isnonoperating = dr["isnonoperating"].ToString();
			Entity.iscombooptional = dr["iscombooptional"].ToString();
			Entity.isneedweigh = dr["isneedweigh"].ToString();
			Entity.iscanmodifyprice = dr["iscanmodifyprice"].ToString();
			Entity.matcode = dr["matcode"].ToString();
			Entity.cguid = dr["cguid"].ToString();
			Entity.pcguid = dr["pcguid"].ToString();
			Entity.porderdishesid = Helper.StringToLong(dr["porderdishesid"].ToString());
			Entity.comdiscode = dr["comdiscode"].ToString();
			Entity.comgcode = dr["comgcode"].ToString();
			Entity.composetype = dr["composetype"].ToString();
			Entity.allowkinds = Helper.StringToDecimal(dr["allowkinds"].ToString());
			Entity.allowcount = Helper.StringToDecimal(dr["allowcount"].ToString());
			Entity.allowamount = Helper.StringToDecimal(dr["allowamount"].ToString());
			Entity.usedisdefaultamount = Helper.StringToDecimal(dr["usedisdefaultamount"].ToString());
			Entity.usedismaxamount = Helper.StringToDecimal(dr["usedismaxamount"].ToString());
			Entity.unit = dr["unit"].ToString();
			Entity.extcode = dr["extcode"].ToString();
			Entity.fincode = dr["fincode"].ToString();
			Entity.dcode = dr["dcode"].ToString();
			Entity.kitcode = dr["kitcode"].ToString();
			Entity.ecode = dr["ecode"].ToString();
			Entity.warcode = dr["warcode"].ToString();
			Entity.totmcode = dr["totmcode"].ToString();
			Entity.totmname = dr["totmname"].ToString();
			Entity.todetailid = Helper.StringToLong(dr["todetailid"].ToString());
			Entity.status = dr["status"].ToString();
			Entity.makestatus = dr["makestatus"].ToString();
			Entity.operaretype = dr["operaretype"].ToString();
			Entity.isdiscount = dr["isdiscount"].ToString();
			Entity.priceispre = dr["priceispre"].ToString();
			Entity.ispresented = dr["ispresented"].ToString();
			Entity.pecode = dr["pecode"].ToString();
			Entity.pename = dr["pename"].ToString();
			Entity.prereason = dr["prereason"].ToString();
			Entity.prereasontype = dr["prereasontype"].ToString();
			Entity.remark = dr["remark"].ToString();
			Entity.orderremark = dr["orderremark"].ToString();
			Entity.gguid = dr["gguid"].ToString();
			Entity.nopreremark = dr["nopreremark"].ToString();
			Entity.storeupdated = dr["storeupdated"].ToString();
			Entity.cuser = Helper.StringToLong(dr["cuser"].ToString());
			
			Entity.comprice = Helper.StringToDecimal(dr["comprice"].ToString());
			Entity.cusername = dr["cusername"].ToString();
			Entity.tottcode = dr["tottcode"].ToString();
			Entity.metname = dr["metname"].ToString();
			Entity.metcode = dr["metcode"].ToString();
			Entity.detailcode = dr["detailcode"].ToString();
			Entity.tmopentime = Helper.StringToDateTime(dr["tmopentime"].ToString());
			Entity.chomac = dr["chomac"].ToString();
			Entity.orderno = dr["orderno"].ToString();
			Entity.subtype = dr["subtype"].ToString();
            return Entity;
        }
    }
}