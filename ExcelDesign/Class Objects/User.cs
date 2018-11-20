using ExcelDesign.ServiceFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    /* v7.1 - 3 October 2018 - Neil Jansen
     * Added properties for new permissions for Cancallations, Refunds and Partial Refunds
     * Added RefundTier for refund option selection
     */

    public class User
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string SessionID { get; set; }
        public bool CreateRMA { get; set; }
        public bool CreateReturnLabel { get; set; }
        public bool CreateExchange { get; set; }
        public bool CreatePartRequest { get; set; }
        public bool CanIssueRefund { get; set; }
        public bool CanPartialRefund { get; set; }
        public bool CreatePDARMA { get; set; }
        public bool CreatePDAExchange { get; set; }
        public bool CreatePDAPartRequest { get; set; }
        public bool CanIssuePDARefund { get; set; }
        public bool CanCancelOrder { get; set; }
        public bool CanCancelPDAOrder { get; set; }
        public bool CanPartialRefundPDA { get; set; }
        public bool Admin { get; set; }
        public bool Developer { get; set; }
        public bool Supervisor { get; set; }
        public string PasswordLastUpdated { get; set; }
        public string PasswordExpiryDate { get; set; }
        public int SessionTimeout { get; set; }
        public string RefundTier { get; set; }

        public User(UserSetup us)
        {
            UserID = us.User[0].UserID;

            for (int i = 0; i < us.User[0].Password.Length; i++)
            {
                Password += "*";
            }

            SessionID = us.User[0].SessionID;

            CreateRMA = us.User[0].CreateRMA.ToUpper() == "YES" ? true : false;               
            CreatePDARMA = us.User[0].CreatePDARma.ToUpper() == "YES" ? true : false;

            CreateReturnLabel = us.User[0].CreateRetLabel.ToUpper() == "YES" ? true : false;

            CreateExchange = us.User[0].CreateExchange.ToUpper() == "YES" ? true : false;
            CreatePDAExchange = us.User[0].CreatePDAExchange.ToUpper() == "YES" ? true : false;
            
            CreatePartRequest = us.User[0].CreatePartRequest.ToUpper() == "YES" ? true : false;
            CreatePDAPartRequest = us.User[0].CreatePDAPartRequest.ToUpper() == "YES" ? true : false;

            CanIssueRefund = us.User[0].IssueRefund.ToUpper() == "YES" ? true : false;
            CanIssuePDARefund = us.User[0].IssuePDARefund.ToUpper() == "YES" ? true : false;

            CanCancelOrder = us.User[0].CancelOrder.ToUpper() == "YES" ? true : false;
            CanCancelPDAOrder = us.User[0].CancelPDAOrder.ToUpper() == "YES" ? true : false;

            CanPartialRefund = us.User[0].CanPartialRefund.ToUpper() == "YES" ? true : false;
            CanPartialRefundPDA = us.User[0].CanPartialRefundPDA.ToUpper() == "YES" ? true : false;

            Supervisor =  us.User[0].Supervisor.ToUpper() == "YES" ? true : false;

            Admin = us.User[0].Admin.ToUpper() == "YES" ? true : false;
            Developer = us.User[0].Developer.ToUpper() == "YES" ? true : false;
            Supervisor = us.User[0].Supervisor.ToUpper() == "YES" ? true : false;

            PasswordLastUpdated = us.User[0].PasswordLastUpdated;
            PasswordExpiryDate = us.User[0].PasswordExpiryDate;

            SessionTimeout = us.User[0].SessionTimeout;

            RefundTier = us.User[0].PartialRefundTier;
        }

        public User()
        {

        }
    }
}