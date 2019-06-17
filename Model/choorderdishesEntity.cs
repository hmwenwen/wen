using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("餐收_点餐菜品2-1")]
    [Serializable]
    public class choorderdishesEntity
    {
		private long _lsid = 0;
		private long _orderdishesid = 0;
		private string _buscode = string.Empty;
		private string _stocode = string.Empty;
		private long _orderid = 0;
		private long _detailid = 0;
		private string _distypecode = string.Empty;
		private string _dtypecode = string.Empty;
		private string _melcode = string.Empty;
		private string _discode = string.Empty;
		private string _disname = string.Empty;
		private string _disothername = string.Empty;
		private decimal _disnum = 0;
		private decimal _disnumconst = 0;
		private decimal _addnum = 0;
		private string _isneedmethod = string.Empty;
		private string _distacode = string.Empty;
		private string _distaname = string.Empty;
		private DateTime _ordertime = DateTime.Parse("1900-01-01");
		private DateTime _addtime = DateTime.Parse("1900-01-01");
		private DateTime _calltime = DateTime.Parse("1900-01-01");
		private DateTime _pushfoodtime = DateTime.Parse("1900-01-01");
		private int _pushfoodstate = 0;
		private string _isentity = string.Empty;
		private int _entitydefcount = 0;
		private decimal _entityprice = 0;
		private int _singlenum = 0;
		private decimal _singleAllmoney = 0;
		private decimal _totaladdmoney = 0;
		private decimal _totaladdmoneydiscount = 0;
		private decimal _allmoney = 0;
		private decimal _allmoneydiscount = 0;
		private decimal _memberallmoney = 0;
		private decimal _resultallmoney = 0;
		private decimal _packageaddmoney = 0;
		private string _ispackage = string.Empty;
		private string _iscanout = string.Empty;
		private string _isout = string.Empty;
		private decimal _refundNum = 0;
		private decimal _refundaddnum = 0;
		private decimal _oneprice = 0;
		private decimal _memberprice = 0;
		private decimal _costprice = 0;
		private decimal _methodmoney = 0;
		private decimal _methodmoneydiscount = 0;
		private decimal _attachmoney = 0;
		private decimal _pushmoney = 0;
		private string _ismember = string.Empty;
		private string _ispre = string.Empty;
		private string _pretype = string.Empty;
		private string _dispcode = string.Empty;
		private decimal _discountratemax = 0;
		private decimal _discountrate = 0;
		private decimal _premoney = 0;
		private string _precheck = string.Empty;
		private DateTime _checktime = DateTime.Parse("1900-01-01");
		private string _ismustconsume = string.Empty;
		private decimal _mustconsumenum = 0;
		private string _iscaninventory = string.Empty;
		private string _isallowmemberprice = string.Empty;
		private string _isattachcalculate = string.Empty;
		private string _isclipcoupons = string.Empty;
		private string _isnonoperating = string.Empty;
		private string _iscombooptional = string.Empty;
		private string _isneedweigh = string.Empty;
		private string _iscanmodifyprice = string.Empty;
		private string _matcode = string.Empty;
		private string _cguid = string.Empty;
		private string _pcguid = string.Empty;
		private long _porderdishesid = 0;
		private string _comdiscode = string.Empty;
		private string _comgcode = string.Empty;
		private string _composetype = string.Empty;
		private decimal _allowkinds = 0;
		private decimal _allowcount = 0;
		private decimal _allowamount = 0;
		private decimal _usedisdefaultamount = 0;
		private decimal _usedismaxamount = 0;
		private string _unit = string.Empty;
		private string _extcode = string.Empty;
		private string _fincode = string.Empty;
		private string _dcode = string.Empty;
		private string _kitcode = string.Empty;
		private string _ecode = string.Empty;
		private string _warcode = string.Empty;
		private string _totmcode = string.Empty;
		private string _totmname = string.Empty;
		private long _todetailid = 0;
		private string _status = string.Empty;
		private string _makestatus = string.Empty;
		private string _operaretype = string.Empty;
		private string _isdiscount = string.Empty;
		private string _priceispre = string.Empty;
		private string _ispresented = string.Empty;
		private string _pecode = string.Empty;
		private string _pename = string.Empty;
		private string _prereason = string.Empty;
		private string _prereasontype = string.Empty;
		private string _remark = string.Empty;
		private string _orderremark = string.Empty;
		private string _gguid = string.Empty;
		private string _nopreremark = string.Empty;
		private string _storeupdated = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private decimal _comprice = 0;
		private string _cusername = string.Empty;
		private string _tottcode = string.Empty;
		private string _metname = string.Empty;
		private string _metcode = string.Empty;
		private string _detailcode = string.Empty;
		private DateTime _tmopentime = DateTime.Parse("1900-01-01");
		private string _chomac = string.Empty;
		private string _orderno = string.Empty;
		private string _subtype = string.Empty;

		/// <summary>
		///
		/// <summary>
		public long lsid
		{
			get { return _lsid; }
			set { _lsid = value; }
		}
		/// <summary>
		///标识
		/// <summary>
		[ModelInfo(Name = "标识",ControlName="txt_orderdishesid", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_005")]
		public long orderdishesid
		{
			get { return _orderdishesid; }
			set { _orderdishesid = value; }
		}
		/// <summary>
		///所属商户编号
		/// <summary>
		[ModelInfo(Name = "所属商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///所属门店编号
		/// <summary>
		[ModelInfo(Name = "所属门店编号",ControlName="txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "choorderdishes_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_011")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///点餐编号
		/// <summary>
		[ModelInfo(Name = "点餐编号",ControlName="txt_orderid", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_014")]
		public long orderid
		{
			get { return _orderid; }
			set { _orderid = value; }
		}
		/// <summary>
		///点餐订单编号
		/// <summary>
		[ModelInfo(Name = "点餐订单编号",ControlName="txt_detailid", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_017")]
		public long detailid
		{
			get { return _detailid; }
			set { _detailid = value; }
		}
		/// <summary>
		///菜品大类编号
		/// <summary>
		[ModelInfo(Name = "菜品大类编号",ControlName="txt_distypecode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_020")]
		public string distypecode
		{
			get { return _distypecode; }
			set { _distypecode = value; }
		}
		/// <summary>
		///所属菜品类型
		/// <summary>
		[ModelInfo(Name = "所属菜品类型",ControlName="txt_dtypecode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_023")]
		public string dtypecode
		{
			get { return _dtypecode; }
			set { _dtypecode = value; }
		}
		/// <summary>
		///菜谱编号
		/// <summary>
		[ModelInfo(Name = "菜谱编号",ControlName="txt_melcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_026")]
		public string melcode
		{
			get { return _melcode; }
			set { _melcode = value; }
		}
		/// <summary>
		///菜品编号
		/// <summary>
		[ModelInfo(Name = "菜品编号",ControlName="txt_discode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_029")]
		public string discode
		{
			get { return _discode; }
			set { _discode = value; }
		}
		/// <summary>
		///菜品名称
		/// <summary>
		[ModelInfo(Name = "菜品名称",ControlName="txt_disname", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdishes_031", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_032")]
		public string disname
		{
			get { return _disname; }
			set { _disname = value; }
		}
		/// <summary>
		///其他名称
		/// <summary>
		[ModelInfo(Name = "其他名称",ControlName="txt_disothername", NotEmpty = false, Length = 128, NotEmptyECode = "choorderdishes_034", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_035")]
		public string disothername
		{
			get { return _disothername; }
			set { _disothername = value; }
		}
		/// <summary>
		///数量
		/// <summary>
		[ModelInfo(Name = "数量",ControlName="txt_disnum", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_037", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_038")]
		public decimal disnum
		{
			get { return _disnum; }
			set { _disnum = value; }
		}
		/// <summary>
		///原数量（套餐标配可选餐品计算数量使用）
		/// <summary>
		[ModelInfo(Name = "原数量（套餐标配可选餐品计算数量使用）",ControlName="txt_disnumconst", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_040", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_041")]
		public decimal disnumconst
		{
			get { return _disnumconst; }
			set { _disnumconst = value; }
		}
		/// <summary>
		///已上数量
		/// <summary>
		[ModelInfo(Name = "已上数量",ControlName="txt_addnum", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_043", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_044")]
		public decimal addnum
		{
			get { return _addnum; }
			set { _addnum = value; }
		}
		/// <summary>
		///做法是否必选
		/// <summary>
		[ModelInfo(Name = "做法是否必选",ControlName="txt_isneedmethod", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_046", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_047")]
		public string isneedmethod
		{
			get { return _isneedmethod; }
			set { _isneedmethod = value; }
		}
		/// <summary>
		///口味编号
		/// <summary>
		[ModelInfo(Name = "口味编号",ControlName="txt_distacode", NotEmpty = false, Length = 256, NotEmptyECode = "choorderdishes_049", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_050")]
		public string distacode
		{
			get { return _distacode; }
			set { _distacode = value; }
		}
		/// <summary>
		///口味名称
		/// <summary>
		[ModelInfo(Name = "口味名称",ControlName="txt_distaname", NotEmpty = false, Length = 256, NotEmptyECode = "choorderdishes_052", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_053")]
		public string distaname
		{
			get { return _distaname; }
			set { _distaname = value; }
		}
		/// <summary>
		///点菜时间
		/// <summary>
		[ModelInfo(Name = "点菜时间",ControlName="txt_ordertime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdishes_055", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_056")]
		public DateTime ordertime
		{
			get { return _ordertime; }
			set { _ordertime = value; }
		}
		/// <summary>
		///菜上齐时间
		/// <summary>
		[ModelInfo(Name = "菜上齐时间",ControlName="txt_addtime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdishes_058", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_059")]
		public DateTime addtime
		{
			get { return _addtime; }
			set { _addtime = value; }
		}
		/// <summary>
		///叫起时间
		/// <summary>
		[ModelInfo(Name = "叫起时间",ControlName="txt_calltime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdishes_061", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_062")]
		public DateTime calltime
		{
			get { return _calltime; }
			set { _calltime = value; }
		}
		/// <summary>
		///催菜时间
		/// <summary>
		[ModelInfo(Name = "催菜时间",ControlName="txt_pushfoodtime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdishes_064", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_065")]
		public DateTime pushfoodtime
		{
			get { return _pushfoodtime; }
			set { _pushfoodtime = value; }
		}
		/// <summary>
		///催菜状态
		/// <summary>
		[ModelInfo(Name = "催菜状态",ControlName="txt_pushfoodstate", NotEmpty = false, Length = 9, NotEmptyECode = "choorderdishes_067", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_068")]
		public int pushfoodstate
		{
			get { return _pushfoodstate; }
			set { _pushfoodstate = value; }
		}
		/// <summary>
		///是否采用条只方案
		/// <summary>
		[ModelInfo(Name = "是否采用条只方案",ControlName="txt_isentity", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_070", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_071")]
		public string isentity
		{
			get { return _isentity; }
			set { _isentity = value; }
		}
		/// <summary>
		///默认条只数量
		/// <summary>
		[ModelInfo(Name = "默认条只数量",ControlName="txt_entitydefcount", NotEmpty = false, Length = 9, NotEmptyECode = "choorderdishes_073", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_074")]
		public int entitydefcount
		{
			get { return _entitydefcount; }
			set { _entitydefcount = value; }
		}
		/// <summary>
		///条只单价
		/// <summary>
		[ModelInfo(Name = "条只单价",ControlName="txt_entityprice", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_076", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_077")]
		public decimal entityprice
		{
			get { return _entityprice; }
			set { _entityprice = value; }
		}
		/// <summary>
		///增加条只数量
		/// <summary>
		[ModelInfo(Name = "增加条只数量",ControlName="txt_singlenum", NotEmpty = false, Length = 9, NotEmptyECode = "choorderdishes_079", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_080")]
		public int singlenum
		{
			get { return _singlenum; }
			set { _singlenum = value; }
		}
		/// <summary>
		///增加条只总金额
		/// <summary>
		[ModelInfo(Name = "增加条只总金额",ControlName="txt_singleAllmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_082", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_083")]
		public decimal singleAllmoney
		{
			get { return _singleAllmoney; }
			set { _singleAllmoney = value; }
		}
		/// <summary>
		///折扣前总加价金额(做法加价+套餐加价+条只加价+附加金额)
		/// <summary>
		[ModelInfo(Name = "折扣前总加价金额(做法加价+套餐加价+条只加价+附加金额)",ControlName="txt_totaladdmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_085", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_086")]
		public decimal totaladdmoney
		{
			get { return _totaladdmoney; }
			set { _totaladdmoney = value; }
		}
		/// <summary>
		///折扣后总加价金额(做法加价+套餐加价+条只加价+附加金额)
		/// <summary>
		[ModelInfo(Name = "折扣后总加价金额(做法加价+套餐加价+条只加价+附加金额)",ControlName="txt_totaladdmoneydiscount", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_088", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_089")]
		public decimal totaladdmoneydiscount
		{
			get { return _totaladdmoneydiscount; }
			set { _totaladdmoneydiscount = value; }
		}
		/// <summary>
		///折扣前菜品总金额(数量*单价)
		/// <summary>
		[ModelInfo(Name = "折扣前菜品总金额(数量*单价)",ControlName="txt_allmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_091", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_092")]
		public decimal allmoney
		{
			get { return _allmoney; }
			set { _allmoney = value; }
		}
		/// <summary>
		///折扣后菜品总金额(数量*单价)
		/// <summary>
		[ModelInfo(Name = "折扣后菜品总金额(数量*单价)",ControlName="txt_allmoneydiscount", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_094", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_095")]
		public decimal allmoneydiscount
		{
			get { return _allmoneydiscount; }
			set { _allmoneydiscount = value; }
		}
		/// <summary>
		///会员价菜品总金额(会员价菜品单价*份数)
		/// <summary>
		[ModelInfo(Name = "会员价菜品总金额(会员价菜品单价*份数)",ControlName="txt_memberallmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_097", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_098")]
		public decimal memberallmoney
		{
			get { return _memberallmoney; }
			set { _memberallmoney = value; }
		}
		/// <summary>
		///结账总金额(总价打折优惠后的总金额)
		/// <summary>
		[ModelInfo(Name = "结账总金额(总价打折优惠后的总金额)",ControlName="txt_resultallmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_100", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_101")]
		public decimal resultallmoney
		{
			get { return _resultallmoney; }
			set { _resultallmoney = value; }
		}
		/// <summary>
		///套餐加价
		/// <summary>
		[ModelInfo(Name = "套餐加价",ControlName="txt_packageaddmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_103", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_104")]
		public decimal packageaddmoney
		{
			get { return _packageaddmoney; }
			set { _packageaddmoney = value; }
		}
		/// <summary>
		///是否套餐
		/// <summary>
		[ModelInfo(Name = "是否套餐",ControlName="txt_ispackage", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_106", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_107")]
		public string ispackage
		{
			get { return _ispackage; }
			set { _ispackage = value; }
		}
		/// <summary>
		///是否可补出
		/// <summary>
		[ModelInfo(Name = "是否可补出",ControlName="txt_iscanout", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_109", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_110")]
		public string iscanout
		{
			get { return _iscanout; }
			set { _iscanout = value; }
		}
		/// <summary>
		///套餐补出标记
		/// <summary>
		[ModelInfo(Name = "套餐补出标记",ControlName="txt_isout", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_112", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_113")]
		public string isout
		{
			get { return _isout; }
			set { _isout = value; }
		}
		/// <summary>
		///退未上单数量
		/// <summary>
		[ModelInfo(Name = "退未上单数量",ControlName="txt_refundNum", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_115", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_116")]
		public decimal refundNum
		{
			get { return _refundNum; }
			set { _refundNum = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_refundaddnum", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_118", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_119")]
		public decimal refundaddnum
		{
			get { return _refundaddnum; }
			set { _refundaddnum = value; }
		}
		/// <summary>
		///单价
		/// <summary>
		[ModelInfo(Name = "单价",ControlName="txt_oneprice", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_121", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_122")]
		public decimal oneprice
		{
			get { return _oneprice; }
			set { _oneprice = value; }
		}
		/// <summary>
		///会员价
		/// <summary>
		[ModelInfo(Name = "会员价",ControlName="txt_memberprice", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_124", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_125")]
		public decimal memberprice
		{
			get { return _memberprice; }
			set { _memberprice = value; }
		}
		/// <summary>
		///成本价
		/// <summary>
		[ModelInfo(Name = "成本价",ControlName="txt_costprice", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_127", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_128")]
		public decimal costprice
		{
			get { return _costprice; }
			set { _costprice = value; }
		}
		/// <summary>
		///折扣前做法加价金额
		/// <summary>
		[ModelInfo(Name = "折扣前做法加价金额",ControlName="txt_methodmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_130", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_131")]
		public decimal methodmoney
		{
			get { return _methodmoney; }
			set { _methodmoney = value; }
		}
		/// <summary>
		///折扣后做法加价金额
		/// <summary>
		[ModelInfo(Name = "折扣后做法加价金额",ControlName="txt_methodmoneydiscount", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_133", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_134")]
		public decimal methodmoneydiscount
		{
			get { return _methodmoneydiscount; }
			set { _methodmoneydiscount = value; }
		}
		/// <summary>
		///附加金额(如换菜差价)
		/// <summary>
		[ModelInfo(Name = "附加金额(如换菜差价)",ControlName="txt_attachmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_136", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_137")]
		public decimal attachmoney
		{
			get { return _attachmoney; }
			set { _attachmoney = value; }
		}
		/// <summary>
		///提成金额
		/// <summary>
		[ModelInfo(Name = "提成金额",ControlName="txt_pushmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_139", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_140")]
		public decimal pushmoney
		{
			get { return _pushmoney; }
			set { _pushmoney = value; }
		}
		/// <summary>
		///客户是否是会员
		/// <summary>
		[ModelInfo(Name = "客户是否是会员",ControlName="txt_ismember", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_142", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_143")]
		public string ismember
		{
			get { return _ismember; }
			set { _ismember = value; }
		}
		/// <summary>
		///是否优惠(单菜优惠)
		/// <summary>
		[ModelInfo(Name = "是否优惠(单菜优惠)",ControlName="txt_ispre", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_145", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_146")]
		public string ispre
		{
			get { return _ispre; }
			set { _ispre = value; }
		}
		/// <summary>
		///优惠方式(金额,折扣)
		/// <summary>
		[ModelInfo(Name = "优惠方式(金额,折扣)",ControlName="txt_pretype", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_148", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_149")]
		public string pretype
		{
			get { return _pretype; }
			set { _pretype = value; }
		}
		/// <summary>
		///折扣方案编号
		/// <summary>
		[ModelInfo(Name = "折扣方案编号",ControlName="txt_dispcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_151", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_152")]
		public string dispcode
		{
			get { return _dispcode; }
			set { _dispcode = value; }
		}
		/// <summary>
		///可最大折扣率
		/// <summary>
		[ModelInfo(Name = "可最大折扣率",ControlName="txt_discountratemax", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_154", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_155")]
		public decimal discountratemax
		{
			get { return _discountratemax; }
			set { _discountratemax = value; }
		}
		/// <summary>
		///折扣率
		/// <summary>
		[ModelInfo(Name = "折扣率",ControlName="txt_discountrate", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_157", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_158")]
		public decimal discountrate
		{
			get { return _discountrate; }
			set { _discountrate = value; }
		}
		/// <summary>
		///优惠金额
		/// <summary>
		[ModelInfo(Name = "优惠金额",ControlName="txt_premoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_160", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_161")]
		public decimal premoney
		{
			get { return _premoney; }
			set { _premoney = value; }
		}
		/// <summary>
		///优惠确认人
		/// <summary>
		[ModelInfo(Name = "优惠确认人",ControlName="txt_precheck", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdishes_163", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_164")]
		public string precheck
		{
			get { return _precheck; }
			set { _precheck = value; }
		}
		/// <summary>
		///确认时间
		/// <summary>
		[ModelInfo(Name = "确认时间",ControlName="txt_checktime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdishes_166", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_167")]
		public DateTime checktime
		{
			get { return _checktime; }
			set { _checktime = value; }
		}
		/// <summary>
		///是否是固定消费商品
		/// <summary>
		[ModelInfo(Name = "是否是固定消费商品",ControlName="txt_ismustconsume", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_169", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_170")]
		public string ismustconsume
		{
			get { return _ismustconsume; }
			set { _ismustconsume = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_mustconsumenum", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_172", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_173")]
		public decimal mustconsumenum
		{
			get { return _mustconsumenum; }
			set { _mustconsumenum = value; }
		}
		/// <summary>
		///是否烟酒可入库类
		/// <summary>
		[ModelInfo(Name = "是否烟酒可入库类",ControlName="txt_iscaninventory", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_175", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_176")]
		public string iscaninventory
		{
			get { return _iscaninventory; }
			set { _iscaninventory = value; }
		}
		/// <summary>
		///是否允许会员价
		/// <summary>
		[ModelInfo(Name = "是否允许会员价",ControlName="txt_isallowmemberprice", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_178", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_179")]
		public string isallowmemberprice
		{
			get { return _isallowmemberprice; }
			set { _isallowmemberprice = value; }
		}
		/// <summary>
		///是否参与附加费计算
		/// <summary>
		[ModelInfo(Name = "是否参与附加费计算",ControlName="txt_isattachcalculate", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_181", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_182")]
		public string isattachcalculate
		{
			get { return _isattachcalculate; }
			set { _isattachcalculate = value; }
		}
		/// <summary>
		///是否支持使用消费券
		/// <summary>
		[ModelInfo(Name = "是否支持使用消费券",ControlName="txt_isclipcoupons", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_184", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_185")]
		public string isclipcoupons
		{
			get { return _isclipcoupons; }
			set { _isclipcoupons = value; }
		}
		/// <summary>
		///是否营业外收入
		/// <summary>
		[ModelInfo(Name = "是否营业外收入",ControlName="txt_isnonoperating", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_187", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_188")]
		public string isnonoperating
		{
			get { return _isnonoperating; }
			set { _isnonoperating = value; }
		}
		/// <summary>
		///套餐是否可选
		/// <summary>
		[ModelInfo(Name = "套餐是否可选",ControlName="txt_iscombooptional", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_190", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_191")]
		public string iscombooptional
		{
			get { return _iscombooptional; }
			set { _iscombooptional = value; }
		}
		/// <summary>
		///是否需称重
		/// <summary>
		[ModelInfo(Name = "是否需称重",ControlName="txt_isneedweigh", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_193", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_194")]
		public string isneedweigh
		{
			get { return _isneedweigh; }
			set { _isneedweigh = value; }
		}
		/// <summary>
		///是否可变价
		/// <summary>
		[ModelInfo(Name = "是否可变价",ControlName="txt_iscanmodifyprice", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_196", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_197")]
		public string iscanmodifyprice
		{
			get { return _iscanmodifyprice; }
			set { _iscanmodifyprice = value; }
		}
		/// <summary>
		///原料编号
		/// <summary>
		[ModelInfo(Name = "原料编号",ControlName="txt_matcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_199", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_200")]
		public string matcode
		{
			get { return _matcode; }
			set { _matcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_cguid", NotEmpty = false, Length = 64, NotEmptyECode = "choorderdishes_202", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_203")]
		public string cguid
		{
			get { return _cguid; }
			set { _cguid = value; }
		}
		/// <summary>
		///父唯一号
		/// <summary>
		[ModelInfo(Name = "父唯一号",ControlName="txt_pcguid", NotEmpty = false, Length = 64, NotEmptyECode = "choorderdishes_205", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_206")]
		public string pcguid
		{
			get { return _pcguid; }
			set { _pcguid = value; }
		}
		/// <summary>
		///父编号(套餐标配可选菜品的父id)
		/// <summary>
		[ModelInfo(Name = "父编号(套餐标配可选菜品的父id)",ControlName="txt_porderdishesid", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_208", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_209")]
		public long porderdishesid
		{
			get { return _porderdishesid; }
			set { _porderdishesid = value; }
		}
		/// <summary>
		///所属套餐编号
		/// <summary>
		[ModelInfo(Name = "所属套餐编号",ControlName="txt_comdiscode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_211", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_212")]
		public string comdiscode
		{
			get { return _comdiscode; }
			set { _comdiscode = value; }
		}
		/// <summary>
		///套餐组别编号（空为标配餐品）
		/// <summary>
		[ModelInfo(Name = "套餐组别编号（空为标配餐品）",ControlName="txt_comgcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_214", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_215")]
		public string comgcode
		{
			get { return _comgcode; }
			set { _comgcode = value; }
		}
		/// <summary>
		///组合方案
		/// <summary>
		[ModelInfo(Name = "组合方案",ControlName="txt_composetype", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_217", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_218")]
		public string composetype
		{
			get { return _composetype; }
			set { _composetype = value; }
		}
		/// <summary>
		///最大可选种数
		/// <summary>
		[ModelInfo(Name = "最大可选种数",ControlName="txt_allowkinds", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_220", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_221")]
		public decimal allowkinds
		{
			get { return _allowkinds; }
			set { _allowkinds = value; }
		}
		/// <summary>
		///合计可选总数量(标配的数量)
		/// <summary>
		[ModelInfo(Name = "合计可选总数量(标配的数量)",ControlName="txt_allowcount", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_223", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_224")]
		public decimal allowcount
		{
			get { return _allowcount; }
			set { _allowcount = value; }
		}
		/// <summary>
		///可选总金额
		/// <summary>
		[ModelInfo(Name = "可选总金额",ControlName="txt_allowamount", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_226", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_227")]
		public decimal allowamount
		{
			get { return _allowamount; }
			set { _allowamount = value; }
		}
		/// <summary>
		///可选菜品默认数量
		/// <summary>
		[ModelInfo(Name = "可选菜品默认数量",ControlName="txt_usedisdefaultamount", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_229", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_230")]
		public decimal usedisdefaultamount
		{
			get { return _usedisdefaultamount; }
			set { _usedisdefaultamount = value; }
		}
		/// <summary>
		///可选菜品最大数量
		/// <summary>
		[ModelInfo(Name = "可选菜品最大数量",ControlName="txt_usedismaxamount", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_232", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_233")]
		public decimal usedismaxamount
		{
			get { return _usedismaxamount; }
			set { _usedismaxamount = value; }
		}
		/// <summary>
		///单位编号
		/// <summary>
		[ModelInfo(Name = "单位编号",ControlName="txt_unit", NotEmpty = false, Length = 8, NotEmptyECode = "choorderdishes_235", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_236")]
		public string unit
		{
			get { return _unit; }
			set { _unit = value; }
		}
		/// <summary>
		///外部码
		/// <summary>
		[ModelInfo(Name = "外部码",ControlName="txt_extcode", NotEmpty = false, Length = 64, NotEmptyECode = "choorderdishes_238", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_239")]
		public string extcode
		{
			get { return _extcode; }
			set { _extcode = value; }
		}
		/// <summary>
		///财务类别编码
		/// <summary>
		[ModelInfo(Name = "财务类别编码",ControlName="txt_fincode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_241", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_242")]
		public string fincode
		{
			get { return _fincode; }
			set { _fincode = value; }
		}
		/// <summary>
		///出品部门编码
		/// <summary>
		[ModelInfo(Name = "出品部门编码",ControlName="txt_dcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_244", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_245")]
		public string dcode
		{
			get { return _dcode; }
			set { _dcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_kitcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_247", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_248")]
		public string kitcode
		{
			get { return _kitcode; }
			set { _kitcode = value; }
		}
		/// <summary>
		///制作厨师编码
		/// <summary>
		[ModelInfo(Name = "制作厨师编码",ControlName="txt_ecode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_250", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_251")]
		public string ecode
		{
			get { return _ecode; }
			set { _ecode = value; }
		}
		/// <summary>
		///所属仓库编号
		/// <summary>
		[ModelInfo(Name = "所属仓库编号",ControlName="txt_warcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_253", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_254")]
		public string warcode
		{
			get { return _warcode; }
			set { _warcode = value; }
		}
		/// <summary>
		///用餐桌台编号(可能是赠菜)
		/// <summary>
		[ModelInfo(Name = "用餐桌台编号(可能是赠菜)",ControlName="txt_totmcode", NotEmpty = false, Length = 8, NotEmptyECode = "choorderdishes_256", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_257")]
		public string totmcode
		{
			get { return _totmcode; }
			set { _totmcode = value; }
		}
		/// <summary>
		///用餐桌台名称
		/// <summary>
		[ModelInfo(Name = "用餐桌台名称",ControlName="txt_totmname", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdishes_259", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_260")]
		public string totmname
		{
			get { return _totmname; }
			set { _totmname = value; }
		}
		/// <summary>
		///用餐桌台点餐订单编号
		/// <summary>
		[ModelInfo(Name = "用餐桌台点餐订单编号",ControlName="txt_todetailid", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_262", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_263")]
		public long todetailid
		{
			get { return _todetailid; }
			set { _todetailid = value; }
		}
		/// <summary>
		///单据状态(0未处理，1挂单，2下单)
		/// <summary>
		[ModelInfo(Name = "单据状态(0未处理，1挂单，2下单)",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_265", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_266")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}
		/// <summary>
		///制作状态(0叫起，1即起，2加急)
		/// <summary>
		[ModelInfo(Name = "制作状态(0叫起，1即起，2加急)",ControlName="txt_makestatus", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_268", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_269")]
		public string makestatus
		{
			get { return _makestatus; }
			set { _makestatus = value; }
		}
		/// <summary>
		///操作类型(退菜,补菜)
		/// <summary>
		[ModelInfo(Name = "操作类型(退菜,补菜)",ControlName="txt_operaretype", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_271", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_272")]
		public string operaretype
		{
			get { return _operaretype; }
			set { _operaretype = value; }
		}
		/// <summary>
		///是否不可打折(0可打折，1不可打折)
		/// <summary>
		[ModelInfo(Name = "是否不可打折(0可打折，1不可打折)",ControlName="txt_isdiscount", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_274", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_275")]
		public string isdiscount
		{
			get { return _isdiscount; }
			set { _isdiscount = value; }
		}
		/// <summary>
		///单价是否是时价特价(0非时价特价)
		/// <summary>
		[ModelInfo(Name = "单价是否是时价特价(0非时价特价)",ControlName="txt_priceispre", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_277", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_278")]
		public string priceispre
		{
			get { return _priceispre; }
			set { _priceispre = value; }
		}
		/// <summary>
		///赠送类别（0非赠送，1本店赠送、2顾客赠送）
		/// <summary>
		[ModelInfo(Name = "赠送类别（0非赠送，1本店赠送、2顾客赠送）",ControlName="txt_ispresented", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_280", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_281")]
		public string ispresented
		{
			get { return _ispresented; }
			set { _ispresented = value; }
		}
		/// <summary>
		///赠菜操作员员工编号
		/// <summary>
		[ModelInfo(Name = "赠菜操作员员工编号",ControlName="txt_pecode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_283", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_284")]
		public string pecode
		{
			get { return _pecode; }
			set { _pecode = value; }
		}
		/// <summary>
		///赠菜操作员姓名
		/// <summary>
		[ModelInfo(Name = "赠菜操作员姓名",ControlName="txt_pename", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdishes_286", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_287")]
		public string pename
		{
			get { return _pename; }
			set { _pename = value; }
		}
		/// <summary>
		///赠菜原因
		/// <summary>
		[ModelInfo(Name = "赠菜原因",ControlName="txt_prereason", NotEmpty = false, Length = 128, NotEmptyECode = "choorderdishes_289", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_290")]
		public string prereason
		{
			get { return _prereason; }
			set { _prereason = value; }
		}
		/// <summary>
		///赠菜原因类别(经理赠送、异物赠送、时令赠送、生日赠送...)
		/// <summary>
		[ModelInfo(Name = "赠菜原因类别(经理赠送、异物赠送、时令赠送、生日赠送...)",ControlName="txt_prereasontype", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdishes_292", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_293")]
		public string prereasontype
		{
			get { return _prereasontype; }
			set { _prereasontype = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "choorderdishes_295", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_296")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		/// <summary>
		///整单备注
		/// <summary>
		[ModelInfo(Name = "整单备注",ControlName="txt_orderremark", NotEmpty = false, Length = 128, NotEmptyECode = "choorderdishes_298", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_299")]
		public string orderremark
		{
			get { return _orderremark; }
			set { _orderremark = value; }
		}
		/// <summary>
		///整单单号
		/// <summary>
		[ModelInfo(Name = "整单单号",ControlName="txt_gguid", NotEmpty = false, Length = 64, NotEmptyECode = "choorderdishes_301", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_302")]
		public string gguid
		{
			get { return _gguid; }
			set { _gguid = value; }
		}
		/// <summary>
		///不优惠原因备注
		/// <summary>
		[ModelInfo(Name = "不优惠原因备注",ControlName="txt_nopreremark", NotEmpty = false, Length = 128, NotEmptyECode = "choorderdishes_304", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_305")]
		public string nopreremark
		{
			get { return _nopreremark; }
			set { _nopreremark = value; }
		}
		/// <summary>
		///是否已更新库存（0未更新，1已更新，结账后调用接口更新库存）
		/// <summary>
		[ModelInfo(Name = "是否已更新库存（0未更新，1已更新，结账后调用接口更新库存）",ControlName="txt_storeupdated", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_307", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_308")]
		public string storeupdated
		{
			get { return _storeupdated; }
			set { _storeupdated = value; }
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
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_comprice", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdishes_316", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_317")]
		public decimal comprice
		{
			get { return _comprice; }
			set { _comprice = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_cusername", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdishes_319", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_320")]
		public string cusername
		{
			get { return _cusername; }
			set { _cusername = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_tottcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_322", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_323")]
		public string tottcode
		{
			get { return _tottcode; }
			set { _tottcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_metname", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_325", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_326")]
		public string metname
		{
			get { return _metname; }
			set { _metname = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_metcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdishes_328", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_329")]
		public string metcode
		{
			get { return _metcode; }
			set { _metcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_detailcode", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdishes_331", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_332")]
		public string detailcode
		{
			get { return _detailcode; }
			set { _detailcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_tmopentime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdishes_334", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_335")]
		public DateTime tmopentime
		{
			get { return _tmopentime; }
			set { _tmopentime = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_chomac", NotEmpty = false, Length = 64, NotEmptyECode = "choorderdishes_337", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_338")]
		public string chomac
		{
			get { return _chomac; }
			set { _chomac = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_orderno", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdishes_340", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_341")]
		public string orderno
		{
			get { return _orderno; }
			set { _orderno = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_subtype", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdishes_343", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdishes_344")]
		public string subtype
		{
			get { return _subtype; }
			set { _subtype = value; }
		}        
    }
}