using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iAnalysis.Model;

namespace iAnalysis.Controllers
{
    [Produces("application/json")]
    [Route("api/GerarAnalise")]
    public class GerarAnaliseController : Controller
    {
        private static float _HBsAGvalorRefPosCMIA = (float)(1.00);
        private static float _HBsAGvalorRefNegCMIA = (float)(1.00);

        private static float _HBsAGvalorRefPosMEIA = (float)(2.00);
        private static float _HBsAGvalorRefNegMEIA = (float)(2.00);

        private static float _AcHCVvalorRefPosCMIA = (float)(0.80);
        private static float _AcHCVvalorRefNegCMIA = (float)(1.00);

        private static float _AcHCVvalorRefPosELISA = (float)(1.00);
        private static float _AcHCVvalorRefNegELISA = (float)(1.00);

        public string GerarResultadosAnalises(string nomeAnalise, int amostraId, int analiseId)
        {
            var _opReativo = new List<string> {"Reativo", "Não Reativo"};
            var _opPosNeg = new List<string> { "Positivo", "Negativo"};

            if (nomeAnalise == "HBsAg")
            {
                var res_HBsAG_CMIA = new ResultadoAnalise
                {
                    AmostraId = amostraId,
                    AnaliseId = analiseId,
                    NomeMetodo = "CMIA",
                    InterpretacaoPos = "Não Reativo",
                    InterpretacaoNeg = "Reativo",
                    ValorReferenciaPos = _HBsAGvalorRefPosCMIA,
                    ValorReferenciaNeg = _HBsAGvalorRefNegCMIA,
                    ResultadoNumerico = new Random().NextDouble() * (_HBsAGvalorRefPosCMIA - _HBsAGvalorRefNegCMIA) + _HBsAGvalorRefNegCMIA,
                    ResultadoTexto = ""
                };

                var res_HBsAG_MEIA = new ResultadoAnalise
                {
                    AmostraId = amostraId,
                    AnaliseId = analiseId,
                    NomeMetodo = "MEIA",
                    InterpretacaoPos = "Negativo",
                    InterpretacaoNeg = "Positivo",
                    ValorReferenciaPos = _HBsAGvalorRefPosMEIA,
                    ValorReferenciaNeg = _HBsAGvalorRefNegMEIA,
                    ResultadoNumerico = new Random().NextDouble() * (_HBsAGvalorRefPosMEIA - _HBsAGvalorRefNegMEIA) + _HBsAGvalorRefNegMEIA,
                    ResultadoTexto = ""
                };
            }

            if (nomeAnalise == "Ac HCV")
            {
                var res_AcHCV_CMIA = new ResultadoAnalise
                {
                    AmostraId = amostraId,
                    AnaliseId = analiseId,
                    NomeMetodo = "CMIA",
                    InterpretacaoPos = "Não Reativo",
                    InterpretacaoNeg = "Reativo",
                    ValorReferenciaPos = _AcHCVvalorRefPosCMIA,
                    ValorReferenciaNeg = _AcHCVvalorRefNegCMIA,
                    ResultadoNumerico = new Random().NextDouble() * (_AcHCVvalorRefPosCMIA - _AcHCVvalorRefNegCMIA) + _AcHCVvalorRefNegCMIA,
                    ResultadoTexto = ""
                };

                var res_AcHCV_ELISA = new ResultadoAnalise
                {
                    AmostraId = amostraId,
                    AnaliseId = analiseId,
                    NomeMetodo = "ELISA",
                    InterpretacaoPos = "Não Reativo",
                    InterpretacaoNeg = "Reativo",
                    ValorReferenciaPos = _AcHCVvalorRefPosELISA,
                    ValorReferenciaNeg = _AcHCVvalorRefNegELISA,
                    ResultadoNumerico = new Random().NextDouble() * (_AcHCVvalorRefPosELISA - _AcHCVvalorRefNegELISA) + _AcHCVvalorRefNegELISA,
                    ResultadoTexto = ""
                };
            }


            return "Welcome";
        }

    }
}