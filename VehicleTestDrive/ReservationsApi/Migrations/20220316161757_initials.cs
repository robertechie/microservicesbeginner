using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationsApi.Migrations
{
    public partial class initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isEmailSent",
                table: "Reservations",
                newName: "IsEmailSent");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEmailSent",
                table: "Reservations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsEmailSent",
                table: "Reservations",
                newName: "isEmailSent");

            migrationBuilder.AlterColumn<int>(
                name: "isEmailSent",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
