using GAM.Models.Questionarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models.DadorViewModels
{
    public class InqueritoAssistenteSocialViewModel
    {
        [StringLength(256)]
        [RegularExpression("^[A-Z][-a-z]+[ ][A-Z][-a-z]+$")]
        public string Nome { get; set; }

        [StringLength(256)]
        [Display(Name = "Documento de Identificacao")]
        public string DocIdentificacao { get; set; }

        public int QuestionarioId { get; set; }

        public List<Pergunta> Perguntas { get; set; }

        public List<string> Respostas { get; set; }
    }
}
