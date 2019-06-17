using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using WxPayAPI;
using XJWZCatering.CommonBasic;
using XJWZCatering.LinkPubWx;
using XJWZCatering.SQL;
using XJWZCatering.WServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace XJWZCatering.WServer.WxServer
{
    /// <summary>
    /// WxPay 的摘要说明
    /// </summary>
    public class WxPay : ServiceBase
    {
        public override void ProcessRequest(HttpContext context)
        {
            if (CheckParameters(context))//检测是否合法
            {
                Dictionary<string, object> dicPar = GetParameters();
                if (dicPar != null)
                {
                    switch (actionname)
                    {
                        case "getpayparams":
                            GetPayParams(dicPar);
                            break;
                        case "cardpay":
                            VipCardPay(dicPar);
                            break;
                        case "refund":
                            Refund(dicPar);
                            break;
                        case "getpayparamsR":
                            GetPayParamsRecharge(dicPar);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 给微信支付做准备，更新相应的数据表
        /// </summary>
        /// <param name="dicPar"></param>
        public void GetPayParams(Dictionary<string, object> dicPar)
        {
            var wxJsApiParam = string.Empty;
            ///要检测的参数信息 money：单位为分
            List<string> pra = new List<string>() { "GUID", "USER_ID", "money", "stocode", "orderno", "type", "zkje", "zkcode", "zkname", "yhje", "yhcode", "yhname", "strJson" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }
            try
            {
                var openid = dicPar["USER_ID"].ToString();
                var wxopenid = Convert.ToString(SQL.SQLTool.ExecuteScalar("SELECT wxopenid FROM WX_members_wx WHERE openid='" + openid + "'"));
                var total_fee = Convert.ToDecimal(dicPar["money"].ToString());
                var stocode = dicPar["stocode"].ToString();
                var orderno = dicPar["orderno"].ToString();
                var strJson = Convert.ToString(dicPar["strJson"]).Replace('\'', '"');

                var sresult = Dishes.GetSoldResult(stocode, strJson);
                if (!string.IsNullOrEmpty(sresult))
                {
                    ToCustomerJson("1", "菜品【" + sresult + "】已售罄");
                    return;
                }

                System.Web.UI.Page page = new System.Web.UI.Page();
                //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
                JsApiPay jsApiPay = new JsApiPay(page);
                jsApiPay.openid = wxopenid;
                jsApiPay.total_fee = Convert.ToInt32(total_fee * 100);  //*100
                //JSAPI支付预处理

                //查询该订单是否已有商户订单号
                var otn = "select out_trade_no from WX_orderdetails where orderno='" + orderno + "' AND openid='" + openid + "'";

                //查询是否已支付，如果已经支付过，返回错误信息给前端
                var existSql = "select trade_state from wx_pay where out_trade_no=(" + otn + ")";
                var result = Convert.ToString(SQL.SQLTool.ExecuteScalar(existSql));

                if (result == "SUCCESS")
                {
                    wxJsApiParam = "paid";
                }
                else
                {
                    SetParam.SetParams(stocode);
                    WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                    wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数         

                    var zkmoney = dicPar["zkje"].ToString();
                    var yhmoney = dicPar["yhje"].ToString();

                    try
                    {
                        //此处更新订单及支付相关的表
                        var sql = "update WX_orderdetails set postJson='" + strJson + "',out_trade_no='" + jsApiPay.out_trade_no + "',discountprice='" + total_fee + "',privilegepre='" + zkmoney + "',singlemoney='" + yhmoney + "'  where orderno='" + orderno + "' AND openid='" + openid + "';UPDATE WX_choorderdetail SET conmoney='" + total_fee + "' WHERE detailcode='" + orderno + "';UPDATE dbo.choorderdetailBreakhistory SET yhqmoney='" + yhmoney + "',zkmoney='" + zkmoney + "',disratemoney='" + yhmoney + "',cardCode='" + dicPar["zkcode"].ToString() + "',cschemediscmoney='" + zkmoney + "',dispname='" + dicPar["zkname"].ToString() + "',disccardCode='" + dicPar["zkcode"].ToString() + "' where detailcodes='" + orderno + "';UPDATE dbo.chopayhistory SET couponmoney='" + yhmoney + "',accountcode='" + dicPar["zkcode"].ToString() + "',paymoney='" + total_fee + "',rmmoney='" + total_fee + "' where detailcode='" + orderno + "'";

                        var type = Convert.ToString(dicPar["type"]);
                        if (type == "3") //0后支付，1先支付，2打赏，3充值
                        {
                            var detailcode = Convert.ToString(SQLTool.ExecuteScalar("SELECT dbo.f_GetChoorderNo()"));
                            sql = "insert into WX_orderdetails(source,buscode,stocode,openid,orderno,sumprice,money,discountprice,status,payType,out_trade_no,cardCode,postJson) values ('wechat','88888888','" + stocode + "','" + openid + "','" + detailcode + "','" + total_fee + "','" + total_fee + "','" + total_fee + "','0','" + type + "','" + jsApiPay.out_trade_no + "','" + orderno + "','" + strJson + "')";
                        }

                        SQLTool.ExecuteNonQuery(sql);
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteErrorMessage(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                wxJsApiParam = "";
                ErrorLog.WriteErrorMessage(ex.ToString());
            }

            Pagcontext.Response.Clear();
            Pagcontext.Response.Write(wxJsApiParam);
            Pagcontext.Response.End();
        }

        /// <summary>
        /// 会员卡支付
        /// </summary>
        /// <param name="dicPar"></param>
        public void VipCardPay(Dictionary<string, object> dicPar)
        {

            List<string> pra = new List<string>() { "GUID", "USER_ID", "money", "stocode", "cardcode", "orderno", "zkje", "zkcode", "zkname", "yhje", "yhcode", "yhname", "strJson" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            var openid = dicPar["USER_ID"].ToString();
            var total_fee = dicPar["money"].ToString();
            var stocode = dicPar["stocode"].ToString();
            var orderno = dicPar["orderno"].ToString();
            var strJson = Convert.ToString(dicPar["strJson"]).Replace('\'', '"');

            var sresult = Dishes.GetSoldResult(stocode, strJson);
            if (!string.IsNullOrEmpty(sresult))
            {
                ToCustomerJson("1", "菜品【" + sresult + "】已售罄");
                return;
            }

            var pcodeSql = "SELECT pcode FROM dbo.chopayhistory where detailcode='" + orderno + "' and strcode='" + stocode + "'";
            var pcode = Convert.ToString(SQL.SQLTool.ExecuteScalar(pcodeSql));


            string ServiceUrl = Helper.GetAppSettings("VipPostUrl");
            string InterfaceUrl = ServiceUrl + "/memcardpay/memcardpay.ashx";
            string DetailParameters = "actionname={0}&parameters={{'GUID':'111','USER_ID':'" + openid + "','buscode':'88888888','stocode':'" + stocode + "','terminalType':'WX','ucode':'微信线上用户','uname':'微信线上用户','paycode':'" + orderno + "','vicepaycode':'" + pcode + "','couponcode':'" + dicPar["yhcode"].ToString() + "','cardcode':'" + dicPar["cardcode"].ToString() + "','expend':'" + total_fee + "','isopen':'0','invamount':'0','addint':'0','deductionint':'0'}}";
            StringBuilder postStr = new StringBuilder();
            string[] arrPar = new string[] { "cardpaynew" };
            postStr.Append(string.Format(DetailParameters, arrPar));//键值对
            string jsonStr = Helper.HttpWebRequestByURL(InterfaceUrl, postStr);

            if (jsonStr.Length > 0)
            {
                var status = JsonHelper.GetJsonValByKey(jsonStr, "status");
                if (status == "0")
                {
                    try
                    {
                        var code = string.Empty;
                        try
                        {
                            code = Tools.RandomCode(); //生成取餐码
                        }
                        catch (Exception ex)
                        {
                            code = "";
                        }

                        var zkmoney = dicPar["zkje"].ToString();
                        var yhmoney = dicPar["yhje"].ToString();


                        //此处更新订单及支付相关的表
                        var sql = "update WX_orderdetails set postJson='" + strJson + "',qcCode='" + code + "',pcode='" + pcode + "',discountprice='" + total_fee + "',status='1',cardCode='" + dicPar["cardcode"].ToString() + "',paytime=getdate(),privilegepre='" + zkmoney + "',singlemoney='" + yhmoney + "' where orderno='" + orderno + "' AND openid='" + openid + "';UPDATE WX_choorderdetail SET status='6',conmoney='" + total_fee + "' WHERE detailcode='" + orderno + "';UPDATE dbo.choorderdetailBreakhistory SET pstatus='4',yhqmoney='" + yhmoney + "',zkmoney='" + zkmoney + "',disratemoney='" + yhmoney + "',cardCode='" + dicPar["zkcode"].ToString() + "',cschemediscmoney='" + zkmoney + "',dispname='" + dicPar["zkname"].ToString() + "',disccardCode='" + dicPar["zkcode"].ToString() + "' where detailcodes='" + orderno + "';UPDATE dbo.chopayhistory SET status='1',paytype='3',payname='会员卡',couponmoney='" + yhmoney + "',accountcode='" + dicPar["zkcode"].ToString() + "',paymoney='" + total_fee + "',rmmoney='" + total_fee + "' where detailcode='" + orderno + "'";
                        SQL.SQLTool.ExecuteNonQuery(sql);

                    }
                    catch (Exception ex)
                    {
                        ToCustomerJson("1", "支付失败");
                        return;
                    }

                    ToCustomerJson("0", "支付成功");
                }
                else if (status == "2")
                {
                    ToCustomerJson("2", "余额不足");
                }
            }

        }

        /// <summary>
        /// 改签退款
        /// </summary>
        /// <param name="dicPar"></param>
        public void Refund(Dictionary<string, object> dicPar)
        {
            List<string> pra = new List<string>() { "GUID", "refund_fee", "orderno" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            var refund_fee = dicPar["refund_fee"].ToString(); //退款金额  如果空，则为退所有
            var orderno = dicPar["orderno"].ToString();

            WxPayData data = new WxPayData();
            var sql = "SELECT money,transaction_id,out_trade_no FROM dbo.Wx_Pay WHERE out_trade_no=(SELECT out_trade_no FROM dbo.WX_orderdetails WHERE orderno='" + orderno + "')";
            var orderInfo = SQL.SQLTool.ExecuteDataTable(sql);
            if (orderInfo.Rows.Count > 0) //如果数据库返回了数据才进行退款
            {
                var total_fee = Convert.ToString(orderInfo.Rows[0]["money"]);  //
                if (refund_fee == "0") refund_fee = total_fee;
                var transaction_id = Convert.ToString(orderInfo.Rows[0]["transaction_id"]);
                var out_trade_no = Convert.ToString(orderInfo.Rows[0]["out_trade_no"]);

                if (!string.IsNullOrEmpty(transaction_id))//微信订单号存在的条件下，则已微信订单号为准
                {
                    data.SetValue("transaction_id", transaction_id);
                }
                else//微信订单号不存在，才根据商户订单号去退款
                {
                    data.SetValue("out_trade_no", out_trade_no);
                }
                // SetParam.SetParams(stocode);
                var out_refund_no = WxPayApi.GenerateOutTradeNo(); //退款单号
                data.SetValue("total_fee", int.Parse(total_fee));//订单总金额
                data.SetValue("refund_fee", int.Parse(refund_fee));//退款金额
                data.SetValue("out_refund_no", out_refund_no);//随机生成商户退款单号
                data.SetValue("op_user_id", WxPayConfig.MCHID);//操作员，默认为商户号
                WxPayData result = WxPayApi.Refund(data);//提交退款申请给API，接收返回数据
                var pResult = result.ToPrintStr();

                var v = pResult.Split('|');
                var strWhere = string.Empty;
                var result_code = string.Empty;
                for (int i = 0; i < v.Count(); i++)
                {
                    if (v[i].Contains("refund_id"))
                    {
                        strWhere += "refund_id='" + Convert.ToString(v[i].Split('=')[1]) + "',";
                    }
                    else if (v[i].Contains("result_code"))
                    {
                        result_code = Convert.ToString(v[i].Split('=')[1]);
                        strWhere += "result_code='" + result_code + "',";
                    }
                    else if (v[i].Contains("err_code_des"))
                    {

                        strWhere += "err_code_des='" + Convert.ToString(v[i].Split('=')[1]) + "',";
                    }
                }

                try
                {
                    sql = "update WX_orderdetails SET " + strWhere + " out_refund_no='" + out_refund_no + "',status='7' WHERE orderno='" + Tools.SafeSql(orderno) + "'";
                    SQLTool.ExecuteScalar(sql);
                    ToCustomerJson("0", result_code);
                }
                catch (Exception ex)
                {
                    ToCustomerJson("1", "退款失败");
                }

            }
            else
            {
                ToCustomerJson("2", "未查询到该订单号的付款信息");
            }
        }


        /// <summary>
        /// 给微信支付做准备，更新相应的数据表
        /// </summary>
        /// <param name="dicPar"></param>
        public void GetPayParamsRecharge(Dictionary<string, object> dicPar)
        {
            var wxJsApiParam = string.Empty;
            ///要检测的参数信息 money：单位为分
            List<string> pra = new List<string>() { "GUID", "USER_ID", "money", "stocode", "strJson", "cardID", "memcode" };
            //检测方法需要的参数
            if (!CheckActionParameters(dicPar, pra))
            {
                return;
            }

            var openid = dicPar["USER_ID"].ToString();
            //var wxopenid = Convert.ToString(SQLTool.ExecuteScalar("SELECT wxopenid FROM WX_members_wx WHERE openid='" + openid + "'"));
            var total_fee = Convert.ToDecimal(dicPar["money"].ToString());
            var stocode = dicPar["stocode"].ToString();
            var strJson = Convert.ToString(dicPar["strJson"]).Replace('\'', '"');
            var cardId = dicPar["cardID"].ToString();
            var memcode = dicPar["memcode"].ToString();

            System.Web.UI.Page page = new System.Web.UI.Page();
            //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
            JsApiPay jsApiPay = new JsApiPay(page);
            jsApiPay.openid = openid;
            jsApiPay.total_fee = Convert.ToInt32(total_fee * 100);  //*100
            //JSAPI支付预处理
            try
            {
                SetParam.SetParams(stocode);
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数   
                try
                {

                    var detailcode = Convert.ToString(SQLTool.ExecuteScalar("declare @ordcode varchar(32); exec [dbo].[p_GetOrderCode] @ordcode output;select 'WX'+@ordcode;"));
                    var sql = "insert into WX_orderdetails(source,buscode,stocode,openid,orderno,sumprice,money,discountprice,status,payType,out_trade_no,postJson) values ('wechat','88888888','" + stocode + "','" + openid + "','" + detailcode + "','" + total_fee + "','" + total_fee + "','" + total_fee + "','0','3','" + jsApiPay.out_trade_no + "','" + strJson + "');";
                    if (!string.IsNullOrEmpty(cardId))
                    {
                        sql += "update members set IDNO='" + Tools.SafeSql(cardId) + "' where ISNULL(IDNO,'')='' and memcode='" + memcode + "';";
                    }
                    SQLTool.ExecuteNonQuery(sql);
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorMessage(ex.ToString());
                }

            }
            catch (Exception ex)
            {
                //  wxJsApiParam = "";
                ErrorLog.WriteErrorMessage("ex:" + ex.ToString());
            }

            try
            {
                Pagcontext.Response.Clear();
                Pagcontext.Response.Write(wxJsApiParam);

            }
            catch (Exception)
            {


            }
            finally
            {
                Pagcontext.Response.End();
            }


        }
    }
}