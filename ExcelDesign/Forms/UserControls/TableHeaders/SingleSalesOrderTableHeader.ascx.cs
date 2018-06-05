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

        protected const string singleSalesOrderDetailPath = "../TableData/SingleSalesOrderDetail.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateHeader(SalesHeader sh, int headCount)
        {
            TableCell tc = new TableCell();
            TableRow tr = new TableRow();

            this.SalesOrderSequence.Text = "Order " + headCount.ToString();
            this.thcExternalDocumentNo.Text = sh.ExternalDocumentNo;

            singleSalesOrderDetailTable = LoadControl(singleSalesOrderDetailPath);
            singleSalesOrderDetailTable.ID = "singleSalesOrderDetailTable_" + headCount.ToString();
            ((SingleSalesOrderDetail)singleSalesOrderDetailTable).PopulateDetail(sh, headCount);

            tc.Height = new Unit("100%");
            tc.ColumnSpan = 4;
            tc.Controls.Add(singleSalesOrderDetailTable);
            tr.Cells.Add(tc);
            this.tblSingleSalesOrderTableHeader.Rows.Add(tr);
        }
    }
}