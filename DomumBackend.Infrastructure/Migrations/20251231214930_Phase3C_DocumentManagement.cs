using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomumBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Phase3C_DocumentManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCategoryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultClassificationLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultAccessLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiresApproval = table.Column<bool>(type: "bit", nullable: false),
                    DefaultRetentionDays = table.Column<int>(type: "int", nullable: false),
                    RetentionReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplianceStandard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowVersioning = table.Column<bool>(type: "bit", nullable: false),
                    AllowMultipleVersions = table.Column<bool>(type: "bit", nullable: false),
                    RequireSignature = table.Column<bool>(type: "bit", nullable: false),
                    AllowSharing = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AllowPublicAccess = table.Column<bool>(type: "bit", nullable: false),
                    DocumentCount = table.Column<int>(type: "int", nullable: false),
                    LastUsedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AllowedFileTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxFileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentCategories_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentRetentions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumentCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassificationLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplyToAllDocuments = table.Column<bool>(type: "bit", nullable: false),
                    RetentionDays = table.Column<int>(type: "int", nullable: false),
                    MinimumRetentionDays = table.Column<int>(type: "int", nullable: false),
                    MaximumRetentionDays = table.Column<int>(type: "int", nullable: false),
                    RetentionUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RetentionTrigger = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnableArchival = table.Column<bool>(type: "bit", nullable: false),
                    DaysBeforeArchival = table.Column<int>(type: "int", nullable: false),
                    ArchivalLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompressBeforeArchival = table.Column<bool>(type: "bit", nullable: false),
                    EncryptBeforeArchival = table.Column<bool>(type: "bit", nullable: false),
                    EnableAutomaticDeletion = table.Column<bool>(type: "bit", nullable: false),
                    DaysBeforeDeletion = table.Column<int>(type: "int", nullable: false),
                    PermanentDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeletionMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecureWipeOverwrites = table.Column<int>(type: "int", nullable: false),
                    RequireApprovalBeforeDeletion = table.Column<bool>(type: "bit", nullable: false),
                    ComplianceStandard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JustificationDocumentation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsComplianceRequired = table.Column<bool>(type: "bit", nullable: false),
                    LegalHold = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LegalHoldExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BypassDeletionIfLegalHold = table.Column<bool>(type: "bit", nullable: false),
                    DocumentsAffected = table.Column<int>(type: "int", nullable: false),
                    DocumentsArchived = table.Column<int>(type: "int", nullable: false),
                    DocumentsDeleted = table.Column<int>(type: "int", nullable: false),
                    LastExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastExecutionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextScheduledExecutionDays = table.Column<int>(type: "int", nullable: true),
                    AutoExecute = table.Column<bool>(type: "bit", nullable: false),
                    ExecutionSchedule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotifyBeforeDeletion = table.Column<bool>(type: "bit", nullable: false),
                    NotificationDaysBefore = table.Column<int>(type: "int", nullable: false),
                    AllowExceptions = table.Column<bool>(type: "bit", nullable: false),
                    ExceptionCount = table.Column<int>(type: "int", nullable: false),
                    ExceptedDocumentIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExceptedUserIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsPilot = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectiveFromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImplementationNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRetentions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentRetentions_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    S3Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    S3BucketName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumentCategory = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumentTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveFromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    ParentDocumentId = table.Column<long>(type: "bigint", nullable: true),
                    VersionNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassificationLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowedRoles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowedUserIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    RequiresSignature = table.Column<bool>(type: "bit", nullable: false),
                    IsEncrypted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    ArchivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchivedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSoftCopy = table.Column<bool>(type: "bit", nullable: false),
                    IsHardCopyStored = table.Column<bool>(type: "bit", nullable: false),
                    HardCopyLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RetentionDays = table.Column<int>(type: "int", nullable: false),
                    ScheduledDeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RetentionReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsComplianceDocument = table.Column<bool>(type: "bit", nullable: false),
                    ComplianceStandard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataClassification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchableText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedDocumentIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessCount = table.Column<int>(type: "int", nullable: false),
                    LastAccessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAccessedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DownloadCount = table.Column<int>(type: "int", nullable: false),
                    PrintCount = table.Column<int>(type: "int", nullable: false),
                    HasBeenModified = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompressed = table.Column<bool>(type: "bit", nullable: false),
                    CompressionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Checksum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiresApproval = table.Column<bool>(type: "bit", nullable: false),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentAccesses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false),
                    AccessedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessedByUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessedByUserRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccessReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccessEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurationSeconds = table.Column<int>(type: "int", nullable: true),
                    WasAuthorized = table.Column<bool>(type: "bit", nullable: false),
                    AuthorizationLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorizationReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentVersionAtAccess = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentStatusAtAccess = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueryParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasDownloaded = table.Column<bool>(type: "bit", nullable: false),
                    DownloadFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasPrinted = table.Column<bool>(type: "bit", nullable: false),
                    PrintPageCount = table.Column<int>(type: "int", nullable: true),
                    WasEdited = table.Column<bool>(type: "bit", nullable: false),
                    WasShared = table.Column<bool>(type: "bit", nullable: false),
                    SharedWithUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SharedWithUserIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SharingExpiration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessViolated = table.Column<bool>(type: "bit", nullable: false),
                    AccessViolationReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAnomalousAccess = table.Column<bool>(type: "bit", nullable: false),
                    AnomalyIndicators = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasEncrypted = table.Column<bool>(type: "bit", nullable: false),
                    WasMasked = table.Column<bool>(type: "bit", nullable: false),
                    DataExfiltrationDetected = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlaggedForReview = table.Column<bool>(type: "bit", nullable: false),
                    FlagReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentAccesses_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentAccesses_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAccesses_AccessedByUserId",
                table: "DocumentAccesses",
                column: "AccessedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAccesses_AccessedDate",
                table: "DocumentAccesses",
                column: "AccessedDate");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAccesses_DocumentId",
                table: "DocumentAccesses",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAccesses_FacilityId",
                table: "DocumentAccesses",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCategories_CategoryCode",
                table: "DocumentCategories",
                column: "CategoryCode");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCategories_FacilityId",
                table: "DocumentCategories",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRetentions_DocumentType",
                table: "DocumentRetentions",
                column: "DocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRetentions_FacilityId",
                table: "DocumentRetentions",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRetentions_IsActive",
                table: "DocumentRetentions",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentCategory",
                table: "Documents",
                column: "DocumentCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentType",
                table: "Documents",
                column: "DocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ExpiryDate",
                table: "Documents",
                column: "ExpiryDate");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_FacilityId",
                table: "Documents",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Status",
                table: "Documents",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UploadedDate",
                table: "Documents",
                column: "UploadedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentAccesses");

            migrationBuilder.DropTable(
                name: "DocumentCategories");

            migrationBuilder.DropTable(
                name: "DocumentRetentions");

            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
