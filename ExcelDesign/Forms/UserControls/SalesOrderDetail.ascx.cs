using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;

namespace ExcelDesign.Forms
{
    public partial class SalesOrderDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        public void PopulateControl(SalesHeader sh)
        {
            lblOrderStatus.Text = sh.OrderStatus;
            lblOrderDate.Text = sh.OrderDate;
            lblSalesOrderNumber.Text = sh.SalesOrderNo;
            lblChannelName.Text = sh.ChannelName;
            lblZendeskTicket.Text = "1234";
            lblZendeskTicketNo.Text = "1";
            lblShipmentDate.Text = sh.ShipmentHeaderObject[0].ShippingDate;
            lblShipments.Text = sh.ShipmentHeaderObject.Count.ToString();
            lblPackages.Text = sh.PostedPackageObject.Count.ToString();
            lblShipMethod.Text = sh.ShipmentHeaderObject[0].ShippingAgentService;
            lblTrackingNo.Text = sh.PostedPackageObject[0].TrackingNo;

            lblExternalDocumentNo.Text = sh.ExternalDocumentNo;
        }
    }
}