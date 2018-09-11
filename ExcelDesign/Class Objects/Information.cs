using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class Information
    {
        public int ObjectID { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public string ObjectDate { get; set; }
        public string ObjectTime { get; set; }
        public string ObjectVersionList { get; set; }
        public bool ObjectCompiled { get; set; }

        public Information(int objectIDP, string objectNameP, string objectTypeP, string objectDateP, string objectTimeP, string objectVersionListP, bool objectCompiledP)
        {
            ObjectID = objectIDP;
            ObjectName = objectNameP;
            ObjectType = objectTypeP;
            ObjectDate = objectDateP;
            ObjectTime = objectTimeP;
            ObjectVersionList = objectVersionListP;
            ObjectCompiled = objectCompiledP;
        }

        public Information()
        {

        }
    }
}