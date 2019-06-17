using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    public class dalForm
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        DataTable dt = new DataTable();

        //获取门店预定列表
        public DataTable GetReserveList(string rid, string resdate, string retime, string metcode, string restatus, string mobile, string name, string currentpage, string pagesize, string stocode, string dicid, string ttcode, ref string sumcount)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@rid",rid),
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@resdate",resdate),
                new SqlParameter("@retime",retime),
                new SqlParameter("@metcode",metcode),
                new SqlParameter("@restatus",restatus),
                new SqlParameter("@mobile",mobile),
                new SqlParameter("@name",name),
                new SqlParameter("@currentpage",currentpage),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@dicid",dicid),
                new SqlParameter("@ttcode",ttcode),
                new SqlParameter("@sumcount",SqlDbType.VarChar ,64,sumcount)
            };

            sqlParameters[12].Direction = ParameterDirection.Output;

            dt = DBHelper.ExecuteDataTable("p_getreservelistbyform", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[12].Value.ToString();
            return dt;
        }

        public DataTable GetReserveList2(string stocode, string resdate, string resstime, string resetime, string restatus, string currentpage, string pagesize, ref string sumcount)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@resdate",resdate),
                new SqlParameter("@resstime",resstime),
                new SqlParameter("@resetime",resetime),
                new SqlParameter("@restatus",restatus),
                new SqlParameter("@currentpage",currentpage),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@sumcount",SqlDbType.VarChar ,64,sumcount)
            };

            sqlParameters[7].Direction = ParameterDirection.Output;

            dt = DBHelper.ExecuteDataTable("p_getreservelist2byform", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[7].Value.ToString();

            return dt;
        }

        //更改客户预定状态及绑定桌台号
        public int ModifyReserveInfo(string rid, string status, string metcode, string metname, string ttcode, string cuser, string cname)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@rid",rid),
                new SqlParameter("@status",status),
                new SqlParameter("@metcode",metcode),
                new SqlParameter("@metname",metname),
                new SqlParameter("@ttcode",ttcode),
                new SqlParameter("@cuser",cuser),
                new SqlParameter("@cname",cname)
            };

            return DBHelper.ExecuteNonQuery("p_modifyreserveinfobyform", CommandType.StoredProcedure, sqlParameters);
        }

        //预定
        public int AddReserve(string stocode, string resdate, string restime, string personNum, string metcode, string metname, string phone, string name, string remark, string status, string TerminalType, string sex, string isvip, string ttcode, string cuser, string cname, string dicid, string tcid, string tcname, ref string rid, ref string mescode)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@resdate",resdate),
                new SqlParameter("@restime",restime),
                new SqlParameter("@personNum",personNum),
                new SqlParameter("@metcode",metcode),
                new SqlParameter("@metname",metname),
                new SqlParameter("@phone",phone),
                new SqlParameter("@name",name),
                new SqlParameter("@remark",remark),
                new SqlParameter("@status",status),
                new SqlParameter("@TerminalType",TerminalType),
                new SqlParameter("@sex",sex),
                new SqlParameter("@isvip",isvip),
                new SqlParameter("@ttcode",ttcode),
                new SqlParameter("@cuser",cuser),
                new SqlParameter("@cname",cname),
                new SqlParameter("@dicid",dicid),
                new SqlParameter("@tcid",tcid),
                new SqlParameter("@tcname",tcname),
                new SqlParameter("@rid",SqlDbType.VarChar ,64,rid),
                new SqlParameter("@mescode",SqlDbType.VarChar ,64,mescode)
            };

            sqlParameters[19].Direction = ParameterDirection.Output;
            sqlParameters[20].Direction = ParameterDirection.Output;
            int count = DBHelper.ExecuteNonQuery("p_addreservebyform", CommandType.StoredProcedure, sqlParameters);
            rid = sqlParameters[19].Value.ToString();
            mescode = sqlParameters[20].Value.ToString();
            return count;
        }

        //修改预定信息
        public int ModifyReserve(string rid, string retime, string personNum, string metcode, string metname, string phone, string name, string remark, string status, string dishesremark, string TerminalType)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@rid",rid),
                new SqlParameter("@retime",retime),
                new SqlParameter("@personNum",personNum),
                new SqlParameter("@metcode",metcode),
                new SqlParameter("@metname",metname),
                new SqlParameter("@phone",phone),
                new SqlParameter("@name",name),
                new SqlParameter("@remark",remark),
                new SqlParameter("@status",status),
                new SqlParameter("@dishesremark",dishesremark),
                new SqlParameter("@TerminalType",TerminalType)
            };

            return DBHelper.ExecuteNonQuery("p_modifyreservebyform", CommandType.StoredProcedure, sqlParameters);
        }

        //取消预定
        public int CancelReserve(string rid)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@rid",rid)
            };

            return DBHelper.ExecuteNonQuery("p_cancelyreservebyform", CommandType.StoredProcedure, sqlParameters);
        }

        //获取门店排队信息
        public DataTable GetWaitList(string stocode, string mobile, string lineid, string status, string currentpage, string pagesize, ref string sumcount)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@lineid",lineid),
                new SqlParameter("@mobile",mobile),
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@status",status),
                new SqlParameter("@currentpage",currentpage),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@sumcount",SqlDbType.VarChar ,64,sumcount)
            };

            sqlParameters[6].Direction = ParameterDirection.Output;

            dt = DBHelper.ExecuteDataTable("p_getbuswaitbyform", CommandType.StoredProcedure, sqlParameters);
            sumcount = sqlParameters[6].Value.ToString();
            return dt;
        }

        //排队
        public DataTable AddWaitInfo(string stocode, string mobile, string lineid, ref string mescode)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@stocode",stocode),
                new SqlParameter("@mobile",mobile),
                new SqlParameter("@lineid",lineid),
                new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
            };

            sqlParameters[3].Direction = ParameterDirection.Output;
            DataTable dt = DBHelper.ExecuteDataTable("p_addwaitinfobyform", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[3].Value.ToString();
            return dt;
        }

        //重新排队
        public DataTable ResetWait(string rid, ref string mescode)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@rid",rid),
                new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
            };

            sqlParameters[1].Direction = ParameterDirection.Output;
            DataTable dt = DBHelper.ExecuteDataTable("p_resetwaitbyform", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return dt;
        }

        //修改排队状态
        public int ModifyWaitStatus(string rid, string status)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@rid",rid),
                new SqlParameter("@status",status)
            };

            return DBHelper.ExecuteNonQuery("p_modifywaitstatusbyform", CommandType.StoredProcedure, sqlParameters);
        }

        //更改点餐状态（后支付门店挂单状态更改为下单状态，或取消）
        public int ModifyOrderStatus(string rid, string status)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@rid",rid),
                new SqlParameter("@status",status)
            };

            return DBHelper.ExecuteNonQuery("p_modifyorderstatusbyform", CommandType.StoredProcedure, sqlParameters);
        }

        //打印程序接收到公众号支付订单信息直接打印出即时订单信息
        public DataTable GetOrderList(string stocode, string orderno)
        {
            return dt;
        }
    }
}
