using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Forms.UserControls.TableHeaders;
using ExcelDesign.Class_Objects;

namespace ExcelDesign.Forms.UserControls.MainTables
{
    public partial class ReturnOrderHeaderTable : System.Web.UI.UserControl
    {
        protected Control singleReturnOrderHeader;

        protected const string singleReturnOrderHeaderPath = "../TableHeaders/SingleReturnOrderTableHeader.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadHeader(List<ReturnHeader> returnHeaderList)
        {
            this.thcTotalReturns.Text = returnHeaderList.Count.ToString();

            int returnCount = 1;
            foreach (ReturnHeader returnHeader in returnHeaderList)
            {
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();
                singleReturnOrderHeader = LoadControl(singleReturnOrderHeaderPath);
                singleReturnOrderHeader.ID = "singleSalesReturnHeader_" + returnCount.ToString();
                ((SingleReturnOrderTableHeader)singleReturnOrderHeader).PopulateHeader(returnHeader, returnCount);

                tc.Height = new Unit("100%");
                tc.ColumnSpan = 7;
                tc.Controls.Add(singleReturnOrderHeader);
                tr.Cells.Add(tc);
                this.tblReturnOrderHeader.Rows.Add(tr);
                returnCount++;
            }
        }
    }
}