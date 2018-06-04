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

        protected const string singleSalesOrderHeaderPath = "../TableHeaders/SingleSalesOrderTableHeader.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadHeader(List<SalesHeader> salesHeaderList)
        {
            this.thcTotalOrders.Text = salesHeaderList.Count.ToString();

            int salesCount = 1;
            foreach (SalesHeader salesHeader in salesHeaderList)
            {
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();
                singleSalesOrderHeader = LoadControl(singleSalesOrderHeaderPath);
                singleSalesOrderHeader.ID = "singleSalesOrderHeader_" + salesCount.ToString();
                ((SingleSalesOrderTableHeader)singleSalesOrderHeader).PopulateHeader(salesHeader, salesCount);

                tc.Height = new Unit("100%");
                tc.ColumnSpan = 7;
                tc.Controls.Add(singleSalesOrderHeader);
                tr.Cells.Add(tc);
                this.tblSalesOrderHeader.Rows.Add(tr);
                salesCount++;
            }
        }
    }
}