﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.DadorViewModels;
using GAM.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace GAM.Controllers.MedicoController
{
    [Authorize(Roles = "Medico")]
    public class ValidacaoCicloDadivaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ValidacaoCicloDadivaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ValidacaoCicloDadiva
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dador.Where(d => d.DadosDador == ValidacaoEnum.Aceite).Where(d => d.EstadoDador == EstadoDadorEnum.PendenteAprovacao).ToListAsync());
        }

        // GET: ValidacaoCicloDadiva/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dador = await _context.Dador.SingleOrDefaultAsync(m => m.DadorId == id);
            if (dador == null)
            {
                return NotFound();
            }
            return View(dador);
        }

        // POST: ValidacaoCicloDadiva/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DadorId,Nome,Morada,DataNasc,LocalNasc,DocIdentificacao,Nacionalidade,Profissao,GrauEscolaridade,EstadoCivil,Altura,Peso,CorPele,CorOlhos,CorCabelo,TexturaCabelo,GrupoSanguineo,Etnia,IniciaisDador,FaseDador,EstadoDador,DadosDador,NumAbortos,TotalGestacoes")] Dador dador)
        {
            if (id != dador.DadorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DadorExists(dador.DadorId))
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
            return View(dador);
        }

        private bool DadorExists(int id)
        {
            return _context.Dador.Any(e => e.DadorId == id);
        }
    }
}
