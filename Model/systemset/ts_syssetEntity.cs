
using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("系统设置")]
    
    public class ts_syssetEntity : BaseModel
    {
        private int _setid = 0;
        private string _stocode = string.Empty;

        private string _key = string.Empty;
        private string _val = string.Empty;
        private int _status = 0;
        private string _descr = string.Empty;
        private string _explain = string.Empty;
        private DateTime _ctime = DateTime.Parse("1900-01-01");

        /// <summary>
        /// 说明
        /// </summary>
        public string explain 
        {
            get { return _explain; }
            set { _explain = value; }
        }

        /// <summary>
        ///设置标识
        /// <summary>
        public int setid
        {
            get { return _setid; }
            set { _setid = value; }
        }
        /// <summary>
        ///引用门店表Store的门店编号字段stocode的值
        /// <summary>
        [ModelInfo(Name = "引用门店表Store的门店编号字段stocode的值", ControlName = "txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "ts_sysset_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_sysset_005")]
        public string stocode
        {
            get { return _stocode; }
            set { _stocode = value; }
        }

        /// <summary>
        ///键
        /// <summary>
        [ModelInfo(Name = "键", ControlName = "txt_key", NotEmpty = false, Length = 32, NotEmptyECode = "ts_sysset_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_sysset_011")]
        public string key
        {
            get { return _key; }
            set { _key = value; }
        }
        /// <summary>
        ///值
        /// <summary>
        [ModelInfo(Name = "值", ControlName = "txt_val", NotEmpty = false, Length = 16, NotEmptyECode = "ts_sysset_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_sysset_014")]
        public string val
        {
            get { return _val; }
            set { _val = value; }
        }
        /// <summary>
        ///有效状态（0无效，1有效）
        /// <summary>
        [ModelInfo(Name = "有效状态（0无效，1有效）", ControlName = "txt_status", NotEmpty = false, Length = 4, NotEmptyECode = "ts_sysset_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_sysset_017")]
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        ///描述
        /// <summary>
        [ModelInfo(Name = "描述", ControlName = "txt_descr", NotEmpty = false, Length = 128, NotEmptyECode = "ts_sysset_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_sysset_020")]
        public string descr
        {
            get { return _descr; }
            set { _descr = value; }
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