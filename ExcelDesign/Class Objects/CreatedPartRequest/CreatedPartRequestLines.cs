﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.CreatedPartRequest
{
    public class CreatedPartRequestLines
    {
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double LineAmount { get; set; }

        public CreatedPartRequestLines(string itemNoP, string descriptionP, int quantityP, double priceP, double lineAmountP)
        {
            ItemNo = itemNoP;
            Description = descriptionP;
            Quantity = quantityP;
            Price = priceP;
            LineAmount = lineAmountP;
        }

        public CreatedPartRequestLines()
        {

        }
    }
}