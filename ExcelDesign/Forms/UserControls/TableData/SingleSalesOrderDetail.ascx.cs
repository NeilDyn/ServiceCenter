using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.Enums;
using ExcelDesign.Forms.UserControls.TableData.DataLines.SalesOrderLines;

namespace ExcelDesign.Forms.UserControls.TableData
{
    public partial class SingleSalesOrderDetail : System.Web.UI.UserControl
    {
        public SalesHeader Sh { get; set; }
        public int CountID { get; set; }
        public int CustID { get; set; }
        public int CustomerCount { get; set; }
        public string OrderNo { get; set; }
        public string DocNo { get; set; }

        public string CanReturn { get; set; }
        public string CanReturnPDA { get; set; }
        public string CanCreatePartRequest { get; set; }
        public string CanCreatePDAPartRequest { get; set; }

        protected TableRow buttonRow = new TableRow();
        protected TableCell cancelOrderCell = new TableCell();
        protected TableCell partRequestCell = new TableCell();
        protected TableCell createReturnCell = new TableCell();
        protected TableCell issueRefundcell = new TableCell();

        protected Button btnCancelOrder = new Button();
        protected Button btnPartRequest = new Button();
        protected Button btnCreateReturn = new Button();
        protected Button btnIssueRefund = new Button();

        protected TableCell shipmentCell;
        protected TableCell packageCell;
        protected TableCell trackingCell;
        protected TableCell commentCell;

        protected Control singleSalesOrderShipmentLines;
        protected Control singleSalesOrderPackageLines;
        protected Control singleSalesOrderTrackingLines;
        protected Control singelSalesOrderCommentLines;

        protected const string singleSalesOrderShipmentLinesPath = "DataLines/SalesOrderLines/SingleSalesOrderShipments.ascx";
        protected const string singleSalesOrderPackageLinesPath = "DataLines/SalesOrderLines/SingleSalesOrderPackages.ascx";
        protected const string singleSalesOrderTrackingLinesPath = "DataLines/SalesOrderLines/SingleSalesOrderTrackingNos.ascx";
        protected const string singleSalesOrderCommentLinesPath = "DataLines/SalesOrderLines/SingleSalesOrderComments.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CustomerCount == 1)
            {
                CreateButtons();
            }

            PopulateDetail();
            PopulateLines();

            User activeUser = (User)Session["ActiveUser"];

            if (activeUser.Admin)
            {
                CanReturn = "true";
                CanReturnPDA = "true";
                CanCreatePartRequest = "true";
                CanCreatePDAPartRequest = "true";
            }
            else if (activeUser.Developer)
            {
                CanReturn = "true";
                CanReturnPDA = "true";
                CanCreatePartRequest = "true";
                CanCreatePDAPartRequest = "true";
            }
            else
            {
                CanReturn = activeUser.CreateRMA ? "true" : "false";
                CanReturnPDA = activeUser.CreatePDARMA ? "true" : "false";
                CanCreatePartRequest = activeUser.CreatePartRequest ? "true" : "false";
                CanCreatePDAPartRequest = activeUser.CreatePDAPartRequest ? "true" : "false";
            }
        }

