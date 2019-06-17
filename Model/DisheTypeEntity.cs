using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("")]
    [Serializable]
    public class DisheTypeEntity
    {
		private long _lsid = 0;
		private long _distypeid = 0;
		private string _buscode = string.Empty;
		private string _stocode = string.Empty;
		private string _pdistypecode = string.Empty;
		private string _distypecode = string.Empty;
		private string _dispath = string.Empty;
		private string _distypename = string.Empty;
		private string _metcode = string.Empty;
		private string _fincode = string.Empty;
		private int _maxdiscount = 0;
		private int _busSort = 0;
		private string _status = string.Empty;
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
		[ModelInfo(Name = "",ControlName="txt_distypeid", NotEmpty = false, Length = 18, NotEmptyECode = "DisheType_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_005")]
		public long distypeid
		{
			get { return _distypeid; }
			set { _distypeid = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "DisheType_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_008")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "DisheType_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_011")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_pdistypecode", NotEmpty = false, Length = 16, NotEmptyECode = "DisheType_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_014")]
		public string pdistypecode
		{
			get { return _pdistypecode; }
			set { _pdistypecode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_distypecode", NotEmpty = false, Length = 16, NotEmptyECode = "DisheType_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_017")]
		public string distypecode
		{
			get { return _distypecode; }
			set { _distypecode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_dispath", NotEmpty = false, Length = 128, NotEmptyECode = "DisheType_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_020")]
		public string dispath
		{
			get { return _dispath; }
			set { _dispath = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_distypename", NotEmpty = false, Length = 32, NotEmptyECode = "DisheType_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_023")]
		public string distypename
		{
			get { return _distypename; }
			set { _distypename = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_metcode", NotEmpty = false, Length = 5, NotEmptyECode = "DisheType_025", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_026")]
		public string metcode
		{
			get { return _metcode; }
			set { _metcode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_fincode", NotEmpty = false, Length = 16, NotEmptyECode = "DisheType_028", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_029")]
		public string fincode
		{
			get { return _fincode; }
			set { _fincode = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_maxdiscount", NotEmpty = false, Length = 3, NotEmptyECode = "DisheType_031", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_032")]
		public int maxdiscount
		{
			get { return _maxdiscount; }
			set { _maxdiscount = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_busSort", NotEmpty = false, Length = 9, NotEmptyECode = "DisheType_034", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_035")]
		public int busSort
		{
			get { return _busSort; }
			set { _busSort = value; }
		}
		/// <summary>
		///
		/// <summary>
		[ModelInfo(Name = "",ControlName="txt_status", NotEmpty = false, Length = 1, NotEmptyECode = "DisheType_037", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DisheType_038")]
		public string status
		{
			get { return _status; }
			set { _status = value; }
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