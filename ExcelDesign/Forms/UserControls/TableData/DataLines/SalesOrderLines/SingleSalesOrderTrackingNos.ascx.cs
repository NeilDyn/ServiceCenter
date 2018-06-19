using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.TableData.DataLines.SalesOrderLines
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
            TrackingTypeEnum trackType = TrackingTypeEnum.Invalid;
            int packCount = 0;

            TableRow breakRow = new TableRow();
            TableCell breakCell = new TableCell
            {
                Text = "<br />"
            };

            breakRow.Cells.Add(breakCell);

            int lineCount = 0;

            foreach (PostedPackage postedPack in PostedPackage)
            {
                packCount++;
                lineCount++;

                TableRow packHeaderRow = new TableRow();
                TableCell packageHeader = new TableCell
                {
                    Text = "Package " + packCount.ToString(),
                };

                packageHeader.Font.Underline = true;
                packageHeader.Font.Bold = true;

                packHeaderRow.Cells.Add(packageHeader);
                this.tblPackageLines.Rows.Add(packHeaderRow);

                TableRow tr = new TableRow();

                TableCell blankCell = new TableCell();
                TableCell packDate = new TableCell();
                TableCell carrier = new TableCell();
                TableCell trackingNo = new TableCell();

                shipMethod = postedPack.ShippingAgent;
                shipMethod += " " + postedPack.ShippingAgentService;

                packDate.Text = postedPack.PackingDate;
                carrier.Text = shipMethod;

                string trackNo = postedPack.TrackingNo;
                Enum.TryParse(postedPack.ShippingAgent, out trackType);
                trackingNo.Text = SetTrackingNo(trackType, trackNo);

                tr.Cells.Add(blankCell);
                tr.Cells.Add(packDate);
                tr.Cells.Add(carrier);
                tr.Cells.Add(trackingNo);

                if (lineCount % 2 == 0)
                {
                    tr.BackColor = Color.White;
                }
                else
                {
                    tr.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                }

                this.tblPackageLines.Rows.Add(tr);
            }
            this.tblPackageLines.Rows.Add(breakRow);
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

                case TrackingTypeEnum.AMAZON:
                    textString = "<a href='http://wwwapps.ups.com/WebTracking/track?track=yes&trackNums=" + trackNo + "' target = '_blank'>" + trackNo + "</a >";
                    break;

                default:
                    textString = trackNo;
                    break;
            }

            return textString;
        }
    }
}