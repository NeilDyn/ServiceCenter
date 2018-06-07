using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls;
using ExcelDesign.Forms.UserControls.CustomerInfo.MainTables;
using ExcelDesign.Forms.UserControls.TableData;
using ExcelDesign.Forms.UserControls.TableHeaders;

namespace ExcelDesign.Forms
{
    public partial class ServiceCenter : System.Web.UI.Page
    {
        protected CallService cs;

        // User Controls
        protected Control multipleCustomers;
        protected Control salesOrderHeader;
        protected Control salesOrderDetail;
        protected Control salesReturnOrderHeader;
        protected Control salesReturnOrderDetails;
        protected Control customerInfo;

        protected Control customerInfoTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(ViewState["Generated"]) == "true")
            {
                //RetrieveData();
                if (IsPostBack)
                {
                    for (int custID = 1; custID < Convert.ToInt32(Session["CustomerCount"]) + 1; custID++)
                    {
                        TableRow tr = new TableRow();
                        TableCell tc = new TableCell();
                        for (int so = 1; so < Convert.ToInt32(Session["SalesOrderCount_" + custID.ToString()]) + 1; so++)
                        {
                            if (Session["SingleSalesOrderTableHeaderDetailCell_CustID" + custID.ToString() + "_" + so.ToString()] != null)
                            {

                                tr.Cells.Add((TableCell)Session["SingleSalesOrderTableHeaderDetailCell_CustID" + custID.ToString() + "_" + so.ToString()]);
                                ((Table)Session["SingleSalesOrderTableHeader" + custID.ToString()]).Rows.Add(tr);

                                tc = new TableCell();
                                tr = new TableRow();
                                tc.Controls.Add(((Table)Session["SingleSalesOrderTableHeader" + custID.ToString()]));
                                tr.Cells.Add(tc);
                                ((Table)Session["SalesOrderHeaderTableCell_CustID" + custID.ToString() + "_" + so.ToString()]).Rows.Add(tr);

                                tr = new TableRow();
                                tr.Cells.Add((TableCell)Session["SalesOrderHeaderTableCell_CustID" + custID.ToString() + "_" + so.ToString()]);
                                ((Table)Session["SalesOrderHeaderTable_" + custID.ToString()]).Rows.Add(tr);

                                tc = new TableCell();
                                tr = new TableRow();
                                tc.Controls.Add((Table)Session["SalesOrderHeaderTable_" + custID.ToString()]);
                                tr.Cells.Add(tc);
                                ((Table)Session["SingleCustomerDetailTableCell_" + custID.ToString()]).Rows.Add(tr);
                            }

                            tr = new TableRow();
                            tr.Cells.Add((TableCell)Session["SingleCustomerDetailTableCell_" + custID.ToString()]);
                            ((Table)Session["SingleCustomerTableDetailTable_" + custID.ToString()]).Rows.Add(tr);

                            if (Session["SingelCustomerTableHeaderCell_" + custID.ToString()] != null)
                            {
                                tr = new TableRow();
                                tr.Cells.Add((TableCell)Session["SingelCustomerTableHeaderCell_" + custID.ToString()]);
                                ((Table)Session["SingleCustomerTableHeader_" + custID.ToString()]).Rows.Add(tr);
                            }

                            tr = new TableRow();
                            tr.Cells.Add((TableCell)Session["SingleCustomerInfoCell_" + custID.ToString()]);
                            ((Table)Session["CustomerInfoTable"]).Rows.Add(tr);
                        }
                    }

                    if (Session["MultipleCustomers"] != null)
                    {
                        this.frmOrderDetails.Controls.Add(((Control)Session["MultipleCustomers"]));
                    }
                    if (Session["CustomerInfoTable"] != null)
                    {
                        this.frmOrderDetails.Controls.Add((Control)Session["CustomerInfoTable"]);
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(ViewState["Generate"]) != "true")
            {
                Session.Clear();
                RetrieveData();
                ViewState["Generated"] = "true";
            }
        }

        protected void RetrieveData()
        {
            string searchValue = txtSearchBox.Text;
            cs = new CallService();

            if (searchValue != null && !string.IsNullOrWhiteSpace(searchValue))
            {
                try
                {
                    cs.OpenService(searchValue);
                    customerInfo = new Control();
                    salesOrderHeader = new Control();
                    salesOrderDetail = new Control();
                    salesReturnOrderHeader = new Control();
                    salesReturnOrderDetails = new Control();
                    PopulateData();
                }
                catch (Exception ex)
                {
                    Session["Error"] = ex.Message;
                    Response.Redirect("ErrorForm.aspx");
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
                }
            }
        }

        protected void PopulateData()
        {
            List<Customer> customers = cs.GetCustomerInfo();
            if (customers.Count > 1)
            {
                multipleCustomers = LoadControl("UserControls/SingleControls/MultipleCustomers.ascx");
                this.frmOrderDetails.Controls.Add(multipleCustomers);
                Session["MultipleCustomers"] = multipleCustomers;
            }

            customerInfoTable = LoadControl("UserControls/MainTables/CustomerInfoTable.ascx");
            customerInfoTable.ID = "Customer_Info_Table";
            ((CustomerInfoTable)customerInfoTable).CustomerList = customers;
            ((CustomerInfoTable)customerInfoTable).CreateCustomerInfo();
            this.frmOrderDetails.Controls.Add(customerInfoTable);

            //Session["CustomerInfoControl"] = customerInfoTable;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("TEST");
        }
    }
}