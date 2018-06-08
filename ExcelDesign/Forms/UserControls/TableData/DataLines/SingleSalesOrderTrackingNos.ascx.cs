using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.TableData.DataLines
{
    public partial class SingleSalesOrderTrackingNos : System.Web.UI.UserControl
    {
        public List<PostedPackage> PostedPackage { get; set; }
        protected string shipMethod;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateData()
        {
            foreach (PostedPackage postedPack in PostedPackage)
            {
                foreach (PostedPackageLine postPackLine in postedPack.PostedPackageLines)
                {
                    TableRow tr = new TableRow();

                    TableCell packNo = new TableCell();
                    TableCell packDate = new TableCell();
                    TableCell item = new TableCell();
                    TableCell desc = new TableCell();
                    TableCell qty = new TableCell();
                    TableCell serialNo = new TableCell();
                    TableCell carrier = new TableCell();
                    TableCell trackingNo = new TableCell();

                    shipMethod = postedPack.ShippingAgent;
                    shipMethod += postedPack.ShippingAgentService;

                    packNo.Text = postPackLine.PackageNo;
                    packDate.Text = postedPack.PackingDate;
                    item.Text = postPackLine.ItemNo;
                    desc.Text = postPackLine.Description;
                    qty.Text = postPackLine.Quantity.ToString();
                    serialNo.Text = postPackLine.SerialNo;
                    carrier.Text = shipMethod;
                    trackingNo.Text = postedPack.TrackingNo;

                    qty.HorizontalAlign = HorizontalAlign.Center;

                    tr.Cells.Add(packNo);
                    tr.Cells.Add(packDate);
                    tr.Cells.Add(item);
                    tr.Cells.Add(desc);
                    tr.Cells.Add(qty);
                    tr.Cells.Add(serialNo);
                    tr.Cells.Add(carrier);
                    tr.Cells.Add(trackingNo);

                    this.tblPackageLines.Rows.Add(tr);
                }
            }
        }
    }
}