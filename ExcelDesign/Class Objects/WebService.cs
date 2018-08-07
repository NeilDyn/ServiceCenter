using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.ServiceFunctions;
using System.Net;
using ExcelDesign.Class_Objects.Enums;
using System.Configuration;

namespace ExcelDesign.Class_Objects
{
    public class WebService
    { 
        private readonly string functionsURL = "Codeunit/Functions";
        private static NetworkCredential credentials;

        protected Functions functions = new Functions();

        public WebService()
        {
            try
            {
                InitializeConnection();
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["Error"] = e.Message;
                HttpContext.Current.Response.Redirect("ErrorForm.aspx");
            }
        }

        public WebService(string usernameP, string passwordP)
        {

        }

        private void InitializeConnection()
        {
            string username = ConfigurationManager.AppSettings["webServiceUserName"].ToString();
            string password = ConfigurationManager.AppSettings["webServicePassword"].ToString();
            string domain = ConfigurationManager.AppSettings["webServiceDomain"].ToString();

            string baseURL = string.Empty;

            string mode = ConfigurationManager.AppSettings["mode"].ToString();

            if ((mode != null) && (mode == "Production"))
            {
                baseURL = "http://jeg-psql1:7047/DynamicsNAV/WS/JEG_SONS,%20Inc/";
            }
            else if(((mode != null) && (mode == "Development")))
            {
                baseURL = "http://jeg-svr2.jeg.local:7058/DynamicsNAV/WS/JEG_SONS,%20Inc/";
            }

            credentials = new NetworkCredential(username, password, domain);
            functions.Url = baseURL + functionsURL;
            functions.Credentials = credentials;
        }

        public SearchResults FindOrder(string searchNo, int searchOption)
        {
            SearchResults results = new SearchResults();
   
            functions.SearchDetermineNoType(SessionID(), searchNo, ref results, searchOption);
            return results;
        }

        public string InitiateAction(string orderNo, ActionType actionType) 
        {
            //string result;
            //result = functions.InitiateUserAction(SessionID(), orderNo, (int)actionType);
            return null;
        }

        protected string SessionID()
        {
            if (HttpContext.Current.Session["ActiveUser"] != null)
            {
                User u = (User)HttpContext.Current.Session["ActiveUser"];
                return u.SessionID;
            }
            else
            {
                return "{A0A0A0A0-A0A0-A0A0-A0A0-A0A0A0A0A0A0}";
            }
        }

        public ReturnOrder CreateReturnOrder(string orderNo, string externalDocumentNo, string returnReason, string notes,
            bool includeResource, bool printRMA, bool createLabel, string email, string lineValues, bool update, string returnTrackingNo)
        {
            ReturnOrder returnRMA = new ReturnOrder();

            functions.CreateReturnOrder(orderNo, externalDocumentNo, returnReason, notes, includeResource, printRMA, createLabel,
                email, lineValues, ref returnRMA, update, SessionID(), returnTrackingNo);

            return returnRMA;
        }

        public string DeleteRMA(string rmaNo)
        {
            return functions.DeleteReturnOrder(rmaNo, SessionID());
        }

        public User UserLogin(string userName, string password)
        {
            UserSetup us = new UserSetup();

            functions.LogIn(userName, password, ref us);

            User login = new User(us);

            return login;
        }

        public void IssueReturnLabel(string rmaNo, string email)
        {
            functions.IssueReturnLabel(rmaNo, email, SessionID());
        }

        public CreatedExchangeOrder CreateExchange(string rmaNo, string externalDocNo, string lineValues)
        {
            CreatedExchangeOrder eo = new CreatedExchangeOrder();

            functions.CreateExchangeOrder(rmaNo, ref eo, externalDocNo, SessionID(), lineValues);

            return eo;
        }

        public void UpdateUserPassword(string currentUser, string password)
        {
            functions.UpdateUserPassword(currentUser, password, SessionID());
        }

        public void ResetSession(string userID)
        {
            functions.ResetSession(userID);
        }
    }
}