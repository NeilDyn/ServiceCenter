using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class ReceiptLine
    {
        private string itemNo;
        private string description;
        private int quantity;
        private int quantityReceived;
        private double price;
        private double lineAmount;

        public ReceiptLine()
        {

        }

        public ReceiptLine(string itemNoP, string descriptionP, int quantityP, int quantityReceivedP, double priceP, double lineAmountP)
        {
            this.ItemNo = itemNoP;
            this.Description = descriptionP;
            this.Quantity = quantityP;
            this.QuantityReceived = quantityReceivedP;
            this.Price = priceP;
            this.LineAmount = lineAmountP;
        }

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


        public int QuantityReceived
        {
            get { return quantityReceived; }
            set { quantityReceived = value; }
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