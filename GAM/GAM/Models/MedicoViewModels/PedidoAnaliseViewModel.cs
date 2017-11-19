using GAM.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.MedicoViewModels
{
    public class PedidoAnaliseViewModel
    {
        [Display (Name = "Nome do Dador")]
        public string NomeDador { get; set; }

        [Display(Name = "ID da Amostra")]
        public int AmostraId { get; set; }

        [Display(Name = "ID da Analise A")]
        public int AnaliseIdA { get; set; }

        [Display(Name = "ID da Analise B")]
        public int AnaliseIdB { get; set; }

        [Display(Name = "Estado da Amostra")]
        public EstadoAmostraEnum EstadoAmostra { get; set; }
    }
}
