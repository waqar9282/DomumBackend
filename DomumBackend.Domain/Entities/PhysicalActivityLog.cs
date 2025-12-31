using DomumBackend.Domain.Enums;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Physical Activity Log - Tracks exercise, sports, and physical activity
    /// </summary>
    public class PhysicalActivityLog : BaseEntity
    {
        public required string FacilityId { get; set; }
        public required string YoungPersonId { get; set; }
        public YoungPerson? YoungPerson { get; set; }

        // Activity Details
        public DateTime ActivityDate { get; set; }
        public ActivityType ActivityType { get; set; }
        public required string ActivityDescription { get; set; } // Specific activity details
        public required string Location { get; set; } // Where activity took place

        // Duration & Intensity
        public TimeSpan Duration { get; set; } // How long the activity lasted
        public int DurationMinutes { get; set; } // Total minutes
        public string? Intensity { get; set; } // Light, Moderate, Vigorous

        // Participation
        public string? Participants { get; set; } // Who participated (staff names, peers)
        public bool StaffSupervised { get; set; }
        public string? SupervisingStaff { get; set; } // Staff member name

        // Performance & Observations
        public string? PerformanceLevel { get; set; } // Excellent, Good, Fair, Poor
        public string? Skills { get; set; } // Skills demonstrated or developed
        public string? Effort { get; set; } // Level of effort shown
        public string? Attitude { get; set; } // Attitude during activity
        public string? SocialInteraction { get; set; } // How they interacted with others

        // Physical Response
        public string? PhysicalCondition { get; set; } // Any physical concerns noted
        public bool IntoleranceObserved { get; set; } // Any physical distress
        public string? IntoleranceDescription { get; set; }
        public int? HeartRatePreActivity { get; set; }
        public int? HeartRatePostActivity { get; set; }

        // Health & Safety
        public bool AnyInjuriesObserved { get; set; }
        public string? InjuryDescription { get; set; }
        public bool ProperEquipmentUsed { get; set; }
        public bool SafetyProtocolsFollowed { get; set; }

        // Goals & Progress
        public bool MeetsPhysicalActivityGoal { get; set; }
        public string? ProgressNotes { get; set; }
        public string? RecommendationsForNextSession { get; set; }

        // Staff Recording
        public required string RecordedByUserId { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}
