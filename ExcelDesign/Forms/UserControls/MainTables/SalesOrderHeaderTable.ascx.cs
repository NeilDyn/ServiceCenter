using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.TableHeaders;

namespace ExcelDesign.Forms.UserControls.MainTables
{
    public partial class SalesOrderHeaderTable : System.Web.UI.UserControl
    {
        protected Control singleSalesOrderHeader;
        public List<SalesHeader> SalesHeaderList { get; set; }
        public int CustID { get; set; }

        private TableRow tr;
        private TableCell tc;

        protected const string singleSalesOrderHeaderPath = "../TableHeaders/SingleSalesOrderTableHeader.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.thcTotalOrders.Text = SalesHeaderList.Count.ToString();
            this.btnExpandOrders.Click += new EventHandler(ExpandOrder);

            if(Session["SalesOrderHeaderTable_" + CustID.ToString()] == null)
            {
                Session["SalesOrderHeaderTable_" + CustID.ToString()] = this.tblSalesOrderHeader;
            }

            Session["SalesOrderCount_" + CustID.ToString()] = SalesHeaderList.Count;
        }

        protected void ExpandOrder(object sender, EventArgs e)
        {
            if(btnExpandOrders.Text == "+")
            {
                btnExpandOrders.Text = "-";

                int salesCount = 1;
                foreach (SalesHeader salesHeader in SalesHeaderList)
                {
                    tr = new TableRow();
                    tc = new TableCell();
                    singleSalesOrderHeader = LoadControl(singleSalesOrderHeaderPath);
                    singleSalesOrderHeader.ID = "singleSalesOrderHeader_" + salesCount.ToString();
                    ((SingleSalesOrderTableHeader)singleSalesOrderHeader).Header = salesHeader;
                    ((SingleSalesOrderTableHeader)singleSalesOrderHeader).HeadCount = salesCount;
                    ((SingleSalesOrderTableHeader)singleSalesOrderHeader).CustID = CustID;

                    tc.Height = new Unit("100%");
                    tc.ColumnSpan = 7;
                    tc.Controls.Add(singleSalesOrderHeader);
                    tr.Cells.Add(tc);
                    this.tblSalesOrderHeader.Rows.Add(tr);
                    salesCount++;
                    Session["SalesOrderHeaderTableCell_CustID" + CustID.ToString() + "_" + salesCount.ToString()] = tc;
                }
            }
            else
            {
                btnExpandOrders.Text = "+";

                this.tblSalesOrderHeader.Rows.Remove(tr);
            }
        }
    }
}