using GAM.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.PMAViewModels
{
    public class PedidoGametasViewModel
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public string Centro { get; set; }

        [Display(Name = "Referência Externa")]
        public string RefExterna { get; set; }

        [Display(Name = "Estado Processo")]
        public EstadoProcesso EstadoProcessoPedido { get; set; }

        [Display(Name = "Idade - Homem")]
        public int IdadeHomem { get; set; }

        [StringLength(20)]
        [Display(Name = "Raca - Homem")]
        public EtniaEnum RacaHomem { get; set; }

        [Display(Name = "Altura - Homem")]
        public int AlturaHomem { get; set; }

        [StringLength(20)]
        [Display(Name = "Cor de Cabelo - Homem")]
        public CorCabeloEnum CorCabeloHomem { get; set; }

        [Display(Name = "Grupo Sanguineo - Homem")]
        public GrupoSanguineoEnum GrupoSanguineoHomem { get; set; }

        [StringLength(20)]
        [Display(Name = "Textura de Cabelo - Homem")]
        public TexturaCabeloEnum TexturaCabeloHomem { get; set; }

        [StringLength(20)]
        [Display(Name = "Cor de Olhos - Homem")]
        public CorOlhosEnum CorOlhosHomem { get; set; }

        [StringLength(20)]
        [Display(Name = "Cor de Pele - Homem")]
        public CorPeleEnum CorPeleHomem { get; set; }

        [Display(Name = "Idade - Mulher")]
        public int IdadeMulher { get; set; }

        [StringLength(20)]
        [Display(Name = "Raca - Mulher")]
        public EtniaEnum RacaMulher { get; set; }

        [Display(Name = "Altura - Mulher")]
        public int AlturaMulher { get; set; }

        [StringLength(20)]
        [Display(Name = "Cor de Cabelo - Mulher")]
        public CorCabeloEnum CorCabeloMulher { get; set; }

        [Display(Name = "Grupo Sanguineo - Mulher")]
        public GrupoSanguineoEnum GrupoSanguineoMulher { get; set; }

        [StringLength(20)]
        [Display(Name = "Textura de Cabelo - Mulher")]
        public TexturaCabeloEnum TexturaCabeloMulher { get; set; }

        [StringLength(20)]
        [Display(Name = "Cor de Olhos - Mulher")]
        public CorOlhosEnum CorOlhosMulher { get; set; }

        [StringLength(20)]
        [Display(Name = "Cor de Pele - Mulher")]
        public CorPeleEnum CorPeleMulher { get; set; }

        [Display(Name = "Originou Gravidez")]
        public SimNaoEnum OriginouGravidez { get; set; }

        [Display(Name = "Número Filhos")]
        public int NrFilhos { get; set; }
    }
}
