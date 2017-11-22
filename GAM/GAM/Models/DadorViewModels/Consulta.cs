using System;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.DadorViewModels
{
    public class Consulta
    {
        public int ConsultaId { get; set; }

        [Display(Name = "Número do doador")]
        public int DadorId { get; set; }
        public Dador Dador { get; set; }

        [Display(Name = "Data da Consulta")]
        public DateTime DataConsulta { get; set; }

    }
}
