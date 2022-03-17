using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationsApi.Migrations
{
    public partial class bn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Vehicles_VehicleId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "Reservations",
                newName: "VehicleVId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_VehicleId",
                table: "Reservations",
                newName: "IX_Reservations_VehicleVId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Vehicles_VehicleVId",
                table: "Reservations",
                column: "VehicleVId",
                principalTable: "Vehicles",
                principalColumn: "VId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Vehicles_VehicleVId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "VehicleVId",
                table: "Reservations",
                newName: "VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_VehicleVId",
                table: "Reservations",
                newName: "IX_Reservations_VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Vehicles_VehicleId",
                table: "Reservations",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
