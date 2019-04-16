using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.Enums;
using ExcelDesign.Class_Objects.FunctionData;
using ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines;

namespace ExcelDesign.Forms.UserControls.TableData
{
    /* v10 - 12 March 2019 - Neil Jansen
     * Updated logic for Issue Return Label to call new function to email and print new logic of Return Label
     * 
     * Added new button Legacy Return Label that executes the current functionality of the Issue Return Label
     * Added Cross Reference No
     */

    public partial class SingleReturnOrderDetail : System.Web.UI.UserControl
    {
        public ReturnHeader Rh { get; set; }
        public int CountID { get; set; }
        public int CustID { get; set; }
        public int CustomerCount { get; set; }
        public string RMANo { get; set; }
        public string DocNo { get; set; }

        public string CanExchange { get; set; }
        public string CanExchangePDA { get; set; }
        public string CanReturn { get; set; }
        public string CanReturnPDA { get; set; }
        public string CanIssueLabel { get; set; }
        public string CanRefund { get; set; }
        public string CanRefundPDA { get; set; }
        public string UPSLabelCreated { get; set; }

        protected TableRow buttonRow = new TableRow();

        protected TableCell createExchangeCell = new TableCell();
        protected TableCell issueRefundCell = new TableCell();
        protected TableCell printRMAInstructions = new TableCell();
        protected TableCell updateRMA = new TableCell();
        protected TableCell legacyReturnLabel = new TableCell();
        protected TableCell issueReturnLabel = new TableCell();

        protected TableCell receiptCell;
        protected TableCell receiveCell;
        protected TableCell commentCell;
        protected TableCell multipleExchangeOrderCell;
        protected TableCell zendeskReturnCell;

        protected Button btnCreateExchange = new Button();
        protected Button btnIssueRefund = new Button();
        protected Button btnPrintRMAInstructions = new Button();
        protected Button btnUpdateRMA = new Button();
        protected Button btnLegacyReturnLabel = new Button();
        protected Button btnReturnLabel = new Button();

        protected Control singleReturnOrderReceiptLines;
        protected Control singleReturnOrderReceiveLines;
        protected Control singelReturnOrderCommentLines;
        protected Control singelReturnOrderMultipleExchangeOrderLines;
        protected Control singelReturnOrderZendeskLines;

        protected const string singleReturnOrderReceiptLinesPath = "DataLines/ReturnOrderLines/SingleReturnOrderReceipts.ascx";
        protected const string singleReturnOrderReceiveLinesPath = "DataLines/ReturnOrderLines/SingleReturnOrderPackages.ascx";
        protected const string singleReturnOrderCommentLinesPath = "DataLines/ReturnOrderLines/SingleReturnOrderComments.ascx";
        protected const string singleReturnOrderMultipleExchangeOrderPath = "DataLines/ReturnOrderLines/SingleReturnOrderExchangeNos.ascx";
        protected const string singleReturnOrderZendeskLinesPath = "DataLines/ReturnOrderLines/SingleReturnOrderZendeskTickets.ascx";

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
                CanReturnPDA = "true";
                CanExchangePDA = "true";
                CanRefund = "true";
                CanRefundPDA = "true";
            }
            else if (activeUser.Developer)
            {
                CanExchange = "true";
                CanReturn = "true";
                CanIssueLabel = "true";
                CanReturnPDA = "true";
                CanExchangePDA = "true";
                CanRefund = "true";
                CanRefundPDA = "true";
            }
            else
            {
                CanReturn = activeUser.CreateRMA ? "true" : "false";
                CanExchange = activeUser.CreateExchange ? "true" : "false";
                CanIssueLabel = activeUser.CreateReturnLabel ? "true" : "false";
                CanReturnPDA = activeUser.CreatePDARMA ? "true" : "false";
                CanExchangePDA = activeUser.CreatePDAExchange ? "true" : "false";
                CanRefund = activeUser.CanIssueRefund ? "true" : "false";
                CanRefundPDA = activeUser.CanIssuePDARefund ? "true" : "false";
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
            btnUpdateRMA.ID = "BtnUpdateRMA" + CustID.ToString() + "_" + CountID.ToString();
            btnUpdateRMA.OnClientClick = "return false;";

            btnLegacyReturnLabel.Text = "Legacy Return Label";
            btnLegacyReturnLabel.ID = "btnLegacyReturnLabel" + CustID.ToString() + "_" + CountID.ToString();
            btnLegacyReturnLabel.OnClientClick = "return false;";

            btnReturnLabel.Text = "Return Label";
            btnReturnLabel.ID = "btnReturnLabel" + CustID.ToString() + "_" + CountID.ToString();
            btnReturnLabel.OnClientClick = "return false;";
        }

