using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomumBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class creatingnewcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfEntryToFacility",
                table: "YoungPeople",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationDate",
                table: "YoungPeople",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationFacilityEmail",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationFacilityName",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationFacilityNumber",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ethnicity",
                table: "YoungPeople",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EyeColour",
                table: "YoungPeople",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "YoungPeople",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HairColour",
                table: "YoungPeople",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthIssues",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "YoungPeople",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IROContactNo",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IROEmail",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IROName",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "YoungPeople",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "YoungPeople",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Religion",
                table: "YoungPeople",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "DateOfEntryToFacility",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "DeactivationDate",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "EducationFacilityEmail",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "EducationFacilityName",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "EducationFacilityNumber",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "Ethnicity",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "EyeColour",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "HairColour",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "HealthIssues",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "IROContactNo",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "IROEmail",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "IROName",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "YoungPeople");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "YoungPeople");
        }
    }
}

