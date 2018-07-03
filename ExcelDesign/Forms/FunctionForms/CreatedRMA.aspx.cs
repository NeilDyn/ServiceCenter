using ExcelDesign.Class_Objects.CreatedReturn;
using ExcelDesign.Class_Objects.Documents;
using System;
using System.Collections.Generic;
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
        protected MemoryStream MemStream { get; set; }

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

                    gdvReturnHeaderLines.DataSource = CRH.CreatedReturnLines;
                    gdvReturnHeaderLines.DataBind();

                    printRMA = Convert.ToString(Request.QueryString["PrintRMAInstructions"]);

                    if (printRMA.ToUpper() == "TRUE")
                    {
                        BtnViewRMAInstructions.Visible = true;
                        RMAInstructionsPDF createPDF = new RMAInstructionsPDF();
                        MemStream = createPDF.CreatePDF(CRH.RMANo);
                    }
                    else
                    {
                        BtnViewRMAInstructions.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "errorinOrderRMA", "alert('" + ex.Message + "');", true);
                }
            }           
        }

        protected void gdvReturnHeaderLines_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        protected void BtnViewRMAInstructions_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.OutputStream.Write(MemStream.GetBuffer(), 0, MemStream.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
        }
    }
}