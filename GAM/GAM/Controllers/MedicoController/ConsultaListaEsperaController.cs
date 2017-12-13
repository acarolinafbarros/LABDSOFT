using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.PMAViewModels;
using Microsoft.AspNetCore.Authorization;
using GAM.Models.Enums;

namespace GAM.Controllers.MedicoController
{
    public class ConsultaListaEsperaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultaListaEsperaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConsultaListaEspera
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Index()
        {
            var pedidosGametas = _context.PedidoGametas.ToList();
            ICollection<PedidoGametasViewModel> listaPedidos = new List<PedidoGametasViewModel>();

            foreach (var p in pedidosGametas)
            {
                if (p.EstadoProcessoPedido == EstadoProcesso.EmListaEspera)
                {
                    var pedidoGam = _context.Casal
                                     .Where(c => c.CasalID == p.CasalId)
                                       .Select(s => new PedidoGametasViewModel
                                       {
                                           Id = p.PedidoGametasId,
                                           Data = p.Data,
                                           Centro = p.Centro,
                                           RefExterna = p.RefExterna,
                                           EstadoProcessoPedido = p.EstadoProcessoPedido,
                                           OriginouGravidez = s.OriginouGravidez,
                                           NrFilhos = s.NrFilhos,
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
            }

            return View(listaPedidos);
            //return View(await _context.PedidoGametasViewModel.ToListAsync());
        }

        // GET: ConsultaListaEspera/Details/5
        [Authorize(Roles = "Medico")]
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
                EstadoProcessoPedido = pedidoGam.EstadoProcessoPedido,

                OriginouGravidez = casalPedido.OriginouGravidez,
                NrFilhos = casalPedido.NrFilhos,
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

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var pedidoGametasViewModel = await _context.PedidoGametasViewModel
            //    .SingleOrDefaultAsync(m => m.Id == id);
            //if (pedidoGametasViewModel == null)
            //{
            //    return NotFound();
            //}

            //return View(pedidoGametasViewModel);
        }
    }
}
