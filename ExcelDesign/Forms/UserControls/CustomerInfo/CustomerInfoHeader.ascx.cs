using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls
{
    public partial class CustomerInfoHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateCustomerInfoHeader(int totalCustomers)
        {
            this.lblTotalCustomers.Text = totalCustomers.ToString();
        }
    }
}