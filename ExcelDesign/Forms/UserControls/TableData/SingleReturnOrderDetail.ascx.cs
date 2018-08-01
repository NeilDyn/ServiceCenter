using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.Enums;
using ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines;

namespace ExcelDesign.Forms.UserControls.TableData
{
    public partial class SingleReturnOrderDetail : System.Web.UI.UserControl
    {
        public ReturnHeader Rh { get; set; }
        public int CountID { get; set; }
        public int CustID { get; set; }
        public int CustomerCount { get; set; }
        public string RMANo { get; set; }
        public string DocNo { get; set; }

        public string CanExchange { get; set; }
        public string CanReturn { get; set; }
        public string CanIssueLabel { get; set; }
        public string UPSLabelCreated { get; set; }

        protected TableRow buttonRow = new TableRow();

        protected TableCell createExchangeCell = new TableCell();
        protected TableCell issueRefundCell = new TableCell();
        protected TableCell printRMAInstructions = new TableCell();
        protected TableCell updateRMA = new TableCell();
        protected TableCell issueReturnLabel = new TableCell();

        protected TableCell receiptCell;
        protected TableCell receiveCell;

        protected Button btnCreateExchange = new Button();
        protected Button btnIssueRefund = new Button();
        protected Button btnPrintRMAInstructions = new Button();
        protected Button btnUpdateRMA = new Button();
        protected Button btnIssueReturnLabel = new Button();

        protected Control singleReturnOrderReceiptLines;
        protected Control singleReturnOrderReceiveLines;

        protected const string singleReturnOrderReceiptLinesPath = "DataLines/ReturnOrderLines/SingleReturnOrderReceipts.ascx";
        protected const string singleReturnOrderReceiveLinesPath = "DataLines/ReturnOrderLines/SingleReturnOrderPackages.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CustomerCount == 1)
            {
                CreateButtons();
            }

            LoadData();
            PopulateLines();

            UPSLabelCreated = tcUPSReturnLabelCreated.Text.ToUpper() == "YES" ? "true" : "false";

            User activeUser = (User)Session["ActiveUser"];

            if (activeUser.Admin)
            {
                CanExchange = "true";
                CanReturn = "true";
                CanIssueLabel = "true";
            }
            else if (activeUser.Developer)
            {
                CanExchange = "true";
                CanReturn = "true";
                CanIssueLabel = "true";
            }
            else
            {
                CanReturn = activeUser.CreateReturnLabel ? "true" : "false";
                CanExchange = activeUser.CreateExchange ? "true" : "false";
                CanIssueLabel = activeUser.CreateReturnLabel ? "true" : "false";
            }
        }

        protected void CreateButtons()
        {
            btnCreateExchange.Text = "Create Exchange";
            btnCreateExchange.ID = "btnCreateExchange" + CustID.ToString() + "_" + CountID.ToString();
            btnCreateExchange.OnClientClick = "return false;";

            btnIssueRefund.Text = "Issue Refund";
            btnIssueRefund.ID = "btnIssueRefund" + CustID.ToString() + "_" + CountID.ToString();
            btnIssueRefund.OnClientClick = "return false;";

            btnPrintRMAInstructions.Text = "Print RMA Instructions";
            btnPrintRMAInstructions.ID = "btnPrintRMAInstructions" + CustID.ToString() + "_" + CountID.ToString();
            btnPrintRMAInstructions.OnClientClick = "return false;";

            btnUpdateRMA.Text = "Update RMA";
            btnUpdateRMA.ID = "btnUpdateRMA" + CustID.ToString() + "_" + CountID.ToString();
            btnUpdateRMA.OnClientClick = "return false;";

            btnIssueReturnLabel.Text = "Issue Return Label";
            btnIssueReturnLabel.ID = "btnIssueReturnLabel" + CustID.ToString() + "_" + CountID.ToString();
            btnIssueReturnLabel.OnClientClick = "return false;";
        }

