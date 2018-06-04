using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.MainTables;

namespace ExcelDesign.Forms.UserControls.TableData
{
    public partial class SingleCustomerTableDetail : System.Web.UI.UserControl
    {
        protected Control salesOrderHeader;
        protected Control returnOrderHeader;

        protected const string salesOrderHeaderPath = "../MainTables/SalesOrderHeaderTable.ascx";
        protected const string returnOrderHeaderPath = "../MainTables/ReturnOrderHeaderTable.ascx";
     
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateData(Customer cust)
        {
            TableRow salesRow = new TableRow();
            TableRow returnRow = new TableRow();
            TableCell salesCell = new TableCell();
            TableCell returnCell = new TableCell();

            this.tcAddress1.Text = cust.Address1;
            this.tcAddress2.Text = cust.Address2;
            this.tcShiptoContact.Text = cust.ShipToContact;
            this.tcCity.Text = cust.City;
            this.tcZip.Text = cust.Zip;
            this.tcState.Text = cust.Country;

            salesOrderHeader = LoadControl(salesOrderHeaderPath);
            ((SalesOrderHeaderTable)salesOrderHeader).LoadHeader(cust.SalesHeader);

            salesCell.Width = new Unit("100%");
            salesCell.ColumnSpan = 6;
            salesCell.Controls.Add(salesOrderHeader);
            salesRow.Cells.Add(salesCell);
            this.tblSingleCustomerDetail.Rows.Add(salesRow);         

            foreach (ReturnHeader returnHeader in cust.ReturnHeaders)
            {

            }
        }
    }
}