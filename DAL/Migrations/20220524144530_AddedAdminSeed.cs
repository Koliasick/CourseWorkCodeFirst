using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddedAdminSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AdditionalPassportData", "Address", "BirthdayDate", "Middlename", "Name", "Photo", "PlaceOfBirth", "RegistrationNumber", "Role", "Surname", "Username" },
                values: new object[] { 1, "No data", "Admin address", new DateTime(2022, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", null, "Uknown", 16868, 1, "Admin", "AdminUser" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "HeadId", "Name" },
                values: new object[] { 1, 858848, 1, "It" });

            migrationBuilder.InsertData(
                table: "WorkTimes",
                columns: new[] { "Id", "Date", "EmployeeId", "WorkDuration" },
                values: new object[] { 1, new DateTime(2022, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2022, 4, 21, 3, 12, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "WorkTimes",
                columns: new[] { "Id", "Date", "EmployeeId", "WorkDuration" },
                values: new object[] { 2, new DateTime(2022, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2022, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "DepartmentId", "Name" },
                values: new object[] { 1, 1, "SysAdmin" });

            migrationBuilder.InsertData(
                table: "EmployeePositions",
                columns: new[] { "Id", "EmployeeId", "PositionId", "ValidTill" },
                values: new object[] { 1, 1, 1, null });

            migrationBuilder.InsertData(
                table: "HourlyRates",
                columns: new[] { "Id", "HourlyPayment", "PositionId", "ValidTill" },
                values: new object[] { 1, 10f, 1, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeePositions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HourlyRates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkTimes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkTimes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
