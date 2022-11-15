using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelRestaurantAPI.Migrations
{
    public partial class SimplifyDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Guests_GuestId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Reservations_ReservationDay_ReservationMonth",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Rooms_RoomNumber",
                table: "CheckIns");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckIns",
                table: "CheckIns");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_GuestId",
                table: "CheckIns");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_RoomNumber",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "AdultAmount",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ChildrenAmount",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "CheckIns");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "DailyBreakfasts");

            migrationBuilder.RenameTable(
                name: "CheckIns",
                newName: "CheckedIn");

            migrationBuilder.RenameColumn(
                name: "ReservationMonth",
                table: "CheckedIn",
                newName: "DailyBreakfastMonth");

            migrationBuilder.RenameColumn(
                name: "ReservationDay",
                table: "CheckedIn",
                newName: "DailyBreakfastDay");

            migrationBuilder.RenameColumn(
                name: "GuestId",
                table: "CheckedIn",
                newName: "Children");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CheckedIn",
                newName: "Adults");

            migrationBuilder.RenameIndex(
                name: "IX_CheckIns_ReservationDay_ReservationMonth",
                table: "CheckedIn",
                newName: "IX_CheckedIn_DailyBreakfastDay_DailyBreakfastMonth");

            migrationBuilder.DropColumn("RoomNumber", "CheckedIn");
            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "CheckedIn",
                type: "int",
                nullable: false);

            migrationBuilder.DropColumn("Adults", "CheckedIn");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "CheckedIn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyBreakfasts",
                table: "DailyBreakfasts",
                columns: new[] { "Day", "Month" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckedIn",
                table: "CheckedIn",
                column: "RoomNumber");

            migrationBuilder.CreateTable(
                name: "Expected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Children = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expected_DailyBreakfasts_Day_Month",
                        columns: x => new { x.Day, x.Month },
                        principalTable: "DailyBreakfasts",
                        principalColumns: new[] { "Day", "Month" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expected_Day_Month",
                table: "Expected",
                columns: new[] { "Day", "Month" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckedIn_DailyBreakfasts_DailyBreakfastDay_DailyBreakfastMonth",
                table: "CheckedIn",
                columns: new[] { "DailyBreakfastDay", "DailyBreakfastMonth" },
                principalTable: "DailyBreakfasts",
                principalColumns: new[] { "Day", "Month" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckedIn_DailyBreakfasts_DailyBreakfastDay_DailyBreakfastMonth",
                table: "CheckedIn");

            migrationBuilder.DropTable(
                name: "Expected");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyBreakfasts",
                table: "DailyBreakfasts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckedIn",
                table: "CheckedIn");

            migrationBuilder.RenameTable(
                name: "DailyBreakfasts",
                newName: "Reservations");

            migrationBuilder.RenameTable(
                name: "CheckedIn",
                newName: "CheckIns");

            migrationBuilder.RenameColumn(
                name: "DailyBreakfastMonth",
                table: "CheckIns",
                newName: "ReservationMonth");

            migrationBuilder.RenameColumn(
                name: "DailyBreakfastDay",
                table: "CheckIns",
                newName: "ReservationDay");

            migrationBuilder.RenameColumn(
                name: "Children",
                table: "CheckIns",
                newName: "GuestId");

            migrationBuilder.RenameColumn(
                name: "Adults",
                table: "CheckIns",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CheckedIn_DailyBreakfastDay_DailyBreakfastMonth",
                table: "CheckIns",
                newName: "IX_CheckIns_ReservationDay_ReservationMonth");

            migrationBuilder.AddColumn<int>(
                name: "AdultAmount",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildrenAmount",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "RoomNumber",
                table: "CheckIns",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CheckIns",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "CheckIns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                columns: new[] { "Day", "Month" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckIns",
                table: "CheckIns",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomNumber);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guests_Rooms_RoomNumber",
                        column: x => x.RoomNumber,
                        principalTable: "Rooms",
                        principalColumn: "RoomNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_GuestId",
                table: "CheckIns",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_RoomNumber",
                table: "CheckIns",
                column: "RoomNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_RoomNumber",
                table: "Guests",
                column: "RoomNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Guests_GuestId",
                table: "CheckIns",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Reservations_ReservationDay_ReservationMonth",
                table: "CheckIns",
                columns: new[] { "ReservationDay", "ReservationMonth" },
                principalTable: "Reservations",
                principalColumns: new[] { "Day", "Month" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Rooms_RoomNumber",
                table: "CheckIns",
                column: "RoomNumber",
                principalTable: "Rooms",
                principalColumn: "RoomNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
