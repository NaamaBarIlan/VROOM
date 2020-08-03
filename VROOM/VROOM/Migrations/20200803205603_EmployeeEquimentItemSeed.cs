using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VROOM.Migrations
{
    public partial class EmployeeEquimentItemSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EquipmentItem",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "EmployeeEquipmentItemEmployeeId", "EmployeeEquipmentItemEquipmentId", "FirstName" },
                values: new object[] { 2, null, null, "Pam" });

            migrationBuilder.InsertData(
                table: "EmployeeEquipmentItem",
                columns: new[] { "EmployeeId", "EquipmentId", "DateBorrowed", "DateReturned", "Status" },
                values: new object[] { 1, 2, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 8, 3, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.InsertData(
                table: "EquipmentItem",
                columns: new[] { "Id", "EmployeeEquipmentItemEmployeeId", "EmployeeEquipmentItemEquipmentId", "Name" },
                values: new object[,]
                {
                    { 1, null, null, "Copy Machine" },
                    { 2, null, null, "Paper Shredder" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmployeeEquipmentItem",
                keyColumns: new[] { "EmployeeId", "EquipmentId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EquipmentItem");
        }
    }
}
