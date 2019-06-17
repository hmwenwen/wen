using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XJWZCatering.CommonBasic;
namespace XJWZCatering.Model
{
    public class BaseModel
    {
        private string _buscode = Helper.GetAppSettings("BusCode");

        /// <summary>
        ///引用商户表Business的商户编号字段buscode的值
        /// <summary>
        [ModelInfo(Name = "商户编号", ControlName = "txt_buscode", NotEmpty = false, Length = 16, RType = RegularExpressions.RegExpType.Normal)]
        public string buscode
        {
            get { return _buscode; }
            set { _buscode = value; }
        }
    }
}
