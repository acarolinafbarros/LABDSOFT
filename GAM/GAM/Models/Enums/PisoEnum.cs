using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public static partial class GamEnums
    {
        public enum PisoEnum
        {
            [Display(Name = "A")]
            PisoA,
            [Display(Name = "B")]
            PisoB,
            [Display(Name = "C")]
            PisoC,
            [Display(Name = "Indefinido")]
            Indefinido

        }
    }
  
}
