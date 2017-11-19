using GAM.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Laboratorio
{
    public class ResultadoAnalise
    {
        public int ResultadoAnaliseId { get; set; }

        public List<Analise> Analises { get; set; }

        public DateTime Data { get; set; }

        [StringLength(100)]
        [Display(Name = "Nome Medico")]
        public string NomeMedico { get; set; }

        [StringLength(100)]
        [Display(Name = "Nome Embriologista")]
        public string NomeEmbriologista { get; set; }
        
        [Display(Name = "Validacao Medico")]
        public ValidacaoEnum ValidacaoMedico { get; set; }

        [Display(Name = "Validacao Laboratorio")]
        public ValidacaoEnum ValidacaoLaboratorio { get; set; }
    }
}
