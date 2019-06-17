using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("客户排队表")]
    [Serializable]
    public class WX_busWaitEntity
    {
		private long _bwid = 0;
		private string _serialNumber = string.Empty;
		private string _buscode = string.Empty;
		private string _strcode = string.Empty;
		private int _busTop = 0;
		private string _sortNum = string.Empty;
		private DateTime _busDate = DateTime.Parse("1900-01-01");
		private string _waitType = string.Empty;
		private string _userName = string.Empty;
		private string _tel = string.Empty;
		private DateTime _waitTime = DateTime.Parse("1900-01-01");
		private string _remark = string.Empty;
		private string _status = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private long _uuser = 0;
		private DateTime _utime = DateTime.Parse("1900-01-01");
		private string _isdelete = string.Empty;
		private string _openid = string.Empty;

		/// <summary>
		///标识id
		/// <summary>
		public long bwid
		{
			get { return _bwid; }
			set { _bwid = value; }
		}
		/// <summary>
		///流水号
		/// <summary>
		[ModelInfo(Name = "流水号",ControlName="txt_serialNumber", NotEmpty = false, Length = 20, NotEmptyECode = "WX_busWait_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_005")]
		public string serialNumber
		{
			get { return _serialNumber; }
			set { _serialNumber = value; }
		}
		/// <summary>
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "WX_busWait_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///门店编号
		/// <summary>
		[ModelInfo(Name = "门店编号",ControlName="txt_strcode", NotEmpty = false, Length = 8, NotEmptyECode = "WX_busWait_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_011")]
		public string strcode
		{
			get { return _strcode; }
			set { _strcode = value; }
		}
		/// <summary>
		///置顶
		/// <summary>
		[ModelInfo(Name = "置顶",ControlName="txt_busTop", NotEmpty = false, Length = 9, NotEmptyECode = "WX_busWait_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_014")]
		public int busTop
		{
			get { return _busTop; }
			set { _busTop = value; }
		}
		/// <summary>
		///排队号
		/// <summary>
		[ModelInfo(Name = "排队号",ControlName="txt_sortNum", NotEmpty = false, Length = 8, NotEmptyECode = "WX_busWait_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_017")]
		public string sortNum
		{
			get { return _sortNum; }
			set { _sortNum = value; }
		}
		/// <summary>
		///日期
		/// <summary>
		[ModelInfo(Name = "日期",ControlName="txt_busDate", NotEmpty = false, Length = 19, NotEmptyECode = "WX_busWait_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_020")]
		public DateTime busDate
		{
			get { return _busDate; }
			set { _busDate = value; }
		}
		/// <summary>
		///排队类型
		/// <summary>
		[ModelInfo(Name = "排队类型",ControlName="txt_waitType", NotEmpty = false, Length = 20, NotEmptyECode = "WX_busWait_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_023")]
		public string waitType
		{
			get { return _waitType; }
			set { _waitType = value; }
		}
		/// <summary>
		///姓名
		/// <summary>
		[ModelInfo(Name = "姓名",ControlName="txt_userName", NotEmpty = false, Length = 20, NotEmptyECode = "WX_busWait_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_026")]
		public string userName
		{
			get { return _userName; }
			set { _userName = value; }
		}
		/// <summary>
		///联系方式
		/// <summary>
		[ModelInfo(Name = "联系方式",ControlName="txt_tel", NotEmpty = false, Length = 20, NotEmptyECode = "WX_busWait_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_029")]
		public string tel
		{
			get { return _tel; }
			set { _tel = value; }
		}
		/// <summary>
		///排队时间
		/// <summary>
		[ModelInfo(Name = "排队时间",ControlName="txt_waitTime", NotEmpty = false, Length = 19, NotEmptyECode = "WX_busWait_031", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_032")]
		public DateTime waitTime
		{
			get { return _waitTime; }
			set { _waitTime = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "WX_busWait_034", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_035")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		/// <summary>
		///状态
		/// <summary>
		[ModelInfo(Name = "状态",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "WX_busWait_037", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_038")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}
		/// <summary>
		///创建人标识
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
		///最后更新人标识
		/// <summary>
		public long uuser
		{
			get { return _uuser; }
			set { _uuser = value; }
		}
		/// <summary>
		///更新时间
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
		/// <summary>
		///openid
		/// <summary>
		[ModelInfo(Name = "openid",ControlName="txt_openid", NotEmpty = false, Length = 32, NotEmptyECode = "WX_busWait_055", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busWait_056")]
		public string openid
		{
			get { return _openid; }
			set { _openid = value; }
		}        
    }
}