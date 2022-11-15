using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelRestaurantAPI.Migrations
{
    public partial class AddedReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationDate",
                table: "CheckIns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Date);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_ReservationDate",
                table: "CheckIns",
                column: "ReservationDate");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Reservations_ReservationDate",
                table: "CheckIns",
                column: "ReservationDate",
                principalTable: "Reservations",
                principalColumn: "Date",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Reservations_ReservationDate",
                table: "CheckIns");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_ReservationDate",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "ReservationDate",
                table: "CheckIns");
        }
    }
}
