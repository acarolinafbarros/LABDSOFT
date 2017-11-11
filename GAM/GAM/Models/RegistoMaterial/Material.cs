using GAM.Models.Enums;
using GAM.Models.Laboratorio;
using System.ComponentModel.DataAnnotations;

namespace GAM.Models.RegistoMaterial
{
    public class Material
    {
        public int MaterialId { get; set; }

        public int EspermogramaId { get; set; }
        public Espermograma Espermograma { get; set; }

        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(20)]
        public string Lote { get; set; }

        public int QuantidadeUtilizada { get; set; }

        public CategoriaEnum Categoria { get; set; }
    }
}
