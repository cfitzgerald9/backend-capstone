using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeServed.Data.Migrations
{
    public partial class addedseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LocationName",
                table: "Locations",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "ApplicationUserId", "FirstName", "LastName", "LocationId" },
                values: new object[,]
                {
                    { 1, null, "Jakob", "Wildman", 3 },
                    { 2, null, "Susan", "MacAfee", 1 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "LocationName", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "South Central", "1001 Centre Way, Charleston, WV 25309" },
                    { 2, "West Virginia Regional Jail and Correctional Facility", "1325 Virginia St E, Charleston, WV 25301" },
                    { 3, "Mt Olive Correctional Complex", "1 Mountainside Way, Mt Olive, WV 25185" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { 1, "Attorney" },
                    { 2, "Guard" },
                    { 3, "Auditor" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "LocationName",
                table: "Locations",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
