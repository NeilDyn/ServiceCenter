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

        protected static log4net.ILog Log { get; set; } = log4net.LogManager.GetLogger(typeof(CustomerInfoTable));

        protected void Page_Load(object sender, EventArgs e)
        {
            this.thcTotalCustomers.Text = CustomerList.Count.ToString();
        }

        public void CreateCustomerInfo()
        {
            int count = 1;

            try
            {
                foreach (Customer cust in CustomerList)
                {
                    TableRow tr = new TableRow();
                    TableRow lineRow = new TableRow();
                    TableCell tc = new TableCell();
                    TableCell line = new TableCell();
                    singleCustomerTableHeader = LoadControl(customerDetailPath);
                    singleCustomerTableHeader.ID = "Customer " + count.ToString();
                    ((SingleCustomerTableHeader)singleCustomerTableHeader).SingleCustomer = cust;
                    ((SingleCustomerTableHeader)singleCustomerTableHeader).Count = count;
                    ((SingleCustomerTableHeader)singleCustomerTableHeader).CustomerCount = CustomerList.Count;

                    tc.Height = new Unit("100%");
                    tc.ColumnSpan = this.infoHeaders.Cells.Count;

                    line.Height = new Unit("100%");
                    line.ColumnSpan = this.infoHeaders.Cells.Count;
                    line.Text = "<hr class='Seperator'/>";

                    tc.Controls.Add(singleCustomerTableHeader);
                    tr.Cells.Add(tc);
                    lineRow.Cells.Add(line);
                    this.tblCustomerInfo.Rows.Add(tr);
                    this.tblCustomerInfo.Rows.Add(lineRow);

                    count++;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                Session["Error"] = ex.Message;
                Response.Redirect("Forms/ErrorForm.aspx");
            }
        }
    }
}