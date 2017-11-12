using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GAM.Models;
using GAM.Models.TestViewModels;
using GAM.Models.DadorViewModels;
using GAM.Models.Laboratorio;
using GAM.Models.Questionarios;
using GAM.Models.RegistoMaterial;

namespace GAM.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Test> Test { get; set; }
        public DbSet<Dador> Dador { get; set; }
        public DbSet<Amostra> Amostra { get; set; }
        public DbSet<Analise> Analise { get; set; }
        public DbSet<Espermograma> Espermograma { get; set; }
        public DbSet<Metodo> Metodo { get; set; }
        public DbSet<ResultadoAnalise> ResultadoAnalise { get; set; }
        public DbSet<Pergunta> Pergunta { get; set; }
        public DbSet<Questionario> Questionario { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<GAM.Models.DadorViewModels.Consulta> Consulta { get; set; }
    }
}