        protected void PopulateDetail()
        {
            int totalTrackingNos = 0;
            string shipmentMethod = string.Empty;

            this.tcOrderStatus.Text = Sh.OrderStatus;
            this.tcOrderDate.Text = Sh.OrderDate;
            this.tcSalesOrderNo.Text = Sh.SalesOrderNo;
            this.tcChannelName.Text = Sh.ChannelName;

            this.tcOrderStatus.ToolTip = Sh.OrderStatus;
            this.tcOrderDate.ToolTip = Sh.OrderDate;
            this.tcSalesOrderNo.ToolTip = Sh.SalesOrderNo;
            this.tcChannelName.ToolTip = Sh.ChannelName;
            TrackingTypeEnum trackType = TrackingTypeEnum.Invalid;

            this.tcIsExchangeOrder.Text = Sh.IsExchangeOrder ? "Yes" : "No";
            this.tcIsPartRequest.Text = Sh.IsPartRequest ? "Yes" : "No";

            if (Sh.RMANo != string.Empty)
            {
                this.tcRMANoTitle.Visible = true;
                this.tcRMANo.Visible = true;
                this.tcRMANo.Text = Sh.RMANo;
            }
            else
            {
                this.tcRMANo.Visible = false;
                this.tcRMANoTitle.Visible = false;
            }  
            
            if(Sh.QuoteOrderNo != string.Empty)
            {
                this.tcOrderNoTitle.Visible = true;
                this.tcOriginalOrderNo.Visible = true;
                this.tcOriginalOrderNo.Text = Sh.QuoteOrderNo;
            }
            else
            {
                this.tcOrderNoTitle.Visible = false;
                this.tcOriginalOrderNo.Visible = false;
            }

            if(Sh.IsPartRequest)
            {
                if(Sh.SalesOrderNo.ToUpper().Contains("SQ"))
                {
                    this.tcOrderStatus.Text = "Pending Approval";
                    this.lblOrderLabelNo.Text = "Quote No:";
                }
                else
                {
                    this.tcOrderStatus.Text = "Approved";
                    this.lblOrderLabelNo.Text = "Sales Order No:";
                }
            }

            OrderNo = Sh.SalesOrderNo;
            DocNo = Sh.ExternalDocumentNo;

            if (Sh.ShipmentHeaderObject.Count > 0)
            {
                if (!Sh.ShipmentHeaderObject[0].GeneratedFromSalesHeader)
                {
                    this.tcShipmentDate.Text = Sh.ShipmentHeaderObject[0].ShippingDate;
                    this.tcShipmentDate.ToolTip = Sh.ShipmentHeaderObject[0].ShippingDate;
                    shipmentMethod = Sh.ShipmentHeaderObject[0].ShippingAgentCode;
                    shipmentMethod += " " + Sh.ShipmentHeaderObject[0].ShippingAgentService;


                    this.tcShipMethod.Text = shipmentMethod;
                    this.tcShipMethod.ToolTip = shipmentMethod;

                    Enum.TryParse(Sh.ShipmentHeaderObject[0].ShippingAgentCode, out trackType);

                    this.tcShipmentsTotal.Text = "<a href='javascript:expandShipments" + CustID.ToString() + "" + CountID.ToString() + "()'>" + Sh.ShipmentHeaderObject.Count.ToString() + "</a>";
                    this.tcShipmentsTotal.ID = "tcShipmentsTotal_" + CustID.ToString() + "_" + CountID.ToString();

                    PopulateShipmentLines();
                }
                else
                {
                    this.tcShipmentsTotal.Text = "<a href='javascript:expandShipments" + CustID.ToString() + "" + CountID.ToString() + "()'>0</a>";
                    this.tcShipmentsTotal.ID = "tcShipmentsTotal_" + CustID.ToString() + "_" + CountID.ToString();
                }
            }

            if (Sh.PostedPackageObject.Count > 0)
            {
                totalTrackingNos = Sh.PostedPackageObject.Count;
                PopulatePackageLines();

                if (totalTrackingNos != 1)
                {
                    PopulateSerialLines();
                    this.tcTrackingNo.Text = "<a href='javascript:expandSerialNos" + CustID.ToString() + "" + CountID.ToString() + "()'>" + "Multiple</a>";
                }
                else
                {
                    string trackNo = Sh.PostedPackageObject[0].TrackingNo;
                    this.tcTrackingNo.Text = SetTrackingNo(trackType, trackNo);
                    this.tcTrackingNo.ToolTip = trackNo;
                }
            }

            this.tcPackagesCount.Text = "<a href='javascript:expandPackages" + CustID.ToString() + "" + CountID.ToString() + "()'>" + Sh.PostedPackageObject.Count.ToString() + "</a>";
            this.tcPackagesCount.ID = "tcPackagesTotal" + CustID.ToString() + "_" + CountID.ToString();

            if (Sh.WarrantyProp != null)
            {
                this.tcStatus.Text = Sh.WarrantyProp.Status;

                if (Sh.WarrantyProp.Status.ToUpper() == "OPEN")
                {
                    this.tcStatus.Attributes.Add("bgcolor", "LawnGreen");
                }
                else if (Sh.WarrantyProp.Status.ToUpper() == "CLOSED")
                {
                    this.tcStatus.Attributes.Add("bgcolor", "Crimson");
                }
                else
                {
                    this.tcStatus.Attributes.Add("bgcolor", "White");
                }

                this.tcPolicy.Text = Sh.WarrantyProp.Policy;
                this.tcDaysRemaining.Text = Sh.WarrantyProp.DaysRemaining.ToString();
                this.tcWarrantyType.Text = Sh.WarrantyProp.WarrantyType;

                if(Sh.WarrantyProp.IsPDA == "YES")
                {
                    this.tcPDAStamp.Text = "PDA Replacement Warranty Program";
                    this.tcPDAStamp.BackColor = Color.LawnGreen;
                    this.tcPDAStamp.HorizontalAlign = HorizontalAlign.Center;
                    this.tcPDAStamp.Font.Bold = true;
                }
                else
                {
                    this.tcPDAStamp.Text = string.Empty;
                    this.tcPDAStamp.BackColor = Color.White;
                }
            }

            this.imgOrderComments.ID = "imgOrderComments_" + CustID.ToString() + "_" + CountID.ToString();
            lblOrderComment.ID = "lblOrderComment_" + CustID.ToString() + "_" + CountID.ToString();

            if (Sh.OrderComments.Count > 0)
            {
                lblOrderComment.Visible = true;
                imgOrderComments.Visible = true;

                PopulateCommentLines();
                imgOrderComments.OnClientClick = "return expandOrderComments" + CustID.ToString() + CountID.ToString() + "()";
            }
            else
            {
                lblOrderComment.Visible = false;
                imgOrderComments.Visible = false;
            }
        }

