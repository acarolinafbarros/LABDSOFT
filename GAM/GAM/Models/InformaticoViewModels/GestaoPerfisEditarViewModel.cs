using GAM.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models.InformaticoViewModels
{
    public class GestaoPerfisEditarViewModel
    {
        public string UtilizadorId { get; set; }

        [Required]
        [Display(Name = "Nome de Utilizador")]
        public string NomeUtilizador { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string AntigaPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NovaPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NovaPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmarPassword { get; set; }

        public PerfilEnum Perfil { get; set; }
    }
}
