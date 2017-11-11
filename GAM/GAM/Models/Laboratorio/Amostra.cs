using GAM.Models.DadorViewModels;
using GAM.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Laboratorio
{
    public class Amostra
    {
        public int AmostraId { get; set; }

        public int DadorId { get; set; }
        public Dador Dador { get; set; }

        public Analise Analise { get; set; }

        public Espermograma Espermograma { get; set; }

        [Display(Name = "Estado da Amostra")]
        public EstadoAmostraEnum EstadoAmostra { get; set; }

        [Display(Name = "Tipo da Amostra")]
        public TipoAmostraEnum TipoAmostra { get; set; }

        [Display(Name = "Data Recolha")]
        public DateTime DataRecolha { get; set; }

        [StringLength(50)]
        public string Localizacao { get; set; }

        [Display(Name = "Numero da Amostra")]
        public int NrAmosta { get; set; }

    }
}
