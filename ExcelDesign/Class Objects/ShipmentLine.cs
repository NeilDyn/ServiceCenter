using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class ShipmentLine
    {
        private string itemNo;
        private string description;
        private int quantity;
        private int quantityShipped;
        private double price;
        private double lineAmount;

        //private PostedPackageLine postedPackageLine;

        public ShipmentLine(string itemNoP, string descriptionP, int quantityP, int quantityShippedP, double priceP, double lineAmountP)
        {
            this.ItemNo = itemNoP;
            this.Description = descriptionP;
            this.Quantity = quantityP;
            this.quantityShipped = quantityShippedP;
            this.Price = priceP;
            this.LineAmount = lineAmountP;
           // this.PostedPackageLine = postedPackageLineP;
        }

        public ShipmentLine()
        {

        }

        //public PostedPackageLine PostedPackageLine
        //{
        //    get { return postedPackageLine; }
        //    set { postedPackageLine = value; }
        //}       

        public string ItemNo
        {
            get { return itemNo; }
            set { itemNo = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public int QuantityShipped
        {
            get { return quantityShipped; }
            set { quantityShipped = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public double LineAmount
        {
            get { return lineAmount; }
            set { lineAmount = value; }
        }
        
    }
}