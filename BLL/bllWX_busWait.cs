using System.Collections.Generic;
using System.Data;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
namespace XJWZCatering.BLL
{
    /// <summary>
    /// 客户排队表业务类
    /// </summary>
    public class bllWX_busWait : bllBase
    {
        DAL.dalWX_busWait dal = new DAL.dalWX_busWait();
        WX_busWaitEntity Entity = new WX_busWaitEntity();

        /// <summary>
        /// 检验表单数据
        /// </summary>
        /// <returns></returns>
        public string CheckPageInfo(string type, string bwid, string serialNumber, string buscode, string strcode, string busTop, string sortNum, string busDate, string waitType, string userName, string tel, string waitTime, string remark, string status, string cuser, string uuser, string openid, out string spanids)
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
            CheckValue<WX_busWaitEntity>(EName, EValue, ref errorCode, ref ControlName, new WX_busWaitEntity());
            //特殊验证写在下面

            if (errorCode.Count > 0)
            {
                strRetuen = ErrMessage.GetMessageInfoByListCode(errorCode);
                spanids = ListTostring(ControlName);
            }
            else//组合对象数据
            {
                Entity = new WX_busWaitEntity();
                Entity.bwid = Helper.StringToLong(bwid);
                Entity.serialNumber = serialNumber;
                Entity.buscode = buscode;
                Entity.strcode = strcode;
                Entity.busTop = Helper.StringToInt(busTop);
                Entity.sortNum = sortNum;
                Entity.busDate = Helper.StringToDateTime(busDate);
                Entity.waitType = waitType;
                Entity.userName = userName;
                Entity.tel = tel;
                Entity.waitTime = Helper.StringToDateTime(waitTime);
                Entity.remark = remark;
                Entity.status = status;
                Entity.cuser = Helper.StringToLong(cuser);

                Entity.uuser = Helper.StringToLong(uuser);


                Entity.openid = openid;
            }
            return strRetuen;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public DataTable Add(string GUID, string UID, out  string bwid, string serialNumber, string buscode, string strcode, string busTop, string sortNum, string busDate, string waitType, string userName, string tel, string waitTime, string remark, string status, string cuser, string uuser, string openid, operatelogEntity entity)
        {
            bwid = "0";
            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }

            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("add", bwid, serialNumber, buscode, strcode, busTop, sortNum, busDate, waitType, userName, tel, waitTime, remark, status, cuser, uuser, openid, out spanids);
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
        public DataTable Update(string GUID, string UID, string bwid, string serialNumber, string buscode, string strcode, string busTop, string sortNum, string busDate, string waitType, string userName, string tel, string waitTime, string remark, string status, string cuser, string uuser, string openid, operatelogEntity entity)
        {

            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("update", bwid, serialNumber, buscode, strcode, busTop, sortNum, busDate, waitType, userName, tel, waitTime, remark, status, cuser, uuser, openid, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
            //获取更新前的数据对象
            WX_busWaitEntity OldEntity = new WX_busWaitEntity();
            OldEntity = GetEntitySigInfo(" where bwid='" + bwid + "'");
            //更新数据
            int result = dal.Update(Entity);
            //检测执行结果
            if (CheckResult(result))
            {
                //写日志
                if (entity != null)
                {
                    blllog.Add<WX_busWaitEntity>(entity, Entity, OldEntity);
                }
            }
            return dtBase;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="bwid">标识</param>
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
        public DataTable Delete(string GUID, string UID, string bwid, operatelogEntity entity)
        {
            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string Mescode = string.Empty;
            int result = dal.Delete(bwid, ref Mescode);
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
        public WX_busWaitEntity GetEntitySigInfo(string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            DataTable dt = GetPagingListInfo("", "0", 1, 1, filter, string.Empty, out recnums, out pagenums);
            if (dt != null && dt.Rows.Count > 0)
            {
                return SetEntityInfo(dt.Rows[0]);
            }
            return new WX_busWaitEntity();
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
            return new bllPaging().GetPagingInfo("WX_busWait", "bwid", "*,cusername=dbo.fnGetUserName(cuser),uusername=dbo.fnGetUserName(uuser)", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }

        /// <summary>
        /// 单行数据转实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private WX_busWaitEntity SetEntityInfo(DataRow dr)
        {
            WX_busWaitEntity Entity = new WX_busWaitEntity();
            Entity.bwid = Helper.StringToLong(dr["bwid"].ToString());
            Entity.serialNumber = dr["serialNumber"].ToString();
            Entity.buscode = dr["buscode"].ToString();
            Entity.strcode = dr["strcode"].ToString();
            Entity.busTop = Helper.StringToInt(dr["busTop"].ToString());
            Entity.sortNum = dr["sortNum"].ToString();
            Entity.busDate = Helper.StringToDateTime(dr["busDate"].ToString());
            Entity.waitType = dr["waitType"].ToString();
            Entity.userName = dr["userName"].ToString();
            Entity.tel = dr["tel"].ToString();
            Entity.waitTime = Helper.StringToDateTime(dr["waitTime"].ToString());
            Entity.remark = dr["remark"].ToString();
            Entity.status = dr["status"].ToString();
            Entity.cuser = Helper.StringToLong(dr["cuser"].ToString());
            Entity.uuser = Helper.StringToLong(dr["uuser"].ToString());
            Entity.openid = dr["openid"].ToString();
            return Entity;
        }

        /// <summary>
        /// 获取当前排队信息
        /// </summary>
        /// <example>1-3人  排队人数3  当前桌号A01</example>
        /// <param name="fiter">查询条件</param>
        /// <returns></returns>
        public DataTable GetDataTableInfoBySQL(string GUID, string UID, string fiter)
        {

            if (!CheckLogin(GUID, UID))//非法登录
            {

                return dtBase;
            }

            string sql = "select a.waittype,a.waitTime  ,b.countall,a.sortNum ,c.minperosn,c.maxperson,c.Turncycle,iswait=1 FROM [busWait] as  a  " +
" INNER join(select waittype,max(waitTime) as waitTime,count(waittype) as countall from [busWait] where  status=0 " + fiter.Replace(" a.", " "); ;
            sql += " group by waittype ) as b on  a.waittype = b. waittype   and a.waitTime = b.waitTime " +
 " inner join (select minperosn,maxperson ,Turncycle,linecode from [dbo].setlineUp     where [status]='1' and isdelete='0'  ) as c on a.waittype = c.linecode " +
    " where a.status ='0' " + fiter + " ORDER  BY b.countall ASC    ";
            DataTable returnDt = new DataTable();
            DataTable dt = new bllPaging().GetDataTableInfoBySQL(sql);
            sql = "SELECT linecode AS waittype, waitTime=GETDATE()  ,countall=0,sortNum='0',  minperosn,maxperson ,Turncycle,iswait=1 from [dbo].setlineUp     where [status]='1' and isdelete='0'  ";
            DataTable dtall = new bllPaging().GetDataTableInfoBySQL(sql);
            if (dt.Rows.Count == 0)
            {
                returnDt = dtall;
            }
            else if (dt.Rows.Count != dtall.Rows.Count)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    DataRow[] drs = dtall.Select(" waittype = " + dr["waittype"]);
                    if (drs != null && dtall.Rows.Count > 0)
                    {
                        dtall.Rows.Remove(drs[0]);
                    }

                }
                dtall.Merge(dt);
                dtall.Select(" 1=1", " waittype  asc ");
                returnDt = dtall;
            }
            else
            {
                returnDt = dt;
            }

            return returnDt;
        }


        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="GUID"></param>
        /// <param name="UID"></param>
        /// <param name="fiter"></param>
        /// <returns></returns>
        public DataTable GetDataTableBySQL(string GUID, string UID, string fiter)
        {

            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            return new bllPaging().GetDataTableInfoBySQL(fiter);
        }

        //排队记录
        public DataTable GetWaitList(string openid, string type, string currentpage, string pagesize, ref string sumcount)
        {
            return dal.GetWaitList(openid, type, currentpage, pagesize, ref sumcount);
        }

        //排队
        public DataTable AddWaitInfo(string openid, string lineid, string stocode, ref string mescode)
        {
            return dal.AddWaitInfo(openid, lineid, stocode, ref mescode);
        }
    }
}