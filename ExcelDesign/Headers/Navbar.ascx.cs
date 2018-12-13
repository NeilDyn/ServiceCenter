using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Headers
{
    public partial class Navbar : System.Web.UI.UserControl
    {
        /* NJ 5 September 2018
         * Created top navigation bar as it's own User Control to add to multiple pages and allow for easier and prettier navigation
        */

        protected const string version = "v9.2";

        protected void Page_Load(object sender, EventArgs e)
        {
            versionList.InnerText = version;

            if (Session["ActiveUser"] != null)
            {
                User u = (User)Session["ActiveUser"];
                currentUser.InnerText = "Welcome " + u.UserID + "!";
                applicationType.InnerText = ConfigurationManager.AppSettings["mode"].ToString(); // Displays the current database the portal is connected to

                if (u.Admin)
                {
                    adminPanel.Visible = true;
                }
                else
                {
                    adminPanel.Visible = false;
                }

                if(u.Admin || u.Developer)
                {
                    aboutPage.Visible = true;
                }
                else
                {
                    aboutPage.Visible = false;
                }
            }
        }
    }
}