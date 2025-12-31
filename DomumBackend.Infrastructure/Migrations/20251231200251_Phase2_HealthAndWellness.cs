using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomumBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Phase2_HealthAndWellness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthAssessments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YoungPersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssessmentType = table.Column<int>(type: "int", nullable: false),
                    AssessmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssessedByProfessional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssessmentLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    BMI = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    BloodPressureSystolic = table.Column<int>(type: "int", nullable: true),
                    BloodPressureDiastolic = table.Column<int>(type: "int", nullable: true),
                    HeartRate = table.Column<int>(type: "int", nullable: true),
                    BodyTemperature = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    VisionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HearingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DentalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmotionalState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Behavior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mood = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuicidalThoughts = table.Column<bool>(type: "bit", nullable: false),
                    SelfHarmConcerns = table.Column<bool>(type: "bit", nullable: false),
                    MentalHealthConcerns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CognitiveFunction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpeechLanguageStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossMotorSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FineMotorSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicProgress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssessmentFindings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recommendations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralRequired = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextAssessmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfidential = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthAssessments_YoungPeople_YoungPersonId",
                        column: x => x.YoungPersonId,
                        principalTable: "YoungPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YoungPersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppointmentType = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HealthcareProfessional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthcareFacility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chief_Complaint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symptoms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FindingsObservations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Treatment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferralTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FollowUpNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfidential = table.Column<bool>(type: "bit", nullable: false),
                    ConsentGiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_YoungPeople_YoungPersonId",
                        column: x => x.YoungPersonId,
                        principalTable: "YoungPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YoungPersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveIngredient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DosageUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityPerDose = table.Column<int>(type: "int", nullable: false),
                    PrescriptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrescribedByDoctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorQualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonForMedication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdministeredByStaff = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DiscontinuationReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnownAllergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PotentialSideEffects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportedSideEffects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContraindicatedWith = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiresRefrigeration = table.Column<bool>(type: "bit", nullable: false),
                    SpecialHandling = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfidential = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medications_YoungPeople_YoungPersonId",
                        column: x => x.YoungPersonId,
                        principalTable: "YoungPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MentalHealthCheckIns",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YoungPersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CheckInLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentMood = table.Column<int>(type: "int", nullable: false),
                    MoodDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmotionalState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoodScore = table.Column<int>(type: "int", nullable: true),
                    SleepQuality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoursSlept = table.Column<int>(type: "int", nullable: true),
                    EnergyLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Appetite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialEngagement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentConcerns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stressors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecentChallenges = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Triggers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CopingStrategies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupportSources = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthyActivities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasSuicidalThoughts = table.Column<bool>(type: "bit", nullable: false),
                    IsSelfHarming = table.Column<bool>(type: "bit", nullable: false),
                    ExpressingHopelessness = table.Column<bool>(type: "bit", nullable: false),
                    SocialWithdrawal = table.Column<bool>(type: "bit", nullable: false),
                    RiskFactorsIdentified = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProtectiveFactors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Strengths = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Goals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffObservations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionsRequired = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferralToMentalHealthRequired = table.Column<bool>(type: "bit", nullable: false),
                    ReferralDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FollowUpNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckInByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfidential = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentalHealthCheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MentalHealthCheckIns_YoungPeople_YoungPersonId",
                        column: x => x.YoungPersonId,
                        principalTable: "YoungPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NutritionLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YoungPersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MealType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortionSize = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Measurement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Protein = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Carbohydrates = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Fat = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Fiber = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Sugar = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Sodium = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Allergens = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DietaryRestrictions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FollowsDietaryPlan = table.Column<bool>(type: "bit", nullable: false),
                    Appetite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EatingBehavior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preferences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DigestiveIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPartOfNutritionPlan = table.Column<bool>(type: "bit", nullable: false),
                    NutritionGoals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DietaryRecommendation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionLogs_YoungPeople_YoungPersonId",
                        column: x => x.YoungPersonId,
                        principalTable: "YoungPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhysicalActivityLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YoungPersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false),
                    ActivityDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Intensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Participants = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffSupervised = table.Column<bool>(type: "bit", nullable: false),
                    SupervisingStaff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformanceLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Effort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialInteraction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntoleranceObserved = table.Column<bool>(type: "bit", nullable: false),
                    IntoleranceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeartRatePreActivity = table.Column<int>(type: "int", nullable: true),
                    HeartRatePostActivity = table.Column<int>(type: "int", nullable: true),
                    AnyInjuriesObserved = table.Column<bool>(type: "bit", nullable: false),
                    InjuryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProperEquipmentUsed = table.Column<bool>(type: "bit", nullable: false),
                    SafetyProtocolsFollowed = table.Column<bool>(type: "bit", nullable: false),
                    MeetsPhysicalActivityGoal = table.Column<bool>(type: "bit", nullable: false),
                    ProgressNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendationsForNextSession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalActivityLogs_YoungPeople_YoungPersonId",
                        column: x => x.YoungPersonId,
                        principalTable: "YoungPeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthAssessments_AssessmentDate",
                table: "HealthAssessments",
                column: "AssessmentDate");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAssessments_AssessmentType",
                table: "HealthAssessments",
                column: "AssessmentType");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAssessments_FacilityId",
                table: "HealthAssessments",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAssessments_YoungPersonId",
                table: "HealthAssessments",
                column: "YoungPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_AppointmentDate",
                table: "MedicalRecords",
                column: "AppointmentDate");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_FacilityId",
                table: "MedicalRecords",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Status",
                table: "MedicalRecords",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_YoungPersonId",
                table: "MedicalRecords",
                column: "YoungPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_FacilityId",
                table: "Medications",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_StartDate",
                table: "Medications",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_Status",
                table: "Medications",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_YoungPersonId",
                table: "Medications",
                column: "YoungPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_MentalHealthCheckIns_CheckInDate",
                table: "MentalHealthCheckIns",
                column: "CheckInDate");

            migrationBuilder.CreateIndex(
                name: "IX_MentalHealthCheckIns_CurrentMood",
                table: "MentalHealthCheckIns",
                column: "CurrentMood");

            migrationBuilder.CreateIndex(
                name: "IX_MentalHealthCheckIns_FacilityId",
                table: "MentalHealthCheckIns",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MentalHealthCheckIns_YoungPersonId",
                table: "MentalHealthCheckIns",
                column: "YoungPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionLogs_FacilityId",
                table: "NutritionLogs",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionLogs_LogDate",
                table: "NutritionLogs",
                column: "LogDate");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionLogs_YoungPersonId",
                table: "NutritionLogs",
                column: "YoungPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalActivityLogs_ActivityDate",
                table: "PhysicalActivityLogs",
                column: "ActivityDate");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalActivityLogs_ActivityType",
                table: "PhysicalActivityLogs",
                column: "ActivityType");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalActivityLogs_FacilityId",
                table: "PhysicalActivityLogs",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalActivityLogs_YoungPersonId",
                table: "PhysicalActivityLogs",
                column: "YoungPersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthAssessments");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "MentalHealthCheckIns");

            migrationBuilder.DropTable(
                name: "NutritionLogs");

            migrationBuilder.DropTable(
                name: "PhysicalActivityLogs");
        }
    }
}
