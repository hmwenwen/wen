using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XJWZCatering.DAL;

namespace XJWZCatering.BLL
{
    public class bllHelp
    {
        dalHelp dal = new dalHelp();
        /// <summary>
        /// 帮助中心
        /// </summary>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="keywords"></param>
        /// <param name="sumcount"></param>
        /// <returns></returns>
        public DataTable GetHelpList(string currentpage, string pagesize, string keywords, ref string sumcount)
        {
            return dal.GetHelpList(currentpage, pagesize, keywords, ref sumcount);
        }
    }
}
