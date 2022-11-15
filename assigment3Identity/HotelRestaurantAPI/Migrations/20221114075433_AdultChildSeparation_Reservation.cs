using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelRestaurantAPI.Migrations
{
    public partial class AdultChildSeparation_Reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GAmount",
                table: "Reservations",
                newName: "ChildrenAmount");

            migrationBuilder.AddColumn<int>(
                name: "AdultAmount",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultAmount",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ChildrenAmount",
                table: "Reservations",
                newName: "GAmount");
        }
    }
}
