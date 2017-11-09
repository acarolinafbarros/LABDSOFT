﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models.Laboratorio
{
    public class Espermograma
    {
        public int EspermogramaId { get; set; }

        [Display(Name = "Data de Espermograma")]
        public DateTime DataEspermograma { get; set; }

        public float Volume { get; set; }

        [StringLength(20)]
        public string Cor { get; set; }

        [StringLength(50)]
        public string Viscosidade { get; set; }

        [StringLength(50)]
        public string Liquefacao { get; set; }

        public float Ph { get; set; }

        [StringLength(250)]
        public string Observacoes { get; set; }

        [Display(Name = "Concentracao de Espermatozoides")]
        public float ConcentracaoEspermatozoides { get; set; }

        public int GrauA { get; set; }
        public int GrauB { get; set; }
        public int GrauC { get; set; }
        public int GrauD { get; set; }

        [Display(Name = "Motilidade Progressiva")]
        public int MotilidadeProgressiva { get; set; }

        [Display(Name = "Motilidade Total")]
        public int MotilidadeTotal { get; set; }

        public float Leucocitos { get; set; }

        public int Vitalidade { get; set; }

        [StringLength(250)]
        [Display(Name = "Observações")]
        public string ObservacoesConcentracao { get; set; }
    }
}
