﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.TableHeaders;

namespace ExcelDesign.Forms.UserControls.MainTables
{
    public partial class SalesOrderHeaderTable : System.Web.UI.UserControl
    {
        protected Control singleSalesOrderHeader;
        public List<SalesHeader> SalesHeaderList { get; set; }
        public int CustID { get; set; }
        public int CustomerCount { get; set; }
        public int SalesOrderCount { get; set; }

        private TableRow tr;
        private TableCell tc;

        protected const string singleSalesOrderHeaderPath = "../TableHeaders/SingleSalesOrderTableHeader.ascx";
        public int salesCount = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.thcTotalOrders.Text = SalesHeaderList.Count.ToString();
            
            if(SalesOrderCount == 1)
            {
                this.btnExpandOrder.Visible = false;
            }
            else
            {
                this.btnExpandOrder.Visible = true;
                this.btnExpandOrder.ID = "btnExpandOrder_" + CustID.ToString();
            }

            if (CustomerCount == 1)
            {
                Session["SalesHeaders"] = SalesHeaderList;
            }
            else
            {
                Session["SalesHeaders"] = null;
            }

            LoadData();
        }

        protected void LoadData()
        {
            foreach (SalesHeader salesHeader in SalesHeaderList)
            {
                tr = new TableRow();
                tc = new TableCell();
                singleSalesOrderHeader = LoadControl(singleSalesOrderHeaderPath);
                singleSalesOrderHeader.ID = "singleSalesOrderHeader_" + salesCount.ToString();

                ((SingleSalesOrderTableHeader)singleSalesOrderHeader).Header = salesHeader;
                ((SingleSalesOrderTableHeader)singleSalesOrderHeader).HeadCount = salesCount;
                ((SingleSalesOrderTableHeader)singleSalesOrderHeader).CustID = CustID;
                ((SingleSalesOrderTableHeader)singleSalesOrderHeader).CustomerCount = CustomerCount;
                ((SingleSalesOrderTableHeader)singleSalesOrderHeader).SalesOrderCount = SalesOrderCount;

                tc.Height = new Unit("100%");
                tc.ColumnSpan = 4;
                tc.Controls.Add(singleSalesOrderHeader);
                tr.Cells.Add(tc);
                tr.ID = "salesOrderDetailHeader_" + CustID.ToString() + "_" + salesCount.ToString();
                this.tblSalesOrderHeader.Rows.Add(tr);
                salesCount++;
            }
        }
    }
}