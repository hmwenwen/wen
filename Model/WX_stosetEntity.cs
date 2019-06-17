using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("门店设置")]
    [Serializable]
    public class WX_stosetEntity
    {
		private int _setreservationid = 0;
		private string _stocode = string.Empty;
		private string _buscode = string.Empty;
		private int _isnetwork = 0;
		private int _isqueue = 0;
		private int _isaddfood = 0;
		private string _festival = string.Empty;
		private string _weekend = string.Empty;
		private DateTime _ntime = DateTime.Parse("1900-01-01");

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
		[ModelInfo(Name = "所属门店",ControlName="txt_stocode", NotEmpty = false, Length = 16, NotEmptyECode = "WX_stoset_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_stoset_005")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "WX_stoset_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_stoset_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///是否开启网络预约（默认和0是关闭，1是开启）
		/// <summary>
		[ModelInfo(Name = "是否开启网络预约（默认和0是关闭，1是开启）",ControlName="txt_isnetwork", NotEmpty = false, Length = 9, NotEmptyECode = "WX_stoset_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_stoset_011")]
		public int isnetwork
		{
			get { return _isnetwork; }
			set { _isnetwork = value; }
		}
		/// <summary>
		///是否开启排队（默认和0是关闭，1是开启）
		/// <summary>
		[ModelInfo(Name = "是否开启排队（默认和0是关闭，1是开启）",ControlName="txt_isqueue", NotEmpty = false, Length = 9, NotEmptyECode = "WX_stoset_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_stoset_014")]
		public int isqueue
		{
			get { return _isqueue; }
			set { _isqueue = value; }
		}
		/// <summary>
		///是否在线点餐（默认和0是关闭，1是开启）
		/// <summary>
		[ModelInfo(Name = "是否在线点餐（默认和0是关闭，1是开启）",ControlName="txt_isaddfood", NotEmpty = false, Length = 9, NotEmptyECode = "WX_stoset_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_stoset_017")]
		public int isaddfood
		{
			get { return _isaddfood; }
			set { _isaddfood = value; }
		}
		/// <summary>
		///节假日接受预定
		/// <summary>
		[ModelInfo(Name = "节假日接受预定",ControlName="txt_festival", NotEmpty = false, Length = 1, NotEmptyECode = "WX_stoset_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_stoset_020")]
		public string festival
		{
			get { return _festival; }
			set { _festival = value; }
		}
		/// <summary>
		///周末接受预定
		/// <summary>
		[ModelInfo(Name = "周末接受预定",ControlName="txt_weekend", NotEmpty = false, Length = 10, NotEmptyECode = "WX_stoset_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_stoset_023")]
		public string weekend
		{
			get { return _weekend; }
			set { _weekend = value; }
		}
		/// <summary>
		///创建时间
		/// <summary>
		[ModelInfo(Name = "创建时间",ControlName="txt_ntime", NotEmpty = false, Length = 19, NotEmptyECode = "WX_stoset_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_stoset_026")]
		public DateTime ntime
		{
			get { return _ntime; }
			set { _ntime = value; }
		}        
    }
}