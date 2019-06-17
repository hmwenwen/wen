using System;
using System.ComponentModel;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    [Description("区域信息表")]
    
    public class areasEntity
    {
		private int _id = 0;
		private int _parentid = 0;
		private int _areaid = 0;
		private string _area = string.Empty;
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
		[ModelInfo(Name = "省份ID",ControlName="txt_parentid", NotEmpty = false, Length = 4, NotEmptyECode = "areas_004", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "areas_005")]
		public int parentid
		{
			get { return _parentid; }
			set { _parentid = value; }
		}
		/// <summary>
		///区域ID
		/// <summary>
		[ModelInfo(Name = "区域ID",ControlName="txt_areaid", NotEmpty = false, Length = 4, NotEmptyECode = "areas_007", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "areas_008")]
		public int areaid
		{
			get { return _areaid; }
			set { _areaid = value; }
		}
		/// <summary>
		///区域名称
		/// <summary>
		[ModelInfo(Name = "区域名称",ControlName="txt_area", NotEmpty = false, Length = 128, NotEmptyECode = "areas_010", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "areas_011")]
		public string area
		{
			get { return _area; }
			set { _area = value; }
		}
		/// <summary>
		///城市字母
		/// <summary>
		[ModelInfo(Name = "城市字母",ControlName="txt_letter", NotEmpty = false, Length = 1, NotEmptyECode = "areas_013", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "areas_014")]
		public string letter
		{
			get { return _letter; }
			set { _letter = value; }
		}
		/// <summary>
		///备注
		/// <summary>
		[ModelInfo(Name = "备注",ControlName="txt_descr", NotEmpty = false, Length = 64, NotEmptyECode = "areas_016", RType = RegularExpressions.RegExpType.Normal, RTypeECode = "areas_017")]
		public string descr
		{
			get { return _descr; }
			set { _descr = value; }
		}        
    }
}