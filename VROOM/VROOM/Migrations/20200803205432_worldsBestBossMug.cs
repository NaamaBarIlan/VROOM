using Microsoft.EntityFrameworkCore.Migrations;

namespace VROOM.Migrations
{
    public partial class worldsBestBossMug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Value" },
                values: new object[] { "World's Best Boss Mug", 20m });

            migrationBuilder.UpdateData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Paper Shredder", 100m });

            migrationBuilder.UpdateData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Fax Machine", 200m });

            migrationBuilder.UpdateData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Lenovo ThinkPad", 700m });

            migrationBuilder.UpdateData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Apple MacBook Pro", 1500m });

            migrationBuilder.InsertData(
                table: "EquipmentItem",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 7, "HP Pavilion", 900m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Paper Shredder", 100m });

            migrationBuilder.UpdateData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Fax Machine", 200m });

            migrationBuilder.UpdateData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Lenovo ThinkPad", 700m });

            migrationBuilder.UpdateData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "Value" },
                values: new object[] { "Apple MacBook Pro", 1500m });

            migrationBuilder.UpdateData(
                table: "EquipmentItem",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Name", "Value" },
                values: new object[] { "HP Pavilion", 900m });
        }
    }
}
