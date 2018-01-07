using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GAM.Models.DadorViewModels;

namespace GAM.Models.PMA
{
    public class MatchStats
    {
        public int MatchStatsId { get; set; }
        public bool CorOlhosHomem { get; set; }
        public bool CorOlhosMulher { get; set; }
        public bool CorCabeloHomem { get; set; }
        public bool CorCabeloMulher { get; set; }
        public bool CorPeleHomem { get; set; }
        public bool CorPeleMulher { get; set; }
        public bool GrupoSanguineoHomem { get; set; }
        public bool GrupoSanguineoMulher { get; set; }
        public bool RacaHomem { get; set; }
        public bool RacaMulher { get; set; }
        public bool TexturaCabeloHomem { get; set; }
        public bool TexturaCabeloMulher { get; set; }
        public int? CasalId { get; set; }
        public int? DadorId { get; set; }
        public Casal Casal { get; set; }
        public Dador Dador { get; set; }
    }

    public class MatchStatsInfo
    {
        // int NrRecords { get; set; }
        public int CorOlhosHomem { get; set; }
        public int CorOlhosMulher { get; set; }
        public int CorCabeloHomem { get; set; }
        public int CorCabeloMulher { get; set; }
        public int CorPeleHomem { get; set; }
        public int CorPeleMulher { get; set; }
        public int GrupoSanguineoHomem { get; set; }
        public int GrupoSanguineoMulher { get; set; }
        public int RacaHomem { get; set; }
        public int RacaMulher { get; set; }
        public int TexturaCabeloHomem { get; set; }
        public int TexturaCabeloMulher { get; set; }
    }

    public class ValidaMatchViewModel
    {
        [Display(Name = "Selecção")]
        public int? MatchStatsId { get; set; }
        [Display(Name = "Centro")]
        public string Centro { get; set; }
        [Display(Name = "Data")]
        public DateTime? Data { get; set; }
        [Display(Name = "Casal")]
        public int? CasalId { get; set; }
        [Display(Name = "Dador")]
        public string Dador { get; set; }
        [Display(Name = "Amostra")]
        public int? AmostraId { get; set; }
        [Display(Name = "Pedido de Gametas")]
        public int? PedidoGametasId { get; set; }
        /*
         *   <th>
                    @Html.DisplayNameFor(model => model.MatchStatsId)
                </th>
                <th>
                    @Html.DisplayNameFor(model => model.Casal.PedidoGametas.Centro)
                </th>
                <th>
                    @Html.DisplayNameFor(model => model.Casal.PedidoGametas.Data)
                </th>
                <th>
                    @Html.DisplayNameFor(model => model.CasalId)
                </th>
                <th>
                    @Html.DisplayNameFor(model => model.DadorId)
                </th>
                <th>
                    @Html.DisplayNameFor(model => model.Casal.PedidoGametas.AmostraId)
                </th>
                @item.Casal.PedidoGametas.PedidoGametasId"
         */
    }

}
