using GAM.Models.Enums;
using GAM.Models.Laboratorio;
using GAM.Models.Questionarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.DadorViewModels
{
    public class Dador
    {
        // Variaveis
        public int DadorId { get; set; }

        public List<Amostra> Amostras { get; set; }

        public List<Consulta> Consultas { get; set; }

        public List<Resposta> Resposta { get; set; }

        //[Required]
        [StringLength(256)]
        [RegularExpression("^[A-Z][-a-z]+[ ][A-Z][-a-z]+$")]
        public string Nome { get; set; }

        ////[Required]
        [StringLength(256)]
        public string Morada { get; set; }
                                                        
        //[Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNasc { get; set; }

        //[Required]
        [StringLength(256)]
        [Display(Name = "Local de Nascimento")]
        public string LocalNasc { get; set; }

        //[Required]
        [StringLength(256)]
        [Display(Name = "Documento de Identificacao")]
        public string DocIdentificacao { get; set; }

        //[Required]
        [StringLength(256)]
        public string Nacionalidade { get; set; }

        //[Required]
        [StringLength(256)]
        public string Profissao { get; set; }

        //[Required]
        [Display(Name = "Grau de Escolaridade")]
        public GrauEscolaridadeEnum GrauEscolaridade { get; set; }

        //[Required]
        [Display(Name = "Estado Civil")]
        public EstadoCivilEnum EstadoCivil { get; set; }

        //[Required]
        [Display(Name = "Número de filhos anteriores à dávida")]
        public int NumFilhos { get; set; }

        //[Required]
        public int Altura { get; set; }

        //[Required]
        public float Peso { get; set; }

        //[Required]
        [Display(Name = "Cor de Pele")]
        public CorPeleEnum CorPele { get; set; }

        //[Required]
        
        [Display(Name = "Cor de Olhos")]
        public CorOlhosEnum CorOlhos { get; set; }

        //[Required]
        [Display(Name = "Cor de Cabelo")]
        public CorCabeloEnum CorCabelo { get; set; }

        //[Required]
        [Display(Name = "Textura de Cabelo")]
        public TexturaCabeloEnum TexturaCabelo { get; set; }

        //[Required]
        [Display(Name = "Grupo Sanguineo")]
        public GrupoSanguineoEnum GrupoSanguineo { get; set; }

        //[Required]
        public EtniaEnum Etnia { get; set; }

        [StringLength(256)]
        [Display(Name = "Iniciais do Dador")]
        public string IniciaisDador { get; set; }

        // Variaveis de auxilio ao sistema
        [Display(Name = "Fase do Processo")]
        public FaseDadorEnum FaseDador { get; set; }

        [Display(Name = "Estado do Processo")]
        public EstadoDadorEnum EstadoDador { get; set; }

        [Display(Name = "Validação de Dados do Dador")]
        public ValidacaoEnum DadosDador { get; set; }

        [Display(Name = "Validação do Inquerito da Assistente Social")]
        public ValidacaoEnum ValidacaoInqueritoAS { get; set; }

        public int NumAbortos { get; set; }

        public int TotalGestacoes { get; set; }

    }
}
