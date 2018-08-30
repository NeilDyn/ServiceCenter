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
        public int CustomerCount { get; set; }

        private TableRow tr;
        private TableCell tc;

        protected const string customerDetailPath = "../TableData/SingleCustomerTableDetail.ascx";

        protected void Page_Load(object sender, EventArgs e)
        {           
            if(CustomerCount == 1)
            {
                this.CustomerSequence.Text = "Customer";
                this.btnExpload.Visible = false;
                this.btnSelectCustomer.Visible = false;
                this.lblSelectActive.Visible = false;
            }
            else
            {
                this.btnExpload.Visible = true;
                this.btnSelectCustomer.Visible = true;
                this.lblSelectActive.Visible = true;
                this.CustomerSequence.Text = "Customer " + Count.ToString();
                this.btnExpload.ID = "btnExpload_" + Count.ToString();
                this.btnSelectCustomer.ID = "btnSelectCustomer_" + Count.ToString();
            }
            
            this.thcCustomerName.Text = SingleCustomer.Name;
            this.tcAddress1.Text = SingleCustomer.Address1;

            this.tcAddress2.Text = SingleCustomer.Address2;
            this.tcShiptoContact.Text = SingleCustomer.ShipToContact;
            this.tcCity.Text = SingleCustomer.City;
            this.tcZip.Text = SingleCustomer.Zip;
            this.tcState.Text = SingleCustomer.State;
            this.tcCountry.Text = SingleCustomer.Country;

            this.trInfo1.ID = "trInfo1_" + Count.ToString();
            this.trInfo2.ID = "trInfo2_" + Count.ToString();
            this.trInfo3.ID = "trInfo3_" + Count.ToString();

            this.ID = "customerHeader_" + Count.ToString();

            LoadData();
        }

        protected void LoadData()
        {
            tr = new TableRow();
            tc = new TableCell();

            customerDetail = LoadControl(customerDetailPath);
            ((SingleCustomerTableDetail)customerDetail).Cust = SingleCustomer;
            ((SingleCustomerTableDetail)customerDetail).CustNo = Count;
            ((SingleCustomerTableDetail)customerDetail).CustomerCount = CustomerCount;

            tc.Height = new Unit("100%");
            tc.ColumnSpan = this.customerColumns.Cells.Count;
            tc.Controls.Add(customerDetail);
            tr.Cells.Add(tc);
            tr.ID = "customerDetails_" + Count.ToString();
            this.tblSingleCustomerTableHeader.Rows.Add(tr);
        }
    }
}