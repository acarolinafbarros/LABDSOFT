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
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public void GetAllAmostras()
        {

            ApplicationDbContext controller;
            controller = GetContextWithoutData();
            AmostrasController amostra_controller = new AmostrasController(controller);
         
            //var viewResult = (ViewResult)amostra_controller.Index();
            //var viewName = viewResult.ViewName;

            //Assert.True(string.IsNullOrEmpty(viewName) || viewName == “Index”);
        }

        
    }
}
