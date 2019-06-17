using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("设置排队")]
    
    public class setlineUpEntity
    {
		private string _linecode = string.Empty;
		private string _stocode = string.Empty;
		private string _buscode = string.Empty;
		private string _ttcode = string.Empty;
		private int _maxperson = 0;
		private int _minperosn = 0;
		private int _Turncycle = 0;
		private string _remark = string.Empty;
		private string _status = string.Empty;
		private long _cuser = 0;
		private long _uuser = 0;
		private DateTime _utime = DateTime.Parse("1900-01-01");
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private string _isdelete = string.Empty;

		/// <summary>
		///
		/// <summary>
		public string linecode
		{
			get { return _linecode; }
			set { _linecode = value; }
		}
		/// <summary>
		///引用门店表Store的门店编号字段stocode的值
		/// <summary>
		[ModelInfo(Name = "引用门店表Store的门店编号字段stocode的值",ControlName="txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "setlineUp_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setlineUp_005")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///引用商户表Business的商户编号字段buscode的值
		/// <summary>
		[ModelInfo(Name = "引用商户表Business的商户编号字段buscode的值",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "setlineUp_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setlineUp_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///类型编号
		/// <summary>
		[ModelInfo(Name = "类型编号",ControlName="txt_ttcode", NotEmpty = false, Length = 8, NotEmptyECode = "setlineUp_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setlineUp_011")]
		public string ttcode
		{
			get { return _ttcode; }
			set { _ttcode = value; }
		}
		/// <summary>
		///最大人数
		/// <summary>
		[ModelInfo(Name = "最大人数",ControlName="txt_maxperson", NotEmpty = false, Length = 4, NotEmptyECode = "setlineUp_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setlineUp_014")]
		public int maxperson
		{
			get { return _maxperson; }
			set { _maxperson = value; }
		}
		/// <summary>
		///最小人数
		/// <summary>
		[ModelInfo(Name = "最小人数",ControlName="txt_minperosn", NotEmpty = false, Length = 4, NotEmptyECode = "setlineUp_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setlineUp_017")]
		public int minperosn
		{
			get { return _minperosn; }
			set { _minperosn = value; }
		}
		/// <summary>
		///翻台周期
		/// <summary>
		[ModelInfo(Name = "翻台周期",ControlName="txt_Turncycle", NotEmpty = false, Length = 4, NotEmptyECode = "setlineUp_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setlineUp_020")]
		public int Turncycle
		{
			get { return _Turncycle; }
			set { _Turncycle = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "setlineUp_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setlineUp_023")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		/// <summary>
		///有效状态（排队是否开启）
		/// <summary>
		[ModelInfo(Name = "有效状态（排队是否开启）",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "setlineUp_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setlineUp_026")]
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
		///引用系统用户表ts_admins的userid字段值
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
		///创建时间
		/// <summary>
		public DateTime ctime
		{
			get { return _ctime; }
			set { _ctime = value; }
		}
		/// <summary>
		///删除标志（0未删除，1已删除，默认值为0）
		/// <summary>
		public string isdelete
		{
			get { return _isdelete; }
			set { _isdelete = value; }
		}        
    }
}