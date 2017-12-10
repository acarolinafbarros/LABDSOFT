using System.ComponentModel.DataAnnotations;
namespace GAM.Models.Enums
{
    public enum EstadoProcesso
    {
        [Display(Name = "Em Análise")]
        EmAnalise,
        [Display(Name = "Resultados Pedido")]
        RecebiResultadosPedido,
        [Display(Name = "Resultados Casal")]
        RegisteiResultadosCasal
    }
}
