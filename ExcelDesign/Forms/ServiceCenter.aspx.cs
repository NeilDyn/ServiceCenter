using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls;

namespace ExcelDesign.Forms
{
    public partial class ServiceCenter : System.Web.UI.Page
    {
        protected CallService cs;

        // User Controls
        protected Control salesOrderHeader;
        protected Control salesOrderDetail;
        protected Control salesReturnOrderHeader;
        protected Control salesReturnOrderDetails;
        protected Control customerInfo;


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
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
                    PopulateCustomerDetails();
                    PopulateOrderDetails();
                    PopulateReturnOrderDetails();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                }
            }
        }

        protected void PopulateOrderDetails()
        {
            int headerCount = 0;
            List<SalesHeader> sh = cs.GetSalesOrders();

            if (sh.Count > 0)
            {
                salesOrderHeader = LoadControl("UserControls/SalesOrderHeader.ascx");
                salesOrderHeader.ID = "SalesOrderHeader";
                ((SalesOrderHeader)salesOrderHeader).Populate(sh.Count);
                this.frmOrderDetails.Controls.Add(salesOrderHeader);

                foreach (SalesHeader header in sh)
                {
                    headerCount++;
                    salesOrderDetail = LoadControl("UserControls/SalesOrderDetail.ascx");
                    salesOrderDetail.ID = "SalesOrderDetail" + headerCount.ToString();
                    ((SalesOrderDetail)salesOrderDetail).PopulateControl(header, headerCount);
                    this.frmOrderDetails.Controls.Add(salesOrderDetail);
                }
            }
        }

        protected void PopulateReturnOrderDetails()
        {
            int headerCount = 0;
            List<ReturnHeader> rh = cs.GetReturnOrders();

            if (rh.Count > 0)
            {
                salesReturnOrderHeader = LoadControl("UserControls/SalesReturnHeader.ascx");
                salesReturnOrderHeader.ID = "SalesReturnHeader";
                ((SalesReturnHeader)salesReturnOrderHeader).Populate(rh.Count);
                this.frmOrderDetails.Controls.Add(salesReturnOrderHeader);

                foreach (ReturnHeader header in rh)
                {
                    headerCount++;
                    salesReturnOrderDetails = LoadControl("UserControls/SalesReturnDetail.ascx");
                    salesReturnOrderDetails.ID = "SalesReturnDetail" + headerCount.ToString();
                    ((SalesReturnDetail)salesReturnOrderDetails).PopulateControl(header, headerCount);
                    this.frmOrderDetails.Controls.Add(salesReturnOrderDetails);
                }
            }
        }

        protected void PopulateCustomerDetails()
        {
            List<Customer> customers = cs.GetCustomerInfo();

            foreach (Customer c in customers)
            {
                customerInfo = LoadControl("UserControls/CustomerInfo.ascx");
                customerInfo.ID = "CustomerInfo";
                ((CustomerInfo)customerInfo).Populate(c);
                this.frmOrderDetails.Controls.Add(customerInfo);
            }         
        }
    }
}