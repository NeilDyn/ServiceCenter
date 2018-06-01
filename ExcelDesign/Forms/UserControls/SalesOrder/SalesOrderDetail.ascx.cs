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

        public void PopulateControl(SalesHeader sh, int headerCount)
        {
            this.lblOrderSequence.Text      = "Order " + headerCount.ToString();
            this.lblOrderStatus.Text        = sh.OrderStatus;
            this.lblOrderDate.Text          = sh.OrderDate;
            this.lblSalesOrderNumber.Text   = sh.SalesOrderNo;
            this.lblChannelName.Text        = sh.ChannelName;
            this.lblZendeskTicket.Text      = "1234";
            this.lblZendeskTicketNo.Text    = "1";

            if (sh.ShipmentHeaderObject.Count != 0)
            {
                this.lblShipmentDate.Text = sh.ShipmentHeaderObject[0].ShippingDate;
                this.lblShipments.Text = sh.ShipmentHeaderObject.Count.ToString();
                this.lblShipMethod.Text = sh.ShipmentHeaderObject[0].ShippingAgentService;

                this.gdvOrderView.DataSource = sh.ShipmentHeaderObject[0].ShipmentLines;
                this.gdvOrderView.DataBind();
            }

            if (sh.PostedPackageObject.Count != 0)
            {
                this.lblPackages.Text = sh.PostedPackageObject.Count.ToString();
                this.lblTrackingNo.Text = sh.PostedPackageObject[0].TrackingNo;
            }

            this.lblExternalDocumentNo.Text = sh.ExternalDocumentNo;

            if (sh.WarrantyProp != null)
            {
                this.tcSetPolicy.Text = sh.WarrantyProp.Policy;
                this.tcSetStatus.Text = sh.WarrantyProp.Status;
                this.tcSetDays.Text = sh.WarrantyProp.DaysRemaining;

                if (sh.WarrantyProp.Status.ToUpper() == "OPEN")
                {
                    this.tcSetStatus.Attributes.Add("bgcolor", "LawnGreen");
                }

                if (sh.WarrantyProp.Status.ToUpper() == "CLOSED")
                {
                    this.tcSetStatus.Attributes.Add("bgcolor", "Crimson");
                }
            }
        }
    }
}