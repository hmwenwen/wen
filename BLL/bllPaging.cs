using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using XJWZCatering.CommonBasic;
//using XJWZCatering.Model.store;

namespace XJWZCatering.BLL
{
    public class bllPaging
    {
        MSSqlDataAccess Obj = new MSSqlDataAccess();

        /// <summary>
        /// 分页功能
        /// </summary>
        /// <param name="tableName">表名，可以是多个表，最好用别名</param>
        /// <param name="primarykey">主键，可以为空，但@order为空时该值不能为空</param>
        /// <param name="fields">要取出的字段，可以是多个表的字段，可以为空，为空表示select *  </param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="currentpage">当前页，表示第页</param>
        /// <param name="filter">条件，可以为空，不用填where</param>
        /// <param name="group">分组依据，可以为空，不用填group by</param>
        /// <param name="order">排序，可以为空，为空默认按主键升序排列，不用填order by</param>
        /// <param name="recnums">记录个数</param>
        /// <param name="pagenums">页数</param>
        /// <returns></returns>
        public DataTable GetPagingInfo(string tableName, string primarykey, string fields, int pageSize, int currentpage, string filter, string group, string order, out int recnums, out int pagenums)
        {
            DataTable Dt = new DataTable("data");
            try
            {
                SqlParameter[] sqlParameters = 
                {
                     new SqlParameter("@tablenames", tableName),
                     new SqlParameter("@primarykey", primarykey),
                     new SqlParameter("@fields", fields),
                     new SqlParameter("@pagesize", pageSize),
                     new SqlParameter("@currentpage", currentpage),
                     new SqlParameter("@filter", filter),
                     new SqlParameter("@group", group),
                     new SqlParameter("@order", order),
                     new SqlParameter("@recnums", 0),
                     new SqlParameter("@pagenums", 0)
                 };
                sqlParameters[8].Direction = ParameterDirection.Output;
                sqlParameters[9].Direction = ParameterDirection.Output;
                Dt = Obj.ExecuteDataTable("dbo.pPagingLarge", CommandType.StoredProcedure, sqlParameters);
                recnums = Helper.StringToInt(sqlParameters[8].Value.ToString());
                pagenums = Helper.StringToInt(sqlParameters[9].Value.ToString());
                return Dt;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
                recnums = 0;
                pagenums = 0;
                return Dt;
            }
        }

        /// <summary>
        /// 执行存储过程获取数据表
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public DataTable GetDataTableInfoByProcedure(string ProcedureName, SqlParameter[] sqlParameters)
        {
            DataTable Dt = new DataTable("data");
            try
            {
                Dt = Obj.ExecuteDataTable(ProcedureName, CommandType.StoredProcedure, sqlParameters);
                return Dt;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
                return Dt;
            }
        }

        /// <summary>
        /// 执行存储过程获取数据表
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public DataSet GetDatasetByProcedure(string ProcedureName, SqlParameter[] sqlParameters)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = Obj.ExecuteDataSet(ProcedureName, CommandType.StoredProcedure, sqlParameters);
                return ds;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
                return ds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public DataTable GetDataTableInfoBySQL(string SQL)
        {
            return Obj.ExecuteDataTable(SQL);
        }

        public DataSet GetDataSetInfoBySQL(string SQL)
        {
            return Obj.ExecuteDataSet(SQL);
        }

