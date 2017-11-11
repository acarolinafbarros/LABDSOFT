using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GAM.Models;
using GAM.Models.TestViewModels;
using GAM.Models.DadorViewModels;
using GAM.Models.Laboratorio;

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

        public DbSet<GAM.Models.Laboratorio.Espermograma> Espermograma { get; set; }
    }
}
