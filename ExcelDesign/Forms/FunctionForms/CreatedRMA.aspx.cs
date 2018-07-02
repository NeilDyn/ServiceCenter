using ExcelDesign.Class_Objects.CreatedReturn;
using System;
using System.Collections.Generic;
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
                //e.Row.Cells[1].Width = new Unit("50%");

                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells[2].Width = new Unit("5%");

                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[3].Width = new Unit("5%");

                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[4].Width = new Unit("5%");
            }
        }
    }
}