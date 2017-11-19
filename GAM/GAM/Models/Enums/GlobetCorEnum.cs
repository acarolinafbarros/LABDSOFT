using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public static partial class GamEnums
    {
        public enum GlobetCorEnum
        {
            [Display(Name = "Azul")]
            GlobetAzul,
            [Display(Name = "Verde")]
            GlobetVerde,
            [Display(Name = "Vermelho")]
            GlobetVermelho,
            [Display(Name = "Amarelo")]
            GlobetAmarelo,
            [Display(Name = "Branco")]
            GlobetBranco,
            [Display(Name = "Preto")]
            GlobetPreto,
            [Display(Name = "Indefinido")]
            Indefinido

        }
    }

}
