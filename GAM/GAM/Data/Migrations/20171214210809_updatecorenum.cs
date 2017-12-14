using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GAM.Data.Migrations
{
    public partial class updatecorenum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CorOlhos",
                table: "Dador",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CorOlhos",
                table: "Dador",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
