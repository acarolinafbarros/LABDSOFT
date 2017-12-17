using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public enum GrupoSanguineoEnum
    {
        [Display(Name = "A+")]
        APos,
        [Display(Name = "A-")]
        ANeg,
        [Display(Name = "AB+")]
        ABPos,
        [Display(Name = "AB-")]
        ABNeg,
        [Display(Name = "B+")]
        BPos,
        [Display(Name = "B-")]
        BNeg,
        [Display(Name = "O+")]
        OPos,
        [Display(Name = "O-")]
        ONeg
    }

    public enum GrupoSanguineoMatchEnum
    {
        [Display(Name = "A")]
        A,
        [Display(Name = "B")]
        B,
        [Display(Name = "AB")]
        AB,
        [Display(Name = "O")]
        O,
        
    }
}
