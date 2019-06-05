using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.BitlyAPI
{
    public class BitlyShorten
    {
        public References references { get; set; }
        public bool archived { get; set; }
        public string[] tags { get; set; }
        public string created_at { get; set; }
        public string title { get; set; }
        public Deeplink[] deeplinks { get; set; }
        public string created_by { get; set; }
        public string long_url { get; set; }
        public string client_id { get; set; }
        public string[] custom_bitlinks { get; set; }
        public string link { get; set; }
        public string id { get; set; }
    }

    public class References
    {
        public string property1 { get; set; }
        public string property2 { get; set; }
    }

    public class Deeplink
    {
        public string bitlink { get; set; }
        public string install_url { get; set; }
        public string created { get; set; }
        public string app_uri_path { get; set; }
        public string modified { get; set; }
        public string install_type { get; set; }
        public string app_guid { get; set; }
        public string guid { get; set; }
        public string os { get; set; }
    }

}