        /// <summary>
        /// 执行SQL 返回第一行第一列
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public string ExecuteScalarBySQL(string SQL)
        {
            object obje = Obj.ExecuteScalar(SQL);
            if (obje != null)
            {
                return obje.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 执行SQL命令,并返回受影响的行数
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public int ExecuteNonQueryBySQL(string SQL)
        {
            return Obj.ExecuteNonQuery(SQL);
        }

        /// <summary>
        /// 执行SQL命令,并返回受影响的行数
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public int ExecuteNonQueryBySQL2(string SQL)
        {
            return Obj.ExecuteNonQuery2(SQL, System.Data.CommandType.Text);
        }

        /// <summary>
        /// 执行sql命令,成功返回true，失败返回false
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public bool ExecuteNonQueryBySQL3(string SQL)
        {
            return Obj.ExecuteNonQuery3(SQL, System.Data.CommandType.Text);
        }

        /// <summary>
        /// 获取关联表的数据
        /// </summary>
        /// <param name="RelaTableName">关联表名称</param>
        /// <param name="getFileName">获取值得字段</param>
        /// <param name="MatchFieldName">匹配字段名</param>
        /// <param name="MatchFieldValue">匹配字段值</param>
        /// <returns></returns>
        public string getRelaValue(string RelaTableName, string getFileName, string MatchFieldName, string MatchFieldValue)
        {
            string strResult = string.Empty;
            string sql = string.Format(" select top 1 {1} from {0} where {2}='{3}'  ", RelaTableName, getFileName, MatchFieldName, MatchFieldValue);
            DataTable dt = GetDataTableInfoBySQL(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                strResult = dt.Rows[0][getFileName].ToString();
            }
            dt = null;
            return strResult;
        }

        /// <summary>
        /// 获取商户和门店编号
        /// </summary>
        /// <returns></returns>
        public DataTable getBusinessStoreCode()
        {
            DataTable dtBS = null;
            string sql = string.Format(" SELECT b.*,s.stocode,s.cname as stocname,s.sname as stosname FROM dbo.Business b LEFT OUTer JOIN Store s ON b.buscode=s.buscode  ");
            dtBS = GetDataTableInfoBySQL(sql);
            return dtBS;
        }

        /// <summary>
        /// 执行sql文件中的语句(执行成功则删除该文件)
        /// </summary>
        /// <param name="SqlFilePath"></param>
        /// <returns></returns>
        public bool ExecSqlFile(string SqlFilePath)
        {
            bool blnResult = true;
            try
            {
                if (System.IO.File.Exists(SqlFilePath))
                {
                    List<string> lstSql = ReadSqlFile(SqlFilePath);
                    if (lstSql != null && lstSql.Count > 0)
                    {
                        for (int i = 0; i < lstSql.Count; i++)
                        {
                            string sql = lstSql[i].Trim();
                            if (sql != "")
                            {
                                if (!ExecuteNonQueryBySQL3(sql))
                                {
                                    blnResult = false;
                                }

                            }
                        }
                    }
                    if (blnResult)
                    {
                        System.IO.File.Delete(SqlFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            return blnResult;
        }

        /// <summary>
        /// 执行sql文件中的语句(执行成功则删除该文件)
        /// </summary>
        /// <param name="SqlFilePath"></param>
        /// <returns></returns>
        public bool ExecSqlFile(string SqlFilePath, bool SuccessClearFile)
        {
            bool blnResult = true;
            try
            {
                if (System.IO.File.Exists(SqlFilePath))
                {
                    List<string> lstSql = ReadSqlFile(SqlFilePath);
                    if (lstSql != null && lstSql.Count > 0)
                    {
                        for (int i = 0; i < lstSql.Count; i++)
                        {
                            string sql = lstSql[i].Trim();
                            if (sql != "")
                            {
                                if (!ExecuteNonQueryBySQL3(sql))
                                {
                                    blnResult = false;
                                }
                            }
                        }
                    }

                    if (SuccessClearFile && blnResult)
                    {
                        FileStream fs = new FileStream(SqlFilePath, FileMode.Create);
                        fs.Close();
                        fs = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            return blnResult;
        }

        /// <summary>
        /// 逐行读取sql文本,过滤掉非法行和注释
        /// </summary>
        /// <param name="sqlFileFullPath"></param>
        /// <returns></returns>
        private List<string> ReadSqlFile(string sqlFileFullPath)
        {
            List<string> lstSql = new List<string>();
            if (System.IO.File.Exists(sqlFileFullPath))
            {
                System.Text.UnicodeEncoding code = new System.Text.UnicodeEncoding();
                System.Text.UTF8Encoding codeutf8 = new UTF8Encoding();
                System.IO.StreamReader file = new System.IO.StreamReader(sqlFileFullPath, System.Text.Encoding.Default);
                string line;
                StringBuilder sbSql = new StringBuilder();
                while ((line = file.ReadLine()) != null)
                {
                    try
                    {
                        string sql = line.Trim();
                        if (sql.Length >= 2)
                        {
                            #region 大于一个字符的
                            string strHead = sql.Substring(0, 2);
                            if (strHead == "--") continue;
                            if (sql.Length >= 3)
                            {
                                strHead = sql.Substring(0, 3).ToLower();
                                if (strHead.ToLower() == "go " || sql.Trim().ToLower() == "go")
                                {
                                    if (sbSql.Length > 0)
                                    {
                                        lstSql.Add(sbSql.ToString() + " ");
                                        sbSql.Clear();
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                if (strHead == "go" || sql.Trim().ToLower() == "go")
                                {
                                    if (sbSql.Length > 0)
                                    {
                                        lstSql.Add(sbSql.ToString() + " ");
                                        sbSql.Clear();
                                        continue;
                                    }
                                }
                            }

                            sql = SqlCommDel(sql);

                            sbSql.AppendLine(sql + " ");
                            #endregion
                        }
                        else
                        {
                            sbSql.AppendLine(sql + " ");
                        }
                    }
                    catch (Exception ex)
                    {
                        string strErr = ex.Message;
                    }

                }

                if (sbSql.Length > 0)
                {
                    lstSql.Add(sbSql.ToString() + " ");
                    sbSql.Clear();

                }

                if (file != null)
                {
                    file.Close();
                    file = null;
                }

                sbSql = null;
            }

            return lstSql;
        }

        /// <summary>
        /// 剔除 --注释语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private string SqlCommDel(string sql)
        {
            string strResult = string.Empty;
            if (sql.Contains("--"))
            {
                if (sql.Trim().Substring(0, 2) == "--") return "";
                int post = sql.IndexOf("--");
                string sqlTem1 = sql.Substring(0, post);
                string sqlTem2 = sql.Substring(post + 2);
                bool blnHaveQM2 = false;
                if (sqlTem2.Length > 0)
                {
                    for (int i = 0; i < sqlTem2.Length; i++)
                    {
                        if (sqlTem2.Substring(i, 1) == "'")
                        {
                            blnHaveQM2 = true;
                            break;
                        }
                    }
                }

                if (blnHaveQM2)
                {//有单引号
                    //检查前面的单引号个数
                    int intQMCount = 0;
                    for (int i = 0; i < sqlTem1.Length; i++)
                    {
                        if (sqlTem1.Substring(i, 1) == "'")
                        {
                            intQMCount++;
                        }
                    }
                    if (intQMCount == 0)
                    {
                        strResult = sqlTem1;
                        return SqlCommDel(strResult);
                    }
                    else
                    {
                        if (intQMCount % 2 == 0)
                        {
                            strResult = sqlTem1;
                            return SqlCommDel(strResult);
                        }
                        else
                        {
                            return SqlCommDel(sqlTem1) + "--" + SqlCommDel(sqlTem2);
                        }
                    }
                }
                else
                {
                    strResult = sqlTem1;
                    return SqlCommDel(strResult);
                }

            }
            else
            {
                return sql;
            }
            return strResult;
        }

        /// <summary>
        /// 将当天及以前的已交班数据导入历史表
        /// </summary>
        /// <returns></returns>
        public bool CurrDataToHistory()
        {
            bool blnResult = false;
            try
            {
                SqlParameter[] sqlParameters = 
                {
                     new SqlParameter("@lastdateTime", (System.DateTime.Now.AddDays(1)).ToString("yyyy-MM-dd")+" 07:00:00" )
                 };
                int intResult = Obj.ExecuteNonQuery("dbo.p_DataImportToHistory", CommandType.StoredProcedure, sqlParameters);
                blnResult = true;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            return blnResult;
        }

        private bool Upload_Request(string address, string fileNamePath, string saveName)
        {
            bool returnValue = false;

            // 要上传的文件 
            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);

            //时间戳 
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");

            //请求头部信息 
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(strBoundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("file");
            sb.Append("\"; filename=\"");
            sb.Append(saveName);
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append("application/octet-stream");
            sb.Append("\r\n");
            sb.Append("\r\n");
            string strPostHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);

            // 根据uri创建HttpWebRequest对象 
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(address));
            httpReq.Method = "POST";

            //对发送的数据不使用缓存 
            httpReq.AllowWriteStreamBuffering = false;

            //设置获得响应的超时时间（300秒） 
            httpReq.Timeout = 300000;
            httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
            long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
            long fileLength = fs.Length;
            httpReq.ContentLength = length;
            try
            {

                //每次上传4k 
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength];

                //已上传的字节数 
                long offset = 0;

                //开始上传时间 
                DateTime startTime = DateTime.Now;
                int size = r.Read(buffer, 0, bufferLength);
                Stream postStream = httpReq.GetRequestStream();

                //发送请求头部消息 
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);
                    offset += size;
                    size = r.Read(buffer, 0, bufferLength);
                }
                //添加尾部的时间戳 
                postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                postStream.Close();

                //获取服务器端的响应 
                WebResponse webRespon = httpReq.GetResponse();
                Stream s = webRespon.GetResponseStream();
                StreamReader sr = new StreamReader(s);
                string jsonStr = sr.ReadToEnd();
                sr.Close();

                string mes = string.Empty;
                string status = string.Empty;
                JsonHelper.JsonToMessage(jsonStr, out status, out mes);
                if (status == "0")
                {
                    //提交远程服务成功
                    returnValue = true;
                }
            }
            catch(Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            finally
            {
                fs.Close();
                r.Close();
            }
            return returnValue;
        }

        /// <summary>
        /// 将当天及以前的已交班数据导入历史表并上传业务数据到连锁端(CGD修改)
        /// </summary>
        public bool UploadBusData(bool isTest)
        {
            bool blnResult = false;
            try
            {
                bool blnToHistory = true;
                if (!isTest)
                {
                    blnToHistory = CurrDataToHistory();
                }
                if (blnToHistory)
                {
                    string strSavePath = string.Empty;
                    #region 获取业务数据sql文件存放路径
                    DataTable dtDataPath = Obj.ExecuteDataTable(string.Format("select top 1 * from StoreUploaddatapath order by utime desc "));
                    if (dtDataPath != null && dtDataPath.Rows.Count > 0)
                    {
                        if (dtDataPath.Rows[0]["datapath"] != DBNull.Value)
                        {
                            strSavePath = dtDataPath.Rows[0]["datapath"].ToString();
                        }
                    }
                    #endregion

                    if (string.IsNullOrEmpty(strSavePath) || strSavePath.Trim() == "")
                    {
                        return false;
                    }
                    string strSearchPattern = "busdata_*.sql";

                    string[] arrSqlFiles = System.IO.Directory.GetFiles(strSavePath, strSearchPattern);
                    if (arrSqlFiles != null && arrSqlFiles.Length > 0)
                    {
                        string strUrl = Helper.GetAppSettings("MemberService");
                        string InterfaceUrl = strUrl.TrimEnd('/') + "/upload/ReceiveBusdata.ashx";
                        bool Flag = false;
                        string fullname = string.Empty;
                        for (int i = 0; i < arrSqlFiles.Length; i++)
                        {
                            fullname = arrSqlFiles[i];
                            Flag = Upload_Request(InterfaceUrl, fullname, fullname.Substring(fullname.LastIndexOf("\\") + 1));
                            if (Flag)
                            {
                                //提交远程服务成功
                                System.IO.File.Delete(arrSqlFiles[i]);
                                ErrorLog.WriteErrorMessage("已上传成功："+arrSqlFiles[i]);
                            }
                            else
                            {
                                ErrorLog.WriteErrorMessage("上传失败："+arrSqlFiles[i]);
                            }
                        }
                    }
                    else
                    {
                        blnResult = true;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            return blnResult;
        }

        /// <summary>
        /// 将当天及以前的门店基础数据上传到连锁端
        /// </summary>
        public bool UploadBusData()
        {
            bool blnResult = false;
            WebServiceHelper objWebServiceHelper = new WebServiceHelper();
            try
            {
                string strSavePath = string.Empty;
                #region 获取业务数据sql文件存放路径
                DataTable dtDataPath = Obj.ExecuteDataTable(string.Format("select top 1 * from StoreUploaddatapath order by utime desc "));
                if (dtDataPath != null && dtDataPath.Rows.Count > 0)
                {
                    if (dtDataPath.Rows[0]["datapath"] != DBNull.Value)
                    {
                        strSavePath = dtDataPath.Rows[0]["datapath"].ToString();
                    }
                    else
                    {
                        strSavePath = "d:\\";
                    }
                }
                #endregion

                if (string.IsNullOrEmpty(strSavePath) || strSavePath.Trim() == "")
                {
                    return false;
                }
                string strSearchPattern = "base_*.sql";

                string[] arrSqlFiles = System.IO.Directory.GetFiles(strSavePath, strSearchPattern);
                if (arrSqlFiles != null && arrSqlFiles.Length > 0)
                {
                    string strUrl = Helper.GetAppSettings("MemberService");
                    string InterfaceUrl = strUrl.TrimEnd('/') + "/upload/ReceiveBusdata.ashx";
                    bool Flag = false;
                    string fullname = string.Empty;
                    for (int i = 0; i < arrSqlFiles.Length; i++)
                    {
                        fullname = arrSqlFiles[i];
                        Flag = Upload_Request(InterfaceUrl, fullname, fullname.Substring(fullname.LastIndexOf("\\") + 1));
                        if (Flag)
                        {
                            //提交远程服务成功
                            System.IO.File.Delete(arrSqlFiles[i]);
                            ErrorLog.WriteErrorMessage("已上传成功：" + arrSqlFiles[i]);
                        }
                        else
                        {
                            ErrorLog.WriteErrorMessage("上传失败：" + arrSqlFiles[i]);
                        }
                    }
                }
                else
                {
                    blnResult = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex);
            }
            return blnResult;
        }
    }
}
