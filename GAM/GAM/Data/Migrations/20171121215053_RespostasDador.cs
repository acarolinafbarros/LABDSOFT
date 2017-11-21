using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class RespostasDador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resposta_DadorId",
                table: "Resposta");

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_DadorId",
                table: "Resposta",
                column: "DadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resposta_DadorId",
                table: "Resposta");

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_DadorId",
                table: "Resposta",
                column: "DadorId",
                unique: true);
        }
    }
}
