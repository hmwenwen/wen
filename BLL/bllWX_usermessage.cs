using System.Collections.Generic;
using System.Data;
using XJWZCatering.CommonBasic;
using XJWZCatering.DAL;
using XJWZCatering.Model;

namespace XJWZCatering.BLL
{
    /// <summary>
    /// 用户消息表业务类
    /// </summary>
    public class bllWX_usermessage : bllBase
    {
        dalWX_usermessage dal = new dalWX_usermessage();
        WX_usermessageEntity Entity = new WX_usermessageEntity();

        /// <summary>
        /// 检验表单数据
        /// </summary>
        /// <returns></returns>
        public string CheckPageInfo(string type, string id, string openid, string msgtype, string status, string msgdetails, string title, out string spanids)
        {
            string strRetuen = string.Empty;
            spanids = string.Empty;
            //要验证的实体属性
            List<string> EName = new List<string>() { };
            //要验证的实体属性值
            List<string> EValue = new List<string>() { };
            //错误信息
            List<string> errorCode = new List<string>();
            List<string> ControlName = new List<string>();
            //验证数据
            CheckValue<WX_usermessageEntity>(EName, EValue, ref errorCode, ref ControlName, new WX_usermessageEntity());
            //特殊验证写在下面

            if (errorCode.Count > 0)
            {
                strRetuen = ErrMessage.GetMessageInfoByListCode(errorCode);
                spanids = ListTostring(ControlName);
            }
            else//组合对象数据
            {
                Entity = new WX_usermessageEntity();
                Entity.id = Helper.StringToLong(id);
                Entity.openid = openid;
                Entity.msgtype = msgtype;
                Entity.status = status;
                Entity.msgdetails = msgdetails;
                Entity.title = title;

            }
            return strRetuen;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public DataTable Add(string GUID, string UID, out  string id, string openid, string msgtype, string status, string msgdetails, string title, operatelogEntity entity)
        {
            id = "0";
            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }

            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("add", id, openid, msgtype, status, msgdetails, title, out spanids);
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
        public DataTable Update(string GUID, string UID, string id, string openid, string msgtype, string status, string msgdetails, string title, operatelogEntity entity)
        {

            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("update", id, openid, msgtype, status, msgdetails, title, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
            //获取更新前的数据对象
            WX_usermessageEntity OldEntity = new WX_usermessageEntity();
            OldEntity = GetEntitySigInfo(" where id='" + id + "'");
            //更新数据
            int result = dal.Update(Entity);
            //检测执行结果
            if (CheckResult(result))
            {
                //写日志
                if (entity != null)
                {
                    blllog.Add<WX_usermessageEntity>(entity, Entity, OldEntity);
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
            if (CheckDeleteResult(result, Mescode))
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
        public WX_usermessageEntity GetEntitySigInfo(string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            DataTable dt = GetPagingListInfo("", "0", 1, 1, filter, string.Empty, out recnums, out pagenums);
            if (dt != null && dt.Rows.Count > 0)
            {
                return SetEntityInfo(dt.Rows[0]);
            }
            return new WX_usermessageEntity();
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
            return new bllPaging().GetPagingInfo("WX_usermessage", "id", "*", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }

        /// <summary>
        /// 单行数据转实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private WX_usermessageEntity SetEntityInfo(DataRow dr)
        {
            WX_usermessageEntity Entity = new WX_usermessageEntity();
            Entity.id = Helper.StringToLong(dr["id"].ToString());
            Entity.openid = dr["openid"].ToString();
            Entity.msgtype = dr["msgtype"].ToString();
            Entity.status = dr["status"].ToString();
            Entity.msgdetails = dr["msgdetails"].ToString();
            Entity.title = dr["title"].ToString();

            return Entity;
        }

        /// <summary>
        /// 消息中心
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public DataTable MyMesInfo(string openid)
        {
            return dal.MyMesInfo(openid);
        }

        /// <summary>
        /// 消息中心列表
        /// </summary>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="openid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable MyMesList(string currentpage, string pagesize, string openid, string type, ref string sumcount)
        {
            return dal.MyMesList(currentpage, pagesize, openid, type, ref sumcount);
        }

        /// <summary>
        /// 批量删除消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="ids"></param>
        public int DelMesList(string openid, string ids)
        {
            return dal.DelMesList(openid, ids);
        }
    }
}