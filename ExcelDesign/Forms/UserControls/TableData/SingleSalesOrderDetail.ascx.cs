using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;

namespace ExcelDesign.Forms.UserControls.TableData
{
    public partial class SingleSalesOrderDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateDetail(SalesHeader sh)
        {
            int totalShipments = 0;
            int totalTrackingNos = 0;
            string shipmentMethod = string.Empty;

            this.tcOrderStatus.Text = sh.OrderStatus;
            this.tcOrderDate.Text = sh.OrderDate;
            this.tcSalesOrderNo.Text = sh.SalesOrderNo;
            this.tcChannelName.Text = sh.ChannelName;
            this.tcShipmentDate.Text = sh.ShipmentHeaderObject[0].ShippingDate;

            foreach (ShipmentHeader shipmentHeader in sh.ShipmentHeaderObject)
            {
                foreach(ShipmentLine shipmentLine in shipmentHeader.ShipmentLines)
                {
                    totalShipments += shipmentLine.Quantity;
                }
            }

            this.tcShipmentsTotal.Text = totalShipments.ToString();
            this.tcPackagesCount.Text = sh.PostedPackageObject.Count.ToString();
            shipmentMethod = sh.ShipmentHeaderObject[0].ShippingAgentService;
            shipmentMethod += " " + sh.ShipmentHeaderObject[0].ShippingAgentCode;

            totalTrackingNos = sh.PostedPackageObject.Count;

            if(totalTrackingNos != 1)
            {
                this.tcTrackingNo.Text = "Multiple";
            }
            else
            {
                this.tcTrackingNo.Text = sh.PostedPackageObject[0].TrackingNo;
            }

            this.tcStatus.Text = sh.WarrantyProp.Status;
            this.tcPolicy.Text = sh.WarrantyProp.Policy;
            this.tcDaysRemaining.Text = sh.WarrantyProp.DaysRemaining;
        }
    }
}