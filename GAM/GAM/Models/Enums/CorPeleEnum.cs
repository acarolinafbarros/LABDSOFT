
namespace GAM.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum CorPeleEnum
    {
        [Display(Name = "Muito Clara")]
        MuitoClara,
        [Display(Name = "Clara")]
        Clara,
        [Display(Name = "Escura")]
        Escura,
        [Display(Name = "Muito Escura")]
        MuitoEscura,
        [Display(Name = "Outros")]
        Outros
    }
}