        protected string SetTrackingNo(TrackingTypeEnum trackType, string trackNo)
        {
            string textString = string.Empty;

            switch (trackType)
            {
                case TrackingTypeEnum.FEDEX:
                    textString = "<a href='http://www.fedex.com/Tracking?language=english&cntry_code=us&tracknumbers=" + trackNo + "' target = '_blank'>" + trackNo + "</a >";
                    break;

                case TrackingTypeEnum.UPS:
                    textString = "<a href='http://wwwapps.ups.com/WebTracking/track?track=yes&trackNums=" + trackNo + "' target = '_blank'>" + trackNo + "</a >";
                    break;

                case TrackingTypeEnum.UPSRT:
                    textString = "<a href='http://wwwapps.ups.com/WebTracking/track?track=yes&trackNums=" + trackNo + "' target = '_blank'>" + trackNo + "</a >";
                    break;

                case TrackingTypeEnum.USPOSTAL:
                    textString = "<a href='https://www.stamps.com/shipstatus/?confirmation=" + trackNo + "' target = '_blank'>" + trackNo + "</a >";
                    break;

                case TrackingTypeEnum.AMAZON:
                    textString = "<a href='https://www.packagemapping.com/track/auto/" + trackNo + "' target = '_blank'>" + trackNo + "</a >";
                    break;

                case TrackingTypeEnum.Invalid:
                    textString = trackNo;
                    break;

                default:
                    textString = trackNo;
                    break;
            }

            return textString;
        }

        protected void CreateButtons()
        {
            btnCancelOrder.Text = "Cancel Order";
            btnCancelOrder.ID = "btnCancelOrder_" + CustID.ToString() + "_" + CountID.ToString();
            btnCancelOrder.OnClientClick = "return false;";

            btnPartRequest.Text = "Part Request";
            btnPartRequest.ID = "btnPartRequest_" + CustID.ToString() + "_" + CountID.ToString();
            btnPartRequest.OnClientClick = "return false;";

            btnCreateReturn.Text = "Create Return";
            btnCreateReturn.ID = "btnCreateReturn_" + CustID.ToString() + "_" + CountID.ToString();
            btnCreateReturn.OnClientClick = "return false;";

            btnIssueRefund.Text = "Issue Refund";
            btnIssueRefund.ID = "btnIssueRefund_" + CustID.ToString() + "_" + CountID.ToString();
            btnIssueRefund.OnClientClick = "return false;";
        }

