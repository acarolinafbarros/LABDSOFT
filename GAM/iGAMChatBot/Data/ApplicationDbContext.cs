namespace iGAMChatBot.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        //public virtual DbSet<Amostra> Amostra { get; set; }
        //public virtual DbSet<Analise> Analise { get; set; }
        //public virtual DbSet<Casal> Casal { get; set; }
        //public virtual DbSet<Consulta> Consulta { get; set; }
        //public virtual DbSet<Dador> Dador { get; set; }
        //public virtual DbSet<Espermograma> Espermograma { get; set; }
        //public virtual DbSet<LocalizacaoAmostra> LocalizacaoAmostra { get; set; }
        //public virtual DbSet<Material> Material { get; set; }
        //public virtual DbSet<Metodo> Metodo { get; set; }
        //public virtual DbSet<PedidoGametas> PedidoGameta { get; set; }
        //public virtual DbSet<Pergunta> Pergunta { get; set; }
        //public virtual DbSet<Questionario> Questionario { get; set; }
        //public virtual DbSet<Resposta> Resposta { get; set; }
        //public virtual DbSet<ResultadoAnalise> ResultadoAnalise { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
