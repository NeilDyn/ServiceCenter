using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.BitlyAPI
{
    public class BitlyAPIErrorHandler
    {
        public string message { get; set; }
        public Error[] errors { get; set; }
        public string resource { get; set; }
        public string description { get; set; }
    }

    public class Error
    {
        public string field { get; set; }
        public string message { get; set; }
        public string error_code { get; set; }
    }

}