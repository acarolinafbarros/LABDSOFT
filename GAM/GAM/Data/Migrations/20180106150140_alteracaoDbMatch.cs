using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class alteracaoDbMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoId",
                table: "Dador",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoRegisto",
                table: "Dador",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FotoHomemId",
                table: "Casal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoMulherId",
                table: "Casal",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoId",
                table: "Dador");

            migrationBuilder.DropColumn(
                name: "TipoRegisto",
                table: "Dador");

            migrationBuilder.DropColumn(
                name: "FotoHomemId",
                table: "Casal");

            migrationBuilder.DropColumn(
                name: "FotoMulherId",
                table: "Casal");
        }
    }
}
