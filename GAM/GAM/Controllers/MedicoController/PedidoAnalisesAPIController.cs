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
using GAM.Views;
using GAM.Models.Laboratorio;
using Newtonsoft.Json.Linq;

namespace GAM.Controllers.MedicoController
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
                    string response = await Res.Content.ReadAsStringAsync();

                    dynamic resultadoAnaliseJSON = JsonConvert.DeserializeObject(response);

                    //Criar objeto ResultadoAnalise com o conteudo de JSON
                    ResultadoAnalise resultadoAnalise = CriarResultadoAnalise(resultadoAnaliseJSON);

                    return View("~/Views/PedidoAnalise/Edit.cshtml", resultadoAnalise);

                }
                //returning the employee list to view  
                return Ok();
            }
        }

        private ResultadoAnalise CriarResultadoAnalise(dynamic resultadoAnaliseJSON)
        {
            List<Analise> analises = new List<Analise>();

            dynamic analisesJSON = resultadoAnaliseJSON.analises;

            foreach (var a in analisesJSON)
            {
                List<Metodo> metodos = new List<Metodo>();

                dynamic metodosJSON = a.metodos;

                foreach (var m in metodosJSON)
                {
                    Metodo metodo = new Metodo
                    {
                        Nome = m.nomeMetodo,
                        InterpretacaoNeg = m.interpretacaoNeg,
                        InterpretacaoPos = m.interpretacaoPos,
                        ValorReferenciaNeg = m.valorReferenciaNeg,
                        ValorReferenciaPos = m.valorReferenciaPos,
                        ResultadoNumerico = m.resultadoNumerico,
                        Resultado = m.resultadoTexto,
                    };

                    metodos.Add(metodo);
                }

                Analise analise = new Analise
                {
                    AmostraId = a.amostraId,
                    Nome = a.nomeAnalise,
                    Data = DateTime.Today,
                    Metodos = metodos
                };

                analises.Add(analise);
            }

            ResultadoAnalise resultadoAnalise = new ResultadoAnalise
            {
                Data = DateTime.Today,
                Analises = analises              
            };

            return resultadoAnalise;
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
