namespace DomumBackend.Application.DTOs
{
    /// <summary>
    /// Health & Wellness DTOs - Data transfer objects for medical records, medications, health assessments, nutrition, physical activity, and mental health check-ins
    /// </summary>
    /// 
    // Medical Record DTOs
    public class MedicalRecordResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public int AppointmentType { get; set; }
        public string? AppointmentTypeDescription { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? HealthcareProfessional { get; set; }
        public string? HealthcareFacility { get; set; }
        public string? Diagnosis { get; set; }
        public string? Chief_Complaint { get; set; }
        public string? Treatment { get; set; }
        public int Status { get; set; }
        public string? StatusDescription { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class MedicalRecordDetailsResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public int AppointmentType { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? HealthcareProfessional { get; set; }
        public string? HealthcareFacility { get; set; }
        public int Status { get; set; }
        public string? Diagnosis { get; set; }
        public string? Chief_Complaint { get; set; }
        public string? Symptoms { get; set; }
        public string? FindingsObservations { get; set; }
        public string? Treatment { get; set; }
        public string? Prescription { get; set; }
        public string? ReferralTo { get; set; }
        public string? Notes { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string? FollowUpNotes { get; set; }
        public string? RecordedByUserId { get; set; }
        public DateTime RecordedDate { get; set; }
        public bool IsConfidential { get; set; }
    }

    // Medication DTOs
    public class MedicationResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public string? MedicationName { get; set; }
        public string? Dosage { get; set; }
        public string? Frequency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public string? StatusDescription { get; set; }
        public string? PrescribedByDoctor { get; set; }
        public string? ReasonForMedication { get; set; }
        public bool IsAdministeredByStaff { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class MedicationDetailsResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public string? MedicationName { get; set; }
        public string? ActiveIngredient { get; set; }
        public string? Dosage { get; set; }
        public string? DosageUnit { get; set; }
        public int QuantityPerDose { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int DurationDays { get; set; }
        public string? Frequency { get; set; }
        public string? Route { get; set; }
        public string? PrescribedByDoctor { get; set; }
        public string? DoctorQualification { get; set; }
        public string? ReasonForMedication { get; set; }
        public bool IsAdministeredByStaff { get; set; }
        public int Status { get; set; }
        public string? DiscontinuationReason { get; set; }
        public string? KnownAllergies { get; set; }
        public string? PotentialSideEffects { get; set; }
        public string? ReportedSideEffects { get; set; }
        public string? ContraindicatedWith { get; set; }
        public string? StorageLocation { get; set; }
        public bool RequiresRefrigeration { get; set; }
        public DateTime RecordedDate { get; set; }
    }

    // Health Assessment DTOs
    public class HealthAssessmentResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public int AssessmentType { get; set; }
        public string? AssessmentTypeDescription { get; set; }
        public DateTime AssessmentDate { get; set; }
        public string? AssessedByProfessional { get; set; }
        public string? AssessmentFindings { get; set; }
        public string? Recommendations { get; set; }
        public bool ReferralRequired { get; set; }
        public DateTime? NextAssessmentDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class HealthAssessmentDetailsResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public int AssessmentType { get; set; }
        public DateTime AssessmentDate { get; set; }
        public string? AssessedByProfessional { get; set; }
        public string? AssessmentLocation { get; set; }
        // Physical
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? BMI { get; set; }
        public int? BloodPressureSystolic { get; set; }
        public int? BloodPressureDiastolic { get; set; }
        public int? HeartRate { get; set; }
        public string? VisionStatus { get; set; }
        public string? HearingStatus { get; set; }
        public string? DentalStatus { get; set; }
        // Mental Health
        public string? EmotionalState { get; set; }
        public string? Behavior { get; set; }
        public string? Mood { get; set; }
        public bool SuicidalThoughts { get; set; }
        public bool SelfHarmConcerns { get; set; }
        public string? MentalHealthConcerns { get; set; }
        // Developmental
        public string? SpeechLanguageStatus { get; set; }
        public string? GrossMotorSkills { get; set; }
        public string? FineMotorSkills { get; set; }
        public string? SocialSkills { get; set; }
        public string? AcademicProgress { get; set; }
        // Summary
        public string? AssessmentFindings { get; set; }
        public string? Recommendations { get; set; }
        public string? ReferralRequired { get; set; }
        public DateTime? NextAssessmentDate { get; set; }
        public DateTime RecordedDate { get; set; }
    }

    // Nutrition Log DTOs
    public class NutritionLogResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public DateTime LogDate { get; set; }
        public string? MealType { get; set; }
        public string? FoodDescription { get; set; }
        public decimal? PortionSize { get; set; }
        public string? Appetite { get; set; }
        public decimal? Calories { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class NutritionLogDetailsResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public DateTime LogDate { get; set; }
        public string? MealType { get; set; }
        public string? FoodDescription { get; set; }
        public string? Ingredients { get; set; }
        public decimal? PortionSize { get; set; }
        public string? Measurement { get; set; }
        public decimal? Calories { get; set; }
        public decimal? Protein { get; set; }
        public decimal? Carbohydrates { get; set; }
        public decimal? Fat { get; set; }
        public decimal? Fiber { get; set; }
        public decimal? Sugar { get; set; }
        public string? Allergens { get; set; }
        public string? DietaryRestrictions { get; set; }
        public string? Appetite { get; set; }
        public string? EatingBehavior { get; set; }
        public string? DigestiveIssues { get; set; }
        public string? Notes { get; set; }
        public bool IsPartOfNutritionPlan { get; set; }
        public string? DietaryRecommendation { get; set; }
        public DateTime RecordedDate { get; set; }
    }

    // Physical Activity Log DTOs
    public class PhysicalActivityLogResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public int ActivityType { get; set; }
        public string? ActivityTypeDescription { get; set; }
        public DateTime ActivityDate { get; set; }
        public string? ActivityDescription { get; set; }
        public int DurationMinutes { get; set; }
        public string? Intensity { get; set; }
        public string? PerformanceLevel { get; set; }
        public string? Effort { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class PhysicalActivityLogDetailsResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public int ActivityType { get; set; }
        public DateTime ActivityDate { get; set; }
        public string? ActivityDescription { get; set; }
        public string? Location { get; set; }
        public int DurationMinutes { get; set; }
        public string? Intensity { get; set; }
        public string? Participants { get; set; }
        public bool StaffSupervised { get; set; }
        public string? SupervisingStaff { get; set; }
        public string? PerformanceLevel { get; set; }
        public string? Skills { get; set; }
        public string? Effort { get; set; }
        public string? Attitude { get; set; }
        public string? SocialInteraction { get; set; }
        public string? PhysicalCondition { get; set; }
        public bool AnyInjuriesObserved { get; set; }
        public string? InjuryDescription { get; set; }
        public bool SafetyProtocolsFollowed { get; set; }
        public bool MeetsPhysicalActivityGoal { get; set; }
        public string? ProgressNotes { get; set; }
        public DateTime RecordedDate { get; set; }
    }

    // Mental Health Check-In DTOs
    public class MentalHealthCheckInResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public DateTime CheckInDate { get; set; }
        public int CurrentMood { get; set; }
        public string? MoodDescription { get; set; }
        public string? EmotionalState { get; set; }
        public int? MoodScore { get; set; }
        public string? SleepQuality { get; set; }
        public string? EnergyLevel { get; set; }
        public string? CurrentConcerns { get; set; }
        public bool HasSuicidalThoughts { get; set; }
        public bool IsSelfHarming { get; set; }
        public string? StaffObservations { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class MentalHealthCheckInDetailsResponseDTO
    {
        public string? Id { get; set; }
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? YoungPersonName { get; set; }
        public DateTime CheckInDate { get; set; }
        public int CurrentMood { get; set; }
        public string? MoodDescription { get; set; }
        public string? EmotionalState { get; set; }
        public int? MoodScore { get; set; }
        public string? SleepQuality { get; set; }
        public int? HoursSlept { get; set; }
        public string? EnergyLevel { get; set; }
        public string? Appetite { get; set; }
        public string? SocialEngagement { get; set; }
        public string? CurrentConcerns { get; set; }
        public string? Stressors { get; set; }
        public string? RecentChallenges { get; set; }
        public string? Triggers { get; set; }
        public string? CopingStrategies { get; set; }
        public string? SupportSources { get; set; }
        public string? HealthyActivities { get; set; }
        public bool HasSuicidalThoughts { get; set; }
        public bool IsSelfHarming { get; set; }
        public bool ExpressingHopelessness { get; set; }
        public bool SocialWithdrawal { get; set; }
        public string? RiskFactorsIdentified { get; set; }
        public string? ProtectiveFactors { get; set; }
        public string? Strengths { get; set; }
        public string? Goals { get; set; }
        public string? StaffObservations { get; set; }
        public string? ActionsRequired { get; set; }
        public bool ReferralToMentalHealthRequired { get; set; }
        public string? ReferralDetails { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string? FollowUpNotes { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}

