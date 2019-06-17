using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("餐收_点餐订单2")]
    [Serializable]
    public class choorderdetailEntity
    {
		private long _id = 0;
		private long _detailid = 0;
		private string _buscode = string.Empty;
		private string _stocode = string.Empty;
		private long _orderid = 0;
		private int _personcount = 0;
		private DateTime _opentime = DateTime.Parse("1900-01-01");
		private DateTime _checkouttime = DateTime.Parse("1900-01-01");
		private int _alltime = 0;
		private DateTime _endtime = DateTime.Parse("1900-01-01");
		private long _shiftid = 0;
		private string _tmcode = string.Empty;
		private string _tmname = string.Empty;
		private string _tablecodes = string.Empty;
		private int _combinenum = 0;
		private DateTime _allfoodtime = DateTime.Parse("1900-01-01");
		private DateTime _pushfoodtime = DateTime.Parse("1900-01-01");
		private string _pushfoodstate = string.Empty;
		private string _closeaccount = string.Empty;
		private string _closeCodes = string.Empty;
		private DateTime _calltime = DateTime.Parse("1900-01-01");
		private long _userid = 0;
		private DateTime _ordertime = DateTime.Parse("1900-01-01");
		private decimal _surchargeamount = 0;
		private decimal _ocostprice = 0;
		private decimal _conmoney = 0;
		private string _status = string.Empty;
		private string _detailcode = string.Empty;
		private string _memcode = string.Empty;
		private string _desCode = string.Empty;
		private DateTime _desctime = DateTime.Parse("1900-01-01");
		private int _printcount = 0;
		private string _remark = string.Empty;
		private string _metname = string.Empty;
		private string _pushid = string.Empty;
		private string _pushname = string.Empty;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");
		private string _ispresented = string.Empty;
		private string _presentedcode = string.Empty;
		private string _cardcode = string.Empty;
		private string _mobile = string.Empty;
		private string _coupons = string.Empty;
		private string _metcode = string.Empty;
		private string _ttcode = string.Empty;
		private string _customer = string.Empty;
		private string _amanagerid = string.Empty;
		private string _amanagername = string.Empty;
		private decimal _olossmoney = 0;
		private string _detailtype = string.Empty;
		private decimal _predishemoney = 0;
		private string _TerminalType = string.Empty;
		private string _depcode = string.Empty;
		private string _depname = string.Empty;

		/// <summary>
		///
		/// <summary>
		public long id
		{
			get { return _id; }
			set { _id = value; }
		}
		/// <summary>
		///订单编号
		/// <summary>
		[ModelInfo(Name = "订单编号",ControlName="txt_detailid", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdetail_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_005")]
		public long detailid
		{
			get { return _detailid; }
			set { _detailid = value; }
		}
		/// <summary>
		///所属商户编号
		/// <summary>
		[ModelInfo(Name = "所属商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdetail_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///所属门店编号
		/// <summary>
		[ModelInfo(Name = "所属门店编号",ControlName="txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "choorderdetail_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_011")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///桌台点餐编号
		/// <summary>
		[ModelInfo(Name = "桌台点餐编号",ControlName="txt_orderid", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdetail_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_014")]
		public long orderid
		{
			get { return _orderid; }
			set { _orderid = value; }
		}
		/// <summary>
		///实际用餐人数
		/// <summary>
		[ModelInfo(Name = "实际用餐人数",ControlName="txt_personcount", NotEmpty = false, Length = 9, NotEmptyECode = "choorderdetail_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_017")]
		public int personcount
		{
			get { return _personcount; }
			set { _personcount = value; }
		}
		/// <summary>
		///点餐时间
		/// <summary>
		[ModelInfo(Name = "点餐时间",ControlName="txt_opentime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdetail_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_020")]
		public DateTime opentime
		{
			get { return _opentime; }
			set { _opentime = value; }
		}
		/// <summary>
		///结账时间
		/// <summary>
		[ModelInfo(Name = "结账时间",ControlName="txt_checkouttime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdetail_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_023")]
		public DateTime checkouttime
		{
			get { return _checkouttime; }
			set { _checkouttime = value; }
		}
		/// <summary>
		///总用时(分钟)
		/// <summary>
		[ModelInfo(Name = "总用时(分钟)",ControlName="txt_alltime", NotEmpty = false, Length = 9, NotEmptyECode = "choorderdetail_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_026")]
		public int alltime
		{
			get { return _alltime; }
			set { _alltime = value; }
		}
		/// <summary>
		///订单结束时间(关台或者继续点餐必须结束时间)
		/// <summary>
		[ModelInfo(Name = "订单结束时间(关台或者继续点餐必须结束时间)",ControlName="txt_endtime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdetail_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_029")]
		public DateTime endtime
		{
			get { return _endtime; }
			set { _endtime = value; }
		}
		/// <summary>
		///班次id
		/// <summary>
		[ModelInfo(Name = "班次id",ControlName="txt_shiftid", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdetail_031", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_032")]
		public long shiftid
		{
			get { return _shiftid; }
			set { _shiftid = value; }
		}
		/// <summary>
		///桌台编号
		/// <summary>
		[ModelInfo(Name = "桌台编号",ControlName="txt_tmcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdetail_034", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_035")]
		public string tmcode
		{
			get { return _tmcode; }
			set { _tmcode = value; }
		}
		/// <summary>
		///桌台名称
		/// <summary>
		[ModelInfo(Name = "桌台名称",ControlName="txt_tmname", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdetail_037", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_038")]
		public string tmname
		{
			get { return _tmname; }
			set { _tmname = value; }
		}
		/// <summary>
		///并台编号(多个)
		/// <summary>
		[ModelInfo(Name = "并台编号(多个)",ControlName="txt_tablecodes", NotEmpty = false, Length = -1, NotEmptyECode = "choorderdetail_040", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_041")]
		public string tablecodes
		{
			get { return _tablecodes; }
			set { _tablecodes = value; }
		}
		/// <summary>
		///并台序号
		/// <summary>
		[ModelInfo(Name = "并台序号",ControlName="txt_combinenum", NotEmpty = false, Length = 9, NotEmptyECode = "choorderdetail_043", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_044")]
		public int combinenum
		{
			get { return _combinenum; }
			set { _combinenum = value; }
		}
		/// <summary>
		///菜上齐时间
		/// <summary>
		[ModelInfo(Name = "菜上齐时间",ControlName="txt_allfoodtime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdetail_046", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_047")]
		public DateTime allfoodtime
		{
			get { return _allfoodtime; }
			set { _allfoodtime = value; }
		}
		/// <summary>
		///催菜时间
		/// <summary>
		[ModelInfo(Name = "催菜时间",ControlName="txt_pushfoodtime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdetail_049", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_050")]
		public DateTime pushfoodtime
		{
			get { return _pushfoodtime; }
			set { _pushfoodtime = value; }
		}
		/// <summary>
		///催菜状态
		/// <summary>
		[ModelInfo(Name = "催菜状态",ControlName="txt_pushfoodstate", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdetail_052", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_053")]
		public string pushfoodstate
		{
			get { return _pushfoodstate; }
			set { _pushfoodstate = value; }
		}
		/// <summary>
		///合帐状态
		/// <summary>
		[ModelInfo(Name = "合帐状态",ControlName="txt_closeaccount", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdetail_055", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_056")]
		public string closeaccount
		{
			get { return _closeaccount; }
			set { _closeaccount = value; }
		}
		/// <summary>
		///合帐编号
		/// <summary>
		[ModelInfo(Name = "合帐编号",ControlName="txt_closeCodes", NotEmpty = false, Length = 512, NotEmptyECode = "choorderdetail_058", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_059")]
		public string closeCodes
		{
			get { return _closeCodes; }
			set { _closeCodes = value; }
		}
		/// <summary>
		///叫起时间
		/// <summary>
		[ModelInfo(Name = "叫起时间",ControlName="txt_calltime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdetail_061", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_062")]
		public DateTime calltime
		{
			get { return _calltime; }
			set { _calltime = value; }
		}
		/// <summary>
		///点菜人
		/// <summary>
		[ModelInfo(Name = "点菜人",ControlName="txt_userid", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdetail_064", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_065")]
		public long userid
		{
			get { return _userid; }
			set { _userid = value; }
		}
		/// <summary>
		///点菜时间
		/// <summary>
		[ModelInfo(Name = "点菜时间",ControlName="txt_ordertime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdetail_067", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_068")]
		public DateTime ordertime
		{
			get { return _ordertime; }
			set { _ordertime = value; }
		}
		/// <summary>
		///附加费总金额
		/// <summary>
		[ModelInfo(Name = "附加费总金额",ControlName="txt_surchargeamount", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdetail_070", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_071")]
		public decimal surchargeamount
		{
			get { return _surchargeamount; }
			set { _surchargeamount = value; }
		}
		/// <summary>
		///成本金额
		/// <summary>
		[ModelInfo(Name = "成本金额",ControlName="txt_ocostprice", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdetail_073", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_074")]
		public decimal ocostprice
		{
			get { return _ocostprice; }
			set { _ocostprice = value; }
		}
		/// <summary>
		///消费金额
		/// <summary>
		[ModelInfo(Name = "消费金额",ControlName="txt_conmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdetail_076", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_077")]
		public decimal conmoney
		{
			get { return _conmoney; }
			set { _conmoney = value; }
		}
		/// <summary>
		///状态
		/// <summary>
		[ModelInfo(Name = "状态",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdetail_079", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_080")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
		}
		/// <summary>
		///订单编号
		/// <summary>
		[ModelInfo(Name = "订单编号",ControlName="txt_detailcode", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdetail_082", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_083")]
		public string detailcode
		{
			get { return _detailcode; }
			set { _detailcode = value; }
		}
		/// <summary>
		///会员编号
		/// <summary>
		[ModelInfo(Name = "会员编号",ControlName="txt_memcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdetail_085", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_086")]
		public string memcode
		{
			get { return _memcode; }
			set { _memcode = value; }
		}
		/// <summary>
		///预定编号（若有预定编号，结账时自动带上订金支付信息）
		/// <summary>
		[ModelInfo(Name = "预定编号（若有预定编号，结账时自动带上订金支付信息）",ControlName="txt_desCode", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdetail_088", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_089")]
		public string desCode
		{
			get { return _desCode; }
			set { _desCode = value; }
		}
		/// <summary>
		///预定单创建时间
		/// <summary>
		[ModelInfo(Name = "预定单创建时间",ControlName="txt_desctime", NotEmpty = false, Length = 19, NotEmptyECode = "choorderdetail_091", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_092")]
		public DateTime desctime
		{
			get { return _desctime; }
			set { _desctime = value; }
		}
		/// <summary>
		///预结算打印次数
		/// <summary>
		[ModelInfo(Name = "预结算打印次数",ControlName="txt_printcount", NotEmpty = false, Length = 9, NotEmptyECode = "choorderdetail_094", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_095")]
		public int printcount
		{
			get { return _printcount; }
			set { _printcount = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_remark", NotEmpty = false, Length = 128, NotEmptyECode = "choorderdetail_097", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_098")]
		public string remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		/// <summary>
		///餐别
		/// <summary>
		[ModelInfo(Name = "餐别",ControlName="txt_metname", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdetail_100", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_101")]
		public string metname
		{
			get { return _metname; }
			set { _metname = value; }
		}
		/// <summary>
		///提成人id
		/// <summary>
		[ModelInfo(Name = "提成人id",ControlName="txt_pushid", NotEmpty = false, Length = 128, NotEmptyECode = "choorderdetail_103", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_104")]
		public string pushid
		{
			get { return _pushid; }
			set { _pushid = value; }
		}
		/// <summary>
		///提成人姓名
		/// <summary>
		[ModelInfo(Name = "提成人姓名",ControlName="txt_pushname", NotEmpty = false, Length = 256, NotEmptyECode = "choorderdetail_106", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_107")]
		public string pushname
		{
			get { return _pushname; }
			set { _pushname = value; }
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
		[ModelInfo(Name = "",ControlName="txt_ispresented", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdetail_115", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_116")]
		public string ispresented
		{
			get { return _ispresented; }
			set { _ispresented = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_presentedcode", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdetail_118", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_119")]
		public string presentedcode
		{
			get { return _presentedcode; }
			set { _presentedcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_cardcode", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdetail_121", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_122")]
		public string cardcode
		{
			get { return _cardcode; }
			set { _cardcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_mobile", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdetail_124", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_125")]
		public string mobile
		{
			get { return _mobile; }
			set { _mobile = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_coupons", NotEmpty = false, Length = 1600, NotEmptyECode = "choorderdetail_127", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_128")]
		public string coupons
		{
			get { return _coupons; }
			set { _coupons = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_metcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdetail_130", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_131")]
		public string metcode
		{
			get { return _metcode; }
			set { _metcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_ttcode", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdetail_133", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_134")]
		public string ttcode
		{
			get { return _ttcode; }
			set { _ttcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_customer", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdetail_136", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_137")]
		public string customer
		{
			get { return _customer; }
			set { _customer = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_amanagerid", NotEmpty = false, Length = 16, NotEmptyECode = "choorderdetail_139", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_140")]
		public string amanagerid
		{
			get { return _amanagerid; }
			set { _amanagerid = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_amanagername", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdetail_142", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_143")]
		public string amanagername
		{
			get { return _amanagername; }
			set { _amanagername = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_olossmoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdetail_145", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_146")]
		public decimal olossmoney
		{
			get { return _olossmoney; }
			set { _olossmoney = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_detailtype", NotEmpty = false, Length = 1, NotEmptyECode = "choorderdetail_148", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_149")]
		public string detailtype
		{
			get { return _detailtype; }
			set { _detailtype = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_predishemoney", NotEmpty = false, Length = 18, NotEmptyECode = "choorderdetail_151", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_152")]
		public decimal predishemoney
		{
			get { return _predishemoney; }
			set { _predishemoney = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_TerminalType", NotEmpty = false, Length = 2, NotEmptyECode = "choorderdetail_154", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_155")]
		public string TerminalType
		{
			get { return _TerminalType; }
			set { _TerminalType = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_depcode", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdetail_157", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_158")]
		public string depcode
		{
			get { return _depcode; }
			set { _depcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_depname", NotEmpty = false, Length = 32, NotEmptyECode = "choorderdetail_160", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "choorderdetail_161")]
		public string depname
		{
			get { return _depname; }
			set { _depname = value; }
		}        
    }
}