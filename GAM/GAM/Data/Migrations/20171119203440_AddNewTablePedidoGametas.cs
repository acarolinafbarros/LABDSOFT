using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class AddNewTablePedidoGametas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Casal",
                columns: table => new
                {
                    CasalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlturaHomem = table.Column<int>(type: "int", nullable: false),
                    AlturaMulher = table.Column<int>(type: "int", nullable: false),
                    CorCabeloHomem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorCabeloMulher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorOlhosHomem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorOlhosMulher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorPeleHomem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorPeleMulher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GrupoSanguineoHomem = table.Column<int>(type: "int", nullable: false),
                    GrupoSanguineoMulher = table.Column<int>(type: "int", nullable: false),
                    IdadeHomem = table.Column<int>(type: "int", nullable: false),
                    IdadeMulher = table.Column<int>(type: "int", nullable: false),
                    RacaHomem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RacaMulher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TexturaCabeloHomem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TexturaCabeloMulher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casal", x => x.CasalID);
                });

            migrationBuilder.CreateTable(
                name: "PedidoGametas",
                columns: table => new
                {
                    PedidoGametasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CasalId = table.Column<int>(type: "int", nullable: false),
                    Centro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefExterna = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoGametas", x => x.PedidoGametasId);
                    table.ForeignKey(
                        name: "FK_PedidoGametas_Casal_CasalId",
                        column: x => x.CasalId,
                        principalTable: "Casal",
                        principalColumn: "CasalID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoGametas_CasalId",
                table: "PedidoGametas",
                column: "CasalId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoGametas");

            migrationBuilder.DropTable(
                name: "Casal");
        }
    }
}
