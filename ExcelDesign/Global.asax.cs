using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ExcelDesign
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //StringBuilder message = new StringBuilder();
            //if(Server != null)
            //{
            //    Exception ex;
            //    //for (ex = Server.GetLastError(); ex != null; ex = ex.InnerException)
            //    //{
            //    //    message.AppendFormat("{0}: {1}{2}",
            //    //                          ex.GetType().FullName,
            //    //                          ex.Message,
            //    //                          ex.StackTrace);
            //    //}

            //    //message.Append(Server.GetLastError().Message.ToString());

            //    message.Append(Server.GetLastError().

            //    Response.Cookies["Error"].Value = message.ToString();
            //    //Session["Error"] = message.ToString();

            //    if (Request.Url.AbsoluteUri.Contains("Forms"))
            //    {
            //        Response.Redirect("ErrorForm.aspx");
            //    }
            //    else
            //    {
            //        Response.Redirect("Forms/ErrorForm.aspx");
            //    }
            //}
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}