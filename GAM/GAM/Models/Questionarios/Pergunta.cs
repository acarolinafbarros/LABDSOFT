using GAM.Models.Enums;
using System.Collections.Generic;

namespace GAM.Models.Questionarios
{
    public class Pergunta
    {
        public int PerguntaId { get; set; }

        public int QuestionarioId { get; set; }
        public Questionario Questionario { get; set; }

        public List<Resposta> Respostas { get; set; }

        public string Descricao { get; set; }

        public TipoRespostaEnum TipoResposta { get; set; }

        public bool Apagado { get; set; }
    }
}
