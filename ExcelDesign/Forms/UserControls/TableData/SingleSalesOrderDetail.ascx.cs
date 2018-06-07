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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PopulateDetail();

            if (Session["SingleSalesOrderDetailTable_" + CustID.ToString()] == null)
            {
                Session["SingleSalesOrderDetailTable_" + CustID.ToString()] = this.tblSingleSalesOrderDetail;
            }

            PopulateLines(Sh);
            CreateButtons(CountID);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void PopulateDetail()
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
                shipmentMethod = Sh.ShipmentHeaderObject[0].ShippingAgentService;
                shipmentMethod += " " + Sh.ShipmentHeaderObject[0].ShippingAgentCode;
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

        public void CreateButtons(int countID)
        {           
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
            this.tblOrderDetailLines.Caption = "ISSUE REFUND CLICKED!";
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
    }
}