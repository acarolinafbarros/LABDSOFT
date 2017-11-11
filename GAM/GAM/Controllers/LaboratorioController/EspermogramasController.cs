using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.Laboratorio;

namespace GAM.Controllers.LaboratorioController
{
    public class EspermogramasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EspermogramasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Espermogramas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Espermograma.ToListAsync());
        }

        // GET: Espermogramas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var espermograma = await _context.Espermograma
                .SingleOrDefaultAsync(m => m.EspermogramaId == id);
            if (espermograma == null)
            {
                return NotFound();
            }
           
            return View(espermograma);
        }

        // GET: Espermogramas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Espermogramas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EspermogramaId,DataEspermograma,Volume,Cor,Viscosidade,Liquefacao,Ph,Observacoes,ConcentracaoEspermatozoides,GrauA,GrauB,GrauC,GrauD,MotilidadeProgressiva,MotilidadeTotal,Leucocitos,Vitalidade,ObservacoesConcentracao")] Espermograma espermograma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(espermograma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(espermograma);
        }

        // GET: Espermogramas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var espermograma = await _context.Espermograma.SingleOrDefaultAsync(m => m.EspermogramaId == id);
            if (espermograma == null)
            {
                return NotFound();
            }
            return View(espermograma);
        }

        // POST: Espermogramas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EspermogramaId,DataEspermograma,Volume,Cor,Viscosidade,Liquefacao,Ph,Observacoes,ConcentracaoEspermatozoides,GrauA,GrauB,GrauC,GrauD,MotilidadeProgressiva,MotilidadeTotal,Leucocitos,Vitalidade,ObservacoesConcentracao")] Espermograma espermograma)
        {
            if (id != espermograma.EspermogramaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(espermograma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspermogramaExists(espermograma.EspermogramaId))
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
            return View(espermograma);
        }

        // GET: Espermogramas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var espermograma = await _context.Espermograma
                .SingleOrDefaultAsync(m => m.EspermogramaId == id);
            if (espermograma == null)
            {
                return NotFound();
            }

            return View(espermograma);
        }

        // POST: Espermogramas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var espermograma = await _context.Espermograma.SingleOrDefaultAsync(m => m.EspermogramaId == id);
            _context.Espermograma.Remove(espermograma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspermogramaExists(int id)
        {
            return _context.Espermograma.Any(e => e.EspermogramaId == id);
        }
    }
}
