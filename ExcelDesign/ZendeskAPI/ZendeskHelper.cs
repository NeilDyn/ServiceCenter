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

namespace ExcelDesign.ZendeskAPI
{
    public class ZendeskHelper
    {
        /* Helper Class Library for Zendesk Just Eat API
         * Created By Neil Jansen, Dynetek (Pty) Ltd.
         */
         
        private static string zendeskUsername = "zalmy@jegsons.com";
        private static string zendeskAPIToken = "9gryAaOh6JfSIaIWPetf42R4y4J0bPvdG3ta2uX9";
        private static string zendeskURL = "https://jegsons.zendesk.com";
        private static ZendeskDefaultConfiguration configuration;
        private static Uri zendeskURI;
        private static IZendeskClient client;

        public ZendeskHelper()
        {
            
        }

        public static List<Zendesk> SearchTickets(string searchCriteria, ref List<long?> listTickets)
        {
            ConnectToZendesk();

            List<Zendesk> zendeskTickets = new List<Zendesk>();
            IListResponse<Ticket> responses = client.Search.Find(new ZendeskQuery<Ticket>().WithCustomFilter("fieldvalue", searchCriteria));
            List<Ticket> responseTickets = (List<Ticket>)responses.Results;

            foreach (Ticket singleTicket in responseTickets)
            {
                if (!listTickets.Any(ticket => ticket.Equals(singleTicket.Id)))
                {
                    zendeskTickets.Add(new Zendesk(singleTicket.Id.ToString()));
                    listTickets.Add(singleTicket.Id);
                }
            }

            return zendeskTickets;
        }

        private static void ConnectToZendesk()
        {
            zendeskURI = new Uri(zendeskURL);
            configuration = new ZendeskDefaultConfiguration(zendeskUsername, zendeskAPIToken);
            client = new ZendeskClient(zendeskURI, configuration);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
    }
}