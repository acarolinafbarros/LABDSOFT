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
using GAM.Models.Enums;
using System.Linq;

namespace GamTests
{
    
    public class ListaTrabalhoLaboratorioControllerTest
    {
        private static ApplicationDbContext context;
        private ListaTrabalhoLaboratorioController _listaTrabalhoLaboratorioController = new ListaTrabalhoLaboratorioController(GetContextWithoutData());

        private static ApplicationDbContext GetContextWithoutData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);

            var dador = new Dador
            {
                DadorId = 1,
                Nome = "TESTER"
            };

            var amostraPorAnalis = new Amostra
            {
                DadorId = 1,
                EstadoAmostra = EstadoAmostraEnum.PorAnalisar,
                TipoAmostra = TipoAmostraEnum.Espermatozoide,
                DataRecolha = DateTime.UtcNow,
                Banco = GamEnums.TipoBancoEnum.Indefinido,
                Piso = GamEnums.PisoEnum.Indefinido,
                Cannister = GamEnums.CannisterEnum.Indefinido,
                GlobetCor = GamEnums.GlobetCorEnum.Indefinido,
                GlobetNumero = GamEnums.GlobetNumeroEnum.Indefinido,
                PalhetaCor = GamEnums.PalhetaCorEnum.Indefinido,
                Dador = dador
            };

            context.Add(amostraPorAnalis);
            context.SaveChanges();

            var amostraEmAnalis = new Amostra
            {
                DadorId = 1,
                EstadoAmostra = EstadoAmostraEnum.EmAnalise,
                TipoAmostra = TipoAmostraEnum.Espermatozoide,
                DataRecolha = DateTime.UtcNow,
                Banco = GamEnums.TipoBancoEnum.Indefinido,
                Piso = GamEnums.PisoEnum.Indefinido,
                Cannister = GamEnums.CannisterEnum.Indefinido,
                GlobetCor = GamEnums.GlobetCorEnum.Indefinido,
                GlobetNumero = GamEnums.GlobetNumeroEnum.Indefinido,
                PalhetaCor = GamEnums.PalhetaCorEnum.Indefinido,
                Dador = dador
            };

            context.Add(amostraEmAnalis);
            context.SaveChanges();

            var amostraAnalis = new Amostra
            {
                DadorId = 1,
                EstadoAmostra = EstadoAmostraEnum.Analisada,
                TipoAmostra = TipoAmostraEnum.Espermatozoide,
                DataRecolha = DateTime.UtcNow,
                Banco = GamEnums.TipoBancoEnum.Indefinido,
                Piso = GamEnums.PisoEnum.Indefinido,
                Cannister = GamEnums.CannisterEnum.Indefinido,
                GlobetCor = GamEnums.GlobetCorEnum.Indefinido,
                GlobetNumero = GamEnums.GlobetNumeroEnum.Indefinido,
                PalhetaCor = GamEnums.PalhetaCorEnum.Indefinido,
                Dador = dador
            };

            context.Add(amostraAnalis);
            context.SaveChanges();

            var amostraCrio = new Amostra
            {
                DadorId = 1,
                EstadoAmostra = EstadoAmostraEnum.Criopreservada,
                TipoAmostra = TipoAmostraEnum.Espermatozoide,
                DataRecolha = DateTime.UtcNow,
                Banco = GamEnums.TipoBancoEnum.Indefinido,
                Piso = GamEnums.PisoEnum.Indefinido,
                Cannister = GamEnums.CannisterEnum.Indefinido,
                GlobetCor = GamEnums.GlobetCorEnum.Indefinido,
                GlobetNumero = GamEnums.GlobetNumeroEnum.Indefinido,
                PalhetaCor = GamEnums.PalhetaCorEnum.Indefinido,
                Dador = dador
            };

            context.Add(amostraCrio);
            context.SaveChanges();

            return context;
        }


        [Fact]
        public void TestIndex()
        {
            // Act
            var actionResultTask = _listaTrabalhoLaboratorioController.Index();
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.ViewData.Model); // add additional checks on the Model
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");

            var model = Assert.IsAssignableFrom<List<Amostra>>(viewResult.Model);
            Assert.Equal(3, model.Count());
        }

    }
}
