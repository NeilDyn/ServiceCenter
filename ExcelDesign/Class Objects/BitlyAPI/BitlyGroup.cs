using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.BitlyAPI
{
    public class BitlyGroup
    {
        public Group[] groups { get; set; }

        public class Group
        {
            public References references { get; set; }
            public string name { get; set; }
            public object[] bsds { get; set; }
            public string created { get; set; }
            public bool is_active { get; set; }
            public string modified { get; set; }
            public string organization_guid { get; set; }
            public string role { get; set; }
            public string guid { get; set; }
        }

        public class References
        {
            public string property1 { get; set; }
            public string property2 { get; set; }
        }

    }
}