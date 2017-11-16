using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using iAnalysis.Model;
using Newtonsoft.Json;

namespace iAnalysis.Controllers
{
    [Route("api/[controller]")]
    public class GerarAnaliseController : ControllerBase
    {
        private static float _HBsAGvalorRefPosCMIA = (float)(1.00);
        private static float _HBsAGvalorRefNegCMIA = (float)(1.00);

        private static float _HBsAGvalorRefPosMEIA = (float)(2.00);
        private static float _HBsAGvalorRefNegMEIA = (float)(2.00);

        private static float _AcHCVvalorRefPosCMIA = (float)(0.80);
        private static float _AcHCVvalorRefNegCMIA = (float)(1.00);

        private static float _AcHCVvalorRefPosELISA = (float)(1.00);
        private static float _AcHCVvalorRefNegELISA = (float)(1.00);

        /// <summary>
        /// Testar no Postman: 
        /// POST - http://localhost:5050/api/geraranalise/
        /// Body - { "AmostraId" : 1, "NomeAnaliseA" : "HBsAg", "NomeAnaliseB" : "Ac HCV" }
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult POSTGerarAnalise([FromBody]JsonValues context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Extrair dados do json
            var amostraId = context.AmostraId;
            var nomeAnaliseA = context.NomeAnaliseA;
            var nomeAnaliseB = context.NomeAnaliseB;

            var listaResultadosAnalise = new List<ResultadoAnalise>();
            var _opReativo = new List<string> { "Reativo", "Não Reativo" };
            var _opPosNeg = new List<string> { "Positivo", "Negativo" };

            // Criar objetos ResultadoAnalise
            var res_HBsAG_CMIA = new ResultadoAnalise
            {
                AmostraId = amostraId,
                NomeAnalise = nomeAnaliseA,
                NomeMetodo = "CMIA",
                InterpretacaoPos = "Não Reativo",
                InterpretacaoNeg = "Reativo",
                ValorReferenciaPos = _HBsAGvalorRefPosCMIA,
                ValorReferenciaNeg = _HBsAGvalorRefNegCMIA,
                ResultadoNumerico = (float)new Random().NextDouble() * (_HBsAGvalorRefPosCMIA - _HBsAGvalorRefNegCMIA) + _HBsAGvalorRefNegCMIA,
                ResultadoTexto = (string)_opReativo[new Random().Next(_opReativo.Count)]
            }; 

            var res_HBsAG_MEIA = new ResultadoAnalise
            {
                AmostraId = amostraId,
                NomeAnalise = nomeAnaliseA,
                NomeMetodo = "MEIA",
                InterpretacaoPos = "Negativo",
                InterpretacaoNeg = "Positivo",
                ValorReferenciaPos = _HBsAGvalorRefPosMEIA,
                ValorReferenciaNeg = _HBsAGvalorRefNegMEIA,
                ResultadoNumerico = (float) new Random().NextDouble() * (_HBsAGvalorRefPosMEIA - _HBsAGvalorRefNegMEIA) + _HBsAGvalorRefNegMEIA,
                ResultadoTexto = (string)_opPosNeg[new Random().Next(_opPosNeg.Count)]
            };

            var res_AcHCV_CMIA = new ResultadoAnalise
            {
                AmostraId = amostraId,
                NomeAnalise = nomeAnaliseB,
                NomeMetodo = "CMIA",
                InterpretacaoPos = "Não Reativo",
                InterpretacaoNeg = "Reativo",
                ValorReferenciaPos = _AcHCVvalorRefPosCMIA,
                ValorReferenciaNeg = _AcHCVvalorRefNegCMIA,
                ResultadoNumerico = (float) new Random().NextDouble() * (_AcHCVvalorRefPosCMIA - _AcHCVvalorRefNegCMIA) + _AcHCVvalorRefNegCMIA,
                ResultadoTexto = (string)_opReativo[new Random().Next(_opReativo.Count)]
            };

            var res_AcHCV_ELISA = new ResultadoAnalise
            {
                AmostraId = amostraId,
                NomeAnalise = nomeAnaliseB,
                NomeMetodo = "ELISA",
                InterpretacaoPos = "Não Reativo",
                InterpretacaoNeg = "Reativo",
                ValorReferenciaPos = _AcHCVvalorRefPosELISA,
                ValorReferenciaNeg = _AcHCVvalorRefNegELISA,
                ResultadoNumerico = (float) new Random().NextDouble() * (_AcHCVvalorRefPosELISA - _AcHCVvalorRefNegELISA) + _AcHCVvalorRefNegELISA,
                ResultadoTexto = (string)_opReativo[new Random().Next(_opReativo.Count)]
            };

            listaResultadosAnalise.Add(res_HBsAG_CMIA);
            listaResultadosAnalise.Add(res_HBsAG_MEIA);
            listaResultadosAnalise.Add(res_AcHCV_CMIA);
            listaResultadosAnalise.Add(res_AcHCV_ELISA);

            var jsonToReturn = JsonConvert.SerializeObject(listaResultadosAnalise);
            return Ok(jsonToReturn);
        }

        /// <summary>
        /// Testar no Postman: 
        /// GET - http://localhost:5050/api/geraranalise/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "iAnalysis API - Gerar Analises";
        }

        /// <summary>
        /// Classe para mapear o json recebido pela GAM
        /// </summary>
        public class JsonValues
        {
            public int AmostraId { get; set; }

            // Analise - HBsAg
            public string NomeAnaliseA { get; set; }

            // Analise - Ac HCV
            public string NomeAnaliseB { get; set; }
        }
    }
}
