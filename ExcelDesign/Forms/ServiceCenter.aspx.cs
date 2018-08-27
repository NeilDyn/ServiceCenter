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
        #region Global

        protected const string version = "v4.7";

        protected CallService cs = new CallService();
        public int SessionTime;
        protected List<Customer> customers = new List<Customer>();

        protected static Thread worker;

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

            if (!this.Page.User.Identity.IsAuthenticated || Session["ActiveUser"] == null)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                versionList.InnerText = version;

                if (Session["ActiveUser"] != null)
                {
                    User u = (User)Session["ActiveUser"];
                    SessionTime = u.SessionTimeout; // Initiates user session timer
                    currentUser.InnerText = "Welcome " + u.UserID + "!";
                    applicationType.InnerText = ConfigurationManager.AppSettings["mode"].ToString(); // Displays the current database the portal is connected to

                    if (u.Admin)
                    {
                        adminPanel.Visible = true;
                    }
                    else
                    {
                        adminPanel.Visible = false;
                    }
                }
            }

            if (IsPostBack)
            {
                if (Session["SearchValue"] != null && Session["SearchSelection"] != null && Session["UserInteraction"] != null)
                {
                    Session["UserInteraction"] = null;

                    if (Session["NoUserInteraction"] == null)
                    {
                        Session["NoUserInteraction"] = null;
                        customerInfo = new Control();
                        salesOrderHeader = new Control();
                        salesOrderDetail = new Control();
                        salesReturnOrderHeader = new Control();
                        salesReturnOrderDetails = new Control();

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
                        int searchSelection = Convert.ToInt32(Session["SearchSelection"]);
                        ConnectToService(searchValue, searchSelection);
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

        #region Functions

        protected void RetrieveData()
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

                    default:
                        searchSelection = 0;
                        break;
                }

                Session["SearchValue"] = searchValue;
                Session["SearchSelection"] = searchSelection;
                ConnectToService(searchValue, searchSelection);
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

        protected void PopulateData()
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
                        //return "Error - " + workerE.Message;
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
                return "Error - " + ex.Message;
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