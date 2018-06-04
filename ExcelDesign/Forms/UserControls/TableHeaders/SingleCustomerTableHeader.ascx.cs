using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.TableData;

namespace ExcelDesign.Forms.UserControls.TableHeaders
{
    public partial class SingleCustomerTableHeader : System.Web.UI.UserControl
    {
        protected Control customerDetail;

        protected const string customerDetailPath = "../TableData/SingleCustomerTableDetail.ascx";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void AddDetails(Customer cust, int count)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            this.CustomerSequence.Text = "Customer " + count.ToString();
            this.thcCustomerName.Text = cust.Name;

            customerDetail = LoadControl(customerDetailPath);
            ((SingleCustomerTableDetail)customerDetail).PopulateData(cust);

            tc.Height = new Unit("100%");
            tc.ColumnSpan = 4;
            tc.Controls.Add(customerDetail);
            tr.Cells.Add(tc);
            this.tblSingleCustomerTableHeader.Rows.Add(tr);
        }
    }
}