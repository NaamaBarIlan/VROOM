using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VROOM.Migrations
{
    public partial class PostMergeSeed4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEquipmentItem_EquipmentItem_EquipmentItemId",
                table: "EmployeeEquipmentItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeEquipmentItem",
                table: "EmployeeEquipmentItem");

            migrationBuilder.DeleteData(
                table: "EmployeeEquipmentItem",
                keyColumns: new[] { "EmployeeId", "EquipmentId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "EmployeeEquipmentItem");

            migrationBuilder.AlterColumn<int>(
                name: "EquipmentItemId",
                table: "EmployeeEquipmentItem",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeEquipmentItem",
                table: "EmployeeEquipmentItem",
                columns: new[] { "EmployeeId", "EquipmentItemId" });

            migrationBuilder.InsertData(
                table: "EmployeeEquipmentItem",
                columns: new[] { "EmployeeId", "EquipmentItemId", "DateBorrowed", "DateReturned", "Status" },
                values: new object[] { 1, 1, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 8, 3, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEquipmentItem_EquipmentItem_EquipmentItemId",
                table: "EmployeeEquipmentItem",
                column: "EquipmentItemId",
                principalTable: "EquipmentItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEquipmentItem_EquipmentItem_EquipmentItemId",
                table: "EmployeeEquipmentItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeEquipmentItem",
                table: "EmployeeEquipmentItem");

            migrationBuilder.DeleteData(
                table: "EmployeeEquipmentItem",
                keyColumns: new[] { "EmployeeId", "EquipmentItemId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.AlterColumn<int>(
                name: "EquipmentItemId",
                table: "EmployeeEquipmentItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "EmployeeEquipmentItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeEquipmentItem",
                table: "EmployeeEquipmentItem",
                columns: new[] { "EmployeeId", "EquipmentId" });

            migrationBuilder.InsertData(
                table: "EmployeeEquipmentItem",
                columns: new[] { "EmployeeId", "EquipmentId", "DateBorrowed", "DateReturned", "EquipmentItemId", "Status" },
                values: new object[] { 1, 1, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 8, 3, 0, 0, 0, 0, DateTimeKind.Local), null, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEquipmentItem_EquipmentItem_EquipmentItemId",
                table: "EmployeeEquipmentItem",
                column: "EquipmentItemId",
                principalTable: "EquipmentItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
