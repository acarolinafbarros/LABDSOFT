using GAM.Models.Enums;
using GAM.Models.Laboratorio;
using System;
using System.ComponentModel.DataAnnotations;
using GAM.Models.PMA;

namespace GAM.Models
{
    public class PedidoGametas
    {
        public int PedidoGametasId { get; set; }

        public int CasalId { get; set; }
        public Casal Casal { get; set; }

        public int? AmostraId { get; set; }
        public Amostra Amostra { get; set; }

        public DateTime Data { get; set; }

        public string Centro { get; set; }

        [Display(Name = "Referência Externa")]
        public string RefExterna { get; set; }

        [Display(Name = "Estado Processo")]
        public EstadoProcesso EstadoProcessoPedido { get; set; }


    }
}
