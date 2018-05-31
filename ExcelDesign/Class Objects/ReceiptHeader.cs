using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class ReceiptHeader
    {
        private string no;
        private string externalDocumentNo;
        private string receiptDate;
        private List<ReceiptLine> receiptLines;

        public ReceiptHeader()
        {

        }

        public ReceiptHeader(string noP, string externalDocumentNoP, string receiptDateP, List<ReceiptLine> receiptLinesP)
        {
            this.No = noP;
            this.ExternalDocumentNo = externalDocumentNoP;
            this.ReceiptDate = receiptDateP;
            this.ReceiptLines = receiptLinesP;
        }

        public List<ReceiptLine> ReceiptLines
        {
            get { return receiptLines; }
            set { receiptLines = value; }
        }


        public string ReceiptDate
        {
            get { return receiptDate; }
            set { receiptDate = value; }
        }


        public string ExternalDocumentNo
        {
            get { return externalDocumentNo; }
            set { externalDocumentNo = value; }
        }


        public string No
        {
            get { return no; }
            set { no = value; }
        }

    }
}