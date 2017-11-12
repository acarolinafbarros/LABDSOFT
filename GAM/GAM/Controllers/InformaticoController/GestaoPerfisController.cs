using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.InformaticoViewModels;
using GAM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using GAM.Models.Enums;

namespace GAM.Controllers.InformaticoController
{
    public class GestaoPerfisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GestaoPerfisController(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        }

        // GET: GestaoPerfis
        public async Task<IActionResult> Index()
        {
            var utilizadores = _context.Users.ToList();

            ICollection<GestaoPerfisViewModel> model = new List<GestaoPerfisViewModel>();

            foreach (var u in utilizadores)
            {
                var role = await _userManager.GetRolesAsync(u);
                GestaoPerfisViewModel gestaoPerfisVM = new GestaoPerfisViewModel { NomeUtilizador = u.UserName, Email = u.Email, Perfil = (PerfilEnum)Enum.Parse(typeof(PerfilEnum), role.First()) };
                model.Add(gestaoPerfisVM);
            }

            return View(model);
        }

        // GET: GestaoPerfis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GestaoPerfis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeUtilizador,Email,Password,ConfirmPassword,Perfil")] GestaoPerfisViewModel gestaoPerfisViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = gestaoPerfisViewModel.NomeUtilizador, Email = gestaoPerfisViewModel.Email };
                var result = await _userManager.CreateAsync(user, gestaoPerfisViewModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(PerfilEnum), gestaoPerfisViewModel.Perfil));
                }

                return RedirectToAction(nameof(Index));
            }
            return View(gestaoPerfisViewModel);
        }

        // GET: GestaoPerfis/Edit/5
        public async Task<IActionResult> Edit(string nomeUtilizador)
        {
            if (nomeUtilizador == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Users.SingleOrDefaultAsync(m => m.UserName == nomeUtilizador);
            if (utilizador == null)
            {
                return NotFound();
            }

            var role = await _userManager.GetRolesAsync(utilizador);

            GestaoPerfisEditarViewModel gestaoPerfisEditarVM = new GestaoPerfisEditarViewModel {UtilizadorId = utilizador.Id, NomeUtilizador = utilizador.UserName, Email = utilizador.Email, Perfil = (PerfilEnum)Enum.Parse(typeof(PerfilEnum), role.First()) };

            return View(gestaoPerfisEditarVM);
        }

        // POST: GestaoPerfis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string UtilizadorId, [Bind("UtilizadorId,NomeUtilizador,Email,AntigaPassword,NovaPassword,ConfirmarPassword,Perfil")] GestaoPerfisEditarViewModel gestaoPerfisEditarViewModel)
        {
            if (UtilizadorId != gestaoPerfisEditarViewModel.UtilizadorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == UtilizadorId);
                    var current_role = await _userManager.GetRolesAsync(user);

                    var result_passwd = await _userManager.ChangePasswordAsync(user, gestaoPerfisEditarViewModel.AntigaPassword, gestaoPerfisEditarViewModel.NovaPassword);

                    if (result_passwd.Succeeded)
                    {
                        user.UserName = gestaoPerfisEditarViewModel.NomeUtilizador;
                        user.Email = gestaoPerfisEditarViewModel.Email;

                        var result = await _userManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            await _userManager.RemoveFromRoleAsync(user, current_role.First());
                            await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(PerfilEnum), gestaoPerfisEditarViewModel.Perfil));
                        }
                    }              
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadorExists(gestaoPerfisEditarViewModel.UtilizadorId))
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
            return View(gestaoPerfisEditarViewModel);
        }

        // GET: GestaoPerfis/Delete/5
        public async Task<IActionResult> Delete(string nomeUtilizador)
        {
            if (nomeUtilizador == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Users.SingleOrDefaultAsync(m => m.UserName == nomeUtilizador);

            if (utilizador == null)
            {
                return NotFound();
            }

            var role = await _userManager.GetRolesAsync(utilizador);

            GestaoPerfisViewModel gestaoPerfisVM = new GestaoPerfisViewModel { UtilizadorId = utilizador.Id, NomeUtilizador = utilizador.UserName, Email = utilizador.Email, Perfil = (PerfilEnum)Enum.Parse(typeof(PerfilEnum), role.First()) };

            return View(gestaoPerfisVM);
        }

        // POST: GestaoPerfis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string utilizadorId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == utilizadorId);
            var current_role = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRoleAsync(user, current_role.First());
            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadorExists(string UtilizadorId)
        {
            return _context.Users.Any(u => u.Id == UtilizadorId);
        }
    }
}
