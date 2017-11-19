using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public static partial class GamEnums
    {
        public enum TipoBancoEnum
        {
            [Display(Name = "CBS 1500")]
            Banco1,
            [Display(Name = "Lab. 30")]
            Banco2,
            [Display(Name = "Indefinido")]
            Indefinido

        }
    }
  
}
