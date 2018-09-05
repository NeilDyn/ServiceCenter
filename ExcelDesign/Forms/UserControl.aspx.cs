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
    public partial class UserControl : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                User activeUser = null;

                if (Session["ActiveUser"] != null)
                {
                    activeUser = (User)Session["ActiveUser"];
                    PopulateSingleUser(activeUser);
                }
            }
        }

        protected void PopulateSingleUser(User u)
        {
            try
            {
                tcUserID.Text = u.UserID;
                tcPassword.Text = u.Password;
                cbxCreateRMA.Checked = u.CreateRMA;
                cbxCreateReturnLabel.Checked = u.CreateReturnLabel;
                cbxCreateExchangeOrder.Checked = u.CreateExchange;
                cbxAdmin.Checked = u.Admin;
                cbxDeveloper.Checked = u.Developer;
                tcLastPasswordUpdate.Text = u.PasswordLastUpdated;
                tcPasswordExpiryDate.Text = u.PasswordExpiryDate;
                cbxCreatePDARMA.Checked = u.CreatePDARMA;
                cbxCreatePDAExchange.Checked = u.CreatePDAExchange;
                cbxCreatePartialRequest.Checked = u.CreatePartRequest;
                cbxCreatePDAPartRequest.Checked = u.CreatePDAPartRequest;
            }
            catch (Exception e)
            {
                Session["Error"] = e.Message;
                Response.Redirect("ErrorForm.aspx");
            }
        }
    }
}