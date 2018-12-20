using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.Documents;
using ExcelDesign.Class_Objects.Enums;
using ExcelDesign.Forms.UserControls;
using ExcelDesign.Forms.UserControls.CustomerInfo.MainTables;
using ExcelDesign.Forms.UserControls.TableData;
using ExcelDesign.Forms.UserControls.TableHeaders;

namespace ExcelDesign.Forms
{
    public partial class ServiceCenter : System.Web.UI.Page
    {
        /* NJ 5 September 2018
         * Updated with User Control Navigation bar.
        */

        /*
         * v7.1 - 3 October 2018 - Neil Jansen
         * Added logic to prevent "Thread was being aborted" to be the error that is displayed as it is caused by lower level exceptions being thrown that should be displayed instead
         * Added logic to display as alert when Order has been cancelled
         * Updated logic to clear all controls and lists when PostBack occours to prevent Customer data to be duplicated
         */

        /* v9.1 - 13 December 2018 - Neil Jansen
         * Update logic to allow IssueRefund to pass Zendesk Ticket # through webservice
         */

        /* v9.3 - 19 December 2018 - Neil Jansen
         * Added new WebMethod to update Zendesk Ticket No
         * 
         * 20 December 2018 - Neil Jansen
         * Added condition to PopulateData function to display message when no search results are returned.
         */

        #region Global

        //protected const string version = "v5.2";

        protected CallService cs = new CallService();
        public int SessionTime;
        protected List<Customer> customers = new List<Customer>();

        protected static Thread worker;

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(ServiceCenter));

        #endregion

        #region Controls

        protected static Control multipleCustomers;
        protected Control salesOrderHeader;
        protected Control salesOrderDetail;
        protected Control salesReturnOrderHeader;
        protected Control salesReturnOrderDetails;
        protected Control customerInfo;
        protected static Control customerInfoTable;
        #endregion

        #region SendService

