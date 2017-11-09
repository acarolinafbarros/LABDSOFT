using GAM.Models.Enums;

namespace GAM.Models.Questionarios
{
    public class Questionario
    {
        public int QuestionarioId { get; set; }

        public AreaQuestionarioEnum Area { get; set; }

        // List<Perguntas>
    }
}
