using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
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
        protected Control multipleCustomers;
        protected Control salesOrderHeader;
        protected Control salesOrderDetail;
        protected Control salesReturnOrderHeader;
        protected Control salesReturnOrderDetails;
        protected Control customerInfo;

        protected Control customerInfoTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if(IsPostBack)
           // {
                //if(Session["ActiveCustomer"] != null)
                //{
                //    customerInfoTable = LoadControl("UserControls/MainTables/CustomerInfoTable.ascx");
                //    customerInfoTable.ID = "Customer_Info_Table";
                //    ((CustomerInfoTable)customerInfoTable).CustomerList = (List<Customer>)Session["ActiveCustomer"];
                //    ((CustomerInfoTable)customerInfoTable).CreateCustomerInfo();
                //    this.frmOrderDetails.Controls.Add(customerInfoTable);
                //}
            //}
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(ViewState["Generate"]) != "true")
            {
                Session.Clear();
                RetrieveData();
                ViewState["Generated"] = "true";
            }
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
        }

        public static void SetActiveCustomer()
        {
            //HttpContext.Current.Session["ActiveCustomer"] = selectedCustomer;
            HttpContext.Current.Response.Write("Test");
            //return true;
        }
    }
}