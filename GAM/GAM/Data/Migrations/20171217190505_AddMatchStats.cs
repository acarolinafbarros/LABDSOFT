using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class AddMatchStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchStats",
                columns: table => new
                {
                    MatchStatsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CorCabeloHomem = table.Column<bool>(type: "bit", nullable: false),
                    CorCabeloMulher = table.Column<bool>(type: "bit", nullable: false),
                    CorOlhosHomem = table.Column<bool>(type: "bit", nullable: false),
                    CorOlhosMulher = table.Column<bool>(type: "bit", nullable: false),
                    CorPeleHomem = table.Column<bool>(type: "bit", nullable: false),
                    CorPeleMulher = table.Column<bool>(type: "bit", nullable: false),
                    GrupoSanguineoHomem = table.Column<bool>(type: "bit", nullable: false),
                    GrupoSanguineoMulher = table.Column<bool>(type: "bit", nullable: false),
                    RacaHomem = table.Column<bool>(type: "bit", nullable: false),
                    RacaMulher = table.Column<bool>(type: "bit", nullable: false),
                    TexturaCabeloHomem = table.Column<bool>(type: "bit", nullable: false),
                    TexturaCabeloMulher = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchStats", x => x.MatchStatsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchStats");
        }
    }
}
