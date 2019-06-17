using System.Collections.Generic;
using System.Data;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
namespace XJWZCatering.BLL
{
    /// <summary>
    /// 客户预订表业务类
    /// </summary>
    public class bllWX_busDestine : bllBase
    {
        DAL.dalWX_busDestine dal = new DAL.dalWX_busDestine();
        WX_busDestineEntity Entity = new WX_busDestineEntity();

        /// <summary>
        /// 检验表单数据
        /// </summary>
        /// <returns></returns>
        public string CheckPageInfo(string type, string ID, string stocode, string buscode, string desDate, string desTime, string dicid, string personNum, string metcode, string userName, string tel, string remark, string status, string dishesremark, string TerminalType, string openid, out string spanids)
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
            CheckValue<WX_busDestineEntity>(EName, EValue, ref errorCode, ref ControlName, new WX_busDestineEntity());
            //特殊验证写在下面

            if (errorCode.Count > 0)
            {
                strRetuen = ErrMessage.GetMessageInfoByListCode(errorCode);
                spanids = ListTostring(ControlName);
            }
            else//组合对象数据
            {
                Entity = new WX_busDestineEntity();
                Entity.ID = Helper.StringToLong(ID);
                Entity.stocode = stocode;
                Entity.buscode = buscode;
                Entity.desDate = Helper.StringToDateTime(desDate);
                Entity.desTime = desTime;
                Entity.dicid = Helper.StringToLong(dicid);
                Entity.personNum = Helper.StringToInt(personNum);
                Entity.metcode = metcode;
                Entity.userName = userName;
                Entity.tel = tel;
                Entity.remark = remark;
                Entity.status = status;

                Entity.dishesremark = dishesremark;
                Entity.TerminalType = TerminalType;
                Entity.openid = openid;
            }
            return strRetuen;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public DataTable Add(string GUID, string UID, out  string ID, string stocode, string buscode, string desDate, string desTime, string dicid, string personNum, string metcode, string userName, string tel, string remark, string status, string dishesremark, string TerminalType, string openid, operatelogEntity entity)
        {
            ID = "0";
            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }

            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("add", ID, stocode, buscode, desDate, desTime, dicid, personNum, metcode, userName, tel, remark, status, dishesremark, TerminalType, openid, out spanids);
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
        public DataTable Update(string GUID, string UID, string ID, string stocode, string buscode, string desDate, string desTime, string dicid, string personNum, string metcode, string userName, string tel, string remark, string status, string dishesremark, string TerminalType, string openid, operatelogEntity entity)
        {

            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("update", ID, stocode, buscode, desDate, desTime, dicid, personNum, metcode, userName, tel, remark, status, dishesremark, TerminalType, openid, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
            //获取更新前的数据对象
            WX_busDestineEntity OldEntity = new WX_busDestineEntity();
            OldEntity = GetEntitySigInfo(" where ID='" + ID + "'");
            //更新数据
            int result = dal.Update(Entity);
            //检测执行结果
            if (CheckResult(result))
            {
                //写日志
                if (entity != null)
                {
                    blllog.Add<WX_busDestineEntity>(entity, Entity, OldEntity);
                }
            }
            return dtBase;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="ID">标识</param>
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
        public DataTable Delete(string GUID, string UID, string ID, operatelogEntity entity)
        {
            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string Mescode = string.Empty;
            int result = dal.Delete(ID, ref Mescode);
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
        public WX_busDestineEntity GetEntitySigInfo(string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            DataTable dt = GetPagingListInfo("", "0", 1, 1, filter, string.Empty, out recnums, out pagenums);
            if (dt != null && dt.Rows.Count > 0)
            {
                return SetEntityInfo(dt.Rows[0]);
            }
            return new WX_busDestineEntity();
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
            return new bllPaging().GetPagingInfo("WX_busDestine", "ID", "*", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }

        /// <summary>
        /// 单行数据转实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private WX_busDestineEntity SetEntityInfo(DataRow dr)
        {
            WX_busDestineEntity Entity = new WX_busDestineEntity();
            Entity.ID = Helper.StringToLong(dr["ID"].ToString());
            Entity.stocode = dr["stocode"].ToString();
            Entity.buscode = dr["buscode"].ToString();
            Entity.desDate = Helper.StringToDateTime(dr["desDate"].ToString());
            Entity.desTime = dr["desTime"].ToString();
            Entity.dicid = Helper.StringToLong(dr["dicid"].ToString());
            Entity.personNum = Helper.StringToInt(dr["personNum"].ToString());
            Entity.metcode = dr["metcode"].ToString();
            Entity.userName = dr["userName"].ToString();
            Entity.tel = dr["tel"].ToString();
            Entity.remark = dr["remark"].ToString();
            Entity.status = dr["status"].ToString();

            Entity.dishesremark = dr["dishesremark"].ToString();
            Entity.TerminalType = dr["TerminalType"].ToString();
            Entity.openid = dr["openid"].ToString();
            return Entity;
        }

        // 获取预约今天 明天 后台预约详细信息及备注信息
        public DataSet GetReserveTimes(string stocode)
        {
            return dal.GetReserveTimes(stocode);
        }

        /// <summary>
        ///获取指定日期预约时间及预定状态
        /// </summary>
        /// <param name="stocode"></param>
        /// <param name="currentdate"></param>
        /// <returns></returns>
        public DataSet GetReserveTime(string stocode, string currentdate)
        {
            return dal.GetReserveTime(stocode, currentdate);
        }

        /// <summary>
        /// 取消预定
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="stocode"></param>
        /// <param name="orderid"></param>
        /// <param name="mescode"></param>
        public void CancelReserve(string openid, string stocode, string orderid, ref string mescode)
        {
            dal.CancelReserve(openid, stocode, orderid, ref mescode);
        }

        //预定 检测
        public void CheckReserve(string openid, string stocode, string sdate, string stime, ref string mescode)
        {
            dal.CheckReserve(openid, stocode, sdate, stime, ref mescode);
        }

        //预约
        public DataTable AddReserve(string openid, string stocode, string rdate, string rtime, int usernum, string remark)
        {
            return dal.AddReserve(openid, stocode, rdate, rtime, usernum, remark);
        }

        //预约记录
        public DataTable GetReserveRecordlist(string openid, string type, string currentpage, string pagesize, ref string sumcount)
        {
            return dal.GetReserveRecordlist(openid, type, currentpage, pagesize, ref sumcount);
        }
    }
}