using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Enums
{
    public enum GrauEscolaridadeEnum
    {
        [Display(Name = "Ensino Basico")]
        EnsinoBasico,
        [Display(Name = "Ensino Secundario")]
        EnsinoSecundario,
        Licenciatura,
        Mestrado,
        Doutoramento
    }
}
