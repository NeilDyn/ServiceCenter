using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms
{
    public partial class AdminControl : System.Web.UI.Page
    {
        /* NJ 5 September 2018
         * Updated with User Control Navigation bar.
        */

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated || Session["ActiveUser"] == null)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void BtnSetMode_Click(object sender, EventArgs e)
        {
            Session["DevelopmentState"] = rblModeSelection.SelectedValue;

            Response.Redirect("ServiceCenter.aspx");
        }
    }
}