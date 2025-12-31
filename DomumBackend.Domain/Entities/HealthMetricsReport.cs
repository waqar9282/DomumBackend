namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Health Metrics Report - Analytics for health and wellness data
    /// Aggregates health assessment, medication, nutrition, and activity data
    /// </summary>
    public class HealthMetricsReport : BaseEntity
    {
        public required string FacilityId { get; set; }
        public Facility? Facility { get; set; }

        // Report Details
        public required string ReportName { get; set; }
        public string? Description { get; set; }
        public DateTime ReportGeneratedDate { get; set; }
        public DateTime ReportPeriodStart { get; set; }
        public DateTime ReportPeriodEnd { get; set; }

        // Health Assessment Metrics
        public int TotalAssessments { get; set; }
        public decimal AverageHeight { get; set; } // cm
        public decimal AverageWeight { get; set; } // kg
        public decimal AverageBodyMassIndex { get; set; }
        public decimal AverageBloodPressureSystolic { get; set; }
        public decimal AverageBloodPressureDiastolic { get; set; }

        // Medication Metrics
        public int YoungPeopleOnMedication { get; set; }
        public int ActiveMedicationCount { get; set; }
        public int MedicationConcerns { get; set; } // Side effects, allergies reported
        public decimal MedicationComplianceRate { get; set; } // Percentage

        // Nutrition Metrics
        public int NutritionLogsRecorded { get; set; }
        public decimal AverageCaloriesPerDay { get; set; }
        public decimal AverageProteinIntake { get; set; } // grams
        public decimal AverageFatIntake { get; set; } // grams
        public int YoungPeopleFollowingDietaryPlan { get; set; }
        public int DietaryRestrictionsIdentified { get; set; }

        // Physical Activity Metrics
        public int ActivityLogsRecorded { get; set; }
        public decimal AverageActivityMinutesPerWeek { get; set; }
        public int YoungPeopleMeetingActivityGoals { get; set; }
        public decimal ActivityComplianceRate { get; set; } // Percentage
        public string? MostCommonActivityType { get; set; }

        // Mental Health Metrics
        public int MentalHealthCheckInsRecorded { get; set; }
        public int YoungPeopleFlaggedForConcern { get; set; } // Suicidal/self-harm
        public decimal AverageMoodScore { get; set; } // If using numerical scale
        public int MentalHealthInterventionsRecommended { get; set; }

        // Health Concerns
        public string? PrevalentHealthConcerns { get; set; } // Common issues
        public int SuicidalThoughtFlags { get; set; }
        public int SelfHarmFlags { get; set; }
        public int AllergiesIdentified { get; set; }

        // Recommendations
        public string? RecommendedActions { get; set; }
        public bool RequiresImmedateAction { get; set; }

        // Metadata
        public required string GeneratedByUserId { get; set; }
        public string? ApprovedByUserId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? FilePath { get; set; } // PDF/Excel file location
    }
}
