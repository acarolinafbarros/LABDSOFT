using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.Laboratorio;
using Microsoft.AspNetCore.Authorization;

namespace GAM.Controllers.LaboratorioController
{
    public class AmostrasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AmostrasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Amostras
        [Authorize(Roles = "Enfermeiro, EnfermeiroCoordenador")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Amostra.Include(a => a.Dador);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Embriologista")]
        public async Task<IActionResult> Allocate()
        {
            var applicationDbContext = _context.Amostra.Include(a => a.Dador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Amostras/Details/5
        [Authorize(Roles = "Enfermeiro, EnfermeiroCoordenador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amostra = await _context.Amostra
                .Include(a => a.Dador)
                .SingleOrDefaultAsync(m => m.AmostraId == id);
            if (amostra == null)
            {
                return NotFound();
            }

            return View(amostra);
        }


        // GET: Amostras/Create
        [Authorize(Roles = "Enfermeiro, EnfermeiroCoordenador")]
        public IActionResult Create()
        {
            ViewData["DadorId"] = new SelectList(_context.Dador, "DadorId", "DadorId");
            return View();
        }

        // POST: Amostras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Enfermeiro, EnfermeiroCoordenador")]
        public async Task<IActionResult> Create([Bind("AmostraId,DadorId,EstadoAmostra,TipoAmostra,DataRecolha,NrAmosta")] Amostra amostra)
        {
            amostra.Banco = Models.Enums.GamEnums.TipoBancoEnum.Indefinido;
            amostra.Piso = Models.Enums.GamEnums.PisoEnum.Indefinido;
            amostra.Cannister = Models.Enums.GamEnums.CannisterEnum.Indefinido;
            amostra.GlobetCor = Models.Enums.GamEnums.GlobetCorEnum.Indefinido;
            amostra.GlobetNumero = Models.Enums.GamEnums.GlobetNumeroEnum.Indefinido;
            amostra.PalhetaCor = Models.Enums.GamEnums.PalhetaCorEnum.Indefinido;

            if (ModelState.IsValid)
            {       
                _context.Add(amostra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DadorId"] = new SelectList(_context.Dador, "DadorId", "DadorId", amostra.DadorId);
            return View(amostra);
        }

        // GET: Amostras/Edit/5
        [Authorize(Roles = "Enfermeiro, EnfermeiroCoordenador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amostra = await _context.Amostra.SingleOrDefaultAsync(m => m.AmostraId == id);
            if (amostra == null)
            {
                return NotFound();
            }
            ViewData["DadorId"] = new SelectList(_context.Dador, "DadorId", "DadorId", amostra.DadorId);
            return View(amostra);
        }

        // POST: Amostras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Enfermeiro, EnfermeiroCoordenador")]
        public async Task<IActionResult> Edit(int id, [Bind("AmostraId,DadorId,EstadoAmostra,TipoAmostra,DataRecolha,Banco,Piso,Cannister,GlobetCor,GlobetNumero,PalhetaCor,NrAmosta")] Amostra amostra)
        {
            if (id != amostra.AmostraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amostra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmostraExists(amostra.AmostraId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DadorId"] = new SelectList(_context.Dador, "DadorId", "DadorId", amostra.DadorId);
            return View(amostra);
        }

        

        private bool AmostraExists(int id)
        {
            return _context.Amostra.Any(e => e.AmostraId == id);
        }
    }
}
