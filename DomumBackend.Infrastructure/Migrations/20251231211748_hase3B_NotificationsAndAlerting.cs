using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomumBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class hase3B_NotificationsAndAlerting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertRules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlertType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TriggerCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThresholdValue = table.Column<int>(type: "int", nullable: false),
                    TimeframeMinutes = table.Column<int>(type: "int", nullable: false),
                    AffectedEntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientRoles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificRecipientUserIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotifyFacilityManager = table.Column<bool>(type: "bit", nullable: false),
                    NotifySafeguardingOfficer = table.Column<bool>(type: "bit", nullable: false),
                    EscalateToDirestion = table.Column<bool>(type: "bit", nullable: false),
                    NotificationChannels = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationDelaySeconds = table.Column<int>(type: "int", nullable: true),
                    AllowAggregation = table.Column<bool>(type: "bit", nullable: false),
                    MaxNotificationsPerDay = table.Column<int>(type: "int", nullable: true),
                    CreatedByUserId_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimesTriggered = table.Column<int>(type: "int", nullable: false),
                    LastTriggeredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TemporarilySuspended = table.Column<bool>(type: "bit", nullable: false),
                    SuspensionUntilDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertRules_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FacilityReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportGeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalYoungPeopleServed = table.Column<int>(type: "int", nullable: false),
                    TotalStaffMembers = table.Column<int>(type: "int", nullable: false),
                    TotalRooms = table.Column<int>(type: "int", nullable: false),
                    OccupancyRate = table.Column<int>(type: "int", nullable: false),
                    TotalIncidentsReported = table.Column<int>(type: "int", nullable: false),
                    HighRiskIncidents = table.Column<int>(type: "int", nullable: false),
                    SafeguardingConcernsReported = table.Column<int>(type: "int", nullable: false),
                    ActiveSafetyAlerts = table.Column<int>(type: "int", nullable: false),
                    IncidentReductionRate = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    YoungPeopleWithHealthAssessments = table.Column<int>(type: "int", nullable: false),
                    YoungPeopleOnMedications = table.Column<int>(type: "int", nullable: false),
                    HealthConcernsIdentified = table.Column<int>(type: "int", nullable: false),
                    HealthGoalCompletionRate = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AuditLogsRecorded = table.Column<int>(type: "int", nullable: false),
                    ComplianceViolations = table.Column<int>(type: "int", nullable: false),
                    ComplianceScore = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    TrainingCompleted = table.Column<int>(type: "int", nullable: false),
                    StaffTurnovers = table.Column<int>(type: "int", nullable: false),
                    StaffRetentionRate = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CriticalSeverity = table.Column<int>(type: "int", nullable: false),
                    HighSeverity = table.Column<int>(type: "int", nullable: false),
                    MediumSeverity = table.Column<int>(type: "int", nullable: false),
                    LowSeverity = table.Column<int>(type: "int", nullable: false),
                    YoungPeopleDischargedSuccessfully = table.Column<int>(type: "int", nullable: false),
                    YoungPeopleReadmitted = table.Column<int>(type: "int", nullable: false),
                    SuccessfulOutcomeRate = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    DocumentsStored = table.Column<int>(type: "int", nullable: false),
                    MedicalRecordsOnFile = table.Column<int>(type: "int", nullable: false),
                    StorageUtilizationPercentage = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    KPI1_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPI1_Value = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    KPI2_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPI2_Value = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    KPI3_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KPI3_Value = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    TopStrengths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaOfImprovement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StrategicRecommendations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiresExecutiveAttention = table.Column<bool>(type: "bit", nullable: false),
                    GeneratedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityReports_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HealthMetricsReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportGeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAssessments = table.Column<int>(type: "int", nullable: false),
                    AverageHeight = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AverageWeight = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AverageBodyMassIndex = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AverageBloodPressureSystolic = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AverageBloodPressureDiastolic = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    YoungPeopleOnMedication = table.Column<int>(type: "int", nullable: false),
                    ActiveMedicationCount = table.Column<int>(type: "int", nullable: false),
                    MedicationConcerns = table.Column<int>(type: "int", nullable: false),
                    MedicationComplianceRate = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    NutritionLogsRecorded = table.Column<int>(type: "int", nullable: false),
                    AverageCaloriesPerDay = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AverageProteinIntake = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AverageFatIntake = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    YoungPeopleFollowingDietaryPlan = table.Column<int>(type: "int", nullable: false),
                    DietaryRestrictionsIdentified = table.Column<int>(type: "int", nullable: false),
                    ActivityLogsRecorded = table.Column<int>(type: "int", nullable: false),
                    AverageActivityMinutesPerWeek = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    YoungPeopleMeetingActivityGoals = table.Column<int>(type: "int", nullable: false),
                    ActivityComplianceRate = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    MostCommonActivityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MentalHealthCheckInsRecorded = table.Column<int>(type: "int", nullable: false),
                    YoungPeopleFlaggedForConcern = table.Column<int>(type: "int", nullable: false),
                    AverageMoodScore = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    MentalHealthInterventionsRecommended = table.Column<int>(type: "int", nullable: false),
                    PrevalentHealthConcerns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuicidalThoughtFlags = table.Column<int>(type: "int", nullable: false),
                    SelfHarmFlags = table.Column<int>(type: "int", nullable: false),
                    AllergiesIdentified = table.Column<int>(type: "int", nullable: false),
                    RecommendedActions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiresImmedateAction = table.Column<bool>(type: "bit", nullable: false),
                    GeneratedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMetricsReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthMetricsReports_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IncidentReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportGeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalIncidents = table.Column<int>(type: "int", nullable: false),
                    HighSeverityCount = table.Column<int>(type: "int", nullable: false),
                    MediumSeverityCount = table.Column<int>(type: "int", nullable: false),
                    LowSeverityCount = table.Column<int>(type: "int", nullable: false),
                    BehavioralIncidents = table.Column<int>(type: "int", nullable: false),
                    InjuryIncidents = table.Column<int>(type: "int", nullable: false),
                    SafetyIncidents = table.Column<int>(type: "int", nullable: false),
                    MedicalIncidents = table.Column<int>(type: "int", nullable: false),
                    OtherIncidents = table.Column<int>(type: "int", nullable: false),
                    OpenIncidents = table.Column<int>(type: "int", nullable: false),
                    ClosedIncidents = table.Column<int>(type: "int", nullable: false),
                    InvestigationIncidents = table.Column<int>(type: "int", nullable: false),
                    IncidentTrend = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    TrendingUp = table.Column<bool>(type: "bit", nullable: false),
                    PeakIncidentDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommonIncidentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YoungPeopleAffected = table.Column<int>(type: "int", nullable: false),
                    RepeatedOffenders = table.Column<int>(type: "int", nullable: false),
                    RecommendedActions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiresImmedateAction = table.Column<bool>(type: "bit", nullable: false),
                    GeneratedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncidentReports_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationPreferences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmailNotificationsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnsubscribedFromAllEmails = table.Column<bool>(type: "bit", nullable: false),
                    UnsubscribedEmailCategories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSNotificationsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnsubscribedFromAllSMS = table.Column<bool>(type: "bit", nullable: false),
                    InAppNotificationsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ShowNotificationBadge = table.Column<bool>(type: "bit", nullable: false),
                    PlaySoundForNotifications = table.Column<bool>(type: "bit", nullable: false),
                    ImmediateAlertFrequency = table.Column<int>(type: "int", nullable: false),
                    HighPriorityFrequency = table.Column<int>(type: "int", nullable: false),
                    MediumPriorityFrequency = table.Column<int>(type: "int", nullable: false),
                    LowPriorityFrequency = table.Column<int>(type: "int", nullable: false),
                    QuietHoursEnabled = table.Column<bool>(type: "bit", nullable: false),
                    QuietHoursStart = table.Column<TimeSpan>(type: "time", nullable: true),
                    QuietHoursEnd = table.Column<TimeSpan>(type: "time", nullable: true),
                    NotifyOnIncidents = table.Column<bool>(type: "bit", nullable: false),
                    NotifyOnHealthConcerns = table.Column<bool>(type: "bit", nullable: false),
                    NotifyOnSafeguardingIssues = table.Column<bool>(type: "bit", nullable: false),
                    NotifyOnAuditEvents = table.Column<bool>(type: "bit", nullable: false),
                    NotifyOnReportGeneration = table.Column<bool>(type: "bit", nullable: false),
                    IncludeDetailedContent = table.Column<bool>(type: "bit", nullable: false),
                    IncludeActionableLinks = table.Column<bool>(type: "bit", nullable: false),
                    UnsubscribeToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferenceLastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationPreferences_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipientUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipientEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtmlContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    TriggerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TriggerEntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    LastErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate_Scheduled = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledSendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsScheduled = table.Column<bool>(type: "bit", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotificationChannel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSubjectTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailBodyTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailHtmlTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSBodyTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSMaxLength = table.Column<int>(type: "int", nullable: true),
                    InAppTitleTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InAppMessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InAppActionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportedVariables = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    EffectiveFromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate_Template = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimesUsed = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasLocalizedVersions = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTemplates_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertRules_FacilityId",
                table: "AlertRules",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertRules_IsActive",
                table: "AlertRules",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityReports_FacilityId",
                table: "FacilityReports",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityReports_ReportGeneratedDate",
                table: "FacilityReports",
                column: "ReportGeneratedDate");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetricsReports_FacilityId",
                table: "HealthMetricsReports",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetricsReports_ReportGeneratedDate",
                table: "HealthMetricsReports",
                column: "ReportGeneratedDate");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentReports_FacilityId",
                table: "IncidentReports",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentReports_ReportGeneratedDate",
                table: "IncidentReports",
                column: "ReportGeneratedDate");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPreferences_FacilityId",
                table: "NotificationPreferences",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPreferences_UserId",
                table: "NotificationPreferences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedDate_Scheduled",
                table: "Notifications",
                column: "CreatedDate_Scheduled");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FacilityId",
                table: "Notifications",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecipientUserId",
                table: "Notifications",
                column: "RecipientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Status",
                table: "Notifications",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_Category",
                table: "NotificationTemplates",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_FacilityId",
                table: "NotificationTemplates",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_TemplateKey",
                table: "NotificationTemplates",
                column: "TemplateKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertRules");

            migrationBuilder.DropTable(
                name: "FacilityReports");

            migrationBuilder.DropTable(
                name: "HealthMetricsReports");

            migrationBuilder.DropTable(
                name: "IncidentReports");

            migrationBuilder.DropTable(
                name: "NotificationPreferences");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationTemplates");
        }
    }
}
