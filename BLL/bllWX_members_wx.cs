using System.Collections.Generic;
using System.Data;
using XJWZCatering.CommonBasic;
using XJWZCatering.DAL;
using XJWZCatering.Model;
namespace XJWZCatering.BLL
{
    /// <summary>
    /// 业务类
    /// </summary>
    public class bllWX_members_wx : bllBase
    {
        dalWX_members_wx dal = new dalWX_members_wx();
        WX_members_wxEntity Entity = new WX_members_wxEntity();

        /// <summary>
        /// 检验表单数据
        /// </summary>
        /// <returns></returns>
        public string CheckPageInfo(string type, string id, string openid, string subscribe, string nickname, string sex, string language, string cityid, string provinceid, string country, string mobile, string headimgurl, string subscribe_scene, string upwd, string notpwd, string wxopenid, out string spanids)
        {
            string strRetuen = string.Empty;
            spanids = string.Empty;
            //要验证的实体属性
            List<string> EName = new List<string>() { };
            //要验证的实体属性值
            List<string> EValue = new List<string>() { };
            //错误信息
            List<string> errorCode = new List<string>();
            List<string> ControlName = new List<string>();
            //验证数据
            CheckValue<WX_members_wxEntity>(EName, EValue, ref errorCode, ref ControlName, new WX_members_wxEntity());
            //特殊验证写在下面

            if (errorCode.Count > 0)
            {
                strRetuen = ErrMessage.GetMessageInfoByListCode(errorCode);
                spanids = ListTostring(ControlName);
            }
            else//组合对象数据
            {
                Entity = new WX_members_wxEntity();
                Entity.id = Helper.StringToLong(id);
                Entity.openid = openid;
                Entity.subscribe = subscribe;
                Entity.nickname = nickname;
                Entity.sex = sex;
                Entity.language = language;
                Entity.cityid = cityid;
                Entity.provinceid = provinceid;
                Entity.country = country;
                Entity.mobile = mobile;
                Entity.headimgurl = headimgurl;
                Entity.subscribe_scene = subscribe_scene;

                Entity.upwd = upwd;
                Entity.notpwd = notpwd;
                Entity.wxopenid = wxopenid;
            }
            return strRetuen;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public DataTable Add(out string id, string openid, string subscribe, string nickname, string sex, string language, string cityid, string provinceid, string country, string mobile, string headimgurl, string subscribe_scene, string upwd, string notpwd, string wxopenid)
        {
            id = "0";
            //if (!CheckLogin(GUID, UID))//非法登录
            //{
            //    return dtBase;
            //}

            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("add", id, openid, subscribe, nickname, sex, language, cityid, provinceid, country, mobile, headimgurl, subscribe_scene, upwd, notpwd, wxopenid, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
            int result = dal.Add(ref Entity);
            //检测执行结果
            CheckResult(result);
            return dtBase;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public DataTable Update(string id, string openid, string subscribe, string nickname, string sex, string language, string cityid, string provinceid, string country, string mobile, string headimgurl, string subscribe_scene, string upwd, string notpwd, string wxopenid)
        {

            dtBase.Clear();
            string spanids = string.Empty;
            string strReturn = CheckPageInfo("update", id, openid, subscribe, nickname, sex, language, cityid, provinceid, country, mobile, headimgurl, subscribe_scene, upwd, notpwd, wxopenid, out spanids);
            //数据页面验证
            if (!CheckControl(strReturn, spanids))
            {
                return dtBase;
            }
            //获取更新前的数据对象
            WX_members_wxEntity OldEntity = new WX_members_wxEntity();
            OldEntity = GetEntitySigInfo(" where openid='" + openid + "'");
            //更新数据
            int result = dal.Update(Entity);
            //检测执行结果
            //if (CheckResult(result))
            //{
            ////写日志
            //if (entity != null)
            //{
            //    blllog.Add<WX_members_wxEntity>(entity, Entity, OldEntity);
            //}
            //}
            return dtBase;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public DataTable UpdateStatus(string GUID, string UID, string ids, string Status)
        {
            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            int result = dal.UpdateStatus(ids, Status);
            //检测执行结果
            CheckResult(result);
            return dtBase;
        }


        /// <summary>
        /// 更新单个字段
        /// </summary>
        /// <returns></returns>
        public DataTable UpdateField(string GUID, string UID, string openid, string fName, string fVal)
        {
            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            int result = dal.UpdateField(openid, fName, fVal);
            //检测执行结果
            CheckResult(result);
            return dtBase;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>返回操作结果</returns>
        public DataTable Delete(string GUID, string UID, string id, operatelogEntity entity)
        {
            if (!CheckLogin(GUID, UID))//非法登录
            {
                return dtBase;
            }
            dtBase.Clear();
            string Mescode = string.Empty;
            int result = dal.Delete(id, ref Mescode);
            //检测执行结果
            if (CheckDeleteResult(result, Mescode))
            {
                //写日志
                if (entity != null)
                {
                    blllog.Add(entity);
                }

            }
            return dtBase;
        }

        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="filter">指定条件</param>
        /// <returns>返回第一行</returns>
        public DataTable GetPagingSigInfo(string GUID, string UID, string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            return GetPagingListInfo(GUID, UID, 1, 1, filter, string.Empty, out recnums, out pagenums);
        }

        /// <summary>
        /// 获取单条数据实体对象
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public WX_members_wxEntity GetEntitySigInfo(string filter)
        {
            int recnums = 0;
            int pagenums = 0;
            DataTable dt = GetPagingListInfo("", "0", 1, 1, filter, string.Empty, out recnums, out pagenums);
            if (dt != null && dt.Rows.Count > 0)
            {
                return SetEntityInfo(dt.Rows[0]);
            }
            return new WX_members_wxEntity();
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentpage"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <param name="recnums"></param>
        /// <returns></returns>
        public DataTable GetPagingListInfo(string GUID, string UID, int pageSize, int currentpage, string filter, string order, out int recnums, out int pagenums)
        {
            if (!CheckLogin(GUID, UID))//非法登录
            {
                recnums = -1;
                pagenums = -1;
                return dtBase;
            }
            return new bllPaging().GetPagingInfo("WX_members_wx", "id", "*", pageSize, currentpage, filter, "", order, out recnums, out pagenums);
        }

        /// <summary>
        /// 单行数据转实体对象
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private WX_members_wxEntity SetEntityInfo(DataRow dr)
        {
            WX_members_wxEntity Entity = new WX_members_wxEntity();
            Entity.id = Helper.StringToLong(dr["id"].ToString());
            Entity.openid = dr["openid"].ToString();
            Entity.subscribe = dr["subscribe"].ToString();
            Entity.nickname = dr["nickname"].ToString();
            Entity.sex = dr["sex"].ToString();
            Entity.language = dr["language"].ToString();
            Entity.cityid = dr["cityid"].ToString();
            Entity.provinceid = dr["provinceid"].ToString();
            Entity.country = dr["country"].ToString();
            Entity.mobile = dr["mobile"].ToString();
            Entity.headimgurl = dr["headimgurl"].ToString();
            Entity.subscribe_scene = dr["subscribe_scene"].ToString();

            Entity.upwd = dr["upwd"].ToString();
            Entity.notpwd = dr["notpwd"].ToString();
            return Entity;
        }

        public int ModifyCityByOpenid(string openid, string proname, string cityname, string areaname)
        {
            return dal.ModifyCityByOpenid(openid, proname, cityname, areaname);
        }

        public int ModifyInfoByOpenid(string openid, string type, string value)
        {
            return dal.ModifyInfoByOpenid(openid, type, value);
        }

        //设置小额免密
        public int SetnotPwd(string openid, string notpwd, string amount, ref string mescode)
        {
            return dal.SetnotPwd(openid, notpwd, amount, ref mescode);
        }

        //设置手机号
        public int SetPhone(string openid, string mobile, ref string mescode)
        {
            return dal.SetPhone(openid, mobile, ref mescode);
        }

        //设置支付密码
        public int SetUPwd(string openid, string upwd, ref string mescode)
        {
            return dal.SetUPwd(openid, upwd, ref mescode);
        }

        //设置支付密码（新）
        public int SetUPwdNew(string openid, string mobile, string idno, string upwd, ref string mescode)
        {
            return dal.SetUPwdNew(openid, mobile, idno, upwd, ref mescode);
        }
    }
}