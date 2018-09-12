using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.CreatedReturn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.PDAForms
{
    public partial class CreatedPDARMA : System.Web.UI.Page
    {
        protected CreatedReturnHeader CRH { get; set; }
        public string OrderNo { get; set; }
        public string RmaNo { get; set; }
        public string ExtDocNo { get; set; }
        public string Update { get; set; }

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(CreatedPDARMA));

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["RMAInteraction"] = true;
            Session["RestartFromUserInteraction"] = true;

            string printRMA = string.Empty;

            try
            {
                if (Session["CreatedRMA"] != null)
                {
                    CRH = (CreatedReturnHeader)Session["CreatedRMA"];
                    Title = "Return: " + CRH.RMANo;
                    RmaNo = CRH.RMANo;

                    tcRmaNo.Text = CRH.RMANo;
                    tcExternalDocNo.Text = CRH.ExternalDocumentNo;
                    tcDateCreated.Text = CRH.DateCreated;
                    tcChannelName.Text = CRH.ChannelName;
                    tcReturnTrackingNo.Text = CRH.ReturnTrackingNo;
                    tcOrderDate.Text = CRH.OrderDate;
                    tcIMEINo.Text = CRH.IMEINo;

                    tcShipToName.Text = CRH.ShipToName;
                    tcShipToAddress1.Text = CRH.ShipToAddress1;
                    tcShipToAddress2.Text = CRH.ShipToAddress2;
                    tcShipToCity.Text = CRH.ShipToCity;
                    tcShipToCode.Text = CRH.ShipToCode;
                    tcShipToState.Text = CRH.ShipToState;

                    PopulateLines();

                    printRMA = Convert.ToString(Request.QueryString["PrintRMAInstructions"]);

                    if (printRMA.ToUpper() == "TRUE")
                    {
                        BtnPrintRMAInstructions.Visible = true;
                    }
                    else
                    {
                        BtnPrintRMAInstructions.Visible = false;
                    }

                    OrderNo = Convert.ToString(Request.QueryString["OrderNo"]);
                    ExtDocNo = Convert.ToString(Request.QueryString["ExternalDocumentNo"]);
                    Update = "True";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "errorinOrderRMA", "alert('" + ex.Message + "');", true);
            }
        }

        protected void PopulateLines()
        {
            try
            {
                int lineCount = 0;

                foreach (CreatedReturnLines line in CRH.CreatedReturnLines)
                {
                    lineCount++;

                    TableRow singleRow = new TableRow();

                    TableCell itemNo = new TableCell();
                    TableCell desc = new TableCell();
                    TableCell qty = new TableCell();
                    TableCell price = new TableCell();
                    TableCell lineAmount = new TableCell();
                    TableCell returnReason = new TableCell();
                    TableCell reqReturnAction = new TableCell();

                    itemNo.ID = "itemNoR_" + lineCount.ToString();
                    qty.ID = "itemQuanityR_" + lineCount.ToString();
                    desc.ID = "descR_" + lineCount.ToString();
                    price.ID = "priceR_" + lineCount.ToString();
                    lineAmount.ID = "lineAmountR_" + lineCount.ToString();
                    returnReason.ID = "returnReasonR_" + lineCount.ToString();
                    reqReturnAction.ID = "reqReturnActionR_" + lineCount.ToString();

                    itemNo.Text = line.ItemNo;
                    desc.Text = line.Description;
                    qty.Text = line.Quantity.ToString();
                    price.Text = "$     " + line.Price.ToGBString();
                    lineAmount.Text = "$    " + line.LineAmount.ToGBString();
                    returnReason.Text = line.ReturnReason;
                    reqReturnAction.Text = line.REQReturnAction;

                    qty.HorizontalAlign = HorizontalAlign.Center;
                    price.HorizontalAlign = HorizontalAlign.Right;
                    lineAmount.HorizontalAlign = HorizontalAlign.Right;

                    singleRow.ID = "CreatedReturnOrderLineRow_" + lineCount.ToString();

                    singleRow.Cells.Add(itemNo);
                    singleRow.Cells.Add(desc);
                    singleRow.Cells.Add(qty);
                    singleRow.Cells.Add(price);
                    singleRow.Cells.Add(lineAmount);
                    singleRow.Cells.Add(returnReason);
                    singleRow.Cells.Add(reqReturnAction);

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

        protected void BtnCancelRMA_Click(object sender, EventArgs e)
        {
            try
            {
                SendService ss = new SendService();

                string delete = ss.DeleteRMA(CRH.RMANo);

                ClientScript.RegisterStartupScript(this.GetType(), "deletedRMA", "alert('" + delete + "');", true);
                ClientScript.RegisterStartupScript(this.GetType(), "closeRMA", "parent.window.close();", true);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "alertError", "alert('" + ex.Message + "');", true);

                if (ex.Message.ToLower().Contains("session"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "closeRMA", "parent.window.close();", true);
                }
            }
        }

        protected void BtnUpdateRMA_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "updateRMA", "UpdateRMA();", true);
        }
    }
}