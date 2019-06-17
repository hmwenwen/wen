using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("省份信息表")]
    
    public class provincesEntity
    {
		private int _id = 0;
		private int _provinceid = 0;
		private string _province = string.Empty;
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
		[ModelInfo(Name = "省份ID",ControlName="txt_provinceid", NotEmpty = false, Length = 4, NotEmptyECode = "provinces_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "provinces_005")]
		public int provinceid
		{
			get { return _provinceid; }
			set { _provinceid = value; }
		}
		/// <summary>
		///省名称
		/// <summary>
		[ModelInfo(Name = "省名称",ControlName="txt_province", NotEmpty = true, Length = 50, NotEmptyECode = "provinces_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "provinces_008")]
		public string province
		{
			get { return _province; }
			set { _province = value; }
		}
		/// <summary>
		///城市字母
		/// <summary>
		[ModelInfo(Name = "城市字母",ControlName="txt_letter", NotEmpty = true, Length = 1, NotEmptyECode = "provinces_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "provinces_011")]
		public string letter
		{
			get { return _letter; }
			set { _letter = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_descr", NotEmpty = false, Length = 64, NotEmptyECode = "provinces_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "provinces_014")]
		public string descr
		{
			get { return _descr; }
			set { _descr = value; }
		}        
    }
}