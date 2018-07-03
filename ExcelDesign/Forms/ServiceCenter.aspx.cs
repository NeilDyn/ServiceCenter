using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Class_Objects.Documents;
using ExcelDesign.Forms.UserControls;
using ExcelDesign.Forms.UserControls.CustomerInfo.MainTables;
using ExcelDesign.Forms.UserControls.TableData;
using ExcelDesign.Forms.UserControls.TableHeaders;

namespace ExcelDesign.Forms
{
    public partial class ServiceCenter : System.Web.UI.Page
    {
        protected CallService cs;

        // User Controls
        protected static Control multipleCustomers;
        protected Control salesOrderHeader;
        protected Control salesOrderDetail;
        protected Control salesReturnOrderHeader;
        protected Control salesReturnOrderDetails;
        protected Control customerInfo;

        protected static Control customerInfoTable;

        protected List<Customer> customers = new List<Customer>();
        protected AjaxControl ajaxService = new AjaxControl();

        protected SendService nonStaticService = new SendService();
        protected static SendService StaticService = new SendService();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);

            if (IsPostBack)
            {
                if (Session["ActiveCustomer"] != null)
                {
                    customers = new List<Customer>();
                    StaticService.CustomerList = customers;
                    customers.Add((Customer)Session["ActiveCustomer"]);
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
            Session.Clear();
            RetrieveData();
        }

        protected void RetrieveData()
        {
            string searchValue = txtSearchBox.Text;
            cs = new CallService();

            if (searchValue != null && !string.IsNullOrWhiteSpace(searchValue))
            {
                try
                {
                    cs.OpenService(searchValue);
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
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
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