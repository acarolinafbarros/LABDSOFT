using System;
using System.Collections.Generic;
using System.Text;
using GAM.Controllers.EmbriologistaController;
using GAM.Controllers.EnfermeiraCoordenadoraController;
using GAM.Data;
using GAM.Models.DadorViewModels;
using GAM.Models.Laboratorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GamTests
{
    
    public class ListaTrabalhoLaboratorioControllerTest
    {
        private static ApplicationDbContext context;
        private ListaTrabalhoLaboratorioController _testController = new ListaTrabalhoLaboratorioController(GetContextWithoutData());

        private static ApplicationDbContext GetContextWithoutData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);

            context.SaveChanges();

            return context;
        }


        [Fact]
        public void TestIndex()
        {
            // Act
            var actionResultTask = _testController.Index();
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.ViewData.Model); // add additional checks on the Model
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");

            var model = Assert.IsAssignableFrom<IEnumerable<Dador>>(viewResult.Model);
            Assert.Single(model);
        }

    }
}
