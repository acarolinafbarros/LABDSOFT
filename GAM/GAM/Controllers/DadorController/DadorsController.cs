using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.DadorViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using GAM.Models.Enums;

namespace GAM.Controllers.DadorController
{
    public class DadorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DadorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dadors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dador.ToListAsync());
        }

        // -------------------------------------------------------------------------------------------------------------------------
        // GET: Dadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dador = await _context.Dador
                .SingleOrDefaultAsync(m => m.DadorID == id);
            if (dador == null)
            {
                return NotFound();
            }

            return View(dador);
        }

        // -------------------------------------------------------------------------------------------------------------------------
        // GET: Dadors/Create
        public IActionResult Create()
        {
            DropDownListEstadoCivilEnum();
            DropDownListGrauEscolaridadeEnum();
            DropDownListGrupoSanguineoEnum();
            return View();
        }

        // POST: Dadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DadorID,Nome,Morada,DataNasc,LocalNasc,DocIdentificacao,Nacionalidade,Profissao,GrauEscolaridade,EstadoCivil,Altura,Peso,CorPele,CorOlhos,CorCabelo,TexturaCabelo,GrupoSanguineo,Etnia,CodigoDador,FaseDador,EstadoDador,NumAbortos,TotalGestacoes")] Dador dador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            DropDownListEstadoCivilEnum();
            DropDownListGrauEscolaridadeEnum();
            DropDownListGrupoSanguineoEnum();
            return View(dador);
        }

        // -------------------------------------------------------------------------------------------------------------------------
        // GET: Dadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dador = await _context.Dador.SingleOrDefaultAsync(m => m.DadorID == id);
            if (dador == null)
            {
                return NotFound();
            }
            DropDownListFaseDadorEnum();
            DropDownListEstadoDadorEnum();
            return View(dador);
        }

        // POST: Dadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DadorID,FaseDador,EstadoDador")] Dador dador)
        {
            if (id != dador.DadorID)
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
                    if (!DadorExists(dador.DadorID))
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
            DropDownListFaseDadorEnum();
            DropDownListEstadoDadorEnum();
            return View(dador);
        }

        // -------------------------------------------------------------------------------------------------------------------------
        // GET: Dadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dador = await _context.Dador
                .SingleOrDefaultAsync(m => m.DadorID == id);
            if (dador == null)
            {
                return NotFound();
            }

            return View(dador);
        }

        // POST: Dadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dador = await _context.Dador.SingleOrDefaultAsync(m => m.DadorID == id);
            _context.Dador.Remove(dador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DadorExists(int id)
        {
            return _context.Dador.Any(e => e.DadorID == id);
        }

        // -------------------------------------------------------------------------------------------------------------------------
        // Dropdown lists form enum types

        public void DropDownListEstadoCivilEnum()
        {
            ViewBag.EstadoCivil = new SelectList(System.Enum.GetValues(typeof(EstadoCivilEnum)));
        }

        public void DropDownListEstadoDadorEnum()
        {
            ViewBag.EstadoDador = new SelectList(System.Enum.GetValues(typeof(EstadoDadorEnum)));
        }

        public void DropDownListFaseDadorEnum()
        {
            ViewBag.FaseDador = new SelectList(System.Enum.GetValues(typeof(FaseDadorEnum)));
        }

        public void DropDownListGrauEscolaridadeEnum()
        {
            ViewBag.GrauEscolaridade = new SelectList(System.Enum.GetValues(typeof(GrauEscolaridadeEnum)));
        }

        public void DropDownListGrupoSanguineoEnum()
        {
            ViewBag.GrupoSanguineo = new SelectList(System.Enum.GetValues(typeof(GrupoSanguineoEnum)));
        }
    }
}
