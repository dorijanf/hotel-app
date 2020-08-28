using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.API.Migrations
{
    public partial class userroletypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0aa74424-2aff-4073-a7cf-b6d1f8e76997");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41c0e01c-cbcb-4c0a-8117-a6e84d9a2e2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82c30897-af2c-49f1-a9d6-e40d642192e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c14acbfd-4297-4929-a633-0edc727b755b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2", "b9c625f8-f205-42cf-ae28-f7d78564eb75", "SuperAdministrator", null },
                    { "1", "a647db07-bf45-4880-8d59-c428728bc28b", "Administrator", null },
                    { "4", "1df6f720-4f32-4c6f-841f-5e20366e4119", "Hotel manager", null },
                    { "3", "ce5ef40d-a3f8-4e86-a13c-ec6f0ba99c1f", "Registered user", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "c14acbfd-4297-4929-a633-0edc727b755b", "27983740-a0d1-46a1-b034-69252c0726fc", "SuperAdministrator", null },
                    { "0aa74424-2aff-4073-a7cf-b6d1f8e76997", "36afafd8-0c14-410a-b5c6-09d9d9d1561f", "Administrator", null },
                    { "82c30897-af2c-49f1-a9d6-e40d642192e1", "69932177-9db4-4f91-81a7-55353ba533cc", "Hotel manager", null },
                    { "41c0e01c-cbcb-4c0a-8117-a6e84d9a2e2c", "0b70aed0-b03e-4bc3-8e42-dcf57268c6e8", "Registered user", null }
                });
        }
    }
}
