using ExcelDesign.Class_Objects.CreatedReturn;
using ExcelDesign.Class_Objects.Documents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.FunctionForms
{
    public partial class CreatedRMA : System.Web.UI.Page
    {
        protected CreatedReturnHeader CRH { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string printRMA = string.Empty;

            if (Session["CreatedRMA"] != null)
            {
                try
                {
                    CRH = (CreatedReturnHeader)Session["CreatedRMA"];
                    Title = "Return: " + CRH.RMANo;

                    tcRmaNo.Text = CRH.RMANo;
                    tcExternalDocNo.Text = CRH.ExternalDocumentNo;
                    tcDateCreated.Text = CRH.DateCreated;
                    tcChannelName.Text = CRH.ChannelName;
                    tcReturnTrackingNo.Text = CRH.ReturnTrackingNo;
                    tcOrderDate.Text = CRH.OrderDate;

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
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "errorinOrderRMA", "alert('" + ex.Message + "');", true);
                }
            }           
        }

        protected void PopulateLines()
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

                TextBox qtyEdit = new TextBox
                {
                    ID = "qtyEdit" + lineCount.ToString(),
                    Text = line.Quantity.ToString(),
                    Width = new Unit("15%")
                };

                itemNo.ID = "itemNoR_" + lineCount.ToString();
                qty.ID = "itemQuanityR_" + lineCount.ToString();
                desc.ID = "descR_" + lineCount.ToString();
                price.ID = "priceR_" + lineCount.ToString();
                lineAmount.ID = "lineAmountR_" + lineCount.ToString();

                itemNo.Text = line.ItemNo;
                desc.Text = line.Description;
                qty.Controls.Add(qtyEdit);
                price.Text = "$     " + line.Price.ToString();
                lineAmount.Text = "$    " + line.LineAmount.ToString();

                qty.HorizontalAlign = HorizontalAlign.Center;
                price.HorizontalAlign = HorizontalAlign.Right;
                lineAmount.HorizontalAlign = HorizontalAlign.Right;

                singleRow.ID = "CreatedReturnOrderLineRow_" + lineCount.ToString();

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
    }
}