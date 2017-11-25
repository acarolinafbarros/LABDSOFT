using GAM.Models.DadorViewModels;

namespace GAM.Models.Questionarios
{
    public class Resposta
    {
        public int RespostaId { get; set; }

        public int PerguntaId { get; set; }
        public Pergunta Pergunta { get; set; }

        public int DadorId { get; set; }
        public Dador Dador { get; set; }

        public string TextoResposta { get; set; }

    }
}
