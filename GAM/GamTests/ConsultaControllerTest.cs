using System;
using System.Collections.Generic;
using System.Text;

namespace GamTests
{
    using System.Linq;

    using GAM.Controllers;
    using GAM.Controllers.EnfermeiraController;
    using GAM.Data;
    using GAM.Models.DadorViewModels;
    using GAM.Models.Laboratorio;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Xunit;

    public class ConsultaControllerTest
    {

        public ApplicationDbContext GetContextWithtData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            var consulta1 = new Consulta { DadorId = 1, DataConsulta = new DateTime(2017,12,1)};
            var consulta2 = new Consulta { DadorId = 1, DataConsulta = new DateTime(2017,12,8)};

            context.Consulta.Add(consulta1);
            context.Consulta.Add(consulta2);

           context.Consulta.AddRange(new List<Consulta>());

            int changed = context.SaveChanges();

            return context;
        }

        [Fact]
        public void TestIndexConsultas()
        {
            using (var context = GetContextWithtData())
            using (var controller = new ConsultasController(context))
            {
                var actionResultTask = controller.Index();
                actionResultTask.Wait();
                var view = actionResultTask.Result as ViewResult;

                Assert.NotNull(view.ViewData);
                Assert.NotNull(view.ViewData.Model);
            }
        }   
    }
}
