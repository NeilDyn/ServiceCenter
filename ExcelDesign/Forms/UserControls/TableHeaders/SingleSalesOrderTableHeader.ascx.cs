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
    public partial class SingleSalesOrderTableHeader : System.Web.UI.UserControl
    {
        protected Control singleSalesOrderDetailTable;
        public SalesHeader Header { get; set; }
        public int HeadCount { get; set; }
        public int CustomerCount { get; set; }
        public int SalesOrderCount { get; set; }

        public int CustID { get; set; }

        private TableRow tr;
        private TableCell tc;

        private TableRow lineRow;
        private TableCell lineCell;

        protected const string singleSalesOrderDetailPath = "../TableData/SingleSalesOrderDetail.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            SellToCustomers sellToCust = SellToCustomers.Invalid;
            Enum.TryParse(Header.SellToCustomerNo, out sellToCust);

            this.thcExternalDocumentNo.Text = SetExternalDocumentNo(sellToCust, Header.ExternalDocumentNo);

            if(SalesOrderCount == 1)
            {
                this.btnExpandCurrentOrder.Visible = false;              
                this.SalesOrderSequence.Text = "Order";
            }
            else
            {
                this.btnExpandCurrentOrder.Visible = true;
                this.SalesOrderSequence.Text = "Order " + HeadCount.ToString();
                this.btnExpandCurrentOrder.ID = "btnExpandCurrentOrder_" + CustID.ToString() + "_" + HeadCount.ToString();
            }

            if (Header.IsExchangeOrder)
            {
                this.SalesOrderSequence.Text = "Exchange Order For " + Header.RMANo;
            }

            PopulateHeader();
        }

        protected string SetExternalDocumentNo(SellToCustomers customerNo, string externalDocumentNo)
        {
            string textString = string.Empty;

            switch (customerNo)
            {
                case SellToCustomers.AMZMKT001:
                    textString = "<a href='https://sellercentral.amazon.com/hz/orders/details?_encoding=UTF8&orderId=" + externalDocumentNo + "' target = '_blank'>" + externalDocumentNo + "</a>";
                    break;

                default:
                    textString = externalDocumentNo;
                    break;
            }

            return textString;
        }

        public void PopulateHeader()
        {
            tr = new TableRow();
            tc = new TableCell();

            lineRow = new TableRow();
            lineCell = new TableCell
            {
                Text = "<hr class='SeperatorOrders'/>",
                Height = new Unit("100%"),
                ColumnSpan = 4
            };

            lineRow.Cells.Add(lineCell);

            singleSalesOrderDetailTable = LoadControl(singleSalesOrderDetailPath);
            singleSalesOrderDetailTable.ID = "singleSalesOrderDetailTable_" + HeadCount.ToString();
           
            tc.Height = new Unit("100%");
            tc.ColumnSpan = 4;
            tc.Controls.Add(singleSalesOrderDetailTable);
            ((SingleSalesOrderDetail)singleSalesOrderDetailTable).Sh = Header;
            ((SingleSalesOrderDetail)singleSalesOrderDetailTable).CountID = HeadCount;
            ((SingleSalesOrderDetail)singleSalesOrderDetailTable).CustID = CustID;
            ((SingleSalesOrderDetail)singleSalesOrderDetailTable).CustomerCount = CustomerCount;

            tr.Cells.Add(tc);
            tr.ID = "singleSalesOrderDetail_" + CustID.ToString() + "_" + HeadCount.ToString();

            this.tblSingleSalesOrderTableHeader.Rows.Add(tr);
            this.tblSingleSalesOrderTableHeader.Rows.Add(lineRow);
        }
    }
}