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
        public List<ReturnHeader> ReturnHeaderList { get; set; }
        public int CustID { get; set; }
        public int CustomerCount { get; set; }
        public int ReturnOrdersCount { get; set; }

        private TableRow tr;
        private TableCell tc;

        protected const string singleReturnOrderHeaderPath = "../TableHeaders/SingleReturnOrderTableHeader.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.thcTotalReturns.Text = ReturnHeaderList.Count.ToString();
             
            if(ReturnOrdersCount == 1)
            {
                this.btnExpandReturn.Visible = false;
            }
            else
            {
                this.btnExpandReturn.ID = "btnExpandReturn_" + CustID.ToString();
            }

            PopulateData();
        }

        protected void PopulateData()
        {
            int returnCount = 1;

            foreach (ReturnHeader returnHeader in ReturnHeaderList)
            {
                tr = new TableRow();
                tc = new TableCell();
                singleReturnOrderHeader = LoadControl(singleReturnOrderHeaderPath);
                singleReturnOrderHeader.ID = "singleSalesReturnHeader_" + returnCount.ToString();

                ((SingleReturnOrderTableHeader)singleReturnOrderHeader).Header = returnHeader;
                ((SingleReturnOrderTableHeader)singleReturnOrderHeader).HeadCount = returnCount;
                ((SingleReturnOrderTableHeader)singleReturnOrderHeader).CustID = CustID;
                ((SingleReturnOrderTableHeader)singleReturnOrderHeader).CustomerCount = CustomerCount;
                ((SingleReturnOrderTableHeader)singleReturnOrderHeader).ReturnOrdersCount = ReturnOrdersCount;

                tc.Height = new Unit("100%");
                tc.ColumnSpan = 4;
                tc.Controls.Add(singleReturnOrderHeader);
                tr.Cells.Add(tc);
                tr.ID = "salesReturnDetailHeader_" + CustID.ToString();
                this.tblReturnOrderHeader.Rows.Add(tr);
                returnCount++;
            }
        }
    }
}