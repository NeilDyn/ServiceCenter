using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.CreatedPartRequest;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.PDAForms
{
    public partial class CreatedPartRequest : System.Web.UI.Page
    {
        protected CreatedPartRequestHeader CPRH { get; set; }

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(CreatedPartRequest));

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["RestartFromUserInteraction"] = true;

            try
            {
                if (Session["CreatedPartRequest"] != null)
                {
                    CPRH = (CreatedPartRequestHeader)Session["CreatedPartRequest"];
                    Title = "Quote: " + CPRH.QuoteNo;

                    tcQuoteNo.Text = CPRH.QuoteNo;
                    tcExternalDocNo.Text = CPRH.ExternalDocumentNo;
                    tcChannelName.Text = CPRH.ChannelName;
                    tcShipMethod.Text = CPRH.ShipMethod;
                    tcOriginalOrderNo.Text = CPRH.PartRequestOrderNo;
                    tcQuoteDate.Text = CPRH.QuoteDate;

                    tcShipToName.Text = CPRH.ShipToName;
                    tcShipToContact.Text = CPRH.ShipToContact;
                    tcShipToAddress1.Text = CPRH.ShipToAddress1;
                    tcShipToAddress2.Text = CPRH.ShipToAddress2;
                    tcShipToCity.Text = CPRH.ShipToCity;
                    tcShipToState.Text = CPRH.ShipToState;
                    tcShipToZip.Text = CPRH.ShipToZip;
                    tcShipToCountry.Text = CPRH.ShipToCountry;

                    PopulateLines();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "errorinPartReuqest", "alert('" + ex.Message + "');", true);
            }
        }

        protected void PopulateLines()
        {
            try
            {
                int lineCount = 0;

                foreach (CreatedPartRequestLines line in CPRH.PartRequestLines)
                {
                    TableRow singleRow = new TableRow();

                    TableCell itemNo = new TableCell();
                    TableCell desc = new TableCell();
                    TableCell qty = new TableCell();
                    TableCell price = new TableCell();
                    TableCell lineAmount = new TableCell();

                    itemNo.ID = "itemNoR_" + lineCount.ToString();
                    qty.ID = "itemQuanityR_" + lineCount.ToString();
                    desc.ID = "descR_" + lineCount.ToString();
                    price.ID = "priceR_" + lineCount.ToString();
                    lineAmount.ID = "lineAmountR_" + lineCount.ToString();

                    itemNo.Text = line.ItemNo;
                    desc.Text = line.Description;
                    qty.Text = line.Quantity.ToString();
                    price.Text = "$     " + line.Price.ToGBString();
                    lineAmount.Text = "$    " + line.LineAmount.ToGBString();

                    qty.HorizontalAlign = HorizontalAlign.Center;
                    price.HorizontalAlign = HorizontalAlign.Right;
                    lineAmount.HorizontalAlign = HorizontalAlign.Right;

                    singleRow.ID = "CreatedPartRequestLineRow_" + lineCount.ToString();

                    singleRow.Cells.Add(itemNo);
                    singleRow.Cells.Add(desc);
                    singleRow.Cells.Add(qty);
                    singleRow.Cells.Add(price);
                    singleRow.Cells.Add(lineAmount);

                    if (lineCount % 2 == 0)
                    {
                        singleRow.BackColor = Color.White;
                    }
                    else
                    {
                        singleRow.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                    }

                    singleRow.Attributes.CssStyle.Add("border-collapse", "collapse");
                    TblReturnHeaderLines.Rows.Add(singleRow);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "alertError", "alert('" + ex.Message + "');", true);
            }
        }
    }
}