﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAM.Data;
using GAM.Models.Questionarios;
using GAM.Models.DadorViewModels;
using static GAM.Models.Enums.GamEnums;
using GAM.Services;

namespace GAM.Controllers.DadorController
{
    public class InqueritoAssistenteSocialController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly TextEmotionService _textEmotionService;

        public InqueritoAssistenteSocialController(ApplicationDbContext context)
        {
            _context = context;
            _textEmotionService = new TextEmotionService();
        }

        // GET: InqueritoAssistenteSocial
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questionario.Where(q => q.Area.Equals(AreaQuestionarioEnum.AssistenteSocial)).ToListAsync());
        }

        // GET: InqueritoAssistenteSocial/Details/5
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

        // GET: InqueritoAssistenteSocial/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InqueritoAssistenteSocial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionarioId,Area")] Questionario questionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionario);
        }

        // GET: InqueritoAssistenteSocial/Edit/5
        public async Task<IActionResult> Edit()
        {
            var questionario = await _context.Questionario.Where(q => q.Area.Equals(AreaQuestionarioEnum.AssistenteSocial)).SingleOrDefaultAsync();
            if (questionario == null)
            {
                return NotFound();
            }

            var perguntas = await _context.Pergunta.Where(p => p.QuestionarioId == questionario.QuestionarioId).ToListAsync();

            InqueritoAssistenteSocialViewModel iasVM = new InqueritoAssistenteSocialViewModel
            {
                QuestionarioId = questionario.QuestionarioId,
                Perguntas = perguntas
            };

            return View(iasVM);
        }

        // POST: InqueritoAssistenteSocial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,QuestionarioId,Perguntas,Respostas")] InqueritoAssistenteSocialViewModel inqueritoAssistenteSocialViewModel)
        {
            if (id != inqueritoAssistenteSocialViewModel.QuestionarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Dador dador = await _context.Dador.Where(d => d.Nome == inqueritoAssistenteSocialViewModel.Nome).SingleOrDefaultAsync();

                    if(dador == null)
                    {
                        return NotFound();
                    }

                    for(int i=0; i<inqueritoAssistenteSocialViewModel.Perguntas.Count; i++)
                    {
                        Resposta resposta = new Resposta
                        {
                            DadorId = dador.DadorId,
                            Dador = dador,
                            PerguntaId = inqueritoAssistenteSocialViewModel.Perguntas[i].PerguntaId,
                            Pergunta = inqueritoAssistenteSocialViewModel.Perguntas[i],
                            TextoResposta = inqueritoAssistenteSocialViewModel.Respostas[i]
                        };

                        _context.Add(resposta);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionarioExists(inqueritoAssistenteSocialViewModel.QuestionarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                //Analyze sentiment CORRIGIR
                _textEmotionService.AnalyzeEmotion(inqueritoAssistenteSocialViewModel.Respostas);

                return RedirectToAction(nameof(Index));
            }
            return View(inqueritoAssistenteSocialViewModel);
        }

        // GET: InqueritoAssistenteSocial/Delete/5
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

        // POST: InqueritoAssistenteSocial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionario = await _context.Questionario.SingleOrDefaultAsync(m => m.QuestionarioId == id);
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
