using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("设置时间")]
    
    public class settimeEntity
    {
		private string _timecode = string.Empty;
		private string _stocode = string.Empty;
		private string _buscode = string.Empty;
		private string _timename = string.Empty;
		private string _timevalue = string.Empty;
		private string _remark = string.Empty;
		private string _status = string.Empty;
		private string _isdelete = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private long _uuser = 0;
		private DateTime _utime = DateTime.Parse("1900-01-01");

		/// <summary>
		///标示
		/// <summary>
		public string timecode
		{
			get { return _timecode; }
			set { _timecode = value; }
		}
		/// <summary>
		///引用门店表Store的门店编号字段stocode的值
		/// <summary>
		[ModelInfo(Name = "引用门店表Store的门店编号字段stocode的值",ControlName="txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "settime_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "settime_005")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///引用商户表Business的商户编号字段buscode的值
		/// <summary>
		[ModelInfo(Name = "引用商户表Business的商户编号字段buscode的值",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "settime_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "settime_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///名称
		/// <summary>
		[ModelInfo(Name = "名称",ControlName="txt_timename", NotEmpty = false, Length = 12, NotEmptyECode = "settime_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "settime_011")]
		public string timename
		{
			get { return _timename; }
			set { _timename = value; }
		}
		/// <summary>
		///置顶
		/// <summary>
		[ModelInfo(Name = "置顶",ControlName="txt_timevalue", NotEmpty = false, Length = 8, NotEmptyECode = "settime_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "settime_014")]
		public string timevalue
		{
			get { return _timevalue; }
			set { _timevalue = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "settime_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "settime_017")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		/// <summary>
		///有效状态（0无效，1有效）
		/// <summary>
		[ModelInfo(Name = "有效状态（0无效，1有效）",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "settime_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "settime_020")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}
		/// <summary>
		///是否删除（0未删除，1已删除，默认值为0）
		/// <summary>
		public string isdelete
		{
			get { return _isdelete; }
			set { _isdelete = value; }
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
    }
}