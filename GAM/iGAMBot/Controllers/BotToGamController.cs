using iGAMBot.Model;
using System;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace iGAMBot.Controllers
{
    public class BotToGamController : ControllerBase
    {
        //Hosted web API REST Service base url  
        //string Baseurl = "http://localhost:51008/api/GamToBot/CheckIfDador";
        string Baseurl = "http://localhost:51889/api/GamToBot/CheckIfDador";

        public ModelDadorResEspermToBot CheckIfDadorForResEsperm(string docIdentificacao)
        {
            using (var client = new HttpClient())
            {
                // Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                
                // Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonString = new JsonDTO
                {
                    DocIdentificacao = docIdentificacao
                };

                var jsonT = JsonConvert.SerializeObject(jsonString);

                var Res = client.PostAsync(Baseurl, new StringContent(jsonT, Encoding.UTF8, "application/json")).Result;

                // Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    ModelDadorResEspermToBot resJson = Res.Content.ReadAsAsync<ModelDadorResEspermToBot>().Result;

                    return resJson;
                }
                else // Não encontrou nenhum match
                {
                    return null;
                }
            }
        }
    }
}