using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public enum TipoRespostaEnum
    {
        [Display(Name = "Sim | Nao")]
        SimNao,
        [Display(Name = "Resposta Aberta")]
        RespostaAberta
    }
}
