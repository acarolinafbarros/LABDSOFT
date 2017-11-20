using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.Laboratorio;
using GAM.Models.MedicoViewModels;
using GAM.Models.Enums;
using System;

namespace GAM.Controllers.MedicoController
{
    public class PedidoAnaliseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoAnaliseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResultadoAnalises
        public IActionResult Index()
        {
            var allDadores = _context.Dador.ToList();
            ICollection<PedidoAnaliseViewModel> listDadoresAmostrasPendentes = new List<PedidoAnaliseViewModel>();

            foreach (var d in allDadores)
            {
                var amostraDadorPendente = _context.Amostra
                    .Where(a => a.DadorId == d.DadorId)
                    .Where(a => a.EstadoAmostra == EstadoAmostraEnum.EmAnalise)
                    .Where(a => a.TipoAmostra == TipoAmostraEnum.Sangue)
                    .Select(s => new PedidoAnaliseViewModel { NomeDador = d.Nome, AmostraId = s.AmostraId, EstadoAmostra = s.EstadoAmostra})
                    .FirstOrDefault();

                if (amostraDadorPendente != null)
                {
                    listDadoresAmostrasPendentes.Add(amostraDadorPendente);
                }
            }

            return View(listDadoresAmostrasPendentes);
        }

        // GET: ResultadoAnalises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultadoAnalise = await _context.ResultadoAnalise.SingleOrDefaultAsync(m => m.ResultadoAnaliseId == id);
            if (resultadoAnalise == null)
            {
                return NotFound();
            }
            return View(resultadoAnalise);
        } 

        private bool ResultadoAnaliseExists(int id)
        {
            return _context.ResultadoAnalise.Any(e => e.ResultadoAnaliseId == id);
        }
    }
}
