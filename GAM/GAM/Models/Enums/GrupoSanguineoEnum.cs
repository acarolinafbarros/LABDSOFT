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
}
