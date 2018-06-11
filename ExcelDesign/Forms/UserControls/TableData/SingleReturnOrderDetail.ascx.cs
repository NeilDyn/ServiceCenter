﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines;

namespace ExcelDesign.Forms.UserControls.TableData
{
    public partial class SingleReturnOrderDetail : System.Web.UI.UserControl
    {
        public ReturnHeader Rh { get; set; }
        public int CountID { get; set; }
        public int CustID { get; set; }

        protected TableRow buttonRow = new TableRow();

        protected TableCell createExchangeCell = new TableCell();
        protected TableCell issueRefundCell = new TableCell();

        protected TableCell receiptCell;
        protected TableCell receiveCell;

        protected Button btnCreateExchange = new Button();
        protected Button btnIssueRefund = new Button();

        protected Control singleReturnOrderReceiptLines;
        protected Control singleReturnOrderReceiveLines;

        protected const string singleReturnOrderReceiptLinesPath = "DataLines/ReturnOrderLines/SingleReturnOrderReceipts.ascx";
        protected const string singleReturnOrderReceiveLinesPath = "DataLines/ReturnOrderLines/SingleReturnOrderPackages.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateButtons();
            LoadData();
            PopulateLines();      
            FormatPage();
        }

        protected void FormatPage()
        {

        }

        protected void CreateButtons()
        {
            btnCreateExchange.Text = "Create Exchange";
            btnCreateExchange.ID = "btnCreateExchange" + CustID.ToString() + "_" + CountID.ToString();
            btnCreateExchange.OnClientClick = "return false;";

            btnIssueRefund.Text = "Issue Refund";
            btnIssueRefund.ID = "btnIssueRefund" + CustID.ToString() + "_" + CountID.ToString();
            btnIssueRefund.OnClientClick = "return false;";
        }

        protected void LoadData()
        {
            int totalReceipts = 0;

            this.tcReturnStatus.Text = Rh.ReturnStatus;
            this.tcDateCreated.Text = Rh.DateCreated;
            this.tcChannelName.Text = Rh.ChannelName;
            this.tcReturnTrackingNo.Text = Rh.ReturnTrackingNo;
            this.tcOrderDate.Text = Rh.OrderDate;

            if (Rh.PostedReceiveObj.Count > 0)
            {           
                PopulateReceiveLines();
            }

            this.tcPackagesCount.Text = "<a href='javascript:expandReceives" + CustID.ToString() + "" + CountID.ToString() + "()'>" + Rh.PostedReceiveObj.Count.ToString() + "</a>";

            if (Rh.ReceiptHeaderObj.Count > 0)
            {
                this.tcReceiptDate.Text = Rh.ReceiptHeaderObj[0].ReceiptDate;
                foreach (ReceiptHeader receiptHeader in Rh.ReceiptHeaderObj)
                {
                    foreach (ReceiptLine receiptLine in receiptHeader.ReceiptLines)
                    {
                        totalReceipts += receiptLine.Quantity;
                    }
                }
            }

            if (totalReceipts > 0)
            {
                PopulateReceiptLines();
            }

            this.tcReceiptsTotal.Text = "<a href='javascript:expandReceipts" + CustID.ToString() + "" + CountID.ToString() + "()'>" + totalReceipts.ToString() + "</a>";
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
                    desc.Text = line.Description;
                    qty.Text = line.Quantity.ToString();
                    qtyReceived.Text = line.QuantityReceived.ToString();
                    total += line.LineAmount;
                    price.Text = "$      " + line.Price.ToString();
                    lineAmount.Text = "$      " + line.LineAmount.ToString();

                    qty.HorizontalAlign = HorizontalAlign.Center;
                    qtyReceived.HorizontalAlign = HorizontalAlign.Center;
                    price.HorizontalAlign = HorizontalAlign.Center;
                    lineAmount.HorizontalAlign = HorizontalAlign.Center;

                    foreach (PostedReceive receive in Rh.PostedReceiveObj)
                    {
                        foreach (PostedReceiveLine receiveLine in receive.PostedReceiveLines)
                        {
                            if (receiveLine.ItemNo == itemNoS)
                            {
                                receiveSerialCount++;

                                if (receiveSerialCount <= 1)
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
                    serialNo.HorizontalAlign = HorizontalAlign.Center;

                    lineRow.ID = "returnInfoLine_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString();

                    if (receiveSerialCount > 1)
                    {
                        moreSerial.Text = "<a href='javascript:expandMoreReturnLines" + CustID.ToString() + CountID.ToString() + "(" + lineCount + ")'> Show More </a>";
                    }

                    lineRow.Cells.Add(itemNo);
                    lineRow.Cells.Add(desc);
                    lineRow.Cells.Add(qty);
                    lineRow.Cells.Add(qtyReceived);
                    lineRow.Cells.Add(price);
                    lineRow.Cells.Add(lineAmount);
                    lineRow.Cells.Add(serialNo);
                    lineRow.Cells.Add(moreSerial);

                    this.tblReturnDetailLines.Rows.Add(lineRow);

                    foreach (string serial in moreLines)
                    {
                        TableCell moreSerialNo = new TableCell
                        {
                            Text = serial,
                            HorizontalAlign = HorizontalAlign.Center
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

                        moreTableRow.ID = "showMoreReturnLines_" + CustID.ToString() + "_" + CountID.ToString() + "_" + lineCount.ToString();
                        this.tblReturnDetailLines.Rows.Add(moreTableRow);
                    }
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

            this.tblReturnDetailLines.Rows.Add(totalRow);

            createExchangeCell.Controls.Add(btnCreateExchange);
            issueRefundCell.Controls.Add(btnIssueRefund);

            buttonRow.Cells.Add(new TableCell());
            buttonRow.Cells.Add(new TableCell());
            buttonRow.Cells.Add(new TableCell());
            buttonRow.Cells.Add(new TableCell());
            buttonRow.Cells.Add(new TableCell());
            buttonRow.Cells.Add(new TableCell());
            buttonRow.Cells.Add(createExchangeCell);
            buttonRow.Cells.Add(issueRefundCell);

            TableCell breakCell = new TableCell();
            TableRow breakRow = new TableRow();
            breakCell.Text = "<br/>";
            breakRow.Cells.Add(breakCell);
            this.tblReturnDetailLines.Rows.Add(breakRow);
            this.tblReturnDetailLines.Rows.Add(buttonRow);
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
    }
}