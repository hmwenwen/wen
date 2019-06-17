using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XJWZCatering.DAL;

namespace XJWZCatering.BLL
{
    public class bllForm
    {
        dalForm dal = new dalForm();

        //获取门店预定列表
        public DataTable GetReserveList(string rid, string resdate, string retime, string metcode, string restatus, string mobile, string name, string currentpage, string pagesize, string stocode, string dicid, string ttcode, ref string sumcount)
        {
            return dal.GetReserveList(rid, resdate, retime, metcode, restatus, mobile, name, currentpage, pagesize, stocode, dicid, ttcode, ref sumcount);
        }

        public DataTable GetReserveList2(string stocode, string resdate, string resstime, string resetime, string restatus, string currentpage, string pagesize, ref string sumcount)
        {
            return dal.GetReserveList2(stocode, resdate, resstime, resetime, restatus, currentpage, pagesize, ref sumcount);
        }

        //更改客户预定状态及绑定桌台号
        public int ModifyReserveInfo(string rid, string status, string metcode, string metname, string ttcode, string cuser, string cname)
        {
            return dal.ModifyReserveInfo(rid, status, metcode, metname, ttcode, cuser, cname);
        }

        //预定
        public int AddReserve(string stocode, string resdate, string restime, string personNum, string metcode, string metname, string phone, string name, string remark, string status, string TerminalType, string sex, string isvip, string ttcode, string cuser, string cname, string dicid, string tcid, string tcname, ref string rid, ref string mescode)
        {
            return dal.AddReserve(stocode, resdate, restime, personNum, metcode, metname, phone, name, remark, status, TerminalType, sex, isvip, ttcode, cuser, cname, dicid, tcid, tcname, ref rid, ref mescode);
        }

        //修改预定信息
        public int ModifyReserve(string rid, string retime, string personNum, string metcode, string metname, string phone, string name, string remark, string status, string dishesremark, string TerminalType)
        {
            return dal.ModifyReserve(rid, retime, personNum, metcode, metname, phone, name, remark, status, dishesremark, TerminalType);
        }

        //取消预定
        public int CancelReserve(string rid)
        {
            return dal.CancelReserve(rid);
        }

        //获取门店排队信息
        public DataTable GetWaitList(string stocode, string mobile, string lineid, string status, string currentpage, string pagesize, ref string sumcount)
        {
            return dal.GetWaitList(stocode, mobile, lineid, status, currentpage, pagesize, ref sumcount);
        }

        //排队
        public DataTable AddWaitInfo(string stocode, string mobile, string lineid, ref string mescode)
        {
            return dal.AddWaitInfo(stocode, mobile, lineid, ref mescode);
        }

        //重新排队
        public DataTable ResetWait(string rid, ref string mescode)
        {
            return dal.ResetWait(rid, ref mescode);
        }

        //修改排队状态
        public int ModifyWaitStatus(string rid, string status)
        {
            return dal.ModifyWaitStatus(rid, status);
        }

        //更改点餐状态（后支付门店挂单状态更改为下单状态，或取消）
        public int ModifyOrderStatus(string rid, string status)
        {
            return dal.ModifyOrderStatus(rid, status);
        }

        //打印程序接收到公众号支付订单信息直接打印出即时订单信息
        public DataTable GetOrderList(string stocode, string orderno)
        {
            return dal.GetOrderList(stocode, orderno);
        }
    }
}
