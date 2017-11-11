using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class AtualizacaoModeloDados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoDador",
                table: "Dador");

            migrationBuilder.RenameColumn(
                name: "DadorID",
                table: "Dador",
                newName: "DadorId");

            migrationBuilder.AlterColumn<string>(
                name: "TexturaCabelo",
                table: "Dador",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Profissao",
                table: "Dador",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Dador",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nacionalidade",
                table: "Dador",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Morada",
                table: "Dador",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "LocalNasc",
                table: "Dador",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Etnia",
                table: "Dador",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "DocIdentificacao",
                table: "Dador",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CorPele",
                table: "Dador",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "CorOlhos",
                table: "Dador",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "CorCabelo",
                table: "Dador",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "IniciaisDador",
                table: "Dador",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Amostra",
                columns: table => new
                {
                    AmostraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DadorId = table.Column<int>(type: "int", nullable: false),
                    DataRecolha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoAmostra = table.Column<int>(type: "int", nullable: false),
                    Localizacao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NrAmosta = table.Column<int>(type: "int", nullable: false),
                    TipoAmostra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amostra", x => x.AmostraId);
                    table.ForeignKey(
                        name: "FK_Amostra_Dador_DadorId",
                        column: x => x.DadorId,
                        principalTable: "Dador",
                        principalColumn: "DadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questionario",
                columns: table => new
                {
                    QuestionarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Area = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionario", x => x.QuestionarioId);
                });

            migrationBuilder.CreateTable(
                name: "ResultadoAnalise",
                columns: table => new
                {
                    ResultadoAnaliseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomeEmbriologista = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NomeMedico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ValidacaoLaboratorio = table.Column<int>(type: "int", nullable: false),
                    ValidacaoMedico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadoAnalise", x => x.ResultadoAnaliseId);
                });

            migrationBuilder.CreateTable(
                name: "Espermograma",
                columns: table => new
                {
                    EspermogramaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmostraId = table.Column<int>(type: "int", nullable: false),
                    ConcentracaoEspermatozoides = table.Column<float>(type: "real", nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DataEspermograma = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrauA = table.Column<int>(type: "int", nullable: false),
                    GrauB = table.Column<int>(type: "int", nullable: false),
                    GrauC = table.Column<int>(type: "int", nullable: false),
                    GrauD = table.Column<int>(type: "int", nullable: false),
                    Leucocitos = table.Column<float>(type: "real", nullable: false),
                    Liquefacao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ObservacoesConcentracao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Ph = table.Column<float>(type: "real", nullable: false),
                    Viscosidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Vitalidade = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espermograma", x => x.EspermogramaId);
                    table.ForeignKey(
                        name: "FK_Espermograma_Amostra_AmostraId",
                        column: x => x.AmostraId,
                        principalTable: "Amostra",
                        principalColumn: "AmostraId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pergunta",
                columns: table => new
                {
                    PerguntaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionarioId = table.Column<int>(type: "int", nullable: false),
                    Resposta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoResposta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pergunta", x => x.PerguntaId);
                    table.ForeignKey(
                        name: "FK_Pergunta_Questionario_QuestionarioId",
                        column: x => x.QuestionarioId,
                        principalTable: "Questionario",
                        principalColumn: "QuestionarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Analise",
                columns: table => new
                {
                    AnaliseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmostraId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ResultadoAnaliseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analise", x => x.AnaliseId);
                    table.ForeignKey(
                        name: "FK_Analise_Amostra_AmostraId",
                        column: x => x.AmostraId,
                        principalTable: "Amostra",
                        principalColumn: "AmostraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Analise_ResultadoAnalise_ResultadoAnaliseId",
                        column: x => x.ResultadoAnaliseId,
                        principalTable: "ResultadoAnalise",
                        principalColumn: "ResultadoAnaliseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Categoria = table.Column<int>(type: "int", nullable: false),
                    EspermogramaId = table.Column<int>(type: "int", nullable: false),
                    Lote = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QuantidadeUtilizada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialId);
                    table.ForeignKey(
                        name: "FK_Material_Espermograma_EspermogramaId",
                        column: x => x.EspermogramaId,
                        principalTable: "Espermograma",
                        principalColumn: "EspermogramaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Metodo",
                columns: table => new
                {
                    MetodoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnaliseId = table.Column<int>(type: "int", nullable: false),
                    InterpretacaoNeg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterpretacaoPos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resultado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultadoNumerico = table.Column<float>(type: "real", nullable: false),
                    ValorReferenciaNeg = table.Column<float>(type: "real", nullable: false),
                    ValorReferenciaPos = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metodo", x => x.MetodoId);
                    table.ForeignKey(
                        name: "FK_Metodo_Analise_AnaliseId",
                        column: x => x.AnaliseId,
                        principalTable: "Analise",
                        principalColumn: "AnaliseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amostra_DadorId",
                table: "Amostra",
                column: "DadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Analise_AmostraId",
                table: "Analise",
                column: "AmostraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Analise_ResultadoAnaliseId",
                table: "Analise",
                column: "ResultadoAnaliseId");

            migrationBuilder.CreateIndex(
                name: "IX_Espermograma_AmostraId",
                table: "Espermograma",
                column: "AmostraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Material_EspermogramaId",
                table: "Material",
                column: "EspermogramaId");

            migrationBuilder.CreateIndex(
                name: "IX_Metodo_AnaliseId",
                table: "Metodo",
                column: "AnaliseId");

            migrationBuilder.CreateIndex(
                name: "IX_Pergunta_QuestionarioId",
                table: "Pergunta",
                column: "QuestionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Metodo");

            migrationBuilder.DropTable(
                name: "Pergunta");

            migrationBuilder.DropTable(
                name: "Espermograma");

            migrationBuilder.DropTable(
                name: "Analise");

            migrationBuilder.DropTable(
                name: "Questionario");

            migrationBuilder.DropTable(
                name: "Amostra");

            migrationBuilder.DropTable(
                name: "ResultadoAnalise");

            migrationBuilder.DropColumn(
                name: "IniciaisDador",
                table: "Dador");

            migrationBuilder.RenameColumn(
                name: "DadorId",
                table: "Dador",
                newName: "DadorID");

            migrationBuilder.AlterColumn<string>(
                name: "TexturaCabelo",
                table: "Dador",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Profissao",
                table: "Dador",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Dador",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nacionalidade",
                table: "Dador",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Morada",
                table: "Dador",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LocalNasc",
                table: "Dador",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Etnia",
                table: "Dador",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocIdentificacao",
                table: "Dador",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorPele",
                table: "Dador",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorOlhos",
                table: "Dador",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorCabelo",
                table: "Dador",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoDador",
                table: "Dador",
                maxLength: 20,
                nullable: true);
        }
    }
}
