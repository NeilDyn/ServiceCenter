using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;

namespace ExcelDesign.Forms.UserControls.TableData.DataLines.SalesOrderLines
{
    public partial class SingleSalesOrderShipments : System.Web.UI.UserControl
    {
        public List<ShipmentHeader> ShipmentHeaders { get; set; }
        protected string shipmentMethod;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateData()
        {
            int shipCount = 0;

            TableRow breakRow = new TableRow();
            TableCell breakCell = new TableCell
            {
                Text = "<br />"
            };

            breakRow.Cells.Add(breakCell);

            foreach (ShipmentHeader sh in ShipmentHeaders)
            {
                shipCount++;

                TableRow shipHeaderRow = new TableRow();
                TableCell shipmentHeader = new TableCell
                {
                    Text = "Shipment " + shipCount.ToString(),
                };

                shipmentHeader.Font.Underline = true;
                shipmentHeader.Font.Bold = true;

                shipHeaderRow.Cells.Add(shipmentHeader);
                this.tblShipmentLines.Rows.Add(shipHeaderRow);

                int lineCount = 0;

                foreach (ShipmentLine sl in sh.ShipmentLines)
                {
                    if (sl.Quantity > 0)
                    {
                        lineCount++;

                        TableRow tr = new TableRow();

                        TableCell blankCell = new TableCell();
                        TableCell shipNo = new TableCell();
                        TableCell shipDate = new TableCell();
                        TableCell item = new TableCell();
                        TableCell desc = new TableCell();
                        TableCell qty = new TableCell();
                        TableCell shipMethod = new TableCell();

                        shipmentMethod = sh.ShippingAgentCode;
                        shipmentMethod += " " + sh.ShippingAgentService;

                        shipNo.Text = sh.No;
                        shipDate.Text = sh.ShippingDate;
                        item.Text = sl.ItemNo;
                        desc.Text = sl.Description;
                        qty.Text = sl.Quantity.ToString();
                        shipMethod.Text = shipmentMethod;

                        qty.HorizontalAlign = HorizontalAlign.Center;

                        tr.Cells.Add(blankCell);
                        tr.Cells.Add(shipNo);
                        tr.Cells.Add(shipDate);
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

                        this.tblShipmentLines.Rows.Add(tr);
                    }
                }
            }

            this.tblShipmentLines.Rows.Add(breakRow);
        }
    }
}