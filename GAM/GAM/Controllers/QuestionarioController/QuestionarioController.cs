using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.Questionarios;
using Newtonsoft.Json;
using GAM.Helpers;
using GAM.Models.Enums;

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
            if (User.IsInRole("Medico"))
            {
                var quest = _context.Questionario.FirstOrDefault(x => x.Area == GamEnums.AreaQuestionarioEnum.Medico);
                if (quest == null)
                {
                    return Create();
                }
                return RedirectToAction("Edit" ,new{id = quest.QuestionarioId});
            }else if (User.IsInRole("AssistenteSocial"))
            {
                var quest = _context.Questionario.FirstOrDefault(x => x.Area == GamEnums.AreaQuestionarioEnum.AssistenteSocial);
                if (quest == null)
                {
                    return Create();
                }
                return RedirectToAction("Edit", new { id = quest.QuestionarioId });

            }
            return View();
        }

        // GET: Questionario/Preview/5
        public async Task<IActionResult> Preview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionario = await _context.Questionario.SingleOrDefaultAsync(m => m.QuestionarioId == id);
            questionario.Perguntas= await _context.Pergunta.Where(m => m.QuestionarioId == id && !m.Apagado).ToListAsync();
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

        public async Task<IActionResult> EditQuestionario(int? idQuestionario, int? dadorId)
        {
            if (idQuestionario == null || dadorId == null)
            {
                return NotFound();
            }

            var questionario = await _context.Pergunta.Where(m => m.QuestionarioId == idQuestionario && !m.Apagado)
                .Include(x=>x.Respostas).ToListAsync();
            var listaPerguntas = questionario.Select(x => new Resposta
            {
                DadorId = dadorId.GetValueOrDefault(-1),
                Pergunta = x,
                PerguntaId = x.PerguntaId,
                TextoResposta = x.Respostas.FirstOrDefault(r=>r.DadorId==dadorId)?.TextoResposta,
                RespostaId = x.Respostas.FirstOrDefault(r => r.DadorId == dadorId).GetFirstOrDefault(new Resposta()).RespostaId
            });
            if (listaPerguntas == null || !listaPerguntas.Any())
            {
                return NotFound();
            }

            return View(listaPerguntas.ToList());
        }

        // GET: Questionario/RealizarQuestionario/5/3
        //[HttpGet("RealizarQuestionario/{idQuestionario}/{dadorId}")]
        public async Task<IActionResult> RealizarQuestionario(int? idQuestionario, int? dadorId)
        {
            if (idQuestionario == null || dadorId == null)
            {
                return NotFound();
            }

            var questionario = await _context.Pergunta.Where(m => m.QuestionarioId == idQuestionario && !m.Apagado).ToListAsync();
            var listaPerguntas = questionario.Select(x => new Resposta
            {
                DadorId = dadorId.GetValueOrDefault(-1),
                Pergunta = x,
                PerguntaId = x.PerguntaId,
                TextoResposta = ""
            });
            if (listaPerguntas == null || !listaPerguntas.Any())
            {
                return NotFound();
            }

            return View(listaPerguntas.ToList());
        }

        // POST: Questionario/RealizarQuestionario/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RealizarQuestionario(int dadorId, string respostasJson)
        {
            //throw new NotImplementedException();

            var respostas = JsonConvert.DeserializeObject<List<Resposta>>(respostasJson);
            //questionario.Perguntas = perguntas;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Resposta.AddRange(respostas);


                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Dadors", new { id = dadorId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            var questionario = await _context.Pergunta.Where(m => respostas.Select(x=>x.PerguntaId).Contains(m.PerguntaId) ).ToListAsync();
            var listaPerguntas = questionario.Select(x => new Resposta
            {
                DadorId = dadorId,
                Pergunta = x,
                PerguntaId = x.PerguntaId,
                TextoResposta = respostas.FirstOrDefault(r=>r.PerguntaId==x.PerguntaId)?.TextoResposta
            });

            return View(listaPerguntas.ToList());
        } 
        
        // POST: Questionario/RealizarQuestionario/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuestionario(int dadorId, string respostasJson)
        {
            //throw new NotImplementedException();

            var respostas = JsonConvert.DeserializeObject<List<Resposta>>(respostasJson);
            //questionario.Perguntas = perguntas;

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var res in respostas)
                    {
                        var resposta = await _context.Resposta.SingleOrDefaultAsync(x => x.RespostaId == res.RespostaId);
                        resposta.TextoResposta = res.TextoResposta;
                        _context.Update(resposta);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Dadors", new {id = dadorId});
                }
                catch (DbUpdateConcurrencyException)
                {
                    //TODO alert user of error
                }
            }

            var questionario = await _context.Pergunta.Where(m => respostas.Select(x=>x.PerguntaId).Contains(m.PerguntaId) ).ToListAsync();
            var listaPerguntas = questionario.Select(x => new Resposta
            {
                DadorId = dadorId,
                Pergunta = x,
                PerguntaId = x.PerguntaId,
                TextoResposta = respostas.FirstOrDefault(r=>r.PerguntaId==x.PerguntaId)?.TextoResposta
            });

            return View(listaPerguntas.ToList());
        }

        // GET: Questionario/Create
        public IActionResult Create()
        {
            if (User.IsInRole("Medico"))
            {
                return View(new Questionario{Area = GamEnums.AreaQuestionarioEnum.Medico });
            }
            else if (User.IsInRole("AssistenteSocial"))
            {
                return View(new Questionario { Area = GamEnums.AreaQuestionarioEnum.AssistenteSocial });
            }

            return RedirectToAction("Index");
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
                return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }

        private bool QuestionarioExists(int id)
        {
            return _context.Questionario.Any(e => e.QuestionarioId == id);
        }
    }
}
