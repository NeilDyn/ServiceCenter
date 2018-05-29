using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;

namespace ExcelDesign.Forms
{
    public partial class ServiceCenter : System.Web.UI.Page
    {
        protected CallService cs;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearchBox.Text;
            cs = new CallService();

            if (searchValue != null && !string.IsNullOrWhiteSpace(searchValue))
            {
                try
                {
                    cs.callService(searchValue);
                    PopulateCustomerDetails();
                    PopulateOrderDetails();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                }
            }
        }

        protected void PopulateOrderDetails()
        {
            List<SalesHeader> sh = cs.GetSalesOrders();

            Control salesOrderDetail = LoadControl("SaledOrderDetail.ascx");

            // ((SalesOrderDetail)salesOrderDetail).PopulateControl(sh, sl);


            //lblOrderStatus.Text = sh.OrderStatus;
            //lblOrderDate.Text = sh.OrderDate;
            //lblSalesOrderNumber.Text = sh.SalesOrderNo;
            //lblChannelName.Text = sh.ChannelName;
            //lblZendeskTicket.Text = "1234";
            //lblZendeskTicketNo.Text = "1";
            //lblShipmentDate.Text = sh.ShipmentHeaderObject.ShippingDate;
            //lblShipments.Text = "1";
            //lblPackages.Text = "1";
            //lblShipMethod.Text = sh.ShipmentHeaderObject.ShippingAgentService;
            //lblTrackingNo.Text = sh.PostedPackageObject.TrackingNo;

            //lblExternalDocumentNo.Text = sh.ExternalDocumentNo;

        }

        protected void PopulateCustomerDetails()
        {
            Customer c = cs.GetCustomerInfo();

            //this.lblName.Text = c.Name;
            //this.lblAddress1.Text = c.Address1;
            //this.lblAddress2.Text = c.Address2;
            //this.lblShipToContact.Text = c.ShipToContact;
            //this.lblCity.Text = c.City;
            //this.lblZip.Text = c.Zip;
            //this.lblState.Text = c.State;
            //this.lblCountry.Text = c.Country;
        }
    }
}