using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XJWZCatering.BLL;
using XJWZCatering.CommonBasic;
using XJWZCatering.Model;
using XJWZCatering.SQL;
using XJWZCatering.WServices;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using XJWZCatering.LinkPubWx;
using System.IO;
using Newtonsoft.Json.Linq;

namespace XJWZCatering.WServer.WxServer
{
    /// <summary>
    /// Dishes 的摘要说明
    /// </summary>
    public class Dishes : ServiceBase
    {
        operatelogEntity logentity = new operatelogEntity();
        bllDisheType blldt = new bllDisheType(); //菜品类别表
        blldishes bll = new blldishes();   //菜品详情表
        string sql = string.Empty;
        DataTable dt = new DataTable();
        string imgurl = Helper.GetAppSettings("imgUrl");

        public override void ProcessRequest(HttpContext context)
        {
            if (CheckParameters(context))//检测是否合法
            {
                Dictionary<string, object> dicPar = GetParameters();
                if (dicPar != null)
                {
                    logentity.module = "菜品列表";
                    switch (actionname.ToLower())
                    {
                        case "dishetype":
                            GetdishesTypeList(dicPar);
                            break;
                        case "dishesdetails":
                            DishesDetails(dicPar);
                            break;
                        case "orderdishes":
                            order(dicPar);
                            break;
                        case "orderdetails":
                            orderDetails(dicPar);
                            break;
                        case "againorder":
                            againOrder(dicPar);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 获取菜单分类及菜单明细
        /// </summary>
        /// <param name="dicPar"></param>
        public void GetdishesTypeList(Dictionary<string, object> dicPar)
        {
            ///要检测的参数信息
            List<string> pra = new List<string>() { "GUID", "USER_ID", "buscode", "stocode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            var guid = dicPar["GUID"].ToString();
            var userid = dicPar["USER_ID"].ToString();
            var buscode = dicPar["buscode"].ToString();
            var stocode = dicPar["stocode"].ToString();

            var strJson = GetMenu(buscode, stocode, "", imgurl);
            ToJsonStr(strJson);
        }

        /// <summary>
        /// 获取菜单明细
        /// </summary>
        /// <param name="buscode"></param>
        /// <param name="stocode"></param>
        /// <param name="strwhere"></param>
        /// <returns></returns>
        private static string GetMenu(string buscode, string stocode, string strwhere, string imgurl)
        {
            var strJson = string.Empty;
            DataSet ds = new bllPaging().GetDataSetInfoBySQL("select pdistypecode,distypecode,distypename FROM [DisheType] where stocode='" + stocode + "' and status='1' and isdelete='0'; SELECT '1' AS selloff,d.discode,d.disname,d.price,d.unit,'" + imgurl + "'+dg.dispath as dispicture,d.iscombo,d.distypecode,d.stocode,dbo.fngetisDisMet(d.discode,d.stocode) as ismethods FROM dishes d left join disgx dg on d.stocode=dg.stocode and d.discode=dg.discode where d.stocode='" + stocode + "' and d.status='1' and d.isdelete='0'  AND dg.isXS=1;");

            if (ds.Tables.Count == 2)
            {
                var dishetypeDt = ds.Tables[0];  //菜品类别
                var dishesDt = ds.Tables[1];   //菜品详情
                var sqdiscode = GetSoldOut(stocode);
                Log.WriteLog("", "sqdiscode", sqdiscode);
                if (sqdiscode != "1")
                {
                    try
                    {
                        var udishes = dishesDt.Select(" discode in('" + sqdiscode.Replace(",", "','") + "')"); //待修改的菜品
                        foreach (var rows in udishes)
                        {
                            rows["selloff"] = "0";
                        }
                    }
                    catch (Exception ex)
                    {
                        //Log.WriteLog("", "Ex", ex.Message);
                    }

                }

                var pdt = dishetypeDt.Select(" pdistypecode=''");//菜品的大类别
                strJson = "[";
                for (int i = 0; i < pdt.Count(); i++)
                {
                    strJson += "{\"pdistype\":\"" + pdt[i]["distypename"].ToString() + "\",\"distype\":[";
                    var sdt = dishetypeDt.Select(" pdistypecode='" + pdt[i]["distypecode"].ToString() + "'"); //菜品小类别
                    for (int j = 0; j < sdt.Count(); j++)
                    {
                        //获取菜单
                        var distypecode = sdt[j]["distypecode"].ToString();
                        var dishdt = dishesDt.Select(" distypecode='" + distypecode + "' and stocode='" + stocode + "'");
                        //如果二级分类没有数据，则不显示
                        if (dishdt.Count() > 0)
                        {
                            strJson += "{\"disNo\":\"" + distypecode + "\",\"dName\":\"" + sdt[j]["distypename"].ToString() + "\",\"dishlist\":[";
                            for (int h = 0; h < dishdt.Count(); h++) //循环行
                            {
                                strJson += "{";
                                for (int c = 0; c < dishdt[h].Table.Columns.Count; c++) //循环列名，用来拼接json
                                {
                                    var ColumnName = dishdt[h].Table.Columns[c].ColumnName;
                                    strJson += string.Format("\"{0}\":\"{1}\",", ColumnName, dishdt[h][ColumnName]);
                                }
                                strJson = strJson.TrimEnd(',');
                                strJson += "},";

                            }
                            strJson = strJson.TrimEnd(',');
                            strJson += "]},";
                        }
                    }
                    strJson = strJson.TrimEnd(',');
                    strJson += "]},";
                }

                strJson = strJson.TrimEnd(',');
                strJson += "]";

            }

            return strJson;
        }

        /// <summary>
        /// 菜品详细（口味，配料，做法等）
        /// </summary>
        /// <param name="dicPar"></param>
        public void DishesDetails(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "discode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            var discode = dicPar["discode"].ToString();
            var stocode = dicPar["stocode"].ToString();
            sql = "SELECT d.discode,d.disname,d.price,d.unit,'" + imgurl + "'+dg.dispath as dispicture,d.iscombo,d.isneedmethod FROM dishes d left join disgx dg on d.stocode=dg.stocode and d.discode=dg.discode where d.discode='" + discode + "' and d.stocode='" + stocode + "'";
            dt = bll.getDataTableBySql(sql);
            var strJson = string.Empty;

            if (dt.Rows.Count > 0)
            {
                var iscombo = Convert.ToString(dt.Rows[0]["iscombo"]);

                if (iscombo == "1")//如果是组合套餐
                {
                    strJson = ComboDishe(discode, stocode);
                }
                else
                {

                    strJson = "[{";

                    strJson += dtToJson(dt, "", "0");

                    //以下是做法及做法加价
                    sql = string.Format(@"select raiseamount,dm.methodcode,methodname,(select mettypename from MethodType where mettypecode=m.mettypecode and stocode='{1}') as mtype from DishesMethods as dm
  inner join methods as m on dm.methodcode=m.methodcode
  where discode='{0}' and m.stocode='{1}' and dm.stocode='{1}'", dt.Rows[0]["discode"].ToString(), stocode);
                    var dishdt = SQLTool.ExecuteDataTable(sql);
                    if (dishdt.Rows.Count > 0)
                    {
                        strJson += ",\"dismname\":\"" + dishdt.Rows[0]["mtype"].ToString() + "\",\"dishesmethods\":[";
                        for (int i = 0; i < dishdt.Rows.Count; i++)
                        {
                            strJson += "{\"price\":\"" + dishdt.Rows[i]["raiseamount"] + "\",\"methodcode\":\"" + dishdt.Rows[i]["methodcode"] + "\",\"methodname\":\"" + dishdt.Rows[i]["methodname"] + "\"},";
                        }
                        strJson = strJson.TrimEnd(',');
                        strJson += "]";
                    }


                    strJson += "}]";
                }
            }

            ToJsonStr(strJson);
        }

        /// <summary>
        /// 套餐详情
        /// </summary>
        /// <param name="discode"></param>
        /// <param name="stocode"></param>
        /// <returns></returns>
        public string ComboDishe(string discode, string stocode)
        {
            var strJson = string.Empty;

            sql = string.Format(@"
select stuff((select top 1 ','+d.disname,','+'{2}'+dg.dispath,','+CONVERT(varchar,d.price) from dishes d left join disgx dg on d.stocode=dg.stocode and d.discode=dg.discode where d.discode='{0}' and d.stocode='{1}' for xml path('')),1,1,'');
select disname,discode,price from dishes where discode in(select usediscode from dishescombo where discode='{0}' and stocode='{1}') and stocode='{1}';
select allowkinds,allowcount,allowamount,usediscode,usedisdefaultamount,usedismaxamount,comgcode from dishesoptional where  discode='{0}' and stocode='{1}';
select comgcode,comgname from combogroup where comgcode in(select comgcode from dishesoptional where discode='{0}' and stocode='{1}' group by comgcode) and stocode='{1}' order by busSort;
select dis.discode,usediscode,disname,unit,ds.price,'{2}'+dg.dispath as dispicture,distypecode,dbo.fngetisDisMet(usediscode,dis.stocode) as ismethods,comgcode from dishesoptional as dis inner join dishes as ds on dis.usediscode=ds.discode and dis.stocode=ds.stocode left join disgx as dg on ds.discode=dg.discode and ds.stocode=dg.stocode where dis.discode='{0}' and dis.stocode='{1}' and ds.stocode='{1}'", discode, stocode, imgurl);

            DataSet ds = new bllPaging().GetDataSetInfoBySQL(sql);

            if (ds.Tables.Count > 0)
            {
                // 套餐名及套餐图片
                var dishinfoDt = ds.Tables[0];
                var dishinfo = dishinfoDt.Rows[0][0].ToString();
                strJson = "{\"img\":\"" + dishinfo.Split(',')[1] + "\",\"name\":\"" + dishinfo.Split(',')[0] + "\",\"discode\":\"" + discode + "\",\"price\":\"" + dishinfo.Split(',')[2] + "\",";

                //套餐必选菜
                var bxDt = ds.Tables[1];

                if (bxDt.Rows.Count > 0)
                {
                    strJson += "\"mustfood\":[";
                    strJson += dtToJson(bxDt, "", "1");
                    strJson += "],";
                }
                //套餐可选菜
                var comboDt = ds.Tables[2];
                if (comboDt.Rows.Count > 0)
                {
                    strJson += "\"combodetails\":[";
                    var disttypedt = ds.Tables[3]; //套餐可选菜类别
                    var combodishe = ds.Tables[4];//套餐可选菜详情

                    for (int i = 0; i < disttypedt.Rows.Count; i++)
                    {
                        var combodiscode = disttypedt.Rows[i]["comgcode"].ToString(); //套餐类别编号

                        var combozs = comboDt.Select(" comgcode='" + combodiscode + "'"); //套餐可选数量及种数等

                        strJson += "{\"allowkinds\":\"" + combozs[0]["allowkinds"].ToString() + "\",\"allowcount\":\"" + combozs[0]["allowcount"].ToString() + "\",\"disNo\":\"" + combodiscode + "\",\"dName\":\"" + disttypedt.Rows[i]["comgname"].ToString() + "\",\"dishlist\":["; //类别

                        var combodisheInfo = combodishe.Select(" comgcode='" + combodiscode + "'"); //类别下的菜品
                        foreach (var dishe in combodisheInfo) //循环菜品
                        {
                            strJson += "{";
                            var comboinfo = comboDt.Select(" usediscode='" + dishe["usediscode"] + "'"); //可选菜品详情
                            strJson += "\"usedisdefaultamount\":\"" + comboinfo[0]["usedisdefaultamount"] + "\",\"usedismaxamount\":\"" + comboinfo[0]["usedismaxamount"] + "\",";
                            for (int h = 0; h < dishe.Table.Columns.Count; h++)
                            {
                                var ColumnName = dishe.Table.Columns[h].ColumnName;
                                if (ColumnName == "comgcode")
                                    break;
                                strJson += string.Format("\"{0}\":\"{1}\",", ColumnName, dishe[ColumnName]);
                            }
                            strJson = strJson.TrimEnd(',');
                            strJson += "},";
                        }
                        strJson = strJson.TrimEnd(',');
                        strJson += "]},";
                    }
                    strJson = strJson.TrimEnd(',');
                    strJson += "]";
                }
                else
                {
                    strJson = strJson.Trim(',');
                }

                strJson += "}";
            }

            return strJson;
        }

        /// <summary>
        /// 用户下单
        /// </summary>
        /// <param name="dicPar"></param>
        /// <returns></returns>
        public void order(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "GUID", "USER_ID", "stocode", "strJson" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            var stocode = JsonConvert.SerializeObject(dicPar["stocode"]);
            var strJson = JsonConvert.SerializeObject(dicPar["strJson"]);

            bllchoorder order = new bllchoorder(); //桌台点餐
            bllchoorderdetail odetail = new bllchoorderdetail(); //餐收_点餐订单
            bllchoorderdishes odishes = new bllchoorderdishes(); //餐收_点餐菜品
            OrderInfo oi = new OrderInfo();
            try
            {
                oi = JsonConvert.DeserializeObject<OrderInfo>(strJson);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex.ToString());
            }

            var sresult = string.Empty;
            var sqresult = GetSoldOut(stocode);
            if (sqresult != "1")
            {
                for (int i = 0; i < oi.dpdishes.Count; i++)
                {
                    if (sqresult.Contains(oi.dpdishes[i].discode))
                    {
                        sresult += oi.dpdishes[i].discode + ",";
                    }
                }
                if (!string.IsNullOrEmpty(sresult))
                {
                    sresult = sresult.TrimEnd(',');
                    var result = SQL.XJWZSQLTool.GetFirststringField("SELECT STUFF((SELECT ','+disname FROM dbo.dishes WHERE discode IN('" + sresult.Replace(",", "','") + "') AND stocode='" + stocode.Trim('"') + "' FOR XML PATH('')),1,1,'') ");
                    ToCustomerJson("1", "菜品【" + result + "】已售罄");
                    return;
                }
            }

            var sy = IsSQ(oi);
            if (sy != "1")
            {
                ToCustomerJson("1", sy);
                return;
            }

            //查询用户当日的预约信息，如果有则填入桌台点餐表  status（0未到店，1已到店，2已离店，3已取消，4未处理，5已逾期）
            var reserveInfo = string.Empty;
            try
            {
                sql = "SELECT STUFF((SELECT TOP 1 '|'+userName,'|'+tel,'|'+ CONVERT(VARCHAR,desDate+desTime,120),'|'+CONVERT(VARCHAR,ctime,120) FROM WX_busDestine WHERE openid='" + oi.openid + "' AND DATEDIFF(MONTH,GETDATE(),desDate)=-1 AND stocode='" + oi.stocode + "' and status=0 FOR XML PATH('')),1,1,'')";
                reserveInfo = Convert.ToString(SQLTool.ExecuteScalar(sql));
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex.ToString());
            }

            string uname = string.Empty, tel = string.Empty, rtime = string.Empty, ctime = string.Empty, cuser = string.Empty, cname = string.Empty;
            cuser = "433"; //创建人  暂时写死 sto_admins表里创建的微信点餐员
            cname = "微信点餐员";
            if (!string.IsNullOrEmpty(reserveInfo))
            {
                var rDetail = reserveInfo.Split('|');
                uname = rDetail[0];  //预约人
                tel = rDetail[1];   //预约人电话
                rtime = rDetail[2]; //预约到店时间
                ctime = rDetail[3]; //预约时间
            }

            var shiftid = 415; //班次id
            var tguid = Guid.NewGuid();

            try
            {
                string choorderTname = "WX_choorder", choorderdetailTname = "WX_choorderdetail", choorderdishesTname = "WX_choorderdishes";
                string orderidstr = "", detailidstr = "", orderdishesidstr = "", p_orderid = "", p_detailid = "", p_orderdishesid = "";

                if (oi.ptype == "0")
                {
                    var pInfo = SQLTool.ExecuteDataTable("SELECT count(cid) from choorderhistory UNION  ALL SELECT count(cid) from choorderdetailhistory UNION ALL SELECT count(cid) from choorderdisheshistory");

                    orderidstr = "orderid,"; detailidstr = "detailid,"; orderdishesidstr = "orderdishesid,";
                    choorderTname = "choorderhistory"; choorderdetailTname = "choorderdetailhistory"; choorderdishesTname = "choorderdisheshistory";

                    p_orderid = "" + 10000000 + Convert.ToInt32(pInfo.Rows[0][0]) + ",";
                    p_detailid = "" + 10000000 + Convert.ToInt32(pInfo.Rows[1][0]) + ",";
                    p_orderdishesid = "" + 10000000 + Convert.ToInt32(pInfo.Rows[2][0]) + ",";

                    sql = string.Empty;
                }

                //插入桌台点餐表
                var ordersql = @"INSERT [dbo]." + choorderTname + " (" + orderidstr + "[buscode], [strcode], [shiftid], [tmcode], [personnum], [username], [userphone], [arrivetime], [opentime], [restime], [checkouttime], [gusetleavetime], [alltime], [allfoodtime], [conmoney], [status], [remark], [cuser], [ctime], [uuser], [utime], [isdelete]) VALUES (" + p_orderid + "N'88888888', '" + oi.stocode + "', " + shiftid + ", '" + oi.tmcode + "', " + oi.personnum + ", '" + uname + "', '" + tel + "', '" + rtime + "',GETDATE(), GETDATE(), '" + ctime + "', '1900-01-01 00:00:00.000', 0,'1900-01-01 00:00:00.000', '" + oi.discountprice + "', N'1', N'" + oi.remark + "', '" + cuser + "', GETDATE(), '" + cuser + "',GETDATE(), N' ');select SCOPE_IDENTITY();";
                var orderid = Convert.ToString(SQLTool.ExecuteScalar(ordersql));
                if (oi.ptype == "0")
                {
                    orderid = p_orderid.TrimEnd(',');
                }

                #region  插入菜品数据
                var metname = "";   //餐别名称
                var metcode = "";   //餐别编号

                var detailcode = Convert.ToString(SQLTool.ExecuteScalar("SELECT dbo.f_GetChoorderNo()"));

                var detailsql = "INSERT [dbo]." + choorderdetailTname + " (" + detailidstr + "[buscode], [stocode], [orderid], [personcount], [opentime], [checkouttime], [alltime], [endtime], [shiftid], [tmcode], [tmname], [tablecodes], [combinenum], [allfoodtime], [pushfoodtime], [pushfoodstate], [closeaccount], [closeCodes], [calltime], [userid], [ordertime], [surchargeamount], [ocostprice], [conmoney], [status], [detailcode], [memcode], [desCode], [desctime], [printcount], [remark], [metname], [pushid], [pushname], [cuser], [ctime], [ispresented], [presentedcode], [cardcode], [mobile], [coupons], [metcode], [ttcode], [customer], [amanagerid], [amanagername], [olossmoney], [detailtype], [predishemoney], [TerminalType], [depcode], [depname]) VALUES (" + p_detailid + "N'88888888', '" + oi.stocode + "', '" + orderid + "',  " + oi.personnum + ", GETDATE(), GETDATE(), 0,'1900-01-01 00:00:00.000', " + shiftid + ",'" + oi.tmcode + "', '" + oi.tmname + "', '', 0, '1900-01-01 00:00:00.000','1900-01-01 00:00:00.000', N' ', N' ', N'', '1900-01-01 00:00:00.000','" + cuser + "', GETDATE(), '" + oi.surchargeamount + "', 0.000, '" + oi.discountprice + "', 1, '" + detailcode + "', N'', N'', '1900-01-01 00:00:00.000', 0, '" + oi.remark + "', '" + metname + "', N'', N'', '" + cuser + "',GETDATE(), '0', N'', N'', N'', N'', '" + metcode + "', '" + oi.ttcode + "', N'', N'', N'', 0.00, N'0', 0.00, N'1', '', '');select SCOPE_IDENTITY();";
                var detailid = Convert.ToString(SQLTool.ExecuteScalar(detailsql));
                if (oi.ptype == "0")
                {
                    detailid = p_detailid.TrimEnd(',');
                }

                //插入套餐
                addCombo(oi, orderid, detailid, detailcode, cuser, tguid);

                //插入单品
                var disnumconst = "0"; // 原数量（套餐标配可选餐品计算数量使用，若是套餐主品则记录是否没有子菜品，1没有，0有） ？？？
                var dishessql = string.Empty;
                for (int i = 0; i < oi.dpdishes.Count; i++)
                {
                    var disInfo = oi.dpdishes[i];
                    var allmoney = disInfo.disnum * disInfo.oneprice;
                    var guid = Guid.NewGuid();

                    var dishesinfo = getDeshesInfo(oi.stocode, disInfo.discode);

                    dishessql += @"INSERT INTO	" + choorderdishesTname + @"(" + orderdishesidstr + @"buscode, stocode, orderid, detailid, distypecode, dtypecode, melcode, discode, disname, disothername, disnum, disnumconst, addnum, isneedmethod, distacode, distaname, ordertime, addtime, calltime, pushfoodtime, pushfoodstate, isentity, entitydefcount, entityprice, singlenum, singleAllmoney, totaladdmoney, totaladdmoneydiscount, allmoney, allmoneydiscount, memberallmoney, resultallmoney, packageaddmoney, ispackage, iscanout, isout, refundNum, refundaddnum, oneprice, memberprice, costprice, methodmoney, methodmoneydiscount, attachmoney, pushmoney, ismember, ispre, pretype, dispcode, discountratemax, discountrate, premoney, precheck, checktime, ismustconsume, mustconsumenum, iscaninventory, isallowmemberprice, isattachcalculate, isclipcoupons, isnonoperating, iscombooptional, isneedweigh, iscanmodifyprice, matcode, cguid, pcguid, porderdishesid, comdiscode, comgcode, composetype, allowkinds, allowcount, allowamount, usedisdefaultamount, usedismaxamount, unit, extcode, fincode, dcode, kitcode, ecode, warcode, totmcode, totmname, todetailid, status, makestatus, operaretype, isdiscount, priceispre, ispresented, pecode, pename, prereason, prereasontype, remark, orderremark, gguid, nopreremark, storeupdated, cuser, ctime, comprice, cusername, tottcode, metname, metcode, detailcode, tmopentime, chomac, orderno, subtype)
VALUES(
" + p_orderdishesid + @"
    '88888888',                              -- buscode - varchar(16)
    '" + oi.stocode + @"',                   -- stocode - varchar(8)
    '" + orderid + @"',                       -- orderid - bigint
    '" + detailid + @"',                       -- detailid - bigint
    '" + Convert.ToString(dishesinfo.Rows[0]["pdistypecode"]) + @"',               -- distypecode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["distypecode"]) + @"',           -- dtypecode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["melcode"]) + @"',           -- melcode - varchar(16)
    '" + disInfo.discode + @"',                -- discode - varchar(16)
    N'" + disInfo.disname + @"',                 -- disname - nvarchar(32)
    '',                                        -- disothername - varchar(128)
    '" + disInfo.disnum + @"',                -- disnum - decimal(18, 2)
    '" + disnumconst + @"',                     -- disnumconst - decimal(18, 2)
    '0.00',                                       -- addnum - decimal(18, 2)
    '" + Convert.ToString(dishesinfo.Rows[0]["isneedmethod"]) + @"',              -- isneedmethod - char(1)
    '" + disInfo.method[0].methodcode + @"',        -- distacode - varchar(256)
    '" + disInfo.method[0].methodname + @"',        -- distaname - varchar(256)
    GETDATE(),                                       -- ordertime - datetime
    GETDATE(),                                       -- addtime - datetime
    GETDATE(),                                          -- calltime - datetime
    GETDATE(),                                      -- pushfoodtime - datetime
    0,                                                -- pushfoodstate - int
    '',                                                   -- isentity - char(1)
    0,                                               -- entitydefcount - int
    NULL,                                            -- entityprice - decimal(18, 2)
    0,                                                      -- singlenum - int
    NULL,                                            -- singleAllmoney - decimal(18, 2)
    NULL,                                                -- totaladdmoney - decimal(18, 2)
    NULL,                                                -- totaladdmoneydiscount - decimal(18, 2)
    '" + allmoney + @"',                                 -- allmoney - decimal(18, 2)
    '" + allmoney + @"',                                -- allmoneydiscount - decimal(18, 2)
    '" + allmoney + @"',                                -- memberallmoney - decimal(18, 2)
    NULL,                                             -- resultallmoney - decimal(18, 2)
    NULL,                                               -- packageaddmoney - decimal(18, 2)
    '',                                                 -- ispackage - char(1)
    '',                                                     -- iscanout - char(1)
    '',                                                 -- isout - char(1)
    NULL,                                                 -- refundNum - decimal(18, 2)
    NULL,                                                     -- refundaddnum - decimal(18, 2)
    '" + disInfo.oneprice + @"',                         -- oneprice - decimal(18, 2)
    '" + Convert.ToString(dishesinfo.Rows[0]["memberprice"]) + @"',                      -- memberprice - decimal(18, 2)
    NULL,      -- costprice - decimal(18, 2)
    '" + disInfo.method[0].methodadd + @"',      -- methodmoney - decimal(18, 2)
    '" + disInfo.method[0].methodadd + @"',      -- methodmoneydiscount - decimal(18, 2)
    NULL,      -- attachmoney - decimal(18, 2)
    NULL,      -- pushmoney - decimal(18, 2)
    '',        -- ismember - char(1)
    '',        -- ispre - char(1)
    '',        -- pretype - char(1)
    '',        -- dispcode - varchar(16)
    NULL,      -- discountratemax - decimal(18, 2)
    '-1',      -- discountrate - decimal(18, 2)
    NULL,      -- premoney - decimal(18, 2)
    N'',       -- precheck - nvarchar(32)
    GETDATE(), -- checktime - datetime
    '',        -- ismustconsume - char(1)
    NULL,      -- mustconsumenum - decimal(18, 2)
    '" + Convert.ToString(dishesinfo.Rows[0]["iscaninventory"]) + @"',        -- iscaninventory - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["isallowmemberprice"]) + @"',        -- isallowmemberprice - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["isattachcalculate"]) + @"',        -- isattachcalculate - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["isclipcoupons"]) + @"',        -- isclipcoupons - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["isnonoperating"]) + @"',        -- isnonoperating - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["iscombooptional"]) + @"',        -- iscombooptional - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["isneedweigh"]) + @"',        -- isneedweigh - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["iscanmodifyprice"]) + @"',        -- iscanmodifyprice - char(1)
    '',        -- matcode - varchar(16)
    '" + guid + @"',        -- cguid - varchar(64)
    '',        -- pcguid - varchar(64)
    0,         -- porderdishesid - bigint
    '',        -- comdiscode - varchar(16)
    '',        -- comgcode - varchar(16)
    '',        -- composetype - varchar(1)
    NULL,      -- allowkinds - decimal(18, 2)
    NULL,      -- allowcount - decimal(18, 2)
    NULL,      -- allowamount - decimal(18, 2)
    NULL,      -- usedisdefaultamount - decimal(18, 2)
    NULL,      -- usedismaxamount - decimal(18, 2)
    '" + Convert.ToString(dishesinfo.Rows[0]["unit"]) + @"',        -- unit - varchar(8)
    '',        -- extcode - varchar(64)
    '" + Convert.ToString(dishesinfo.Rows[0]["fincode"]) + @"',        -- fincode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["dcode"]) + @"',        -- dcode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["kitcode"]) + @"',        -- kitcode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["ecode"]) + @"',        -- ecode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["warcode"]) + @"',        -- warcode - varchar(16)
    '" + oi.tmcode + @"',        -- totmcode - varchar(8)
    N'" + (oi.ttname + "—" + oi.tmname) + @"',       -- totmname - nvarchar(32)
    '" + detailid + @"',         -- todetailid - bigint
    '1',        -- status - char(1)
    '1',        -- makestatus - char(1)
    '0',        -- operaretype - char(1)
    '0',        -- isdiscount - char(1)
    '0',        -- priceispre - char(1)
    '0',        -- ispresented - char(1)
    '',        -- pecode - varchar(16)
    N'',       -- pename - nvarchar(32)
    N'',       -- prereason - nvarchar(128)
    N'',       -- prereasontype - nvarchar(32)
    N'',       -- remark - nvarchar(128)
    N'" + oi.remark + @"',       -- orderremark - nvarchar(128)
    '" + tguid + @"',        -- gguid - varchar(64)
    N'',       -- nopreremark - nvarchar(128)
    '0',        -- storeupdated - char(1)
    '" + cuser + @"',         -- cuser - bigint
    GETDATE(), -- ctime - datetime
    '" + disInfo.oneprice + @"',      -- comprice - decimal(18, 2)
    N'微信管理员',       -- cusername - nvarchar(32)
    '" + oi.ttcode + @"',        -- tottcode - varchar(16)
    '" + metname + @"',        -- metname - varchar(16)
    '" + metcode + @"',        -- metcode - varchar(16)
    '" + detailcode + @"',        -- detailcode - varchar(32)
    GETDATE(), -- tmopentime - datetime
    '',        -- chomac - varchar(64)
    '',        -- orderno - varchar(32)
    ''         -- subtype - char(1)
    );";
                }
                #endregion

                if (!string.IsNullOrEmpty(dishessql))
                    SQLTool.ExecuteNonQuery(dishessql);


                //插入订单和用户关系表  
                var odSql = "INSERT INTO dbo.WX_orderdetails(memcode,source,buscode,stocode,openid,orderno,payType,status,couname,checkcode,singlemoney,cardCode,privilegepre,iseCard,sumprice,postJson,discountprice,remake) VALUES ('','wechat','88888888','" + oi.stocode + "','" + oi.openid + "','" + detailcode + "','" + oi.ptype + "','0','" + oi.couname + "','" + oi.checkcode + "','" + oi.singlemoney + "','" + oi.cardcode + "','" + oi.cschemediscmoney + "','" + oi.isecard + "','" + oi.sumprice + "','" + JsonConvert.SerializeObject(dicPar["strJson"]) + "'," + oi.discountprice + ",'" + oi.remark + "');";
                SQLTool.ExecuteNonQuery(odSql);

                //插入支付信息
                paySet(oi, detailcode, shiftid, cuser, cname, orderid, detailid);

                ToCustomerJson("0", detailcode);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex.ToString());
                ToCustomerJson("2", "网络繁忙，请稍后重试！");
            }
        }

        /// <summary>
        /// 微信支付表
        /// </summary>
        /// <param name="oi"></param>
        /// <param name="detailcode"></param>
        /// <param name="shiftid"></param>
        /// <param name="cuser"></param>
        /// <param name="cname"></param>
        /// <param name="orderid"></param>
        /// <param name="detailid"></param>
        public void paySet(OrderInfo oi, string detailcode, int shiftid, string cuser, string cname, string orderid, string detailid)
        {

            //插入支付表
            var orderno = Convert.ToString(SQLTool.ExecuteScalar("SELECT dbo.f_GetChoorderNo()"));
            var dguid = Guid.NewGuid();
            var paySql = @"
INSERT INTO dbo.choorderdetailBreakhistory
(
detailbreakid,
    buscode,
    stocode,
    detailcodes,
    shiftid,
    ordertime,
    ordertimemax,
    orderno,
    dguid,
    ctmcodes,
    tablecodes,
    incomemoney,
    outsidemoney,
    disheattmoney,
    allattmoney,
    totalmoney,
    ordermonetary,
    topaymoney,
    discedmoney,
    ordertype,
    expendtype,
    pstatus,
    bnum,
    jfnum,
    jfmoney,
    yhqmoney,
    zkmoney,
    premoney,
    mlmoney,
    qtmoney,
    mlmoneytype,
    destinemoney,
    desmoney,
    desmoneyback,
    descodes,
    ispre,
    pretype,
    dispcode,
    disratemoney,
    cardCode,
    zdprecheckid,
    zdprecheck,
    zdchecktime,
    dcprecheckid,
    dcprecheck,
    dcchecktime,
    mlsqrid,
    mlsqrname,
    mlrtime,
    dksqrid,
    dksqrname,
    dktime,
    thentime,
    printcount,
    isprintinvoice,
    printinvtime,
    invtitle,
    invmoney,
    dyrid,
    dyrname,
    remark,
    cuser,
    cusername,
    ctime,
    utime,
    prereason,
    discreason,
    mlreason,
    backorderreason,
    backordernum,
    feemoney,
    cardbalance,
    mobile,
    prediscountrate,
    cschemetype,
    cschemediscountrate,
    cschemediscmoney,
    mempricediscmoney,
    cschemecheckid,
    cschemecheckname,
    cschemechecktime,
    isgivecoupon,
    conlossMoney,
    dispname,
    disccardCode,
    presdishemoney,
    comordrcount,
    ivamoney,
    cuponvamoney,
    postmac,
    mealname,
    persons,
    TerminalType,
    ExtBak1,
    ExtBak2,
    carddisctype,
    depcode,
    depname
)
VALUES
(
'0',
    '88888888',        -- buscode - varchar(16)
    '" + oi.stocode + @"',        -- stocode - varchar(8)
    '" + detailcode + @"',        -- detailcodes - varchar(max)
    '" + shiftid + @"',         -- shiftid - bigint
    GETDATE(), -- ordertime - datetime
    GETDATE(), -- ordertimemax - datetime
    '" + orderno + @"',        -- orderno - varchar(32)
    '" + dguid + @"',        -- dguid - varchar(64)
    '" + oi.tmcode + @"',        -- ctmcodes - varchar(max)
    '',        -- tablecodes - varchar(max)
    '" + oi.sumprice + @"',      -- incomemoney - decimal(18, 3)
    '0',      -- outsidemoney - decimal(18, 3)
    '" + oi.surchargeamount + @"',      -- disheattmoney - decimal(18, 3)
    '" + oi.surchargeamount + @"',      -- allattmoney - decimal(18, 3)
    '" + oi.sumprice + @"',      -- totalmoney - decimal(18, 3)
    '" + oi.sumprice + @"',      -- ordermonetary - decimal(18, 2)
     '" + oi.sumprice + @"',      -- topaymoney - decimal(18, 3)
    '" + (oi.singlemoney + oi.cschemediscmoney) + @"',      -- discedmoney - decimal(18, 2)
    '0',        -- ordertype - char(1)
    '1',        -- expendtype - char(1)
    '0',        -- pstatus - char(1)
    1,         -- bnum - int
    NULL,      -- jfnum - decimal(18, 2)
    NULL,      -- jfmoney - decimal(18, 2)
    '" + oi.singlemoney + @"',      -- yhqmoney - decimal(18, 2)
    '" + oi.cschemediscmoney + @"',      -- zkmoney - decimal(18, 2)
    NULL,      -- premoney - decimal(18, 2)
    NULL,      -- mlmoney - decimal(18, 2)
    NULL,      -- qtmoney - decimal(18, 2)
    '',        -- mlmoneytype - char(1)
    NULL,      -- destinemoney - decimal(18, 2)
    NULL,      -- desmoney - decimal(18, 2)
    NULL,      -- desmoneyback - decimal(18, 2)
    '',        -- descodes - varchar(5120)
    '',        -- ispre - char(1)
    '',        -- pretype - char(1)
    '" + oi.cardcode + @"',        -- dispcode - varchar(16)
    '" + (oi.singlemoney + oi.cschemediscmoney) + @"',      -- disratemoney - decimal(18, 2)
    '" + oi.cardcode + @"',        -- cardCode - varchar(16)
    0,         -- zdprecheckid - bigint
    N'',       -- zdprecheck - nvarchar(32)
    GETDATE(), -- zdchecktime - datetime
    0,         -- dcprecheckid - bigint
    N'',       -- dcprecheck - nvarchar(32)
    GETDATE(), -- dcchecktime - datetime
    0,         -- mlsqrid - bigint
    N'',       -- mlsqrname - nvarchar(32)
    GETDATE(), -- mlrtime - datetime
    0,         -- dksqrid - bigint
    N'',       -- dksqrname - nvarchar(32)
    GETDATE(), -- dktime - datetime
    GETDATE(), -- thentime - datetime
    0,         -- printcount - int
    '',        -- isprintinvoice - char(1)
    GETDATE(), -- printinvtime - datetime
    N'',       -- invtitle - nvarchar(128)
    NULL,      -- invmoney - decimal(18, 2)
    0,         -- dyrid - bigint
    N'',       -- dyrname - nvarchar(32)
    N'',       -- remark - nvarchar(128)
    '" + cuser + @"',         -- cuser - bigint
    '" + cname + @"',       -- cusername - nvarchar(32)
    GETDATE(), -- ctime - datetime
    GETDATE(), -- utime - datetime
    N'',       -- prereason - nvarchar(128)
    N'',       -- discreason - nvarchar(128)
    N'',       -- mlreason - nvarchar(128)
    N'',       -- backorderreason - nvarchar(1024)
    0,         -- backordernum - int
    NULL,      -- feemoney - decimal(18, 2)
    NULL,      -- cardbalance - decimal(18, 2)
    '',        -- mobile - varchar(32)
    NULL,      -- prediscountrate - decimal(18, 2)
    '4',        -- cschemetype - char(1)
    '" + oi.privilegepre + @"',      -- cschemediscountrate - decimal(18, 2)
    '" + oi.cschemediscmoney + @"',      -- cschemediscmoney - decimal(18, 2)
    NULL,      -- mempricediscmoney - decimal(18, 2)
    0,         -- cschemecheckid - bigint
    N'',       -- cschemecheckname - nvarchar(32)
    GETDATE(), -- cschemechecktime - datetime
    '',        -- isgivecoupon - char(1)
    NULL,      -- conlossMoney - decimal(18, 2)
    N'',       -- dispname - nvarchar(64)
    '" + oi.cardcode + @"',        -- disccardCode - varchar(16)
    NULL,      -- presdishemoney - decimal(18, 2)
    0,         -- comordrcount - int
    NULL,      -- ivamoney - decimal(18, 2)
    NULL,      -- cuponvamoney - decimal(18, 2)
    '',        -- postmac - varchar(64)
    N'',       -- mealname - nvarchar(16)
    0,         -- persons - int
    '',        -- TerminalType - varchar(2)
    '',        -- ExtBak1 - varchar(64)
    '',        -- ExtBak2 - varchar(64)
    '',        -- carddisctype - char(1)
    '',        -- depcode - varchar(32)
    N''        -- depname - nvarchar(32)
    );";

            var pcode = Convert.ToString(SQLTool.ExecuteScalar("SELECT dbo.f_GetPayOrderNo()")); //支付编号


            paySql += @"INSERT INTO dbo.chopayhistory
(
payid,
    orderid,
    detailid,
    detailcode,
    buscode,
    strcode,
    orderno,
    dguid,
    pcode,
    paytype,
    payname,
    paymoney,
    rmmoney,
    cashmoney,
    vamoney,
    vadesc,
    intcount,
    intmoney,
    intvamoney,
    couponmoney,
    accountcode,
    statustype,
    status,
    bstatus,
    pguid,
    bpguid,
    bcode,
    backSettlement,
    isinv,
    invmoney,
    useint,
    addint,
    remark,
    cuser,
    ctime,
    paytime,
    shiftid,
    ordertime,
    ordertimemax,
    olddetailcode,
    expendtype,
    qrcode,
    backuserid,
    backusername,
    backreason,
    backordernum,
    payer,
    cardbalance,
    cardlevel,
    cardlevelname,
    checkid,
    checker,
    descodes,
    cardtype,
    paypostmac,
    ThirdAccount,
    SerialNumber,
    bak1,
    notbackmoney
)
VALUES
( 
'0',
'" + orderid + @"',                     -- orderid - bigint
    '" + detailid + @"',                     -- detailid - bigint
    '" + detailcode + @"',                    -- detailcode - varchar(max)
    '88888888',                    -- buscode - varchar(16)
    '" + oi.stocode + @"',                    -- strcode - varchar(8)
    '" + orderno + @"',                    -- orderno - varchar(32)
    '" + dguid + @"',                    -- dguid - varchar(64)
    '" + pcode + @"',                    -- pcode - varchar(64)
    '1392',                    -- paytype - varchar(16)
    '微信',                    -- payname - varchar(64)
    '" + oi.discountprice + @"',                  -- paymoney - decimal(18, 3)
    '" + oi.discountprice + @"',                  -- rmmoney - decimal(18, 3)
    NULL,                  -- cashmoney - decimal(18, 2)
    NULL,                  -- vamoney - decimal(18, 3)
    N'',                   -- vadesc - nvarchar(32)
    0,                     -- intcount - int
    NULL,                  -- intmoney - decimal(18, 2)
    NULL,                  -- intvamoney - decimal(18, 2)
    '" + oi.singlemoney + @"',                  -- couponmoney - decimal(18, 2)
    '',                    -- accountcode - varchar(32)
    '0',                    -- statustype - char(1)
    '0',                    -- status - char(1)
    '',                    -- bstatus - char(1)
    '" + Guid.NewGuid() + @"',                    -- pguid - varchar(64)
    '',                    -- bpguid - varchar(64)
    '',                    -- bcode - varchar(64)
    '1',                    -- backSettlement - char(1)
    '1',                    -- isinv - char(1)
    '" + oi.sumprice + @"',                  -- invmoney - decimal(18, 3)
    0,                     -- useint - int
    '',                     -- addint - int
    N'',                   -- remark - nvarchar(128)
    '" + cuser + @"',                     -- cuser - bigint
    GETDATE(),             -- ctime - datetime
    GETDATE(), -- paytime - smalldatetime
    '" + shiftid + @"',                     -- shiftid - bigint
    GETDATE(),             -- ordertime - datetime
    GETDATE(),             -- ordertimemax - datetime
    '',                    -- olddetailcode - varchar(5120)
    '1',                    -- expendtype - char(1)
    '',                    -- qrcode - varchar(1024)
    0,                     -- backuserid - bigint
    N'',                   -- backusername - nvarchar(32)
    N'',                   -- backreason - nvarchar(128)
    0,                     -- backordernum - int
    '" + oi.openid + @"',                    -- payer - varchar(64)
    NULL,                  -- cardbalance - decimal(18, 2)
    '',                    -- cardlevel - varchar(64)
    N'',                   -- cardlevelname - nvarchar(64)
    0,                     -- checkid - bigint
    N'',                   -- checker - nvarchar(32)
    '',                    -- descodes - varchar(5120)
    '',                    -- cardtype - varchar(8)
    '',                    -- paypostmac - varchar(64)
    '',                    -- ThirdAccount - varchar(128)
    '',                    -- SerialNumber - varchar(128)
    '',                    -- bak1 - varchar(64)
    ''                     -- notbackmoney - char(1)
    )";

            try
            {
                SQLTool.ExecuteNonQuery(paySql);
            }
            catch (Exception e)
            {
                ErrorLog.WriteErrorMessage(e.ToString());
            }
        }

        /// <summary>
        /// 添加套餐
        /// </summary>
        /// <param name="oi"></param>
        /// <param name="orderid"></param>
        /// <param name="detailid"></param>
        /// <param name="detailcode"></param>
        /// <param name="cuser"></param>
        public void addCombo(OrderInfo oi, string orderid, string detailid, string detailcode, string cuser, System.Guid oguid)
        {

            var choorderdishesTname = "WX_choorderdishes";
            string orderdishesidstr = "", p_orderdishesid = "";
            if (oi.ptype == "0")
            {
                var pInfo = SQLTool.ExecuteDataTable("SELECT count(cid) from choorderhistory UNION  ALL SELECT count(cid) from choorderdetailhistory UNION ALL SELECT count(cid) from choorderdisheshistory");
                orderdishesidstr = "orderdishesid,";
                choorderdishesTname = "choorderdisheshistory";
                p_orderdishesid = 10000000 + Convert.ToInt32(pInfo.Rows[2][0]) + ",";

            }



            //插入单品
            var disnumconst = "0"; // 原数量（套餐标配可选餐品计算数量使用，若是套餐主品则记录是否没有子菜品，1没有，0有） ？？？
            var dishessql = string.Empty;
            for (int j = 0; j < oi.combodishes.Count; j++) //循环套餐
            {
                var comdisInfo = oi.combodishes[j];
                var allmoney = comdisInfo.cnum * comdisInfo.cprice;
                var cguid = Guid.NewGuid(); //套餐父id

                var comdishesinfo = getDeshesInfo(oi.stocode, comdisInfo.ccode);

                var comdishessql = @"INSERT INTO " + choorderdishesTname + @"(" + orderdishesidstr + @"buscode, stocode, orderid, detailid, distypecode, dtypecode, melcode, discode, disname, disothername, disnum, disnumconst, addnum, isneedmethod, distacode, distaname, ordertime, addtime, calltime, pushfoodtime, pushfoodstate, isentity, entitydefcount, entityprice, singlenum, singleAllmoney, totaladdmoney, totaladdmoneydiscount, allmoney, allmoneydiscount, memberallmoney, resultallmoney, packageaddmoney, ispackage, iscanout, isout, refundNum, refundaddnum, oneprice, memberprice, costprice, methodmoney, methodmoneydiscount, attachmoney, pushmoney, ismember, ispre, pretype, dispcode, discountratemax, discountrate, premoney, precheck, checktime, ismustconsume, mustconsumenum, iscaninventory, isallowmemberprice, isattachcalculate, isclipcoupons, isnonoperating, iscombooptional, isneedweigh, iscanmodifyprice, matcode, cguid, pcguid, porderdishesid, comdiscode, comgcode, composetype, allowkinds, allowcount, allowamount, usedisdefaultamount, usedismaxamount, unit, extcode, fincode, dcode, kitcode, ecode, warcode, totmcode, totmname, todetailid, status, makestatus, operaretype, isdiscount, priceispre, ispresented, pecode, pename, prereason, prereasontype, remark, orderremark, gguid, nopreremark, storeupdated, cuser, ctime, comprice, cusername, tottcode, metname, metcode, detailcode, tmopentime, chomac, orderno, subtype)
VALUES(
" + p_orderdishesid + @"
    '88888888',                              -- buscode - varchar(16)
    '" + oi.stocode + @"',                   -- stocode - varchar(8)
    '" + orderid + @"',                       -- orderid - bigint
    '" + detailid + @"',                       -- detailid - bigint
    '" + Convert.ToString(comdishesinfo.Rows[0]["pdistypecode"]) + @"',               -- distypecode - varchar(16)
    '" + Convert.ToString(comdishesinfo.Rows[0]["distypecode"]) + @"',           -- dtypecode - varchar(16)
    '" + Convert.ToString(comdishesinfo.Rows[0]["melcode"]) + @"',           -- melcode - varchar(16)
    '" + comdisInfo.ccode + @"',                -- discode - varchar(16)
    N'" + comdisInfo.cname + @"',                 -- disname - nvarchar(32)
    '',                                        -- disothername - varchar(128)
    '" + comdisInfo.cnum + @"',                -- disnum - decimal(18, 2)
    '" + disnumconst + @"',                     -- disnumconst - decimal(18, 2)
    '0.00',                                       -- addnum - decimal(18, 2)
    '" + Convert.ToString(comdishesinfo.Rows[0]["isneedmethod"]) + @"',              -- isneedmethod - char(1)
    '',        -- distacode - varchar(256)
    '',        -- distaname - varchar(256)
    GETDATE(),                                       -- ordertime - datetime
    GETDATE(),                                       -- addtime - datetime
    GETDATE(),                                          -- calltime - datetime
    GETDATE(),                                      -- pushfoodtime - datetime
    0,                                                -- pushfoodstate - int
    '',                                                   -- isentity - char(1)
    0,                                               -- entitydefcount - int
    NULL,                                            -- entityprice - decimal(18, 2)
    0,                                                      -- singlenum - int
    NULL,                                            -- singleAllmoney - decimal(18, 2)
    NULL,                                                -- totaladdmoney - decimal(18, 2)
    NULL,                                                -- totaladdmoneydiscount - decimal(18, 2)
    '" + allmoney + @"',                                 -- allmoney - decimal(18, 2)
    '" + allmoney + @"',                                -- allmoneydiscount - decimal(18, 2)
    '" + allmoney + @"',                                -- memberallmoney - decimal(18, 2)
    NULL,                                             -- resultallmoney - decimal(18, 2)
    '" + comdisInfo.packageaddmoney + @"',               -- packageaddmoney - decimal(18, 2)
    '1',                                                 -- ispackage - char(1)
    '',                                                     -- iscanout - char(1)
    '',                                                 -- isout - char(1)
    NULL,                                                 -- refundNum - decimal(18, 2)
    NULL,                                                     -- refundaddnum - decimal(18, 2)
    '" + comdisInfo.cprice + @"',                         -- oneprice - decimal(18, 2)
    '" + Convert.ToString(comdishesinfo.Rows[0]["memberprice"]) + @"',                      -- memberprice - decimal(18, 2)
    NULL,      -- costprice - decimal(18, 2)
    NULL,      -- methodmoney - decimal(18, 2)
    NULL,      -- methodmoneydiscount - decimal(18, 2)
    NULL,      -- attachmoney - decimal(18, 2)
    NULL,      -- pushmoney - decimal(18, 2)
    '',        -- ismember - char(1)
    '',        -- ispre - char(1)
    '',        -- pretype - char(1)
    '',        -- dispcode - varchar(16)
    NULL,      -- discountratemax - decimal(18, 2)
    '-1',      -- discountrate - decimal(18, 2)
    NULL,      -- premoney - decimal(18, 2)
    N'',       -- precheck - nvarchar(32)
    GETDATE(), -- checktime - datetime
    '',        -- ismustconsume - char(1)
    NULL,      -- mustconsumenum - decimal(18, 2)
    '" + Convert.ToString(comdishesinfo.Rows[0]["iscaninventory"]) + @"',        -- iscaninventory - char(1)
    '" + Convert.ToString(comdishesinfo.Rows[0]["isallowmemberprice"]) + @"',        -- isallowmemberprice - char(1)
    '" + Convert.ToString(comdishesinfo.Rows[0]["isattachcalculate"]) + @"',        -- isattachcalculate - char(1)
    '" + Convert.ToString(comdishesinfo.Rows[0]["isclipcoupons"]) + @"',        -- isclipcoupons - char(1)
    '" + Convert.ToString(comdishesinfo.Rows[0]["isnonoperating"]) + @"',        -- isnonoperating - char(1)
    '" + Convert.ToString(comdishesinfo.Rows[0]["iscombooptional"]) + @"',        -- iscombooptional - char(1)
    '" + Convert.ToString(comdishesinfo.Rows[0]["isneedweigh"]) + @"',        -- isneedweigh - char(1)
    '" + Convert.ToString(comdishesinfo.Rows[0]["iscanmodifyprice"]) + @"',        -- iscanmodifyprice - char(1)
    '',        -- matcode - varchar(16)
    '" + cguid + @"',        -- cguid - varchar(64)
    '',        -- pcguid - varchar(64)
    0,         -- porderdishesid - bigint
    '',        -- comdiscode - varchar(16)
    '',        -- comgcode - varchar(16)
    '',        -- composetype - varchar(1)
    NULL,      -- allowkinds - decimal(18, 2)
    NULL,      -- allowcount - decimal(18, 2)
    NULL,      -- allowamount - decimal(18, 2)
    NULL,      -- usedisdefaultamount - decimal(18, 2)
    NULL,      -- usedismaxamount - decimal(18, 2)
    '" + Convert.ToString(comdishesinfo.Rows[0]["unit"]) + @"',        -- unit - varchar(8)
    '',        -- extcode - varchar(64)
    '" + Convert.ToString(comdishesinfo.Rows[0]["fincode"]) + @"',        -- fincode - varchar(16)
    '" + Convert.ToString(comdishesinfo.Rows[0]["dcode"]) + @"',        -- dcode - varchar(16)
    '" + Convert.ToString(comdishesinfo.Rows[0]["kitcode"]) + @"',        -- kitcode - varchar(16)
    '" + Convert.ToString(comdishesinfo.Rows[0]["ecode"]) + @"',        -- ecode - varchar(16)
    '" + Convert.ToString(comdishesinfo.Rows[0]["warcode"]) + @"',        -- warcode - varchar(16)
    '" + oi.tmcode + @"',        -- totmcode - varchar(8)
    N'" + (oi.ttname + "—" + oi.tmname) + @"',       -- totmname - nvarchar(32)
    '" + detailid + @"',         -- todetailid - bigint
    '1',        -- status - char(1)
    '1',        -- makestatus - char(1)
    '0',        -- operaretype - char(1)
    '0',        -- isdiscount - char(1)
    '0',        -- priceispre - char(1)
    '0',        -- ispresented - char(1)
    '',        -- pecode - varchar(16)
    N'',       -- pename - nvarchar(32)
    N'',       -- prereason - nvarchar(128)
    N'',       -- prereasontype - nvarchar(32)
    N'',       -- remark - nvarchar(128)
    N'" + oi.remark + @"',       -- orderremark - nvarchar(128)
    '" + oguid + @"',        -- gguid - varchar(64)
    N'',       -- nopreremark - nvarchar(128)
    '0',        -- storeupdated - char(1)
    '" + cuser + @"',         -- cuser - bigint
    GETDATE(), -- ctime - datetime
    '" + comdisInfo.cprice + @"',      -- comprice - decimal(18, 2)
    N'微信管理员',       -- cusername - nvarchar(32)
    '" + oi.ttcode + @"',        -- tottcode - varchar(16)
    '',        -- metname - varchar(16)
    '',        -- metcode - varchar(16)
    '" + detailcode + @"',        -- detailcode - varchar(32)
    GETDATE(), -- tmopentime - datetime
    '',        -- chomac - varchar(64)
    '',        -- orderno - varchar(32)
    ''         -- subtype - char(1)
    );select SCOPE_IDENTITY();";

                //因为套餐里的单品字段需要套餐的id，所以需要先把套餐插入数据库返回id
                var orderdishesid = Convert.ToString(SQLTool.ExecuteScalar(comdishessql));
                var keyid = 0;
                if (oi.ptype == "0")
                {
                    orderdishesid = p_orderdishesid;
                    keyid = SQLTool.GetFirstIntField("SELECT count(cid) from " + choorderdishesTname); //查询choorderdisheshistory表里有多少条记录
                }


                for (int i = 0; i < oi.combodishes[j].dpdishes.Count; i++) //循环单品
                {
                    if (oi.ptype == "0")
                    {
                        p_orderdishesid = "" + 10000000 + keyid + i + ",";

                    }

                    var disInfo = oi.combodishes[j].dpdishes[i];
                    var dallmoney = disInfo.disnum * disInfo.oneprice;
                    var guid = Guid.NewGuid();

                    var dishesinfo = getDeshesInfo(oi.stocode, disInfo.discode);

                    dishessql += @"INSERT INTO	" + choorderdishesTname + @"(" + orderdishesidstr + @"buscode, stocode, orderid, detailid, distypecode, dtypecode, melcode, discode, disname, disothername, disnum, disnumconst, addnum, isneedmethod, distacode, distaname, ordertime, addtime, calltime, pushfoodtime, pushfoodstate, isentity, entitydefcount, entityprice, singlenum, singleAllmoney, totaladdmoney, totaladdmoneydiscount, allmoney, allmoneydiscount, memberallmoney, resultallmoney, packageaddmoney, ispackage, iscanout, isout, refundNum, refundaddnum, oneprice, memberprice, costprice, methodmoney, methodmoneydiscount, attachmoney, pushmoney, ismember, ispre, pretype, dispcode, discountratemax, discountrate, premoney, precheck, checktime, ismustconsume, mustconsumenum, iscaninventory, isallowmemberprice, isattachcalculate, isclipcoupons, isnonoperating, iscombooptional, isneedweigh, iscanmodifyprice, matcode, cguid, pcguid, porderdishesid, comdiscode, comgcode, composetype, allowkinds, allowcount, allowamount, usedisdefaultamount, usedismaxamount, unit, extcode, fincode, dcode, kitcode, ecode, warcode, totmcode, totmname, todetailid, status, makestatus, operaretype, isdiscount, priceispre, ispresented, pecode, pename, prereason, prereasontype, remark, orderremark, gguid, nopreremark, storeupdated, cuser, ctime, comprice, cusername, tottcode, metname, metcode, detailcode, tmopentime, chomac, orderno, subtype)
VALUES(
" + p_orderdishesid + @"
    '88888888',                              -- buscode - varchar(16)
    '" + oi.stocode + @"',                   -- stocode - varchar(8)
    '" + orderid + @"',                       -- orderid - bigint
    '" + detailid + @"',                       -- detailid - bigint
    '" + Convert.ToString(dishesinfo.Rows[0]["pdistypecode"]) + @"',               -- distypecode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["distypecode"]) + @"',           -- dtypecode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["melcode"]) + @"',           -- melcode - varchar(16)
    '" + disInfo.discode + @"',                -- discode - varchar(16)
    N'" + disInfo.disname + @"',                 -- disname - nvarchar(32)
    '',                                        -- disothername - varchar(128)
    '" + disInfo.disnum + @"',                -- disnum - decimal(18, 2)
    '" + disnumconst + @"',                     -- disnumconst - decimal(18, 2)
    '0.00',                                       -- addnum - decimal(18, 2)
    '" + Convert.ToString(dishesinfo.Rows[0]["isneedmethod"]) + @"',              -- isneedmethod - char(1)
    '" + disInfo.method[0].methodcode + @"',        -- distacode - varchar(256)
    '" + disInfo.method[0].methodname + @"',        -- distaname - varchar(256)
    GETDATE(),                                       -- ordertime - datetime
    GETDATE(),                                       -- addtime - datetime
    GETDATE(),                                          -- calltime - datetime
    GETDATE(),                                      -- pushfoodtime - datetime
    0,                                                -- pushfoodstate - int
    '',                                                   -- isentity - char(1)
    0,                                               -- entitydefcount - int
    NULL,                                            -- entityprice - decimal(18, 2)
    0,                                                      -- singlenum - int
    NULL,                                            -- singleAllmoney - decimal(18, 2)
    NULL,                                                -- totaladdmoney - decimal(18, 2)
    NULL,                                                -- totaladdmoneydiscount - decimal(18, 2)
    '" + dallmoney + @"',                                 -- allmoney - decimal(18, 2)
    '" + dallmoney + @"',                                -- allmoneydiscount - decimal(18, 2)
    '" + dallmoney + @"',                                -- memberallmoney - decimal(18, 2)
    NULL,                                             -- resultallmoney - decimal(18, 2)
    NULL,                                               -- packageaddmoney - decimal(18, 2)
    '',                                                 -- ispackage - char(1)
    '',                                                     -- iscanout - char(1)
    '',                                                 -- isout - char(1)
    NULL,                                                 -- refundNum - decimal(18, 2)
    NULL,                                                     -- refundaddnum - decimal(18, 2)
    '" + disInfo.oneprice + @"',                         -- oneprice - decimal(18, 2)
    '" + Convert.ToString(dishesinfo.Rows[0]["memberprice"]) + @"',                      -- memberprice - decimal(18, 2)
    NULL,      -- costprice - decimal(18, 2)
    NULL,      -- methodmoney - decimal(18, 2)
    NULL,      -- methodmoneydiscount - decimal(18, 2)
    NULL,      -- attachmoney - decimal(18, 2)
    NULL,      -- pushmoney - decimal(18, 2)
    '',        -- ismember - char(1)
    '',        -- ispre - char(1)
    '',        -- pretype - char(1)
    '',        -- dispcode - varchar(16)
    NULL,      -- discountratemax - decimal(18, 2)
    '-1',      -- discountrate - decimal(18, 2)
    NULL,      -- premoney - decimal(18, 2)
    N'',       -- precheck - nvarchar(32)
    GETDATE(), -- checktime - datetime
    '',        -- ismustconsume - char(1)
    NULL,      -- mustconsumenum - decimal(18, 2)
    '" + Convert.ToString(dishesinfo.Rows[0]["iscaninventory"]) + @"',        -- iscaninventory - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["isallowmemberprice"]) + @"',        -- isallowmemberprice - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["isattachcalculate"]) + @"',        -- isattachcalculate - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["isclipcoupons"]) + @"',        -- isclipcoupons - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["isnonoperating"]) + @"',        -- isnonoperating - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["iscombooptional"]) + @"',        -- iscombooptional - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["isneedweigh"]) + @"',        -- isneedweigh - char(1)
    '" + Convert.ToString(dishesinfo.Rows[0]["iscanmodifyprice"]) + @"',        -- iscanmodifyprice - char(1)
    '',        -- matcode - varchar(16)
    '" + guid + @"',        -- cguid - varchar(64)
    '" + cguid + @"',        -- pcguid - varchar(64)
    '" + orderdishesid.TrimEnd(',') + @"',         -- porderdishesid - bigint
    '" + comdisInfo.ccode + @"',        -- comdiscode - varchar(16)
    '',        -- comgcode - varchar(16)
    '',        -- composetype - varchar(1)
    NULL,      -- allowkinds - decimal(18, 2)
    NULL,      -- allowcount - decimal(18, 2)
    NULL,      -- allowamount - decimal(18, 2)
    NULL,      -- usedisdefaultamount - decimal(18, 2)
    NULL,      -- usedismaxamount - decimal(18, 2)
    '" + Convert.ToString(dishesinfo.Rows[0]["unit"]) + @"',        -- unit - varchar(8)
    '',        -- extcode - varchar(64)
    '" + Convert.ToString(dishesinfo.Rows[0]["fincode"]) + @"',        -- fincode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["dcode"]) + @"',        -- dcode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["kitcode"]) + @"',        -- kitcode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["ecode"]) + @"',        -- ecode - varchar(16)
    '" + Convert.ToString(dishesinfo.Rows[0]["warcode"]) + @"',        -- warcode - varchar(16)
    '" + oi.tmcode + @"',        -- totmcode - varchar(8)
    N'" + (oi.ttname + "—" + oi.tmname) + @"',       -- totmname - nvarchar(32)
    '" + detailid + @"',         -- todetailid - bigint
    '1',        -- status - char(1)
    '1',        -- makestatus - char(1)
    '0',        -- operaretype - char(1)
    '0',        -- isdiscount - char(1)
    '0',        -- priceispre - char(1)
    '0',        -- ispresented - char(1)
    '',        -- pecode - varchar(16)
    N'',       -- pename - nvarchar(32)
    N'',       -- prereason - nvarchar(128)
    N'',       -- prereasontype - nvarchar(32)
    N'',       -- remark - nvarchar(128)
    N'" + oi.remark + @"',       -- orderremark - nvarchar(128)
    '" + oguid + @"',        -- gguid - varchar(64)
    N'',       -- nopreremark - nvarchar(128)
    '0',        -- storeupdated - char(1)
    '" + cuser + @"',         -- cuser - bigint
    GETDATE(), -- ctime - datetime
    '" + disInfo.oneprice + @"',      -- comprice - decimal(18, 2)
    N'微信管理员',       -- cusername - nvarchar(32)
    '" + oi.ttcode + @"',        -- tottcode - varchar(16)
    '',        -- metname - varchar(16)
    '',        -- metcode - varchar(16)
    '" + detailcode + @"',        -- detailcode - varchar(32)
    GETDATE(), -- tmopentime - datetime
    '',        -- chomac - varchar(64)
    '',        -- orderno - varchar(32)
    ''         -- subtype - char(1)
    );";
                }
                SQLTool.ExecuteNonQuery(dishessql);
                dishessql = string.Empty;
            }

        }

        /// <summary>
        /// 获取菜品信息（包括菜品分类等）
        /// </summary>
        /// <param name="stocode"></param>
        /// <param name="discode"></param>
        /// <param name="distype"></param>
        /// <returns></returns>
        public DataTable getDeshesInfo(string stocode, string discode)
        {
            try
            {
                return SQLTool.ExecuteDataTable("SELECT TOP 1 dt.pdistypecode,dis.* FROM   dbo.dishes AS dis left JOIN dbo.DisheType AS dt ON dis.distypecode=dt.distypecode WHERE dis.stocode='" + stocode + "' AND discode='" + discode + "'");
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex.ToString());
                return null;
            }

        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="dicPar"></param>
        public void orderDetails(Dictionary<string, object> dicPar)
        {

            List<string> pra = new List<string>() { "USER_ID", "stocode", "ordercode", "paytype" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            var ordercode = dicPar["ordercode"];
            var stocode = dicPar["stocode"];

            sql = "SELECT remake,dbo.getpaytype(od.orderno,od.payType,od.status,od.cardCode,od.out_trade_no) as paytype,od.ctime,(SELECT cname FROM dbo.Store WHERE stocode=od.stocode) AS fname,status,od.orderno,(SELECT qcCode FROM WX_orderdetails WHERE orderno='" + ordercode + "') AS qcCode,od.postJson FROM dbo.WX_orderdetails AS od WHERE orderno='" + ordercode + "'";
            var orderInfo = SQLTool.ExecuteDataTable(sql);

            var qcCode = string.Empty;
            var qcCodeUrl = string.Empty;
            try
            {
                qcCode = Convert.ToString(orderInfo.Rows[0]["qcCode"]);
                if (!string.IsNullOrEmpty(qcCode))
                {

                    var imgPath = "/erqimg/" + qcCode + ".jpg";
                    if (!File.Exists(Pagcontext.Server.MapPath("~" + imgPath + "")))
                        qcCodeUrl = imgurl + DoWaitProcess(qcCode);
                    else
                        qcCodeUrl = imgurl + imgPath;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex.ToString());
                qcCodeUrl = "";
            }

            //免密额度
            sql = "SELECT amount FROM dbo.WX_members_wx WHERE openid='" + dicPar["USER_ID"] + "' AND notpwd=1";
            var amount = SQLTool.GetFirstField(sql);
            if (string.IsNullOrEmpty(amount))
            {
                amount = "0";
            }

            var strJson = Convert.ToString(orderInfo.Rows[0]["postJson"]);

            if (!strJson.Contains("payType"))
            {
                strJson = strJson.Insert(1, "\"payType\":\"" + Convert.ToString(orderInfo.Rows[0]["paytype"]) + "\",\"ordertime\":\"" + Convert.ToString(orderInfo.Rows[0]["ctime"]) + "\",\"fname\":\"" + Convert.ToString(orderInfo.Rows[0]["fname"]) + "\",\"status\":\"" + Convert.ToString(orderInfo.Rows[0]["status"]) + "\",\"orderno\":\"" + Convert.ToString(orderInfo.Rows[0]["orderno"]) + "\",\"qcCode\":\"" + qcCode + "\",\"qcCodeUrl\":\"" + qcCodeUrl + "\",\"amount\":\"" + amount + "\",\"remake\":\"" + Convert.ToString(orderInfo.Rows[0]["remake"]) + "\",");
            }
            else
            {
                var str = (JObject)JsonConvert.DeserializeObject(strJson);
                str["status"] = Convert.ToString(orderInfo.Rows[0]["status"]);
                str["qcCode"] = qcCode;
                str["qcCodeUrl"] = qcCodeUrl;
                strJson = Convert.ToString(str);
            }

            ToJsonStr(strJson);
        }

        /// <summary>
        /// 再次下单
        /// </summary>
        /// <param name="dicPar"></param>
        public void againOrder(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "USER_ID", "stocode", "ordercode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            try
            {
                sql = "select postJson from WX_orderdetails where openid='" + Tools.SafeSql(dicPar["USER_ID"].ToString()) + "' and orderno='" + Tools.SafeSql(dicPar["ordercode"].ToString()) + "'";
                dt = new bllPaging().GetDataTableInfoBySQL(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    ReturnListJson(dt);
                }
                else
                {
                    ToCustomerJson("1", "暂无数据");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorMessage(ex.ToString());
                ToCustomerJson("2", "网络繁忙，请稍后再试");
            }
        }

        /// <summary>
        /// datatable转换成json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="excludeColumns"></param>
        /// <returns></returns>
        public string dtToJson(DataTable dt, string excludeColumns, string iscombo)
        {
            var strJson = string.Empty;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    if (iscombo == "1") strJson += "{";
                    for (int h = 0; h < dt.Columns.Count; h++)
                    {
                        var ColumnName = dt.Columns[h].ColumnName;
                        if (ColumnName != excludeColumns)
                            strJson += string.Format("\"{0}\":\"{1}\",", ColumnName, dt.Rows[0][ColumnName]);
                    }
                    strJson = strJson.TrimEnd(',');
                    if (iscombo == "1") strJson += "}";
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strJson += "{";
                        for (int h = 0; h < dt.Columns.Count; h++)
                        {
                            var ColumnName = dt.Columns[h].ColumnName;
                            if (ColumnName != excludeColumns)
                                strJson += string.Format("\"{0}\":\"{1}\",", ColumnName, dt.Rows[i][ColumnName]);
                        }
                        strJson = strJson.TrimEnd(',');
                        strJson += "},";
                    }
                    strJson = strJson.TrimEnd(',');
                }

            }
            else
            {
                strJson = "";
            }
            return strJson;
        }

        /// <summary>
        /// 判断用户下单菜品是否大于售罄菜品
        /// </summary>
        /// <param name="oi"></param>
        /// <returns></returns>
        public static string IsSQ(OrderInfo oi)
        {
            var stocode = oi.stocode;
            string discodelist = string.Empty;
            var sql = string.Format("select ptype from storegx WHERE stocode='{0}';SELECT discode,[count] FROM WX_dishesSelloff WHERE stocode='{0}';select postJson from WX_orderdetails where stocode='{0}' and status=1 and postJson IS NOT NULL and DATEDIFF(DAY,paytime,GETDATE())=0 ", stocode);
            var ds = SQL.XJWZSQLTool.ExecuteDataset(sql);
            if (ds.Tables.Count != 3)
            {
                return "1";
            }

            var ptype = Convert.ToString(ds.Tables[0].Rows[0][0]);
            if (ptype == "1") //如果是后付，则不需要判断
            {
                return "1";
            }

            try
            {
                var sqdt = ds.Tables[1]; //设置了售罄的菜品
                if (sqdt.Rows.Count > 0)
                {
                    var dt = ds.Tables[2];//当日下单的所有菜品的json
                    var dishes = sqdt.Clone();  //当天所有的菜菜品集合
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var pJson = dt.Rows[i][0].ToString();
                        if (!string.IsNullOrEmpty(pJson))
                        {
                            var jo = (JObject)JsonConvert.DeserializeObject(pJson);
                            var jArray = (JArray)JsonConvert.DeserializeObject(jo["dpdishes"].ToString());
                            for (int x = 0; x < jArray.Count; x++)
                            {
                                var row = dishes.NewRow();
                                var discode = jArray[x]["discode"].ToString();
                                var num = Convert.ToInt32(jArray[x]["disnum"]);
                                row["discode"] = discode;
                                row["count"] = num;
                                var dishesrows = dishes.Select(" discode='" + discode + "'");
                                if (dishesrows.Length == 0) //如果菜品在新的datatble里没有，则添加到datable里，如果有的话则把当前的数量加上
                                {
                                    dishes.Rows.Add(row);
                                }
                                else
                                {
                                    dishesrows[0]["count"] = Convert.ToInt32(dishesrows[0]["count"]) + num;
                                }
                            }
                        }
                    }

                    for (int y = 0; y < sqdt.Rows.Count; y++) //循环所有设置售罄的菜品
                    {
                        var sqdiscode = sqdt.Rows[y]["discode"].ToString();
                        for (int x = 0; x < oi.dpdishes.Count; x++)  //循环用户下单的菜品
                        {
                            var dpdiscode = oi.dpdishes[x].discode;
                            var dpnum = oi.dpdishes[x].disnum;
                            if (dpdiscode == sqdiscode)   //下单的菜品里如果有设置了售罄的菜品
                            {
                                var dishe = dishes.Select("discode='" + dpdiscode + "'");
                                var dnum = 0;
                                if (dishe.Length > 0) dnum = Convert.ToInt32(dishe[0]["count"]);
                                var z = dnum + dpnum;                                //当天售出的数量加上用户下单的数量
                                var i = Convert.ToInt32(sqdt.Rows[y]["count"]);      //每日售罄的数量
                                if (z > i)
                                {
                                    discodelist += oi.dpdishes[x].disname + "只剩下了" + (i - dnum) + "份,";
                                }
                            }
                        }
                    }


                    if (!string.IsNullOrEmpty(discodelist))
                        discodelist = discodelist.TrimEnd(',');
                    else
                        discodelist = "1";

                }
                else
                {
                    discodelist = "1";
                }
            }
            catch (Exception ex)
            {
                discodelist = "1";
            }
            return discodelist;
        }

        /// <summary>
        /// 获取售罄的商品
        /// </summary>
        /// <param name="stocode"></param>
        /// <returns></returns>
        public static string GetSoldOut(string stocode)
        {
            stocode = stocode.Trim('"');
            string discodelist = string.Empty;
            var sql = string.Format("select ptype from storegx WHERE stocode='{0}';SELECT discode,[count] FROM WX_dishesSelloff WHERE stocode='{0}';select postJson from WX_orderdetails where stocode='{0}' and status=1 and postJson IS NOT NULL and DATEDIFF(DAY,paytime,GETDATE())=0 ", stocode);
            var ds = SQL.XJWZSQLTool.ExecuteDataset(sql);
            if (ds.Tables.Count != 3)
            {
                return "1";
            }

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                var ptype = Convert.ToString(ds.Tables[0].Rows[0][0]);
                if (ptype == "1") //如果是后付，则不需要判断
                {
                    return "1";
                }

                try
                {
                    var sqdt = ds.Tables[1]; //设置了售罄的菜品
                    if (sqdt.Rows.Count > 0)
                    {
                        var dt = ds.Tables[2];//当日下单的所有菜品的json
                        var dishes = sqdt.Clone();  //当天所有的菜菜品集合
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                var pJson = dt.Rows[i][0].ToString();
                                if (!string.IsNullOrEmpty(pJson))
                                {
                                    var jo = (JObject)JsonConvert.DeserializeObject(pJson);
                                    var jArray = (JArray)JsonConvert.DeserializeObject(jo["dpdishes"].ToString());
                                    for (int x = 0; x < jArray.Count; x++)
                                    {
                                        var row = dishes.NewRow();
                                        var discode = jArray[x]["discode"].ToString();
                                        var num = Convert.ToInt32(jArray[x]["disnum"]);
                                        row["discode"] = discode;
                                        row["count"] = num;
                                        var dishesrows = dishes.Select(" discode='" + discode + "'");
                                        if (dishesrows.Length == 0) //如果菜品在新的datatble里没有，则添加到datable里，如果有的话则把当前的数量加上
                                        {
                                            dishes.Rows.Add(row);
                                        }
                                        else
                                        {
                                            dishesrows[0]["count"] = Convert.ToInt32(dishesrows[0]["count"]) + num;
                                        }
                                    }
                                }
                            }

                            for (int y = 0; y < sqdt.Rows.Count; y++) //循环所有设置售罄的菜品
                            {
                                var sqdiscode = sqdt.Rows[y]["discode"].ToString();
                                var dishe = dishes.Select("discode='" + sqdiscode + "'");
                                if (dishe.Length > 0)
                                {
                                    var z = Convert.ToInt32(dishe[0]["count"]);     //当天售出的数量
                                    var x = Convert.ToInt32(sqdt.Rows[y]["count"]); //每日售罄的数量
                                    if (z >= x)
                                    {
                                        discodelist += sqdiscode + ",";
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(discodelist))
                                discodelist = discodelist.TrimEnd(',');
                            else
                                discodelist = "1";
                        }
                        else
                        {
                            discodelist = "1";
                        }
                    }
                    else
                    {
                        discodelist = "1";
                    }
                }
                catch (Exception ex)
                {
                    discodelist = "1";
                }
            }
            else
            {
                discodelist = "1";
            }
            return discodelist;
        }

        /// <summary>
        /// 支付的时候判断是否有售罄的
        /// </summary>
        /// <param name="stocode"></param>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static string GetSoldResult(string stocode, string strJson)
        {

            var sresult = string.Empty;  //下单的商品
            var sqresult = Dishes.GetSoldOut(stocode);
            var result = string.Empty;
            if (sqresult != "1")
            {

                var jo = (JObject)JsonConvert.DeserializeObject(strJson);
                var jArray = (JArray)JsonConvert.DeserializeObject(jo["dpdishes"].ToString());
                for (int x = 0; x < jArray.Count; x++)
                {

                    if (sqresult.Contains(jArray[x]["discode"].ToString()))
                    {
                        sresult += jArray[x]["discode"].ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(sresult))
                {
                    sresult = sresult.TrimEnd(',');
                    result = SQL.XJWZSQLTool.GetFirststringField("SELECT STUFF((SELECT ','+disname FROM dbo.dishes WHERE discode IN('" + sresult.Replace(",", "','") + "') AND stocode='" + stocode.Trim('"') + "' FOR XML PATH('')),1,1,'') ");

                }
            }
            return result;
        }

        /// <summary>
        /// 做法详情
        /// </summary>
        public class MethodItem
        {
            /// <summary>
            /// 做法编号
            /// </summary>
            public string methodcode { get; set; }
            /// <summary>
            /// 做法名称
            /// </summary>
            public string methodname { get; set; }
            /// <summary>
            /// 做法加价
            /// </summary>
            public string methodadd { get; set; }
        }

        /// <summary>
        /// 单品信息
        /// </summary>
        public class DpdishesItem
        {
            /// <summary>
            /// 菜品编号
            /// </summary>
            public string discode { get; set; }
            /// <summary>
            /// 菜品名称
            /// </summary>
            public string disname { get; set; }

            public string img { get; set; }
            public string unit { get; set; }

            /// <summary>
            /// 数量
            /// </summary>
            public int disnum { get; set; }
            /// <summary>
            /// 单价
            /// </summary>
            public float oneprice { get; set; }
            /// <summary>
            /// 菜品总金额(条只金额+菜品金额+加价金额)
            /// </summary>
            public float allmoney { get; set; }
            /// <summary>
            /// 口味编号
            /// </summary>
            public string distacode { get; set; }
            /// <summary>
            /// 口味名称
            /// </summary>
            public string distaname { get; set; }
            /// <summary>
            /// 总加价金额
            /// </summary>
            public string totaladdmoney { get; set; }
            /// <summary>
            /// 做法详情
            /// </summary>
            public List<MethodItem> method { get; set; }

        }

        /// <summary>
        /// 套餐信息
        /// </summary>
        public class CombodishesItem
        {
            /// <summary>
            /// 套餐编号
            /// </summary>
            public string ccode { get; set; }
            /// <summary>
            /// 套餐名称
            /// </summary>
            public string cname { get; set; }
            /// <summary>
            /// 套餐单价
            /// </summary>
            public float cprice { get; set; }
            public string img { get; set; }
            public string unit { get; set; }
            /// <summary>
            /// 套餐数量
            /// </summary>
            public int cnum { get; set; }
            /// <summary>
            /// 套餐总金额(条只金额+菜品金额+加价金额)
            /// </summary>
            public float callmoney { get; set; }
            /// <summary>
            /// 套餐总加价
            /// </summary>
            public float packageaddmoney { get; set; }
            /// <summary>
            /// 单品信息
            /// </summary>
            public List<DpdishesItem> dpdishes { get; set; }
        }

        /// <summary>
        /// 订单信息
        /// </summary>
        public class OrderInfo
        {
            /// <summary>
            /// 用户id
            /// </summary>
            public string openid { get; set; }

            /// <summary>
            /// 门店id
            /// </summary>
            public string stocode { get; set; }

            /// <summary>
            /// 总价格
            /// </summary>
            public string sumprice { get; set; }

            /// <summary>
            /// 折扣后的总金额
            /// </summary>
            public float discountprice { get; set; }

            /// <summary>
            /// 附加总金额
            /// </summary>
            public string surchargeamount { get; set; }

            /// <summary>
            /// 优惠券编码
            /// </summary>
            public string checkcode { get; set; }

            /// <summary>
            /// 优惠券名称
            /// </summary>
            public string couname { get; set; }

            /// <summary>
            /// 优惠金额
            /// </summary>
            public float singlemoney { get; set; }

            /// <summary>
            /// 折扣卡编码
            /// </summary>
            public string cardcode { get; set; }

            /// <summary>
            /// 折扣方案名称
            /// </summary>
            public string dispname { get; set; }

            /// <summary>
            /// 折扣卡类型 1：电子卡  0：非电子卡
            /// </summary>
            public string isecard { get; set; }

            /// <summary>
            /// 折扣金额
            /// </summary>
            public float cschemediscmoney { get; set; }

            /// <summary>
            /// 折扣率
            /// </summary>
            public string privilegepre { get; set; }

            /// <summary>
            /// 先支付还是后支付（0：先支付，1：后支付）
            /// </summary>
            public string ptype { get; set; }

            /// <summary>
            /// 总数量(套餐算一个菜)
            /// </summary>
            public string number { get; set; }

            /// <summary>
            /// 桌台类型
            /// </summary>
            public string ttcode { get; set; }

            /// <summary>
            /// 桌台类型名称
            /// </summary>
            public string ttname { get; set; }

            /// <summary>
            /// 桌台编号
            /// </summary>
            public string tmcode { get; set; }

            /// <summary>
            /// 桌台名称
            /// </summary>
            public string tmname { get; set; }

            /// <summary>
            /// 就餐人数
            /// </summary>
            public int personnum { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public string remark { get; set; }

            /// <summary>
            /// 单品信息
            /// </summary>
            public List<DpdishesItem> dpdishes { get; set; }

            /// <summary>
            /// 套餐信息
            /// </summary>
            public List<CombodishesItem> combodishes { get; set; }
        }
    }
}