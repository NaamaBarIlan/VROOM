using Microsoft.EntityFrameworkCore.Migrations;

namespace VROOM.Migrations
{
    public partial class fixingSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 9);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EquipmentItem",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 8, "Apple MacBook Pro", 1500m });

            migrationBuilder.InsertData(
                table: "EquipmentItem",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 9, "HP Pavilion", 900m });
        }
    }
}
