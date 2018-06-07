using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Customer Cust { get; set; }
        public int CustNo { get; set; }

        protected const string salesOrderHeaderPath = "../MainTables/SalesOrderHeaderTable.ascx";
        protected const string returnOrderHeaderPath = "../MainTables/ReturnOrderHeaderTable.ascx";
     
        protected void Page_Load(object sender, EventArgs e)
        {
            this.tcAddress1.Text = Cust.Address1;
            this.tcAddress2.Text = Cust.Address2;
            this.tcShiptoContact.Text = Cust.ShipToContact;
            this.tcCity.Text = Cust.City;
            this.tcZip.Text = Cust.Zip;
            this.tcState.Text = Cust.Country;

            if (Session["SingleCustomerTableDetailTable_" + CustNo.ToString()] == null)
            {
                Session["SingleCustomerTableDetailTable_" + CustNo.ToString()] = this.tblSingleCustomerDetail;
            }
            PopulateData();        
        }

        public void PopulateData()
        {
            TableRow salesRow = new TableRow();
            TableRow returnRow = new TableRow();
            TableCell salesCell = new TableCell();
            TableCell returnCell = new TableCell();

            

            if (Cust.SalesHeader.Count > 0)
            {
                salesOrderHeader = LoadControl(salesOrderHeaderPath);
                ((SalesOrderHeaderTable)salesOrderHeader).SalesHeaderList = Cust.SalesHeader;
                ((SalesOrderHeaderTable)salesOrderHeader).CustID = CustNo;

                salesCell.Width = new Unit("100%");
                salesCell.ColumnSpan = 6;
                salesCell.Controls.Add(salesOrderHeader);
                salesRow.Cells.Add(salesCell);
                this.tblSingleCustomerDetail.Rows.Add(salesRow);
                Session["SingleCustomerDetailTableCell_" + CustNo.ToString()] = salesCell;
            }

            if (Cust.ReturnHeaders.Count > 0)
            {
                returnOrderHeader = LoadControl(returnOrderHeaderPath);
                ((ReturnOrderHeaderTable)returnOrderHeader).LoadHeader(Cust.ReturnHeaders);

                returnCell.Width = new Unit("100%");
                returnCell.ColumnSpan = 6;
                returnCell.Controls.Add(returnOrderHeader);
                returnRow.Cells.Add(returnCell);
                this.tblSingleCustomerDetail.Rows.Add(returnRow);
            }
        }
    }
}