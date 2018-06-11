using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class PostedPackageLine
    {
        public double Price { get; set; }
        public string SerialNo { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ItemNo { get; set; }
        public string PackageNo { get; set; }

        public PostedPackageLine(string serialNoP, string packNoP, string itemNoP, string descP, int qtyP, double priceP)
        {
            this.SerialNo = serialNoP;
            this.SerialNo = serialNoP;
            this.PackageNo = packNoP;
            this.ItemNo = itemNoP;
            this.Description = descP;
            this.Quantity = qtyP;
            this.Price = priceP;
        }

        public PostedPackageLine()
        {

        }       
    }
}