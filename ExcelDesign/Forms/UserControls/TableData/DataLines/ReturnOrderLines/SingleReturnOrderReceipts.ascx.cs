using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;

namespace ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines
{
    public partial class SingleReturnOrderReceipts : System.Web.UI.UserControl
    {
        public List<ReceiptHeader> ReceiptHeaders { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateData()
        {
            int receiptCount = 0;

            TableRow breakRow = new TableRow();
            TableCell breakCell = new TableCell
            {
                Text = "<br />"
            };

            breakRow.Cells.Add(breakCell);

            foreach (ReceiptHeader rh in ReceiptHeaders)
            {
                receiptCount++;

                TableRow receiptHeaderRow = new TableRow();
                TableCell receiptHeader = new TableCell
                {
                    Text = "Receipt " + receiptCount.ToString(),
                };

                receiptHeader.Font.Underline = true;
                receiptHeader.Font.Bold = true;

                receiptHeaderRow.Cells.Add(receiptHeader);
                this.tblReceiptLines.Rows.Add(receiptHeaderRow);

                int lineCount = 0;

                foreach (ReceiptLine rl in rh.ReceiptLines)
                {
                    if (rl.Quantity > 0)
                    {
                        lineCount++;

                        TableRow tr = new TableRow();

                        TableCell blankCell = new TableCell();
                        TableCell receiptNo = new TableCell();
                        TableCell receiptDate = new TableCell();
                        TableCell item = new TableCell();
                        TableCell desc = new TableCell();
                        TableCell qty = new TableCell();
                        TableCell shipMethod = new TableCell();

                        receiptNo.Text = rh.No;
                        receiptDate.Text = rh.ReceiptDate;
                        item.Text = rl.ItemNo;
                        desc.Text = rl.Description;
                        qty.Text = rl.Quantity.ToString();
                        shipMethod.Text = rh.ShippingAgentCode;

                        qty.HorizontalAlign = HorizontalAlign.Center;

                        tr.Cells.Add(blankCell);
                        tr.Cells.Add(receiptNo);
                        tr.Cells.Add(receiptDate);
                        tr.Cells.Add(item);
                        tr.Cells.Add(desc);
                        tr.Cells.Add(qty);
                        tr.Cells.Add(shipMethod);

                        if (lineCount % 2 == 0)
                        {
                            tr.BackColor = Color.White;
                        }
                        else
                        {
                            tr.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                        }

                        this.tblReceiptLines.Rows.Add(tr);
                    }
                }
            }

            this.tblReceiptLines.Rows.Add(breakRow);
        }
    }
}