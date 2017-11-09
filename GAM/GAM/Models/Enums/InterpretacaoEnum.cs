using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public enum InterpretacaoEnum
    {
        [Display(Name = "Nao Reativo")]
        NaoReativo,
        Reativo,
        Negativo,
        Positivo
    }
}
