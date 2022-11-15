using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelRestaurantAPI.Data.Migrations
{
    public partial class AllIdentityUsersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KitchenstaffUser_FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KitchenstaffUser_LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceptionUser_FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceptionUser_LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KitchenstaffUser_FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KitchenstaffUser_LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReceptionUser_FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReceptionUser_LastName",
                table: "AspNetUsers");
        }
    }
}
