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
        protected string email;

        protected bool resources;
        protected bool printRMA;
        protected bool createLabel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Defects> doList = (List<Defects>)Session["Defects"];
                List<ReturnReason> rrList = (List<ReturnReason>)Session["ReturnReasons"];

                this.ddlDefectOptions.DataValueField = "Option";
                this.ddlDefectOptions.DataSource = doList;
                this.ddlDefectOptions.DataBind();

                this.ddlReturnReason.DataValueField = "Description";
                this.ddlReturnReason.DataSource = rrList;
                this.ddlReturnReason.DataBind();

                this.tcOrderNo.Text = Convert.ToString(Request.QueryString["OrderNo"]);
                this.tcDocNo.Text = Convert.ToString(Request.QueryString["ExternalDocumentNo"]);
            }
        }

        protected void btnCreateRMA_Click(object sender, EventArgs e)
        {
            string returnRMA;
            int returnReasonSelect;

            try
            {
                returnReasonSelect = ddlReturnReason.SelectedIndex;
                orderNo = tcOrderNo.Text;
                docNo = tcDocNo.Text;
                notes = txtNotes.Text;
                returnReason = ((List<ReturnReason>)Session["ReturnReasons"])[returnReasonSelect].ReasonCode;
                defect = ddlDefectOptions.SelectedIndex;
                resources = cbxResources.Checked;
                printRMA = cbxPrintRMA.Checked;
                createLabel = cbxCreateLable.Checked;
                email = txtCustEmail.Text;

                string validateMsg = ValidateInput();

                if (validateMsg == "All Input Valid")
                {
                    SendService ss = new SendService();
                    returnRMA = ss.CreateReturnOrder(orderNo, docNo, returnReason, defect, notes, resources, printRMA, createLabel, email);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + returnRMA + "');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + validateMsg + "');", true);
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
            }
        }

        protected string ValidateInput()
        {
            string valid = "All Input Valid";

            try
            {
                if (!String.IsNullOrWhiteSpace(returnReason))
                {
                    if (defect > 0)
                    {
                        if (createLabel)
                        {
                            if(!String.IsNullOrWhiteSpace(email))
                            {
                                return valid;
                            }
                            else
                            {
                                return "Updated email is required for creating a return label.";
                            }
                        } 
                        else
                        {
                            return valid;
                        }
                    }
                    else
                    {
                        return "Please select a valid defect option.";
                    }

                }
                else
                {
                    return "Please select a valid return reason";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}