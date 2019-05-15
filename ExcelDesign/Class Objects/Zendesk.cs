using ExcelDesign.ZendeskAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    /* v10 - 18 March 2019 - Neil Jansen
     * Added Recipient property
     */

    public class Zendesk
    {
        public string TicketNo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public bool FromNAV { get; set; }
        public string FromEmailAddress { get; set; }
        public string FromEmailName { get; set; }
        public string ToEmailsAddress { get; set; }
        public string ToEmailsName { get; set; }

        public Zendesk()
        {

        }

        public Zendesk(string ticketNoP, DateTime? createdDateP, DateTime? updatedDateP, string subjectP, string statusP, string priorityP, bool fromNAVP, 
            string fromEmailAddressP, string fromEmailNameP, string toEmailAddressP, string toEmailNameP)
        {
            TicketNo = ticketNoP;
            CreatedDate = createdDateP;
            UpdatedDate = updatedDateP;
            Subject = subjectP;
            Status = statusP;
            Priority = priorityP;
            FromNAV = fromNAVP;
            FromEmailAddress = fromEmailAddressP;
            FromEmailName = FromEmailName;
            ToEmailsAddress = toEmailAddressP;
            ToEmailsName = toEmailNameP;
        }

        public Zendesk(string ticketNoP, bool fromNAVP)
        {
            TicketNo = ticketNoP;
            FromNAV = fromNAVP;
        }

        public string TicketLink()
        {                       
            return TicketNo == "0" ? TicketNo : String.Format("<a href = 'https://jegsons.zendesk.com/agent/tickets/" + TicketNo + "' target = '_blank' > #" + TicketNo + " </ a > ");
        }

        public void UpdateZendeskTicketWithPDFFile(string pdf64String, string rmaFileName, string amazonBucketURL)
        {
            ZendeskHelper helper = new ZendeskHelper();
            long.TryParse(TicketNo, out long updateTicketNo);

            if (updateTicketNo != 0)
            {
                if (pdf64String != string.Empty)
                {
                    byte[] buffer = Convert.FromBase64String(pdf64String);
                    using (MemoryStream stream = new MemoryStream(buffer))
                    {
                        using (MemoryFile file = new MemoryFile(stream, "application/pdf", rmaFileName + ".pdf"))
                        {
                            helper.UpdateZendeskTicketWithPDFFile(updateTicketNo, file, rmaFileName, amazonBucketURL, true);
                        }
                    }
                }
                else
                {
                    helper.UpdateZendeskTicketWithPDFFile(updateTicketNo, null, rmaFileName, amazonBucketURL, false);
                }
            }
            else
            {
                throw new Exception("Invalid Zendesk Ticket No. attempting to update"); // prevent invalid tickets from being update due to parsing error
            }
        }

        public long? CreateNewZendeskTicketWithPDFFile(string pdf64String, string rmaFileName, string amazonBucketURL, string emailTo, string customerName, string extDocNo)
        {
            long? newZendeskTicket = 0;
            ZendeskHelper helper = new ZendeskHelper();

            if (pdf64String != string.Empty)
            {
                byte[] buffer = Convert.FromBase64String(pdf64String);
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    using (MemoryFile file = new MemoryFile(stream, "application/pdf", rmaFileName + ".pdf"))
                    {
                        newZendeskTicket = helper.CreateNewZendeskTicketWithPDFFile(file, rmaFileName, amazonBucketURL, emailTo, customerName, true, extDocNo);
                    }
                }
            }
            else
            {
                newZendeskTicket = helper.CreateNewZendeskTicketWithPDFFile(null, rmaFileName, amazonBucketURL, emailTo, customerName, false, extDocNo);
            }

            return newZendeskTicket;
        }

        public void DownloadRMAPDFManually(string pdf64String, string rmaFileName)
        {
            if(pdf64String != string.Empty)
            {
                byte[] buffer = Convert.FromBase64String(pdf64String);
                MemoryStream stream = new MemoryStream(buffer);

                using (MemoryFile file = new MemoryFile(stream, "application/pdf", rmaFileName + ".pdf"))
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + file.FileName + "\";");
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
        }

        public Zendesk VerifyInsertedZendeskTicket(long zendeskTicket)
        {
            ZendeskHelper helper = new ZendeskHelper();

            return helper.VerifyZendeskTicket(zendeskTicket);
        }
    }
}