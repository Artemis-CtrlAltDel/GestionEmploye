using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionEmploye.Migrations
{
    public partial class AddAbsence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Admin_AdminId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Employe_EmployeId",
                table: "Person");

            migrationBuilder.CreateTable(
                name: "Absence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmployeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Absence_Employe_EmployeId",
                        column: x => x.EmployeId,
                        principalTable: "Employe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Absence_EmployeId",
                table: "Absence",
                column: "EmployeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Admin_AdminId",
                table: "Person",
                column: "AdminId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Admin_AdminId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Employe_EmployeId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Absence");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Admin_AdminId",
                table: "Person",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Employe_EmployeId",
                table: "Person",
                column: "EmployeId",
                principalTable: "Employe",
                principalColumn: "Id");
        }
    }
}
