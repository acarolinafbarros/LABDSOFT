using System;
using System.Collections.Generic;

namespace iAnalysis.Model
{
    public class ResultadoAnalise
    {
        public ICollection<Analise> Analises { get; set; }
    }

    public class Analise
    {
        public int AmostraId { get; set; }

        public string NomeAnalise { get; set; }

        public ICollection<Metodo> Metodos { get; set; }
    }

    public class Metodo
    {
        public string NomeMetodo { get; set; }

        public string InterpretacaoPos { get; set; }

        public string InterpretacaoNeg { get; set; }

        public double ValorReferenciaPos { get; set; }

        public double ValorReferenciaNeg { get; set; }

        public double ResultadoNumerico { get; set; }

        public string ResultadoTexto { get; set; }
    }
}
