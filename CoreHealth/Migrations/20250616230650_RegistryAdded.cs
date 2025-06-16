using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreHealth.Migrations
{
    /// <inheritdoc />
    public partial class RegistryAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlImage",
                table: "Medication",
                newName: "urlImage");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Service",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "HighSystem",
                table: "Service",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Service",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Prescription",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "HighSystem",
                table: "Prescription",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Prescription",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Patient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "HighSystem",
                table: "Patient",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Patient",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Medication",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "HighSystem",
                table: "Medication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Medication",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Doctor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "HighSystem",
                table: "Doctor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Doctor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ClinicHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "HighSystem",
                table: "ClinicHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ClinicHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Clinic",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "HighSystem",
                table: "Clinic",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Clinic",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Appointment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "HighSystem",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Appointment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "HighSystem",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "HighSystem",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "HighSystem",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "HighSystem",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "HighSystem",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "ClinicHistory");

            migrationBuilder.DropColumn(
                name: "HighSystem",
                table: "ClinicHistory");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ClinicHistory");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "HighSystem",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "HighSystem",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "urlImage",
                table: "Medication",
                newName: "UrlImage");
        }
    }
}