        protected void PopulateLines()
        {
            double total = 0;
            TableRow totalRow = new TableRow();

            TableCell totalString = new TableCell();
            TableCell totalCell = new TableCell();

            int lineCount = 0;

            foreach (ShipmentHeader header in Sh.ShipmentHeaderObject)
            {
                foreach (ShipmentLine line in header.ShipmentLines)
                {
                    if (line.Quantity > 0)
                    {
                        lineCount++;

                        TableRow singleRow = new TableRow();
                        string itemNoS = string.Empty;
                        string firstSerialNo = string.Empty;
                        int packageSerialCount = 0;
                        List<string> moreLines = new List<string>();

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
                        itemNo.ToolTip = line.ItemNo;
                        desc.Text = line.Description;
                        desc.ToolTip = line.Description;
                        qty.Text = line.Quantity.ToString();
                        qtyShipped.Text = line.QuantityShipped.ToString();
                        total += line.LineAmount;
                        price.Text = "$      " + line.Price.ToGBString();
                        lineAmount.Text = "$      " + line.LineAmount.ToGBString();

                        qty.HorizontalAlign = HorizontalAlign.Center;
                        qtyShipped.HorizontalAlign = HorizontalAlign.Center;
                        price.HorizontalAlign = HorizontalAlign.Right;
                        lineAmount.HorizontalAlign = HorizontalAlign.Right;

                        foreach (PostedPackage package in Sh.PostedPackageObject)
                        {
                            foreach (PostedPackageLine packageLine in package.PostedPackageLines)
                            {
                                if (packageLine.ItemNo == itemNoS && package.PostedSourceID == header.No)
                                {
                                    packageSerialCount++;

                                    if (packageSerialCount == 1)
                                    {
                                        firstSerialNo = packageLine.SerialNo;
                                    }
                                    else
                                    {
                                        moreLines.Add(packageLine.SerialNo);
                                    }
                                }
                            }
                        }

                        serialNo.Text = firstSerialNo;
                        serialNo.ToolTip = firstSerialNo;
                        serialNo.HorizontalAlign = HorizontalAlign.Center;

                        singleRow.ID = "salesInfoLine_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString();
                        if (packageSerialCount > 1)
                        {
                            moreSerial.Text = "<a id='expandMoreClickOrderLine_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString() + "' href ='javascript:expandMoreOrderLines" + CustID.ToString() + CountID.ToString() + "(" + lineCount + ")'>Show More</a>";
                            moreSerial.ID = "expandShowMoreOrderLine_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString();
                        }

                        singleRow.Cells.Add(itemNo);
                        singleRow.Cells.Add(desc);
                        singleRow.Cells.Add(qty);
                        singleRow.Cells.Add(qtyShipped);
                        singleRow.Cells.Add(price);
                        singleRow.Cells.Add(lineAmount);
                        singleRow.Cells.Add(serialNo);
                        singleRow.Cells.Add(moreSerial);

                        if (lineCount % 2 == 0)
                        {
                            singleRow.BackColor = Color.White;
                        }
                        else
                        {
                            singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                        }

                        singleRow.Attributes.CssStyle.Add("border-collapse", "collapse");

                        this.tblOrderDetailLines.Rows.Add(singleRow);

                        int moreLineCount = 0;

                        foreach (string serial in moreLines)
                        {
                            moreLineCount++;

                            TableCell moreSerialNo = new TableCell
                            {
                                Text = serial,
                                HorizontalAlign = HorizontalAlign.Center,
                                ToolTip = serial
                            };

                            TableRow moreTableRow = new TableRow();

                            moreTableRow.Cells.Add(new TableCell());
                            moreTableRow.Cells.Add(new TableCell());
                            moreTableRow.Cells.Add(new TableCell());
                            moreTableRow.Cells.Add(new TableCell());
                            moreTableRow.Cells.Add(new TableCell());
                            moreTableRow.Cells.Add(new TableCell());
                            moreTableRow.Cells.Add(moreSerialNo);
                            moreTableRow.Cells.Add(new TableCell());

                            if (lineCount % 2 == 0)
                            {
                                moreTableRow.BackColor = Color.White;
                            }
                            else
                            {
                                moreTableRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                            }

                            moreTableRow.Attributes.CssStyle.Add("border-collapse", "collapse");
                            moreTableRow.ID = "showMoreOrderLines_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString() + "_" + moreLineCount.ToString();

                            this.tblOrderDetailLines.Rows.Add(moreTableRow);
                        }

                        if (moreLines.Count > 0)
                        {
                            TableRow blankRow = new TableRow();
                            TableCell blankCell = new TableCell
                            {
                                Text = "<br />"
                            };

                            blankRow.Cells.Add(blankCell);
                            blankRow.ID = "showMoreOrderLines_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString();
                            this.tblOrderDetailLines.Rows.Add(blankRow);
                        }
                    }
                }
            }

            totalString.Text = "Total:";
            totalString.Font.Bold = true;

            totalCell.Text = "$      " + total.ToGBString();
            totalCell.Font.Bold = true;

            totalString.HorizontalAlign = HorizontalAlign.Right;
            totalCell.HorizontalAlign = HorizontalAlign.Right;

            totalString.Attributes.CssStyle.Add("border-top", "2px solid black");
            totalString.Attributes.CssStyle.Add("border-collapse", "collapse");

            totalCell.Attributes.CssStyle.Add("border-top", "2px solid black");
            totalCell.Attributes.CssStyle.Add("border-collapse", "collapse");

            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(totalString);
            totalRow.Cells.Add(totalCell);
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());

