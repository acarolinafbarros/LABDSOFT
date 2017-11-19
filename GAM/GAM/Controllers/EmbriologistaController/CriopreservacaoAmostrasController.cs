using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.Laboratorio;

namespace GAM.Controllers.EmbriologistaController
{
    using GAM.Models.Enums;

    public class CriopreservacaoAmostrasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CriopreservacaoAmostrasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CriopreservacaoAmostras
        public async Task<IActionResult> Index()
        {
         
            var applicationDbContext = _context.Amostra.Where(a => (a.TipoAmostra == TipoAmostraEnum.Sangue) 
                                                                    && (a.EstadoAmostra == EstadoAmostraEnum.Analisada)
                                                                    && (a.Banco == GamEnums.TipoBancoEnum.Indefinido)
                                                                    && (a.Piso == GamEnums.PisoEnum.Indefinido)
                                                                    && (a.Cannister == GamEnums.CannisterEnum.Indefinido)
                                                                    && (a.GlobetCor == GamEnums.GlobetCorEnum.Indefinido)
                                                                    && (a.GlobetNumero == GamEnums.GlobetNumeroEnum.Indefinido)
                                                                    && (a.PalhetaCor == GamEnums.PalhetaCorEnum.Indefinido)) ;

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CriopreservacaoAmostras/Details/5
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


        // GET: CriopreservacaoAmostras/Edit/5
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

        // POST: CriopreservacaoAmostras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