        protected void LoadData()
        {
            this.tcReturnStatus.Text = Rh.ReturnStatus;
            this.tcDateCreated.Text = Rh.DateCreated;
            this.tcChannelName.Text = Rh.ChannelName;
            this.tcOrderDate.Text = Rh.OrderDate;
            this.tcEmail.Text = Rh.Email;
            this.tcIMEINo.Text = Rh.IMEINo;

            this.tcReturnStatus.ToolTip = Rh.ReturnStatus;
            this.tcDateCreated.ToolTip = Rh.DateCreated;
            this.tcChannelName.ToolTip = Rh.ChannelName;
            this.tcOrderDate.ToolTip = Rh.OrderDate;
            this.tcEmail.ToolTip = Rh.Email;
            this.tcIMEINo.ToolTip = Rh.IMEINo;

            this.tcUPSReturnLabelCreated.Text = Rh.ReturnLabelCreated == true ? "Yes" : "No";

            int exchangeCounter = 0;
            int totalCounter = 0;

            foreach (ReceiptHeader head in Rh.ReceiptHeaderObj)
            {
                foreach (ReceiptLine line in head.ReceiptLines)
                {
                    totalCounter += line.QuantityReceived;
                    exchangeCounter += line.QuantityExchanged;
                    totalCounter -= line.QuantityRefunded;
                }
            }

            if (exchangeCounter == totalCounter && exchangeCounter != 0)
            {
                this.tcExchangeStatus.Text = "Completed";
            }
            else if (exchangeCounter == 0)
            {
                this.tcExchangeStatus.Text = "No";
            }
            else
            {
                this.tcExchangeStatus.Text = "Partial Exchanged";
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

            if (Rh.ExchangeCreated)
            {
                if (Rh.ExchangeOrderNo.Count > 1)
                {
                    this.tcExchangeOrderNo.Text = "<a href='javascript:expandMultipleExchange" + CustID.ToString() + "" + CountID.ToString() + "()'>Multiple</a>";
                    PopulateMultipleExchangeOrderNo();
                }
                else
                {
                    this.tcExchangeOrderNo.Text = Rh.ExchangeOrderNo[0];
                }
            }

            this.imgReturnComments.ID = "imgReturnComments_" + CustID.ToString() + "_" + CountID.ToString();

            lblReturnComment.ID = "lblReturnComment_" + CustID.ToString() + "_" + CountID.ToString();

            if(Rh.ReturnComments.Count > 0)
            {
                lblReturnComment.Visible = true;
                imgReturnComments.Visible = true;

                PopulateCommentLines();
                imgReturnComments.OnClientClick = "return expandReturnComments" + CustID.ToString() + CountID.ToString() + "()";
            }
            else
            {
                lblReturnComment.Visible = false;
                imgReturnComments.Visible = false;
            }

            tcZendeskTickets.Text = Rh.Tickets.Count > 0 ? "<a href='javascript:expandReturnZendeskTickets" + CustID.ToString() + "" + CountID.ToString() + "()'>" + Rh.Tickets.Count.ToString() + "</a>" : Rh.Tickets.Count.ToString();
            PopulateZendeskTicketLines();         
        }

        protected void PopulateLines()
        {
            double total = 0;
            TableRow totalRow = new TableRow();
            TableCell totalString = new TableCell();
            TableCell totalCell = new TableCell();
            List<ReturnReason> rrList = (List<ReturnReason>)Session["ReturnReasons"];

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
                        TableCell crossRefNo = new TableCell();
                        TableCell qty = new TableCell();
                        TableCell qtyReceived = new TableCell();
                        TableCell qtyExchanged = new TableCell();
                        TableCell qtyRefunded = new TableCell();
                        TableCell price = new TableCell();
                        TableCell lineAmount = new TableCell();
                        TableCell serialNo = new TableCell();
                        TableCell returnReason = new TableCell();
                        TableCell reqReturnAction = new TableCell();
                        TableCell moreSerial = new TableCell();

                        itemNoS = line.ItemNo;
                        itemNo.Text = line.ItemNo;
                        itemNo.ToolTip = line.ItemNo;
                        desc.Text = line.Description;
                        desc.ToolTip = line.Description;
                        crossRefNo.Text = line.CrossRefNo;
                        crossRefNo.ToolTip = line.CrossRefNo;
                        qty.Text = line.Quantity.ToString();
                        qtyReceived.Text = line.QuantityReceived.ToString();
                        qtyExchanged.Text = line.QuantityExchanged.ToString();
                        qtyRefunded.Text = line.QuantityRefunded.ToString();
                        total += line.LineAmount;
                        price.Text = "$      " + line.Price.ToGBString();
                        lineAmount.Text = "$      " + line.LineAmount.ToGBString();

                        foreach (ReturnReason rr in rrList)
                        {
                            if (line.ReturnReasonCode == rr.ReasonCode)
                            {
                                returnReason.Text = rr.Display;
                            }
                        }

                        reqReturnAction.Text = line.REQReturnAction;

                        qty.HorizontalAlign = HorizontalAlign.Center;
                        qtyReceived.HorizontalAlign = HorizontalAlign.Center;
                        qtyExchanged.HorizontalAlign = HorizontalAlign.Center;
                        qtyRefunded.HorizontalAlign = HorizontalAlign.Center;
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
                        lineRow.Cells.Add(crossRefNo);
                        lineRow.Cells.Add(qty);
                        lineRow.Cells.Add(qtyReceived);
                        lineRow.Cells.Add(qtyExchanged);
                        lineRow.Cells.Add(qtyRefunded);
                        lineRow.Cells.Add(price);
                        lineRow.Cells.Add(lineAmount);
                        lineRow.Cells.Add(returnReason);
                        lineRow.Cells.Add(reqReturnAction);
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
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(totalString);
            totalRow.Cells.Add(totalCell);
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());
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
                legacyReturnLabel.Controls.Add(btnLegacyReturnLabel);
                issueReturnLabel.Controls.Add(btnReturnLabel);

                buttonRow.Cells.Add(new TableCell());
                buttonRow.Cells.Add(new TableCell());
                buttonRow.Cells.Add(new TableCell());
                buttonRow.Cells.Add(new TableCell());
                buttonRow.Cells.Add(new TableCell());
                buttonRow.Cells.Add(updateRMA);
                buttonRow.Cells.Add(createExchangeCell);
                buttonRow.Cells.Add(issueRefundCell);
                buttonRow.Cells.Add(legacyReturnLabel);
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

        protected void PopulateCommentLines()
        {
            commentCell = new TableCell();
            singelReturnOrderCommentLines = LoadControl(singleReturnOrderCommentLinesPath);
            ((SingleReturnOrderComments)singelReturnOrderCommentLines).CommentLines = Rh.ReturnComments;
            ((SingleReturnOrderComments)singelReturnOrderCommentLines).PopulateData();

            commentCell.Controls.Add(singelReturnOrderCommentLines);
            commentCell.ColumnSpan = 8;
            commentCell.Height = new Unit("100%");
            commentCell.Width = new Unit("50%");
            this.expandReturnComments.Cells.Add(commentCell);
            this.expandReturnComments.ID = "expandReturnComments_" + CustID.ToString() + "_" + CountID.ToString();
        }

        protected void PopulateZendeskTicketLines()
        {
            zendeskReturnCell = new TableCell();
            singelReturnOrderZendeskLines = LoadControl(singleReturnOrderZendeskLinesPath);
            ((SingleReturnOrderZendeskTickets)singelReturnOrderZendeskLines).ZendeskTickets = Rh.Tickets;
            ((SingleReturnOrderZendeskTickets)singelReturnOrderZendeskLines).PopulateData();

            zendeskReturnCell.Controls.Add(singelReturnOrderZendeskLines);
            zendeskReturnCell.ColumnSpan = 8;
            zendeskReturnCell.Height = new Unit("100%");
            zendeskReturnCell.Width = new Unit("50%");
            this.expandReturnZendeskTickets.Cells.Add(zendeskReturnCell);
            this.expandReturnZendeskTickets.ID = "expandReturnZendeskTickets_" + CustID.ToString() + "_" + CountID.ToString();
        }

        protected void PopulateMultipleExchangeOrderNo()
        {
            List<string> exchangeOrderNos = new List<string>();

            for (int i = 0; i < Rh.ExchangeOrderNo.Count; i++)
            {
                exchangeOrderNos.Add(Rh.ExchangeOrderNo[i]);
            }

            multipleExchangeOrderCell = new TableCell();
            singelReturnOrderMultipleExchangeOrderLines = LoadControl(singleReturnOrderMultipleExchangeOrderPath);
            ((SingleReturnOrderExhangeNos)singelReturnOrderMultipleExchangeOrderLines).ExchangeOrderNos = exchangeOrderNos;
            ((SingleReturnOrderExhangeNos)singelReturnOrderMultipleExchangeOrderLines).PopulateLines();

            multipleExchangeOrderCell.Controls.Add(singelReturnOrderMultipleExchangeOrderLines);
            multipleExchangeOrderCell.ColumnSpan = 7;
            multipleExchangeOrderCell.Height = new Unit("100%");
            multipleExchangeOrderCell.Width = new Unit("50%");
            this.expandMultipleExchange.Cells.Add(new TableCell());
            this.expandMultipleExchange.Cells.Add(new TableCell());
            this.expandMultipleExchange.Cells.Add(new TableCell());
            this.expandMultipleExchange.Cells.Add(new TableCell());
            this.expandMultipleExchange.Cells.Add(new TableCell());
            this.expandMultipleExchange.Cells.Add(new TableCell());
            this.expandMultipleExchange.Cells.Add(multipleExchangeOrderCell);
            this.expandMultipleExchange.ID = "expandMultipleExchange" + CustID.ToString() + "_" + CountID.ToString();
        }

        protected void imgComments_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}