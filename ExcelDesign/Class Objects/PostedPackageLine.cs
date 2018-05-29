using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class PostedPackageLine
    {
        private string serialNo;
        private string packageNo;
        private string itemNo;
        private string description;
        private int quantity;
        private string type;
        private double price;
        
        public PostedPackageLine(string serialNoP, string packNoP, string itemNoP, string descP, int qtyP, double priceP)
        {
            this.SerialNo = serialNoP;
            this.serialNo = serialNoP;
            this.packageNo = packNoP;
            this.itemNo = itemNoP;
            this.description = descP;
            this.quantity = qtyP;
            this.price = priceP;
        }

        public PostedPackageLine()
        {

        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }


        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        public string ItemNo
        {
            get { return itemNo; }
            set { itemNo = value; }
        }

        public string PackageNo
        {
            get { return packageNo; }
            set { packageNo = value; }
        }
    }
}