using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class adicionadoDadosMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CasalId",
                table: "MatchStats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DadorId",
                table: "MatchStats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchStats_CasalId",
                table: "MatchStats",
                column: "CasalId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStats_DadorId",
                table: "MatchStats",
                column: "DadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchStats_Casal_CasalId",
                table: "MatchStats",
                column: "CasalId",
                principalTable: "Casal",
                principalColumn: "CasalID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchStats_Dador_DadorId",
                table: "MatchStats",
                column: "DadorId",
                principalTable: "Dador",
                principalColumn: "DadorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchStats_Casal_CasalId",
                table: "MatchStats");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchStats_Dador_DadorId",
                table: "MatchStats");

            migrationBuilder.DropIndex(
                name: "IX_MatchStats_CasalId",
                table: "MatchStats");

            migrationBuilder.DropIndex(
                name: "IX_MatchStats_DadorId",
                table: "MatchStats");

            migrationBuilder.DropColumn(
                name: "CasalId",
                table: "MatchStats");

            migrationBuilder.DropColumn(
                name: "DadorId",
                table: "MatchStats");
        }
    }
}
