using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("客户预订表")]
    [Serializable]
    public class WX_busDestineEntity
    {
		private long _ID = 0;
		private string _stocode = string.Empty;
		private string _buscode = string.Empty;
		private DateTime _desDate = DateTime.Parse("1900-01-01");
		private string _desTime = string.Empty;
		private long _dicid = 0;
		private int _personNum = 0;
		private string _metcode = string.Empty;
		private string _userName = string.Empty;
		private string _tel = string.Empty;
		private string _remark = string.Empty;
		private string _status = string.Empty;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private string _dishesremark = string.Empty;
		private string _TerminalType = string.Empty;
		private string _openid = string.Empty;

		/// <summary>
		///ID
		/// <summary>
		public long ID
		{
			get { return _ID; }
			set { _ID = value; }
		}
		/// <summary>
		///所属门店
		/// <summary>
		[ModelInfo(Name = "所属门店",ControlName="txt_stocode", NotEmpty = false, Length = 16, NotEmptyECode = "WX_busDestine_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_005")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "WX_busDestine_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///预定日期
		/// <summary>
		[ModelInfo(Name = "预定日期",ControlName="txt_desDate", NotEmpty = false, Length = 19, NotEmptyECode = "WX_busDestine_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_011")]
		public DateTime desDate
		{
			get { return _desDate; }
			set { _desDate = value; }
		}
		/// <summary>
		///预定时间
		/// <summary>
		[ModelInfo(Name = "预定时间",ControlName="txt_desTime", NotEmpty = false, Length = 10, NotEmptyECode = "WX_busDestine_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_014")]
		public string desTime
		{
			get { return _desTime; }
			set { _desTime = value; }
		}
		/// <summary>
		///预定来源
		/// <summary>
		[ModelInfo(Name = "预定来源",ControlName="txt_dicid", NotEmpty = false, Length = 18, NotEmptyECode = "WX_busDestine_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_017")]
		public long dicid
		{
			get { return _dicid; }
			set { _dicid = value; }
		}
		/// <summary>
		///人数
		/// <summary>
		[ModelInfo(Name = "人数",ControlName="txt_personNum", NotEmpty = false, Length = 3, NotEmptyECode = "WX_busDestine_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_020")]
		public int personNum
		{
			get { return _personNum; }
			set { _personNum = value; }
		}
		/// <summary>
		///餐别编号
		/// <summary>
		[ModelInfo(Name = "餐别编号",ControlName="txt_metcode", NotEmpty = false, Length = 2, NotEmptyECode = "WX_busDestine_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_023")]
		public string metcode
		{
			get { return _metcode; }
			set { _metcode = value; }
		}
		/// <summary>
		///姓名
		/// <summary>
		[ModelInfo(Name = "姓名",ControlName="txt_userName", NotEmpty = false, Length = 32, NotEmptyECode = "WX_busDestine_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_026")]
		public string userName
		{
			get { return _userName; }
			set { _userName = value; }
		}
		/// <summary>
		///电话
		/// <summary>
		[ModelInfo(Name = "电话",ControlName="txt_tel", NotEmpty = false, Length = 20, NotEmptyECode = "WX_busDestine_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_029")]
		public string tel
		{
			get { return _tel; }
			set { _tel = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "WX_busDestine_031", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_032")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		/// <summary>
		///状态（0未到店，1已到店，2已离店，3已取消，4未处理，5已逾期）
		/// <summary>
		[ModelInfo(Name = "状态（0未到店，1已到店，2已离店，3已取消，4未处理，5已逾期）",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "WX_busDestine_034", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_035")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
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
		///点餐备注
		/// <summary>
		[ModelInfo(Name = "点餐备注",ControlName="txt_dishesremark", NotEmpty = false, Length = 128, NotEmptyECode = "WX_busDestine_040", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_041")]
		public string dishesremark
		{
			get { return _dishesremark; }
			set { _dishesremark = value; }
		}
		/// <summary>
		///终端类型（1中餐端、2快餐端、3美食端、4刷卡端、5超市端）
		/// <summary>
		[ModelInfo(Name = "终端类型（1中餐端、2快餐端、3美食端、4刷卡端、5超市端）",ControlName="txt_TerminalType", NotEmpty = false, Length = 2, NotEmptyECode = "WX_busDestine_043", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_044")]
		public string TerminalType
		{
			get { return _TerminalType; }
			set { _TerminalType = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_openid", NotEmpty = false, Length = 32, NotEmptyECode = "WX_busDestine_046", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_busDestine_047")]
		public string openid
		{
			get { return _openid; }
			set { _openid = value; }
		}        
    }
}