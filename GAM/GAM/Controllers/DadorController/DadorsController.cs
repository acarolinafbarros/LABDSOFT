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
            //-------------------- Pele -------------------

            //Pele Muito Clara
            ViewBag.totalDoadoresPeleMC = _context.Dador.Where(a => a.CorPele == CorPeleEnum.MuitoClara).Count();
            ViewBag.dadoresEfectivosMC= _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorPele == CorPeleEnum.MuitoClara).Count();
            ViewBag.totalDoadoresRejeitadosMC= _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorPele == CorPeleEnum.MuitoClara).Count();
            ViewBag.totalDoadoresQuarentenaMC= _context.Dador.Where(c => c.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorPele == CorPeleEnum.MuitoClara).Count();
            ViewBag.totalDoadoresEmCursoMC= _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorPele == CorPeleEnum.MuitoClara).Count();

            //Pele Clara
            ViewBag.totalDoadoresPeleC = _context.Dador.Where(a => a.CorPele == CorPeleEnum.Clara).Count();
            ViewBag.dadoresEfectivosC = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorPele == CorPeleEnum.Clara).Count();
            ViewBag.totalDoadoresRejeitadosC = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorPele == CorPeleEnum.Clara).Count();
            ViewBag.totalDoadoresQuarentenaC = _context.Dador.Where(c => c.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorPele == CorPeleEnum.Clara).Count();
            ViewBag.totalDoadoresEmCursoC = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorPele == CorPeleEnum.Clara).Count();

            //Pele Escura
            ViewBag.totalDoadoresPeleE = _context.Dador.Where(a => a.CorPele == CorPeleEnum.Escura).Count();
            ViewBag.dadoresEfectivosE = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorPele == CorPeleEnum.Escura).Count();
            ViewBag.totalDoadoresRejeitadosE = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorPele == CorPeleEnum.Escura).Count();
            ViewBag.totalDoadoresQuarentenaE = _context.Dador.Where(c => c.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorPele == CorPeleEnum.Escura).Count();
            ViewBag.totalDoadoresEmCursoE = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorPele == CorPeleEnum.Escura).Count();

            //Pele Muito Escura
            ViewBag.totalDoadoresPeleME = _context.Dador.Where(a => a.CorPele == CorPeleEnum.MuitoEscura).Count();
            ViewBag.dadoresEfectivosME = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorPele == CorPeleEnum.MuitoEscura).Count();
            ViewBag.totalDoadoresRejeitadosME = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorPele == CorPeleEnum.MuitoEscura).Count();
            ViewBag.totalDoadoresQuarentenaME = _context.Dador.Where(c => c.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorPele == CorPeleEnum.MuitoEscura).Count();
            ViewBag.totalDoadoresEmCursoME = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorPele == CorPeleEnum.MuitoEscura).Count();

            //Pele Outros
            ViewBag.totalDoadoresPeleO = _context.Dador.Where(a => a.CorPele == CorPeleEnum.Outros).Count();
            ViewBag.dadoresEfectivosO = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorPele == CorPeleEnum.Outros).Count();
            ViewBag.totalDoadoresRejeitadosO = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorPele == CorPeleEnum.Outros).Count();
            ViewBag.totalDoadoresQuarentenaO = _context.Dador.Where(c => c.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorPele == CorPeleEnum.Outros).Count();
            ViewBag.totalDoadoresEmCursoO = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorPele == CorPeleEnum.Outros).Count();


            //-------------------- Olhos -------------------

            ViewBag.totalDoadoresOlhos = _context.Dador.Count();
            ViewBag.totalDoadoresEfectivosOlhos = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Count();
            ViewBag.totalDoadoresRejeitadosOlhos = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Count();
            ViewBag.totalDoadoresQuarentenaOlhos = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Count();
            ViewBag.totalDoadoresEmCursoOlhos = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Count();
          

            //Olhos Azuis
            ViewBag.totalDoadoresOlhosAzuis = _context.Dador.Where(a => a.CorOlhos == CorOlhosEnum.Azul).Count();
            ViewBag.dadoresEfectivosAzuis = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorOlhos == CorOlhosEnum.Azul).Count();
            ViewBag.dadoresRejeitadosAzuis = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorOlhos == CorOlhosEnum.Azul).Count();
            ViewBag.dadoresQuarentenaAzuis = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorOlhos == CorOlhosEnum.Azul).Count();
            ViewBag.dadoresEmCursoAzuis = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorOlhos == CorOlhosEnum.Azul).Count();
            

            //Olhos Castanhos
            ViewBag.totalDoadoresOlhosCastanhos = _context.Dador.Where(a => a.CorOlhos == CorOlhosEnum.Castanho).Count();
            ViewBag.dadoresEfectivosCastanhos = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorOlhos == CorOlhosEnum.Castanho).Count();
            ViewBag.dadoresRejeitadosCastanhos = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorOlhos == CorOlhosEnum.Castanho).Count();
            ViewBag.dadoresQuarentenaCastanhos = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorOlhos == CorOlhosEnum.Castanho).Count();
            ViewBag.dadoresEmCursoCastanhos = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorOlhos == CorOlhosEnum.Castanho).Count();
         

            //Olhos Verdes
            ViewBag.totalDoadoresOlhosVerdes = _context.Dador.Where(a => a.CorOlhos == CorOlhosEnum.Verde).Count();
            ViewBag.dadoresEfectivosVerdes = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorOlhos == CorOlhosEnum.Verde).Count();
            ViewBag.dadoresRejeitadosVerdes = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorOlhos == CorOlhosEnum.Verde).Count();
            ViewBag.dadoresQuarentenaVerdes = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorOlhos == CorOlhosEnum.Verde).Count();
            ViewBag.dadoresEmCursoVerdes = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorOlhos == CorOlhosEnum.Verde).Count();


            //Olhos Outros
            ViewBag.totalDoadoresOlhosOutros = _context.Dador.Where(a => a.CorOlhos == CorOlhosEnum.Outro).Count();
            ViewBag.dadoresEfectivosOutros = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorOlhos == CorOlhosEnum.Outro).Count();
            ViewBag.dadoresRejeitadosOutros = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorOlhos == CorOlhosEnum.Outro).Count();
            ViewBag.dadoresQuarentenaOutros = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorOlhos == CorOlhosEnum.Outro).Count();
            ViewBag.dadoresEmCursoOutros = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorOlhos == CorOlhosEnum.Outro).Count();

            //-------------------- Cor cabelo -------------------

            //loiro
            ViewBag.totalDoadoresCL = _context.Dador.Where(a => a.CorCabelo == CorCabeloEnum.Loiro).Count();
            ViewBag.dadoresEfectivosCL = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorCabelo == CorCabeloEnum.Loiro).Count();
            ViewBag.dadoresRejeitadosCL = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorCabelo == CorCabeloEnum.Loiro).Count();
            ViewBag.dadoresQuarentenaCL = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorCabelo == CorCabeloEnum.Loiro).Count();
            ViewBag.dadoresEmCursoCL = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorCabelo == CorCabeloEnum.Loiro).Count();

            //Castanho
            ViewBag.totalDoadoresCCa = _context.Dador.Where(a => a.CorCabelo == CorCabeloEnum.Castanho).Count();
            ViewBag.dadoresEfectivosCCa = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorCabelo == CorCabeloEnum.Castanho).Count();
            ViewBag.dadoresRejeitadosCCa = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorCabelo == CorCabeloEnum.Castanho).Count();
            ViewBag.dadoresQuarentenaCCa = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorCabelo == CorCabeloEnum.Castanho).Count();
            ViewBag.dadoresEmCursoCCa = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorCabelo == CorCabeloEnum.Castanho).Count();

            //Ruivo
            ViewBag.totalDoadoresCR = _context.Dador.Where(a => a.CorCabelo == CorCabeloEnum.Ruivo).Count();
            ViewBag.dadoresEfectivosCR = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorCabelo == CorCabeloEnum.Ruivo).Count();
            ViewBag.dadoresRejeitadosCR = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorCabelo == CorCabeloEnum.Ruivo).Count();
            ViewBag.dadoresQuarentenaCR = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorCabelo == CorCabeloEnum.Ruivo).Count();
            ViewBag.dadoresEmCursoCR = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorCabelo == CorCabeloEnum.Ruivo).Count();

            //Preto
            ViewBag.totalDoadoresCP = _context.Dador.Where(a => a.CorCabelo == CorCabeloEnum.Preto).Count();
            ViewBag.dadoresEfectivosCP = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorCabelo == CorCabeloEnum.Preto).Count();
            ViewBag.dadoresRejeitadosCP = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorCabelo == CorCabeloEnum.Preto).Count();
            ViewBag.dadoresQuarentenaCP = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorCabelo == CorCabeloEnum.Preto).Count();
            ViewBag.dadoresEmCursoCP = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorCabelo == CorCabeloEnum.Preto).Count();

            //Branco
            ViewBag.totalDoadoresCB = _context.Dador.Where(a => a.CorCabelo == CorCabeloEnum.Branco).Count();
            ViewBag.dadoresEfectivosCB = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorCabelo == CorCabeloEnum.Branco).Count();
            ViewBag.dadoresRejeitadosCB = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorCabelo == CorCabeloEnum.Branco).Count();
            ViewBag.dadoresQuarentenaCB = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorCabelo == CorCabeloEnum.Branco).Count();
            ViewBag.dadoresEmCursoCB= _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorCabelo == CorCabeloEnum.Branco).Count();

            //Cinzento
            ViewBag.totalDoadoresCCi = _context.Dador.Where(a => a.CorCabelo == CorCabeloEnum.Cinzento).Count();
            ViewBag.dadoresEfectivosCCi = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorCabelo == CorCabeloEnum.Cinzento).Count();
            ViewBag.dadoresRejeitadosCCi = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorCabelo == CorCabeloEnum.Cinzento).Count();
            ViewBag.dadoresQuarentenaCCi = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorCabelo == CorCabeloEnum.Cinzento).Count();
            ViewBag.dadoresEmCursoCCi = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorCabelo == CorCabeloEnum.Cinzento).Count();

            //Outros
            ViewBag.totalDoadoresCO = _context.Dador.Where(a => a.CorCabelo == CorCabeloEnum.Outros).Count();
            ViewBag.dadoresEfectivosCO = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.CorCabelo == CorCabeloEnum.Outros).Count();
            ViewBag.dadoresRejeitadosCO = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.CorCabelo == CorCabeloEnum.Outros).Count();
            ViewBag.dadoresQuarentenaCO = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.CorCabelo == CorCabeloEnum.Outros).Count();
            ViewBag.dadoresEmCursoCO = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.CorCabelo == CorCabeloEnum.Outros ).Count();

            //-------------------- Textura cabelo -------------------
            //Liso
            ViewBag.totalDoadoresTL = _context.Dador.Where(a => a.TexturaCabelo == TexturaCabeloEnum.Liso).Count();
            ViewBag.dadoresEfectivosTL = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Liso).Count();
            ViewBag.dadoresRejeitadosTL = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Liso).Count();
            ViewBag.dadoresQuarentenaTL = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Liso).Count();
            ViewBag.dadoresEmCursoTL = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Liso).Count();

            //Grisalho
            ViewBag.totalDoadoresTG = _context.Dador.Where(a => a.TexturaCabelo == TexturaCabeloEnum.Grisalho).Count();
            ViewBag.dadoresEfectivosTG = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Grisalho).Count();
            ViewBag.dadoresRejeitadosTG = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Grisalho).Count();
            ViewBag.dadoresQuarentenaTG = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Grisalho).Count();
            ViewBag.dadoresEmCursoTG = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Grisalho).Count();


            //Ondulado
            ViewBag.totalDoadoresTO = _context.Dador.Where(a => a.TexturaCabelo == TexturaCabeloEnum.Ondulado).Count();
            ViewBag.dadoresEfectivosTO = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Ondulado).Count();
            ViewBag.dadoresRejeitadosTO = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Ondulado).Count();
            ViewBag.dadoresQuarentenaTO = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Ondulado).Count();
            ViewBag.dadoresEmCursoTO = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Ondulado).Count();

            //Outros
            ViewBag.totalDoadoresTOu = _context.Dador.Where(a => a.TexturaCabelo == TexturaCabeloEnum.Outros).Count();
            ViewBag.dadoresEfectivosTOu = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Aceite).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Outros).Count();
            ViewBag.dadoresRejeitadosTOu = _context.Dador.Where(c => c.EstadoDador == EstadoDadorEnum.Rejeitado).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Outros).Count();
            ViewBag.dadoresQuarentenaTOu = _context.Dador.Where(x => x.FaseDador == FaseDadorEnum.SetimaDadiva).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Outros).Count();
            ViewBag.dadoresEmCursoTOu = _context.Dador.Where(z => z.EstadoDador == EstadoDadorEnum.PendenteAprovacao).Where(a => a.TexturaCabelo == TexturaCabeloEnum.Outros).Count();


            return View();
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
