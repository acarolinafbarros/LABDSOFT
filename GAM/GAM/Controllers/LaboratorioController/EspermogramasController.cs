using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.Laboratorio;
using GAM.Models.Enums;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Embriologista, DiretoraLaboratorio")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Espermograma
                .Where(x=>x.ValidacaoDiretorLaboratorio== ValidacaoEnum.Pendente  || x.ValidacaoEmbriologista==ValidacaoEnum.Pendente)
                .Include(e => e.Amostra);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Espermogramas/Details/5
        [Authorize(Roles = "Embriologista, DiretoraLaboratorio")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var espermograma = await _context.Espermograma
                .Include(e => e.Amostra)
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
            ViewData["AmostraId"] = new SelectList(_context.Amostra, "AmostraId", "AmostraId");
            return View();
        }

        // POST: Espermogramas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Embriologista")]
        public async Task<IActionResult> Create([Bind("EspermogramaId,AmostraId,DataEspermograma,Volume,Cor,Viscosidade,Liquefacao,Ph,Observacoes,ConcentracaoEspermatozoides,GrauA,GrauB,GrauC,GrauD,Leucocitos,Vitalidade,ObservacoesConcentracao,ValidacaoDiretorLaboratorio, ValidacaoEmbriologista")] Espermograma espermograma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(espermograma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmostraId"] = new SelectList(_context.Amostra, "AmostraId", "AmostraId", espermograma.AmostraId);
            return View(espermograma);
        }

        // GET: Espermogramas/Edit/5
        [Authorize(Roles = "DiretoraLaboratorio, Embriologista")]
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
            DropDownListValidacaoDiretorLaboratorioEnum();
            DropDownListValidacaoEmbriologistaEnum();
            ViewData["AmostraId"] = new SelectList(_context.Amostra, "AmostraId", "AmostraId", espermograma.AmostraId);
            return View(espermograma);
        }

        // POST: Espermogramas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "DiretoraLaboratorio, Embriologista")]
        public async Task<IActionResult> Edit(int id, [Bind("EspermogramaId,AmostraId,DataEspermograma,Volume,Cor,Viscosidade,Liquefacao,Ph,Observacoes,ConcentracaoEspermatozoides,GrauA,GrauB,GrauC,GrauD,Leucocitos,Vitalidade,ObservacoesConcentracao, ValidacaoDiretorLaboratorio, ValidacaoEmbriologista")] Espermograma espermograma)
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
            DropDownListValidacaoDiretorLaboratorioEnum();
            DropDownListValidacaoEmbriologistaEnum();
            ViewData["AmostraId"] = new SelectList(_context.Amostra, "AmostraId", "AmostraId", espermograma.AmostraId);
            return View(espermograma);
        }

       

        private bool EspermogramaExists(int id)
        {
            return _context.Espermograma.Any(e => e.EspermogramaId == id);
        }


        public void DropDownListValidacaoDiretorLaboratorioEnum()
        {
            ViewBag.ValidacaoDiretorLaboratorio = new SelectList(System.Enum.GetValues(typeof(ValidacaoEnum)));
        }

        public void DropDownListValidacaoEmbriologistaEnum()
        {
            ViewBag.ValidacaoEmbriologista = new SelectList(System.Enum.GetValues(typeof(ValidacaoEnum)));
        }

    }
}
