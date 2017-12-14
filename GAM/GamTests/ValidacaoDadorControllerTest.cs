using GAM.Controllers.EnfermeiraCoordenadoraController;
using GAM.Data;
using GAM.Models.DadorViewModels;
using GAM.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GamTests
{
    public class ValidacaoDadorControllerTest
    {
        private static ApplicationDbContext context;
        private static EncryptorTestInstance encryptorTestInstance = new EncryptorTestInstance();

        private ValidacaoDadorController _validacaoDadorController = new ValidacaoDadorController(GetContextWithoutData(), encryptorTestInstance.CreateProtector(""));

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
                CorPele = CorPeleEnum.Clara,
                CorOlhos = CorOlhosEnum.Azul,
                CorCabelo = CorCabeloEnum.Loiro,
                TexturaCabelo = TexturaCabeloEnum.Ondulado,
                GrupoSanguineo = GrupoSanguineoEnum.ABNeg,
                Etnia = EtniaEnum.Negro,
                IniciaisDador = "MM123",
                FaseDador = FaseDadorEnum.PrimeiraDadiva,
                EstadoDador = EstadoDadorEnum.PendenteAprovacao,
                DadosDador = ValidacaoEnum.Pendente,
                NumAbortos = 0,
                TotalGestacoes = 0
            };

            var dador2 = new Dador
            {
                DadorId = 2,
                Nome = "Marcelo Branco",
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
                CorPele = CorPeleEnum.Clara,
                CorOlhos = CorOlhosEnum.Azul,
                CorCabelo = CorCabeloEnum.Loiro,
                TexturaCabelo = TexturaCabeloEnum.Ondulado,
                GrupoSanguineo = GrupoSanguineoEnum.ABNeg,
                Etnia = EtniaEnum.Negro,
                IniciaisDador = "MM123",
                FaseDador = FaseDadorEnum.PrimeiraDadiva,
                EstadoDador = EstadoDadorEnum.Aceite,
                DadosDador = ValidacaoEnum.Aceite,
                NumAbortos = 0,
                TotalGestacoes = 0
            };

            context.Add(dador);
            context.Add(dador2);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void TestIndex()
        {
            // Act
            var actionResultTask = _validacaoDadorController.Index();
            actionResultTask.Wait();
            var viewResult = actionResultTask.Result as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.ViewData.Model); // add additional checks on the Model
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");

            var model = Assert.IsAssignableFrom<IEnumerable<Dador>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public void TestEdit()
        {
            var dador = context.Dador.Where(d => d.DadorId == 1).Single();
            dador.DadosDador = ValidacaoEnum.Aceite;

            // Act
            var actionResultTask = _validacaoDadorController.Edit(1, dador);
            actionResultTask.Wait();

            // Assert
            Assert.True(actionResultTask.IsCompletedSuccessfully);

            var resultDador = context.Dador.Where(d => d.DadorId == 1).Single();
            Assert.Equal(ValidacaoEnum.Aceite, resultDador.DadosDador);
        }
    }
}
