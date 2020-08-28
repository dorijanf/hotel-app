using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.API.Migrations
{
    public partial class UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "c14acbfd-4297-4929-a633-0edc727b755b", "27983740-a0d1-46a1-b034-69252c0726fc", "SuperAdministrator", null },
                    { "0aa74424-2aff-4073-a7cf-b6d1f8e76997", "36afafd8-0c14-410a-b5c6-09d9d9d1561f", "Administrator", null },
                    { "82c30897-af2c-49f1-a9d6-e40d642192e1", "69932177-9db4-4f91-81a7-55353ba533cc", "Hotel manager", null },
                    { "41c0e01c-cbcb-4c0a-8117-a6e84d9a2e2c", "0b70aed0-b03e-4bc3-8e42-dcf57268c6e8", "Registered user", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "caae4119-e428-4df0-94ed-4b7299f35228", "bdf2a332-5922-4894-bd8d-20a0de2b16be", "SuperAdministrator", null },
                    { "956f33c4-82b0-4f44-a5b1-dab0b74704a3", "014e7073-812b-46eb-9504-1d40cea5354a", "Administrator", null },
                    { "2d492157-1ce7-43e0-a181-142d249107dd", "6fccda2d-7ad4-42b9-a0c9-e56429f4b5c3", "Hotel manager", null },
                    { "971ea664-a5e2-4825-8a44-ff7bcf23c0be", "c78ad85b-cfa1-4d68-85db-2b5b35d199bc", "Registered user", null }
                });
        }
    }
}
