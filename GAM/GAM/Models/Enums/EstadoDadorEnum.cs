using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public enum EstadoDadorEnum
    {
        [Display(Name = "Processo em espera para abertura")]
        ProcessoInativo,
        [Display(Name = "Pendente de Aprovacao")]
        PendenteAprovacao,
        [Display(Name = "Aceite")]
        Aceite,
        [Display(Name = "Rejeitado")]
        Rejeitado
    }
}
