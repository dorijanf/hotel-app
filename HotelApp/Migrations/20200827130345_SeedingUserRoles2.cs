using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.API.Migrations
{
    public partial class SeedingUserRoles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "caae4119-e428-4df0-94ed-4b7299f35228", "bdf2a332-5922-4894-bd8d-20a0de2b16be", "SuperAdministrator", null },
                    { "956f33c4-82b0-4f44-a5b1-dab0b74704a3", "014e7073-812b-46eb-9504-1d40cea5354a", "Administrator", null },
                    { "2d492157-1ce7-43e0-a181-142d249107dd", "6fccda2d-7ad4-42b9-a0c9-e56429f4b5c3", "Hotel manager", null },
                    { "971ea664-a5e2-4825-8a44-ff7bcf23c0be", "c78ad85b-cfa1-4d68-85db-2b5b35d199bc", "Registered user", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d492157-1ce7-43e0-a181-142d249107dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "956f33c4-82b0-4f44-a5b1-dab0b74704a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "971ea664-a5e2-4825-8a44-ff7bcf23c0be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "caae4119-e428-4df0-94ed-4b7299f35228");

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
    }
}
