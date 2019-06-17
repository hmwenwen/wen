using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("公司信息表")]
    
    public class companyEntity
    {
		private long _comid = 0;
        private string _comcode = Helper.GetAppSettings("BusCode");
		private string _pcomcode = string.Empty;
		private string _comname = string.Empty;
		private string _comindcode = string.Empty;
		private int _comprovinceid = 0;
		private int _comcityid = 0;
		private int _comareaid = 0;
		private string _comaddress = string.Empty;
		private string _comprincipal = string.Empty;
		private string _comprincipaltel = string.Empty;
		private string _comtel = string.Empty;
		private string _comemail = string.Empty;
		private string _comlogo = string.Empty;
		private string _comurl = string.Empty;
		private string _comcoordx = string.Empty;
		private string _comcoordy = string.Empty;
		private string _comstatus = string.Empty;
		private string _comdesc = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private long _uuser = 0;
		private DateTime _utime = DateTime.Parse("1900-01-01");
		private string _buscode = string.Empty;

		/// <summary>
		///公司ID
		/// <summary>
		public long comid
		{
			get { return _comid; }
			set { _comid = value; }
		}
		/// <summary>
		///公司编号
		/// <summary>
		public string comcode
		{
			get { return _comcode; }
			set { _comcode = value; }
		}
		/// <summary>
		///所属母公司编码
		/// <summary>
		[ModelInfo(Name = "所属母公司编码",ControlName="txt_pcomcode", NotEmpty = false, Length = 8, NotEmptyECode = "company_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_008")]
		public string pcomcode
		{
			get { return _pcomcode; }
			set { _pcomcode = value; }
		}
		/// <summary>
		///公司名称
		/// <summary>
		[ModelInfo(Name = "公司名称",ControlName="txt_comname", NotEmpty = false, Length = 64, NotEmptyECode = "company_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_011")]
		public string comname
		{
			get { return _comname; }
			set { _comname = value; }
		}
		/// <summary>
		///所属行业
		/// <summary>
		[ModelInfo(Name = "所属行业",ControlName="txt_comindcode", NotEmpty = false, Length = 16, NotEmptyECode = "company_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_014")]
		public string comindcode
		{
			get { return _comindcode; }
			set { _comindcode = value; }
		}
		/// <summary>
		///所在省
		/// <summary>
		[ModelInfo(Name = "所在省",ControlName="txt_comprovinceid", NotEmpty = false, Length = 4, NotEmptyECode = "company_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_017")]
		public int comprovinceid
		{
			get { return _comprovinceid; }
			set { _comprovinceid = value; }
		}
		/// <summary>
		///所在城市
		/// <summary>
		[ModelInfo(Name = "所在城市",ControlName="txt_comcityid", NotEmpty = false, Length = 4, NotEmptyECode = "company_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_020")]
		public int comcityid
		{
			get { return _comcityid; }
			set { _comcityid = value; }
		}
		/// <summary>
		///所在区
		/// <summary>
		[ModelInfo(Name = "所在区",ControlName="txt_comareaid", NotEmpty = false, Length = 4, NotEmptyECode = "company_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_023")]
		public int comareaid
		{
			get { return _comareaid; }
			set { _comareaid = value; }
		}
		/// <summary>
		///地址
		/// <summary>
		[ModelInfo(Name = "地址",ControlName="txt_comaddress", NotEmpty = false, Length = 128, NotEmptyECode = "company_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_026")]
		public string comaddress
		{
			get { return _comaddress; }
			set { _comaddress = value; }
		}
		/// <summary>
		///负责人
		/// <summary>
		[ModelInfo(Name = "负责人",ControlName="txt_comprincipal", NotEmpty = false, Length = 32, NotEmptyECode = "company_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_029")]
		public string comprincipal
		{
			get { return _comprincipal; }
			set { _comprincipal = value; }
		}
		/// <summary>
		///负责人联系电话
		/// <summary>
		[ModelInfo(Name = "负责人联系电话",ControlName="txt_comprincipaltel", NotEmpty = false, Length = 32, NotEmptyECode = "company_031", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_032")]
		public string comprincipaltel
		{
			get { return _comprincipaltel; }
			set { _comprincipaltel = value; }
		}
		/// <summary>
		///电话
		/// <summary>
		[ModelInfo(Name = "电话",ControlName="txt_comtel", NotEmpty = false, Length = 32, NotEmptyECode = "company_034", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_035")]
		public string comtel
		{
			get { return _comtel; }
			set { _comtel = value; }
		}
		/// <summary>
		///电子邮箱
		/// <summary>
		[ModelInfo(Name = "电子邮箱",ControlName="txt_comemail", NotEmpty = false, Length = 64, NotEmptyECode = "company_037", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_038")]
		public string comemail
		{
			get { return _comemail; }
			set { _comemail = value; }
		}
		/// <summary>
		///Logo
		/// <summary>
		[ModelInfo(Name = "Logo",ControlName="txt_comlogo", NotEmpty = false, Length = 64, NotEmptyECode = "company_040", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_041")]
		public string comlogo
		{
			get { return _comlogo; }
			set { _comlogo = value; }
		}
		/// <summary>
		///公司网址
		/// <summary>
		[ModelInfo(Name = "公司网址",ControlName="txt_comurl", NotEmpty = false, Length = 128, NotEmptyECode = "company_043", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_044")]
		public string comurl
		{
			get { return _comurl; }
			set { _comurl = value; }
		}
		/// <summary>
		///X坐标
		/// <summary>
		[ModelInfo(Name = "X坐标",ControlName="txt_comcoordx", NotEmpty = false, Length = 32, NotEmptyECode = "company_046", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_047")]
		public string comcoordx
		{
			get { return _comcoordx; }
			set { _comcoordx = value; }
		}
		/// <summary>
		///Y坐标
		/// <summary>
		[ModelInfo(Name = "Y坐标",ControlName="txt_comcoordy", NotEmpty = false, Length = 32, NotEmptyECode = "company_049", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_050")]
		public string comcoordy
		{
			get { return _comcoordy; }
			set { _comcoordy = value; }
		}
		/// <summary>
		///状态
		/// <summary>
		[ModelInfo(Name = "状态",ControlName="txt_comstatus", NotEmpty = false, Length = 1, NotEmptyECode = "company_052", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_053")]
		public string comstatus
		{
			get { return _comstatus; }
			set { _comstatus = value; }
		}
		/// <summary>
		///公司描述
		/// <summary>
		[ModelInfo(Name = "公司描述",ControlName="txt_comdesc", NotEmpty = false, Length = 512, NotEmptyECode = "company_055", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_056")]
		public string comdesc
		{
			get { return _comdesc; }
			set { _comdesc = value; }
		}
		/// <summary>
		///扩展字段1
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
		///删除时间
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
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "company_070", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "company_071")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}        
    }
}