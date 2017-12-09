using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.DadorViewModels;
using GAM.Models.Enums;
using GAM.Security;
using Microsoft.AspNetCore.DataProtection;
using GAM.Models.DiretoraBancoViewModels;
using GAM.Models.Laboratorio;
using Microsoft.AspNetCore.Authorization;

namespace GAM.Controllers.DiretoraBancoController
{
    [Authorize(Roles = "DiretoraBanco")]
    public class ConsultaDestinosGametasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private EncryptorDador _encryptor;

        public ConsultaDestinosGametasController(ApplicationDbContext context, IDataProtectionProvider provider)
        {
            _context = context;
            _encryptor = new EncryptorDador(provider);
        }

        // GET: ConsultaDestinosGametas
        public async Task<IActionResult> Index()
        {
            return View(_encryptor.DecryptDataList(await _context.Dador
                .Where(d => d.Amostras.Any(a => a.EstadoAmostra == EstadoAmostraEnum.Enviada))
                .ToListAsync()));
        }

        // GET: ConsultaDestinosGametas/Details/5
        public async Task<IActionResult> Details(int? id)
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

            var amostras = await _context.Amostra
                .Where(a => a.DadorId == id)
                .Where(a => a.EstadoAmostra == EstadoAmostraEnum.Enviada)
                .ToListAsync();

            ICollection<ConsultaDestinosGametasViewModel> model = new List<ConsultaDestinosGametasViewModel>();

            foreach(Amostra a in amostras)
            {
                ConsultaDestinosGametasViewModel cdgVM = new ConsultaDestinosGametasViewModel
                {
                    NrEnvio = 1,
                    NrAmostra = a.AmostraId,
                    NomeDador = dador.Nome,
                    Centro = "CentroPMA",
                    DataEnvio = DateTime.Parse("1-12-2017"),
                    RefExterna = "Ref21A3"
                };

                model.Add(cdgVM);
            }

            return View(model);
        }

        private bool DadorExists(int id)
        {
            return _context.Dador.Any(e => e.DadorId == id);
        }
    }
}
