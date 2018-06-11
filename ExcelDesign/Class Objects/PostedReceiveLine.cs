using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class PostedReceiveLine
    {
        public string Type { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ReceiveNo { get; set; }
        public string SerialNo { get; set; }
        public string ItemNo { get; set; }

        public PostedReceiveLine()
        {

        }

        public PostedReceiveLine(string itemNoP, string serialNoP, string receiveNoP, string descriptionP, int quantityP, string typeP)
        {
            this.ItemNo = itemNoP;
            this.SerialNo = serialNoP;
            this.ReceiveNo = receiveNoP;
            this.Description = descriptionP;
            this.Quantity = quantityP;
            this.Type = typeP;
        }
    }
}