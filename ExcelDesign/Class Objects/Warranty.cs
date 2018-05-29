using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class Warranty
    {
        private string status;
        private string policy;
        private int daysRemaining;
        public Warranty()
        {

        }
        public Warranty(string pStatus, string pPolicy, int pDaysRemaining)
        {
            status = pStatus;
            policy = pPolicy;
            daysRemaining = pDaysRemaining;
        }

        public string  Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Policy
        {
            get { return policy; }
            set { policy = value; }
        }
        
        public int DaysRemaining
        {
            get { return daysRemaining; }
            set { daysRemaining = value; }
        }
        
            
    }
}