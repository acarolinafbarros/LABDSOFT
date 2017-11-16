using System;
using System.Collections.Generic;
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
    [Authorize(Roles ="Enfermeiro, EnfermeiroCoordenador")]
    public class AmostrasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AmostrasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Amostras
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Amostra.Include(a => a.Dador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Amostras/Details/5
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
        //[AuthLog(Roles = "Enfermeira")]
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
        public async Task<IActionResult> Create([Bind("AmostraId,DadorId,EstadoAmostra,TipoAmostra,DataRecolha,Localizacao,NrAmosta")] Amostra amostra)
        {
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
        public async Task<IActionResult> Edit(int id, [Bind("AmostraId,DadorId,EstadoAmostra,TipoAmostra,DataRecolha,Localizacao,NrAmosta")] Amostra amostra)
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

        // GET: Amostras/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Amostras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amostra = await _context.Amostra.SingleOrDefaultAsync(m => m.AmostraId == id);
            _context.Amostra.Remove(amostra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AmostraExists(int id)
        {
            return _context.Amostra.Any(e => e.AmostraId == id);
        }
    }
}
