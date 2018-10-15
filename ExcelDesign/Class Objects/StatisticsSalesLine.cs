using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    /* v7.1 - 3 October 2018 - Neil Jansen
     * Added logic for Is Older than 24 Hours bucket
     */

    /* v7.2 - 15 October 2018 - Neil Jansen
     * Added new property for Customer Refund Permission to determine if the could Auto process from portal
     */ 

    public class StatisticsSalesLine
    {
        public string DocType { get; set; }
        public string DocNo { get; set; }
        public string ExternalDocumentNo { get; set; }
        public string ItemNo { get; set; }   
        public int Qty { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string REQReturnAction { get; set; }
        public string CustomerNumber { get; set; }
        public string Status { get; set; }
        public bool IsNotInvtAvailable { get; set; }
        public bool IsOlderThan72Hours { get; set; }
        public bool IsOlderThan48Hours { get; set; }
        public bool IsOlderThan24Hours { get; set; }
        public bool IsPendingSQApproval { get; set; }
        public bool CustAllowRefund { get; set; }

        public StatisticsSalesLine(string docTypeP, string docNoP, string externalDocumentNoP, string itemNoP, int qtyP, string descriptionP, string createdDateP, string reqReturnActionP,
            bool isNotInvAvailableP, bool isOlderThan72HoursP, bool isPendingSQApprovalP, string customerNoP, bool isOlderThan48HoursP, string statusP, bool isOlderThan24HoursP, bool custAllowRefundP)
        {
            DocType = docTypeP;
            DocNo = docNoP;
            ExternalDocumentNo = externalDocumentNoP;
            ItemNo = itemNoP;
            Qty = qtyP;
            Description = descriptionP;
            CreatedDate = createdDateP;
            REQReturnAction = reqReturnActionP;
            IsNotInvtAvailable = isNotInvAvailableP;
            IsOlderThan72Hours = isOlderThan72HoursP;
            IsPendingSQApproval = isPendingSQApprovalP;
            CustomerNumber = customerNoP;
            IsOlderThan48Hours = isOlderThan48HoursP;
            Status = statusP;
            IsOlderThan24Hours = isOlderThan24HoursP;
            CustAllowRefund = custAllowRefundP;
        }

        public StatisticsSalesLine()
        {

        }
    }
}