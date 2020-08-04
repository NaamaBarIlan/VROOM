using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VROOM.Migrations
{
    public partial class PostMergeSeed5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmployeeEquipmentItem",
                columns: new[] { "EmployeeId", "EquipmentItemId", "DateBorrowed", "DateReturned", "Status" },
                values: new object[] { 1, 6, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 8, 3, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.InsertData(
                table: "EmployeeEquipmentItem",
                columns: new[] { "EmployeeId", "EquipmentItemId", "DateBorrowed", "DateReturned", "Status" },
                values: new object[] { 2, 4, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 8, 3, 0, 0, 0, 0, DateTimeKind.Local), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeEquipmentItem",
                keyColumns: new[] { "EmployeeId", "EquipmentItemId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "EmployeeEquipmentItem",
                keyColumns: new[] { "EmployeeId", "EquipmentItemId" },
                keyValues: new object[] { 2, 4 });
        }
    }
}
