using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.FunctionData;

namespace ExcelDesign.Forms.FunctionForms
{
    public partial class CreateReturn : System.Web.UI.Page
    {
        protected string orderNo;
        protected string docNo;
        protected string notes;
        protected string returnReason;
        protected int defect;

        protected bool resources;
        protected bool printRMA;
        protected bool createLabel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                List<Defects> doList = (List<Defects>)Session["Defects"];
                List<ReturnReason> rrList = (List<ReturnReason>)Session["ReturnReasons"];

                this.ddlDefectOptions.DataValueField = "Option";
                this.ddlDefectOptions.DataSource = doList;
                this.ddlDefectOptions.DataBind();

                this.ddlReturnReason.DataValueField = "ReasonCode";
                this.ddlReturnReason.DataSource = rrList;
                this.ddlReturnReason.DataBind();

                this.tcOrderNo.Text = Convert.ToString(Request.QueryString["OrderNo"]);
                this.tcDocNo.Text = Convert.ToString(Request.QueryString["ExternalDocumentNo"]);
            }
        }

        protected void btnCreateRMA_Click(object sender, EventArgs e)
        {
            string returnRMA;

            try
            {
                orderNo = tcOrderNo.Text;
                docNo = tcDocNo.Text;
                notes = txtNotes.Text;
                returnReason = ddlReturnReason.SelectedItem.Text;
                defect = ddlDefectOptions.SelectedIndex;
                resources = cbxResources.Checked;
                printRMA = cbxPrintRMA.Checked;
                createLabel = cbxCreateLable.Checked;

                SendService ss = new SendService();
                returnRMA = ss.CreateReturnOrder(orderNo, docNo, returnReason, defect, notes, resources, printRMA, createLabel);

                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + returnRMA + "');", true);
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Redirect("../ErrorForm.aspx");
            }
        }
    }
}