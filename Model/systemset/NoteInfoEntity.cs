using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("短信信息")]
    
    public class NoteInfoEntity
    {
		private long _nifid = 0;
		private string _buscode = string.Empty;
		private string _stocode = string.Empty;
		private string _tcode = string.Empty;
		private string _sendmob = string.Empty;
		private string _status = string.Empty;
		private string _notecontent = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");

		/// <summary>
		///标识
		/// <summary>
		public long nifid
		{
			get { return _nifid; }
			set { _nifid = value; }
		}
		/// <summary>
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "NoteInfo_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteInfo_005")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///门店编号
		/// <summary>
		[ModelInfo(Name = "门店编号",ControlName="txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "NoteInfo_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteInfo_008")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///短信模板
		/// <summary>
		[ModelInfo(Name = "短信模板",ControlName="txt_tcode", NotEmpty = false, Length = 64, NotEmptyECode = "NoteInfo_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteInfo_011")]
		public string tcode
		{
			get { return _tcode; }
			set { _tcode = value; }
		}
		/// <summary>
		///接收手机号
		/// <summary>
		[ModelInfo(Name = "接收手机号",ControlName="txt_sendmob", NotEmpty = false, Length = 12, NotEmptyECode = "NoteInfo_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteInfo_014")]
		public string sendmob
		{
			get { return _sendmob; }
			set { _sendmob = value; }
		}
		/// <summary>
		///发送状态
		/// <summary>
		[ModelInfo(Name = "发送状态",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "NoteInfo_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteInfo_017")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}
		/// <summary>
		///短信内容
		/// <summary>
		[ModelInfo(Name = "短信内容",ControlName="txt_notecontent", NotEmpty = false, Length = 256, NotEmptyECode = "NoteInfo_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "NoteInfo_020")]
		public string notecontent
		{
			get { return _notecontent; }
			set { _notecontent = value; }
		}
		/// <summary>
		///发送人
		/// <summary>
		public long cuser
		{
			get { return _cuser; }
			set { _cuser = value; }
		}
		/// <summary>
		///发送时间
		/// <summary>
		public DateTime ctime
		{
			get { return _ctime; }
			set { _ctime = value; }
		}        
    }
}