using System;
using Xunit;

namespace GamTests
{
    using System.Collections.Generic;


    using GAM.Controllers.LaboratorioController;
    using GAM.Data;
    using GAM.Models.Laboratorio;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using GAM.Models.Enums;

    using GAM.Models.RegistoMaterial;

    public class MateriaisControllerTest
    {

        private ApplicationDbContext GetContextWithoutData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.Material.AddRange(new List<Material>());

            int changed = context.SaveChanges();

            return context;
        }

        [Fact]
        public void TestIndexMateriais()
        {
            // Arrange
            ApplicationDbContext controller;
            controller = GetContextWithoutData();
            MateriaisController material_controller = new MateriaisController(controller);

            // Act
            var actionResultTask = material_controller.Index();
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.Model); // add additional checks on the Model
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");
        }

        [Fact]
        public void TestCreateMateriais()
        {
            ApplicationDbContext controller;
            controller = GetContextWithoutData();
            MateriaisController material_controller = new MateriaisController(controller);
           
            //MaterialId, Categoria, EspermogramaId, Lote, Nome, QuantidadeUtilizada
            var materialToAdd = new Material
            {
                MaterialId = 1,
                Categoria = CategoriaEnum.Laboratorial,
                EspermogramaId = 1,
                Lote = "2",
                Nome = "luva",
                QuantidadeUtilizada = 2  
            };

            // Act
            var actionResultTask = material_controller.Create(materialToAdd);
            controller.Add(materialToAdd);

            // Assert
            Assert.True(actionResultTask.IsCompletedSuccessfully);

        }
    }
}
