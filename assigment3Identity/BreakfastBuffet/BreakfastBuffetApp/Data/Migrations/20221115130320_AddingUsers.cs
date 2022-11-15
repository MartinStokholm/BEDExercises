using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreakfastBuffetApp.Data.Migrations
{
    public partial class AddingUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KitchenReports",
                columns: table => new
                {
                    Day = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitchenReports", x => new { x.Day, x.Month });
                });

            migrationBuilder.CreateTable(
                name: "CheckedIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Children = table.Column<int>(type: "int", nullable: false),
                    KitchenReportDay = table.Column<int>(type: "int", nullable: false),
                    KitchenReportMonth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckedIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckedIns_KitchenReports_KitchenReportDay_KitchenReportMonth",
                        columns: x => new { x.KitchenReportDay, x.KitchenReportMonth },
                        principalTable: "KitchenReports",
                        principalColumns: new[] { "Day", "Month" },
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_Expected_KitchenReports_Day_Month",
                        columns: x => new { x.Day, x.Month },
                        principalTable: "KitchenReports",
                        principalColumns: new[] { "Day", "Month" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckedIns_KitchenReportDay_KitchenReportMonth",
                table: "CheckedIns",
                columns: new[] { "KitchenReportDay", "KitchenReportMonth" });

            migrationBuilder.CreateIndex(
                name: "IX_Expected_Day_Month",
                table: "Expected",
                columns: new[] { "Day", "Month" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckedIns");

            migrationBuilder.DropTable(
                name: "Expected");

            migrationBuilder.DropTable(
                name: "KitchenReports");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
