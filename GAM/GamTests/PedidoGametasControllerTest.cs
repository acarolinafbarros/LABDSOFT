using GAM.Controllers.PMAController;
using GAM.Data;
using GAM.Models;
using GAM.Models.Enums;
using GAM.Models.PMAViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace GamTests
{
    public class PedidoGametasControllerTest
    {
        private static ApplicationDbContext context;
        private PedidoGametasController _pedidoGametasController = new PedidoGametasController(GetContextWithoutData());

        private static ApplicationDbContext GetContextWithoutData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);

            var novoCasal = new Casal
            {
                CasalID = 1,
                IdadeHomem = 45,
                RacaHomem = "Latino",
                AlturaHomem = 177,
                CorCabeloHomem = "Castanho",
                GrupoSanguineoHomem = GrupoSanguineoEnum.ANeg,
                TexturaCabeloHomem = "Liso",
                CorOlhosHomem = "Castanho",
                CorPeleHomem = "Moreno",

                IdadeMulher = 38,
                RacaMulher = "Latino",
                AlturaMulher = 155,
                CorCabeloMulher = "Loiro",
                GrupoSanguineoMulher = GrupoSanguineoEnum.ABNeg,
                TexturaCabeloMulher = "Ondulado",
                CorOlhosMulher = "Azul",
                CorPeleMulher = "Branco"
            };

            context.Add(novoCasal);
            context.SaveChanges();

            var novoPedidoGametas = new PedidoGametas
            {
                PedidoGametasId = 1,
                CasalId = 1,
                Data = DateTime.UtcNow,
                Centro = "Centro PMA Leiria",
                RefExterna = "PMLEI2002",
                EstadoProcessoPedido = EstadoProcesso.EmAnalise
            };

            context.Add(novoPedidoGametas);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void TestIndex()
        {
            // Act
            var actionResultTask = _pedidoGametasController.Index();
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.ViewData.Model); // add additional checks on the Model
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");

            var model = Assert.IsAssignableFrom<IEnumerable<PedidoGametasViewModel>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public void TestDetails()
        {
            // Act
            var actionResultTask = _pedidoGametasController.Details(1);
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.ViewData.Model); // add additional checks on the Model
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Details");

            var model = Assert.IsAssignableFrom<PedidoGametasViewModel>(viewResult.Model);
            Assert.Equal("Centro PMA Leiria", model.Centro);
        }
     
    }
}
