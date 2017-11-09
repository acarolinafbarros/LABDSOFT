using System;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Laboratorio
{
    public class Analise
    {
        public int AnaliseId { get; set; }

        // FK - Resultado Analise
        // FK - List<Metodos>

        [StringLength(100)]
        public string Nome { get; set; }

        public DateTime Data { get; set; }
    }
}
