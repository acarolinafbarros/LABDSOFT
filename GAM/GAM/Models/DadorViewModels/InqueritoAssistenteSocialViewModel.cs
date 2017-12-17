using GAM.Models.Questionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models.DadorViewModels
{
    public class InqueritoAssistenteSocialViewModel
    {
        public string Nome { get; set; }

        public int QuestionarioId { get; set; }

        public List<Pergunta> Perguntas { get; set; }

        public List<string> Respostas { get; set; }
    }
}
