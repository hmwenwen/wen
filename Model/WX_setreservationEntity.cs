using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("设置预定")]
    [Serializable]
    public class WX_setreservationEntity
    {
		private int _setreservationid = 0;
		private string _stocode = string.Empty;
		private string _buscode = string.Empty;
		private string _ttcode = string.Empty;
		private int _maxdeposit = 0;
		private string _methoddeposit = string.Empty;
		private int _nolimitdate = 0;
		private int _daydeposit = 0;
		private string _Duetype = string.Empty;
		private string _remark = string.Empty;
		private string _status = string.Empty;
		private long _cuser = 0;
		private long _uuser = 0;
		private DateTime _utime = DateTime.Parse("1900-01-01");
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private string _isdelete = string.Empty;
		private DateTime _btime = DateTime.Parse("1900-01-01");
		private DateTime _etime = DateTime.Parse("1900-01-01");

		/// <summary>
		///标识id
		/// <summary>
		public int setreservationid
		{
			get { return _setreservationid; }
			set { _setreservationid = value; }
		}
		/// <summary>
		///所属门店
		/// <summary>
		[ModelInfo(Name = "所属门店",ControlName="txt_stocode", NotEmpty = false, Length = 16, NotEmptyECode = "WX_setreservation_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_005")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "WX_setreservation_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///桌台类型编号
		/// <summary>
		[ModelInfo(Name = "桌台类型编号",ControlName="txt_ttcode", NotEmpty = false, Length = 8, NotEmptyECode = "WX_setreservation_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_011")]
		public string ttcode
		{
			get { return _ttcode; }
			set { _ttcode = value; }
		}
		/// <summary>
		///预定上限
		/// <summary>
		[ModelInfo(Name = "预定上限",ControlName="txt_maxdeposit", NotEmpty = false, Length = 9, NotEmptyECode = "WX_setreservation_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_014")]
		public int maxdeposit
		{
			get { return _maxdeposit; }
			set { _maxdeposit = value; }
		}
		/// <summary>
		///预定方式
		/// <summary>
		[ModelInfo(Name = "预定方式",ControlName="txt_methoddeposit", NotEmpty = false, Length = 2, NotEmptyECode = "WX_setreservation_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_017")]
		public string methoddeposit
		{
			get { return _methoddeposit; }
			set { _methoddeposit = value; }
		}
		/// <summary>
		///提前预定不限
		/// <summary>
		[ModelInfo(Name = "提前预定不限",ControlName="txt_nolimitdate", NotEmpty = false, Length = 9, NotEmptyECode = "WX_setreservation_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_020")]
		public int nolimitdate
		{
			get { return _nolimitdate; }
			set { _nolimitdate = value; }
		}
		/// <summary>
		///提前预定天数
		/// <summary>
		[ModelInfo(Name = "提前预定天数",ControlName="txt_daydeposit", NotEmpty = false, Length = 9, NotEmptyECode = "WX_setreservation_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_023")]
		public int daydeposit
		{
			get { return _daydeposit; }
			set { _daydeposit = value; }
		}
		/// <summary>
		///餐别设置
		/// <summary>
		[ModelInfo(Name = "餐别设置",ControlName="txt_Duetype", NotEmpty = false, Length = 16, NotEmptyECode = "WX_setreservation_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_026")]
		public string Duetype
		{
			get { return _Duetype; }
			set { _Duetype = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "WX_setreservation_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_029")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		/// <summary>
		///状态
		/// <summary>
		[ModelInfo(Name = "状态",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "WX_setreservation_031", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_032")]
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
		///创建时间
		/// <summary>
		public DateTime ctime
		{
			get { return _ctime; }
			set { _ctime = value; }
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
		///供应开始时间
		/// <summary>
		[ModelInfo(Name = "供应开始时间",ControlName="txt_btime", NotEmpty = false, Length = 19, NotEmptyECode = "WX_setreservation_049", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_050")]
		public DateTime btime
		{
			get { return _btime; }
			set { _btime = value; }
		}
		/// <summary>
		///供应结束时间
		/// <summary>
		[ModelInfo(Name = "供应结束时间",ControlName="txt_etime", NotEmpty = false, Length = 19, NotEmptyECode = "WX_setreservation_052", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_setreservation_053")]
		public DateTime etime
		{
			get { return _etime; }
			set { _etime = value; }
		}        
    }
}