using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.CreatedExchange;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.FunctionForms
{
    public partial class CreatedExchange : System.Web.UI.Page
    {
        protected CreatedExchangeHeader CEH { get; set; }

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(CreatedExchange));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CreatedExchange"] != null)
                {
                    CEH = (CreatedExchangeHeader)Session["CreatedExchange"];
                    Title = "Order: " + CEH.OrderNo;

                    tcOrderNo.Text = CEH.OrderNo;
                    tcExternalDocNo.Text = CEH.ExternalDocumentNo;
                    tcChannelName.Text = CEH.ChannelName;
                    tcShipMethod.Text = CEH.ShipMethod;
                    tcRmaNo.Text = CEH.RMANo;
                    tcOrderDate.Text = CEH.OrderDate;

                    tcShipToName.Text = CEH.ShipToName;
                    tcShipToContact.Text = CEH.ShipToContact;
                    tcShipToAddress1.Text = CEH.ShipToAddress1;
                    tcShipToAddress2.Text = CEH.ShipToAddress2;
                    tcShipToCity.Text = CEH.ShipToCity;
                    tcShipToState.Text = CEH.ShipToState;
                    tcShipToZip.Text = CEH.ShipToZip;
                    tcShipToCountry.Text = CEH.ShipToCountry;

                    PopulateLines();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "errorinOrderExchange", "alert('" + ex.Message + "');", true);
            }

        }

        protected void PopulateLines()
        {
            try
            {
                int lineCount = 0;

                foreach (CreatedExchangeLines line in CEH.ExchangeLines)
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

                    singleRow.ID = "CreatedExchangeOrderLineRow_" + lineCount.ToString();

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