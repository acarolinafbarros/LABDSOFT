using GAM.Models.Enums;

namespace GAM.Models.Questionarios
{
    public class Pergunta
    {
        public int PerguntaId { get; set; }

        public int QuestionarioId { get; set; }
        public Questionario Questionario { get; set; }

        public string Descricao { get; set; }

        public TipoRespostaEnum TipoResposta { get; set; }

        public string Resposta { get; set; }
    }
}