        protected void LoadData()
        {
            this.tcReturnStatus.Text = Rh.ReturnStatus;
            this.tcDateCreated.Text = Rh.DateCreated;
            this.tcChannelName.Text = Rh.ChannelName;
            this.tcOrderDate.Text = Rh.OrderDate;
            this.tcEmail.Text = Rh.Email;

            this.tcReturnStatus.ToolTip = Rh.ReturnStatus;
            this.tcDateCreated.ToolTip = Rh.DateCreated;
            this.tcChannelName.ToolTip = Rh.ChannelName;
            this.tcOrderDate.ToolTip = Rh.OrderDate;
            this.tcEmail.ToolTip = Rh.Email;

            if (Rh.ReturnLabelCreated)
            {
                this.tcUPSReturnLabelCreated.Text = "Yes";
            }
            else
            {
                this.tcUPSReturnLabelCreated.Text = "No";
            }

            TrackingTypeEnum trackType = TrackingTypeEnum.Invalid;

            RMANo = Rh.RMANo;
            DocNo = Rh.ExternalDocumentNo;

            if (Rh.PostedReceiveObj.Count > 0)
            {
                PopulateReceiveLines();
            }

            this.tcPackagesCount.Text = "<a href='javascript:expandReceives" + CustID.ToString() + "" + CountID.ToString() + "()'>" + Rh.PostedReceiveObj.Count.ToString() + "</a>";

            if (Rh.ReceiptHeaderObj.Count > 0)
            {
                if (!Rh.ReceiptHeaderObj[0].PopulatedFromShipmentHeader)
                {
                    Enum.TryParse(Rh.ReceiptHeaderObj[0].ShippingAgentCode, out trackType);

                    this.tcReceiptDate.Text = Rh.ReceiptHeaderObj[0].ReceiptDate;
                    this.tcReceiptDate.ToolTip = Rh.ReceiptHeaderObj[0].ReceiptDate;
                    this.tcReceiptsTotal.Text = "<a href='javascript:expandReceipts" + CustID.ToString() + "" + CountID.ToString() + "()'>" + Rh.ReceiptHeaderObj.Count.ToString() + "</a>";
                    PopulateReceiptLines();
                }
                else
                {
                    this.tcReceiptsTotal.Text = "<a href='javascript:expandReceipts" + CustID.ToString() + "" + CountID.ToString() + "()'>0</a>";
                }
            }

            string trackNo = Rh.ReturnTrackingNo;
            this.tcReturnTrackingNo.Text = SetTrackingNo(trackType, trackNo);
            this.tcReturnTrackingNo.ToolTip = trackNo;
        }

