using GAM.Models.Enums;
using GAM.Models.RegistoMaterial;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models.Laboratorio
{
    public class Espermograma
    {
        public int EspermogramaId { get; set; }

        public int AmostraId { get; set; }
        public Amostra Amostra { get; set; }

        public List<Material> Materiais { get; set; }

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
        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "Concentração de Espermatozoides")]
        public float ConcentracaoEspermatozoides { get; set; }

        public int GrauA { get; set; }
        public int GrauB { get; set; }
        public int GrauC { get; set; }
        public int GrauD { get; set; }

        [Display(Name = "Motilidade Progressiva")]
        public int MotilidadeProgressiva => GrauA + GrauB;

        [Display(Name = "Motilidade Total")]
        public int MotilidadeTotal => GrauA + GrauB + GrauC;

        public float Leucocitos { get; set; }

        public int Vitalidade { get; set; }

        [StringLength(250)]
        [Display(Name = "Observações")]
        public string ObservacoesConcentracao { get; set; }

        [Display(Name = "Validacao Medico")]
        public ValidacaoEnum ValidacaoDiretorLaboratorio { get; set; }

        [Display(Name = "Validacao Laboratorio")]
        public ValidacaoEnum ValidacaoEmbriologista { get; set; }

    }
}