            this.tblOrderDetailLines.Rows.Add(totalRow);

            TableCell breakCell = new TableCell();
            TableRow breakRow = new TableRow();
            breakCell.Text = "<br/>";
            breakRow.Cells.Add(breakCell);
            this.tblOrderDetailLines.Rows.Add(breakRow);

            if (CustomerCount == 1)
            {
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

                this.tblOrderDetailLines.Rows.Add(buttonRow);
            }
        }

        protected void PopulateShipmentLines()
        {
            shipmentCell = new TableCell();
            singleSalesOrderShipmentLines = LoadControl(singleSalesOrderShipmentLinesPath);
            ((SingleSalesOrderShipments)singleSalesOrderShipmentLines).ShipmentHeaders = Sh.ShipmentHeaderObject;
            ((SingleSalesOrderShipments)singleSalesOrderShipmentLines).PopulateData();

            shipmentCell.Controls.Add(singleSalesOrderShipmentLines);
            shipmentCell.ColumnSpan = 8;
            shipmentCell.Height = new Unit("100%");
            shipmentCell.Width = new Unit("100%");
            this.expandShipments.Cells.Add(shipmentCell);
            this.expandShipments.ID = "expandShipments_" + CustID.ToString() + "_" + CountID.ToString();
        }

        protected void PopulatePackageLines()
        {
            packageCell = new TableCell();
            singleSalesOrderPackageLines = LoadControl(singleSalesOrderPackageLinesPath);
            ((SingleSalesOrderPackages)singleSalesOrderPackageLines).PostedPackage = Sh.PostedPackageObject;
            ((SingleSalesOrderPackages)singleSalesOrderPackageLines).PopulateData();

            packageCell.Controls.Add(singleSalesOrderPackageLines);
            packageCell.ColumnSpan = 8;
            packageCell.Height = new Unit("100%");
            packageCell.Width = new Unit("100%");
            this.expandPackages.Cells.Add(packageCell);
            this.expandPackages.ID = "expandPackages_" + CustID.ToString() + "_" + CountID.ToString();
        }

        protected void PopulateSerialLines()
        {
            trackingCell = new TableCell();
            singleSalesOrderTrackingLines = LoadControl(singleSalesOrderTrackingLinesPath);
            ((SingleSalesOrderTrackingNos)singleSalesOrderTrackingLines).PostedPackage = Sh.PostedPackageObject;
            ((SingleSalesOrderTrackingNos)singleSalesOrderTrackingLines).PopulateData();

            trackingCell.Controls.Add(singleSalesOrderTrackingLines);
            trackingCell.ColumnSpan = 8;
            trackingCell.Height = new Unit("100%");
            trackingCell.Width = new Unit("100%");
            this.expandSerialNos.Cells.Add(trackingCell);
            this.expandSerialNos.ID = "expandSerialNos_" + CustID.ToString() + "_" + CountID.ToString();
        }

        protected void PopulateCommentLines()
        {
            commentCell = new TableCell();
            singelSalesOrderCommentLines = LoadControl(singleSalesOrderCommentLinesPath);
            ((SingleSalesOrderComments)singelSalesOrderCommentLines).CommentLines = Sh.OrderComments;
            ((SingleSalesOrderComments)singelSalesOrderCommentLines).PopulateData();

            commentCell.Controls.Add(singelSalesOrderCommentLines);
            commentCell.ColumnSpan = 8;
            commentCell.Height = new Unit("100%");
            commentCell.Width = new Unit("50%");
            this.expandOrderComments.Cells.Add(commentCell);
            this.expandOrderComments.ID = "expandOrderComments_" + CustID.ToString() + "_" + CountID.ToString();
        }
    }
}