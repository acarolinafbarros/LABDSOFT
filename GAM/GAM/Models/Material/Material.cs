using GAM.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.Material
{
    public class Material
    {
        public int MaterialId { get; set; }

        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(20)]
        public string Lote { get; set; }

        public int StockDisponivel { get; set; }

        public CategoriaEnum Categoria { get; set; }
    }
}
