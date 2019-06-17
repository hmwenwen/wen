using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XJWZCatering.DAL;

namespace XJWZCatering.BLL
{
    public class bllCoupon
    {
        dalCoupon dal = new dalCoupon();

        //获取用户优惠券列表
        public DataTable GetCouponList(string openid, string type, string currentpage, string pagesize, ref string sumcount)
        {
            return dal.GetCouponList(openid, type, currentpage, pagesize, ref sumcount);
        }

        //根据优惠券号获取优惠券明细
        public DataTable GetCouponDetail(string couponcode)
        {
            return dal.GetCouponDetail(couponcode);
        }

        //根据订单消费金额获取满足条件的优惠券信息
        public DataTable GetCouponListByMoney(string openid, string stocode, string money)
        {
            return dal.GetCouponListByMoney(openid, stocode, money);
        }

        //获取用户会员卡折扣信息及优惠券信息
        public DataSet GetCardAndCouponList(string openid, string stocode, string money, string discodes)
        {
            return dal.GetCardAndCouponList(openid, stocode, money, discodes);
        }

        //优惠券发放
        //优惠券活动code ,会员code,发放门店code
        public int ReceiveCoupon(string mccode, string memcode, string ffstocode, ref string mescode)
        {
            return dal.ReceiveCoupon(mccode, memcode, ffstocode, ref mescode);
        }

        //获取用户优惠券列表(影城）（小程序使用）
        public DataTable GetMVCouponList(string memcode, string type, string currentpage, string pagesize, ref string sumcount)
        {
            return dal.GetMVCouponList(memcode, type, currentpage, pagesize, ref sumcount);
        }

        //根据消费的金额获取满足条件的用户优惠券信息（影城券）（小程序使用）
        public DataTable GetMovieMainCouponByMoney(string stocode, string memcode, string money, string discode, string discodes)
        {
            return dal.GetMovieMainCouponByMoney(stocode, memcode, money, discode, discodes);
        }

        /// <summary>
        /// 分享优惠券
        /// </summary>
        /// <param name="GUID"></param>
        /// <param name="UID"></param>
        /// <param name="memcode"></param>
        /// <param name="checkcode"></param>
        /// <param name="mescode"></param>
        /// <returns></returns>
        public int ShareCoupon(string GUID, string UID, string memcode, string checkcode, ref string mescode)
        {
            //if (!CheckLogin(GUID, UID))//非法登录
            //{
            //    return 1;
            //}
            return dal.ShareCoupon(UID, memcode, checkcode, ref mescode);
        }

        /// <summary>
        /// 领取优惠券
        /// </summary>
        /// <param name="GUID"></param>
        /// <param name="UID"></param>
        /// <param name="memcode"></param>
        /// <param name="checkcode"></param>
        /// <param name="mescode"></param>
        /// <returns></returns>
        public int GetShareCoupon(string GUID, string UID, string memcode, string checkcode, ref string mescode)
        {
            //if (!CheckLogin(GUID, UID))//非法登录
            //{
            //    return 1;
            //}
            return dal.GetShareCoupon(UID, memcode, checkcode, ref mescode);
        }

        /// <summary>
        /// 获取需要推送消息的优惠券用户
        /// </summary>
        /// <param name="tipsCycle"></param>
        /// <returns></returns>
        public DataTable GetPushCoupon(string tipsCycle)
        {
            return dal.GetPushCoupon(tipsCycle);
        }
    }
}
