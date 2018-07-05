using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public static class DoubleExtensions
    {
        public static string ToGBString(this double value)
        {
            return value.ToString(CultureInfo.GetCultureInfo("en-GB"));
        }
    }
}