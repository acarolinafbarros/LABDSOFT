using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public static partial class GamEnums
    {
        public enum CannisterEnum
        {
            [Display(Name = "1")]
            Cannister1,
            [Display(Name = "2")]
            Cannister2,
            [Display(Name = "3")]
            Cannister3,
            [Display(Name = "Indefinido")]
            Indefinido

        }
    }

}
