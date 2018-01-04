using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GAM.Data;
using GAM.Models;
using GAM.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GAM.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public SettingsController(ApplicationDbContext context, IDataProtectionProvider provider, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var item = await _context.Settings.SingleOrDefaultAsync();
            if (item == null)
            {
                item = new Settings
                {
                    HappyHourEnd = new TimeSpan(23,59,59),
                    HappyHourBegin = new TimeSpan(0,0,0),
                    PhotoMatchValue = 50,
                    MatchFormula = ""
                };
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Settings s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var itemDb = _context.Settings.SingleOrDefault();
                    if (itemDb == null)
                    {
                        //Create
                        itemDb= new Settings();
                        itemDb.HappyHourBegin = s.HappyHourBegin;
                        itemDb.HappyHourEnd = s.HappyHourEnd;
                        itemDb.PhotoMatchValue = s.PhotoMatchValue;
                        itemDb.MatchFormula = s.MatchFormula;
                        _context.Settings.Add(itemDb);
                    }
                    else
                    {
                        //edit

                        itemDb.HappyHourBegin = s.HappyHourBegin;
                        itemDb.HappyHourEnd = s.HappyHourEnd;
                        itemDb.PhotoMatchValue = s.PhotoMatchValue;
                        itemDb.MatchFormula = s.MatchFormula;
                        _context.Update(itemDb);
                    }
                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }
    }
}