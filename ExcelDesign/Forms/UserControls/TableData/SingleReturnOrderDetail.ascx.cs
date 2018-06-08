using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;

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

        protected Button btnCreateExchange = new Button();
        protected Button btnIssueRefund = new Button();

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
                this.tcReceiptDate.Text = Rh.ReceiptHeaderObj[0].ReceiptDate;
            }

            foreach (ReceiptHeader receiptHeader in Rh.ReceiptHeaderObj)
            {
                foreach (ReceiptLine receiptLine in receiptHeader.ReceiptLines)
                {
                    totalReceipts += receiptLine.Quantity;
                }
            }

            this.tcReceiptsTotal.Text = totalReceipts.ToString();
            this.tcPackagesCount.Text = Rh.PostedReceiveObj.Count.ToString();      
        }

        private void PopulateLines()
        {
            double total = 0;
            TableRow totalRow = new TableRow();
            TableCell totalString = new TableCell();
            TableCell totalCell = new TableCell();

            foreach (ReceiptHeader header in Rh.ReceiptHeaderObj)
            {
                foreach (ReceiptLine line in header.ReceiptLines)
                {
                    TableRow lineRow = new TableRow();
                    string itemNoS = string.Empty;
                    string firstSerialNo = string.Empty;
                    int receiveSerialCount = 0;

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
                            }
                        }
                    }

                    serialNo.Text = firstSerialNo;

                    if (receiveSerialCount > 1)
                    {
                        moreSerial.Text = "Show More";
                        moreSerial.Font.Underline = true;
                        moreSerial.Style.Value = "text-decoration-color: blue;";
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
    }
}