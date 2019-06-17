using System.Collections.Generic;
using System.Data;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
namespace XJWZCatering.BLL
{
	/// <summary>
    /// 餐收_桌台点餐1业务类
    /// </summary>
    public class bllchoorder : bllBase
    {
		DAL.dalchoorder dal = new DAL.dalchoorder();
        choorderEntity Entity = new choorderEntity();

		/// <summary>
        /// 检验表单数据
        /// </summary>
        /// <returns></returns>
        public string CheckPageInfo(string type, string lsid, string orderid, string buscode, string strcode, string shiftid, string tmcode, string personnum, string username, string userphone, string arrivetime, string opentime, string restime, string checkouttime, string gusetleavetime, string alltime, string allfoodtime, string conmoney, string status, string remark, string cuser, string uuser, out string spanids)
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
            CheckValue<choorderEntity>(EName, EValue, ref errorCode, ref ControlName, new choorderEntity());
            //特殊验证写在下面

            if (errorCode.Count > 0)
            {
                strRetuen = ErrMessage.GetMessageInfoByListCode(errorCode);
                spanids = ListTostring(ControlName);
            }
            else//组合对象数据
            {
                Entity = new choorderEntity();
				Entity.lsid = Helper.StringToLong(lsid);
				Entity.orderid = Helper.StringToLong(orderid);
				Entity.buscode = buscode;
				Entity.strcode = strcode;
				Entity.shiftid = Helper.StringToLong(shiftid);
				Entity.tmcode = tmcode;
				Entity.personnum = Helper.StringToInt(personnum);
				Entity.username = username;
				Entity.userphone = userphone;
				Entity.arrivetime = arrivetime;
				Entity.opentime = Helper.StringToDateTime(opentime);
				Entity.restime = Helper.StringToDateTime(restime);
				Entity.checkouttime = Helper.StringToDateTime(checkouttime);
				Entity.gusetleavetime = Helper.StringToDateTime(gusetleavetime);
				Entity.alltime = Helper.StringToInt(alltime);
				Entity.allfoodtime = Helper.StringToDateTime(allfoodtime);
				Entity.conmoney = Helper.StringToDecimal(conmoney);
				Entity.status = status;
				Entity.remark = remark;
				Entity.cuser = Helper.StringToLong(cuser);
				
				Entity.uuser = Helper.StringToLong(uuser);
				
				
            }
            return strRetuen;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public DataTable Add(string GUID, string UID, out  string lsid, string orderid, string buscode, string strcode, string shiftid, string tmcode, string personnum, string username, string userphone, string arrivetime, string opentime, string restime, string checkouttime, string gusetleavetime, string alltime, string allfoodtime, string conmoney, string status, string remark, string cuser, string uuser, operatelogEntity entity)
        {
			lsid = "0";
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }

            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("add",  lsid, orderid, buscode, strcode, shiftid, tmcode, personnum, username, userphone, arrivetime, opentime, restime, checkouttime, gusetleavetime, alltime, allfoodtime, conmoney, status, remark, cuser, uuser, out spanids);
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
        public DataTable Update(string GUID, string UID,  string lsid, string orderid, string buscode, string strcode, string shiftid, string tmcode, string personnum, string username, string userphone, string arrivetime, string opentime, string restime, string checkouttime, string gusetleavetime, string alltime, string allfoodtime, string conmoney, string status, string remark, string cuser, string uuser, operatelogEntity entity)
        {
			
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("update",  lsid, orderid, buscode, strcode, shiftid, tmcode, personnum, username, userphone, arrivetime, opentime, restime, checkouttime, gusetleavetime, alltime, allfoodtime, conmoney, status, remark, cuser, uuser, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
			//获取更新前的数据对象
            choorderEntity OldEntity = new choorderEntity();
            OldEntity = GetEntitySigInfo(" where lsid='" + lsid + "'");
			//更新数据
            int result = dal.Update(Entity);
            //检测执行结果
            if (CheckResult(result))
            {
                //写日志
                if (entity != null)
                {
                    blllog.Add<choorderEntity>(entity, Entity, OldEntity);
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
        public choorderEntity GetEntitySigInfo(string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            DataTable dt = GetPagingListInfo("", "0", 1, 1, filter, string.Empty, out recnums, out pagenums);
            if (dt != null && dt.Rows.Count > 0)
            {
                return SetEntityInfo(dt.Rows[0]);
            }
            return new choorderEntity();
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
            return new bllPaging().GetPagingInfo("choorder", "lsid", "*,cusername=dbo.fnGetUserName(cuser),uusername=dbo.fnGetUserName(uuser)", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }

		/// <summary>
        /// 单行数据转实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private choorderEntity SetEntityInfo(DataRow dr)
        {
            choorderEntity Entity = new choorderEntity();
			Entity.lsid = Helper.StringToLong(dr["lsid"].ToString());
			Entity.orderid = Helper.StringToLong(dr["orderid"].ToString());
			Entity.buscode = dr["buscode"].ToString();
			Entity.strcode = dr["strcode"].ToString();
			Entity.shiftid = Helper.StringToLong(dr["shiftid"].ToString());
			Entity.tmcode = dr["tmcode"].ToString();
			Entity.personnum = Helper.StringToInt(dr["personnum"].ToString());
			Entity.username = dr["username"].ToString();
			Entity.userphone = dr["userphone"].ToString();
			Entity.arrivetime = dr["arrivetime"].ToString();
			Entity.opentime = Helper.StringToDateTime(dr["opentime"].ToString());
			Entity.restime = Helper.StringToDateTime(dr["restime"].ToString());
			Entity.checkouttime = Helper.StringToDateTime(dr["checkouttime"].ToString());
			Entity.gusetleavetime = Helper.StringToDateTime(dr["gusetleavetime"].ToString());
			Entity.alltime = Helper.StringToInt(dr["alltime"].ToString());
			Entity.allfoodtime = Helper.StringToDateTime(dr["allfoodtime"].ToString());
			Entity.conmoney = Helper.StringToDecimal(dr["conmoney"].ToString());
			Entity.status = dr["status"].ToString();
			Entity.remark = dr["remark"].ToString();
			Entity.cuser = Helper.StringToLong(dr["cuser"].ToString());
			
			Entity.uuser = Helper.StringToLong(dr["uuser"].ToString());
			
			
            return Entity;
        }
    }
}