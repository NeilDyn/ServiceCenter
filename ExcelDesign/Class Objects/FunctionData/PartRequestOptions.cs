using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.FunctionData
{
    public class PartRequestOptions
    {
        public string PartRequestOption { get; set; }

        public PartRequestOptions(string partRequestOptionP)
        {
            PartRequestOption = partRequestOptionP;
        }

        public PartRequestOptions()
        {

        }
    }
}