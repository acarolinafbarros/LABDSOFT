using iGAMBot.Model;
using System;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace iGAMBot.Controllers
{
    public class BotToGamController : ControllerBase
    {
        //Hosted web API REST Service base url  
        //string Baseurl = "http://localhost:51008/api/GamToBot/CheckIfDador"; // Manuel
        //string Baseurl = "http://localhost:51889/api/GamToBot/CheckIfDador"; // Carolina

        string Baseurl = "http://localhost:61264/api/GamToBot/CheckIfDador"; // Maria
        string Baseurl_Consulta = "http://localhost:61264/api/GamToBot/CheckIfDadorCancelarConsulta"; // Maria
        string Baseurl_CancelarConsulta = "http://localhost:61264/api/GamToBot/CancelarConsulta"; // Maria
        string Baseurl_ListarConsulta = "http://localhost:61264/api/GamToBot/ListarConsulta"; // Maria
        string Baseurl_MarcarConsulta = "http://localhost:61264/api/GamToBot/MarcarConsulta"; // Maria

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

        public ModelDadorCancelarConsultToBot CheckIfDadorForCancelarConsulta(string docIdentificacao)
        {
            using (var client = new HttpClient())
            {
                // Passing service base url  
                client.BaseAddress = new Uri(Baseurl_Consulta);

                client.DefaultRequestHeaders.Clear();

                // Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonString = new JsonDTO
                {
                    DocIdentificacao = docIdentificacao
                };

                var jsonT = JsonConvert.SerializeObject(jsonString);

                var Res = client.PostAsync(Baseurl_Consulta, new StringContent(jsonT, Encoding.UTF8, "application/json")).Result;

                // Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    ModelDadorCancelarConsultToBot resJson = Res.Content.ReadAsAsync<ModelDadorCancelarConsultToBot>().Result;

                    return resJson;
                }
                else // Não encontrou nenhum match
                {
                    return null;
                }
            }
        }

        public bool CancelarConsulta(int dadorId, int consultaId)
        {
            using (var client = new HttpClient())
            {
                // Passing service base url  
                client.BaseAddress = new Uri(Baseurl_CancelarConsulta);

                client.DefaultRequestHeaders.Clear();

                // Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonString = new JsonDTOForConsulta
                {
                    DadorId = dadorId,
                    ConsultaId = consultaId                 
                };

                var jsonT = JsonConvert.SerializeObject(jsonString);

                var Res = client.PostAsync(Baseurl_CancelarConsulta, new StringContent(jsonT, Encoding.UTF8, "application/json")).Result;

                // Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    return true;
                }
                else // Não conseguiu remover a consulta
                {
                    return false;
                }
            }
        }

        public List<ModelDadorMarcarConsultToBot> CheckIfDadorForMarcarConsulta(string docIdentificacao)
        {
            using (var client = new HttpClient())
            {
                // Passing service base url  
                client.BaseAddress = new Uri(Baseurl_ListarConsulta);

                client.DefaultRequestHeaders.Clear();

                // Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonString = new JsonDTO
                {
                    DocIdentificacao = docIdentificacao
                };

                var jsonT = JsonConvert.SerializeObject(jsonString);

                var Res = client.PostAsync(Baseurl_ListarConsulta, new StringContent(jsonT, Encoding.UTF8, "application/json")).Result;

                // Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    List<ModelDadorMarcarConsultToBot> resJson = Res.Content.ReadAsAsync<List<ModelDadorMarcarConsultToBot>>().Result;

                    return resJson;
                }
                else // Não encontrou nenhum match
                {
                    return null;
                }
            }
        }

        public bool MarcarConsulta(int dadorId, int slotId)
        {
            using (var client = new HttpClient())
            {
                // Passing service base url  
                client.BaseAddress = new Uri(Baseurl_MarcarConsulta);

                client.DefaultRequestHeaders.Clear();

                // Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonString = new JsonDTOForMarcarConsulta
                {
                    DadorId = dadorId,
                    SlotId = slotId
                };

                var jsonT = JsonConvert.SerializeObject(jsonString);

                var Res = client.PostAsync(Baseurl_MarcarConsulta, new StringContent(jsonT, Encoding.UTF8, "application/json")).Result;

                // Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    return true;
                }
                else // Não conseguiu remover a consulta
                {
                    return false;
                }
            }
        }
    }
}