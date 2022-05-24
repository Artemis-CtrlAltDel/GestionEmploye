using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionEmploye.Migrations
{
    public partial class AddAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Person",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Admin_EmployeId",
                table: "Person",
                column: "EmployeId",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Admin_EmployeId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Person");
        }
    }
}
