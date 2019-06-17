using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XJWZCatering.CommonBasic;
using XJWZCatering.LinkPubWx;
using XJWZCatering.WXHelper;

namespace XJWZCatering.WServer.WxServer
{
    public partial class PullUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        BaseWeb bw = new BaseWeb();
        string access_token = "";
        string access_token_time = "1900-01-01";

        protected void btnPull_Click(object sender, EventArgs e)
        {
            PullBegin();
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        private void gettoken()
        {
            if (Helper.StringToDateTime(access_token_time).AddMinutes(110) < DateTime.Now)
            {
                string dtSql = "select * from [dbo].[wx_access_token]";
                DataTable dt = SQL.XJWZSQLTool.ExecuteDataTable(dtSql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    access_token = dt.Rows[0]["access_token"].ToString();
                    access_token_time = dt.Rows[0]["utime"].ToString();
                }
            }
        }


        public void PullBegin()
        {
            gettoken();
            string dtSql = "select name from syscolumns where id=(select max(id) from sysobjects where xtype='u' and name='WX_members_wx_bak')";
            DataTable dt = SQL.XJWZSQLTool.ExecuteDataTable(dtSql);
            DataTable dtData = new DataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtData.Columns.Add(new DataColumn(dt.Rows[i][0].ToString())); //构建datatable
            }

            var nextopenid = "";
            int count = 0;
            do
            {
                dtData.Clear();
                var url = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + access_token + "&next_openid=" + nextopenid;

                WebClient client = new WebClient();
                var result = Encoding.UTF8.GetString(client.DownloadData(url));
                JObject obj = (JObject)JsonConvert.DeserializeObject(result);

                count = Convert.ToInt32(obj["count"]); //当前请求的用户数
                nextopenid = obj["next_openid"].ToString(); //下一页的标识
                var openids = obj["data"]["openid"];
                for (int i = 0; i < openids.Count(); i++)
                {
                    AddWxUser(openids[i].ToString(), ref dtData);
                    if (dtData.Rows.Count == 1000)
                    {
                        try
                        {
                            SqlBulkCopyInsert(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString(), "WX_members_wx_bak", dtData);
                        }
                        catch
                        { }
                        dtData.Clear();
                    }
                }

                if (dtData.Rows.Count > 0)
                {
                    try
                    {
                        SqlBulkCopyInsert(System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString(), "WX_members_wx_bak", dtData);
                    }
                    catch
                    { }
                    dtData.Clear();
                }

                Thread.Sleep(1000);

            } while (count == 10000);

        }

        /// <summary>
        /// 获取微信用户信息
        /// </summary>
        /// <param name="openid"></param>
        public void AddWxUser(string openid, ref DataTable dt)
        {
            gettoken();
            if (SQL.XJWZSQLTool.GetFirstIntField("select count(1) from WX_members_wx_bak where wxopenid='" + openid + "'") > 0) //如果统一的大id存在，则说明该用户已在关注过关联的公众
            {
                return;
            }
            var url = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + access_token + "&openid=" + openid + "&lang=zh_CN";
            var userJson = bw.WXWebRequest(url, string.Empty);
            if (userJson.Length > 0)
            {
                var strsubscribe = JsonHelper.GetJsonValByKey(userJson, "subscribe");
                var strName = Helper.ReplaceString(JsonHelper.GetJsonValByKey(userJson, "nickname"));
                var strSex = JsonHelper.GetJsonValByKey(userJson, "sex");
                var strCity = JsonHelper.GetJsonValByKey(userJson, "city");
                var strLanguage = JsonHelper.GetJsonValByKey(userJson, "language");
                var strProvince = JsonHelper.GetJsonValByKey(userJson, "province");
                var strCountry = JsonHelper.GetJsonValByKey(userJson, "country");
                var strHeadimg = JsonHelper.GetJsonValByKey(userJson, "headimgurl");
                var unionid = JsonHelper.GetJsonValByKey(userJson, "unionid");
                var strsubscribe_scene = JsonHelper.GetJsonValByKey(userJson, "subscribe_scene");

                if (SQL.XJWZSQLTool.GetFirstIntField("select count(1) from WX_members_wx_bak where openid='" + unionid + "'") > 0) //如果统一的大id存在，则说明该用户已在关注过关联的公众
                {
                    return;
                }

                var dr = dt.NewRow();
                dr["openid"] = unionid;
                dr["subscribe"] = strsubscribe;
                dr["nickname"] = Helper.ReplaceString(strName);
                dr["sex"] = strSex;
                dr["cityid"] = strCity;
                dr["provinceid"] = strProvince;
                dr["language"] = strLanguage;
                dr["headimgurl"] = strHeadimg;
                dr["subscribe_scene"] = strsubscribe_scene;
                dr["ctime"] = DateTime.Now;
                dr["wxopenid"] = openid;
                dr["country"] = strCountry;
                dr["type"] = "1";
                dt.Rows.Add(dr);

            }
        }


        #region 使用SqlBulkCopy将DataTable中的数据批量插入数据库中
        /// <summary>  
        /// 注意：DataTable中的列需要与数据库表中的列完全一致。
        /// 已自测可用。
        /// </summary>  
        /// <param name="conStr">数据库连接串</param>
        /// <param name="strTableName">数据库中对应的表名</param>  
        /// <param name="dtData">数据集</param>  
        public void SqlBulkCopyInsert(string conStr, string strTableName, DataTable dtData)
        {
            try
            {
                using (SqlBulkCopy sqlRevdBulkCopy = new SqlBulkCopy(conStr))//引用SqlBulkCopy  
                {
                    sqlRevdBulkCopy.DestinationTableName = strTableName;//数据库中对应的表名  
                    sqlRevdBulkCopy.NotifyAfter = dtData.Rows.Count;//有几行数据  
                    sqlRevdBulkCopy.WriteToServer(dtData);//数据导入数据库  
                    sqlRevdBulkCopy.Close();//关闭连接  
                }

                Response.Write("成功" + dtData.Rows.Count + "条");
            }
            catch (Exception ex)
            {
                Response.Write("失败：" + ex.Message);
            }
        }
        #endregion
    }
}