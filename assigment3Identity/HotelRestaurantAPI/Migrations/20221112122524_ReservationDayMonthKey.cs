using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelRestaurantAPI.Migrations
{
    public partial class ReservationDayMonthKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Reservations_ReservationDate",
                table: "CheckIns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_ReservationDate",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "ReservationDate",
                table: "CheckIns");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservationDay",
                table: "CheckIns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservationMonth",
                table: "CheckIns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                columns: new[] { "Day", "Month" });

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_ReservationDay_ReservationMonth",
                table: "CheckIns",
                columns: new[] { "ReservationDay", "ReservationMonth" });

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Reservations_ReservationDay_ReservationMonth",
                table: "CheckIns",
                columns: new[] { "ReservationDay", "ReservationMonth" },
                principalTable: "Reservations",
                principalColumns: new[] { "Day", "Month" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Reservations_ReservationDay_ReservationMonth",
                table: "CheckIns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_ReservationDay_ReservationMonth",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationDay",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "ReservationMonth",
                table: "CheckIns");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationDate",
                table: "CheckIns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Date");

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
    }
}
