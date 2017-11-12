using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class UpdateTableAnalise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Analise_AmostraId",
                table: "Analise");

            migrationBuilder.CreateIndex(
                name: "IX_Analise_AmostraId",
                table: "Analise",
                column: "AmostraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Analise_AmostraId",
                table: "Analise");

            migrationBuilder.CreateIndex(
                name: "IX_Analise_AmostraId",
                table: "Analise",
                column: "AmostraId",
                unique: true);
        }
    }
}
