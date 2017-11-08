using System.ComponentModel.DataAnnotations;

namespace GAM.Models.DadorViewModels
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
