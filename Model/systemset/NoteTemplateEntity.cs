using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("短信模板")]
    
    public class NoteTemplateEntity
    {
		private long _notid = 0;
		private string _buscode = string.Empty;
		private string _stocode = string.Empty;
		private string _tcode = string.Empty;
		private string _tname = string.Empty;
		private string _ttype = string.Empty;
		private string _status = string.Empty;
		private string _remark = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private long _uuser = 0;
		private DateTime _utime = DateTime.Parse("1900-01-01");

		/// <summary>
		///标识
		/// <summary>
		public long notid
		{
			get { return _notid; }
			set { _notid = value; }
		}
		/// <summary>
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "NoteTemplate_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteTemplate_005")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///门店编号
		/// <summary>
		[ModelInfo(Name = "门店编号",ControlName="txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "NoteTemplate_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteTemplate_008")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///模板编号
		/// <summary>
		[ModelInfo(Name = "模板编号",ControlName="txt_tcode", NotEmpty = true, Length = 64, NotEmptyECode = "NoteTemplate_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteTemplate_011")]
		public string tcode
		{
			get { return _tcode; }
			set { _tcode = value; }
		}
		/// <summary>
		///模板名称
		/// <summary>
		[ModelInfo(Name = "模板名称",ControlName="txt_tname", NotEmpty = true, Length = 64, NotEmptyECode = "NoteTemplate_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteTemplate_014")]
		public string tname
		{
			get { return _tname; }
			set { _tname = value; }
		}
		/// <summary>
		///模板类型
		/// <summary>
		[ModelInfo(Name = "模板类型",ControlName="ddl_ttype", NotEmpty = true, Length = 32, NotEmptyECode = "NoteTemplate_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteTemplate_017")]
		public string ttype
		{
			get { return _ttype; }
			set { _ttype = value; }
		}
		/// <summary>
		///状态
		/// <summary>
		[ModelInfo(Name = "状态",ControlName="ddl_status", NotEmpty = true, Length = 1, NotEmptyECode = "NoteTemplate_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteTemplate_020")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}
		/// <summary>
		///备注/模板信息
		/// <summary>
		[ModelInfo(Name = "备注/模板信息",ControlName="txt_remark", NotEmpty = true, Length = 256, NotEmptyECode = "NoteTemplate_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteTemplate_023")]
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
		///更新人标识
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