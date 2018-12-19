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
using System.Threading.Tasks;

namespace ExcelDesign.Class_Objects
{
    /* v7.1 - 3 October 2018 - Neil Jansen
     * Added functions for Issue Refund, Cancel Order, Process Exchanges and Partial Refunds
     */

    /* v7.2 - 15 October 2018 - Neil Jansen
    * Updated Process Items function to added parameter type determing if processing replacements or refunds.
    */

    /* v9.2 - 12 December 2018 - Neil Jansen
    * Update CreateReturnOrders and PDA Return functions to pass Zendesk Ticket # Parameter  
    *  
    * 13 December 2018 - Neil Jansen
    * Update CreateExchangeOrder to pass Zendesk Ticket # Parameter
    * Update CreatePartialRequest(Actuall a part request) to pass Zendesk Ticket # Parameter
    * Update PartialRefund to pass Zendesk Ticket # Parameter
    * Update CancelOrder to pass Zendesk Ticket # Parameter
    * Update IssueRefund to pass Zendesk Ticket # Parameter
    */

    /* v9.3 - 19 December 2018 - Neil Jansen
     * Added new Function UpdateZendeskNo
     */

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
            functions.Timeout = 300000;
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

        public CreatedPartialRequest CreatePartRequest(string orderNo, string externalDocumentNo, string lineDetails, string notes,
            string shippingDetails, string email, int zendeskTicketNo)
        {
            CreatedPartialRequest returnPartReq = new CreatedPartialRequest();

            functions.CreatePartRequest(orderNo, externalDocumentNo, ref returnPartReq, SessionID(), lineDetails, notes, 
                shippingDetails, email, zendeskTicketNo);

            return returnPartReq;
        }

        public ReturnOrder CreateReturnOrder(string orderNo, string externalDocumentNo, string returnReason, string notes,
            bool includeResource, bool printRMA, bool createLabel, string email, string lineValues, bool update, string returnTrackingNo,
            string shippingDetails, string imeiNo, int zendeskTicketNo)
        {
            ReturnOrder returnRMA = new ReturnOrder();

            /* v9.2 - 12 December 2018 - Neil Jansen */
            functions.CreateReturnOrder(orderNo, externalDocumentNo, returnReason, notes, includeResource, printRMA, createLabel,
                email, lineValues, ref returnRMA, update, SessionID(), returnTrackingNo, shippingDetails, imeiNo, zendeskTicketNo);

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

        public void IssueReturnLabel(string rmaNo, string email, string sessionID)
        {
            functions.IssueReturnLabelAsync(rmaNo, email, sessionID);
        }

        public CreatedExchangeOrder CreateExchange(string rmaNo, string externalDocNo, string lineValues, int zendeskTicketNo)
        {
            CreatedExchangeOrder eo = new CreatedExchangeOrder();

            functions.CreateExchangeOrder(rmaNo, ref eo, externalDocNo, SessionID(), lineValues, zendeskTicketNo);

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

        public Statistics GetStatisticsInfo()
        {
            Statistics stats = new Statistics();

            functions.GetStatistics(SessionID(), ref stats);

            return stats;
        }

        public AboutObjects GetObjectInfo()
        {
            AboutObjects ao = new AboutObjects();

            functions.PullAboutDetails(SessionID(), ref ao);

            return ao;
        }

        public void IssueRefund(string rmaNo, string sessionID, int zendeskTicketNo)
        {
            functions.CreateRefund(rmaNo, sessionID, zendeskTicketNo);
        }

        public void CancelOrder(string orderNo, string docNo, string lineValues, int zendeskTicketNo)
        {
            functions.CancelOrder(orderNo, docNo, lineValues, SessionID(), zendeskTicketNo);
        }

        public void ProcessItems(string rmaList, string sessionID, string type)
        {
            if (type == "Replacement")
            {
                functions.ProcessReplacements(rmaList, sessionID);
            }
            else
            {
                functions.ProcessRefunds(rmaList, sessionID);
            }
        }

        public void PartialRefund(string orderNo, string docNo, string lineValues, int zendeskTicketNo)
        {
            functions.PartialRefund(orderNo, docNo, lineValues, SessionID(), zendeskTicketNo);
        }

        public void UpdateREQReturnAction(string rmaList, string sessionID)
        {
            functions.UpdateREQReturnAction(rmaList, sessionID);
        }

        public SuggestSimilarItem GetSuggestSimilarItems(string itemNo, int suggestionOption)
        {
            SuggestSimilarItem ssi = new SuggestSimilarItem();
            functions.ViewSimilarItem(SessionID(), itemNo, suggestionOption, ref ssi);
            return ssi;
        }

        public void ProcessSuggestSimilarItems(string suggestionList, string sessionID)
        {
            functions.UpdateRMAItemNo(sessionID, suggestionList);
        }

        public void UpdateZendeskTicket(string sessionID, int currentTicketNo, int updateTicketNo)
        {
            functions.UpdateZendeskTicket(sessionID, currentTicketNo, updateTicketNo);
        }
    }
}
 