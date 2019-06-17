using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("语言信息表")]
    
    public class languagesEntity
    {
		private long _lanid = 0;
		private string _cname = string.Empty;
		private string _code = string.Empty;
		private int _orderno = 0;
		private string _remark = string.Empty;
		private string _status = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");

		/// <summary>
		///标识
		/// <summary>
		public long lanid
		{
			get { return _lanid; }
			set { _lanid = value; }
		}
		/// <summary>
		///语言名称
		/// <summary>
		[ModelInfo(Name = "语言名称",ControlName="txt_cname", NotEmpty = true, Length = 50, NotEmptyECode = "languages_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "languages_005")]
		public string cname
		{
			get { return _cname; }
			set { _cname = value; }
		}
		/// <summary>
		///代码
		/// <summary>
		[ModelInfo(Name = "代码",ControlName="txt_code", NotEmpty = true, Length = 16, NotEmptyECode = "languages_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "languages_008")]
		public string code
		{
			get { return _code; }
			set { _code = value; }
		}
		/// <summary>
		///排序号
		/// <summary>
		[ModelInfo(Name = "排序号",ControlName="txt_orderno", NotEmpty = false, Length = 4, NotEmptyECode = "languages_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "languages_011")]
		public int orderno
		{
			get { return _orderno; }
			set { _orderno = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remark", NotEmpty = false, Length = 64, NotEmptyECode = "languages_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "languages_014")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		/// <summary>
		///状态
		/// <summary>
		[ModelInfo(Name = "状态",ControlName="ddl_status", NotEmpty = false, Length = 1, NotEmptyECode = "languages_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "languages_017")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}
		/// <summary>
		///创建人
		/// <summary>
		public long cuser
		{
			get { return _cuser; }
			set { _cuser = value; }
		}
		/// <summary>
		///时间
		/// <summary>
		public DateTime ctime
		{
			get { return _ctime; }
			set { _ctime = value; }
		}        
    }
}