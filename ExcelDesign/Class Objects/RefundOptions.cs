using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class RefundOptions
    {
        public string Tier { get; set; }
        public string Option { get; set; }

        public RefundOptions(string tierP, string optionP)
        {
            Tier = tierP;
            Option = optionP;
        }

        public RefundOptions()
        {
                
        }

        public List<RefundOptions> Populate()
        {
            List<RefundOptions> populatedOptions = new List<RefundOptions>
            {
                new RefundOptions("User", "10%"),
                new RefundOptions("User", "20%"),
                new RefundOptions("User", "30%"),
                new RefundOptions("User", "40%"),
                new RefundOptions("User", "50%"),
                new RefundOptions("Supervisor", "10%"),
                new RefundOptions("Supervisor", "20%"),
                new RefundOptions("Supervisor", "30%"),
                new RefundOptions("Supervisor", "40%"),
                new RefundOptions("Supervisor", "50%"),
                new RefundOptions("Supervisor", "60%"),
                new RefundOptions("Supervisor", "70%"),
                new RefundOptions("Supervisor", "80%"),
                new RefundOptions("Supervisor", "90%"),
                new RefundOptions("Supervisor", "100%"),
            };

            return populatedOptions;
        }
    }
}