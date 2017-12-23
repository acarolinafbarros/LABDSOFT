using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class fixLastMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoGametas_MatchStats_MatchStatsId",
                table: "PedidoGametas");

            migrationBuilder.DropIndex(
                name: "IX_PedidoGametas_MatchStatsId",
                table: "PedidoGametas");

            migrationBuilder.DropIndex(
                name: "IX_MatchStats_CasalId",
                table: "MatchStats");

            migrationBuilder.DropColumn(
                name: "MatchStatsId",
                table: "PedidoGametas");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStats_CasalId",
                table: "MatchStats",
                column: "CasalId",
                unique: true,
                filter: "[CasalId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MatchStats_CasalId",
                table: "MatchStats");

            migrationBuilder.AddColumn<int>(
                name: "MatchStatsId",
                table: "PedidoGametas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PedidoGametas_MatchStatsId",
                table: "PedidoGametas",
                column: "MatchStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStats_CasalId",
                table: "MatchStats",
                column: "CasalId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoGametas_MatchStats_MatchStatsId",
                table: "PedidoGametas",
                column: "MatchStatsId",
                principalTable: "MatchStats",
                principalColumn: "MatchStatsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
