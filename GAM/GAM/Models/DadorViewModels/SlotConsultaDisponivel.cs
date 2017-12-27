using System;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.DadorViewModels
{
    public class SlotConsultaDisponivel
    {
        public int SlotConsultaDisponivelId { get; set; }

        [Display(Name = "Data da Consulta")]
        public DateTime DataConsultaDisponivel { get; set; }
    }
}
