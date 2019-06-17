using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("预定备注设置")]
    [Serializable]
    public class WX_reserveremakeEntity
    {
		private int _id = 0;
		private string _stocode = string.Empty;
		private string _buscode = string.Empty;
		private string _remake = string.Empty;
		private int _sort = 0;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private long _uuser = 0;
		private DateTime _utime = DateTime.Parse("1900-01-01");
		private string _status = string.Empty;

		/// <summary>
		///标识id
		/// <summary>
		public int id
		{
			get { return _id; }
			set { _id = value; }
		}
		/// <summary>
		///所属门店
		/// <summary>
		[ModelInfo(Name = "所属门店",ControlName="txt_stocode", NotEmpty = false, Length = 16, NotEmptyECode = "WX_reserveremake_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_reserveremake_005")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "WX_reserveremake_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_reserveremake_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remake", NotEmpty = false, Length = 20, NotEmptyECode = "WX_reserveremake_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_reserveremake_011")]
		public string remake
		{
			get { return _remake; }
			set { _remake = value; }
		}
		/// <summary>
		///排序值
		/// <summary>
		[ModelInfo(Name = "排序值",ControlName="txt_sort", NotEmpty = false, Length = 9, NotEmptyECode = "WX_reserveremake_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_reserveremake_014")]
		public int sort
		{
			get { return _sort; }
			set { _sort = value; }
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
		///状态
		/// <summary>
		[ModelInfo(Name = "状态",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "WX_reserveremake_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_reserveremake_029")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}        
    }
}