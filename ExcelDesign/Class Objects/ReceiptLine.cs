﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class ReceiptLine
    {
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int QuantityReceived { get; set; }
        public double Price { get; set; }
        public double LineAmount { get; set; }

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
    }
}