using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public static partial class GamEnums
    {
        public enum PalhetaCorEnum
        {
            [Display(Name = "Azul")]
            PalhetaAzul,
            [Display(Name = "Verde")]
            PalhetaVerde,
            [Display(Name = "Vermelho")]
            PalhetaVermelho,
            [Display(Name = "Amarelo")]
            PalhetaAmarelo,
            [Display(Name = "Branco")]
            PalhetaBranco,
            [Display(Name = "Preto")]
            PalhetaPreto,
            [Display(Name = "Indefinido")]
            Indefinido

        }
    }

}
