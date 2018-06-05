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
        protected SalesHeader detailHeader;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateDetail(SalesHeader sh, int countID)
        {
            int totalShipments = 0;
            int totalTrackingNos = 0;
            string shipmentMethod = string.Empty;

            this.tcOrderStatus.Text = sh.OrderStatus;
            this.tcOrderDate.Text = sh.OrderDate;
            this.tcSalesOrderNo.Text = sh.SalesOrderNo;
            this.tcChannelName.Text = sh.ChannelName;

            if (sh.ShipmentHeaderObject.Count > 0)
            {
                this.tcShipmentDate.Text = sh.ShipmentHeaderObject[0].ShippingDate;
                shipmentMethod = sh.ShipmentHeaderObject[0].ShippingAgentService;
                shipmentMethod += " " + sh.ShipmentHeaderObject[0].ShippingAgentCode;
            }

            foreach (ShipmentHeader shipmentHeader in sh.ShipmentHeaderObject)
            {
                foreach (ShipmentLine shipmentLine in shipmentHeader.ShipmentLines)
                {
                    totalShipments += shipmentLine.Quantity;
                }
            }

            this.tcShipmentsTotal.Text = totalShipments.ToString();
            this.tcPackagesCount.Text = sh.PostedPackageObject.Count.ToString();
            

            totalTrackingNos = sh.PostedPackageObject.Count;

            if (sh.PostedPackageObject.Count > 0)
            {
                if (totalTrackingNos != 1)
                {
                    this.tcTrackingNo.Text = "Multiple";
                }
                else
                {
                    this.tcTrackingNo.Text = sh.PostedPackageObject[0].TrackingNo;
                }
            }

            this.tcStatus.Text = sh.WarrantyProp.Status;
            this.tcPolicy.Text = sh.WarrantyProp.Policy;
            this.tcDaysRemaining.Text = sh.WarrantyProp.DaysRemaining;

            PopulateLines(sh);
            CreateButtons(countID);
        }

        public void CreateButtons(int countID)
        {
            TableRow buttonRow = new TableRow();
            TableCell cancelOrderCell = new TableCell();
            TableCell partRequestCell = new TableCell();
            TableCell createReturnCell = new TableCell();
            TableCell issueRefundcell = new TableCell();

            Button btnCancelOrder = new Button();
            Button btnPartRequest = new Button();
            Button btnCreateReturn = new Button();
            Button btnIssueRefund = new Button();

            btnCancelOrder.Text = "Cancel Order";
            btnCancelOrder.ID = "btnCancelOrder_" + countID.ToString();
            btnCancelOrder.Click += new EventHandler(CancelOrder);

            btnPartRequest.Text = "Part Request";
            btnPartRequest.ID= "btnPartRequest_" + countID.ToString();
            btnPartRequest.Click += new EventHandler(PartRequest);

            btnCreateReturn.Text = "Create Return";
            btnCreateReturn.ID = "btnCreateReturn_" + countID.ToString();
            btnCreateReturn.Click += new EventHandler(CreateReturn);

            btnIssueRefund.Text = "Issue Refund";
            btnIssueRefund.ID = "btnIssueRefund_" + countID.ToString();
            btnIssueRefund.Click += new EventHandler(IssueRefund);

            cancelOrderCell.Controls.Add(btnCancelOrder);
            partRequestCell.Controls.Add(btnPartRequest);
            createReturnCell.Controls.Add(btnCreateReturn);
            issueRefundcell.Controls.Add(btnIssueRefund);

            buttonRow.Cells.Add(new TableCell());
            buttonRow.Cells.Add(new TableCell());
            buttonRow.Cells.Add(new TableCell());
            buttonRow.Cells.Add(cancelOrderCell);
            buttonRow.Cells.Add(partRequestCell);
            buttonRow.Cells.Add(createReturnCell);
            buttonRow.Cells.Add(issueRefundcell);

            TableCell breakCell = new TableCell();
            TableRow breakRow = new TableRow();
            breakCell.Text = "<br/>";
            breakRow.Cells.Add(breakCell);
            this.tblOrderDetailLines.Rows.Add(breakRow);
            this.tblOrderDetailLines.Rows.Add(buttonRow);
        }

        protected void CancelOrder(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + btn.ID.ToString() + "': CANCEL THIS ORDER!);", true);
        }

        protected void PartRequest(object sender, EventArgs e)
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert(PARTIAL THIS ORDER!);", true);
        }

        protected void CreateReturn(object sender, EventArgs e)
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert(CREATE A RETURN FOR THIS ORDER!);", true);
        }

        protected void IssueRefund(object sender, EventArgs e)
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert(ISSUE A REFUND FOR THIS ORDER!);", true);
        }

        private void PopulateLines(SalesHeader sh)
        {
            double total = 0;
            TableRow totalRow = new TableRow();
            TableCell totalString = new TableCell();
            TableCell totalCell = new TableCell();

            foreach (ShipmentHeader header in sh.ShipmentHeaderObject)
            {
                foreach (ShipmentLine line in header.ShipmentLines)
                {
                    TableRow lineRow = new TableRow();
                    string itemNoS = string.Empty;
                    string firstSerialNo = string.Empty;
                    int packageSerialCount = 0;

                    TableCell itemNo = new TableCell();
                    TableCell desc = new TableCell();
                    TableCell qty = new TableCell();
                    TableCell qtyShipped = new TableCell();
                    TableCell price = new TableCell();
                    TableCell lineAmount = new TableCell();
                    TableCell serialNo = new TableCell();
                    TableCell moreSerial = new TableCell();

                    itemNoS = line.ItemNo;
                    itemNo.Text = line.ItemNo;
                    desc.Text = line.Description;
                    qty.Text = line.Quantity.ToString();
                    qtyShipped.Text = line.QuantityShipped.ToString();
                    total += line.LineAmount;
                    price.Text = "$      " + line.Price.ToString();
                    lineAmount.Text = "$      " + line.LineAmount.ToString();

                    foreach (PostedPackage package in sh.PostedPackageObject)
                    {
                        foreach (PostedPackageLine packageLine in package.PostedPackageLines)
                        {
                            if(packageLine.ItemNo == itemNoS)
                            {
                                packageSerialCount++;

                                if(packageSerialCount <= 1)
                                {
                                    firstSerialNo = packageLine.SerialNo;
                                }
                            }
                        }
                    }

                    serialNo.Text = firstSerialNo;

                    if(packageSerialCount > 1)
                    {
                        moreSerial.Text = "Show More";
                        moreSerial.Font.Underline = true;
                        moreSerial.Style.Value = "text-decoration-color: blue;";
                    }

                    lineRow.Cells.Add(itemNo);
                    lineRow.Cells.Add(desc);
                    lineRow.Cells.Add(qty);
                    lineRow.Cells.Add(qtyShipped);
                    lineRow.Cells.Add(price);
                    lineRow.Cells.Add(lineAmount);
                    lineRow.Cells.Add(serialNo);
                    lineRow.Cells.Add(moreSerial);

                    this.tblOrderDetailLines.Rows.Add(lineRow);
                }
            }

            totalString.Text = "Total:";
            totalString.Font.Bold = true;

            totalCell.Text = "$      " + total.ToString();
            totalCell.Font.Bold = true;

            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(totalString);
            totalRow.Cells.Add(totalCell);
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());

            this.tblOrderDetailLines.Rows.Add(totalRow);
        }
    }
}