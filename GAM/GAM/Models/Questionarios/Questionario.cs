using GAM.Models.Enums;
using System.Collections.Generic;

namespace GAM.Models.Questionarios
{
    public class Questionario
    {
        public int QuestionarioId { get; set; }

        public List<Pergunta> Perguntas { get; set; }

        public GamEnums.AreaQuestionarioEnum Area { get; set; }
    }
}
