using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomumBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Phase3E_StaffManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmploymentStatus = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DBSCheckStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DBSCheckExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SafeguardingTrainingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstAidCertExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FireSafetyTrainingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SupervisionFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastSupervisionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmergencyContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformanceRating = table.Column<int>(type: "int", nullable: true),
                    LastAppraisalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfessionalMemberships = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaffAllocations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    YoungPersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AllocationType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    HoursPerWeek = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShiftPattern = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecializationAreas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformanceNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffAllocations_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffAllocations_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffAllocations_YoungPeople_YoungPersonId",
                        column: x => x.YoungPersonId,
                        principalTable: "YoungPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Email",
                table: "Staff",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_EmploymentStatus",
                table: "Staff",
                column: "EmploymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_FacilityId",
                table: "Staff",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_IsActive",
                table: "Staff",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UserId",
                table: "Staff",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAllocations_AllocationType",
                table: "StaffAllocations",
                column: "AllocationType");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAllocations_FacilityId",
                table: "StaffAllocations",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAllocations_IsActive",
                table: "StaffAllocations",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAllocations_StaffId",
                table: "StaffAllocations",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAllocations_YoungPersonId",
                table: "StaffAllocations",
                column: "YoungPersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffAllocations");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
