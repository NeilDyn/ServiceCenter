using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.Enums;

namespace ExcelDesign.Forms.UserControls.TableData.DataLines.SalesOrderLines
{
    public partial class SingleSalesOrderPackages : System.Web.UI.UserControl
    {
        public List<PostedPackage> PostedPackage { get; set; }

        protected string shipMethod;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateData()
        {
            TrackingTypeEnum trackType = TrackingTypeEnum.Invalid;

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
                    shipMethod += " " + postedPack.ShippingAgentService;

                    packNo.Text = postPackLine.PackageNo;
                    packDate.Text = postedPack.PackingDate;
                    item.Text = postPackLine.ItemNo;
                    desc.Text = postPackLine.Description;
                    qty.Text = postPackLine.Quantity.ToString();
                    serialNo.Text = postPackLine.SerialNo;
                    carrier.Text = shipMethod;

                    string trackNo = postedPack.TrackingNo;
                    Enum.TryParse(postedPack.ShippingAgent, out trackType);
                    trackingNo.Text = SetTrackingNo(trackType, trackNo);

                    qty.HorizontalAlign = HorizontalAlign.Center;

                    tr.Cells.Add(packNo);
                    tr.Cells.Add(packDate);
                    tr.Cells.Add(item);
                    tr.Cells.Add(desc);
                    tr.Cells.Add(qty);
                    tr.Cells.Add(serialNo);
                    tr.Cells.Add(carrier);
                    tr.Cells.Add(trackingNo);

                    this.tblSalesPackageLines.Rows.Add(tr);
                }
            }
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

                case TrackingTypeEnum.Invalid:
                    textString = trackNo;
                    break;

                default:
                    break;
            }

            return textString;
        }
    }
}