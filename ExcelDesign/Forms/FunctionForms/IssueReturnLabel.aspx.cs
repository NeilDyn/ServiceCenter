using ExcelDesign.Class_Objects;
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
    /* v10 - 19 March 2018 - Neil Jansen
     * Created SubForm for usage of Issue Return Label
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

                    if(existingLabel.ToUpper() == "TRUE")
                    {
                        BtnIssueReturnLabel.Text = "Download Return Label";
                    }
                }
                else
                {
                    ticketNo = ddlZendeskTickets.SelectedValue;
                }

                LoadIssueReturnData();
            }
            catch (Exception loadE)
            {
                Log.Error(loadE.Message, loadE);
            }
        }

        protected void LoadIssueReturnData()
        {
            try
            {
                existingLabel = Convert.ToString(Request.QueryString["ExistingLabel"]);

                if (existingLabel.ToUpper() == "FALSE")
                {
                    trExistingZendeskTicket.Visible = true;
                    trNewZendeskTicket.Visible = true;

                    zendeskTickets = new List<Zendesk>();
                    cust = (Customer)Session["SelectedCustomer"];

                    foreach (SalesHeader head in cust.SalesHeader)
                    {
                        foreach (Zendesk ticket in head.Tickets)
                        {
                            if (!zendeskTickets.Any(t => t.TicketNo.Equals(ticket.TicketNo)))
                            {
                                zendeskTickets.Add(ticket);
                            }
                        }
                    }

                    foreach (ReturnHeader returnHead in cust.ReturnHeaders)
                    {
                        foreach (Zendesk ticket in returnHead.Tickets)
                        {
                            if (!zendeskTickets.Any(t => t.TicketNo.Equals(ticket.TicketNo)))
                            {
                                zendeskTickets.Add(ticket);
                            }
                        }
                    }

                    zendeskTicketsParsed = JsonConvert.SerializeObject(zendeskTickets);

                    ddlZendeskTickets.DataValueField = "TicketNo";
                    ddlZendeskTickets.DataSource = zendeskTickets;
                    ddlZendeskTickets.DataBind();
                }
                else
                {
                    trExistingZendeskTicket.Visible = false;
                    trNewZendeskTicket.Visible = false;
                    zendeskTicketsParsed = JsonConvert.SerializeObject(string.Empty);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                ClientScript.RegisterStartupScript(this.GetType(), "exceptionAlert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void BtnIssueReturnLabel_Click(object sender, EventArgs e)
        {
            try
            {
                no = tcNo.Text;
                docNo = tcDocNo.Text;
                newEmail = txtcustEmailAddress.Text;
                newZendeskTicket = cbxNewZendeskTicket.Checked;
                existingZendeskTicket = cbxZendeskTickets.Checked;
                downloadManually = cbxDownloadManually.Checked;
                SendService ss = new SendService();
                Zendesk zendeskTicket = new Zendesk();
                string pdf64String = string.Empty;

                if (!downloadManually)
                {
                    cust = (Customer)Session["SelectedCustomer"];
                    foreach (SalesHeader head in cust.SalesHeader)
                    {
                        foreach (Zendesk ticket in head.Tickets)
                        {
                            if (!zendeskTickets.Any(t => t.TicketNo.Equals(ticket.TicketNo)))
                            {
                                zendeskTickets.Add(ticket);
                            }
                        }
                    }

                    foreach (ReturnHeader returnHead in cust.ReturnHeaders)
                    {
                        foreach (Zendesk ticket in returnHead.Tickets)
                        {
                            if (!zendeskTickets.Any(t => t.TicketNo.Equals(ticket.TicketNo)))
                            {
                                zendeskTickets.Add(ticket);
                            }
                        }
                    }
                }                

                if (existingZendeskTicket)
                {
                    //ticketNo = ddlZendeskTickets.SelectedValue;
                    foreach (Zendesk ticket in zendeskTickets)
                    {
                        if (ticket.TicketNo == ticketNo)
                        {
                            emailSubject = ticket.Subject;
                            fromEmail = ticket.FromEmailAddress;
                            fromEmailName = ticket.FromEmailName;

                            emailTo = ticket.ToEmailsAddress;
                            zendeskTicket = ticket;
                        }
                    }

                    if (emailTo != null)
                    {
                        int index = emailTo.IndexOf('@');
                        emailTo = emailTo.Insert(index, "+id" + ticketNo);
                    }
                    else
                    {
                        emailTo = string.Empty;
                    }

                    if (fromEmail == null)
                    {
                        fromEmail = string.Empty;
                    }

                    pdf64String = ss.IssueReturnLabel(no, emailTo, existingZendeskTicket, fromEmail, downloadManually, newEmail, fromEmailName, emailSubject);

                    Session["NoUserInteraction"] = true;
                    if (pdf64String != string.Empty)
                    {
                        zendeskTicket.UpdateZendeskTicketWithPDFFile(pdf64String, no);

                        ClientScript.RegisterStartupScript(this.GetType(), "issueReturnLableExistingTicket", "alert('" + no + ", Return label is being processed and will be emailed within 1 hour.');", true);
                        ClientScript.RegisterStartupScript(this.GetType(), "closeErrorAlert", "parent.window.close();", true);
                    }
                }
                else if(newZendeskTicket)
                {

                    //pdf64String = ss.IssueReturnLabel(no, toEmail, existingZendeskTicket, fromEmail, downloadManually, newEmail, fromEmailName, emailSubject);
                    //Session["NoUserInteraction"] = true;
                    //if (pdf64String != string.Empty)
                    //{
                    //    long? newZendeskTicketID = 0;

                    //    newZendeskTicketID = zendeskTicket.CreateNewZendeskTicketWithPDFFile(pdf64String, no);

                    //    ClientScript.RegisterStartupScript(this.GetType(), "issueReturnLabelNewTicket", "alert('New Zendesk Ticket is:" + newZendeskTicketID + ".\n" + no + ", Return label is being processed and will be emailed within 1 hour.');", true);
                    //    ClientScript.RegisterStartupScript(this.GetType(), "closeErrorAlert", "parent.window.close();", true);
                    //}
                    ClientScript.RegisterStartupScript(this.GetType(), "notyetImplemented", "alert('New TIcket Functionality not yet implemented.');", true);
                }
                else if(downloadManually)
                {
                    emailTo = string.Empty;
                    fromEmail = string.Empty;
                    pdf64String = ss.IssueReturnLabel(no, emailTo, existingZendeskTicket, fromEmail, downloadManually, newEmail, fromEmailName, emailSubject);

                    Session["NoUserInteraction"] = true;

                    if(pdf64String != string.Empty)
                    {
                        zendeskTicket.DownloadRMAPDFManually(pdf64String, no);

                        ClientScript.RegisterStartupScript(this.GetType(), "manualDownloadExistingTicket", "alert('" + no + ", Return label has been successfully downloaded.');", true);
                        ClientScript.RegisterStartupScript(this.GetType(), "closeAfterDownload", "parent.window.close();", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "manualDownloadExistingTicket", "alert('Please select a valid option.');", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

                ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('" + ex.Message.Replace("'", "\"") + "');", true);

                if (ex.Message.ToLower().Contains("session"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "closeErrorAlert", "parent.window.close();", true);
                }
            }
        }
    }
}