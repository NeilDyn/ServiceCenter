using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.TableData;

namespace ExcelDesign.Forms.UserControls.TableHeaders
{
    public partial class SingleSalesOrderTableHeader : System.Web.UI.UserControl
    {
        protected Control singleSalesOrderDetailTable;
        public SalesHeader Header { get; set; }
        public int HeadCount { get; set; }

        public int CustID { get; set; }

        protected const string singleSalesOrderDetailPath = "../TableData/SingleSalesOrderDetail.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.SalesOrderSequence.Text = "Order " + HeadCount.ToString();
            this.thcExternalDocumentNo.Text = Header.ExternalDocumentNo;

            if (Session["SingleSalesOrderTableHeader_" + CustID.ToString()] == null)
            {
                Session["SingleSalesOrderTableHeader_" + CustID.ToString()] = this.tblSingleSalesOrderTableHeader;
            }
            PopulateHeader();
        }

        public void PopulateHeader()
        {          
            TableCell tc = new TableCell();
            TableRow tr = new TableRow();
         
            singleSalesOrderDetailTable = LoadControl(singleSalesOrderDetailPath);
            singleSalesOrderDetailTable.ID = "singleSalesOrderDetailTable_" + HeadCount.ToString();
           
            tc.Height = new Unit("100%");
            tc.ColumnSpan = 4;
            tc.Controls.Add(singleSalesOrderDetailTable);
            ((SingleSalesOrderDetail)singleSalesOrderDetailTable).Sh = Header;
            ((SingleSalesOrderDetail)singleSalesOrderDetailTable).CountID = HeadCount;
            ((SingleSalesOrderDetail)singleSalesOrderDetailTable).CustID = CustID;

            tr.Cells.Add(tc);
            this.tblSingleSalesOrderTableHeader.Rows.Add(tr);
            Session["SingleSalesOrderTableHeaderDetailCell_CustID" + CustID.ToString() + "_" + HeadCount.ToString()] = tc;
        }

        protected void btnExpandCurrentCustomer_Click(object sender, EventArgs e)
        {

        }
    }
}