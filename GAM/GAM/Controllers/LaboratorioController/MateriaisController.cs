using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.RegistoMaterial;
using Microsoft.AspNetCore.Authorization;

namespace GAM.Controllers.LaboratorioController
{
    public class MateriaisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MateriaisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Materiais
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Material.Include(m => m.Espermograma);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Materiais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Material
                .Include(m => m.Espermograma)
                .SingleOrDefaultAsync(m => m.MaterialId == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // GET: Materiais/Create
        [Authorize(Roles = "Embriologista")]
        public IActionResult Create()
        {
            ViewData["EspermogramaId"] = new SelectList(_context.Espermograma, "EspermogramaId", "EspermogramaId");
            return View();
        }

        // POST: Materiais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Embriologista")]
        public async Task<IActionResult> Create([Bind("MaterialId,EspermogramaId,Nome,Lote,QuantidadeUtilizada,Categoria")] Material material)
        {
            if (ModelState.IsValid)
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspermogramaId"] = new SelectList(_context.Espermograma, "EspermogramaId", "EspermogramaId", material.EspermogramaId);
            return View(material);
        }

        // GET: Materiais/Edit/5
        [Authorize(Roles = "Embriologista")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Material.SingleOrDefaultAsync(m => m.MaterialId == id);
            if (material == null)
            {
                return NotFound();
            }
            ViewData["EspermogramaId"] = new SelectList(_context.Espermograma, "EspermogramaId", "EspermogramaId", material.EspermogramaId);
            return View(material);
        }

        // POST: Materiais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Embriologista")]
        public async Task<IActionResult> Edit(int id, [Bind("MaterialId,EspermogramaId,Nome,Lote,QuantidadeUtilizada,Categoria")] Material material)
        {
            if (id != material.MaterialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(material.MaterialId))
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
            ViewData["EspermogramaId"] = new SelectList(_context.Espermograma, "EspermogramaId", "EspermogramaId", material.EspermogramaId);
            return View(material);
        }

        // GET: Materiais/Delete/5
        [Authorize(Roles = "Embriologista")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Material
                .Include(m => m.Espermograma)
                .SingleOrDefaultAsync(m => m.MaterialId == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // POST: Materiais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Embriologista")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Material.SingleOrDefaultAsync(m => m.MaterialId == id);
            _context.Material.Remove(material);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialExists(int id)
        {
            return _context.Material.Any(e => e.MaterialId == id);
        }
    }
}
