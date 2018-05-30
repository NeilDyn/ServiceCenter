using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms
{
    public partial class Demo : System.Web.UI.Page
    {
        private Control salesOrderHeader;
        private Control salesOrderDetail;
        private Control customerInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int occurance = 0;
                ViewState["Counter"] = occurance;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            customerInfo = LoadControl("UserControls/CustomerInfo.ascx");
            frmDemo.Controls.Add(customerInfo);

            int count = Convert.ToInt32((ViewState["Counter"]));
            ViewState["Counter"] = count + 1;
            int counter = Convert.ToInt32(ViewState["Counter"]);

            if (counter > 0)
            {
                salesOrderHeader = LoadControl("UserControls/SalesOrderHeader.ascx");
                salesOrderHeader.ID = "SalesOrderHeader";
                frmDemo.Controls.Add(salesOrderHeader);

                for (int i = 0; i < counter; i++)
                {
                    salesOrderDetail = LoadControl("UserControls/SalesOrderDetail.ascx");
                    salesOrderDetail.ID = "SalesOrderDetail" + i.ToString();
                   // ((SalesOrderDetail)salesOrderDetail).PopulateControl(i + 1);
                    
                     salesOrderHeader.Controls.Add(salesOrderDetail);
                }
            }
        }

        protected void ListOfArguments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}