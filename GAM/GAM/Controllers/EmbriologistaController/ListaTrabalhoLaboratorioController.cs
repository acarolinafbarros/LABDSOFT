using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace GAM.Controllers.EmbriologistaController
{
    [Authorize(Roles = "Embriologista")]
    public class ListaTrabalhoLaboratorioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListaTrabalhoLaboratorioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ListaTrabalhoLaboratorio
        public async Task<IActionResult> Index()
        {
            // Para Amostras com TipoAmostraEnum = Espermatozoide listar:
            //      Amostras com EstadoAmostraEnum = PorAnalisar -> Espermogramas Pendentes para a amostra id...
            //      Amostras com EstadoAmostraEnum = EmAnalise -> Espermograma em processo para a amostra id...
            //      Amostras com EstadoAmostraEnum = Analisada -> Espermograma pendente para aprovação para a amostra...

            var applicationDbContext = _context.Amostra
                .Where(x => x.TipoAmostra == TipoAmostraEnum.Espermatozoide)
                .Where(x => x.EstadoAmostra != EstadoAmostraEnum.Criopreservada )
                .OrderBy(x=>x.EstadoAmostra).Include(a => a.Dador).Include(x => x.LocalizacaoAmostra);


            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ListaTrabalhoLaboratorio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amostra = await _context.Amostra
                .Include(a => a.Dador).Include(x => x.LocalizacaoAmostra)
                .SingleOrDefaultAsync(m => m.AmostraId == id);
            if (amostra == null)
            {
                return NotFound();
            }

            return View(amostra);
        }

        private bool AmostraExists(int id)
        {
            return _context.Amostra.Any(e => e.AmostraId == id);
        }
    }
}
