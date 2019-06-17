using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("设置备注")]
    
    public class setremarkEntity
    {
		private string _remarkcode = string.Empty;
		private string _dictype = string.Empty;
		private string _stocode = string.Empty;
		private string _buscode = string.Empty;
		private string _dicname = string.Empty;
		private int _orderno = 0;
		private string _status = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private string _isdelete = string.Empty;

		/// <summary>
		///字典编号
		/// <summary>
		public string remarkcode
		{
			get { return _remarkcode; }
			set { _remarkcode = value; }
		}
		/// <summary>
		///类别
		/// <summary>
		[ModelInfo(Name = "类别",ControlName="txt_dictype", NotEmpty = false, Length = 1, NotEmptyECode = "setremark_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setremark_005")]
		public string dictype
		{
			get { return _dictype; }
			set { _dictype = value; }
		}
		/// <summary>
		///引用门店表Store的门店编号字段stocode的值
		/// <summary>
		[ModelInfo(Name = "引用门店表Store的门店编号字段stocode的值",ControlName="txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "setremark_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setremark_008")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///引用商户表Business的商户编号字段buscode的值
		/// <summary>
		[ModelInfo(Name = "引用商户表Business的商户编号字段buscode的值",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "setremark_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setremark_011")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///字典名称
		/// <summary>
		[ModelInfo(Name = "字典名称",ControlName="txt_dicname", NotEmpty = false, Length = 16, NotEmptyECode = "setremark_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setremark_014")]
		public string dicname
		{
			get { return _dicname; }
			set { _dicname = value; }
		}
		/// <summary>
		///排序号
		/// <summary>
		[ModelInfo(Name = "排序号",ControlName="txt_orderno", NotEmpty = false, Length = 4, NotEmptyECode = "setremark_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setremark_017")]
		public int orderno
		{
			get { return _orderno; }
			set { _orderno = value; }
		}
		/// <summary>
		///有效状态（0无效，1有效）
		/// <summary>
		[ModelInfo(Name = "有效状态（0无效，1有效）",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "setremark_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "setremark_020")]
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
}