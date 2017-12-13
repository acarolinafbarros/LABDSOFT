using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class PedidoGametasAmostra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmostraId",
                table: "PedidoGametas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PedidoGametas_AmostraId",
                table: "PedidoGametas",
                column: "AmostraId",
                unique: true,
                filter: "[AmostraId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoGametas_Amostra_AmostraId",
                table: "PedidoGametas",
                column: "AmostraId",
                principalTable: "Amostra",
                principalColumn: "AmostraId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoGametas_Amostra_AmostraId",
                table: "PedidoGametas");

            migrationBuilder.DropIndex(
                name: "IX_PedidoGametas_AmostraId",
                table: "PedidoGametas");

            migrationBuilder.DropColumn(
                name: "AmostraId",
                table: "PedidoGametas");
        }
    }
}
