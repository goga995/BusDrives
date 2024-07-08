using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusDrives.DataService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusDrives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DriverCompany = table.Column<string>(type: "TEXT", nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartingCityId = table.Column<int>(type: "INTEGER", nullable: false),
                    FinalCityId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsRecurring = table.Column<bool>(type: "INTEGER", nullable: false),
                    RecurrenceInterval = table.Column<string>(type: "TEXT", nullable: false),
                    RecurrenceEndDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusDrives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusDrives_Cities_FinalCityId",
                        column: x => x.FinalCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusDrives_Cities_StartingCityId",
                        column: x => x.StartingCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusDrives_FinalCityId",
                table: "BusDrives",
                column: "FinalCityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusDrives_StartingCityId",
                table: "BusDrives",
                column: "StartingCityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusDrives");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
