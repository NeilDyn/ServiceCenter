using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class ShipmentLine
    {
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int QuantityShipped { get; set; }
        public double Price { get; set; }
        public double LineAmount { get; set; }
        public string Type { get; set; }

        public ShipmentLine(string itemNoP, string descriptionP, int quantityP, int quantityShippedP, double priceP, double lineAmountP, string typeP)
        {
            this.ItemNo = itemNoP;
            this.Description = descriptionP;
            this.Quantity = quantityP;
            this.QuantityShipped = quantityShippedP;
            this.Price = priceP;
            this.LineAmount = lineAmountP;
            this.Type = typeP;
        }

        public ShipmentLine()
        {

        }    
    }
}