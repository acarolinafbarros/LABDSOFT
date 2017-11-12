using GAM.Models.Enums;
using GAM.Models.Laboratorio;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.RegistoMaterial
{
    public class Material
    {
        [Display(Name = "Material Id")]
        public int MaterialId { get; set; }

        [Display(Name = "Espermograma Id")]
        public int EspermogramaId { get; set; }

        public Espermograma Espermograma { get; set; }

        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(20)]
        public string Lote { get; set; }

        [Display(Name = "Quantidade Utilizada")]
        public int QuantidadeUtilizada { get; set; }

        public CategoriaEnum Categoria { get; set; }
    }
}
