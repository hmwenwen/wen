
using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("系统登录日志")]
    
    public class tl_loginlogEntity : BaseModel
    {
        private long _logid = 0;
   
        private string _strcode = string.Empty;
        private long _userid = 0;
        private string _cname = string.Empty;
        private string _ip = string.Empty;
        private string _logcontent = string.Empty;
        private DateTime _ctime = DateTime.Parse("1900-01-01");

        /// <summary>
        ///标识
        /// <summary>
        public long logid
        {
            get { return _logid; }
            set { _logid = value; }
        }

        /// <summary>
        ///门店编号
        /// <summary>
        [ModelInfo(Name = "门店编号", ControlName = "txt_strcode", NotEmpty = false, Length = 8, NotEmptyECode = "tl_loginlog_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "tl_loginlog_008")]
        public string strcode
        {
            get { return _strcode; }
            set { _strcode = value; }
        }
        /// <summary>
        ///引用系统用户表ts_admins的userid字段值
        /// <summary>
        [ModelInfo(Name = "引用系统用户表ts_admins的userid字段值", ControlName = "txt_userid", NotEmpty = false, Length = 8, NotEmptyECode = "tl_loginlog_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "tl_loginlog_011")]
        public long userid
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        ///登录姓名
        /// <summary>
        [ModelInfo(Name = "登录姓名", ControlName = "txt_cname", NotEmpty = false, Length = 32, NotEmptyECode = "tl_loginlog_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "tl_loginlog_014")]
        public string cname
        {
            get { return _cname; }
            set { _cname = value; }
        }
        /// <summary>
        ///登录IP
        /// <summary>
        [ModelInfo(Name = "登录IP", ControlName = "txt_ip", NotEmpty = false, Length = 32, NotEmptyECode = "tl_loginlog_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "tl_loginlog_017")]
        public string ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        /// <summary>
        ///日志信息
        /// <summary>
        [ModelInfo(Name = "日志信息", ControlName = "txt_logcontent", NotEmpty = false, Length = 256, NotEmptyECode = "tl_loginlog_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "tl_loginlog_020")]
        public string logcontent
        {
            get { return _logcontent; }
            set { _logcontent = value; }
        }
        /// <summary>
        ///创建时间
        /// <summary>
        public DateTime ctime
        {
            get { return _ctime; }
            set { _ctime = value; }
        }
    }
}