using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Laboratorio
{
    public class Analise
    {
        public int AnaliseId { get; set; }

        public int AmostraId { get; set; }
        public Amostra Amostra { get; set; }

        public int ResultadoAnaliseId { get; set; }
        public ResultadoAnalise ResultadoAnalise { get; set; }

        public List<Metodo> Metodos { get; set; }

        [StringLength(100)]
        public string Nome { get; set; }

        public DateTime Data { get; set; }
    }
}
