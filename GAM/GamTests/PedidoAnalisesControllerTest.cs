using GAM.Controllers.MedicoController;
using GAM.Data;
using GAM.Models.DadorViewModels;
using GAM.Models.Enums;
using GAM.Models.Laboratorio;
using GAM.Models.MedicoViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace GamTests
{
    public class PedidoAnalisesControllerTest
    {
        private static ApplicationDbContext context;
        private static EncryptorTestInstance encryptorTestInstance = new EncryptorTestInstance();

        private PedidoAnaliseController _pedidoAnalisesController = new PedidoAnaliseController(GetContextWithoutData(), encryptorTestInstance.CreateProtector(""));

        private static ApplicationDbContext GetContextWithoutData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);

            var dador = new Dador
            {
                DadorId = 1,
                Nome = "Marcelo Moreno",
                Morada = "Praça D. Joao II",
                DataNasc = DateTime.UtcNow,
                LocalNasc = "Amarante",
                DocIdentificacao = "123987456",
                Nacionalidade = "Portugal",
                Profissao = "Engenheiro Quimico",
                GrauEscolaridade = GrauEscolaridadeEnum.Mestrado,
                EstadoCivil = EstadoCivilEnum.Viuvo,
                NumFilhos = 0,
                Altura = 185,
                Peso = 78,
                CorPele = "Branco",
                CorOlhos = CorOlhosEnum.Verde,
                CorCabelo = "Preto",
                TexturaCabelo = "Aspero",
                GrupoSanguineo = GrupoSanguineoEnum.ABNeg,
                Etnia = "Apache",
                IniciaisDador = "MM123",
                FaseDador = FaseDadorEnum.PrimeiraDadiva,
                EstadoDador = EstadoDadorEnum.ProcessoInativo,
                DadosDador = ValidacaoEnum.Aceite,
                NumAbortos = 0,
                TotalGestacoes = 0
            };

            context.Add(dador);
            context.SaveChanges();

            var amostra = new Amostra
            {
                DadorId = 1,
                EstadoAmostra = EstadoAmostraEnum.PorAnalisar,
                TipoAmostra = TipoAmostraEnum.Sangue,
                DataRecolha = DateTime.UtcNow,
            };

            context.Add(amostra);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void TestIndex()
        {
            // Act
            var actionResultTask = _pedidoAnalisesController.Index();
            var viewResult = actionResultTask as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.ViewData.Model); // add additional checks on the Model
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");

            var model = Assert.IsAssignableFrom<IEnumerable<PedidoAnaliseViewModel>>(viewResult.Model);
            Assert.Single(model);
        }
    }
}
