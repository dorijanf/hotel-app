using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.API.Migrations
{
    public partial class ConfiguringDatabaseConstrainsFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "ReservationStatuses",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "HotelStatuses",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9ee6cb7a-8ecb-4a01-aca8-03a2d8bb6462");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2ba0042e-d487-41ea-97a7-0d42832ac110");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "49e85644-a4e7-4ccc-9fc9-fc874bc8047c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "6abc0bce-e0cc-4ce9-9091-e370c2f90792");

            migrationBuilder.UpdateData(
                table: "HotelStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: 1);

            migrationBuilder.UpdateData(
                table: "HotelStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: 4);

            migrationBuilder.UpdateData(
                table: "HotelStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: 2);

            migrationBuilder.UpdateData(
                table: "HotelStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ReservationStatuses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HotelStatuses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "ea148973-ea16-407e-99a3-02a0d7b29eb9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "fd8a643c-2b25-4213-b66e-e4d8e828b35e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "45bababb-67af-482b-9144-8dfc3f3be65e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "cf58b8d7-c687-469b-a9d0-b0959f787d60");

            migrationBuilder.UpdateData(
                table: "HotelStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "HotelStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Innactive");

            migrationBuilder.UpdateData(
                table: "HotelStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "HotelStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Denied");

            migrationBuilder.UpdateData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Processing");

            migrationBuilder.UpdateData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Accepted");

            migrationBuilder.UpdateData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Denied");

            migrationBuilder.UpdateData(
                table: "ReservationStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Cancelled");
        }
    }
}
