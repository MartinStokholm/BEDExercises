using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreakfastBuffetAppV2.Data.Migrations
{
    public partial class NewDataModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BreakfastCheckIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Kids = table.Column<int>(type: "int", nullable: false),
                    Roomnumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakfastCheckIns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BreakfastGuestsExpecteds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Kids = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakfastGuestsExpecteds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakfastCheckIns");

            migrationBuilder.DropTable(
                name: "BreakfastGuestsExpecteds");
        }
    }
}
