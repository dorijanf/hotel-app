using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.API.Migrations
{
    public partial class CityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Hotels");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Hotels",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8f2ff38f-8740-4cd5-9ac4-dea6b8f9277d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "a0e99b98-9e8e-4a31-85ea-0822976b37b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "06b9196c-c84b-49fd-a4c8-8a4277f563ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "c1f8d155-3cb9-4728-a617-894fb7b93ebc");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 13, false, "Barcelona" },
                    { 12, false, "Rome" },
                    { 11, false, "Madrid" },
                    { 10, false, "Shanghai" },
                    { 9, false, "Cape Town" },
                    { 7, false, "Tokyo" },
                    { 6, false, "Rijeka" },
                    { 5, false, "Budapest" },
                    { 4, false, "Zagreb" },
                    { 3, false, "London" },
                    { 2, false, "Paris" },
                    { 8, false, "Rio de Janeiro" },
                    { 1, false, "New York" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CityId",
                table: "Hotels",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Cities_CityId",
                table: "Hotels",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Cities_CityId",
                table: "Hotels");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_CityId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Hotels");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Hotels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "05d0486b-0a48-46ea-a48d-2133ea3f0343");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c8b573f6-418b-4f57-9d2e-77dafc2530df");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "a9ac1be0-059e-4d1e-85fc-7e7e69644d41");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "5dc1b153-e7ec-4012-8d55-2a008a68ca78");
        }
    }
}
