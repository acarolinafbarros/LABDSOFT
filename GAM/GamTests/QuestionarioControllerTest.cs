using System;
using System.Collections.Generic;
using System.Text;
using GAM.Controllers.MedicoController;
using Xunit;
using GAM.Data;
using Microsoft.EntityFrameworkCore;
using GAM.Models.DadorViewModels;
using Microsoft.AspNetCore.Mvc;
using GAM.Models.Enums;
using System.Linq;
using GAM.Controllers.QuestionarioController;
using GAM.Models.Questionarios;

namespace GamTests
{
    public class QuestionarioControllerTest
    {
        private static ApplicationDbContext context;
        private QuestionarioController _questionarioController = new QuestionarioController(GetContextWithoutData());

        private static ApplicationDbContext GetContextWithoutData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);

            var questionarioSemPerguntas = new Questionario
            {
                Area =GamEnums.AreaQuestionarioEnum.Medico,
                Perguntas = new List<Pergunta>(),
            };
            var questionarioComPerguntas = new Questionario
            {
                Area = GamEnums.AreaQuestionarioEnum.Medico,
                Perguntas = new List<Pergunta>
                {
                    new Pergunta
                    {
                        Apagado = false,
                        Descricao = "P1",
                        TipoResposta = TipoRespostaEnum.SimNao,
                    },
                    new Pergunta
                    {
                        Apagado = false,
                        Descricao = "P2",
                        TipoResposta = TipoRespostaEnum.SimNao,
                    },
                    new Pergunta
                    {
                        Apagado = false,
                        Descricao = "P3",
                        TipoResposta = TipoRespostaEnum.SimNao,
                    }

                },
            };

            context.Add(questionarioSemPerguntas);
            context.Add(questionarioComPerguntas);
            context.SaveChanges();

            return context;
        }

        //TODO Refazer caso de teste.
        //[Fact]
        //public void TestIndex()
        //{
        //    // Act
        //    var actionResultTask = _questionarioController.Index();
        //    actionResultTask.Wait();
        //    var viewResult = actionResultTask.Result as ViewResult;

        //    // Assert
        //    Assert.NotNull(viewResult);
        //    Assert.NotNull(viewResult.ViewData.Model); // add additional checks on the Model
        //    Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Create" || viewResult.ViewName == "Edit");

        //    var model = Assert.IsAssignableFrom<IEnumerable<Dador>>(viewResult.Model);
        //    Assert.Single(model);
        //}

        [Fact]
        public void TestEdit()
        {
            var editModel = context.Questionario.FirstOrDefault();
            editModel.Area = GamEnums.AreaQuestionarioEnum.AssistenteSocial;
            var perguntas = Newtonsoft.Json.JsonConvert.SerializeObject(editModel.Perguntas);
            // Act
            var actionResultTask = _questionarioController.Edit(editModel.QuestionarioId, editModel, perguntas);
            actionResultTask.Wait();

            // Assert
            Assert.True(actionResultTask.IsCompletedSuccessfully);

            var result = context.Questionario.Single(d => d.QuestionarioId == editModel.QuestionarioId);
            Assert.Equal(GamEnums.AreaQuestionarioEnum.AssistenteSocial, result.Area);
        }

    }
}
