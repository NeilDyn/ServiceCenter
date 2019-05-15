using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZendeskApi.Client;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Responses;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Client.Resources;
using System.Net;
using ZendeskApi.Contracts.Requests;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExcelDesign.ZendeskAPI
{
    public class ZendeskHelper
    {
        /* Helper Class Library for Zendesk Just Eat API
         * Created By Neil Jansen, Dynetek (Pty) Ltd.
         */

        /* v10 - 18 March 2019 - Neil Jansen
         * Added Recipient to be pulled and stored in class.
         */

        /* v10 - 19 March 2019 - Neil Jansen
         * Added new logic to retrieve emails to and from, from a zendesk ticket.
         * 
         * 25 March 2019
         * Added new function to update zendesk tickets with PDF attachment
         * Added new function to create new Zendesk tickets with PDF Attachment
         * Added functionality to use Current User logged in ZendeskEmail to connec to to the api;
         */

        /* v10.2 - 24 March 2019 - Neil Jansen
         * Updated Ticket Status to 'SOLVED' when updating or creating a Zendesk Ticket
         */

        /* v11 - 15 May 2019 - Neil Jansen
         * Added functionality to allow ZendeskAPI to be switch between Sandbox version and Production version
         * 
         * Update CreateNewZendesk ticket functionality to first create a new ticket on behalf of the customer, 
         * and immediately after to update it with the return shipping infromation.
         * 
         * Updated the create and update zendesk ticket functions to accept and include in their comments the Amazon S3 bucket URL
         * 
         * Developed new function to verify a zendesk ticket. This is used when a user inputs an existing zendesk ticket manually.
         */

        private static string zendeskUsername = string.Empty;
        private static readonly string zendeskAPIToken = "9gryAaOh6JfSIaIWPetf42R4y4J0bPvdG3ta2uX9";
        private static readonly string zendeskSandboxAPIToken = "TlULrYzysqHf7h0tk6pkPg6fbiuhamvBMNaLCBbm";
        private static readonly string zendeskURL = "https://jegsons.zendesk.com";
        private static readonly string zendeskSandboxURL = "https://jegsons1556527099.zendesk.com";
        private static ZendeskDefaultConfiguration configuration;
        private static Uri zendeskURI;
        private static IZendeskClient client;

        //True to use Sandbox version, False for production
        private static readonly bool versionTypeSandbox = true;

        public ZendeskHelper()
        {

        }

        public static List<Zendesk> SearchCustomFieldTickets(string searchCriteria, ref List<long?> listTickets)
        {
            /* Created by Neil Jansen - 5 December 2018
             * Create a list of Zendesk objects through filtering the custom fields in Zendesk
             * 
             * Updated by Neil Jansen - 12 December 2018
             * Changed function name to specify 'CustomFields' - Updated Zendesk list object creation to include new parameters
             */

            ConnectToZendesk(versionTypeSandbox);

            List<Zendesk> zendeskTickets = new List<Zendesk>();
            IListResponse<Ticket> responses = client.Search.Find(new ZendeskQuery<Ticket>().WithCustomFilter("fieldvalue", searchCriteria));
            List<Ticket> responseTickets = (List<Ticket>)responses.Results;

            ZendDeskEmailEntry fromEmails = new ZendDeskEmailEntry();
            ZendDeskEmailEntry toEmails = new ZendDeskEmailEntry();

            foreach (Ticket singleTicket in responseTickets)
            {
                if (!listTickets.Any(ticket => ticket.Equals(singleTicket.Id)))
                {
                    fromEmails = ZendDeskEmailParser.GetEmailList(singleTicket.Via.Source.From);
                    toEmails = ZendDeskEmailParser.GetEmailList(singleTicket.Via.Source.To);

                    zendeskTickets.Add(new Zendesk(singleTicket.Id.ToString(), singleTicket.Created, singleTicket.Updated, singleTicket.Subject,
                        singleTicket.Status.ToString(), singleTicket.Priority, false, fromEmails.Address, fromEmails.Name, toEmails.Address, toEmails.Name));
                    listTickets.Add(singleTicket.Id);
                }
            }

            return zendeskTickets;
        }

        public static List<Zendesk> SearchSubjectTickets(string searchCriteria, ref List<long?> listTickets)
        {
            /* Created by Neil Jansen - 12 December 2018
             * Create a list of Zendesk objects through filtering the Subject field in Zendesk
             */

            ConnectToZendesk(versionTypeSandbox);

            List<Zendesk> zendeskTickets = new List<Zendesk>();
            IListResponse<Ticket> responses = client.Search.Find(new ZendeskQuery<Ticket>().WithCustomFilter("subject", searchCriteria));
            List<Ticket> responseTickets = (List<Ticket>)responses.Results;

            ZendDeskEmailEntry fromEmails = new ZendDeskEmailEntry();
            ZendDeskEmailEntry toEmails = new ZendDeskEmailEntry();

            foreach (Ticket singleTicket in responseTickets)
            {
                if (!listTickets.Any(ticket => ticket.Equals(singleTicket.Id)))
                {
                    fromEmails = ZendDeskEmailParser.GetEmailList(singleTicket.Via.Source.From);
                    toEmails = ZendDeskEmailParser.GetEmailList(singleTicket.Via.Source.To);

                    zendeskTickets.Add(new Zendesk(singleTicket.Id.ToString(), singleTicket.Created, singleTicket.Updated, singleTicket.Subject,
                        singleTicket.Status.ToString(), singleTicket.Priority, false, fromEmails.Address, fromEmails.Name, toEmails.Address, toEmails.Name));
                    listTickets.Add(singleTicket.Id);
                }
            }

            return zendeskTickets;
        }

        public static List<Zendesk> SearchDescriptionTickets(string searchCriteria, ref List<long?> listTickets)
        {
            /* Created by Neil Jansen - 12 December 2018
             * Create a list of Zendesk objects through filtering the Description field in Zendesk
             */

            ConnectToZendesk(versionTypeSandbox);

            List<Zendesk> zendeskTickets = new List<Zendesk>();
            IListResponse<Ticket> responses = client.Search.Find(new ZendeskQuery<Ticket>().WithCustomFilter("description", searchCriteria));
            List<Ticket> responseTickets = (List<Ticket>)responses.Results;

            ZendDeskEmailEntry fromEmails = new ZendDeskEmailEntry();
            ZendDeskEmailEntry toEmails = new ZendDeskEmailEntry();

            foreach (Ticket singleTicket in responseTickets)
            {
                if (!listTickets.Any(ticket => ticket.Equals(singleTicket.Id)))
                {
                    fromEmails = ZendDeskEmailParser.GetEmailList(singleTicket.Via.Source.From);
                    toEmails = ZendDeskEmailParser.GetEmailList(singleTicket.Via.Source.To);

                    zendeskTickets.Add(new Zendesk(singleTicket.Id.ToString(), singleTicket.Created, singleTicket.Updated, singleTicket.Subject,
                        singleTicket.Status.ToString(), singleTicket.Priority, false, fromEmails.Address, fromEmails.Name, toEmails.Address, toEmails.Name));
                    listTickets.Add(singleTicket.Id);
                }
            }

            return zendeskTickets;
        }

        public static List<Zendesk> SearchRequestorTickets(string searchCriteria, ref List<long?> listTickets)
        {
            /* Created by Neil Jansen - 18 December 2018
             * Create a list of Zendesk objects through filtering the Description field in Zendesk
             */

            //true if sandbox, false for production
            ConnectToZendesk(versionTypeSandbox);

            List<Zendesk> zendeskTickets = new List<Zendesk>();
            IListResponse<Ticket> responses = client.Search.Find(new ZendeskQuery<Ticket>().WithCustomFilter("requester", searchCriteria));
            List<Ticket> responseTickets = (List<Ticket>)responses.Results;

            ZendDeskEmailEntry fromEmails = new ZendDeskEmailEntry();
            ZendDeskEmailEntry toEmails = new ZendDeskEmailEntry();

            foreach (Ticket singleTicket in responseTickets)
            {
                if (!listTickets.Any(ticket => ticket.Equals(singleTicket.Id)))
                {
                    fromEmails = ZendDeskEmailParser.GetEmailList(singleTicket.Via.Source.From);
                    toEmails = ZendDeskEmailParser.GetEmailList(singleTicket.Via.Source.To);

                    zendeskTickets.Add(new Zendesk(singleTicket.Id.ToString(), singleTicket.Created, singleTicket.Updated, singleTicket.Subject,
                        singleTicket.Status.ToString(), singleTicket.Priority, false, fromEmails.Address, fromEmails.Name, toEmails.Address, toEmails.Name));
                    listTickets.Add(singleTicket.Id);
                }
            }

            return zendeskTickets;
        }

        private static void ConnectToZendesk(bool sandbox)
        {
            if (HttpContext.Current.Session["ActiveUser"] != null)
            {
                Class_Objects.User u = (Class_Objects.User)HttpContext.Current.Session["ActiveUser"];
                zendeskUsername = u.ZendeskEmail;
                if (zendeskUsername == string.Empty || zendeskUsername == null)
                {
                    throw new Exception("Your Zendesk email address credentials are not set up. Please see I.T.");
                }

                if (!sandbox)
                {
                    zendeskURI = new Uri(zendeskURL);
                    configuration = new ZendeskDefaultConfiguration(zendeskUsername, zendeskAPIToken);
                }
                else
                {
                    zendeskURI = new Uri(zendeskSandboxURL);
                    configuration = new ZendeskDefaultConfiguration(zendeskUsername, zendeskSandboxAPIToken);
                }

                client = new ZendeskClient(zendeskURI, configuration);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
        }

        public void UpdateZendeskTicketWithPDFFile(long ticketNo, MemoryFile file, string rmaNo, string amazonBucketURL, bool fileAttached)
        {
            ConnectToZendesk(versionTypeSandbox);

            
            IResponse<Ticket> ticketResponse = client.Tickets.Get(ticketNo);
            Ticket ticket = ticketResponse.Item;
            Ticket newTicket = ticket;
            TicketComment newComment = new TicketComment
            {
                Id = ticketNo
            };

            if (fileAttached)
            {
                string uploadToken = string.Empty;
                IResponse<Upload> response = client.Upload.Post(new UploadRequest
                {
                    Item = file
                });

                uploadToken = response.Item.Token;
                newComment.AddAttachmentToComment(uploadToken);

                newComment.Body = @"Hello,

                Your return request has been approved.  Your Return Merchandise Authorization number is " + rmaNo + @".

                Please see attached document for return instructions and shipping label. Alternatively [Click here](" + amazonBucketURL + @") to download your instructions return instructions and shipping label manually.

                IMPORTANT: Please remove ALL locks and passwords. Any device(s) received locked with your information will be denied, returned at your expense with no refund submitted for processing.

                Thank You";
            }
            else
            {
                newComment.Body = @"Hello,

                Your return request has been approved.  Your Return Merchandise Authorization number is " + rmaNo + @".

                [Click here](" + amazonBucketURL + @") to download your return instructions and shipping label.

                IMPORTANT: Please remove ALL locks and passwords. Any device(s) received locked with your information will be denied, returned at your expense with no refund submitted for processing.

                Thank You";
            }

            newComment.IsPublic = true;
            newComment.Type = TicketEventType.Comment;
            
            newTicket.Comment = newComment;
            newTicket.Status = TicketStatus.Solved;

            client.Tickets.Put(new TicketRequest { Item = newTicket });
        }

        public long? CreateNewZendeskTicketWithPDFFile(MemoryFile file, string rmaNo, string amazonBucketURL, string customerEmail, string customerName, bool fileAttached, string ExtDocNo)
        {
            ConnectToZendesk(versionTypeSandbox);

            string uploadToken = string.Empty;
            long? createdTicketID;
            Ticket newTicket = new Ticket();
            Ticket updateNewTicket = new Ticket();
            Ticket ticketReponse;

            // First we need to create the Zendesk Ticket per the customer request BEFORE we send the return shipping information
            newTicket.Subject = "Return Label Request for: " + ExtDocNo;

            TicketComment newComment = new TicketComment
            {
                IsPublic = true,
                Type = TicketEventType.Comment,
                Body = "Return Label Request"
            };

            newTicket.Comment = newComment;

            Via via = new Via
            {
                Channel = "web_service"
            };

            newTicket.Via = via;

            TicketRequester ticketRequester = new TicketRequester
            {
                Name = customerName,
                Email = customerEmail,
            };

            newTicket.Requester = ticketRequester;
            newTicket.Recipient = customerEmail;

            TicketRequest newRequest = new TicketRequest
            {
                Item = newTicket
            };

            IResponse<Ticket> responseTicket = client.Tickets.Post(newRequest);

            // We need to retrieve the ticket ID of the newly created Ticket
            createdTicketID = responseTicket.Item.Id;

            // Now we immediately update the newly created ticket with the return shipping information
            IResponse<Ticket> updateTicketResponse = client.Tickets.Get(createdTicketID.Value);
            ticketReponse = updateTicketResponse.Item;
            updateNewTicket = ticketReponse;

            TicketComment comment = new TicketComment
            {
                IsPublic = true,
                Type = TicketEventType.Comment
            };

            if (fileAttached)
            {
                IResponse<Upload> response = client.Upload.Post(new UploadRequest
                {
                    Item = file
                });

                uploadToken = response.Item.Token;
                comment.AddAttachmentToComment(uploadToken);

                comment.Body = @"Hello,

                Your return request has been approved.  Your Return Merchandise Authorization number is " + rmaNo + @".

                Please see attached document for return instructions and shipping label. Alternatively [Click here](" + amazonBucketURL + @") to download your instructions return instructions and shipping label manually.

                IMPORTANT: Please remove ALL locks and passwords. Any device(s) received locked with your information will be denied, returned at your expense with no refund submitted for processing.

                Thank You";
            }
            else
            {
                comment.Body = @"Hello,

                Your return request has been approved.  Your Return Merchandise Authorization number is " + rmaNo + @".

                [Click here](" + amazonBucketURL + @") to download your return instructions and shipping label.

                IMPORTANT: Please remove ALL locks and passwords. Any device(s) received locked with your information will be denied, returned at your expense with no refund submitted for processing.

                Thank You";
            }

            updateNewTicket.Comment = comment;
            updateNewTicket.Status = TicketStatus.Solved;         

            client.Tickets.Put(new TicketRequest { Item = updateNewTicket });

            return createdTicketID;
        }

        public Zendesk VerifyZendeskTicket(long zendeskTicket)
        {
            Zendesk ticket = new Zendesk();

            try
            {
                IResponse<Ticket> validTicket = client.Tickets.Get(zendeskTicket);
                Ticket response = validTicket.Item;

                ZendDeskEmailEntry fromEmails = ZendDeskEmailParser.GetEmailList(response.Via.Source.From);
                ZendDeskEmailEntry toEmails = ZendDeskEmailParser.GetEmailList(response.Via.Source.To);

                ticket = new Zendesk(response.Id.ToString(), response.Created, response.Updated, response.Subject,
                        response.Status.ToString(), response.Priority, false, fromEmails.Address, fromEmails.Name, toEmails.Address, toEmails.Name);
            }
            catch(Exception)
            {
                ticket = null;
            }

            return ticket;
        }
    }
}