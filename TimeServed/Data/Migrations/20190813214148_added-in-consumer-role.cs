using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeServed.Data.Migrations
{
    public partial class addedinconsumerrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttorneyId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttorneyId",
                table: "AspNetUsers");
        }
    }
}
