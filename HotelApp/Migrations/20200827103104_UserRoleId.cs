using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.API.Migrations
{
    public partial class UserRoleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "SuperAdministrator", "2939aa3c-fe71-4589-8cd3-980c0de9c222", "SuperAdministrator", "superadministrator" },
                    { "Administrator", "3d90db8c-f72d-4b71-b040-98f393a2f940", "Administrator", "administrator" },
                    { "Hotel manager", "c701ffb8-5b8e-406f-8fec-01bbc56fd471", "Hotel manager", "hotel manager" },
                    { "Registered user", "a8b57aa8-466e-4886-92c1-650c5883984e", "Registered user", "registered user" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Administrator");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Hotel manager");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Registered user");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "SuperAdministrator");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "edfce7c5-563f-4344-8539-52bc1cc199f9", "SuperAdministrator", "superadministrator" },
                    { "2", "5de4ac57-e8e7-4a34-b619-40653c1454bb", "Administrator", "administrator" },
                    { "3", "5124a184-f099-4682-adff-edbd2e1b43cd", "Hotel manager", "hotel manager" },
                    { "4", "7196d7b0-2d94-4bf1-aa3a-8e4011d45dbb", "Registered user", "registered user" }
                });
        }
    }
}
