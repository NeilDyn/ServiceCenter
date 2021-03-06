﻿using System;
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
        public int CustomerCount { get; set; }

        private TableRow salesRow;
        private TableRow returnRow;        
        private TableCell salesCell;
        private TableCell returnCell;

        protected const string salesOrderHeaderPath = "../MainTables/SalesOrderHeaderTable.ascx";
        protected const string returnOrderHeaderPath = "../MainTables/ReturnOrderHeaderTable.ascx";
     
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.tcAddress2.Text = Cust.Address2;
            //this.tcShiptoContact.Text = Cust.ShipToContact;
            //this.tcCity.Text = Cust.City;
            //this.tcZip.Text = Cust.Zip;
            //this.tcState.Text = Cust.State;
            //this.tcCountry.Text = Cust.Country;
            
            PopulateData();

            if(CustomerCount == 1)
            {
                Session["SelectedCustomer"] = Cust;
            }
        }

        public void PopulateData()
        {
            salesRow = new TableRow();
            returnRow = new TableRow();
            salesCell = new TableCell();
            returnCell = new TableCell();

            if (Cust.SalesHeader.Count > 0)
            {
                salesOrderHeader = LoadControl(salesOrderHeaderPath);
                ((SalesOrderHeaderTable)salesOrderHeader).SalesHeaderList = Cust.SalesHeader;
                ((SalesOrderHeaderTable)salesOrderHeader).CustID = CustNo;
                ((SalesOrderHeaderTable)salesOrderHeader).CustomerCount = CustomerCount;
                ((SalesOrderHeaderTable)salesOrderHeader).SalesOrderCount = Cust.SalesHeader.Count;

                salesCell.Width = new Unit("100%");
                salesCell.ColumnSpan = 10;
                salesCell.Controls.Add(salesOrderHeader);
                salesRow.Cells.Add(salesCell);
                this.tblSingleCustomerDetail.Rows.Add(salesRow);
            }

            if (Cust.ReturnHeaders.Count > 0)
            {
                returnOrderHeader = LoadControl(returnOrderHeaderPath);
                ((ReturnOrderHeaderTable)returnOrderHeader).ReturnHeaderList = Cust.ReturnHeaders;
                ((ReturnOrderHeaderTable)returnOrderHeader).CustID = CustNo;
                ((ReturnOrderHeaderTable)returnOrderHeader).CustomerCount = CustomerCount;
                ((ReturnOrderHeaderTable)returnOrderHeader).ReturnOrdersCount = Cust.ReturnHeaders.Count;

                returnCell.Width = new Unit("100%");
                returnCell.ColumnSpan = 10;
                returnCell.Controls.Add(returnOrderHeader);
                returnRow.Cells.Add(returnCell);
                this.tblSingleCustomerDetail.Rows.Add(returnRow);
            }
        }
    }
}