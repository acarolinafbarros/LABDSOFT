using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class AddedPedidoGametasViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidoGametasViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlturaHomem = table.Column<int>(type: "int", nullable: false),
                    AlturaMulher = table.Column<int>(type: "int", nullable: false),
                    Centro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorCabeloHomem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorCabeloMulher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorOlhosHomem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorOlhosMulher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorPeleHomem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CorPeleMulher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrupoSanguineoHomem = table.Column<int>(type: "int", nullable: false),
                    GrupoSanguineoMulher = table.Column<int>(type: "int", nullable: false),
                    IdadeHomem = table.Column<int>(type: "int", nullable: false),
                    IdadeMulher = table.Column<int>(type: "int", nullable: false),
                    RacaHomem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RacaMulher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RefExterna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TexturaCabeloHomem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TexturaCabeloMulher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoGametasViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoGametasViewModel");
        }
    }
}
