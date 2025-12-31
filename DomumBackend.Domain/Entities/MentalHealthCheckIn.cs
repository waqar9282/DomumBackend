using DomumBackend.Domain.Enums;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Mental Health Check-In - Tracks mood, wellbeing, and emotional state over time
    /// </summary>
    public class MentalHealthCheckIn : BaseEntity
    {
        public string FacilityId { get; set; }
        public string YoungPersonId { get; set; }
        public YoungPerson YoungPerson { get; set; }

        // Check-in Details
        public DateTime CheckInDate { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public string CheckInLocation { get; set; } // Where check-in occurred

        // Mood & Emotional State
        public MoodLevel CurrentMood { get; set; }
        public string MoodDescription { get; set; } // In their words
        public string EmotionalState { get; set; } // Happy, sad, anxious, angry, calm, etc.
        public int? MoodScore { get; set; } // 1-10 scale (0-10)

        // Wellness Indicators
        public string SleepQuality { get; set; } // Poor, Fair, Good, Excellent
        public int? HoursSlept { get; set; } // Last night
        public string EnergyLevel { get; set; } // Low, Moderate, High
        public string Appetite { get; set; } // Normal, Decreased, Increased
        public string SocialEngagement { get; set; } // Withdrawn, Quiet, Normal, Very Social

        // Concerns & Triggers
        public string CurrentConcerns { get; set; } // What's bothering them
        public string Stressors { get; set; } // What's causing stress
        public string RecentChallenges { get; set; } // Recent difficulties
        public string Triggers { get; set; } // What triggers negative emotions

        // Coping Strategies
        public string CopingStrategies { get; set; } // How they're coping
        public string SupportSources { get; set; } // Who/what provides support
        public string HealthyActivities { get; set; } // Positive activities they're doing

        // Risk Assessment
        public bool HasSuicidalThoughts { get; set; }
        public bool IsSelfHarming { get; set; }
        public bool ExpressingHopelessness { get; set; }
        public bool SocialWithdrawal { get; set; }
        public string RiskFactorsIdentified { get; set; }

        // Protective Factors
        public string ProtectiveFactors { get; set; } // What keeps them safe
        public string Strengths { get; set; } // Personal strengths noted
        public string Goals { get; set; } // What they're working towards

        // Staff Observations & Response
        public string StaffObservations { get; set; } // What staff noticed
        public string ActionsRequired { get; set; } // Any interventions needed
        public bool ReferralToMentalHealthRequired { get; set; }
        public string ReferralDetails { get; set; }

        // Follow-up
        public DateTime? FollowUpDate { get; set; }
        public string FollowUpNotes { get; set; }

        // Staff Recording
        public string CheckInByUserId { get; set; } // Staff who conducted check-in
        public DateTime RecordedDate { get; set; }
        public bool IsConfidential { get; set; }
    }
}
