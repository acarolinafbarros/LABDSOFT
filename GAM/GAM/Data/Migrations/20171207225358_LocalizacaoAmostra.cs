using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class LocalizacaoAmostra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Banco",
                table: "Amostra");

            migrationBuilder.DropColumn(
                name: "Cannister",
                table: "Amostra");

            migrationBuilder.DropColumn(
                name: "GlobetCor",
                table: "Amostra");

            migrationBuilder.DropColumn(
                name: "GlobetNumero",
                table: "Amostra");

            migrationBuilder.DropColumn(
                name: "PalhetaCor",
                table: "Amostra");

            migrationBuilder.DropColumn(
                name: "Piso",
                table: "Amostra");

            migrationBuilder.CreateTable(
                name: "LocalizacaoAmostra",
                columns: table => new
                {
                    LocalizacaoAmostraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmostraId = table.Column<int>(type: "int", nullable: true),
                    Banco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cannister = table.Column<int>(type: "int", nullable: false),
                    GlobetCor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GlobetNumero = table.Column<int>(type: "int", nullable: false),
                    PalhetaCor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Piso = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizacaoAmostra", x => x.LocalizacaoAmostraId);
                    table.ForeignKey(
                        name: "FK_LocalizacaoAmostra_Amostra_AmostraId",
                        column: x => x.AmostraId,
                        principalTable: "Amostra",
                        principalColumn: "AmostraId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalizacaoAmostra_AmostraId",
                table: "LocalizacaoAmostra",
                column: "AmostraId",
                unique: true,
                filter: "[AmostraId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalizacaoAmostra");

            migrationBuilder.AddColumn<int>(
                name: "Banco",
                table: "Amostra",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cannister",
                table: "Amostra",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GlobetCor",
                table: "Amostra",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GlobetNumero",
                table: "Amostra",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PalhetaCor",
                table: "Amostra",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Piso",
                table: "Amostra",
                nullable: false,
                defaultValue: 0);
        }
    }
}
