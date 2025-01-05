using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointments.Migrations
{
    public partial class AddPropertiesToPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AMKA",
                table: "Patients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Patients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AMKA",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Patients");
        }
    }
}
