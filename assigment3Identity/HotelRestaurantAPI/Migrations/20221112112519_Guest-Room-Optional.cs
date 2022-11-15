using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelRestaurantAPI.Migrations
{
    public partial class GuestRoomOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Guest_GuestId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_Guest_Rooms_RoomNumber",
                table: "Guest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guest",
                table: "Guest");

            migrationBuilder.RenameTable(
                name: "Guest",
                newName: "Guests");

            migrationBuilder.RenameIndex(
                name: "IX_Guest_RoomNumber",
                table: "Guests",
                newName: "IX_Guests_RoomNumber");

            migrationBuilder.AlterColumn<int>(
                name: "RoomNumber",
                table: "Guests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guests",
                table: "Guests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Guests_GuestId",
                table: "CheckIns",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Rooms_RoomNumber",
                table: "Guests",
                column: "RoomNumber",
                principalTable: "Rooms",
                principalColumn: "RoomNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Guests_GuestId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Rooms_RoomNumber",
                table: "Guests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guests",
                table: "Guests");

            migrationBuilder.RenameTable(
                name: "Guests",
                newName: "Guest");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_RoomNumber",
                table: "Guest",
                newName: "IX_Guest_RoomNumber");

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
                name: "PK_Guest",
                table: "Guest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Guest_GuestId",
                table: "CheckIns",
                column: "GuestId",
                principalTable: "Guest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_Rooms_RoomNumber",
                table: "Guest",
                column: "RoomNumber",
                principalTable: "Rooms",
                principalColumn: "RoomNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
