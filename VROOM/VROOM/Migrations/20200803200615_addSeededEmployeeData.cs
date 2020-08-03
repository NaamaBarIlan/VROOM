using Microsoft.EntityFrameworkCore.Migrations;

namespace VROOM.Migrations
{
    public partial class addSeededEmployeeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchAddress",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchPhone",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dept",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Employee",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BranchAddress", "BranchName", "BranchPhone", "Dept", "Email", "LastName", "Phone", "Title" },
                values: new object[] { "1725 Slough Avenue, Scranton, PA", "Scranton Branch", "(570) 348-4100", "Management", "mscott@vroom.com", "Scott", "(570)-348-4178", "Regional Manager" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "BranchAddress", "BranchName", "BranchPhone", "Dept", "Email", "FirstName", "LastName", "Phone", "Title" },
                values: new object[] { 3, "1725 Slough Avenue, Scranton, PA", "Scranton Branch", "(570) 348-4100", "Sales", "jhalpert@vroom.com", "James", "Halpert", "(570) 348-4186", "Sales Representative" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "BranchAddress", "BranchName", "BranchPhone", "Dept", "Email", "FirstName", "LastName", "Phone", "Title" },
                values: new object[] { 2, "1725 Slough Avenue, Scranton, PA", "Scranton Branch", "(570) 348-4100", "Administration", "pbeesly@vroom.com", "Pamela", "Beesly", "(570) 348-4118", "Office Manager" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEquipmentItem_Employee_EmployeeId",
                table: "EmployeeEquipmentItem",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEquipmentItem_Employee_EmployeeId",
                table: "EmployeeEquipmentItem");

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "BranchAddress",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "BranchPhone",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Dept",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Employee");
        }
    }
}
