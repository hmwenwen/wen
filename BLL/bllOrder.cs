using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XJWZCatering.DAL;

namespace XJWZCatering.BLL
{
    public class bllOrder
    {
        dalOrder dal = new dalOrder();

        //点餐记录
        public DataTable GetOrderList(string openid, string type, string currentpage, string pagesize, ref string sumcount)
        {
            return dal.GetOrderList(openid, type, currentpage, pagesize, ref sumcount);
        }

        //根据订单号获取即时订单信息
        public DataTable GetOrderListByForm(string stocode, string orderno)
        {
            return dal.GetOrderListByForm(stocode, orderno);
        }

        //改变订单打印状态
        public int ChangePrintStatusByForm(string stocode, string orderno, ref string mescode)
        {
            return dal.ChangePrintStatusByForm(stocode, orderno, ref mescode);
        }

        //根据时间区间及处理状态获取订单信息
        public DataTable GetOrderListDataByForm(string stocode, string startdate, string enddate, string currentpage, string pagesize, string isprint, ref string sumcount)
        {
            return dal.GetOrderListDataByForm(stocode, startdate, enddate, currentpage, pagesize, isprint, ref sumcount);
        }
    }
}
