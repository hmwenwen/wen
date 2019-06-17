using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("超级审核人")]
    [Serializable]
    public class SuperAuditorEntity
    {
		private long _sauid = 0;
        private string _buscode = Helper.GetAppSettings("BusCode");
		private string _stocode = string.Empty;
		private string _ctype = string.Empty;
		private long _userid = 0;
		private string _remark = string.Empty;
		private string _status = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");

		/// <summary>
		///标识
		/// <summary>
		public long sauid
		{
			get { return _sauid; }
			set { _sauid = value; }
		}
		/// <summary>
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "SuperAuditor_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "SuperAuditor_005")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///门店编号
		/// <summary>
		[ModelInfo(Name = "门店编号",ControlName="txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "SuperAuditor_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "SuperAuditor_008")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///类型
		/// <summary>
		[ModelInfo(Name = "类型",ControlName="ddl_ctype", NotEmpty = false, Length = 2, NotEmptyECode = "SuperAuditor_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "SuperAuditor_011")]
		public string ctype
		{
			get { return _ctype; }
			set { _ctype = value; }
		}
		/// <summary>
		///用户ID
		/// <summary>
		[ModelInfo(Name = "用户ID",ControlName="txt_userid", NotEmpty = true, Length = 18, NotEmptyECode = "SuperAuditor_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "SuperAuditor_014")]
		public long userid
		{
			get { return _userid; }
			set { _userid = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "SuperAuditor_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "SuperAuditor_017")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		/// <summary>
		///状态
		/// <summary>
		[ModelInfo(Name = "状态",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "SuperAuditor_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "SuperAuditor_020")]
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
		///创建时间
		/// <summary>
		public DateTime ctime
		{
			get { return _ctime; }
			set { _ctime = value; }
		}        
    }
}