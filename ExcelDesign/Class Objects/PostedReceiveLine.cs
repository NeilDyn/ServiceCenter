using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class PostedReceiveLine
    {
        private string itemNo;
        private string serialNo;
        private string receiveNo;
        private string description;
        private int quantity;
        private string type;

        public PostedReceiveLine()
        {

        }

        public PostedReceiveLine(string itemNoP, string serialNoP, string receiveNoP, string descriptionP, int quantityP, string typeP)
        {
            this.ItemNo = itemNoP;
            this.serialNo = serialNoP;
            this.ReceiveNo = receiveNoP;
            this.Description = descriptionP;
            this.Quantity = quantityP;
            this.Type = typeP;
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


        public string ReceiveNo
        {
            get { return receiveNo; }
            set { receiveNo = value; }
        }


        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }


        public string ItemNo
        {
            get { return itemNo; }
            set { itemNo = value; }
        }

    }
}