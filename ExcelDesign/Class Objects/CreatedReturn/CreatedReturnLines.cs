using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.CreatedReturn
{
    public class CreatedReturnLines
    {
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double LineAmount { get; set; }
        public string REQReturnAction { get; set; }
        public string ReturnReason { get; set; }

        public CreatedReturnLines(string itemNoP, string descriptionP, int quantityP, double priceP, double lineAmountP,
                                    string reqReturnActionP, string returnReasonP)
        {
            ItemNo = itemNoP;
            Description = descriptionP;
            Quantity = quantityP;
            Price = priceP;
            LineAmount = lineAmountP;
            REQReturnAction = reqReturnActionP;
            ReturnReason = returnReasonP;
        }

        public CreatedReturnLines()
        {
                
        }
    }
}