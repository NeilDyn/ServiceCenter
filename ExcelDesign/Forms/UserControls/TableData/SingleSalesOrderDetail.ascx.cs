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
        public SalesHeader Sh { get; set; }
        public int CountID { get; set; }
        public int CustID { get; set; }

        protected TableRow buttonRow = new TableRow();
        protected TableCell cancelOrderCell = new TableCell();
        protected TableCell partRequestCell = new TableCell();
        protected TableCell createReturnCell = new TableCell();
        protected TableCell issueRefundcell = new TableCell();

        protected Button btnCancelOrder = new Button();
        protected Button btnPartRequest = new Button();
        protected Button btnCreateReturn = new Button();
        protected Button btnIssueRefund = new Button();

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateButtons();
            PopulateDetail();

            if (Session["SingleSalesOrderDetailTable_" + CustID.ToString()] == null)
            {
                Session["SingleSalesOrderDetailTable_" + CustID.ToString()] = this.tblSingleSalesOrderDetail;
            }

            PopulateLines();
            FormatPage();
        }

        protected void FormatPage()
        {

        }

        protected void PopulateDetail()
        {
            int totalShipments = 0;
            int totalTrackingNos = 0;
            string shipmentMethod = string.Empty;

            this.tcOrderStatus.Text = Sh.OrderStatus;
            this.tcOrderDate.Text = Sh.OrderDate;
            this.tcSalesOrderNo.Text = Sh.SalesOrderNo;
            this.tcChannelName.Text = Sh.ChannelName;

            if (Sh.ShipmentHeaderObject.Count > 0)
            {
                this.tcShipmentDate.Text = Sh.ShipmentHeaderObject[0].ShippingDate;
                shipmentMethod = Sh.ShipmentHeaderObject[0].ShippingAgentCode;
                shipmentMethod += " " + Sh.ShipmentHeaderObject[0].ShippingAgentService;
            }

            foreach (ShipmentHeader shipmentHeader in Sh.ShipmentHeaderObject)
            {
                foreach (ShipmentLine shipmentLine in shipmentHeader.ShipmentLines)
                {
                    totalShipments += shipmentLine.Quantity;
                }
            }

            this.tcShipmentsTotal.Text = totalShipments.ToString();
            this.tcPackagesCount.Text = Sh.PostedPackageObject.Count.ToString();
            this.tcShipMethod.Text = shipmentMethod;


            totalTrackingNos = Sh.PostedPackageObject.Count;

            if (Sh.PostedPackageObject.Count > 0)
            {
                if (totalTrackingNos != 1)
                {
                    this.tcTrackingNo.Text = "Multiple";
                }
                else
                {
                    this.tcTrackingNo.Text = Sh.PostedPackageObject[0].TrackingNo;
                }
            }

            this.tcStatus.Text = Sh.WarrantyProp.Status;
            this.tcPolicy.Text = Sh.WarrantyProp.Policy;
            this.tcDaysRemaining.Text = Sh.WarrantyProp.DaysRemaining;                  
        }

        public void CreateButtons()
        {           
            btnCancelOrder.Text = "Cancel Order";
            btnCancelOrder.ID = "btnCancelOrder_" + CustID.ToString() + "_" + CountID.ToString();
            btnCancelOrder.OnClientClick = "return false;";

            btnPartRequest.Text = "Part Request";
            btnPartRequest.ID= "btnPartRequest_" + CustID.ToString() + "_" + CountID.ToString();
            btnPartRequest.OnClientClick = "return false;";

            btnCreateReturn.Text = "Create Return";
            btnCreateReturn.ID = "btnCreateReturn_" + CustID.ToString() + "_" + CountID.ToString();
            btnCreateReturn.OnClientClick = "return false;";

            btnIssueRefund.Text = "Issue Refund";
            btnIssueRefund.ID = "btnIssueRefund_" + CustID.ToString() + "_" + CountID.ToString();
            btnIssueRefund.OnClientClick = "return false;";
        }

        private void PopulateLines()
        {
            double total = 0;
            TableRow totalRow = new TableRow();
            TableCell totalString = new TableCell();
            TableCell totalCell = new TableCell();

            foreach (ShipmentHeader header in Sh.ShipmentHeaderObject)
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

                    qty.HorizontalAlign = HorizontalAlign.Center;
                    qtyShipped.HorizontalAlign = HorizontalAlign.Center;
                    price.HorizontalAlign = HorizontalAlign.Center;
                    lineAmount.HorizontalAlign = HorizontalAlign.Center;

                    foreach (PostedPackage package in Sh.PostedPackageObject)
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

            cancelOrderCell.Controls.Add(btnCancelOrder);
            partRequestCell.Controls.Add(btnPartRequest);
            createReturnCell.Controls.Add(btnCreateReturn);
            issueRefundcell.Controls.Add(btnIssueRefund);

            buttonRow.Cells.Add(new TableCell());
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
    }
}