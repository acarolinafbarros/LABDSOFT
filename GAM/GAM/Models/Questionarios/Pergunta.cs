using GAM.Models.Enums;

namespace GAM.Models.Questionarios
{
    public class Pergunta
    {
        public int PerguntaId { get; set; }

        // FK - QuestionarioId

        public string Descricao { get; set; }

        public TipoRespostaEnum TipoResposta { get; set; }

        public string Resposta { get; set; }
    }
}
