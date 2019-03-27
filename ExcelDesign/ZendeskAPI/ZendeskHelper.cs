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
         * Added functionality to use Current User logged in ZendeskEmail to connecto to the api;
         */

        private static string zendeskUsername = string.Empty;
        private static readonly string zendeskAPIToken = "9gryAaOh6JfSIaIWPetf42R4y4J0bPvdG3ta2uX9";
        private static readonly string zendeskURL = "https://jegsons.zendesk.com";
        private static ZendeskDefaultConfiguration configuration;
        private static Uri zendeskURI;
        private static IZendeskClient client;

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

            ConnectToZendesk();

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

            ConnectToZendesk();

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

            ConnectToZendesk();

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

            ConnectToZendesk();

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

        private static void ConnectToZendesk()
        {
            if (HttpContext.Current.Session["ActiveUser"] != null)
            {
                Class_Objects.User u = (Class_Objects.User)HttpContext.Current.Session["ActiveUser"];
                zendeskUsername = u.ZendeskEmail;
                if (zendeskUsername == string.Empty || zendeskUsername == null)
                {
                    throw new Exception("Your Zendesk email address credentials are not set up. Please see I.T.");
                }

                zendeskURI = new Uri(zendeskURL);
                configuration = new ZendeskDefaultConfiguration(zendeskUsername, zendeskAPIToken);
                client = new ZendeskClient(zendeskURI, configuration);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
        }

        public void UpdateZendeskTicketWithPDFFile(long ticketNo, MemoryFile file, string rmaNo)
        {
            ConnectToZendesk();

            string uploadToken = string.Empty;
            IResponse<Ticket> ticketResponse = client.Tickets.Get(ticketNo);
            Ticket ticket = ticketResponse.Item;
            Ticket newTicket = ticket;
            TicketComment newComment = new TicketComment();

            IResponse<Upload> response = client.Upload.Post(new UploadRequest
            {
                Item = file
            });

            uploadToken = response.Item.Token;

            newComment.Id = ticketNo;
            newComment.Body = @"Hello,

Your return request has been approved.  Your Return Merchandise Authorization number is " + rmaNo + @".

Please see attached document for return instructions and shipping label.

IMPORTANT: Please remove ALL locks and passwords. Any device(s) received locked with your information will be denied, returned at your expense with no refund submitted for processing.

Thank You";
            newComment.IsPublic = true;
            newComment.Type = TicketEventType.Comment;
            newComment.AddAttachmentToComment(uploadToken);

            newTicket.Comment = newComment;

            client.Tickets.Put(new TicketRequest { Item = newTicket });
        }

        public long? CreateNewZendeskTicketWithPDFFile(MemoryFile file, string rmaNo)
        {
            ConnectToZendesk();

            string uploadToken = string.Empty;
            Ticket ticket = new Ticket();
            TicketComment comment = new TicketComment();

            IResponse<Upload> response = client.Upload.Post(new UploadRequest
            {
                Item = file
            });

            uploadToken = response.Item.Token;
            comment.AddAttachmentToComment(uploadToken);
            comment.IsPublic = true;
            comment.Type = TicketEventType.Comment;
            comment.Body = @"Hello,

Your return request has been approved.  Your Return Merchandise Authorization number is " + rmaNo + @".

Please see attached document for return instructions and shipping label.

IMPORTANT: Please remove ALL locks and passwords. Any device(s) received locked with your information will be denied, returned at your expense with no refund submitted for processing.";

            ticket.Recipient = "itsupport@jegsons.zendesk.com";

            ticket.Subject = "Return Request Information";
            ticket.Comment = comment;

            TicketRequest request = new TicketRequest
            {
                Item = ticket
            };

            client.Tickets.Post(request);

            return request.Item.Id;
        }
    }
}