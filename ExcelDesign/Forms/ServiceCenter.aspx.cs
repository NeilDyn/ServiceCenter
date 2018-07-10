using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
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
        protected CallService cs = new CallService();

        // User Controls
        protected static Control multipleCustomers;
        protected Control salesOrderHeader;
        protected Control salesOrderDetail;
        protected Control salesReturnOrderHeader;
        protected Control salesReturnOrderDetails;
        protected Control customerInfo;

        protected static Control customerInfoTable;

        protected List<Customer> customers = new List<Customer>();

        protected SendService nonStaticService = new SendService();
        protected static SendService StaticService = new SendService();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);

            if (!this.Page.User.Identity.IsAuthenticated || Session["ActiveUser"] == null)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            if (IsPostBack)
            {
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Session["SessionID"] != null)
            {
                User activeUser = new User();

                if (Session["ActiveUser"] != null)
                {
                    activeUser = (User)Session["ActiveUser"];
                }

                Session.Clear();

                Session["ActiveUser"] = activeUser;
                RetrieveData();
            }
            else
            {
                Session.Clear();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void RetrieveData()
        {
            string searchValue = txtSearchBox.Text;
            SearchOptions so = SearchOptions.Preset;
            string searchOption = string.Empty;
            int searchSelection = -2;

            if (searchValue != null && !string.IsNullOrWhiteSpace(searchValue))
            {
                searchOption = DdlSearchOptions.SelectedValue.Replace(" ", "").Replace("-", "");
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

                    case SearchOptions.PONumber:
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

                try
                {
                    cs.OpenService(searchValue, searchSelection);
                    customerInfo = new Control();
                    salesOrderHeader = new Control();
                    salesOrderDetail = new Control();
                    salesReturnOrderHeader = new Control();
                    salesReturnOrderDetails = new Control();
                    PopulateData();
                }
                catch (Exception ex)
                {
                    Session["Error"] = ex.Message;
                    Response.Redirect("ErrorForm.aspx");
                }
            }
        }

        protected void PopulateData()
        {
            List<Customer> customers = cs.GetCustomerInfo();
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

        [WebMethod]
        public static string SetActiveCustomer(int custID)
        {
            try
            {
                StaticService.SetActiveCustomer(custID);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["Error"] = e.Message;
                HttpContext.Current.Response.Redirect("ErrorForm.aspx");
                return e.Message;
            }

            return "1";
        }

        protected void BtnCreatePDF_Click(object sender, EventArgs e)
        {

        }
    }
}