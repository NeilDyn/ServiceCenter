using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;

namespace ExcelDesign.Forms
{
    public partial class ServiceCenter : System.Web.UI.Page
    {
        protected CallService cs;
        protected Control salesOrderDetail;

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
                    cs.callService(searchValue);
                    PopulateCustomerDetails();
                    PopulateOrderDetails();
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

            foreach (SalesHeader header in sh)
            {
                salesOrderDetail = LoadControl("SaledOrderDetail.ascx");
                //((SalesOrderDetail)salesOrderDetail).PopulateControl(sh);
            }
            
        }

        protected void PopulateCustomerDetails()
        {
            Customer c = cs.GetCustomerInfo();
            Control customerInfo = LoadControl("UserControls/CustomerInfo.ascx");
            //((Customer))
            
        }
    }
}