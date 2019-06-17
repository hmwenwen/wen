using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("餐收_桌台点餐1")]
    [Serializable]
    public class choorderEntity
    {
		private long _lsid = 0;
		private long _orderid = 0;
		private string _buscode = string.Empty;
		private string _strcode = string.Empty;
		private long _shiftid = 0;
		private string _tmcode = string.Empty;
		private int _personnum = 0;
		private string _username = string.Empty;
		private string _userphone = string.Empty;
		private string _arrivetime = string.Empty;
		private DateTime _opentime = DateTime.Parse("1900-01-01");
		private DateTime _restime = DateTime.Parse("1900-01-01");
		private DateTime _checkouttime = DateTime.Parse("1900-01-01");
		private DateTime _gusetleavetime = DateTime.Parse("1900-01-01");
		private int _alltime = 0;
		private DateTime _allfoodtime = DateTime.Parse("1900-01-01");
		private decimal _conmoney = 0;
		private string _status = string.Empty;
		private string _remark = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private long _uuser = 0;
		private DateTime _utime = DateTime.Parse("1900-01-01");
		private string _isdelete = string.Empty;

		/// <summary>
		///
		/// <summary>
		public long lsid
		{
			get { return _lsid; }
			set { _lsid = value; }
		}
		/// <summary>
		///点餐编号
		/// <summary>
		[ModelInfo(Name = "点餐编号",ControlName="txt_orderid", NotEmpty = false, Length = 18, NotEmptyECode = "choorder_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_005")]
		public long orderid
		{
			get { return _orderid; }
			set { _orderid = value; }
		}
		/// <summary>
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "choorder_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///门店编号
		/// <summary>
		[ModelInfo(Name = "门店编号",ControlName="txt_strcode", NotEmpty = false, Length = 8, NotEmptyECode = "choorder_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_011")]
		public string strcode
		{
			get { return _strcode; }
			set { _strcode = value; }
		}
		/// <summary>
		///班次id
		/// <summary>
		[ModelInfo(Name = "班次id",ControlName="txt_shiftid", NotEmpty = false, Length = 18, NotEmptyECode = "choorder_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_014")]
		public long shiftid
		{
			get { return _shiftid; }
			set { _shiftid = value; }
		}
		/// <summary>
		///桌台编号
		/// <summary>
		[ModelInfo(Name = "桌台编号",ControlName="txt_tmcode", NotEmpty = false, Length = 8, NotEmptyECode = "choorder_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_017")]
		public string tmcode
		{
			get { return _tmcode; }
			set { _tmcode = value; }
		}
		/// <summary>
		///人数
		/// <summary>
		[ModelInfo(Name = "人数",ControlName="txt_personnum", NotEmpty = false, Length = 9, NotEmptyECode = "choorder_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_020")]
		public int personnum
		{
			get { return _personnum; }
			set { _personnum = value; }
		}
		/// <summary>
		///预定人姓名
		/// <summary>
		[ModelInfo(Name = "预定人姓名",ControlName="txt_username", NotEmpty = false, Length = 20, NotEmptyECode = "choorder_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_023")]
		public string username
		{
			get { return _username; }
			set { _username = value; }
		}
		/// <summary>
		///预定人电话
		/// <summary>
		[ModelInfo(Name = "预定人电话",ControlName="txt_userphone", NotEmpty = false, Length = 12, NotEmptyECode = "choorder_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_026")]
		public string userphone
		{
			get { return _userphone; }
			set { _userphone = value; }
		}
		/// <summary>
		///预定到店时间
		/// <summary>
		[ModelInfo(Name = "预定到店时间",ControlName="txt_arrivetime", NotEmpty = false, Length = 32, NotEmptyECode = "choorder_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_029")]
		public string arrivetime
		{
			get { return _arrivetime; }
			set { _arrivetime = value; }
		}
		/// <summary>
		///开台时间
		/// <summary>
		[ModelInfo(Name = "开台时间",ControlName="txt_opentime", NotEmpty = false, Length = 19, NotEmptyECode = "choorder_031", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_032")]
		public DateTime opentime
		{
			get { return _opentime; }
			set { _opentime = value; }
		}
		/// <summary>
		///预定时间
		/// <summary>
		[ModelInfo(Name = "预定时间",ControlName="txt_restime", NotEmpty = false, Length = 19, NotEmptyECode = "choorder_034", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_035")]
		public DateTime restime
		{
			get { return _restime; }
			set { _restime = value; }
		}
		/// <summary>
		///结账时间
		/// <summary>
		[ModelInfo(Name = "结账时间",ControlName="txt_checkouttime", NotEmpty = false, Length = 19, NotEmptyECode = "choorder_037", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_038")]
		public DateTime checkouttime
		{
			get { return _checkouttime; }
			set { _checkouttime = value; }
		}
		/// <summary>
		///客离时间
		/// <summary>
		[ModelInfo(Name = "客离时间",ControlName="txt_gusetleavetime", NotEmpty = false, Length = 19, NotEmptyECode = "choorder_040", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_041")]
		public DateTime gusetleavetime
		{
			get { return _gusetleavetime; }
			set { _gusetleavetime = value; }
		}
		/// <summary>
		///总用时(分钟)
		/// <summary>
		[ModelInfo(Name = "总用时(分钟)",ControlName="txt_alltime", NotEmpty = false, Length = 9, NotEmptyECode = "choorder_043", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_044")]
		public int alltime
		{
			get { return _alltime; }
			set { _alltime = value; }
		}
		/// <summary>
		///菜上齐时间
		/// <summary>
		[ModelInfo(Name = "菜上齐时间",ControlName="txt_allfoodtime", NotEmpty = false, Length = 19, NotEmptyECode = "choorder_046", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_047")]
		public DateTime allfoodtime
		{
			get { return _allfoodtime; }
			set { _allfoodtime = value; }
		}
		/// <summary>
		///消费金额
		/// <summary>
		[ModelInfo(Name = "消费金额",ControlName="txt_conmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorder_049", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_050")]
		public decimal conmoney
		{
			get { return _conmoney; }
			set { _conmoney = value; }
		}
		/// <summary>
		///状态
		/// <summary>
		[ModelInfo(Name = "状态",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "choorder_052", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_053")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "choorder_055", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorder_056")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
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
    }
}