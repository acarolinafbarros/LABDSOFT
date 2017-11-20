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
using GAM.Data;
using Microsoft.EntityFrameworkCore;

namespace GAM.Controllers.MedicoController
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PedidoAnalisesAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoAnalisesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

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

                    await InsertInDatabase(resultadoAnalise);

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

        private async Task InsertInDatabase(ResultadoAnalise resultadoAnalise)
        {
            var novoResAnalise = new ResultadoAnalise
            {
                Data = resultadoAnalise.Data
            };

            await _context.ResultadoAnalise.AddAsync(novoResAnalise);
            await _context.SaveChangesAsync();

            var objResAnalise = await _context.ResultadoAnalise.AsNoTracking().LastOrDefaultAsync();

            var amostraID = -1;

            foreach (var a in resultadoAnalise.Analises)
            {
                var novaAnalise = new Analise
                {
                    AmostraId = a.AmostraId,
                    Nome = a.Nome,
                    Data = a.Data,
                    ResultadoAnaliseId = objResAnalise.ResultadoAnaliseId
                };

                await _context.Analise.AddAsync(novaAnalise);
                await _context.SaveChangesAsync();

                amostraID = a.AmostraId;

                var objAnalise = await _context.Analise.LastOrDefaultAsync();

                foreach (var m in a.Metodos)
                {
                    var novoMetodo = new Metodo
                    {
                        AnaliseId = objAnalise.AnaliseId,
                        Nome = m.Nome,
                        InterpretacaoNeg = m.InterpretacaoNeg,
                        InterpretacaoPos = m.InterpretacaoPos,
                        ValorReferenciaNeg = m.ValorReferenciaNeg,
                        ValorReferenciaPos = m.ValorReferenciaPos,
                        ResultadoNumerico = m.ResultadoNumerico,
                        Resultado = m.Resultado
                    };

                    await _context.Metodo.AddAsync(novoMetodo);
                    await _context.SaveChangesAsync();
                }
            }

            var getAmostraToUpdate = await _context.Amostra.AsNoTracking().SingleOrDefaultAsync(ams => ams.AmostraId == amostraID);

            var amostraToUpdate = new Amostra
            {
                AmostraId = getAmostraToUpdate.AmostraId,
                DadorId = getAmostraToUpdate.DadorId,
                DataRecolha = getAmostraToUpdate.DataRecolha,
                EstadoAmostra = Models.Enums.EstadoAmostraEnum.Analisada,
                GlobetCor = getAmostraToUpdate.GlobetCor,
                GlobetNumero = getAmostraToUpdate.GlobetNumero,
                Cannister = getAmostraToUpdate.Cannister,
                PalhetaCor = getAmostraToUpdate.PalhetaCor,
                Piso = getAmostraToUpdate.Piso,
                TipoAmostra = getAmostraToUpdate.TipoAmostra
            };

            _context.Update(amostraToUpdate);
            await _context.SaveChangesAsync();
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
