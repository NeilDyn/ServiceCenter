using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class Item
    {
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }

        public Item(string itemNoP, string descriptionP, double unitPriceP)
        {
            ItemNo = itemNoP;
            Description = descriptionP;
            UnitPrice = unitPriceP;
        }

        public Item()
        {

        }
    }
}