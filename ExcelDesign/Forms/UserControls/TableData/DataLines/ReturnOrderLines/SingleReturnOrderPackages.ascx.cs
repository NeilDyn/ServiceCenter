using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines
{
    public partial class SingleReturnOrderPackages : System.Web.UI.UserControl
    {
        public List<PostedReceive> PostedReceive { get; set; }

        protected string shipMethod;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateData()
        {
            TrackingTypeEnum trackType = TrackingTypeEnum.Invalid;
            int receiveCount = 0;

            TableRow breakRow = new TableRow();
            TableCell breakCell = new TableCell
            {
                Text = "<br />"
            };

            breakRow.Cells.Add(breakCell);

            foreach (PostedReceive postedReceive in PostedReceive)
            {
                receiveCount++;

                TableRow receiveHeaderRow = new TableRow();
                TableCell receiveHeader = new TableCell
                {
                    Text = "Receive " + receiveCount.ToString(),
                };

                receiveHeader.Font.Underline = true;
                receiveHeader.Font.Bold = true;

                receiveHeaderRow.Cells.Add(receiveHeader);
                this.tblReturnPackageLines.Rows.Add(receiveHeaderRow);

                int lineCount = 0;

                foreach (PostedReceiveLine postReceiveLine in postedReceive.PostedReceiveLines)
                {
                    lineCount++;

                    TableRow tr = new TableRow();

                    TableCell blankCell = new TableCell();
                    TableCell packNo = new TableCell();
                    TableCell packDate = new TableCell();
                    TableCell item = new TableCell();
                    TableCell desc = new TableCell();
                    TableCell qty = new TableCell();
                    TableCell serialNo = new TableCell();
                    TableCell carrier = new TableCell();
                    TableCell trackingNo = new TableCell();

                    shipMethod = postedReceive.ShippingAgent;
                    shipMethod += postedReceive.ShippingAgentService;

                    packNo.Text = postReceiveLine.ReceiveNo;
                    packDate.Text = postedReceive.ReceiveDate;
                    item.Text = postReceiveLine.ItemNo;
                    desc.Text = postReceiveLine.Description;
                    qty.Text = postReceiveLine.Quantity.ToString();
                    serialNo.Text = postReceiveLine.SerialNo;
                    carrier.Text = shipMethod;

                    string trackNo = postedReceive.TrackingNo;
                    Enum.TryParse(postedReceive.ShippingAgent, out trackType);
                    trackingNo.Text = SetTrackingNo(trackType, trackNo);

                    qty.HorizontalAlign = HorizontalAlign.Center;

                    tr.Cells.Add(blankCell);
                    tr.Cells.Add(packNo);
                    tr.Cells.Add(packDate);
                    tr.Cells.Add(item);
                    tr.Cells.Add(desc);
                    tr.Cells.Add(qty);
                    tr.Cells.Add(serialNo);
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


                    if (lineCount % 2 == 0)
                    {
                        tr.BackColor = Color.White;
                    }
                    else
                    {
                        tr.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                    }

                    this.tblReturnPackageLines.Rows.Add(tr);
                }
            }

            this.tblReturnPackageLines.Rows.Add(breakRow);
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
                    textString = trackNo;
                    break;
            }

            return textString;
        }
    }
}