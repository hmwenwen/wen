using System.Data;
using System.Data.SqlClient;
using XJWZCatering.Model;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.DAL
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public partial class dalWX_members_wx
    {
        MSSqlDataAccess DBHelper = new MSSqlDataAccess();
        int intReturn;
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ref WX_members_wxEntity Entity)
        {
            intReturn = 0;
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@id", Entity.id),
				new SqlParameter("@openid", Entity.openid),
				new SqlParameter("@subscribe", Entity.subscribe),
				new SqlParameter("@nickname", Entity.nickname),
				new SqlParameter("@sex", Entity.sex),
				new SqlParameter("@language", Entity.language),
				new SqlParameter("@cityid", Entity.cityid),
				new SqlParameter("@provinceid", Entity.provinceid),
				new SqlParameter("@country", Entity.country),
				new SqlParameter("@mobile", Entity.mobile),
				new SqlParameter("@headimgurl", Entity.headimgurl),
				new SqlParameter("@subscribe_scene", Entity.subscribe_scene),
				new SqlParameter("@upwd", Entity.upwd),
				new SqlParameter("@notpwd", Entity.notpwd),
                new SqlParameter("@wxopenid", Entity.wxopenid)
             };
            sqlParameters[0].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_members_wx_Add", CommandType.StoredProcedure, sqlParameters);
            if (intReturn == 0)
            {
                Entity.id = int.Parse(sqlParameters[0].Value.ToString());
            }
            return intReturn;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(WX_members_wxEntity Entity)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@openid", Entity.openid),
				new SqlParameter("@subscribe", Entity.subscribe),
				new SqlParameter("@nickname", Entity.nickname),
				new SqlParameter("@sex", Entity.sex),
				new SqlParameter("@language", Entity.language),
				new SqlParameter("@cityid", Entity.cityid),
				new SqlParameter("@provinceid", Entity.provinceid),
				new SqlParameter("@country", Entity.country),
				new SqlParameter("@mobile", Entity.mobile),
				new SqlParameter("@headimgurl", Entity.headimgurl),
				new SqlParameter("@subscribe_scene", Entity.subscribe_scene),
				new SqlParameter("@upwd", Entity.upwd),
				new SqlParameter("@notpwd", Entity.notpwd),
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_members_wx_Update", CommandType.StoredProcedure, sqlParameters);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int UpdateStatus(string ids, string Status)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@ids", ids),
				new SqlParameter("@status", Status)
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_members_wx_UpdateStatus", CommandType.StoredProcedure, sqlParameters);
        }


        /// <summary>
        /// 更新单个字段
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="field">状态</param>
        /// <returns></returns>
        public int UpdateField(string openid, string fieldName, string fieldVal)
        {
            SqlParameter[] sqlParameters = 
            {
				new SqlParameter("@openid", openid),
				new SqlParameter("@FieldName", fieldName),
                new SqlParameter("@FieldVal", fieldVal)
             };
            return DBHelper.ExecuteNonQuery("dbo.p_WX_members_wx_UpdateField", CommandType.StoredProcedure, sqlParameters);
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID，多个用,分隔</param>
        /// <returns>返回操作结果</returns>
        public int Delete(string id, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@id", id),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };
            sqlParameters[1].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_WX_members_wx_Delete", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[1].Value.ToString();
            return intReturn;
        }

        public int ModifyCityByOpenid(string openid, string proname, string cityname, string areaname)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@proname", proname),
                 new SqlParameter("@cityname", cityname),
                 new SqlParameter("@areaname", areaname)
             };

            return DBHelper.ExecuteNonQuery("p_modifycitybyopenid", CommandType.StoredProcedure, sqlParameters);
        }

        public int ModifyInfoByOpenid(string openid, string type, string value)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@type", type),
                 new SqlParameter("@value", value)
             };

            return DBHelper.ExecuteNonQuery("p_modifyinfobyopenid", CommandType.StoredProcedure, sqlParameters);
        }

        //设置小额免密
        public int SetnotPwd(string openid, string notpwd, string amount, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@notpwd", notpwd),
                 new SqlParameter("@amount", amount),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };

            sqlParameters[3].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_wx_setnotpwd", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[3].Value.ToString();
            return intReturn;
        }

        //设置手机号
        public int SetPhone(string openid, string mobile, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@mobile", mobile),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };

            sqlParameters[2].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_wx_setphone", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[2].Value.ToString();
            return intReturn;
        }

        //设置支付密码
        public int SetUPwd(string openid, string upwd, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@upwd", upwd),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };

            sqlParameters[2].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_wx_setpwd", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[2].Value.ToString();
            return intReturn;
        }

        //设置支付密码（新）
        public int SetUPwdNew(string openid, string mobile, string idno, string upwd, ref string mescode)
        {
            SqlParameter[] sqlParameters = 
            {
                 new SqlParameter("@openid", openid),
                 new SqlParameter("@mobile",mobile),
                 new SqlParameter("@idno",idno),
                 new SqlParameter("@upwd", upwd),
                 new SqlParameter("@mescode",SqlDbType.NVarChar ,256,mescode)
             };

            sqlParameters[4].Direction = ParameterDirection.Output;
            intReturn = DBHelper.ExecuteNonQuery("dbo.p_wx_setpwdnew", CommandType.StoredProcedure, sqlParameters);
            mescode = sqlParameters[4].Value.ToString();
            return intReturn;
        }
    }
}