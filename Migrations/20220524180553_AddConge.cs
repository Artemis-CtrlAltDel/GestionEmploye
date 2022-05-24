using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionEmploye.Migrations
{
    public partial class AddConge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CongeRemaining",
                table: "Employe",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Conge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DemandeTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    EmployeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conge_Employe_EmployeId",
                        column: x => x.EmployeId,
                        principalTable: "Employe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conge_EmployeId",
                table: "Conge",
                column: "EmployeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conge");

            migrationBuilder.DropColumn(
                name: "CongeRemaining",
                table: "Employe");
        }
    }
}
