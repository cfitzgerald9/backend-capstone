using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeServed.Data.Migrations
{
    public partial class removedjoinforuserandclient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClients");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Clients",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Clients");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "UserClients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClients", x => x.Id);
                });
        }
    }
}
