using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XJWZCatering.BLL
{
    public class bllStore : bllBase
    {
        DAL.dalStore dal = new DAL.dalStore();

        /// <summary>
        /// 找店
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="currentpage"></param>
        /// <param name="citycode"></param>
        /// <param name="shopcircle"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public DataTable FindStore(string pagesize, string currentpage, string citycode, string shopcircle, string keywords, ref string sumcount)
        {
            return dal.FindStore(pagesize, currentpage, citycode, shopcircle, keywords, ref sumcount);
        }

        /// <summary>
        /// 门店详情
        /// </summary>
        /// <param name="stocode"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public DataSet GetStoDetail(string stocode, string openid)
        {
            return dal.GetStoDetail(stocode, openid);
        }
    }
}
