using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeServed.Data.Migrations
{
    public partial class finaladjustmentstoappointments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteLocations");

            migrationBuilder.RenameColumn(
                name: "VisitDate",
                table: "Appointments",
                newName: "VisitDateStart");

            migrationBuilder.AddColumn<string>(
                name: "CaseNumber",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VisitDateEnd",
                table: "Appointments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseNumber",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "VisitDateEnd",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "VisitDateStart",
                table: "Appointments",
                newName: "VisitDate");

            migrationBuilder.CreateTable(
                name: "FavoriteLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteLocations", x => x.Id);
                });
        }
    }
}
