using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("")]
    [Serializable]
    public class WX_members_wxEntity
    {
		private long _id = 0;
		private string _openid = string.Empty;
		private string _subscribe = string.Empty;
		private string _nickname = string.Empty;
		private string _sex = string.Empty;
		private string _language = string.Empty;
		private string _cityid = string.Empty;
		private string _provinceid = string.Empty;
		private string _country = string.Empty;
		private string _mobile = string.Empty;
		private string _headimgurl = string.Empty;
		private string _subscribe_scene = string.Empty;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private string _upwd = string.Empty;
		private string _notpwd = string.Empty;
        private string _wxopenid = string.Empty;


        public string wxopenid
        {
            get { return _wxopenid; }
            set { _wxopenid = value; }
        }

		/// <summary>
		///
		/// <summary>
		public long id
		{
			get { return _id; }
			set { _id = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_openid", NotEmpty = false, Length = 32, NotEmptyECode = "WX_members_wx_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_005")]
		public string openid
		{
			get { return _openid; }
			set { _openid = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_subscribe", NotEmpty = false, Length = 1, NotEmptyECode = "WX_members_wx_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_008")]
		public string subscribe
		{
			get { return _subscribe; }
			set { _subscribe = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_nickname", NotEmpty = false, Length = 25, NotEmptyECode = "WX_members_wx_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_011")]
		public string nickname
		{
			get { return _nickname; }
			set { _nickname = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_sex", NotEmpty = false, Length = 1, NotEmptyECode = "WX_members_wx_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_014")]
		public string sex
		{
			get { return _sex; }
			set { _sex = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_language", NotEmpty = false, Length = 20, NotEmptyECode = "WX_members_wx_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_017")]
		public string language
		{
			get { return _language; }
			set { _language = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_cityid", NotEmpty = false, Length = 20, NotEmptyECode = "WX_members_wx_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_020")]
		public string cityid
		{
			get { return _cityid; }
			set { _cityid = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_provinceid", NotEmpty = false, Length = 20, NotEmptyECode = "WX_members_wx_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_023")]
		public string provinceid
		{
			get { return _provinceid; }
			set { _provinceid = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_country", NotEmpty = false, Length = 20, NotEmptyECode = "WX_members_wx_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_026")]
		public string country
		{
			get { return _country; }
			set { _country = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_mobile", NotEmpty = false, Length = 12, NotEmptyECode = "WX_members_wx_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_029")]
		public string mobile
		{
			get { return _mobile; }
			set { _mobile = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_headimgurl", NotEmpty = false, Length = 500, NotEmptyECode = "WX_members_wx_031", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_032")]
		public string headimgurl
		{
			get { return _headimgurl; }
			set { _headimgurl = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_subscribe_scene", NotEmpty = false, Length = 50, NotEmptyECode = "WX_members_wx_034", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_035")]
		public string subscribe_scene
		{
			get { return _subscribe_scene; }
			set { _subscribe_scene = value; }
		}
		/// <summary>
		///
		/// <summary>
		public DateTime ctime
		{
			get { return _ctime; }
			set { _ctime = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_upwd", NotEmpty = false, Length = 20, NotEmptyECode = "WX_members_wx_040", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_041")]
		public string upwd
		{
			get { return _upwd; }
			set { _upwd = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_notpwd", NotEmpty = false, Length = 1, NotEmptyECode = "WX_members_wx_043", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "WX_members_wx_044")]
		public string notpwd
		{
			get { return _notpwd; }
			set { _notpwd = value; }
		}        
    }
}