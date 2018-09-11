using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
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
        public bool IsPendingSQApproval { get; set; }

        public StatisticsSalesLine(string docTypeP, string docNoP, string externalDocumentNoP, string itemNoP, int qtyP, string descriptionP, string createdDateP, string reqReturnActionP,
            bool isNotInvAvailableP, bool isOlderThan72HoursP, bool isPendingSQApprovalP, string customerNoP, bool isOlderThan48HoursP, string statusP)
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
        }

        public StatisticsSalesLine()
        {

        }
    }
}