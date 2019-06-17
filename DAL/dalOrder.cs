using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    public class dalOrder
    {
        DataTable dt = new DataTable();
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();

        //点餐记录
        public DataTable GetOrderList(string openid, string type, string currentpage, string pagesize, ref string sumcount)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@openid",openid),
                new SqlParameter("@type",type),
                new SqlParameter("@currentpage",currentpage),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@sumcount",SqlDbType.VarChar ,64,sumcount)
            };
            sqlParameters[4].Direction = ParameterDirection.Output;

            dt = DBHelper.ExecuteDataTable("p_orderhistory", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[4].Value.ToString();

            return dt;
        }

        //根据订单号获取即时订单信息
        public DataTable GetOrderListByForm(string stocode, string orderno)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@orderno",orderno)
            };

            return DBHelper.ExecuteDataTable("p_getorderinfobyform", CommandType.StoredProcedure, sqlParameters);
        }

        //改变订单打印状态
        public int ChangePrintStatusByForm(string stocode, string orderno, ref string mescode)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@orderno",orderno),
                new SqlParameter("@mescode",mescode)
            };

            sqlParameters[2].Direction = ParameterDirection.Output;
            int count = DBHelper.ExecuteNonQuery("p_changeprintstatusbyform", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[2].Value.ToString();
            return count;
        }

        //根据时间区间及处理状态获取订单信息
        public DataTable GetOrderListDataByForm(string stocode, string startdate, string enddate, string currentpage, string pagesize, string isprint, ref string sumcount)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@startdate",startdate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@currentpage",currentpage),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@isprint",isprint),
                new SqlParameter("@sumcount",SqlDbType.VarChar ,64,sumcount)
            };
            sqlParameters[6].Direction = ParameterDirection.Output;

            dt = DBHelper.ExecuteDataTable("p_getorderinfodatabyform", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[6].Value.ToString();

            return dt;
        }
    }
}
