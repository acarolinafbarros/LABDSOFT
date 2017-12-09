using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public enum EstadoAmostraEnum
    {
        [Display(Name = "Por Analisar")]
        PorAnalisar,
        [Display(Name = "Em Analise")]
        EmAnalise,
        [Display(Name = "Analisada")]
        Analisada,
        [Display(Name = "Criopreservada")]
        Criopreservada,
        Enviada
    }
}
