using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("设置时间")]
    [Serializable]
    public class WX_settimeEntity
    {
		private int _timeid = 0;
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
		private DateTime _btime = DateTime.Parse("1900-01-01");
		private DateTime _etime = DateTime.Parse("1900-01-01");

		/// <summary>
		///标识id
		/// <summary>
		public int timeid
		{
			get { return _timeid; }
			set { _timeid = value; }
		}
		/// <summary>
		///所属门店
		/// <summary>
		[ModelInfo(Name = "所属门店",ControlName="txt_stocode", NotEmpty = false, Length = 16, NotEmptyECode = "WX_settime_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_settime_005")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "WX_settime_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_settime_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///名称
		/// <summary>
		[ModelInfo(Name = "名称",ControlName="txt_timename", NotEmpty = false, Length = 12, NotEmptyECode = "WX_settime_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_settime_011")]
		public string timename
		{
			get { return _timename; }
			set { _timename = value; }
		}
		/// <summary>
		///值
		/// <summary>
		[ModelInfo(Name = "值",ControlName="txt_timevalue", NotEmpty = false, Length = 8, NotEmptyECode = "WX_settime_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_settime_014")]
		public string timevalue
		{
			get { return _timevalue; }
			set { _timevalue = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "WX_settime_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_settime_017")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		/// <summary>
		///状态
		/// <summary>
		[ModelInfo(Name = "状态",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "WX_settime_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_settime_020")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}
		/// <summary>
		///是否删除
		/// <summary>
		public string isdelete
		{
			get { return _isdelete; }
			set { _isdelete = value; }
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
		///开始时间
		/// <summary>
		[ModelInfo(Name = "开始时间",ControlName="txt_btime", NotEmpty = false, Length = 19, NotEmptyECode = "WX_settime_037", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_settime_038")]
		public DateTime btime
		{
			get { return _btime; }
			set { _btime = value; }
		}
		/// <summary>
		///结束时间
		/// <summary>
		[ModelInfo(Name = "结束时间",ControlName="txt_etime", NotEmpty = false, Length = 19, NotEmptyECode = "WX_settime_040", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_settime_041")]
		public DateTime etime
		{
			get { return _etime; }
			set { _etime = value; }
		}        
    }
}