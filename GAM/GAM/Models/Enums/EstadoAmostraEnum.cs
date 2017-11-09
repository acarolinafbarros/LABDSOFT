using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public enum EstadoAmostraEnum
    {
        [Display(Name = "Em Analise")]
        EmAnalise,
        Analisada,
        Criopreservada
    }
}
