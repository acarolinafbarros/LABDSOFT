using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.DadorViewModels;
using GAM.Security;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;
using System;
using GAM.Models.Enums;

namespace GAM.Controllers.DadorController
{
    public class DadorsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private EncryptorDador _encryptor;

        public DadorsController(ApplicationDbContext context, IDataProtectionProvider provider)
        {
            _context = context;
            _encryptor = new EncryptorDador(provider);
        }

        // GET: Dadors
        public async Task<IActionResult> Index()
        {
            

            return View(_encryptor.DecryptDataList(await _context.Dador.ToListAsync()));
        }

        public async Task<IActionResult> ActivityReport()
        {
            //-------------------- Olhos -------------------

            var totalDoadoresOlhos = _context.Dador.Count();
            var totalDoadoresEfectivosOlhos = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Count();
            var totalDoadoresRejeitadosOlhos = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Count();
            var totalDoadoresQuarentenaOlhos = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Count();
            var totalDoadoresEmCursoOlhos = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Count();

            //Olhos Azuis
            var totalDoadoresOlhosAzuis = _context.Dador.Where(a => a.CorOlhos == CorOlhosEnum.Azul).Count();
            var dadoresEfectivosAzuis = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorOlhos == CorOlhosEnum.Azul).Count();
            var dadoresRejeitadosAzuis = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorOlhos == CorOlhosEnum.Azul).Count();
            var dadoresQuarentenaAzuis = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorOlhos == CorOlhosEnum.Azul).Count();
            var dadoresEmCursoAzuis = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorOlhos == CorOlhosEnum.Azul).Count();

            //Olhos Castanhos
            var totalDoadoresOlhosCastanhos = _context.Dador.Where(a => a.CorOlhos == CorOlhosEnum.Castanho).Count();
            var dadoresEfectivosCastanhos = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorOlhos == CorOlhosEnum.Castanho).Count();
            var dadoresRejeitadosCastanhos = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorOlhos == CorOlhosEnum.Castanho).Count();
            var dadoresQuarentenaCastanhos = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorOlhos == CorOlhosEnum.Castanho).Count();
            var dadoresEmCursoCastanhos = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorOlhos == CorOlhosEnum.Castanho).Count();

            //Olhos Verdes
            var totalDoadoresOlhosVerdes = _context.Dador.Where(a => a.CorOlhos == CorOlhosEnum.Verde).Count();
            var dadoresEfectivosVerdes = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorOlhos == CorOlhosEnum.Verde).Count();
            var dadoresRejeitadosVerdes = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorOlhos == CorOlhosEnum.Verde).Count();
            var dadoresQuarentenaVerdes = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorOlhos == CorOlhosEnum.Verde).Count();
            var dadoresEmCursoVerdes = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorOlhos == CorOlhosEnum.Verde).Count();

            //Olhos Outros
            var totalDoadoresOlhosOutros = _context.Dador.Where(a => a.CorOlhos == CorOlhosEnum.Outro).Count();
            var dadoresEfectivosOutros = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorOlhos == CorOlhosEnum.Outro).Count();
            var dadoresRejeitadosOutros = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorOlhos == CorOlhosEnum.Outro).Count();
            var dadoresQuarentenaOutros = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorOlhos == CorOlhosEnum.Outro).Count();
            var dadoresEmCursoOutros = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorOlhos == CorOlhosEnum.Outro).Count();

            List<int> lista = new List<int>();
            lista.Add(totalDoadoresOlhos);
            lista.Add(totalDoadoresEfectivosOlhos);
            lista.Add(totalDoadoresRejeitadosOlhos);
            lista.Add(totalDoadoresQuarentenaOlhos);
            lista.Add(totalDoadoresEmCursoOlhos);

            return View(lista);
        }

            // -------------------------------------------------------------------------------------------------------------------------
            // GET: Dadors/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dador = _encryptor.DecryptData(await _context.Dador
                .SingleOrDefaultAsync(m => m.DadorId == id));

            dador.Resposta = await _context.Resposta.Where(x => x.DadorId == dador.DadorId).Include(x => x.Pergunta).ToListAsync();
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

                dador = _encryptor.EncryptData(dador);

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

            var dador = _encryptor.DecryptData(await _context.Dador.SingleOrDefaultAsync(m => m.DadorId == id));
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
                    var dadorDb = _encryptor.DecryptData(await _context.Dador.SingleAsync(x => x.DadorId == id));
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

            var dador = _encryptor.DecryptData(await _context.Dador
                .SingleOrDefaultAsync(m => m.DadorId == id));
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
            var dador = _encryptor.DecryptData(await _context.Dador.SingleOrDefaultAsync(m => m.DadorId == id));
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
            string initials = "" + tokens[0].ElementAt(0) + tokens[1].ElementAt(0);

            return initials;
        }

       
    }

}
