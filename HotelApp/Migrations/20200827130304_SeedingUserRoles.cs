using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.API.Migrations
{
    public partial class SeedingUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "d6a6f974-de5b-4a3a-a984-70317a832cb3", "c102cb91-15fb-4e38-878e-5bc7ddb1754c", "SuperAdministrator", null },
                    { "e37c4986-76d1-44f3-b700-cf1640c0ee4a", "dfcbd366-d224-4660-8ee6-3d08c3e46e86", "Administrator", null },
                    { "99cec44b-7a22-4633-978e-e3e2aa91ccb7", "08333a81-4054-4e55-a490-fca5b6f4e5db", "Hotel manager", null },
                    { "a7bcbcfb-d463-4e25-b178-da9c5e61f0b5", "d832dcbe-850e-4539-a5be-5001a8fab98d", "Registered user", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99cec44b-7a22-4633-978e-e3e2aa91ccb7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7bcbcfb-d463-4e25-b178-da9c5e61f0b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6a6f974-de5b-4a3a-a984-70317a832cb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e37c4986-76d1-44f3-b700-cf1640c0ee4a");

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
    }
}