        protected SendService nonStaticService = new SendService();
        protected static SendService StaticService = new SendService();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);

            try
            {
                if (!this.Page.User.Identity.IsAuthenticated || Session["ActiveUser"] == null)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                else
                {
                    if (Session["ActiveUser"] != null)
                    {
                        User u = (User)Session["ActiveUser"];
                        SessionTime = u.SessionTimeout; // Initiates user session timer
                    }
                }

                if (IsPostBack)
                {
                    customerInfoTable = new Control();
                    customerInfo = new Control();
                    salesOrderHeader = new Control();
                    salesOrderDetail = new Control();
                    salesReturnOrderHeader = new Control();
                    salesReturnOrderDetails = new Control();
                    customers = new List<Customer>();

                    if (Session["SearchValue"] != null && Session["SearchSelection"] != null && Session["UserInteraction"] != null)
                    {
                        Session["UserInteraction"] = null;

                        if (Session["NoUserInteraction"] == null)
                        {
                            Session["NoUserInteraction"] = null;

                            if (Session["CustomerList"] != null)
                            {
                                customers = (List<Customer>)Session["CustomerList"];
                            }
                            PopulateData();
                        }
                        else
                        {
                            Session["NoUserInteraction"] = null;
                            string searchValue = Convert.ToString(Session["SearchValue"]);

                            if (searchValue == "ORDER CANCELLED")
                            {
                                txtSearchBox.Text = string.Empty;
                                Session["NoUserInteraction"] = null;
                                PopulateData();
                            }
                            else
                            {
                                int searchSelection = Convert.ToInt32(Session["SearchSelection"]);
                                ConnectToService(searchValue, searchSelection);
                            }
                        }
                    }

                    if (Session["ActiveCustomer"] != null)
                    {
                        customers = new List<Customer>
                {
                    (Customer)Session["ActiveCustomer"]
                };

                        customerInfoTable = LoadControl("UserControls/MainTables/CustomerInfoTable.ascx");
                        customerInfoTable.ID = "Customer_Info_Table";
                        ((CustomerInfoTable)customerInfoTable).CustomerList = customers;
                        ((CustomerInfoTable)customerInfoTable).CreateCustomerInfo();
                        this.frmOrderDetails.Controls.Add(customerInfoTable);
                        Session["ActiveCustomer"] = null;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    Log.Error(ex.Message, ex);
                    Session["Error"] = ex.Message;

                    if (Request.Url.AbsoluteUri.Contains("Forms"))
                    {
                        Response.Redirect("ErrorForm.aspx");
                    }
                    else
                    {
                        Response.Redirect("Forms/ErrorForm.aspx");
                    }
                }
            }
        }

        #region Functions

        protected void RetrieveData()
        {
            try
            {
                string searchValue = txtSearchBox.Text;
                SearchOptions so = SearchOptions.Preset;
                string searchOption = string.Empty;
                int searchSelection = -2;

                if (searchValue != null && !string.IsNullOrWhiteSpace(searchValue))
                {
                    searchOption = DdlSearchOptions.SelectedValue.Replace(" ", "").Replace("-", "").Replace(".", "");
                    searchOption = searchOption.Replace("(ExludesShiptoFilters)", "");
                    Enum.TryParse(searchOption, out so);

                    switch (so)
                    {
                        case SearchOptions.Default:
                            searchSelection = 0;
                            break;

                        case SearchOptions.SearchAll:
                            searchSelection = 1;
                            break;

                        case SearchOptions.ExternalDocumentNo:
                            searchSelection = 2;
                            break;

                        case SearchOptions.TrackingNo:
                            searchSelection = 3;
                            break;

                        case SearchOptions.IMEI:
                            searchSelection = 4;
                            break;

                        case SearchOptions.ShiptoName:
                            searchSelection = 5;
                            break;

                        case SearchOptions.ShiptoAddress:
                            searchSelection = 6;
                            break;

                        case SearchOptions.RMANo:
                            searchSelection = 7;
                            break;

                        case SearchOptions.Preset:
                            searchSelection = 0;
                            break;

                        case SearchOptions.EbayUserID:
                            searchSelection = 8;
                            break;

                        default:
                            searchSelection = 0;
                            break;
                    }

                    Session["SearchValue"] = searchValue;
                    Session["SearchSelection"] = searchSelection;
                    ConnectToService(searchValue, searchSelection);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    if (Session["Error"] == null)
                    {
                        Log.Error(ex.Message, ex);
                        Session["Error"] = ex.Message;
                    }

                    if (Request.Url.AbsoluteUri.Contains("Forms"))
                    {
                        Response.Redirect("ErrorForm.aspx");
                    }
                    else
                    {
                        Response.Redirect("Forms/ErrorForm.aspx");
                    }
                }
            }
        }

        protected void ConnectToService(string searchValue, int searchSelection)
        {
            try
            {
                cs.OpenService(searchValue, searchSelection);
                customerInfo = new Control();
                salesOrderHeader = new Control();
                salesOrderDetail = new Control();
                salesReturnOrderHeader = new Control();
                salesReturnOrderDetails = new Control();
                customers = cs.GetCustomerInfo();
                Session["CustomerList"] = customers;
                PopulateData();
            }
            catch (Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    if (ex.Message.ToUpper().Contains("HAS BEEN CANCELLED"))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "orderCancelled", "alert('" + ex.Message + "');", true);
                    }
                    else
                    {
                        if (Session["Error"] == null)
                        {
                            Log.Error(ex.Message, ex);
                            Session["Error"] = ex.Message;
                        }

                        if (Request.Url.AbsoluteUri.Contains("Forms"))
                        {
                            Response.Redirect("ErrorForm.aspx");
                        }
                        else
                        {
                            Response.Redirect("Forms/ErrorForm.aspx");
                        }
                    }
                }
            }
        }

        protected void PopulateData()
        {
            try
            {
                if(customers.Count > 0)
                {
                    if (customers.Count > 1)
                    {
                        multipleCustomers = LoadControl("UserControls/SingleControls/MultipleCustomers.ascx");
                        this.frmOrderDetails.Controls.Add(multipleCustomers);
                        Session["MultipleCustomers"] = multipleCustomers;
                    }

                    customerInfoTable = LoadControl("UserControls/MainTables/CustomerInfoTable.ascx");
                    customerInfoTable.ID = "Customer_Info_Table";
                    ((CustomerInfoTable)customerInfoTable).CustomerList = customers;
                    ((CustomerInfoTable)customerInfoTable).CreateCustomerInfo();
                    this.frmOrderDetails.Controls.Add(customerInfoTable);

                    StaticService.CustomerList = customers;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "noSerachResults", "alert(No Search Results Found.);", true);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    if (Session["Error"] == null)
                    {
                        Log.Error(ex.Message, ex);
                        Session["Error"] = ex.Message;
                    }

                    if (Request.Url.AbsoluteUri.Contains("Forms"))
                    {
                        Response.Redirect("ErrorForm.aspx");
                    }
                    else
                    {
                        Response.Redirect("Forms/ErrorForm.aspx");
                    }
                }
            }
        }

        #endregion

        #region AjaxWebMethods

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SetActiveCustomer(int custID)
        {
            try
            {
                StaticService.SetActiveCustomer(custID);
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e);
                return e.Message;
            }

            return "success";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string IssueReturnLabel(string rmaNo, string email)
        {
            try
            {
                string sessionID = string.Empty;
                if (HttpContext.Current.Session["ActiveUser"] != null)
                {
                    User u = (User)HttpContext.Current.Session["ActiveUser"];
                    sessionID = u.SessionID;
                }
                else
                {
                    sessionID = "{A0A0A0A0-A0A0-A0A0-A0A0-A0A0A0A0A0A0}";
                }

                worker = new Thread(() =>
                {
                    try
                    {
                        StaticService.IssueReturnLabel(rmaNo, email, sessionID);
                    }
                    catch (Exception workerE)
                    {
                        Log.Error(workerE.Message, workerE);
                    }
                });

                worker.Start();

                HttpContext.Current.Session["NoUserInteraction"] = true;
                HttpContext.Current.Session["UserInteraction"] = true;
            }
            catch (Exception e)
            {
                return "Error - " + e.Message;
            }

            return "success";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string UpdateUserPassword(string currentUser, string newPassword)
        {
            try
            {
                StaticService.UpdateUserPassword(currentUser, newPassword);
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e);
                return "Error - " + e.Message;
            }

            return "Success";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SessionCompleted()
        {
            try
            {
                User u = (User)HttpContext.Current.Session["ActiveUser"];
                StaticService.ResetSession(u.UserID);
                HttpContext.Current.Session["ActiveUser"] = null;
                FormsAuthentication.SignOut();
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e);
                return e.Message;
            }

            return "You session has expired. You will now be redirected to the login page.";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string UserLogout()
        {
            try
            {
                HttpContext.Current.Session.Clear();
                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return "Error - " + ex.Message;
            }

            return "Success";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string IssueRefund(string rmaNo, int zendeskTicketNo)
        {
            try
            {
                string sessionID = string.Empty;
                if (HttpContext.Current.Session["ActiveUser"] != null)
                {
                    User u = (User)HttpContext.Current.Session["ActiveUser"];
                    sessionID = u.SessionID;
                }
                else
                {
                    sessionID = "{A0A0A0A0-A0A0-A0A0-A0A0-A0A0A0A0A0A0}";
                }


                StaticService.IssueRefund(rmaNo, sessionID, zendeskTicketNo);

                HttpContext.Current.Session["NoUserInteraction"] = true;
                HttpContext.Current.Session["UserInteraction"] = true;
            }
            catch (Exception e)
            {
                return "Error - " + e.Message;
            }

            return "Success";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string UpdateZendeskTicket(int currentTicketNo, int updateTicketNo)
        {
            try
            {
                string sessionID = string.Empty;
                if (HttpContext.Current.Session["ActiveUser"] != null)
                {
                    User u = (User)HttpContext.Current.Session["ActiveUser"];
                    sessionID = u.SessionID;
                }
                else
                {
                    sessionID = "{A0A0A0A0-A0A0-A0A0-A0A0-A0A0A0A0A0A0}";
                }

                StaticService.UpdateZendeskTicket(sessionID, currentTicketNo, updateTicketNo);

                HttpContext.Current.Session["NoUserInteraction"] = true;
                HttpContext.Current.Session["UserInteraction"] = true;
            }
            catch (Exception e)
            {
                return "Error - " + e.Message;
            }

            return "Success";
        }

        #endregion

        #region Buttons

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Session["SessionID"] != null)
            {
                User activeUser = new User();

                if (Session["ActiveUser"] != null)
                {
                    activeUser = (User)Session["ActiveUser"];
                }

                Session["ActiveUser"] = activeUser;
                Session["SearchValue"] = null;
                Session["SearchSelection"] = null;
                RetrieveData();
            }
            else
            {
                Session.Clear();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        #endregion
    }
}
