using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class UpdateValidacaoEspermograma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ValidacaoDiretorLaboratorio",
                table: "Espermograma",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValidacaoEmbriologista",
                table: "Espermograma",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidacaoDiretorLaboratorio",
                table: "Espermograma");

            migrationBuilder.DropColumn(
                name: "ValidacaoEmbriologista",
                table: "Espermograma");
        }
    }
}
