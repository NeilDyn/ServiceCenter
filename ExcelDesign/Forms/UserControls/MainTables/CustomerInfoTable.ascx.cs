using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.TableHeaders;

namespace ExcelDesign.Forms.UserControls.CustomerInfo.MainTables
{
    public partial class CustomerInfoTable : System.Web.UI.UserControl
    {
        protected Control singleCustomerTableHeader;

        protected const string customerDetailPath = "../TableHeaders/SingleCustomerTableHeader.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CreateCustomerInfo(List<Customer> customerList)
        {
            this.thcTotalCustomers.Text = customerList.Count.ToString();
            int count = 1;
            foreach (Customer cust in customerList)
            {
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();
                singleCustomerTableHeader = LoadControl(customerDetailPath);
                singleCustomerTableHeader.ID = "Customer" + count.ToString();
                ((SingleCustomerTableHeader)singleCustomerTableHeader).AddDetails(cust, count);

                tc.Height = new Unit("100%");
                tc.ColumnSpan = 3;

                tc.Controls.Add(singleCustomerTableHeader);
                tr.Cells.Add(tc);
                this.tblCustomerInfo.Rows.Add(tr);
                count++;
            }          
        }
    }
}