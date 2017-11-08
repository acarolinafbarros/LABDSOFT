using System;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.DadorViewModels
{
    public class Dador
    {
        // Variaveis
        public int DadorID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Morada { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNasc { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Local de Nascimento")]
        public string LocalNasc { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Documento de Identificacao")]
        public string DocIdentificacao { get; set; }

        [Required]
        [StringLength(50)]
        public string Nacionalidade { get; set; }

        [Required]
        [StringLength(50)]
        public string Profissao { get; set; }

        [Required]
        [Display(Name = "Grau de Escolaridade")]
        public GrauEscolaridadeEnum GrauEscolaridade { get; set; }

        [Required]
        [Display(Name = "Estado Civil")]
        public EstadoCivilEnum EstadoCivil { get; set; }

        [Required]
        public int Altura { get; set; }

        [Required]
        public float Peso { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Cor de Pele")]
        public string CorPele { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Cor de Olhos")]
        public string CorOlhos { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Cor de Cabelo")]
        public string CorCabelo { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Textura de Cabelo")]
        public string TexturaCabelo { get; set; }

        [Required]
        [Display(Name = "Grupo Sanguineo")]
        public GrupoSanguineoEnum GrupoSanguineo { get; set; }

        [Required]
        [StringLength(10)]
        public string Etnia { get; set; }

        [StringLength(20)]
        public string CodigoDador { get; set; }

        // Variaveis de auxilio ao sistema
        public FaseDadorEnum FaseDador { get; set; }
        public EstadoDadorEnum EstadoDador { get; set; }
        public int NumAbortos { get; set; }
        public int TotalGestacoes { get; set; }
    }
}
