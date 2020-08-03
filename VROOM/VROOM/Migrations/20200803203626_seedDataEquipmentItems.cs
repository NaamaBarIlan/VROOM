using Microsoft.EntityFrameworkCore.Migrations;

namespace VROOM.Migrations
{
    public partial class seedDataEquipmentItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EquipmentItem",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "EquipmentItem",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentItemId",
                table: "EmployeeEquipmentItem",
                nullable: true);

            migrationBuilder.InsertData(
                table: "EquipmentItem",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "Copy Machine", 8000m },
                    { 2, "Paper Shredder", 100m },
                    { 3, "Fax Machine", 200m },
                    { 4, "Lenovo ThinkPad", 700m },
                    { 5, "Apple MacBook Pro", 1500m },
                    { 6, "HP Pavilion", 900m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEquipmentItem_EquipmentItemId",
                table: "EmployeeEquipmentItem",
                column: "EquipmentItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEquipmentItem_EquipmentItem_EquipmentItemId",
                table: "EmployeeEquipmentItem",
                column: "EquipmentItemId",
                principalTable: "EquipmentItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEquipmentItem_EquipmentItem_EquipmentItemId",
                table: "EmployeeEquipmentItem");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEquipmentItem_EquipmentItemId",
                table: "EmployeeEquipmentItem");

            migrationBuilder.DeleteData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EquipmentItem");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "EquipmentItem");

            migrationBuilder.DropColumn(
                name: "EquipmentItemId",
                table: "EmployeeEquipmentItem");
        }
    }
}
