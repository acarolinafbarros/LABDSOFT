using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class addMatchstatsOnCasal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchStatsId",
                table: "PedidoGametas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PedidoGametas_MatchStatsId",
                table: "PedidoGametas",
                column: "MatchStatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoGametas_MatchStats_MatchStatsId",
                table: "PedidoGametas",
                column: "MatchStatsId",
                principalTable: "MatchStats",
                principalColumn: "MatchStatsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoGametas_MatchStats_MatchStatsId",
                table: "PedidoGametas");

            migrationBuilder.DropIndex(
                name: "IX_PedidoGametas_MatchStatsId",
                table: "PedidoGametas");

            migrationBuilder.DropColumn(
                name: "MatchStatsId",
                table: "PedidoGametas");
        }
    }
}
