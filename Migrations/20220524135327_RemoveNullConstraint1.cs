using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionEmploye.Migrations
{
    public partial class RemoveNullConstraint1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Admin_EmployeId",
                table: "Person");

            migrationBuilder.CreateIndex(
                name: "IX_Person_AdminId",
                table: "Person",
                column: "AdminId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Admin_AdminId",
                table: "Person",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Admin_AdminId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_AdminId",
                table: "Person");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Admin_EmployeId",
                table: "Person",
                column: "EmployeId",
                principalTable: "Admin",
                principalColumn: "Id");
        }
    }
}
