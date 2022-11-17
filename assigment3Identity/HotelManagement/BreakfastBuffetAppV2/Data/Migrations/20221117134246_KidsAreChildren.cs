using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreakfastBuffetAppV2.Data.Migrations
{
    public partial class KidsAreChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kids",
                table: "BreakfastGuestsExpecteds",
                newName: "Children");

            migrationBuilder.RenameColumn(
                name: "Kids",
                table: "BreakfastCheckIns",
                newName: "Children");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Children",
                table: "BreakfastGuestsExpecteds",
                newName: "Kids");

            migrationBuilder.RenameColumn(
                name: "Children",
                table: "BreakfastCheckIns",
                newName: "Kids");
        }
    }
}