        protected void PopulateLines()
        {
            double total = 0;
            TableRow totalRow = new TableRow();
            TableCell totalString = new TableCell();
            TableCell totalCell = new TableCell();

            int lineCount = 0;

            foreach (ReceiptHeader header in Rh.ReceiptHeaderObj)
            {
                foreach (ReceiptLine line in header.ReceiptLines)
                {
                    if (line.Quantity > 0)
                    {
                        lineCount++;

                        TableRow lineRow = new TableRow();
                        string itemNoS = string.Empty;
                        string firstSerialNo = string.Empty;
                        int receiveSerialCount = 0;
                        List<string> moreLines = new List<string>();

                        TableCell itemNo = new TableCell();
                        TableCell desc = new TableCell();
                        TableCell qty = new TableCell();
                        TableCell qtyReceived = new TableCell();
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
                        qtyReceived.Text = line.QuantityReceived.ToString();
                        total += line.LineAmount;
                        price.Text = "$      " + line.Price.ToGBString();
                        lineAmount.Text = "$      " + line.LineAmount.ToGBString();

                        qty.HorizontalAlign = HorizontalAlign.Center;
                        qtyReceived.HorizontalAlign = HorizontalAlign.Center;
                        price.HorizontalAlign = HorizontalAlign.Right;
                        lineAmount.HorizontalAlign = HorizontalAlign.Right;

                        foreach (PostedReceive receive in Rh.PostedReceiveObj)
                        {
                            foreach (PostedReceiveLine receiveLine in receive.PostedReceiveLines)
                            {
                                if (receiveLine.ItemNo == itemNoS && receive.PostedSourceID == header.No)
                                {
                                    receiveSerialCount++;

                                    if (receiveSerialCount == 1)
                                    {
                                        firstSerialNo = receiveLine.SerialNo;
                                    }
                                    else
                                    {
                                        moreLines.Add(receiveLine.SerialNo);
                                    }
                                }
                            }
                        }

                        serialNo.Text = firstSerialNo;
                        serialNo.ToolTip = firstSerialNo;
                        serialNo.HorizontalAlign = HorizontalAlign.Center;

                        lineRow.ID = "returnInfoLine_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString();

                        if (receiveSerialCount > 1)
                        {
                            moreSerial.Text = "<a id='expandMoreClickReturnLine_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString() + "' href ='javascript:expandMoreReturnLines" + CustID.ToString() + CountID.ToString() + "(" + lineCount + ")'>Show More</a>";
                            moreSerial.ID = "expandShowMoreReturnLine_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString();
                        }

                        lineRow.Cells.Add(itemNo);
                        lineRow.Cells.Add(desc);
                        lineRow.Cells.Add(qty);
                        lineRow.Cells.Add(qtyReceived);
                        lineRow.Cells.Add(price);
                        lineRow.Cells.Add(lineAmount);
                        lineRow.Cells.Add(serialNo);
                        lineRow.Cells.Add(moreSerial);

                        if (lineCount % 2 == 0)
                        {
                            lineRow.BackColor = Color.White;
                        }
                        else
                        {
                            lineRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                        }

                        lineRow.Attributes.CssStyle.Add("border-collapse", "collapse");
                        this.tblReturnDetailLines.Rows.Add(lineRow);

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
                            moreTableRow.ID = "showMoreReturnLines_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString();
                            this.tblReturnDetailLines.Rows.Add(moreTableRow);
                        }

                        if (moreLines.Count > 0)
                        {
                            TableRow blankRow = new TableRow();
                            TableCell blankCell = new TableCell
                            {
                                Text = "<br />"
                            };

                            blankRow.Cells.Add(blankCell);
                            blankRow.ID = "showMoreReturnLines_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString() + "_" + moreLineCount.ToString();
                            this.tblReturnDetailLines.Rows.Add(blankRow);
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

            this.tblReturnDetailLines.Rows.Add(totalRow);

            TableCell breakCell = new TableCell();
            TableRow breakRow = new TableRow();
            breakCell.Text = "<br/>";
            breakRow.Cells.Add(breakCell);
            this.tblReturnDetailLines.Rows.Add(breakRow);

            if (CustomerCount == 1)
            {
                createExchangeCell.Controls.Add(btnCreateExchange);
                issueRefundCell.Controls.Add(btnIssueRefund);
                updateRMA.Controls.Add(btnUpdateRMA);
                printRMAInstructions.Controls.Add(btnPrintRMAInstructions);
                issueReturnLabel.Controls.Add(btnIssueReturnLabel);

                buttonRow.Cells.Add(new TableCell());
                buttonRow.Cells.Add(new TableCell());
                buttonRow.Cells.Add(new TableCell());
                buttonRow.Cells.Add(updateRMA);
                buttonRow.Cells.Add(createExchangeCell);
                buttonRow.Cells.Add(issueRefundCell);
                buttonRow.Cells.Add(issueReturnLabel);
                buttonRow.Cells.Add(printRMAInstructions);

                this.tblReturnDetailLines.Rows.Add(buttonRow);
            }
        }

        protected void PopulateReceiveLines()
        {
            receiveCell = new TableCell();
            singleReturnOrderReceiveLines = LoadControl(singleReturnOrderReceiveLinesPath);
            ((SingleReturnOrderPackages)singleReturnOrderReceiveLines).PostedReceive = Rh.PostedReceiveObj;
            ((SingleReturnOrderPackages)singleReturnOrderReceiveLines).PopulateData();

            receiveCell.Controls.Add(singleReturnOrderReceiveLines);
            receiveCell.ColumnSpan = 8;
            receiveCell.Height = new Unit("100%");
            receiveCell.Width = new Unit("100%");
            this.expandReceives.Cells.Add(receiveCell);
            this.expandReceives.ID = "expandReceives_" + CustID.ToString() + "_" + CountID.ToString();
        }

        protected void PopulateReceiptLines()
        {
            receiptCell = new TableCell();
            singleReturnOrderReceiptLines = LoadControl(singleReturnOrderReceiptLinesPath);
            ((SingleReturnOrderReceipts)singleReturnOrderReceiptLines).ReceiptHeaders = Rh.ReceiptHeaderObj;
            ((SingleReturnOrderReceipts)singleReturnOrderReceiptLines).PopulateData();

            receiptCell.Controls.Add(singleReturnOrderReceiptLines);
            receiptCell.ColumnSpan = 8;
            receiptCell.Height = new Unit("100%");
            receiptCell.Width = new Unit("100%");
            this.expandReceipts.Cells.Add(receiptCell);
            this.expandReceipts.ID = "expandReceipts_" + CustID.ToString() + "_" + CountID.ToString();
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
                    textString = "<a href='https://www.packagemapping.com/track/auto/" + trackNo + "' target = '_blank'>" + trackNo + "</a >";
                    break;

                default:
                    textString = "<a href='https://www.packagemapping.com/track/auto/" + trackNo + "' target = '_blank'>" + trackNo + "</a >";
                    break;
            }

            return textString;
        }
    }
}