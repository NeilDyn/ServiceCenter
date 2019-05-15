using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ExcelDesign.Forms
{
    public partial class ErrorForm : System.Web.UI.Page
    {
        protected Control test;

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.errorMessage.InnerText = Request.Cookies["Error"].Value;
            //Response.Cookies["Error"].Value = null;

            this.errorMessage.InnerText = Session["Error"].ToString();
            Session["Error"] = null;
        }          
    }
}