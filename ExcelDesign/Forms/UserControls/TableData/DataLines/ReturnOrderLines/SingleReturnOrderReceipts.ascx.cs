using System;
using System.Collections.Generic;
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
            foreach (ReceiptHeader rh in ReceiptHeaders)
            {
                foreach (ReceiptLine rl in rh.ReceiptLines)
                {
                    TableRow tr = new TableRow();

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

                    tr.Cells.Add(receiptNo);
                    tr.Cells.Add(receiptDate);
                    tr.Cells.Add(item);
                    tr.Cells.Add(desc);
                    tr.Cells.Add(qty);
                    tr.Cells.Add(shipMethod);

                    this.tblReceiveLines.Rows.Add(tr);
                }
            }
        }
    }
}