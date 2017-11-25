using System;
using Xunit;

namespace GamTests
{
    using System.Collections.Generic;
    using System.Linq;

    using GAM.Controllers.LaboratorioController;
    using GAM.Data;
    using GAM.Models.Laboratorio;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using GAM.Models.Enums;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class AmostraControllerTest
    {

        private ApplicationDbContext GetContextWithoutData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.Amostra.AddRange(new List<Amostra>());

            int changed = context.SaveChanges();

            return context;
        }

        [Fact]
        public void TestIndexAmostras()
        {
            // Arrange
            ApplicationDbContext controller;
            controller = GetContextWithoutData();
            AmostrasController amostra_controller = new AmostrasController(controller);

            // Act
            var actionResultTask = amostra_controller.Index();
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.Model); // add additional checks on the Model
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");
        }

        [Fact]
        public void TestCreateAmostras()
        {
            ApplicationDbContext controller;
            controller = GetContextWithoutData();
            AmostrasController amostra_controller = new AmostrasController(controller);
            //AmostraId,DadorId,EstadoAmostra,TipoAmostra,DataRecolha,NrAmosta"
            var amostraToAdd = new Amostra
                                   {
                                       DadorId = 1,
                                       EstadoAmostra = EstadoAmostraEnum.EmAnalise,
                                       TipoAmostra = TipoAmostraEnum.Sangue,
                                       DataRecolha = DateTime.UtcNow
                                   };

            // Act
            var actionResultTask = amostra_controller.Create(amostraToAdd);
            controller.Add(amostraToAdd);

            // Assert
            Assert.Equal(true, actual: actionResultTask.IsCompletedSuccessfully);

        }
    }
}
