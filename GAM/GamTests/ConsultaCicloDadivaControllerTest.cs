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
    public class ConsultaCicloDadivaControllerTest
    {
        //private static ApplicationDbContext context;
        //private ConsultaCicloDadivaController _consultaCicloDadivaController = new ConsultaCicloDadivaController(GetContextWithoutData());

        //private static ApplicationDbContext GetContextWithoutData()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .Options;

        //    context = new ApplicationDbContext(options);

        //    var dador = new Dador
        //    {
        //        DadorId = 1,
        //        Nome = "Marcelo Moreno",
        //        Morada = "Praça D. Joao II",
        //        DataNasc = DateTime.UtcNow,
        //        LocalNasc = "Amarante",
        //        DocIdentificacao = "123987456",
        //        Nacionalidade = "Portugal",
        //        Profissao = "Engenheiro Quimico",
        //        GrauEscolaridade = GrauEscolaridadeEnum.Mestrado,
        //        EstadoCivil = EstadoCivilEnum.Viuvo,
        //        NumFilhos = 0,
        //        Altura = 185,
        //        Peso = 78,
        //        CorPele = "Branco",
        //        CorOlhos = "Verde",
        //        CorCabelo = "Preto",
        //        TexturaCabelo = "Liso",
        //        GrupoSanguineo = GrupoSanguineoEnum.BNeg,
        //        Etnia = "Apache",
        //        IniciaisDador = "MM",
        //        FaseDador = FaseDadorEnum.PrimeiraDadiva,
        //        EstadoDador = EstadoDadorEnum.ProcessoInativo,
        //        DadosDador = ValidacaoEnum.Aceite,
        //        NumAbortos = 0,
        //        TotalGestacoes = 0
        //    };

        //    var dador2 = new Dador
        //    {
        //        DadorId = 2,
        //        Nome = "Marcelo Branco",
        //        Morada = "Praça D. Joao II",
        //        DataNasc = DateTime.UtcNow,
        //        LocalNasc = "Amarante",
        //        DocIdentificacao = "123987456",
        //        Nacionalidade = "Portugal",
        //        Profissao = "Engenheiro Quimico",
        //        GrauEscolaridade = GrauEscolaridadeEnum.Mestrado,
        //        EstadoCivil = EstadoCivilEnum.Viuvo,
        //        NumFilhos = 0,
        //        Altura = 185,
        //        Peso = 78,
        //        CorPele = "Branco",
        //        CorOlhos = "Verde",
        //        CorCabelo = "Preto",
        //        TexturaCabelo = "Liso",
        //        GrupoSanguineo = GrupoSanguineoEnum.BNeg,
        //        Etnia = "Apache",
        //        IniciaisDador = "MM",
        //        FaseDador = FaseDadorEnum.PrimeiraDadiva,
        //        EstadoDador = EstadoDadorEnum.ProcessoInativo,
        //        DadosDador = ValidacaoEnum.Pendente,
        //        NumAbortos = 0,
        //        TotalGestacoes = 0
        //    };

        //    context.Add(dador);
        //    context.Add(dador2);
        //    context.SaveChanges();

        //    return context;
        //}

        //[Fact]
        //public void TestIndex()
        //{
        //    // Act
        //    var actionResultTask = _consultaCicloDadivaController.Index();
        //    actionResultTask.Wait();
        //    var viewResult = actionResultTask.Result as ViewResult;

        //    // Assert
        //    Assert.NotNull(viewResult);
        //    Assert.NotNull(viewResult.ViewData.Model); // add additional checks on the Model
        //    Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");

        //    var model = Assert.IsAssignableFrom<IEnumerable<Dador>>(viewResult.Model);
        //    Assert.Single(model);
        //}

        //[Fact]
        //public void TestDetails()
        //{
        //    // Act
        //    var actionResultTask = _consultaCicloDadivaController.Details(1);
        //    actionResultTask.Wait();
        //    var viewResult = actionResultTask.Result as ViewResult;

        //    // Assert
        //    Assert.NotNull(viewResult);
        //    Assert.NotNull(viewResult.ViewData.Model); // add additional checks on the Model
        //    Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Details");

        //    var model = Assert.IsAssignableFrom<Dador>(viewResult.Model);
        //    Assert.Equal(1, model.DadorId);
        //}

        //[Fact]
        //public void TestEdit()
        //{
        //    var dador = context.Dador.Where(d => d.DadorId == 1).Single();
        //    dador.EstadoDador = EstadoDadorEnum.PendenteAprovacao;

        //    // Act
        //    var actionResultTask = _consultaCicloDadivaController.Edit(1, dador);
        //    actionResultTask.Wait();

        //    // Assert
        //    Assert.True(actionResultTask.IsCompletedSuccessfully);

        //    var resultDador = context.Dador.Where(d => d.DadorId == 1).Single();
        //    Assert.Equal(EstadoDadorEnum.PendenteAprovacao, resultDador.EstadoDador);
        //}
    }
}
