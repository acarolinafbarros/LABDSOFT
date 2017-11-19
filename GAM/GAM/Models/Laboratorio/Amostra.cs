using GAM.Models.DadorViewModels;
using GAM.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Laboratorio
{
    public class Amostra
    {
        public int AmostraId { get; set; }

        [Display(Name = "Número do dador")]
        public int DadorId { get; set; }
        public Dador Dador { get; set; }

        public List<Analise> Analise { get; set; }

        public Espermograma Espermograma { get; set; }

        [Display(Name = "Estado da Amostra")]
        public EstadoAmostraEnum EstadoAmostra { get; set; }

        [Display(Name = "Tipo da Amostra")]
        public TipoAmostraEnum TipoAmostra { get; set; }

        [Display(Name = "Data de Recolha")]
        public DateTime DataRecolha { get; set; }

        // Localização

        [Display(Name = "Banco")]
        public GamEnums.TipoBancoEnum Banco { get; set; }

        [Display(Name = "Piso")]
        public GamEnums.PisoEnum Piso { get; set; }

        [Display(Name = "Cannister")]
        public GamEnums.CannisterEnum Cannister { get; set; }

        [Display(Name = "Globet Cor")]
        public GamEnums.GlobetCorEnum GlobetCor { get; set; }

        [Display(Name = "Globet Número")]
        public GamEnums.GlobetNumeroEnum GlobetNumero { get; set; }

        [Display(Name = "Palheta Cor")]
        public GamEnums.PalhetaCorEnum PalhetaCor { get; set; }

        [Display(Name = "Numero da Amostra")]
        public int NrAmosta => AmostraId; 
    }
}
