using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("城市信息表")]
    
    public class citysEntity
    {
		private int _id = 0;
		private int _parentid = 0;
		private int _cityid = 0;
		private string _city = string.Empty;
		private string _hot = string.Empty;
		private string _letter = string.Empty;
		private string _descr = string.Empty;

		/// <summary>
		///标识
		/// <summary>
		public int id
		{
			get { return _id; }
			set { _id = value; }
		}
		/// <summary>
		///省份ID
		/// <summary>
		[ModelInfo(Name = "省份ID",ControlName="txt_parentid", NotEmpty = false, Length = 4, NotEmptyECode = "citys_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "citys_005")]
		public int parentid
		{
			get { return _parentid; }
			set { _parentid = value; }
		}
		/// <summary>
		///城市ID
		/// <summary>
		[ModelInfo(Name = "城市ID",ControlName="txt_cityid", NotEmpty = false, Length = 4, NotEmptyECode = "citys_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "citys_008")]
		public int cityid
		{
			get { return _cityid; }
			set { _cityid = value; }
		}
		/// <summary>
		///城市名称
		/// <summary>
		[ModelInfo(Name = "城市名称",ControlName="txt_city", NotEmpty = false, Length = 50, NotEmptyECode = "citys_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "citys_011")]
		public string city
		{
			get { return _city; }
			set { _city = value; }
		}
		/// <summary>
		///是否热门
		/// <summary>
		[ModelInfo(Name = "是否热门",ControlName="txt_hot", NotEmpty = false, Length = 1, NotEmptyECode = "citys_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "citys_014")]
		public string hot
		{
			get { return _hot; }
			set { _hot = value; }
		}
		/// <summary>
		///城市字母
		/// <summary>
		[ModelInfo(Name = "城市字母",ControlName="txt_letter", NotEmpty = false, Length = 1, NotEmptyECode = "citys_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "citys_017")]
		public string letter
		{
			get { return _letter; }
			set { _letter = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_descr", NotEmpty = false, Length = 64, NotEmptyECode = "citys_019", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "citys_020")]
		public string descr
		{
			get { return _descr; }
			set { _descr = value; }
		}        
    }
}