using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelRestaurantAPI.Migrations
{
    public partial class Rooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Room_RoomNumber",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_Guest_Room_RoomNumber",
                table: "Guest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "RoomNumber",
                table: "Guest",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "RoomNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Rooms_RoomNumber",
                table: "CheckIns",
                column: "RoomNumber",
                principalTable: "Rooms",
                principalColumn: "RoomNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_Rooms_RoomNumber",
                table: "Guest",
                column: "RoomNumber",
                principalTable: "Rooms",
                principalColumn: "RoomNumber",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Rooms_RoomNumber",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_Guest_Rooms_RoomNumber",
                table: "Guest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "RoomNumber",
                table: "Guest",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "RoomNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Room_RoomNumber",
                table: "CheckIns",
                column: "RoomNumber",
                principalTable: "Room",
                principalColumn: "RoomNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_Room_RoomNumber",
                table: "Guest",
                column: "RoomNumber",
                principalTable: "Room",
                principalColumn: "RoomNumber");
        }
    }
}
