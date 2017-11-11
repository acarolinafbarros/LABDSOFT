using System.ComponentModel.DataAnnotations;

namespace GAM.Models.DiretorGeralViewModels
{
    public class DiretorGeral
    {
        // Variaveis
        public int DiretorGeralId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
    }
}
