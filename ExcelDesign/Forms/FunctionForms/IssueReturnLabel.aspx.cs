using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.IssueReturnLabel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.FunctionForms
{
    /* v10 - 19 March 2019 - Neil Jansen
     * Created SubForm for usage of Issue Return Label
     */
    
    /* v11 - 15 May 2019 - Neil Jansen
    * Redesigned form. Changed all existing logic to User Control.
    * Call functions from User Control inherited to continue logic.
    */

    public partial class IssueReturnLabel : System.Web.UI.Page
    {
        public Customer cust;
        protected string no;
        protected string docNo;
        protected string ticketNo;
        protected string existingLabel;

        protected bool existingZendeskTicket;
        protected bool newZendeskTicket;
        protected bool downloadManually;

        protected string newEmail;
        protected string fromEmail;
        protected string emailTo;
        protected string fromEmailName;
        protected string emailSubject;
        protected string emailBody;

        protected List<Zendesk> zendeskTickets;
        public string zendeskTicketsParsed;

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(IssueReturnLabel));

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserInteraction"] = true;

            try
            {
                if (!IsPostBack)
                {
                    tcNo.Text = Convert.ToString(Request.QueryString["No"]);
                    tcDocNo.Text = Convert.ToString(Request.QueryString["ExternalDocumentNo"]);
                    existingLabel = Convert.ToString(Request.QueryString["ExistingLabel"]);

                    cust = (Customer)Session["SelectedCustomer"];

                    if (cust != null)
                    {
                        tcShipFromName.Text = cust.Name;
                        tcShipFromAddress1.Text = cust.Address1;
                        tcShipFromAddress2.Text = cust.Address2;
                        tcShipFromState.Text = cust.State;
                        tcShipFromCity.Text = cust.City;
                        tcShipFromCode.Text = cust.Zip;
                    }

                    if (existingLabel.ToUpper() == "TRUE")
                    {
                        BtnIssueReturnLabel.Text = "Download Return Label";
                    }
                }
            }
            catch (Exception loadE)
            {
                Log.Error(loadE.Message, loadE);
            }
        }

        protected void BtnIssueReturnLabel_Click(object sender, EventArgs e)
        {
            Zendesk ticket = new Zendesk();
            bool dataVerified = ZendeskIssueReturnLabelControl.VerifyInput(ref ticket);

            if (dataVerified)
            {
                ZendeskIssueReturnLabelControl.IssueZendeskReturnLabel(tcNo.Text, tcDocNo.Text, true, ticket);
            }
        }
    }
}