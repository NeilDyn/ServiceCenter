using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class Warranty
    {
        public string Status { get; set; }
        public string Policy { get; set; }
        public string DaysRemaining { get; set; }

        public Warranty()
        {

        }
        public Warranty(string pStatus, string pPolicy, string pDaysRemaining)
        {
            Status = pStatus;
            Policy = pPolicy;
            DaysRemaining = pDaysRemaining;
        }    
    }
}