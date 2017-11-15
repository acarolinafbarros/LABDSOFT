using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.Questionarios;
using Newtonsoft.Json;

namespace GAM.Controllers.QuestionarioController
{
    public class QuestionarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Questionario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questionario.ToListAsync());
        }

        // GET: Questionario/Edit/5
        public async Task<IActionResult> Preview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionario = await _context.Questionario.SingleOrDefaultAsync(m => m.QuestionarioId == id);
            if (questionario == null)
            {
                return NotFound();
            }
            return View(questionario);
        }

        // GET: Questionario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionario = await _context.Questionario
                .SingleOrDefaultAsync(m => m.QuestionarioId == id);
            if (questionario == null)
            {
                return NotFound();
            }

            return View(questionario);
        }

        // GET: Questionario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questionario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionarioId,Area")] Questionario questionario, string perguntasJson)
        {
            var perguntas  = JsonConvert.DeserializeObject<List<Pergunta>>(perguntasJson);
            questionario.Perguntas = perguntas;
            if (ModelState.IsValid)
            {
                _context.Add(questionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionario);
        }

        // GET: Questionario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionario = await _context.Questionario.SingleOrDefaultAsync(m => m.QuestionarioId == id);
            if (questionario == null)
            {
                return NotFound();
            }
            var perguntas = await _context.Pergunta.Where(m => m.QuestionarioId == id).ToListAsync();
            questionario.Perguntas = perguntas;

            return View(questionario);
        }

        // POST: Questionario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionarioId,Area")] Questionario questionario, string perguntasJson)
        {
            if (id != questionario.QuestionarioId)
            {
                return NotFound();
            }

            var perguntas = JsonConvert.DeserializeObject<List<Pergunta>>(perguntasJson);
            //questionario.Perguntas = perguntas;

            if (ModelState.IsValid)
            {
                try
                {
                    var perguntasId = perguntas.Select(x => x.PerguntaId);
                    var listaDb = await _context.Pergunta.Where(m => m.QuestionarioId == id).ToListAsync();
                    //Tratamento das perguntas
                    foreach (var pergunta in listaDb.ToList())
                    {
                        if (perguntasId.Contains(pergunta.PerguntaId))
                        {
                            var updatedQuestion = perguntas.SingleOrDefault(x => x.PerguntaId == pergunta.PerguntaId);
                            pergunta.Descricao = updatedQuestion.Descricao;
                            pergunta.TipoResposta = updatedQuestion.TipoResposta;
                            _context.Update(pergunta);
                        }
                        else
                        {
                            _context.Pergunta.Remove(pergunta);
                        }
                    }

                    _context.Pergunta.AddRange(perguntas.Where(x => x.PerguntaId == -1).Select(x=>new Pergunta
                    {
                        Descricao = x.Descricao,
                        QuestionarioId = questionario.QuestionarioId,
                        TipoResposta = x.TipoResposta
                    }));

                    _context.Update(questionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionarioExists(questionario.QuestionarioId))
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
            return View(questionario);
        }

        // GET: Questionario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionario = await _context.Questionario
                .SingleOrDefaultAsync(m => m.QuestionarioId == id);
            if (questionario == null)
            {
                return NotFound();
            }

            return View(questionario);
        }

        // POST: Questionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionario = await _context.Questionario.SingleOrDefaultAsync(m => m.QuestionarioId == id);
            _context.Pergunta.RemoveRange(await _context.Pergunta.Where(x=>x.QuestionarioId== id).ToListAsync());
            _context.Questionario.Remove(questionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionarioExists(int id)
        {
            return _context.Questionario.Any(e => e.QuestionarioId == id);
        }
    }
}
