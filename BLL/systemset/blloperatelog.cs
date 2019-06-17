using System.Collections.Generic;
using System;
using System.Data;
using System.Text;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;
using System.Reflection;

namespace XJWZCatering.BLL
{
    /// <summary>
    /// 后台用户操作日志业务类
    /// </summary>
    public class blloperatelog
    {
        DAL.daloperatelog dal = new DAL.daloperatelog();
        operatelogEntity Entity = new operatelogEntity();

        public blloperatelog()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entity"></param>
        /// <param name="NewEntity"></param>
        /// <param name="OldEntity"></param>
        /// <returns></returns>
        public int Add<T>(operatelogEntity Entity, T NewEntity, T OldEntity)
        {
            if (NewEntity == null || OldEntity == null)
            {
                return 1;
            }
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] PropertyList = NewEntity.GetType().GetProperties();
            if (PropertyList.Length > 0)
            {
                ModelInfoAttribute myAttr;
                foreach (PropertyInfo item in PropertyList)
                {
                    try
                    {
                        switch (item.Name)
                        {
                            case "cuser":
                            case "ctime":
                            case "uuser":
                            case "utime":
                            case "isdelete":
                                continue;
                                break;
                        }

                        string NewVal = NewEntity.GetType().GetProperty(item.Name).GetValue(NewEntity, null).ToString();
                        string OldVal = OldEntity.GetType().GetProperty(item.Name).GetValue(OldEntity, null).ToString();
                        if (NewVal != OldVal)
                        {
                            myAttr = (ModelInfoAttribute)Attribute.GetCustomAttribute(NewEntity.GetType().GetProperty(item.Name), typeof(ModelInfoAttribute));
                            if (myAttr != null)
                            {
                                sb.AppendLine(myAttr.Name + ":" + OldVal + " -> " + NewVal + "；");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteErrorMessage(ex);
                    }
                }
            }
            if (sb.Length > 0)
            {
                Entity.logcontent = sb.ToString();
                Entity.ip = IPHelp.GetClientIP();
                return dal.Add(Entity);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 插入操作日志
        /// </summary>
        /// <param name="Entity">日志对象</param>
        /// <returns></returns>
        public int Add(operatelogEntity Entity)
        {
            Entity.ip = IPHelp.GetClientIP();
            return dal.Add(Entity);
        }

        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="filter">指定条件</param>
        /// <returns>返回第一行</returns>
        public DataTable GetPagingSigInfo(string GUID, string UID, string filter)
        {
            DataTable dtLogin = LoginUniqueness.LoginedCheckFromPage(GUID, UID);
            if (dtLogin.Rows.Count > 0)//非法登录
            {
                return dtLogin;
            }
            int recnums = 0;
            int pages = 0;
            DataTable dt = GetPagingListInfo(GUID, UID, 1, 1, filter, string.Empty, out recnums, out pages);
            return dt;
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
        public DataTable GetPagingListInfo(string GUID, string UID, int pageSize, int currentpage, string filter, string order, out int recnums, out int pages)
        {
            return new bllPaging().GetPagingInfo("operatelog", "id", "*,cusername=dbo.fnGetUserName(cuser)", pageSize, currentpage, filter, "", order, out recnums, out pages);
        }
    }
}