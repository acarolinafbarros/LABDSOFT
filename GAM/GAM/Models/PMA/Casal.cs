using GAM.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models
{
    public class Casal
    {
        // Variaveis
        public int CasalID { get; set; }

        public PedidoGametas PedidoGametas { get; set; }

        public int IdadeHomem { get; set; }
        public int IdadeMulher { get; set; }

        [Display(Name = "Raça - Homem")]
        public EtniaEnum RacaHomem { get; set; }

        [Display(Name = "Raça - Mulher")]
        public EtniaEnum RacaMulher { get; set; }

        public int AlturaHomem { get; set; }
        public int AlturaMulher { get; set; }

        [Display(Name = "Cor de Cabelo - Homem")]
        public CorCabeloEnum CorCabeloHomem { get; set; }

        [Display(Name = "Cor de Cabelo - Mulher")]
        public CorCabeloEnum CorCabeloMulher { get; set; }

        [Display(Name = "Grupo Sanguineo - Homem")]
        public GrupoSanguineoEnum GrupoSanguineoHomem { get; set; }
        [Display(Name = "Grupo Sanguineo - Mulher")]
        public GrupoSanguineoEnum GrupoSanguineoMulher { get; set; }

        [Display(Name = "Textura de Cabelo - Homem")]
        public TexturaCabeloEnum TexturaCabeloHomem { get; set; }

        [Display(Name = "Textura de Cabelo - Mulher")]
        public TexturaCabeloEnum TexturaCabeloMulher { get; set; }

        [Display(Name = "Cor de Olhos - Homem")]
        public CorOlhosEnum CorOlhosHomem { get; set; }

        [Display(Name = "Cor de Olhos - Mulher")]
        public CorOlhosEnum CorOlhosMulher { get; set; }

        [Display(Name = "Cor de Pele - Homem")]
        public CorPeleEnum CorPeleHomem { get; set; }

        [Display(Name = "Cor de Pele - Mulher")]
        public CorPeleEnum CorPeleMulher { get; set; }

        [Display(Name = "Originou Gravidez")]
        public SimNaoEnum OriginouGravidez { get; set; }

        [Display(Name = "Número Filhos")]
        public int NrFilhos { get; set; }
    }
}
