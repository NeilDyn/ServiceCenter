using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZendeskApi.Client;
using ZendeskApi.Contracts;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Responses;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Client.Resources;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


/// <summary>
/// V10 - PAB 19 March 2019
/// Object to parse the emails received in a JObject into a list of type ZendDeskEmailEntry
/// </summary>
public static class ZendDeskEmailParser
{
    /*
    V10 - PAB 19 March 2019
    Object to parse the emails resived in a JObject into a list of type ZendDeskEmailEntry
    */

    public static ZendDeskEmailEntry GetEmailList(JObject jObject) {
        //Get the list of emails in the JObject.
        ZendDeskEmailEntry emailList = new ZendDeskEmailEntry();
        JToken CurrentToken = jObject.Root;
        int count = 0;

        do
        {
            if (CurrentToken != null)
            {
                ZendDeskEmailEntry entry = JsonConvert.DeserializeObject<ZendDeskEmailEntry>(CurrentToken.ToString());

                if ((entry.Name != null) && (entry.Address != null))
                {
                    if (count == 0)
                    {
                        //Email address is not blank and has not been added to the list
                        emailList = entry;
                    }
                }
            }
        } while ((CurrentToken = jObject.Next) != null);

        return emailList;
    }
}

