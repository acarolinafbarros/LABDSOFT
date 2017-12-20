using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace iGAMBot.Controllers
{
    public class LUISService
    {
        public async Task<string> MakeRequest(string sentence)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // This app ID is for a public sample app that recognizes requests to turn on and turn off lights
            var luisAppId = "a25794fc-f24d-4f3d-803f-77a8323c9a3c";
            var subscriptionKey = "e2f6649b61d840d7b1b219b6918f31b9";

            // The request header contains your subscription key
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // The "q" parameter contains the utterance to send to LUIS
            queryString["q"] = sentence;

            // These optional request parameters are set to their default values
            queryString["timezoneOffset"] = "0";
            queryString["verbose"] = "true";
            queryString["spellCheck"] = "false";
            queryString["staging"] = "false";

            var uri = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/" + luisAppId + "?" + queryString;
            var response = await client.GetAsync(uri);

            var strResponseContent = await response.Content.ReadAsStringAsync();

            // Display the JSON result from LUIS
            Console.WriteLine(strResponseContent.ToString());

            return strResponseContent.ToString();
        }
    }
}