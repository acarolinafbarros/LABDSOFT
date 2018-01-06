using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.PMAViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.IO;
using GAM.Helpers;
using GAM.Models;
using GAM.Models.Enums;
using Microsoft.AspNetCore.Hosting;

namespace GAM.Controllers.PMAController
{
    public class PedidoGametasController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IHostingEnvironment _hostingEnvironment;


        public PedidoGametasController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment=null)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: PedidoGametas
        [Authorize(Roles = "PMA")]
        public IActionResult Index()
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
        public async Task<IActionResult> Create([Bind("Id,Data,Centro,RefExterna,IdadeHomem,RacaHomem,AlturaHomem,CorCabeloHomem,GrupoSanguineoHomem,TexturaCabeloHomem,CorOlhosHomem,CorPeleHomem,IdadeMulher,RacaMulher,AlturaMulher,CorCabeloMulher,GrupoSanguineoMulher,TexturaCabeloMulher,CorOlhosMulher,CorPeleMulher")] PedidoGametasViewModel pedidoGametasViewModel, Microsoft.AspNetCore.Http.IFormFile fileHomem, Microsoft.AspNetCore.Http.IFormFile fileMulher)
        {
            if (ModelState.IsValid)
            {
                var novoCasal = new Models.Casal
                {
                    OriginouGravidez = SimNaoEnum.Indefinido,
                    NrFilhos = 0,

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
                var pathUpload = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "casais");
                var fileUpload = "";
                //save image
                if (fileHomem.Length > 0)
                {
                    fileUpload = await fileHomem.SaveFileDefault(_hostingEnvironment, pathUpload);

                    if (fileUpload != "")
                    {
                        var fotoCasal = MatchHelper.MicrosoftCognitiveServices.Faces.AddFaceToFaceList(
                            Path.Combine(pathUpload, fileUpload), fileUpload);

                        novoCasal.FotoHomemId = fotoCasal.ToString();
                    }
                }
                if (fileMulher.Length > 0)
                {
                    fileUpload = await fileMulher.SaveFileDefault(_hostingEnvironment,
                        Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "casais"));

                    if (fileUpload != "")
                    {
                        var fotoCasal = MatchHelper.MicrosoftCognitiveServices.Faces.AddFaceToFaceList(
                            Path.Combine(pathUpload, fileUpload), fileUpload);

                        novoCasal.FotoHomemId = fotoCasal.ToString();
                    }
                }

                await _context.Casal.AddAsync(novoCasal);
                await _context.SaveChangesAsync();

                var objNovoCasal = await _context.Casal.LastOrDefaultAsync();

                var novoPedidoGametas = new PedidoGametas
                { 
                    CasalId = objNovoCasal.CasalID,
                    Data = pedidoGametasViewModel.Data,
                    Centro = pedidoGametasViewModel.Centro,
                    RefExterna = pedidoGametasViewModel.RefExterna,
                    EstadoProcessoPedido = EstadoProcesso.EmAnalise
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
        }

        // POST: PedidoGametas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PMA")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdadeHomem,RacaHomem,AlturaHomem,CorCabeloHomem,GrupoSanguineoHomem,TexturaCabeloHomem,CorOlhosHomem,CorPeleHomem,IdadeMulher,RacaMulher,AlturaMulher,CorCabeloMulher,GrupoSanguineoMulher,TexturaCabeloMulher,CorOlhosMulher,CorPeleMulher,OriginouGravidez,NrFilhos")] PedidoGametasViewModel pedidoGametasViewModel)
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
                        OriginouGravidez = pedidoGametasViewModel.OriginouGravidez,
                        NrFilhos = pedidoGametasViewModel.NrFilhos,

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

                    if (casalToUpdate.OriginouGravidez != SimNaoEnum.Indefinido)
                    {
                        var pedidoGamToUpdate = new PedidoGametas
                        {
                            PedidoGametasId = pedidoGam.PedidoGametasId,
                            CasalId = casalToUpdate.CasalID,
                            Data = pedidoGam.Data,
                            Centro = pedidoGam.Centro,
                            RefExterna = pedidoGam.RefExterna,
                            EstadoProcessoPedido = EstadoProcesso.RegisteiResultadosCasal
                        };
                        _context.Update(pedidoGamToUpdate);
                        await _context.SaveChangesAsync();
                    }

                    _context.Update(casalToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
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
                EstadoProcessoPedido = pedidoGam.EstadoProcessoPedido,
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
    }
}
