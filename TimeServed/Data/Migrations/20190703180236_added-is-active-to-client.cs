using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeServed.Data.Migrations
{
    public partial class addedisactivetoclient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Clients",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Clients",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Clients",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Clients",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Clients",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
