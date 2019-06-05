using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.BitlyAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.IssueReturnLabel
{
    /* v10 - 19 March 2019 - Neil Jansen
     * Created SubForm for usage of Issue Return Label
     */

    /* v11 - 3 May 2019 - Neil Jansen
     * Changed Form to user control
     * 
     * 15 May 2019 - Neil Jansen
     * Update overall logic to be incorporated as binding.
     * Developed and update functions to validate data entries respectively of what process is being called.
     * Developed function to allow validation to be called from parent pages.
     */

    /* v11.1 - 3 June 2019 - Neil Jansen
     * Added functionality to incorporate Copy to Clipboard functionality and Generate URL
     */

    public partial class ZendeskIssueReturnLabel : System.Web.UI.UserControl
    {
        public Customer cust;

        protected List<Zendesk> zendeskTickets;
        public string zendeskTicketsParsed = string.Empty;

        #region GlobalVariables

        public string No { get; set; }
        public string DocNo { get; set; }
        public string ExistingLabel { get; set; }
        public string TicketNo { get; set; }
        public string EmailTo { get; set; }
        public string FromEmailName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public bool ExistingZendeskTicket
        {
            get { return cbxZendeskTickets.Checked; }
        }
        public bool NewZendeskTicket
        {
            get { return cbxNewZendeskTicket.Checked; }
        }
        public bool DownloadManually
        {
            get { return cbxDownloadManually.Checked; }
        }
        public bool GenerateURL
        {
            get { return cbxGenerateURL.Checked; }
        }
        public string ZendeskTickets
        {
            get { return ddlZendeskTickets.SelectedValue; }
        }
        public string FromEmail
        {
            get { return txtFromEmail.Text; }
            set { txtFromEmail.Text = value; }
        }
        public string ToEmail
        {
            get { return txtToEmail.Text; }
        }
        public string CustomerEmailAddress
        {
            get { return txtcustEmailAddress.Text; }
        }
        public string CustomerName
        {
            get { return txtCustomerName.Text; }
            set { txtCustomerName.Text = value; }
        }
        public bool SelectedZendeskTicket
        {
            get { return cbxSelectZendeskTicket.Checked; }
        }
        public bool InsertZendeskTicket
        {
            get { return cbxInsertZendeskTicket.Checked; }
        }
        public string InsertedZendeskTicket
        {
            get { return txtInsertZendeskTicket.Text; }
        }

        #endregion

        public Zendesk ZendeskTicket { get; set; }

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(ZendeskIssueReturnLabel));

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserInteraction"] = true;

            try
            {
                No = Convert.ToString(Request.QueryString["No"]);
                DocNo = Convert.ToString(Request.QueryString["ExternalDocumentNo"]);

                if (IsPostBack)
                {
                    TicketNo = ddlZendeskTickets.SelectedValue;
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
                ExistingLabel = Convert.ToString(Request.QueryString["ExistingLabel"]);

                if (ExistingLabel.ToUpper() == "FALSE")
                {
                    trExistingZendeskTicket.Visible = true;
                    trNewZendeskTicket.Visible = true;

                    zendeskTickets = new List<Zendesk>();
                    cust = (Customer)Session["SelectedCustomer"];
                    zendeskTickets.Add(new Zendesk("", new DateTime(), new DateTime(), "", "", "", false, "", "", "", "", 0)); // set a blank default

                    foreach (SalesHeader head in cust.SalesHeader)
                    {
                        foreach (Zendesk ticket in head.Tickets)
                        {
                            if (!zendeskTickets.Any(t => t.TicketNo.Equals(ticket.TicketNo)))
                            {
                                if (String.IsNullOrEmpty(ticket.ToEmailsAddress) && String.IsNullOrEmpty(ticket.ToEmailsName))
                                {
                                    ticket.GetRequester();
                                }

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
                                if (String.IsNullOrEmpty(ticket.ToEmailsAddress) && String.IsNullOrEmpty(ticket.ToEmailsName))
                                {
                                    ticket.GetRequester();
                                }
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "exceptionAlert", "alert('" + ex.Message + "');", true);
            }
        }

        public bool VerifyInput(ref Zendesk zendeskTicket)
        {
            bool validInput = false;

            if (ExistingZendeskTicket)
            {
                if (SelectedZendeskTicket)
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

                   // TicketNo = ddlZendeskTickets.SelectedValue; ~ why are you here? Go away!

                    if (TicketNo != string.Empty)
                    {
                        foreach (Zendesk ticket in zendeskTickets)
                        {
                            if (ticket.TicketNo == TicketNo)
                            {
                                EmailSubject = ticket.Subject;
                                FromEmail = ticket.FromEmailAddress;
                                FromEmailName = ticket.FromEmailName;

                                EmailTo = ticket.ToEmailsAddress;
                                zendeskTicket = ticket;
                            }
                        }

                        validInput = true;
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "selectValidTicketNo", "alert('Please select a valid existing Zendesk ticket when attempting to update a ticket number.');", true);
                    }
                }
                else if (InsertZendeskTicket)
                {
                    long.TryParse(InsertedZendeskTicket, out long ticketResult);

                    if (ticketResult != 0)
                    {
                        Zendesk Zendesk = new Zendesk();
                        zendeskTicket = Zendesk.VerifyInsertedZendeskTicket(ticketResult);

                        if (zendeskTicket != null)
                        {
                            if (String.IsNullOrEmpty(zendeskTicket.FromEmailAddress) && String.IsNullOrEmpty(zendeskTicket.FromEmailName))
                            {
                                zendeskTicket.GetRequester();
                            }

                            validInput = true;
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "invalidZendeskTicket", "alert('The Zendesk ticket is invalid, please enter a valid ticket number.');", true);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "invalidTicketNoInserted", "alert('Please insert a valid Zendesk ticket number.');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "invalidZendeskUpdateSelection", "alert('Please select a valid option to update existing Zendesk Ticket.');", true);
                }
            }
            else if (NewZendeskTicket)
            {
                if (HttpContext.Current.Session["ActiveUser"] != null)
                {
                    User u = (User)HttpContext.Current.Session["ActiveUser"];
                    FromEmail = u.ZendeskEmail;

                    EmailTo = CustomerEmailAddress;

                    string validateInput = ValidateInput(EmailTo, CustomerName);
                    if (validateInput != "Valid Input")
                    {
                        validInput = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "unvalidatedData", "alert('" + validateInput + "');", true);
                    }
                    else
                    {
                        validInput = true;
                    }
                }
                else
                {
                    throw new Exception("Unable to retrieve current Zendesk Email address.");
                }
            }
            else if (DownloadManually)
            {
                validInput = true;
            }
            else if (GenerateURL)
            {
                validInput = true;
            }

            return validInput;
        }

        public void IssueZendeskReturnLabel(string no, string docNo, bool closeWindow, Zendesk ticket)
        {
            SendService ss = new SendService();

            string pdf64String = string.Empty;
            string amazonBucketURL = string.Empty;

            try
            {
                if (ExistingZendeskTicket)
                {
                    string message = string.Empty;
                    ZendeskTicket = ticket;

                    if (EmailTo != null)
                    {
                        int index = EmailTo.IndexOf('@');
                        EmailTo = EmailTo.Insert(index, "+id" + TicketNo);
                    }
                    else
                    {
                        EmailTo = string.Empty;
                    }

                    if (FromEmail == null)
                    {
                        FromEmail = string.Empty;
                    }

                    pdf64String = ss.IssueReturnLabel(no, EmailTo, ExistingZendeskTicket, FromEmail, DownloadManually, CustomerEmailAddress, FromEmailName, EmailSubject, ref amazonBucketURL, GenerateURL);

                    Session["NoUserInteraction"] = true;

                    ZendeskTicket.UpdateZendeskTicketWithPDFFile(pdf64String, no, amazonBucketURL);

                    message = no + ", Return label is being processed and will be emailed within 1 hour.";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "issueReturnLableExistingTicket", "alert('" + Json.Encode(message) + "');", true);

                    if (closeWindow)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAfterProcessing", "parent.window.close();", true);
                    }
                }
                else if (NewZendeskTicket)
                {
                    string message = string.Empty;

                    pdf64String = ss.IssueReturnLabel(no, EmailTo, ExistingZendeskTicket, FromEmail, DownloadManually, CustomerEmailAddress, FromEmailName, EmailSubject, ref amazonBucketURL, GenerateURL);
                    Session["NoUserInteraction"] = true;

                    long? newZendeskTicketID = 0;

                    ZendeskTicket = new Zendesk();
                    newZendeskTicketID = ZendeskTicket.CreateNewZendeskTicketWithPDFFile(pdf64String, no, amazonBucketURL, EmailTo, CustomerName, docNo);

                    message = "New Zendesk Ticket is: " + newZendeskTicketID + @".

" + no + ", Return label is being processed and will be emailed within 1 hour.";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "issueReturnLabelNewTicket", "alert('" + Json.Encode(message) + "');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "closeErrorAlert", "parent.window.close();", true);
                }
                else if (DownloadManually)
                {
                    string message = string.Empty;
                    pdf64String = ss.IssueReturnLabel(no, string.Empty, ExistingZendeskTicket, string.Empty, DownloadManually, CustomerEmailAddress, FromEmailName, EmailSubject, ref amazonBucketURL, GenerateURL);
                    Session["NoUserInteraction"] = true;

                    if (pdf64String != string.Empty) // Display only the URL
                    {
                        ZendeskTicket = new Zendesk();

                        ZendeskTicket.DownloadRMAPDFManually(pdf64String, no);

                        message = no + ", Return label has been successfully downloaded.";

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "manualDownloadExistingTicket", "alert('" + Json.Encode(message) + "');", true);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAfterDownload", "parent.window.close();", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "manualDownloadExistingTicket", "alert('The file could not be downloaded.');", true);
                    }
                }
                else if (GenerateURL)
                {
                    string message = string.Empty;
                    string clipboardMessage = string.Empty;
                    pdf64String = ss.IssueReturnLabel(no, string.Empty, ExistingZendeskTicket, string.Empty, DownloadManually, CustomerEmailAddress, FromEmailName, EmailSubject, ref amazonBucketURL, GenerateURL);
                    Session["NoUserInteraction"] = true;

                    if (amazonBucketURL != string.Empty) // Display only the URL
                    {
                        BitlyAPI bitlyAPI = new BitlyAPI();

                        string url = bitlyAPI.ShortenURL(amazonBucketURL);

                        message = no + ", Return label URL has been successfully generated.";

                        clipboardMessage = @"Hello, 

Your return request has been approved.  Your Return Merchandise Authorization number is " + no + @"

Follow the link below to download your return instructions and shipping label. Or copy and paste the link into your browser.

" + url + @"

IMPORTANT: Please remove ALL locks and passwords. Any device(s) received locked with your information will be denied, returned at your expense with no refund submitted for processing.

Thank You";

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "clipboardMessage", "CopyToClipboard('" + Json.Encode(clipboardMessage) + "');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "manualGenerateURL", "alert('The URL could not be generated.');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "manualDownloadExistingTicket", "alert('Please select a valid option.');", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", "alert('" + ex.Message.Replace("'", "\"").Replace("\n", "\\n") + "');", true);

                if (ex.Message.ToLower().Contains("session"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "closeErrorAlert", "parent.window.close();", true);
                }
            }
        }

        protected string ValidateInput(string customerEmail, string customerName)
        {
            string validInput = "Valid Input";

            if (!String.IsNullOrEmpty(customerEmail) || !String.IsNullOrWhiteSpace(customerEmail))
            {
                if (IsValidEmail(customerEmail))
                {
                    if (!String.IsNullOrWhiteSpace(customerName) || !String.IsNullOrEmpty(customerName))
                    {
                        return validInput;
                    }
                    else
                    {
                        return "Please enter a customer name.";
                    }
                }
                else
                {
                    if (!IsValidEmail(EmailTo))
                    {
                        return "Invalid customer email address entered! Please insert a valid email address.";
                    }
                }
            }
            else
            {
                return "Please insert a customer email address.";
            }

            return validInput;
        }

        protected bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch (Exception mailE)
            {
                Log.Error(mailE.Message, mailE);

                return false;
            }
        }
    }
}