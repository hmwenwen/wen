using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XJWZCatering.DAL;

namespace XJWZCatering.BLL
{
    public class bllReward
    {
        dalWXReward dal = new dalWXReward();

        /// <summary>
        /// 根据门店编号及员工编号获取打赏信息
        /// </summary>
        /// <param name="empcode"></param>
        /// <param name="stocode"></param>
        /// <returns></returns>
        public DataTable GetWXRewardInfo(string empcode, string stocode)
        {
            return dal.GetWXRewardInfo(empcode, stocode);
        }

        /// <summary>
        /// 打赏
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="empcode"></param>
        /// <param name="money"></param>
        /// <param name="point"></param>
        /// <param name="rcontent"></param>
        /// <param name="rid"></param>
        public void AddWXReward(string openid, string empcode, string money, string point, string rcontent, ref string rid, ref string orderno)
        {
            dal.AddWXReward(openid, empcode, money, point, rcontent, ref rid, ref orderno);
        }

        /// <summary>
        /// 更改打赏支付状态
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public int UpdateWXRewardStatus(string empcode, string rid)
        {
            return dal.UpdateWXRewardStatus(empcode, rid);
        }
    }
}
