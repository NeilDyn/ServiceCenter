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
    public partial class SingleReturnOrderTableHeader : System.Web.UI.UserControl
    {
        protected Control singleReturnOrderDetailTable;

        protected const string singleReturnOrderDetailPath = "../TableData/SingleReturnOrderDetail.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateHeader(ReturnHeader rh, int headCount)
        {
            TableCell tc = new TableCell();
            TableRow tr = new TableRow();

            this.ReturnOrderSequence.Text = "Return " + headCount.ToString();
            this.thcRMANo.Text = rh.RMANo;
            this.thcExternalDocumentNo.Text = rh.ExternalDocumentNo;

            singleReturnOrderDetailTable = LoadControl(singleReturnOrderDetailPath);
            singleReturnOrderDetailTable.ID = "singleReturnOrderDetailTable_" + headCount.ToString();
            ((SingleReturnOrderDetail)singleReturnOrderDetailTable).PopulateDetail(rh);

            tc.Height = new Unit("100%");
            tc.ColumnSpan = 4;
            tc.Controls.Add(singleReturnOrderDetailTable);
            tr.Cells.Add(tc);
            this.tblSingleReturnOrderTableHeader.Rows.Add(tr);
        }
    }
}