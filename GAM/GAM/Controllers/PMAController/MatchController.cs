using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GAM.Data;
using GAM.Helpers;
using GAM.Models;
using GAM.Models.Enums;
using GAM.Models.PMAViewModels;
using Microsoft.AspNetCore.Mvc;

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

            var casal = _context.Casal.FirstOrDefault(x => x.CasalID == id);
            var matchList = _context.Dador.Where(x => casal.GamMatch(x)).ToList();

            return View("ListaDadores", matchList);
        }
    }
}
