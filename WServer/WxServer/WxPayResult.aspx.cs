using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayAPI;
using XJWZCatering.CommonBasic;
using XJWZCatering.WServices;
using XJWZCatering.WXHelper;

namespace XJWZCatering.WServer.WxServer
{
    public partial class WxPayResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Notify n = new Notify(this);
                WxPayData notifyData = n.GetNotifyData();

                var transaction_id = notifyData.GetValue("transaction_id").ToString(); //微信支付订单号
                var result_code = notifyData.GetValue("result_code").ToString(); //业务结果
                var out_trade_no = notifyData.GetValue("out_trade_no").ToString();  //商户订单号

                //根据订单号请求微信接口查询订单详情
                WxPayData data = new WxPayData();
                if (!string.IsNullOrEmpty(transaction_id))//如果微信订单号存在，则以微信订单号为准
                {
                    data.SetValue("transaction_id", transaction_id);
                }
                else//微信订单号不存在，才根据商户订单号去查单
                {
                    data.SetValue("out_trade_no", out_trade_no);
                }

                WxPayData result = WxPayApi.OrderQuery(data);//提交订单查询请求给API，接收返回数据

                if (result.GetValue("return_code").ToString() == "SUCCESS" && result.GetValue("result_code").ToString() == "SUCCESS")
                {
                    var guid = Guid.NewGuid();
                    var wxopenid = result.GetValue("openid").ToString();
                    var openid = Convert.ToString(SQL.SQLTool.ExecuteScalar("SELECT openid FROM WX_members_wx WHERE wxopenid='" + wxopenid + "'"));
                    var mch_id = result.GetValue("mch_id").ToString(); //商户号
                    var money = result.GetValue("total_fee").ToString();   //订单金额
                    var trade_type = result.GetValue("trade_type").ToString();   //交易类型  JSAPI、NATIVE、APP
                    var trade_state = result.GetValue("trade_state").ToString();  //交易状态描述
                    var time_end = result.GetValue("time_end").ToString();   //支付完成时间

                    try
                    {
                        var code = string.Empty;

                        var sql = "INSERT INTO [dbo].[Wx_Pay]([guid],[openid],[mch_id],[money],[result_code],[transaction_id] ,[out_trade_no],[trade_type],[trade_state],[time_end]) values('" + guid + "','" + openid + "','" + mch_id + "','" + money + "','" + result_code + "','" + transaction_id + "','" + out_trade_no + "','" + trade_type + "','" + trade_state + "','" + time_end + "');";

                        var payTypeSql = "select top 1 payType,isnull(postJson,'') postJson,orderno from WX_orderdetails where out_trade_no='" + out_trade_no + "' and openid='" + openid + "'"; //payType:0后支付，1先支付，2打赏，3充值
                        var tempdt = SQL.SQLTool.ExecuteDataTable(payTypeSql);
                        if (tempdt == null)
                        {
                            return;
                        }

                        var orderno = tempdt.Rows[0]["orderno"].ToString();
                        var payType = tempdt.Rows[0][0].ToString();
                        if (payType == "2")  //打赏
                        {
                            sql += "UPDATE WX_Reward set paytime='" + DateTime.Now + "',paystatus='1' where openid='" + openid + "' AND orderno=(SELECT orderno FROM WX_orderdetails WHERE out_trade_no='" + out_trade_no + "');";
                        }
                        else if (payType == "3")
                        {
                            try
                            {
                                var tempJson = tempdt.Rows[0][1].ToString();
                                if (!string.IsNullOrEmpty(tempJson))
                                {
                                    tempJson = tempJson.TrimEnd('}');
                                    tempJson += ",\"pcode\":\"" + out_trade_no + "\"}";
                                }
                                var postData = "actionname=cardrechargebywx&parameters=" + tempJson + "";

                                var postResult = Helper.HttpWebRequestByURL2(Helper.GetAppSettings("VipPostUrl") + "memberCard/WSMemCard.ashx", postData);
                                var msg = JsonHelper.GetJsonValByKey(postResult, "status");
                                Log.Debug(DateTime.Now.ToString() + " 接口请求数据：" + postData, "接口返回:" + msg);
                                if (msg == "0")
                                {
                                    sql += "UPDATE WX_choorderdetail SET status='6' WHERE detailcode='" + orderno + "';";   //更新订单详情表
                                }
                                else
                                {
                                    Log.Debug("微信充值返回错误，微信流水号为：" + out_trade_no, msg);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Debug("微信充值返回错误，微信流水号为：" + out_trade_no, ex.Message);
                            }

                        }
                        else
                        {

                            try
                            {
                                code = LinkPubWx.Tools.RandomCode(); //生成券码
                            }
                            catch (Exception ex)
                            {
                                code = "";
                            }
                            //查询订单编号

                            sql += "UPDATE WX_choorderdetail SET status='6',conmoney='" + money + "' WHERE detailcode='" + orderno + "';";   //更新订单详情表
                            sql += "UPDATE dbo.choorderdetailBreakhistory SET pstatus='4' where detailcodes='" + orderno + "';"; //更新支付表
                            sql += "UPDATE dbo.chopayhistory SET status='1',paymoney='" + money + "' where detailcode='" + orderno + "';";

                        }
                        sql += "update WX_orderdetails set [status]=1,qcCode='" + code + "',paytime=getdate() where out_trade_no='" + out_trade_no + "' and openid='" + openid + "';"; //更新微信订单详情表

                        SQL.SQLTool.ExecuteNonQuery(sql);
                       

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        //通知微信平台该数据已处理成功，无需重复验证
                        WxPayData res = new WxPayData();
                        res.SetValue("return_code", "SUCCESS");
                        res.SetValue("return_msg", "OK");
                        Response.Write(res.ToXml());
                        Response.End();
                    }
                }
                else
                {
                    Log.Error(this.Page.ToString(), "订单查询失败:");
                    WxPayData res = new WxPayData();
                    res.SetValue("return_code", "FAIL");
                    res.SetValue("return_msg", "订单查询失败");
                    Response.Write(res.ToXml());
                    Response.End();
                }
            }

        }
    }
}