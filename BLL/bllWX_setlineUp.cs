using System.Collections.Generic;
using System.Data;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
namespace XJWZCatering.BLL
{
	/// <summary>
    /// 设置排队业务类
    /// </summary>
    public class bllWX_setlineUp : bllBase
    {
		DAL.dalWX_setlineUp dal = new DAL.dalWX_setlineUp();
        WX_setlineUpEntity Entity = new WX_setlineUpEntity();

		/// <summary>
        /// 检验表单数据
        /// </summary>
        /// <returns></returns>
        public string CheckPageInfo(string type, string lineid, string linecode, string stocode, string buscode, string ttcode, string maxperson, string minperosn, string Turncycle, string remark, string status, string cuser, string uuser, out string spanids)
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
            CheckValue<WX_setlineUpEntity>(EName, EValue, ref errorCode, ref ControlName, new WX_setlineUpEntity());
            //特殊验证写在下面

            if (errorCode.Count > 0)
            {
                strRetuen = ErrMessage.GetMessageInfoByListCode(errorCode);
                spanids = ListTostring(ControlName);
            }
            else//组合对象数据
            {
                Entity = new WX_setlineUpEntity();
				Entity.lineid = Helper.StringToLong(lineid);
				Entity.linecode = linecode;
				Entity.stocode = stocode;
				Entity.buscode = buscode;
				Entity.ttcode = ttcode;
				Entity.maxperson = Helper.StringToInt(maxperson);
				Entity.minperosn = Helper.StringToInt(minperosn);
				Entity.Turncycle = Helper.StringToInt(Turncycle);
				Entity.remark = remark;
				Entity.status = status;
				Entity.cuser = Helper.StringToLong(cuser);
				Entity.uuser = Helper.StringToLong(uuser);
				
				
				
            }
            return strRetuen;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public DataTable Add(string GUID, string UID, out  string lineid, string linecode, string stocode, string buscode, string ttcode, string maxperson, string minperosn, string Turncycle, string remark, string status, string cuser, string uuser, operatelogEntity entity)
        {
			lineid = "0";
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }

            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("add",  lineid, linecode, stocode, buscode, ttcode, maxperson, minperosn, Turncycle, remark, status, cuser, uuser, out spanids);
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
        public DataTable Update(string GUID, string UID,  string lineid, string linecode, string stocode, string buscode, string ttcode, string maxperson, string minperosn, string Turncycle, string remark, string status, string cuser, string uuser, operatelogEntity entity)
        {
			
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("update",  lineid, linecode, stocode, buscode, ttcode, maxperson, minperosn, Turncycle, remark, status, cuser, uuser, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
			//获取更新前的数据对象
            WX_setlineUpEntity OldEntity = new WX_setlineUpEntity();
            OldEntity = GetEntitySigInfo(" where lineid='" + lineid + "'");
			//更新数据
            int result = dal.Update(Entity);
            //检测执行结果
            if (CheckResult(result))
            {
                //写日志
                if (entity != null)
                {
                    blllog.Add<WX_setlineUpEntity>(entity, Entity, OldEntity);
                }
            }
            return dtBase;
        }

		/// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="lineid">标识</param>
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
        public DataTable Delete(string GUID, string UID, string lineid, operatelogEntity entity)
        {
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
			string Mescode = string.Empty;
            int result = dal.Delete(lineid, ref Mescode);
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
        public WX_setlineUpEntity GetEntitySigInfo(string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            DataTable dt = GetPagingListInfo("", "0", 1, 1, filter, string.Empty, out recnums, out pagenums);
            if (dt != null && dt.Rows.Count > 0)
            {
                return SetEntityInfo(dt.Rows[0]);
            }
            return new WX_setlineUpEntity();
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
            return new bllPaging().GetPagingInfo("WX_setlineUp", "lineid", "*,cusername=dbo.fnGetUserName(cuser),uusername=dbo.fnGetUserName(uuser)", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }

		/// <summary>
        /// 单行数据转实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private WX_setlineUpEntity SetEntityInfo(DataRow dr)
        {
            WX_setlineUpEntity Entity = new WX_setlineUpEntity();
			Entity.lineid = Helper.StringToLong(dr["lineid"].ToString());
			Entity.linecode = dr["linecode"].ToString();
			Entity.stocode = dr["stocode"].ToString();
			Entity.buscode = dr["buscode"].ToString();
			Entity.ttcode = dr["ttcode"].ToString();
			Entity.maxperson = Helper.StringToInt(dr["maxperson"].ToString());
			Entity.minperosn = Helper.StringToInt(dr["minperosn"].ToString());
			Entity.Turncycle = Helper.StringToInt(dr["Turncycle"].ToString());
			Entity.remark = dr["remark"].ToString();
			Entity.status = dr["status"].ToString();
			Entity.cuser = Helper.StringToLong(dr["cuser"].ToString());
			Entity.uuser = Helper.StringToLong(dr["uuser"].ToString());
			
			
			
            return Entity;
        }
    }
}