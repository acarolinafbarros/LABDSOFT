using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.PMAViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using GAM.Models;
using System;

namespace GAM.Controllers.PMAController
{
    public class PedidoGametasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoGametasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PedidoGametas
        [Authorize(Roles = "PMA")]
        public async Task<IActionResult> Index()
        {
            var pedidosGametas = _context.PedidoGametas.ToList();
            ICollection<PedidoGametasViewModel> listaPedidos = new List<PedidoGametasViewModel>();

            foreach (var p in pedidosGametas)
            {
                var pedidoGam = _context.Casal
                    .Where(c => c.CasalID == p.CasalId)
                    .Select(s => new PedidoGametasViewModel
                    {
                        Id = p.PedidoGametasId,
                        Data = p.Data,
                        Centro = p.Centro,
                        RefExterna = p.RefExterna,
                        IdadeHomem = s.IdadeHomem,
                        RacaHomem = s.RacaHomem,
                        AlturaHomem = s.AlturaHomem,
                        CorCabeloHomem = s.CorCabeloHomem,
                        GrupoSanguineoHomem = s.GrupoSanguineoHomem,
                        TexturaCabeloHomem = s.TexturaCabeloHomem,
                        CorOlhosHomem = s.CorOlhosHomem,
                        CorPeleHomem = s.CorPeleHomem,
                        IdadeMulher = s.IdadeMulher,
                        RacaMulher = s.RacaMulher,
                        AlturaMulher = s.AlturaMulher,
                        CorCabeloMulher = s.CorCabeloMulher,
                        GrupoSanguineoMulher = s.GrupoSanguineoMulher,
                        TexturaCabeloMulher = s.TexturaCabeloMulher,
                        CorOlhosMulher = s.CorOlhosMulher,
                        CorPeleMulher = s.CorPeleMulher
                    })
                    .FirstOrDefault();
                    
                 listaPedidos.Add(pedidoGam);
            }

            return View(listaPedidos);
        }

        // GET: PedidoGametas/Details/5
        [Authorize(Roles = "PMA")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoGam = await _context.PedidoGametas.SingleOrDefaultAsync(p => p.PedidoGametasId == id);
            var casalPedido = await _context.Casal.SingleOrDefaultAsync(c => c.CasalID == pedidoGam.CasalId);

            if (pedidoGam == null || casalPedido == null)
            {
                return NotFound();
            }

            var pedidoGamViewModel = new PedidoGametasViewModel
            {
                Id = pedidoGam.PedidoGametasId,
                Data = pedidoGam.Data,
                Centro = pedidoGam.Centro,
                RefExterna = pedidoGam.RefExterna,
                IdadeHomem = casalPedido.IdadeHomem,
                RacaHomem = casalPedido.RacaHomem,
                AlturaHomem = casalPedido.AlturaHomem,
                CorCabeloHomem = casalPedido.CorCabeloHomem,
                GrupoSanguineoHomem = casalPedido.GrupoSanguineoHomem,
                TexturaCabeloHomem = casalPedido.TexturaCabeloHomem,
                CorOlhosHomem = casalPedido.CorOlhosHomem,
                CorPeleHomem = casalPedido.CorPeleHomem,
                IdadeMulher = casalPedido.IdadeMulher,
                RacaMulher = casalPedido.RacaMulher,
                AlturaMulher = casalPedido.AlturaMulher,
                CorCabeloMulher = casalPedido.CorCabeloMulher,
                GrupoSanguineoMulher = casalPedido.GrupoSanguineoMulher,
                TexturaCabeloMulher = casalPedido.TexturaCabeloMulher,
                CorOlhosMulher = casalPedido.CorOlhosMulher,
                CorPeleMulher = casalPedido.CorPeleMulher
            };

            return View(pedidoGamViewModel);
        }

