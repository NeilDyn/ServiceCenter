using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.Enums;
using ExcelDesign.Forms.UserControls.TableData;

namespace ExcelDesign.Forms.UserControls.TableHeaders
{
    public partial class SingleReturnOrderTableHeader : System.Web.UI.UserControl
    {
        protected Control singleReturnOrderDetailTable;

        public ReturnHeader Header { get; set; }
        public int HeadCount { get; set; }
        public int CustID { get; set; }
        public int CustomerCount { get; set; }
        public int ReturnOrdersCount { get; set; }

        private TableRow tr;
        private TableCell tc;

        private TableRow lineRow;
        private TableCell lineCell;

        protected const string singleReturnOrderDetailPath = "../TableData/SingleReturnOrderDetail.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {          
            this.thcRMANo.Text = Header.RMANo;

            SellToCustomers sellToCust = SellToCustomers.Invalid;
            Enum.TryParse(Header.SellToCustomerNo, out sellToCust);

            this.thcExternalDocumentNo.Text = SetExternalDocumentNo(sellToCust, Header.ExternalDocumentNo);

            if (ReturnOrdersCount == 1)
            {
                this.ReturnOrderSequence.Text = "Return";
                this.btnExpandCurrentReturn.Visible = false;
            }
            else
            {
                this.btnExpandCurrentReturn.Visible = true;
                this.ReturnOrderSequence.Text = "Return " + HeadCount.ToString();
                this.btnExpandCurrentReturn.ID = "btnExpandCurrentReturn_" + CustID.ToString() + "_" + HeadCount.ToString();
            }
         
            PopulateData();
        }

        protected string SetExternalDocumentNo(SellToCustomers customerNo, string externalDocumentNo)
        {
            string textString = string.Empty;

            switch (customerNo)
            {
                case SellToCustomers.AMZMKT001:
                    textString = "<a href='https://sellercentral.amazon.com/hz/orders/details?_encoding=UTF8&orderId=" + externalDocumentNo + "' target = '_blank'>" + externalDocumentNo + "</a>";
                    break;

                case SellToCustomers.Invalid:
                    textString = externalDocumentNo;
                    break;

                default:
                    textString = externalDocumentNo;
                    break;
            }

            return textString;
        }

        protected void PopulateData()
        {
            tc = new TableCell();
            tr = new TableRow();

            lineRow = new TableRow();
            lineCell = new TableCell
            {
                Text = "<hr class='SeperatorOrders'/>",
                Height = new Unit("100%"),
                ColumnSpan = 6
            };

            singleReturnOrderDetailTable = LoadControl(singleReturnOrderDetailPath);
            singleReturnOrderDetailTable.ID = "singleReturnOrderDetailTable_" + HeadCount.ToString();
            ((SingleReturnOrderDetail)singleReturnOrderDetailTable).Rh = Header;
            ((SingleReturnOrderDetail)singleReturnOrderDetailTable).CountID = HeadCount;
            ((SingleReturnOrderDetail)singleReturnOrderDetailTable).CustID = CustID;
            ((SingleReturnOrderDetail)singleReturnOrderDetailTable).CustomerCount = CustomerCount;

            tc.Height = new Unit("100%");
            tc.ColumnSpan = 6;
            tc.Controls.Add(singleReturnOrderDetailTable);
            tr.Cells.Add(tc);
            tr.ID = "singleReturnOrderDetail_" + CustID.ToString() + "_" + HeadCount.ToString();
            this.tblSingleReturnOrderTableHeader.Rows.Add(tr);
            this.tblSingleReturnOrderTableHeader.Rows.Add(lineRow);
        }
    }
}