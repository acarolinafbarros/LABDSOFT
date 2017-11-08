using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class CriacaoTabelaDador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dador",
                columns: table => new
                {
                    DadorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Altura = table.Column<int>(type: "int", nullable: false),
                    CodigoDador = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorCabelo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CorOlhos = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CorPele = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DataNasc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocIdentificacao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstadoCivil = table.Column<int>(type: "int", nullable: false),
                    EstadoDador = table.Column<int>(type: "int", nullable: false),
                    Etnia = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FaseDador = table.Column<int>(type: "int", nullable: false),
                    GrauEscolaridade = table.Column<int>(type: "int", nullable: false),
                    GrupoSanguineo = table.Column<int>(type: "int", nullable: false),
                    LocalNasc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nacionalidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumAbortos = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    Profissao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TexturaCabelo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TotalGestacoes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dador", x => x.DadorID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dador");
        }
    }
}
