using Microsoft.EntityFrameworkCore.Migrations;

namespace VROOM.Migrations
{
    public partial class EEItemRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "EmployeeEquipmentItem");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "EmployeeEquipmentItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeEquipmentItem",
                keyColumns: new[] { "EmployeeId", "EquipmentItemId" },
                keyValues: new object[] { 1, 1 },
                column: "StatusId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeEquipmentItem",
                keyColumns: new[] { "EmployeeId", "EquipmentItemId" },
                keyValues: new object[] { 1, 6 },
                column: "StatusId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeEquipmentItem",
                keyColumns: new[] { "EmployeeId", "EquipmentItemId" },
                keyValues: new object[] { 2, 4 },
                column: "StatusId",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "EmployeeEquipmentItem");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "EmployeeEquipmentItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeEquipmentItem",
                keyColumns: new[] { "EmployeeId", "EquipmentItemId" },
                keyValues: new object[] { 1, 1 },
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeEquipmentItem",
                keyColumns: new[] { "EmployeeId", "EquipmentItemId" },
                keyValues: new object[] { 1, 6 },
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeEquipmentItem",
                keyColumns: new[] { "EmployeeId", "EquipmentItemId" },
                keyValues: new object[] { 2, 4 },
                column: "Status",
                value: 1);
        }
    }
}
