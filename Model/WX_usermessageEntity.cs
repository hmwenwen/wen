using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("用户消息表")]
    [Serializable]
    public class WX_usermessageEntity
    {
		private long _id = 0;
		private string _openid = string.Empty;
		private string _msgtype = string.Empty;
		private string _status = string.Empty;
		private string _msgdetails = string.Empty;
		private string _title = string.Empty;
		private DateTime _ctime = DateTime.Parse("1900-01-01");

		/// <summary>
		///标识
		/// <summary>
		public long id
		{
			get { return _id; }
			set { _id = value; }
		}
		/// <summary>
		///openId
		/// <summary>
		[ModelInfo(Name = "openId",ControlName="txt_openid", NotEmpty = false, Length = 32, NotEmptyECode = "WX_usermessage_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_usermessage_005")]
		public string openid
		{
			get { return _openid; }
			set { _openid = value; }
		}
		/// <summary>
		///消息类型
		/// <summary>
		[ModelInfo(Name = "消息类型",ControlName="txt_msgtype", NotEmpty = false, Length = 1, NotEmptyECode = "WX_usermessage_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_usermessage_008")]
		public string msgtype
		{
			get { return _msgtype; }
			set { _msgtype = value; }
		}
		/// <summary>
		///消息状态（已读，未读）
		/// <summary>
		[ModelInfo(Name = "消息状态（已读，未读）",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "WX_usermessage_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_usermessage_011")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}
		/// <summary>
		///消息详情
		/// <summary>
		[ModelInfo(Name = "消息详情",ControlName="txt_msgdetails", NotEmpty = false, Length = 16, NotEmptyECode = "WX_usermessage_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_usermessage_014")]
		public string msgdetails
		{
			get { return _msgdetails; }
			set { _msgdetails = value; }
		}
		/// <summary>
		///消息标题
		/// <summary>
		[ModelInfo(Name = "消息标题",ControlName="txt_title", NotEmpty = false, Length = 25, NotEmptyECode = "WX_usermessage_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_usermessage_017")]
		public string title
		{
			get { return _title; }
			set { _title = value; }
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