using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace ExcelDesign.Class_Objects.BitlyAPI
{
    public class BitlyAPI
    {
        public string URL { get; set; }
        public string AccessToken { get; set; }
        public string Host { get; set; }

        public BitlyAPI()
        {
            string urlP = string.Empty;
            string accessTokenP = string.Empty;
            string hostP = string.Empty;

            WebService ws = new WebService();
            ws.GetAPISetup(ref accessTokenP, ref urlP, ref hostP);

            URL = urlP;
            AccessToken = accessTokenP;
            Host = hostP;
        }

        private string GetGroupGUID()
        {
            string groupGUID = string.Empty;

            StringBuilder groupBuilder = new StringBuilder(URL);
            groupBuilder.Append("groups");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(groupBuilder.ToString());
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add("Authorization", "Bearer " + HttpUtility.UrlEncode(AccessToken));
            request.Accept = "application/json";
            request.Host = Host;
            request.ServicePoint.Expect100Continue = false;
            request.ContentLength = 0;

            try
            {
                WebResponse webResponse = request.GetResponse();

                var responseString = "";
                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    responseString = reader.ReadToEnd();                 
                }

                BitlyGroup groupObject = new BitlyGroup();

                groupObject = JsonConvert.DeserializeObject<BitlyGroup>(responseString);
                groupGUID = groupObject.groups[0].guid;
                groupGUID = groupGUID.Replace("https", "http");
            }
            catch (WebException wex)
            {
                string response = string.Empty;

                using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
            }

            return groupGUID;
        }

        public string ShortenURL(string longURL)
        {         
            string shortURL = string.Empty;
            string groupGUID = GetGroupGUID();

            try
            {
                using (WebClient client = new WebClient())
                {                
                    var reqParam = new { long_url = longURL };

                    string parsedJson = JsonConvert.SerializeObject(reqParam);
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] requestParams = encoding.GetBytes(parsedJson);

                    client.Headers["Authorization"] = "Bearer " + AccessToken;
                    client.Headers["Content-Type"] = "application/json";

                    byte[] responseBytes = client.UploadData(URL + "shorten?", "POST", requestParams);

                    var responseString = "";
                    using (Stream responseStream = new MemoryStream(responseBytes))
                    {
                        using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            responseString = reader.ReadToEnd();
                        }
                    }

                    BitlyShorten shortObject = new BitlyShorten();

                    shortObject = JsonConvert.DeserializeObject<BitlyShorten>(responseString);
                    shortURL = shortObject.link;
                }
            }
            catch (WebException wex)
            {
                string response = string.Empty;

                using (StreamReader reader = new StreamReader(wex.Response.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }

                BitlyAPIErrorHandler errorHandler = new BitlyAPIErrorHandler();

                errorHandler = JsonConvert.DeserializeObject<BitlyAPIErrorHandler>(response);

                StringBuilder exceptionString = new StringBuilder();
                exceptionString.Append("Bitly API Error:\n\n");
                exceptionString.AppendFormat("Field - {0}\nError Code - {1}\n", errorHandler.errors[0].field, errorHandler.errors[0].error_code);
                exceptionString.AppendFormat("Message - {0}\nDescription - {1}", errorHandler.message, errorHandler.description);

                throw new Exception(exceptionString.ToString());
            }

            return shortURL;
        }
    }
}