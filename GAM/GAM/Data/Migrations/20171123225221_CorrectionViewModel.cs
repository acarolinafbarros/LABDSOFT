using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class CorrectionViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoGametasViewModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidoGametasViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlturaHomem = table.Column<int>(nullable: false),
                    AlturaMulher = table.Column<int>(nullable: false),
                    Centro = table.Column<string>(nullable: true),
                    CorCabeloHomem = table.Column<string>(maxLength: 20, nullable: true),
                    CorCabeloMulher = table.Column<string>(maxLength: 20, nullable: true),
                    CorOlhosHomem = table.Column<string>(maxLength: 20, nullable: true),
                    CorOlhosMulher = table.Column<string>(maxLength: 20, nullable: true),
                    CorPeleHomem = table.Column<string>(maxLength: 20, nullable: true),
                    CorPeleMulher = table.Column<string>(maxLength: 20, nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    GrupoSanguineoHomem = table.Column<int>(nullable: false),
                    GrupoSanguineoMulher = table.Column<int>(nullable: false),
                    IdadeHomem = table.Column<int>(nullable: false),
                    IdadeMulher = table.Column<int>(nullable: false),
                    RacaHomem = table.Column<string>(maxLength: 20, nullable: true),
                    RacaMulher = table.Column<string>(maxLength: 20, nullable: true),
                    RefExterna = table.Column<string>(nullable: true),
                    TexturaCabeloHomem = table.Column<string>(maxLength: 20, nullable: true),
                    TexturaCabeloMulher = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoGametasViewModel", x => x.Id);
                });
        }
    }
}
