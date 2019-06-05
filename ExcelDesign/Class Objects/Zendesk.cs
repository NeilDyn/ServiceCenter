using ExcelDesign.ZendeskAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using ZendeskApi.Contracts.Models;

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
        public long RequesterID { get; set; }
        public string SubmitterID { get; set; }

        private ZendeskHelper helper = new ZendeskHelper();

        public Zendesk()
        {

        }

        public Zendesk(string ticketNoP, DateTime? createdDateP, DateTime? updatedDateP, string subjectP, string statusP, string priorityP, bool fromNAVP,
            string fromEmailAddressP, string fromEmailNameP, string toEmailAddressP, string toEmailNameP, long requesterIDP)
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
            RequesterID = requesterIDP;
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
            long.TryParse(TicketNo, out long updateTicketNo);
            bool validURL = amazonBucketURL == string.Empty ? false : true;

            if (updateTicketNo != 0)
            {
                if (pdf64String != string.Empty)
                {
                    byte[] buffer = Convert.FromBase64String(pdf64String);
                    using (MemoryStream stream = new MemoryStream(buffer))
                    {
                        using (MemoryFile file = new MemoryFile(stream, "application/pdf", rmaFileName + ".pdf"))
                        {
                            helper.UpdateZendeskTicketWithPDFFile(updateTicketNo, file, rmaFileName, amazonBucketURL, true, validURL);
                        }
                    }
                }
                else
                {
                    helper.UpdateZendeskTicketWithPDFFile(updateTicketNo, null, rmaFileName, amazonBucketURL, false, validURL);
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
            bool validURL = amazonBucketURL == string.Empty ? false : true;

            if (pdf64String != string.Empty)
            {
                byte[] buffer = Convert.FromBase64String(pdf64String);
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    using (MemoryFile file = new MemoryFile(stream, "application/pdf", rmaFileName + ".pdf"))
                    {
                        newZendeskTicket = helper.CreateNewZendeskTicketWithPDFFile(file, rmaFileName, amazonBucketURL, emailTo, customerName, true, extDocNo, validURL);
                    }
                }
            }
            else
            {
                newZendeskTicket = helper.CreateNewZendeskTicketWithPDFFile(null, rmaFileName, amazonBucketURL, emailTo, customerName, false, extDocNo, validURL);
            }

            return newZendeskTicket;
        }

        public void DownloadRMAPDFManually(string pdf64String, string rmaFileName)
        {
            if (pdf64String != string.Empty)
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
            return helper.VerifyZendeskTicket(zendeskTicket);
        }

        public void GetRequester()
        {
            TicketRequester requester = new TicketRequester();
            requester = helper.GetRequester(RequesterID);
            FromEmailAddress = requester.Email;
            FromEmailName = requester.Name;
        }

        public string ShortenURL()
        {
            string username = string.Empty;
            string accessToken = string.Empty;
            string url = string.Empty;

            string shortenedURL = string.Empty;

            

            

            StringBuilder bitlyQuery = new StringBuilder(url);
            bitlyQuery.Append("shorten?");
            bitlyQuery.Append("&format=txt");
            bitlyQuery.Append("&longUrl=");
            bitlyQuery.Append(HttpUtility.UrlEncode(url));
            bitlyQuery.Append("&login=");
            bitlyQuery.Append(HttpUtility.UrlEncode(username));
            //bitlyQuery.Append("&apiKey=");
            //bitlyQuery.Append(HttpUtility.UrlEncode(accessToken));

            HttpWebRequest request = WebRequest.Create(bitlyQuery.ToString()) as HttpWebRequest;
            request.Headers.Add("Authorization", "Bearer " + HttpUtility.UrlEncode(accessToken));

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ServicePoint.Expect100Continue = false;
            request.ContentLength = 0;


            WebResponse webResponse = request.GetResponse();
            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                shortenedURL = reader.ReadLine();
            }

            return shortenedURL;
        }
    }
}