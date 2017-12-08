using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models.Laboratorio
{
    public class LocalizacaoAmostra
    {
        public int LocalizacaoAmostraId { get; set; }

        public int? AmostraId { get; set; }
        public Amostra Amostra { get; set; }

        [Display(Name = "Banco")]
        public string Banco { get; set; }

        [Display(Name = "Piso")]
        public string Piso { get; set; }

        [Display(Name = "Cannister")]
        public int Cannister { get; set; }

        [Display(Name = "Globet Cor")]
        public string GlobetCor { get; set; }

        [Display(Name = "Globet Número")]
        public int GlobetNumero { get; set; }

        [Display(Name = "Palheta Cor")]
        public string PalhetaCor { get; set; }
    }
}
