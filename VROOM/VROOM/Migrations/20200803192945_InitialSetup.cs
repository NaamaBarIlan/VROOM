using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VROOM.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeEquipmentItemEmployeeId",
                table: "EquipmentItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeEquipmentItemEquipmentId",
                table: "EquipmentItem",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBorrowed",
                table: "EmployeeEquipmentItem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReturned",
                table: "EmployeeEquipmentItem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "EmployeeEquipmentItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeEquipmentItemEmployeeId",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeEquipmentItemEquipmentId",
                table: "Employee",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentItem_EmployeeEquipmentItemEmployeeId_EmployeeEquipmentItemEquipmentId",
                table: "EquipmentItem",
                columns: new[] { "EmployeeEquipmentItemEmployeeId", "EmployeeEquipmentItemEquipmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeEquipmentItemEmployeeId_EmployeeEquipmentItemEquipmentId",
                table: "Employee",
                columns: new[] { "EmployeeEquipmentItemEmployeeId", "EmployeeEquipmentItemEquipmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_EmployeeEquipmentItem_EmployeeEquipmentItemEmployeeId_EmployeeEquipmentItemEquipmentId",
                table: "Employee",
                columns: new[] { "EmployeeEquipmentItemEmployeeId", "EmployeeEquipmentItemEquipmentId" },
                principalTable: "EmployeeEquipmentItem",
                principalColumns: new[] { "EmployeeId", "EquipmentId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentItem_EmployeeEquipmentItem_EmployeeEquipmentItemEmployeeId_EmployeeEquipmentItemEquipmentId",
                table: "EquipmentItem",
                columns: new[] { "EmployeeEquipmentItemEmployeeId", "EmployeeEquipmentItemEquipmentId" },
                principalTable: "EmployeeEquipmentItem",
                principalColumns: new[] { "EmployeeId", "EquipmentId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_EmployeeEquipmentItem_EmployeeEquipmentItemEmployeeId_EmployeeEquipmentItemEquipmentId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentItem_EmployeeEquipmentItem_EmployeeEquipmentItemEmployeeId_EmployeeEquipmentItemEquipmentId",
                table: "EquipmentItem");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentItem_EmployeeEquipmentItemEmployeeId_EmployeeEquipmentItemEquipmentId",
                table: "EquipmentItem");

            migrationBuilder.DropIndex(
                name: "IX_Employee_EmployeeEquipmentItemEmployeeId_EmployeeEquipmentItemEquipmentId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "EmployeeEquipmentItemEmployeeId",
                table: "EquipmentItem");

            migrationBuilder.DropColumn(
                name: "EmployeeEquipmentItemEquipmentId",
                table: "EquipmentItem");

            migrationBuilder.DropColumn(
                name: "DateBorrowed",
                table: "EmployeeEquipmentItem");

            migrationBuilder.DropColumn(
                name: "DateReturned",
                table: "EmployeeEquipmentItem");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "EmployeeEquipmentItem");

            migrationBuilder.DropColumn(
                name: "EmployeeEquipmentItemEmployeeId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "EmployeeEquipmentItemEquipmentId",
                table: "Employee");
        }
    }
}
