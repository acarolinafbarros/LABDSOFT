using GAM.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.PMAViewModels
{
    public class Casal
    {
        // Variaveis
        public int CasalID { get; set; }

        public int IdadeHomem { get; set; }
        public int IdadeMulher { get; set; }

        [StringLength(20)]
        [Display(Name = "Raca - Homem")]
        public string RacaHomem { get; set; }
        [StringLength(20)]
        [Display(Name = "Raca - Mulher")]
        public string RacaMulher { get; set; }

        public int AlturaHomem { get; set; }
        public int AlturaMulher { get; set; }

        [StringLength(20)]
        [Display(Name = "Cor de Cabelo - Homem")]
        public string CorCabeloHomem { get; set; }
        [StringLength(20)]
        [Display(Name = "Cor de Cabelo - Mulher")]
        public string CorCabeloMulher { get; set; }

        [Display(Name = "Grupo Sanguineo - Homem")]
        public GrupoSanguineoEnum GrupoSanguineoHomem { get; set; }
        [Display(Name = "Grupo Sanguineo - Mulher")]
        public GrupoSanguineoEnum GrupoSanguineoMulher { get; set; }

        [StringLength(20)]
        [Display(Name = "Textura de Cabelo - Homem")]
        public string TexturaCabeloHomem { get; set; }
        [StringLength(20)]
        [Display(Name = "Textura de Cabelo - Mulher")]
        public string TexturaCabeloMulher { get; set; }

        [StringLength(20)]
        [Display(Name = "Cor de Olhos - Homem")]
        public string CorOlhosHomem { get; set; }
        [StringLength(20)]
        [Display(Name = "Cor de Olhos - Mulher")]
        public string CorOlhosMulher { get; set; }

        [StringLength(20)]
        [Display(Name = "Cor de Pele - Homem")]
        public string CorPeleHomem { get; set; }
        [StringLength(20)]
        [Display(Name = "Cor de Pele - Mulher")]
        public string CorPeleMulher { get; set; }
    }
}
