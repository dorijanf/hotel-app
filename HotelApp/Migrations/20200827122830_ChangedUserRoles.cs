using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.API.Migrations
{
    public partial class ChangedUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "b8cf6e1e-227e-4141-af89-93e6ee6f2bea", "d8947b61-ea25-43ba-825d-af2df386069d", "SuperAdministrator", null },
                    { "321f4769-181e-4b54-85ae-4bf06e822b7c", "5af3e9ba-0d3f-48ae-83db-d325ae1f3f8d", "Administrator", null },
                    { "cb0033b7-fce6-4f0d-8d5e-b1cf2dabd12c", "ae0c67f5-5004-429c-99ac-4b64b8ec7e83", "Hotel manager", null },
                    { "af438495-26b7-44e2-805b-9c4b1bf59588", "476f5818-3120-4060-98e6-5ca41471a48e", "Registered user", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "321f4769-181e-4b54-85ae-4bf06e822b7c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af438495-26b7-44e2-805b-9c4b1bf59588");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8cf6e1e-227e-4141-af89-93e6ee6f2bea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb0033b7-fce6-4f0d-8d5e-b1cf2dabd12c");

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
    }
}
