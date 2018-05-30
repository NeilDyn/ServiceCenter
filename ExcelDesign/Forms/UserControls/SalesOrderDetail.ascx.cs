using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;

namespace ExcelDesign.Forms.UserControls
{
    public partial class SalesOrderDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        public void PopulateControl(SalesHeader sh, int headerCount)
        {
            this.lblOrderSequence.Text      = "Order " + headerCount.ToString();
            this.lblOrderStatus.Text        = sh.OrderStatus;
            this.lblOrderDate.Text          = sh.OrderDate;
            this.lblSalesOrderNumber.Text   = sh.SalesOrderNo;
            this.lblChannelName.Text        = sh.ChannelName;
            this.lblZendeskTicket.Text      = "1234";
            this.lblZendeskTicketNo.Text    = "1";
            this.lblShipmentDate.Text       = sh.ShipmentHeaderObject[0].ShippingDate;
            this.lblShipments.Text          = sh.ShipmentHeaderObject.Count.ToString();
            this.lblPackages.Text           = sh.PostedPackageObject.Count.ToString();
            this.lblShipMethod.Text         = sh.ShipmentHeaderObject[0].ShippingAgentService;
            this.lblTrackingNo.Text         = sh.PostedPackageObject[0].TrackingNo;

            this.lblExternalDocumentNo.Text = sh.ExternalDocumentNo;

            this.gdvOrderView.DataSource    = sh.ShipmentHeaderObject[0].ShipmentLines;
            this.gdvOrderView.DataBind();
        }
    }
}