using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class UpdateModelAmostra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Localizacao",
                table: "Amostra");

            migrationBuilder.AddColumn<int>(
                name: "Banco",
                table: "Amostra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cannister",
                table: "Amostra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GlobetCor",
                table: "Amostra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GlobetNumero",
                table: "Amostra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PalhetaCor",
                table: "Amostra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Piso",
                table: "Amostra",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Localizacao",
                table: "Amostra",
                maxLength: 50,
                nullable: true);
        }
    }
}
