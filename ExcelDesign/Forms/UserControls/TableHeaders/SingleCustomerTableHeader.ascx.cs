using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.TableData;

namespace ExcelDesign.Forms.UserControls.TableHeaders
{
    public partial class SingleCustomerTableHeader : System.Web.UI.UserControl
    {
        protected Control customerDetail;
        public Customer SingleCustomer { get; set; }
        public int Count { get; set; }

        private TableRow tr;
        private TableCell tc;

        protected const string customerDetailPath = "../TableData/SingleCustomerTableDetail.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnExpandCurrentCustomer.ID = "Customer_" + Count.ToString();
            this.btnExpandCurrentCustomer.Click += new EventHandler(ExpandCustomer);

            this.CustomerSequence.Text = "Customer " + Count.ToString();
            this.thcCustomerName.Text = SingleCustomer.Name;          

            if (Session["SingleCustomerTableHeader_" + Count.ToString()] == null)
            {
                Session["SingleCustomerTableHeader_" + Count.ToString()] = this.tblSingleCustomerTableHeader;
            }
        }

        protected void ExpandCustomer(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void LoadData()
        {
            if (btnExpandCurrentCustomer.Text == "+")
            {
                tr = new TableRow();
                tc = new TableCell();
                btnExpandCurrentCustomer.Text = "-";

                customerDetail = LoadControl(customerDetailPath);
                ((SingleCustomerTableDetail)customerDetail).Cust = SingleCustomer;
                ((SingleCustomerTableDetail)customerDetail).CustNo = Count;

                tc.Height = new Unit("100%");
                tc.ColumnSpan = 4;
                tc.Controls.Add(customerDetail);
                tr.Cells.Add(tc);
                tr.ID = "Customer_" + Count.ToString();
                this.tblSingleCustomerTableHeader.Rows.Add(tr);
                Session["SingelCustomerTableHeaderCell_" + Count.ToString()] = tc;
            }
            else
            {
                btnExpandCurrentCustomer.Text = "+";

                this.tblSingleCustomerTableHeader.Rows.Remove(tr);
                Session["SingelCustomerTableHeaderCell_" + Count.ToString()] = null;
            }
        }
    }
}