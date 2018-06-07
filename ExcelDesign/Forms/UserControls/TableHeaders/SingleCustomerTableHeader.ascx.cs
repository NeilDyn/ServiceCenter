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
    public partial class SingleCustomerTableHeader : System.Web.UI.UserControl, IPostBackEventHandler
    {
        protected Control customerDetail;
        public Customer SingleCustomer { get; set; }
        public int Count { get; set; }

        private TableCell tc;

        protected const string customerDetailPath = "../TableData/SingleCustomerTableDetail.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.customerDetails.ID = "customerDetails_" + Count.ToString();
            this.btnExpload.ID = "btnExpload_" + Count.ToString();

            this.CustomerSequence.Text = "Customer " + Count.ToString();
            this.thcCustomerName.Text = SingleCustomer.Name;

            if (Session["SingleCustomerTableHeader_" + Count.ToString()] == null)
            {
                Session["SingleCustomerTableHeader_" + Count.ToString()] = this.tblSingleCustomerTableHeader;
            }
            LoadData();
        }

        [WebMethod]
        public void ExpandCustomer()
        {
            customerDetails.Visible = true;
        }

        protected void LoadData()
        {
            //if (btnExpandCurrentCustomer.Text == "+")
            //{
            tc = new TableCell();
            //btnExpandCurrentCustomer.Text = "-";

            customerDetail = LoadControl(customerDetailPath);
            ((SingleCustomerTableDetail)customerDetail).Cust = SingleCustomer;
            ((SingleCustomerTableDetail)customerDetail).CustNo = Count;

            tc.Height = new Unit("100%");
            tc.ColumnSpan = 4;
            tc.Controls.Add(customerDetail);
            //tr.Cells.Add(tc);
            //tr.ID = "Customer_" + Count.ToString();
            this.customerDetails.Cells.Add(tc);
            //this.customerDetails.Visible = false;
            Session["SingelCustomerTableHeaderCell_" + Count.ToString()] = tc;
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            
        }
        //else
        //{
        //   btnExpandCurrentCustomer.Text = "+";

        // this.tblSingleCustomerTableHeader.Rows.Remove(tr);
        //Session["SingelCustomerTableHeaderCell_" + Count.ToString()] = null;
        //}
    }

}