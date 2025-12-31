using DomumBackend.Domain.Enums;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Health Assessment - Tracks physical, mental, and developmental evaluations
    /// </summary>
    public class HealthAssessment : BaseEntity
    {
        public required string FacilityId { get; set; }
        public required string YoungPersonId { get; set; }
        public YoungPerson? YoungPerson { get; set; }

        // Assessment Details
        public HealthAssessmentType AssessmentType { get; set; }
        public DateTime AssessmentDate { get; set; }
        public required string AssessedByProfessional { get; set; } // Name and qualification
        public required string AssessmentLocation { get; set; }

        // Physical Health Assessment
        public decimal? Height { get; set; } // cm
        public decimal? Weight { get; set; } // kg
        public decimal? BMI { get; set; } // Calculated
        public int? BloodPressureSystolic { get; set; }
        public int? BloodPressureDiastolic { get; set; }
        public int? HeartRate { get; set; } // bpm
        public decimal? BodyTemperature { get; set; } // Celsius
        public string? VisionStatus { get; set; } // Normal, needs correction, impaired
        public string? HearingStatus { get; set; }
        public string? DentalStatus { get; set; }
        public string? PhysicalCondition { get; set; } // General observations

        // Mental Health Assessment
        public string? EmotionalState { get; set; } // Description
        public string? Behavior { get; set; } // Observed behaviors
        public string? Mood { get; set; } // Current mood description
        public bool SuicidalThoughts { get; set; }
        public bool SelfHarmConcerns { get; set; }
        public string? MentalHealthConcerns { get; set; } // Anxiety, depression, etc.
        public string? CognitiveFunction { get; set; } // Memory, concentration, etc.

        // Developmental Assessment
        public string? SpeechLanguageStatus { get; set; }
        public string? GrossMotorSkills { get; set; } // Running, jumping, coordination
        public string? FineMotorSkills { get; set; } // Writing, drawing, dexterity
        public string? SocialSkills { get; set; }
        public string? AcademicProgress { get; set; }

        // Recommendations
        public string? AssessmentFindings { get; set; } // Summary of findings
        public string? Recommendations { get; set; } // Follow-up needed
        public string? ReferralRequired { get; set; } // If specialist needed
        public DateTime? NextAssessmentDate { get; set; }

        // Compliance
        public required string RecordedByUserId { get; set; }
        public DateTime RecordedDate { get; set; }
        public bool IsConfidential { get; set; }
    }
}
