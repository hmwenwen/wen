using System.Collections.Generic;
using System.Data;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
namespace XJWZCatering.BLL
{
	/// <summary>
    /// 业务类
    /// </summary>
    public class bllDisheType : bllBase
    {
		DAL.dalDisheType dal = new DAL.dalDisheType();
        DisheTypeEntity Entity = new DisheTypeEntity();

		/// <summary>
        /// 检验表单数据
        /// </summary>
        /// <returns></returns>
        public string CheckPageInfo(string type, string lsid, string distypeid, string buscode, string stocode, string pdistypecode, string distypecode, string dispath, string distypename, string metcode, string fincode, string maxdiscount, string busSort, string status, string cuser, string uuser, out string spanids)
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
            CheckValue<DisheTypeEntity>(EName, EValue, ref errorCode, ref ControlName, new DisheTypeEntity());
            //特殊验证写在下面

            if (errorCode.Count > 0)
            {
                strRetuen = ErrMessage.GetMessageInfoByListCode(errorCode);
                spanids = ListTostring(ControlName);
            }
            else//组合对象数据
            {
                Entity = new DisheTypeEntity();
				Entity.lsid = Helper.StringToLong(lsid);
				Entity.distypeid = Helper.StringToLong(distypeid);
				Entity.buscode = buscode;
				Entity.stocode = stocode;
				Entity.pdistypecode = pdistypecode;
				Entity.distypecode = distypecode;
				Entity.dispath = dispath;
				Entity.distypename = distypename;
				Entity.metcode = metcode;
				Entity.fincode = fincode;
				Entity.maxdiscount = Helper.StringToInt(maxdiscount);
				Entity.busSort = Helper.StringToInt(busSort);
				Entity.status = status;
				Entity.cuser = Helper.StringToLong(cuser);
				
				Entity.uuser = Helper.StringToLong(uuser);
				
				
            }
            return strRetuen;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public DataTable Add(string GUID, string UID, out  string lsid, string distypeid, string buscode, string stocode, string pdistypecode, string distypecode, string dispath, string distypename, string metcode, string fincode, string maxdiscount, string busSort, string status, string cuser, string uuser, operatelogEntity entity)
        {
			lsid = "0";
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }

            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("add",  lsid, distypeid, buscode, stocode, pdistypecode, distypecode, dispath, distypename, metcode, fincode, maxdiscount, busSort, status, cuser, uuser, out spanids);
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
        public DataTable Update(string GUID, string UID,  string lsid, string distypeid, string buscode, string stocode, string pdistypecode, string distypecode, string dispath, string distypename, string metcode, string fincode, string maxdiscount, string busSort, string status, string cuser, string uuser, operatelogEntity entity)
        {
			
			if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("update",  lsid, distypeid, buscode, stocode, pdistypecode, distypecode, dispath, distypename, metcode, fincode, maxdiscount, busSort, status, cuser, uuser, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
			//获取更新前的数据对象
            DisheTypeEntity OldEntity = new DisheTypeEntity();
            OldEntity = GetEntitySigInfo(" where lsid='" + lsid + "'");
			//更新数据
            int result = dal.Update(Entity);
            //检测执行结果
            if (CheckResult(result))
            {
                //写日志
                if (entity != null)
                {
                    blllog.Add<DisheTypeEntity>(entity, Entity, OldEntity);
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
        public DisheTypeEntity GetEntitySigInfo(string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            DataTable dt = GetPagingListInfo("", "0", 1, 1, filter, string.Empty, out recnums, out pagenums);
            if (dt != null && dt.Rows.Count > 0)
            {
                return SetEntityInfo(dt.Rows[0]);
            }
            return new DisheTypeEntity();
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
            return new bllPaging().GetPagingInfo("DisheType", "lsid", "*,cusername=dbo.fnGetUserName(cuser),uusername=dbo.fnGetUserName(uuser)", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }

		/// <summary>
        /// 单行数据转实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private DisheTypeEntity SetEntityInfo(DataRow dr)
        {
            DisheTypeEntity Entity = new DisheTypeEntity();
			Entity.lsid = Helper.StringToLong(dr["lsid"].ToString());
			Entity.distypeid = Helper.StringToLong(dr["distypeid"].ToString());
			Entity.buscode = dr["buscode"].ToString();
			Entity.stocode = dr["stocode"].ToString();
			Entity.pdistypecode = dr["pdistypecode"].ToString();
			Entity.distypecode = dr["distypecode"].ToString();
			Entity.dispath = dr["dispath"].ToString();
			Entity.distypename = dr["distypename"].ToString();
			Entity.metcode = dr["metcode"].ToString();
			Entity.fincode = dr["fincode"].ToString();
			Entity.maxdiscount = Helper.StringToInt(dr["maxdiscount"].ToString());
			Entity.busSort = Helper.StringToInt(dr["busSort"].ToString());
			Entity.status = dr["status"].ToString();
			Entity.cuser = Helper.StringToLong(dr["cuser"].ToString());
			
			Entity.uuser = Helper.StringToLong(dr["uuser"].ToString());
			
			
            return Entity;
        }
    }
}