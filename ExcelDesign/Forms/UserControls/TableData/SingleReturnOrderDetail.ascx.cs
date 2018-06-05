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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateDetail(ReturnHeader rh)
        {
            int totalReceipts = 0;

            this.tcReturnStatus.Text = rh.ReturnStatus;
            this.tcDateCreated.Text = rh.DateCreated;
            this.tcChannelName.Text = rh.ChannelName;          
            this.tcReturnTrackingNo.Text = rh.ReturnTrackingNo;
            this.tcOrderDate.Text = rh.OrderDate;

            if(rh.PostedReceiveObj.Count > 0)
            {
                this.tcReceiptDate.Text = rh.ReceiptHeaderObj[0].ReceiptDate;
            }

            foreach (ReceiptHeader receiptHeader in rh.ReceiptHeaderObj)
            {
                foreach (ReceiptLine receiptLine in receiptHeader.ReceiptLines)
                {
                    totalReceipts += receiptLine.Quantity;
                }
            }

            this.tcReceiptsTotal.Text = totalReceipts.ToString();
            this.tcPackagesCount.Text = rh.PostedReceiveObj.Count.ToString();

            PopulateLines(rh);
        }

        private void PopulateLines(ReturnHeader rh)
        {
            double total = 0;
            TableRow totalRow = new TableRow();
            TableCell totalString = new TableCell();
            TableCell totalCell = new TableCell();

            foreach (ReceiptHeader header in rh.ReceiptHeaderObj)
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

                    foreach (PostedReceive receive in rh.PostedReceiveObj)
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
        }
    }
}