using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public enum FaseDadorEnum
    {
        [Display(Name = "Aguardar Descongelação de controle")]
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
        [Display(Name = "Quarentena")]
        SetimaDadiva
    }
}
