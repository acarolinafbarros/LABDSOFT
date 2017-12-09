using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAM.Models.DiretoraBancoViewModels
{
    public class ConsultaDestinosGametasViewModel
    {
        [Display(Name = "Numero do Envio")]
        public int NrEnvio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data do Envio")]
        public DateTime DataEnvio { get; set; }
        
        [Display(Name = "Nome do Dador")]
        public string NomeDador { get; set; }

        [Display(Name = "Numero da Amostra")]
        public int NrAmostra;

        [Display(Name = "Centro de Destino")]
        public string Centro { get; set; }

        [Display(Name = "Referência Externa do Centro")]
        public string RefExterna { get; set; }
    }
}
