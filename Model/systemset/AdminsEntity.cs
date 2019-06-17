using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;

namespace XJWZCatering.Model
{
    [Description("后台用户信息")]

    public class AdminsEntity
    {
        private long _userid = 0;
        private string _buscode = Helper.GetAppSettings("BusCode");
        private string _strcode = string.Empty;
        private string _username = string.Empty;
        private string _upwd = string.Empty;
        private string _realname = string.Empty;
        private string _umobile = string.Empty;
        private string _empcode = string.Empty;
        private string _remark = string.Empty;
        private string _status = string.Empty;
        //固定字段
        private long _cuser = 0;
        private DateTime _ctime = DateTime.Parse("1900-01-01");
        private long _uuser = 0;
        private DateTime _utime = DateTime.Parse("1900-01-01");
        private string _isdelete = string.Empty;

        private string _scope = string.Empty;
        private string _stocode = string.Empty;

        public string scope
        {
            get { return _scope; }
            set { _scope = value; }
        }

        public string stocode
        {
            get { return _stocode; }
            set { _stocode = value; }
        }


        #region //扩展字段 tsg
        private string _GUID = string.Empty;
        /// <summary>
        ///令牌
        /// <summary>
        public string GUID
        {
            get { return _GUID; }
            set { _GUID = value; }
        }

        private string _roleids = string.Empty;
        /// <summary>
        ///用户角色
        /// <summary>
        public string roleids
        {
            get { return _roleids; }
            set { _roleids = value; }
        }

        #endregion

        /// <summary>
        ///用户标识
        /// <summary>
        public long userid
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        ///商户编号
        /// <summary>
        [ModelInfo(Name = "商户编号", ControlName = "txt_buscode", NotEmpty = true, Length = 16, NotEmptyECode = "admins_001", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "admins_002")]
        public string buscode
        {
            get { return _buscode; }
            set { _buscode = value; }
        }
        /// <summary>
        ///门店编号
        /// <summary>
        [ModelInfo(Name = "门店编号", ControlName = "txt_strcode", NotEmpty = true, Length = 16, NotEmptyECode = "admins_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "admins_005")]
        public string strcode
        {
            get { return _strcode; }
            set { _strcode = value; }
        }
        /// <summary>
        ///用户名
        /// <summary>
        [ModelInfo(Name = "用户名", ControlName = "txt_uname", NotEmpty = true, Length = 16, NotEmptyECode = "admins_007", RType = RegularExpressions.RegExpType.Username, RTypeECode = "admins_008")]
        public string uname
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        ///密码
        /// <summary>
        [ModelInfo(Name = "密码", ControlName = "txt_upwd", NotEmpty = true, Length = 32, NotEmptyECode = "admins_010", RType = RegularExpressions.RegExpType.Password, RTypeECode = "admins_011")]
        public string upwd
        {
            get { return _upwd; }
            set { _upwd = value; }
        }
        /// <summary>
        ///姓名
        /// <summary>
        [ModelInfo(Name = "姓名", ControlName = "txt_realname", NotEmpty = true, Length = 32, NotEmptyECode = "admins_013", RType = RegularExpressions.RegExpType.Normal)]
        public string realname
        {
            get { return _realname; }
            set { _realname = value; }
        }
        /// <summary>
        ///手机号
        /// <summary>
        [ModelInfo(Name = "手机号", ControlName = "txt_umobile", NotEmpty = false, Length = 32, NotEmptyECode = "admins_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "admins_017")]
        public string umobile
        {
            get { return _umobile; }
            set { _umobile = value; }
        }
        /// <summary>
        ///员工编号
        /// <summary>
        [ModelInfo(Name = "员工编号", ControlName = "txt_empcode", NotEmpty = false, Length = 32, NotEmptyECode = "admins_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "admins_020")]
        public string empcode
        {
            get { return _empcode; }
            set { _empcode = value; }
        }
        /// <summary>
        ///备注
        /// <summary>
        [ModelInfo(Name = "备注", ControlName = "txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "admins_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "admins_023")]
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        ///状态
        /// <summary>
        [ModelInfo(Name = "状态", ControlName = "ddl_status", NotEmpty = true, Length = 1, NotEmptyECode = "admins_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "admins_026")]
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        ///创建人
        /// <summary>
        public long cuser
        {
            get { return _cuser; }
            set { _cuser = value; }
        }
        /// <summary>
        ///创建时间
        /// <summary>
        public DateTime ctime
        {
            get { return _ctime; }
            set { _ctime = value; }
        }
        /// <summary>
        ///修改人
        /// <summary>
        public long uuser
        {
            get { return _uuser; }
            set { _uuser = value; }
        }
        /// <summary>
        ///修改时间
        /// <summary>
        public DateTime utime
        {
            get { return _utime; }
            set { _utime = value; }
        }

        /// <summary>
        ///删除标志
        /// <summary>
        public string isdelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }


    }
}