using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.Laboratorio;
using GAM.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace GAM.Controllers.EmbriologistaController
{
    [Authorize(Roles = "Embriologista")]
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
            var applicationDbContext = _context.Amostra.Where(a => a.TipoAmostra == TipoAmostraEnum.Espermatozoide
                                                               && a.EstadoAmostra == EstadoAmostraEnum.Analisada
                                                               && a.LocalizacaoAmostra==null);

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

            var localizacao = _context.LocalizacaoAmostra.Where(x => x.AmostraId == null)
                .Select(x => new
                                {
                                    id = x.LocalizacaoAmostraId,
                    localizacao_dados= $"Banco - {x.Banco},  Piso - {x.Piso}, Cannister - {x.Cannister}, Globet Cor - {x.GlobetCor}, Globet Numero - {x.GlobetNumero}, Palheta Cor - {x.PalhetaCor} "
                });
            var lista_localizacao = new SelectList(localizacao, "id", "localizacao_dados", amostra.LocalizacaoAmostra?.LocalizacaoAmostraId);
            ViewData["DadorId"] = new SelectList(_context.Dador, "DadorId", "DadorId", amostra.DadorId);
            ViewData["Localizacao"] = lista_localizacao.ToList();
            return View(amostra);
        }

        // POST: CriopreservacaoAmostras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int localizacaoId, [Bind("AmostraId,DadorId,EstadoAmostra,TipoAmostra,DataRecolha,Banco,Piso,Cannister,GlobetCor,GlobetNumero,PalhetaCor,NrAmosta")] Amostra amostra)
        {
            if (id != amostra.AmostraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    amostra.EstadoAmostra = EstadoAmostraEnum.Criopreservada;
                    var localizacao = await _context.LocalizacaoAmostra.SingleOrDefaultAsync(x => x.LocalizacaoAmostraId == localizacaoId);
                    localizacao.AmostraId = id;
                   
                    _context.Update(localizacao);
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
