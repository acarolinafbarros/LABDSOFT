using System.ComponentModel.DataAnnotations;

namespace GAM.Models.DadorViewModels
{
    public enum FaseDadorEnum
    {
        [Display(Name = "Primeira Dadiva")]
        PrimeiraDadiva,
        [Display(Name = "Segunda Dadiva")]
        SegundaDadiva,
        [Display(Name = "Terceira Dadiva")]
        TerceiraDadiva,
        [Display(Name = "Quarta Dadiva")]
        QuartaDadiva,
        [Display(Name = "Quinta Dadiva")]
        QuintaDadiva,
        [Display(Name = "Sexta Dadiva")]
        SextaDadiva,
        [Display(Name = "Setima Dadiva")]
        SetimaDadiva
    }
}
