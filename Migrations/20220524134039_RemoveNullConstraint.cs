using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionEmploye.Migrations
{
    public partial class RemoveNullConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Admin_EmployeId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Employe_EmployeId",
                table: "Person");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeId",
                table: "Person",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "Person",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Admin_EmployeId",
                table: "Person",
                column: "EmployeId",
                principalTable: "Admin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Employe_EmployeId",
                table: "Person",
                column: "EmployeId",
                principalTable: "Employe",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Admin_EmployeId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Employe_EmployeId",
                table: "Person");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeId",
                table: "Person",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "Person",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Admin_EmployeId",
                table: "Person",
                column: "EmployeId",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Employe_EmployeId",
                table: "Person",
                column: "EmployeId",
                principalTable: "Employe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
