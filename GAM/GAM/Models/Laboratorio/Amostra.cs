using GAM.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Laboratorio
{
    public class Amostra
    {
        public int AmostraId { get; set; }

        [Display(Name = "Estado da Amostra")]
        public EstadoAmostraEnum EstadoAmostra { get; set; }

        [Display(Name = "Data Recolha")]
        public DateTime DataRecolha { get; set; }

        [StringLength(50)]
        public string Localizacao { get; set; }
    }
}
