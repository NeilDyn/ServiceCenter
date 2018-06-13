using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.FunctionData
{
    public class Defects
    {
        public string Option { get; set; }

        public Defects()
        {

        }

        public Defects(string optionP)
        {
            this.Option = optionP;
        }
    }
}