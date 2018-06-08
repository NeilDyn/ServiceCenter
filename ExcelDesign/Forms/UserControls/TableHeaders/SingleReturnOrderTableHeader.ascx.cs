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

        public ReturnHeader Header { get; set; }
        public int HeadCount { get; set; }
        public int CustID { get; set; }

        private TableRow tr;
        private TableCell tc;

        protected const string singleReturnOrderDetailPath = "../TableData/SingleReturnOrderDetail.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ReturnOrderSequence.Text = "Return " + HeadCount.ToString();
            this.thcRMANo.Text = Header.RMANo;
            this.thcExternalDocumentNo.Text = Header.ExternalDocumentNo;

            this.btnExpandCurrentReturn.ID = "btnExpandCurrentReturn_" + CustID.ToString() + "_" + HeadCount.ToString();
            PopulateData();
        }

        protected void PopulateData()
        {
            tc = new TableCell();
            tr = new TableRow();
        
            singleReturnOrderDetailTable = LoadControl(singleReturnOrderDetailPath);
            singleReturnOrderDetailTable.ID = "singleReturnOrderDetailTable_" + HeadCount.ToString();
            ((SingleReturnOrderDetail)singleReturnOrderDetailTable).Rh = Header;
            ((SingleReturnOrderDetail)singleReturnOrderDetailTable).CountID = HeadCount;
            ((SingleReturnOrderDetail)singleReturnOrderDetailTable).CustID = CustID;

            tc.Height = new Unit("100%");
            tc.ColumnSpan = 6;
            tc.Controls.Add(singleReturnOrderDetailTable);
            tr.Cells.Add(tc);
            tr.ID = "singleReturnOrderDetail_" + CustID.ToString() + "_" + HeadCount.ToString();
            this.tblSingleReturnOrderTableHeader.Rows.Add(tr);
        }
    }
}