using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XJWZCatering.DAL;

namespace XJWZCatering.BLL
{
    public class bllWXComplaints
    {
        dalWXComplaints dal = new dalWXComplaints();

        /// <summary>
        /// 添加投诉
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="stoname"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int AddComplaints(string openid, string stoname, string content)
        {
            return dal.AddComplaints(openid, stoname, content);
        }

        /// <summary>
        /// 投诉建议列表
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="pagesize"></param>
        /// <param name="currentpage"></param>
        /// <param name="mescode"></param>
        /// <returns></returns>
        public DataTable GetComplaintsList(string openid, string pagesize, string currentpage, ref string sumcount)
        {
            return dal.GetComplaintsList(openid, pagesize, currentpage, ref sumcount);
        }
    }
}
