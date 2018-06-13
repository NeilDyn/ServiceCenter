using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.ServiceFunctions;
using System.Net;
using ExcelDesign.Class_Objects.Enums;

namespace ExcelDesign.Class_Objects
{
    public class WebService
    {
        private readonly string baseURL = "http://jeg-svr2.jeg.local:7058/DynamicsNAV/WS/JEG_SONS,%20Inc/";
        private readonly string functionsURL = "Codeunit/Functions";
        private static NetworkCredential credentials;

        protected string username = "nchristodoulou";
        protected string password = "JEGnewedi2018`";
        protected string domain = "JEG";

        protected Functions functions = new Functions();


        public WebService()
        {
            try
            {
                InitializeConnection();
            }
            catch (Exception e)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + e.Message + "');", true);
            }
        }

        public WebService(string usernameP, string passwordP)
        {

        }

        private void InitializeConnection()
        {
            credentials = new NetworkCredential(username, password, domain);
            functions.Url = baseURL + functionsURL;
            functions.Credentials = credentials;
        }

        public SearchResults FindOrder(string searchNo)
        {
            SearchResults results = new SearchResults();
   
            functions.SearchDetermineNoType(SessionID(), searchNo, ref results);
            return results;
        }

        public string InitiateAction(string orderNo, ActionType actionType) 
        {
            string result;
            //result = functions.InitiateUserAction(SessionID(), orderNo, (int)actionType);
            return null;
        }

        protected string SessionID()
        {
            return "testSesh";
        }
    }
}