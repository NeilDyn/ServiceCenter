using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms
{
    public partial class AboutPage : System.Web.UI.Page
    {
        protected CallService cs = new CallService();
        protected List<Information> aboutInformation;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated || Session["ActiveUser"] == null)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            if (!IsPostBack)
            {
                try
                {
                    User activeUser = null;

                    if (Session["ActiveUser"] != null)
                    {
                        activeUser = (User)Session["ActiveUser"];

                        if (activeUser.Admin || activeUser.Developer)
                        {
                            aboutInformation = cs.GetBuildInformation();
                            PopulateLines();
                        }
                        else
                        {
                            FormsAuthentication.RedirectToLoginPage();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Session["Error"] = ex.Message;

                    if (Request.Url.AbsoluteUri.Contains("Forms"))
                    {
                        Response.Redirect("ErrorForm.aspx");
                    }
                    else
                    {
                        Response.Redirect("Forms/ErrorForm.aspx");
                    }
                }
            }
        }

        protected void PopulateLines()
        {
            try
            {
                int lineCount = 0;

                foreach (Information line in aboutInformation)
                {
                    lineCount++;

                    TableRow tr = new TableRow();

                    TableCell id = new TableCell();
                    TableCell name = new TableCell();
                    TableCell type = new TableCell();
                    TableCell versionList = new TableCell();
                    TableCell date = new TableCell();
                    TableCell time = new TableCell();
                    TableCell compiled = new TableCell();

                    CheckBox cbxCompiled = new CheckBox
                    {
                        ID = "cbxCompiled",
                        Enabled = false
                    };

                    id.Text = line.ObjectID.ToString();
                    name.Text = line.ObjectName;
                    type.Text = line.ObjectType;
                    versionList.Text = line.ObjectVersionList;
                    date.Text = line.ObjectDate;
                    time.Text = line.ObjectTime;
                    cbxCompiled.Checked = line.ObjectCompiled;
                    compiled.Controls.Add(cbxCompiled);

                    tr.Cells.Add(id);
                    tr.Cells.Add(name);
                    tr.Cells.Add(type);
                    tr.Cells.Add(versionList);
                    tr.Cells.Add(date);
                    tr.Cells.Add(time);
                    tr.Cells.Add(compiled);

                    if (lineCount % 2 == 0)
                    {
                        tblAboutObjects.BackColor = Color.White;
                    }
                    else
                    {
                        tblAboutObjects.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                    }

                    tblAboutObjects.Rows.Add(tr);
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;

                if (Request.Url.AbsoluteUri.Contains("Forms"))
                {
                    Response.Redirect("ErrorForm.aspx");
                }
                else
                {
                    Response.Redirect("Forms/ErrorForm.aspx");
                }
            }
        }
    }
}