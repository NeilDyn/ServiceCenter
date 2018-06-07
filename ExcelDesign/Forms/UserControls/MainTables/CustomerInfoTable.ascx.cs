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

        public List<Customer> CustomerList { get; set; }

        protected const string customerDetailPath = "../TableHeaders/SingleCustomerTableHeader.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.thcTotalCustomers.Text = CustomerList.Count.ToString();

            if (Session["CustomerInfoTable"] == null)
            {
                Session["CustomerInfoTable"] = this.tblCustomerInfo;
            }
            //CreateCustomerInfo();        
        }

        public void CreateCustomerInfo()
        {
            Session["CustomerCount"] = CustomerList.Count.ToString();
                  
            int count = 1;

            foreach (Customer cust in CustomerList)
            {
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();
                singleCustomerTableHeader = LoadControl(customerDetailPath);
                singleCustomerTableHeader.ID = "Customer " + count.ToString();
                ((SingleCustomerTableHeader)singleCustomerTableHeader).SingleCustomer = cust;
                ((SingleCustomerTableHeader)singleCustomerTableHeader).Count = count;

                tc.Height = new Unit("100%");
                tc.ColumnSpan = this.infoHeaders.Cells.Count;

                tc.Controls.Add(singleCustomerTableHeader);
                tr.Cells.Add(tc);
                this.tblCustomerInfo.Rows.Add(tr);                
                Session["SingleCustomerInfoCell_" + count.ToString()] = tc;
                count++;
            }
        }
    }
}