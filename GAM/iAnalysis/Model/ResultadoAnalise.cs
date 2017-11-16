using System;

namespace iAnalysis.Model
{
    public class ResultadoAnalise
    {
        public int AmostraId { get; set; }

        public string NomeAnalise { get; set; }

        public string NomeMetodo { get; set; }

        public string InterpretacaoPos { get; set; }

        public string InterpretacaoNeg { get; set; }

        public double ValorReferenciaPos { get; set; }

        public double ValorReferenciaNeg { get; set; }

        public double ResultadoNumerico { get; set; } 

        public string ResultadoTexto { get; set; }
    }
}
