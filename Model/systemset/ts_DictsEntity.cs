using System;
using System.Collections.Generic;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("系统字典信息")]
    
    public class ts_DictsEntity
    {
        private long _dicid = 0;
        private string _buscode = Helper.GetAppSettings("BusCode");
        private string _strcode = string.Empty;
        private string _dictype = string.Empty;
        private string _lng = string.Empty;
        private long _pdicid = 0;
        private string _diccode = string.Empty;
        private string _dicname = string.Empty;
        private string _dicvalue = string.Empty;
        private int _orderno = 0;
        private string _remark = string.Empty;
        private string _status = string.Empty;
        private long _cuser = 0;
        private DateTime _ctime = DateTime.Parse("1900-01-01");
        private string _isdelete = string.Empty;

        /// <summary>
        ///标识
        /// <summary>
        public long dicid
        {
            get { return _dicid; }
            set { _dicid = value; }
        }
        /// <summary>
        ///商户编号
        /// <summary>
        [ModelInfo(Name = "商户编号", ControlName = "txt_buscode", NotEmpty = true, Length = 16, NotEmptyECode = "ts_Dicts_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_Dicts_005")]
        public string buscode
        {
            get { return _buscode; }

        }
        /// <summary>
        ///门店编号
        /// <summary>
        [ModelInfo(Name = "门店编号", ControlName = "txt_strcode", NotEmpty = false, Length = 8, NotEmptyECode = "ts_Dicts_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_Dicts_008")]
        public string strcode
        {
            get { return _strcode; }
            set { _strcode = value; }
        }
        /// <summary>
        ///类别
        /// <summary>
        [ModelInfo(Name = "类别", ControlName = "txt_dictype", NotEmpty = false, Length = 32, NotEmptyECode = "ts_Dicts_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_Dicts_011")]
        public string dictype
        {
            get { return _dictype; }
            set { _dictype = value; }
        }
        /// <summary>
        ///语言代码
        /// <summary>
        [ModelInfo(Name = "语言代码", ControlName = "txt_lng", NotEmpty = false, Length = 16, NotEmptyECode = "ts_Dicts_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_Dicts_014")]
        public string lng
        {
            get { return _lng; }
            set { _lng = value; }
        }
        /// <summary>
        ///父ID
        /// <summary>
        [ModelInfo(Name = "父ID", ControlName = "txt_pdicid", NotEmpty = false, Length = 8, NotEmptyECode = "ts_Dicts_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_Dicts_017")]
        public long pdicid
        {
            get { return _pdicid; }
            set { _pdicid = value; }
        }
        /// <summary>
        /// 字典编号
        /// </summary>
        [ModelInfo(Name = "字典编号", ControlName = "txt_diccode", NotEmpty = false, Length = 16, NotEmptyECode = "ts_Dicts_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_Dicts_020")]
        public string diccode
        {
            get { return _diccode; }
            set { _diccode = value; }
        }
        /// <summary>
        ///字典名称
        /// <summary>
        [ModelInfo(Name = "字典名称", ControlName = "txt_dicname", NotEmpty = false, Length = 32, NotEmptyECode = "ts_Dicts_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_Dicts_023")]
        public string dicname
        {
            get { return _dicname; }
            set { _dicname = value; }
        }
        /// <summary>
        ///字典值
        /// <summary>
        [ModelInfo(Name = "字典值", ControlName = "txt_dicvalue", NotEmpty = false, Length = 32, NotEmptyECode = "ts_Dicts_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_Dicts_026")]
        public string dicvalue
        {
            get { return _dicvalue; }
            set { _dicvalue = value; }
        }
        /// <summary>
        ///排序号
        /// <summary>
        [ModelInfo(Name = "排序号", ControlName = "txt_orderno", NotEmpty = false, Length = 4, NotEmptyECode = "ts_Dicts_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_Dicts_029")]
        public int orderno
        {
            get { return _orderno; }
            set { _orderno = value; }
        }
        /// <summary>
        ///备注
        /// <summary>
        [ModelInfo(Name = "备注", ControlName = "txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "ts_Dicts_031", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_Dicts_032")]
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        ///有效状态（0无效，1有效）
        /// <summary>
        [ModelInfo(Name = "有效状态（0无效，1有效）", ControlName = "txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "ts_Dicts_034", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "ts_Dicts_035")]
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        ///引用系统用户表ts_admins的userid字段值
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
        ///是否删除（0未删除，1已删除，默认值为0）
        /// <summary>
        public string isdelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }
    }

    public class ts_DictDto
    {
        public int id { get; set; }
        public string pId { get; set; }
        public string name { get; set; }
        public bool open { get; set; }
        public bool isParent { get; set; }
        public string iconClose { get; set; }
        public string iconOpen { get; set; }
        public string icon { get; set; }
        public List<ts_DictDto> children { get; set; }
    }


    //public class ts_DictlistDto
    //{
    //    public int id { get; set; }
    //    public int pId { get; set; }
    //    public string name { get; set; }
    //    public bool open { get; set; }
    //    public bool isParent { get; set; }
    //    public string iconSkin { get; set; }


    //}




}