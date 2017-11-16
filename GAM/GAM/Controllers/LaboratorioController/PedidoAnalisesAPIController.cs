using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace GAM.Controllers.LaboratorioController
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PedidoAnalisesAPIController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "http://localhost:5050/api/geraranalise";

        // GET: api/PedidoAnalises
        [HttpGet]
        public async Task<ActionResult> PedidoAnalisesAPI(int amostraId)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonString = new JsonValues
                {
                    AmostraId = amostraId,
                    NomeAnaliseA = "HBsAg",
                    NomeAnaliseB = "Ac HCV"
                };

                var jsonT = JsonConvert.SerializeObject(jsonString);

                HttpResponseMessage Res = await client.PostAsync(Baseurl, new StringContent(jsonT, Encoding.UTF8, "application/json"));

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    var EmpInfo = JsonConvert.DeserializeObject(EmpResponse);
                    return View(EmpInfo);

                }
                //returning the employee list to view  
                return Ok();
            }
        }
    }

    public class JsonValues
    {
        public int AmostraId { get; set; }

        // Analise - HBsAg
        public string NomeAnaliseA { get; set; }

        // Analise - Ac HCV
        public string NomeAnaliseB { get; set; }
    }
}
