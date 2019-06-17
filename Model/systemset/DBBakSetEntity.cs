using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("数据备份设置")]
    
    public class DBBakSetEntity
    {
		private long _dbsid = 0;
		private string _buscode = string.Empty;
		private string _stocode = string.Empty;
		private string _bakpath = string.Empty;
		private string _bakcycle = string.Empty;
		private DateTime _btime = DateTime.Parse("1900-01-01");
		private string _isauto = string.Empty;
		private int _durday = 0;
		private long _cuser = 0;
		private DateTime _ctime = DateTime.Parse("1900-01-01");

		/// <summary>
		///标识
		/// <summary>
		public long dbsid
		{
			get { return _dbsid; }
			set { _dbsid = value; }
		}
		/// <summary>
		///商户编号
		/// <summary>
		[ModelInfo(Name = "商户编号",ControlName="txt_buscode", NotEmpty = false, Length = 16, NotEmptyECode = "DBBakSet_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DBBakSet_005")]
		public string buscode
		{
			get { return _buscode; }
			set { _buscode = value; }
		}
		/// <summary>
		///门店编号
		/// <summary>
		[ModelInfo(Name = "门店编号",ControlName="txt_stocode", NotEmpty = false, Length = 8, NotEmptyECode = "DBBakSet_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DBBakSet_008")]
		public string stocode
		{
			get { return _stocode; }
			set { _stocode = value; }
		}
		/// <summary>
		///备份目录
		/// <summary>
		[ModelInfo(Name = "备份目录",ControlName="txt_bakpath", NotEmpty = true, Length = 256, NotEmptyECode = "DBBakSet_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DBBakSet_011")]
		public string bakpath
		{
			get { return _bakpath; }
			set { _bakpath = value; }
		}
		/// <summary>
		///备份周期
		/// <summary>
		[ModelInfo(Name = "备份周期",ControlName="txt_bakcycle", NotEmpty = true, Length = 32, NotEmptyECode = "DBBakSet_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DBBakSet_014")]
		public string bakcycle
		{
			get { return _bakcycle; }
			set { _bakcycle = value; }
		}
		/// <summary>
		///执行开始时间
		/// <summary>
		[ModelInfo(Name = "执行开始时间",ControlName="txt_btime", NotEmpty = true, Length = 8, NotEmptyECode = "DBBakSet_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DBBakSet_017")]
		public DateTime btime
		{
			get { return _btime; }
			set { _btime = value; }
		}
		/// <summary>
		///是否自动执行
		/// <summary>
		[ModelInfo(Name = "是否自动执行",ControlName="txt_isauto", NotEmpty = true, Length = 1, NotEmptyECode = "DBBakSet_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DBBakSet_020")]
		public string isauto
		{
			get { return _isauto; }
			set { _isauto = value; }
		}
		/// <summary>
		///保留备份数据时长(天)
		/// <summary>
		[ModelInfo(Name = "保留备份数据时长(天)",ControlName="txt_durday", NotEmpty = false, Length = 4, NotEmptyECode = "DBBakSet_022", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "DBBakSet_023")]
		public int durday
		{
			get { return _durday; }
			set { _durday = value; }
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