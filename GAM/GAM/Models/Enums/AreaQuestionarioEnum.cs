using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public static partial class GamEnums
    {
        public enum AreaQuestionarioEnum
        {
            Medico,
            [Display(Name = "Assistente Social")]
            AssistenteSocial
        }
    }
  
}
