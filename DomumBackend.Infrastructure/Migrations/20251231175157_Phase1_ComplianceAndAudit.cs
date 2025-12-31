using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomumBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Phase1_ComplianceAndAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangedProperty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    YoungPersonId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IncidentType = table.Column<int>(type: "int", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncidentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherPersonsInvolved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffPresent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InjuriesDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatmentProvided = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalAttentionRequired = table.Column<bool>(type: "bit", nullable: false),
                    MedicalTreatmentLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WitnessStatements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RootCause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectiveActions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNotifiable = table.Column<bool>(type: "bit", nullable: false),
                    NotifiedAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosingNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfidential = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidents_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_YoungPeople_YoungPersonId",
                        column: x => x.YoungPersonId,
                        principalTable: "YoungPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SafeguardingConcerns",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    YoungPersonId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ConcernType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcernDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcernAboutPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedToUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DSLNotifiedUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DSLNotifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReportedToExternalAuthority = table.Column<bool>(type: "bit", nullable: false),
                    ExternalAuthorityContacted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    InvestigationNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionsTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Evidence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfidential = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafeguardingConcerns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafeguardingConcerns_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafeguardingConcerns_YoungPeople_YoungPersonId",
                        column: x => x.YoungPersonId,
                        principalTable: "YoungPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SafetyAlerts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    YoungPersonId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlertLevel = table.Column<int>(type: "int", nullable: false),
                    PreventiveActions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseActions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeactivatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeactivationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotifyAllStaff = table.Column<bool>(type: "bit", nullable: false),
                    Urgency = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafetyAlerts_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SafetyAlerts_YoungPeople_YoungPersonId",
                        column: x => x.YoungPersonId,
                        principalTable: "YoungPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IncidentSafeguardingConcern",
                columns: table => new
                {
                    LinkedConcernsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LinkedIncidentsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentSafeguardingConcern", x => new { x.LinkedConcernsId, x.LinkedIncidentsId });
                    table.ForeignKey(
                        name: "FK_IncidentSafeguardingConcern_Incidents_LinkedIncidentsId",
                        column: x => x.LinkedIncidentsId,
                        principalTable: "Incidents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncidentSafeguardingConcern_SafeguardingConcerns_LinkedConcernsId",
                        column: x => x.LinkedConcernsId,
                        principalTable: "SafeguardingConcerns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityId",
                table: "AuditLogs",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Timestamp",
                table: "AuditLogs",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_FacilityId",
                table: "Incidents",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_IncidentDate",
                table: "Incidents",
                column: "IncidentDate");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_Status",
                table: "Incidents",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_YoungPersonId",
                table: "Incidents",
                column: "YoungPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentSafeguardingConcern_LinkedIncidentsId",
                table: "IncidentSafeguardingConcern",
                column: "LinkedIncidentsId");

            migrationBuilder.CreateIndex(
                name: "IX_SafeguardingConcerns_ConcernType",
                table: "SafeguardingConcerns",
                column: "ConcernType");

            migrationBuilder.CreateIndex(
                name: "IX_SafeguardingConcerns_FacilityId",
                table: "SafeguardingConcerns",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SafeguardingConcerns_Status",
                table: "SafeguardingConcerns",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SafeguardingConcerns_YoungPersonId",
                table: "SafeguardingConcerns",
                column: "YoungPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyAlerts_AlertLevel",
                table: "SafetyAlerts",
                column: "AlertLevel");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyAlerts_FacilityId",
                table: "SafetyAlerts",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyAlerts_IsActive",
                table: "SafetyAlerts",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyAlerts_YoungPersonId",
                table: "SafetyAlerts",
                column: "YoungPersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "IncidentSafeguardingConcern");

            migrationBuilder.DropTable(
                name: "SafetyAlerts");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "SafeguardingConcerns");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Facilities");
        }
    }
}
