using System;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models
{
    public class PedidoGametas
    {
        public int PedidoGametasId { get; set; }

        public int CasalId { get; set; }
        public Casal Casal { get; set; }

        public DateTime Data { get; set; }

        public string Centro { get; set; }

        [Display(Name = "Referência Externa")]
        public string RefExterna { get; set; }
    }
}
