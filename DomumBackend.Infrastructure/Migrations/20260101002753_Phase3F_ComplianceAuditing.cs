using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomumBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Phase3F_ComplianceAuditing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplianceAudits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedToStaffId = table.Column<long>(type: "bigint", nullable: true),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastCompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ComplianceScore = table.Column<int>(type: "int", nullable: false),
                    TotalChecklistItems = table.Column<int>(type: "int", nullable: false),
                    CompletedChecklistItems = table.Column<int>(type: "int", nullable: false),
                    RegulatoryBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Findings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recommendations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiresFollowUp = table.Column<bool>(type: "bit", nullable: false),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FollowUpStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FollowUpFindings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ActualCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DocumentReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRemote = table.Column<bool>(type: "bit", nullable: false),
                    AuditorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditorOrganization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditorContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotesForFollowUp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceAudits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplianceChecklistItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplianceAuditId = table.Column<long>(type: "bigint", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemNumber = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompliant = table.Column<bool>(type: "bit", nullable: false),
                    EvidenceProvided = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedByStaffId = table.Column<long>(type: "bigint", nullable: true),
                    ReviewerNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Requirement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guidance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiresEvidenceDocument = table.Column<bool>(type: "bit", nullable: false),
                    DocumentReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NonComplianceReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectionAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectionDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CorrectionCompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RiskLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceChecklistItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplianceDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplianceAuditId = table.Column<long>(type: "bigint", nullable: false),
                    NonConformityId = table.Column<long>(type: "bigint", nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedByStaffId = table.Column<long>(type: "bigint", nullable: true),
                    ReviewStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewFeedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCurrentVersion = table.Column<bool>(type: "bit", nullable: false),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    DocumentReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccessLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplianceNonConformities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplianceAuditId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RootCause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectionAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponsibleStaffId = table.Column<long>(type: "bigint", nullable: true),
                    IdentifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evidence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    RequiresFollowUp = table.Column<bool>(type: "bit", nullable: false),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FollowUpFindings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegulatoryReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostToResolve = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResolutionNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedByStaffId = table.Column<long>(type: "bigint", nullable: true),
                    VerificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceNonConformities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplianceAudits");

            migrationBuilder.DropTable(
                name: "ComplianceChecklistItems");

            migrationBuilder.DropTable(
                name: "ComplianceDocuments");

            migrationBuilder.DropTable(
                name: "ComplianceNonConformities");
        }
    }
}
