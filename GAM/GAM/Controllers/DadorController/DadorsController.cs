using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.DadorViewModels;

namespace GAM.Controllers.DadorController
{
    public class DadorsController : BaseController
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
                .SingleOrDefaultAsync(m => m.DadorId == id);

            dador.Resposta = await _context.Resposta.Where(x => x.DadorId == dador.DadorId).Include(x=>x.Pergunta).ToListAsync();
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
            return View();
        }

        // POST: Dadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DadorId,Nome,Morada,DataNasc,LocalNasc,DocIdentificacao,Nacionalidade,Profissao,GrauEscolaridade,EstadoCivil,NumFilhos,Altura,Peso,CorPele,CorOlhos,CorCabelo,TexturaCabelo,GrupoSanguineo,Etnia,IniciaisDador,FaseDador,EstadoDador,DadosDador,NumAbortos,TotalGestacoes")] Dador dador)
        {
            if (ModelState.IsValid)
            {
                dador.IniciaisDador = RetrieveInitials(dador.Nome);

                _context.Add(dador);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexRegistered", "Home");
            }
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

            var dador = await _context.Dador.SingleOrDefaultAsync(m => m.DadorId == id);
            if (dador == null)
            {
                return NotFound();
            }
            return View(dador);
        }

        // POST: Dadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DadorId,FaseDador,EstadoDador")] Dador dador)
        {
            if (id != dador.DadorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dadorDb = await _context.Dador.SingleAsync(x => x.DadorId == id);
                    dadorDb.EstadoDador = dador.EstadoDador;
                    dadorDb.FaseDador = dador.FaseDador;
                    _context.Update(dadorDb);
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

        // -------------------------------------------------------------------------------------------------------------------------
        // GET: Dadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dador = await _context.Dador
                .SingleOrDefaultAsync(m => m.DadorId == id);
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
            var dador = await _context.Dador.SingleOrDefaultAsync(m => m.DadorId == id);
            _context.Dador.Remove(dador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DadorExists(int id)
        {
            return _context.Dador.Any(e => e.DadorId == id);
        }

        private string RetrieveInitials(string name)
        {
            string[] tokens = name.Split(' ');
            string initials = ""+tokens[0].ElementAt(0) + tokens[1].ElementAt(0);

            return initials;
        }
    }
}
