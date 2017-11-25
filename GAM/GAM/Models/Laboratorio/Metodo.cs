using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Laboratorio
{
    public class Metodo
    {
        public int MetodoId { get; set; }

        public int AnaliseId { get; set; }
        public Analise Analise { get; set; }

        [Display(Name = "Metodo")]
        public string Nome { get; set; }

        [Display(Name = "Interpretacao +")]
        public string InterpretacaoPos { get; set; }

        [Display(Name = "Interpretacao -")]
        public string InterpretacaoNeg { get; set; }

        [Display(Name = "Valor Referencia +")]
        public float ValorReferenciaPos { get; set; }

        [Display(Name = "Valor Referencia -")]
        public float ValorReferenciaNeg { get; set; }

        [Display(Name = "Resultado Numerico")]
        public float ResultadoNumerico { get; set; }

        [Display(Name = "Resultado")]
        public string Resultado { get; set; }
    }
}
