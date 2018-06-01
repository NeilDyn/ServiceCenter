using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;

namespace ExcelDesign.Forms.UserControls
{
    public partial class SalesReturnDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateControl(ReturnHeader rh, int headerCount)
        {
            this.lblOrderSequence.Text = "Return " + headerCount.ToString();
            this.lblReturnStatus.Text = rh.ReturnStatus;
            this.lblOrderDate.Text = rh.OrderDate;
            this.lblRMANo.Text = rh.RMANo;
            this.lblChannelName.Text = rh.ChannelName;
            this.lblZendeskTicket.Text = "1234";
            this.lblZendeskTicketNo.Text = "1";

            if (rh.ReceiptHeaderObj.Count != 0)
            {
                this.lblReceiptDate.Text = rh.ReceiptHeaderObj[0].ReceiptDate;
                this.lblReceipts.Text = rh.ReceiptHeaderObj.Count.ToString();

                this.gdvReturnVView.DataSource = rh.ReceiptHeaderObj[0].ReceiptLines;
                this.gdvReturnVView.DataBind();
            }

            if (rh.PostedReceiveObj.Count != 0)
            {
                this.lblPackages.Text = rh.PostedReceiveObj.Count.ToString();
            }

            this.lblExternalDocumentNo.Text = rh.ExternalDocumentNo;
            this.lblReturnTrackingNo.Text = rh.ReturnTrackingNo;
            this.lblOrderDate.Text = rh.OrderDate;
        }
    }
}