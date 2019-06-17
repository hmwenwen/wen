using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("")]
    [Serializable]
    public class dishesEntity
    {
		private long _lsid = 0;
		private long _disid = 0;
		private string _buscode = string.Empty;
		private string _stocode = string.Empty;
		private string _discode = string.Empty;
		private string _disname = string.Empty;
		private string _melcode = string.Empty;
		private string _disothername = string.Empty;
		private string _distypecode = string.Empty;
		private string _quickcode = string.Empty;
		private string _customcode = string.Empty;
		private string _unit = string.Empty;
		private decimal _price = 0;
		private decimal _memberprice = 0;
		private string _ismultiprice = string.Empty;
		private decimal _costprice = 0;
		private string _iscostbyingredient = string.Empty;
		private decimal _pushmoney = 0;
		private string _matclscode = string.Empty;
		private string _matcode = string.Empty;
		private string _extcode = string.Empty;
		private string _fincode = string.Empty;
		private string _dcode = string.Empty;
		private string _kitcode = string.Empty;
		private string _ecode = string.Empty;
		private int _maketime = 0;
		private string _qrcode = string.Empty;
		private string _dispicture = string.Empty;
		private string _remark = string.Empty;
		private string _isentity = string.Empty;
		private int _entitydefcount = 0;
		private decimal _entityprice = 0;
		private string _iscanmodifyprice = string.Empty;
		private string _isneedweigh = string.Empty;
		private string _isneedmethod = string.Empty;
		private string _iscaninventory = string.Empty;
		private string _iscancustom = string.Empty;
		private string _iscandeposit = string.Empty;
		private string _isallowmemberprice = string.Empty;
		private string _isattachcalculate = string.Empty;
		private string _isclipcoupons = string.Empty;
		private string _iscombo = string.Empty;
		private string _iscombooptional = string.Empty;
		private string _isnonoperating = string.Empty;
		private string _status = string.Empty;
		private int _busSort = 0;
		private string _warcode = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private long _uuser = 0;
		private DateTime _utime = DateTime.Parse("1900-01-01");
		private string _isdelete = string.Empty;

		/// <summary>
		///
		/// <summary>
		public long lsid
		{
			get { return _lsid; }
			set { _lsid = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_disid", NotEmpty = false, Length = 18, NotEmptyECode = "dishes_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_005")]
		public long disid
		{
			get { return _disid; }
			set { _disid = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "dishes_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "dishes_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_011")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_discode", NotEmpty = false, Length = 16, NotEmptyECode = "dishes_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_014")]
		public string discode
		{
			get { return _discode; }
			set { _discode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_disname", NotEmpty = false, Length = 32, NotEmptyECode = "dishes_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_017")]
		public string disname
		{
			get { return _disname; }
			set { _disname = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_melcode", NotEmpty = false, Length = 5, NotEmptyECode = "dishes_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_020")]
		public string melcode
		{
			get { return _melcode; }
			set { _melcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_disothername", NotEmpty = false, Length = 128, NotEmptyECode = "dishes_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_023")]
		public string disothername
		{
			get { return _disothername; }
			set { _disothername = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_distypecode", NotEmpty = false, Length = 16, NotEmptyECode = "dishes_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_026")]
		public string distypecode
		{
			get { return _distypecode; }
			set { _distypecode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_quickcode", NotEmpty = false, Length = 32, NotEmptyECode = "dishes_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_029")]
		public string quickcode
		{
			get { return _quickcode; }
			set { _quickcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_customcode", NotEmpty = false, Length = 8, NotEmptyECode = "dishes_031", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_032")]
		public string customcode
		{
			get { return _customcode; }
			set { _customcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_unit", NotEmpty = false, Length = 8, NotEmptyECode = "dishes_034", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_035")]
		public string unit
		{
			get { return _unit; }
			set { _unit = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_price", NotEmpty = false, Length = 18, NotEmptyECode = "dishes_037", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_038")]
		public decimal price
		{
			get { return _price; }
			set { _price = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_memberprice", NotEmpty = false, Length = 18, NotEmptyECode = "dishes_040", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_041")]
		public decimal memberprice
		{
			get { return _memberprice; }
			set { _memberprice = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ismultiprice", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_043", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_044")]
		public string ismultiprice
		{
			get { return _ismultiprice; }
			set { _ismultiprice = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_costprice", NotEmpty = false, Length = 18, NotEmptyECode = "dishes_046", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_047")]
		public decimal costprice
		{
			get { return _costprice; }
			set { _costprice = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_iscostbyingredient", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_049", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_050")]
		public string iscostbyingredient
		{
			get { return _iscostbyingredient; }
			set { _iscostbyingredient = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_pushmoney", NotEmpty = false, Length = 18, NotEmptyECode = "dishes_052", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_053")]
		public decimal pushmoney
		{
			get { return _pushmoney; }
			set { _pushmoney = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_matclscode", NotEmpty = false, Length = 8, NotEmptyECode = "dishes_055", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_056")]
		public string matclscode
		{
			get { return _matclscode; }
			set { _matclscode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_matcode", NotEmpty = false, Length = 16, NotEmptyECode = "dishes_058", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_059")]
		public string matcode
		{
			get { return _matcode; }
			set { _matcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_extcode", NotEmpty = false, Length = 64, NotEmptyECode = "dishes_061", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_062")]
		public string extcode
		{
			get { return _extcode; }
			set { _extcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_fincode", NotEmpty = false, Length = 16, NotEmptyECode = "dishes_064", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_065")]
		public string fincode
		{
			get { return _fincode; }
			set { _fincode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_dcode", NotEmpty = false, Length = 16, NotEmptyECode = "dishes_067", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_068")]
		public string dcode
		{
			get { return _dcode; }
			set { _dcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_kitcode", NotEmpty = false, Length = 5, NotEmptyECode = "dishes_070", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_071")]
		public string kitcode
		{
			get { return _kitcode; }
			set { _kitcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ecode", NotEmpty = false, Length = 16, NotEmptyECode = "dishes_073", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_074")]
		public string ecode
		{
			get { return _ecode; }
			set { _ecode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_maketime", NotEmpty = false, Length = 3, NotEmptyECode = "dishes_076", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_077")]
		public int maketime
		{
			get { return _maketime; }
			set { _maketime = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_qrcode", NotEmpty = false, Length = 256, NotEmptyECode = "dishes_079", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_080")]
		public string qrcode
		{
			get { return _qrcode; }
			set { _qrcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_dispicture", NotEmpty = false, Length = 128, NotEmptyECode = "dishes_082", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_083")]
		public string dispicture
		{
			get { return _dispicture; }
			set { _dispicture = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "dishes_085", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_086")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_isentity", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_088", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_089")]
		public string isentity
		{
			get { return _isentity; }
			set { _isentity = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_entitydefcount", NotEmpty = false, Length = 9, NotEmptyECode = "dishes_091", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_092")]
		public int entitydefcount
		{
			get { return _entitydefcount; }
			set { _entitydefcount = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_entityprice", NotEmpty = false, Length = 18, NotEmptyECode = "dishes_094", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_095")]
		public decimal entityprice
		{
			get { return _entityprice; }
			set { _entityprice = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_iscanmodifyprice", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_097", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_098")]
		public string iscanmodifyprice
		{
			get { return _iscanmodifyprice; }
			set { _iscanmodifyprice = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_isneedweigh", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_100", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_101")]
		public string isneedweigh
		{
			get { return _isneedweigh; }
			set { _isneedweigh = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_isneedmethod", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_103", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_104")]
		public string isneedmethod
		{
			get { return _isneedmethod; }
			set { _isneedmethod = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_iscaninventory", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_106", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_107")]
		public string iscaninventory
		{
			get { return _iscaninventory; }
			set { _iscaninventory = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_iscancustom", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_109", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_110")]
		public string iscancustom
		{
			get { return _iscancustom; }
			set { _iscancustom = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_iscandeposit", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_112", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_113")]
		public string iscandeposit
		{
			get { return _iscandeposit; }
			set { _iscandeposit = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_isallowmemberprice", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_115", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_116")]
		public string isallowmemberprice
		{
			get { return _isallowmemberprice; }
			set { _isallowmemberprice = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_isattachcalculate", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_118", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_119")]
		public string isattachcalculate
		{
			get { return _isattachcalculate; }
			set { _isattachcalculate = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_isclipcoupons", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_121", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_122")]
		public string isclipcoupons
		{
			get { return _isclipcoupons; }
			set { _isclipcoupons = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_iscombo", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_124", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_125")]
		public string iscombo
		{
			get { return _iscombo; }
			set { _iscombo = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_iscombooptional", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_127", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_128")]
		public string iscombooptional
		{
			get { return _iscombooptional; }
			set { _iscombooptional = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_isnonoperating", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_130", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_131")]
		public string isnonoperating
		{
			get { return _isnonoperating; }
			set { _isnonoperating = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "dishes_133", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_134")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_busSort", NotEmpty = false, Length = 9, NotEmptyECode = "dishes_136", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_137")]
		public int busSort
		{
			get { return _busSort; }
			set { _busSort = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_warcode", NotEmpty = false, Length = 8, NotEmptyECode = "dishes_139", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "dishes_140")]
		public string warcode
		{
			get { return _warcode; }
			set { _warcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		public long cuser
		{
			get { return _cuser; }
			set { _cuser = value; }
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
		public long uuser
		{
			get { return _uuser; }
			set { _uuser = value; }
		}
		/// <summary>
		///
		/// <summary>
		public DateTime utime
		{
			get { return _utime; }
			set { _utime = value; }
		}
		/// <summary>
		///
		/// <summary>
		public string isdelete
		{
			get { return _isdelete; }
			set { _isdelete = value; }
		}        
    }
}