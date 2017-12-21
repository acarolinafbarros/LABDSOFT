﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GAM.Data;
using GAM.Helpers;
using GAM.Models;
using GAM.Models.Enums;
using GAM.Models.PMA;
using GAM.Models.PMAViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GAM.Controllers.PMAController
{
    public class MatchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PedidoGametas
        //[Authorize(Roles = "PMA")]
        public async Task<IActionResult> Index()
        {
            ICollection<Casal> lista = _context.PedidoGametas
                .Where(x => x.EstadoProcessoPedido == EstadoProcesso.EmAnalise 
                || x.EstadoProcessoPedido == EstadoProcesso.EmListaEspera)
                .Select(x => x.Casal).ToList();

             //Se obtiver match -> EncontrouMatch
             //Se nao obtiver match -> EmEspera


            return View(lista);
        }

        public async Task<IActionResult> GetMatchList(int? id)
        {
            if (id == null)
                return NotFound();


            //Recolher dados estatisticos do Match feito anteriormente.
            var matchStatsInfoDb = _context.MatchStats.ToList();
            var matchStatsInfo = new MatchStatsInfo
            {
                //NrRecords = matchStatsInfoDb.Count(),
                GrupoSanguineoMulher = matchStatsInfoDb.Count(x => x.GrupoSanguineoMulher),
                GrupoSanguineoHomem = matchStatsInfoDb.Count(x => x.GrupoSanguineoHomem),
                CorOlhosMulher = matchStatsInfoDb.Count(x => x.CorOlhosMulher),
                CorCabeloMulher = matchStatsInfoDb.Count(x => x.CorCabeloMulher),
                RacaHomem = matchStatsInfoDb.Count(x => x.RacaHomem),
                CorPeleMulher = matchStatsInfoDb.Count(x => x.CorPeleMulher),
                CorOlhosHomem = matchStatsInfoDb.Count(x => x.CorOlhosHomem),
                CorCabeloHomem = matchStatsInfoDb.Count(x => x.CorCabeloHomem),
                CorPeleHomem = matchStatsInfoDb.Count(x => x.CorPeleHomem),
                TexturaCabeloHomem = matchStatsInfoDb.Count(x => x.TexturaCabeloHomem),
                TexturaCabeloMulher = matchStatsInfoDb.Count(x => x.TexturaCabeloMulher),
                RacaMulher = matchStatsInfoDb.Count(x => x.RacaMulher),
            };

            //Recolher dados do casal e lista de dadores filtrados com factor de exclusao
            var casal = _context.Casal.FirstOrDefault(x => x.CasalID == id);
            var matchList = _context.Dador.Where(x => casal.GamMatch(x)).ToList();

            //Mecanismo de ordenação baseada nas escolhas frequentes do utilizador
            var listaOrdenada = MatchHelper.GetOrdedList(matchList, casal, matchStatsInfo);
            if (!listaOrdenada.Any())
            {
                casal.PedidoGametas.EstadoProcessoPedido = EstadoProcesso.EmListaEspera;

                //TODO apresentar mensagem utilizador de "sem match possiveis de momento"
                return NotFound();
            }

            ViewBag.CasalId = id;
            return View("ListaDadores", listaOrdenada);
        }

        public async Task<IActionResult> SelectMatch(int? id, int? casalid)
        {
            if (id == null || casalid == null)
                return NotFound();

            var casalMatch = _context.Casal.Include(x=>x.PedidoGametas).FirstOrDefault(x => x.CasalID == casalid);
            var dadorMatch = _context.Dador.Include(x=>x.Amostras).FirstOrDefault(x => x.DadorId == id);

            if (dadorMatch == null || casalMatch == null)
                return NoContent();

            var matchStats = MatchHelper.GetMatchStats(casalMatch, dadorMatch);
            matchStats.DadorId = dadorMatch.DadorId;
            matchStats.CasalId = casalMatch.CasalID;

            

            casalMatch.PedidoGametas.EstadoProcessoPedido = EstadoProcesso.EncontrouMatch;
            var amostraSelecionada = dadorMatch.Amostras.Where(x=>x.PedidoGametas==null)
                .OrderByDescending(x => x.AmostraId).FirstOrDefault();

            if (amostraSelecionada == null)
            {
                //TODO Mensagem erro no processo
                return NoContent();
            }

            casalMatch.PedidoGametas.AmostraId = amostraSelecionada.AmostraId;

            await _context.MatchStats.AddAsync(matchStats);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ValidaMatch()
        {
            ICollection<Casal> lista = _context.PedidoGametas
                .Where(x => x.EstadoProcessoPedido == EstadoProcesso.EncontrouMatch)
                .Select(x => x.Casal).ToList();

            return View(lista);
        }
    }
}
