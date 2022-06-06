using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionEmploye.Migrations
{
    public partial class UpdateCongeQuota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CongeRemaining",
                table: "Employe",
                type: "INTEGER",
                nullable: false,
                defaultValue: 30,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CongeRemaining",
                table: "Employe",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 30);
        }
    }
}