        // GET: PedidoGametas/Create
        [Authorize(Roles = "PMA")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PedidoGametas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PMA")]
        public async Task<IActionResult> Create([Bind("Id,Data,Centro,RefExterna,IdadeHomem,RacaHomem,AlturaHomem,CorCabeloHomem,GrupoSanguineoHomem,TexturaCabeloHomem,CorOlhosHomem,CorPeleHomem,IdadeMulher,RacaMulher,AlturaMulher,CorCabeloMulher,GrupoSanguineoMulher,TexturaCabeloMulher,CorOlhosMulher,CorPeleMulher")] PedidoGametasViewModel pedidoGametasViewModel)
        {
            if (ModelState.IsValid)
            {
                var novoCasal = new Models.Casal
                {
                    IdadeHomem = pedidoGametasViewModel.IdadeHomem,
                    RacaHomem = pedidoGametasViewModel.RacaHomem,
                    AlturaHomem = pedidoGametasViewModel.AlturaHomem,
                    CorCabeloHomem = pedidoGametasViewModel.CorCabeloHomem,
                    GrupoSanguineoHomem = pedidoGametasViewModel.GrupoSanguineoHomem,
                    TexturaCabeloHomem = pedidoGametasViewModel.TexturaCabeloHomem,
                    CorOlhosHomem = pedidoGametasViewModel.CorOlhosHomem,
                    CorPeleHomem = pedidoGametasViewModel.CorPeleHomem,

                    IdadeMulher = pedidoGametasViewModel.IdadeMulher,
                    RacaMulher = pedidoGametasViewModel.RacaMulher,
                    AlturaMulher = pedidoGametasViewModel.AlturaMulher,
                    CorCabeloMulher = pedidoGametasViewModel.CorCabeloMulher,
                    GrupoSanguineoMulher = pedidoGametasViewModel.GrupoSanguineoMulher,
                    TexturaCabeloMulher = pedidoGametasViewModel.TexturaCabeloMulher,
                    CorOlhosMulher = pedidoGametasViewModel.CorOlhosMulher,
                    CorPeleMulher = pedidoGametasViewModel.CorPeleMulher
                };

                await _context.Casal.AddAsync(novoCasal);
                await _context.SaveChangesAsync();

                var objNovoCasal = await _context.Casal.LastOrDefaultAsync();

                var novoPedidoGametas = new Models.PedidoGametas
                { 
                    CasalId = objNovoCasal.CasalID,
                    Data = pedidoGametasViewModel.Data,
                    Centro = pedidoGametasViewModel.Centro,
                    RefExterna = pedidoGametasViewModel.RefExterna
                };

                await _context.PedidoGametas.AddAsync(novoPedidoGametas);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedidoGametasViewModel);
        }

        // GET: PedidoGametas/Edit/5
        [Authorize(Roles = "PMA")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoGam = await _context.PedidoGametas.SingleOrDefaultAsync(p => p.PedidoGametasId == id);
            var casalPedido = await _context.Casal.SingleOrDefaultAsync(c => c.CasalID == pedidoGam.CasalId);

            if (pedidoGam == null || casalPedido == null)
            {
                return NotFound();
            }

            var pedidoGamViewModel = new PedidoGametasViewModel
            {
                Id = pedidoGam.PedidoGametasId,
                Data = pedidoGam.Data,
                Centro = pedidoGam.Centro,
                RefExterna = pedidoGam.RefExterna,
                IdadeHomem = casalPedido.IdadeHomem,
                RacaHomem = casalPedido.RacaHomem,
                AlturaHomem = casalPedido.AlturaHomem,
                CorCabeloHomem = casalPedido.CorCabeloHomem,
                GrupoSanguineoHomem = casalPedido.GrupoSanguineoHomem,
                TexturaCabeloHomem = casalPedido.TexturaCabeloHomem,
                CorOlhosHomem = casalPedido.CorOlhosHomem,
                CorPeleHomem = casalPedido.CorPeleHomem,
                IdadeMulher = casalPedido.IdadeMulher,
                RacaMulher = casalPedido.RacaMulher,
                AlturaMulher = casalPedido.AlturaMulher,
                CorCabeloMulher = casalPedido.CorCabeloMulher,
                GrupoSanguineoMulher = casalPedido.GrupoSanguineoMulher,
                TexturaCabeloMulher = casalPedido.TexturaCabeloMulher,
                CorOlhosMulher = casalPedido.CorOlhosMulher,
                CorPeleMulher = casalPedido.CorPeleMulher
            };

            return View(pedidoGamViewModel);
        }

        // POST: PedidoGametas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PMA")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdadeHomem,RacaHomem,AlturaHomem,CorCabeloHomem,GrupoSanguineoHomem,TexturaCabeloHomem,CorOlhosHomem,CorPeleHomem,IdadeMulher,RacaMulher,AlturaMulher,CorCabeloMulher,GrupoSanguineoMulher,TexturaCabeloMulher,CorOlhosMulher,CorPeleMulher")] PedidoGametasViewModel pedidoGametasViewModel)
        {
            var pedidoGam = await _context.PedidoGametas.AsNoTracking().SingleOrDefaultAsync(p => p.PedidoGametasId == pedidoGametasViewModel.Id);
            var casalPedido = await _context.Casal.AsNoTracking().SingleOrDefaultAsync(c => c.CasalID == pedidoGam.CasalId);

            if (pedidoGam == null || casalPedido == null)
            {
                return NotFound();
            }

            if (id != pedidoGam.PedidoGametasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var casalToUpdate = new Casal
                    {
                        CasalID = casalPedido.CasalID,
                        IdadeHomem = pedidoGametasViewModel.IdadeHomem,
                        RacaHomem = pedidoGametasViewModel.RacaHomem,
                        AlturaHomem = pedidoGametasViewModel.AlturaHomem,
                        CorCabeloHomem = pedidoGametasViewModel.CorCabeloHomem,
                        GrupoSanguineoHomem = pedidoGametasViewModel.GrupoSanguineoHomem,
                        TexturaCabeloHomem = pedidoGametasViewModel.TexturaCabeloHomem,
                        CorOlhosHomem = pedidoGametasViewModel.CorOlhosHomem,
                        CorPeleHomem = pedidoGametasViewModel.CorPeleHomem,
                        IdadeMulher = pedidoGametasViewModel.IdadeMulher,
                        RacaMulher = pedidoGametasViewModel.RacaMulher,
                        AlturaMulher = pedidoGametasViewModel.AlturaMulher,
                        CorCabeloMulher = pedidoGametasViewModel.CorCabeloMulher,
                        GrupoSanguineoMulher = pedidoGametasViewModel.GrupoSanguineoMulher,
                        TexturaCabeloMulher = pedidoGametasViewModel.TexturaCabeloMulher,
                        CorOlhosMulher = pedidoGametasViewModel.CorOlhosMulher,
                        CorPeleMulher = pedidoGametasViewModel.CorPeleMulher
                    };

                    _context.Update(casalToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoGametasViewModelExists(pedidoGametasViewModel.Id))
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
            return View(pedidoGametasViewModel);
        }

        // GET: PedidoGametas/Delete/5
        [Authorize(Roles = "PMA")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoGam = await _context.PedidoGametas.SingleOrDefaultAsync(p => p.PedidoGametasId == id);
            var casalPedido = await _context.Casal.SingleOrDefaultAsync(c => c.CasalID == pedidoGam.CasalId);

            if (pedidoGam == null || casalPedido == null)
            {
                return NotFound();
            }

            var pedidoGamViewModel = new PedidoGametasViewModel
            {
                Id = pedidoGam.PedidoGametasId,
                Data = pedidoGam.Data,
                Centro = pedidoGam.Centro,
                RefExterna = pedidoGam.RefExterna,
                IdadeHomem = casalPedido.IdadeHomem,
                RacaHomem = casalPedido.RacaHomem,
                AlturaHomem = casalPedido.AlturaHomem,
                CorCabeloHomem = casalPedido.CorCabeloHomem,
                GrupoSanguineoHomem = casalPedido.GrupoSanguineoHomem,
                TexturaCabeloHomem = casalPedido.TexturaCabeloHomem,
                CorOlhosHomem = casalPedido.CorOlhosHomem,
                CorPeleHomem = casalPedido.CorPeleHomem,
                IdadeMulher = casalPedido.IdadeMulher,
                RacaMulher = casalPedido.RacaMulher,
                AlturaMulher = casalPedido.AlturaMulher,
                CorCabeloMulher = casalPedido.CorCabeloMulher,
                GrupoSanguineoMulher = casalPedido.GrupoSanguineoMulher,
                TexturaCabeloMulher = casalPedido.TexturaCabeloMulher,
                CorOlhosMulher = casalPedido.CorOlhosMulher,
                CorPeleMulher = casalPedido.CorPeleMulher
            };

            return View(pedidoGamViewModel);
        }

        // POST: PedidoGametas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PMA")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoGam = await _context.PedidoGametas.SingleOrDefaultAsync(p => p.PedidoGametasId == id);
            var casalPedido = await _context.Casal.SingleOrDefaultAsync(c => c.CasalID == pedidoGam.CasalId);
            _context.Casal.Remove(casalPedido);
            _context.PedidoGametas.Remove(pedidoGam);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoGametasViewModelExists(int id)
        {
            return _context.PedidoGametasViewModel.Any(e => e.Id == id);
        }
    }
}
