using System;
using System.Collections.Generic;
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
            foreach (ShipmentHeader sh in ShipmentHeaders)
            {
                foreach (ShipmentLine sl in sh.ShipmentLines)
                {
                    TableRow tr = new TableRow();

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

                    tr.Cells.Add(shipNo);
                    tr.Cells.Add(shipDate);
                    tr.Cells.Add(item);
                    tr.Cells.Add(desc);
                    tr.Cells.Add(qty);
                    tr.Cells.Add(shipMethod);

                    this.tblShipmentLines.Rows.Add(tr);
                }
            }
        }
    }
}