using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected CallService cs = new CallService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UserLogin(object sender, EventArgs e)
        {
            string userID = string.Empty;
            string password = string.Empty;

            try
            {
                User user = cs.LoginUser(txtUserID.Text.Trim(), txtPassword.Text.Trim());
                Session["SessionID"] = user.SessionID;
                Session["ActiveUser"] = user;              
                FormsAuthentication.RedirectFromLoginPage(user.UserID, false);
            }
            catch (Exception ex)
            {
                Session["SessionID"] = null;
                Session["ActiveUser"] = null;
                Session["Error"] = ex.Message;
                Response.Redirect("ErrorForm.aspx");
            }
        }
    }
}