using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class PostedPackageLine
    {
        private string serialNo;

        public PostedPackageLine(string serialNoP)
        {
            this.SerialNo = serialNoP;
        }

        public PostedPackageLine()
        {

        }

        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }
    }
}