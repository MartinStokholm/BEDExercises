using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelRestaurantAPI.Migrations
{
    public partial class CheckinNormalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckedIn",
                table: "CheckedIn");
            
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CheckedIn",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckedIn",
                table: "CheckedIn",
                column: "Id");

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "CheckedIn",
                type: "int",
                nullable: false,
                defaultValue: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckedIn",
                table: "CheckedIn");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CheckedIn");

            migrationBuilder.AlterColumn<int>(
                name: "RoomNumber",
                table: "CheckedIn",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckedIn",
                table: "CheckedIn",
                column: "RoomNumber");
        }
    }
}
