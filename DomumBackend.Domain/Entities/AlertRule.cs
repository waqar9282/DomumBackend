namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Alert Rule - Defines alert criteria and actions
    /// Configures automatic alerts when certain conditions are met
    /// </summary>
    public class AlertRule : BaseEntity
    {
        public required string FacilityId { get; set; }
        public Facility? Facility { get; set; }

        // Alert Configuration
        public required string RuleName { get; set; }
        public string? Description { get; set; }
        public required string AlertType { get; set; } // HighIncidentRate, HealthConcern, SafeguardingRisk, etc.
        public bool IsActive { get; set; } = true;

        // Trigger Conditions
        public string? TriggerCondition { get; set; } // JSON or structured condition logic
        public int ThresholdValue { get; set; } // e.g., number of incidents in a time period
        public int TimeframeMinutes { get; set; } // Time period for threshold check
        public string? AffectedEntityType { get; set; } // Incident, HealthAssessment, YoungPerson, etc.

        // Recipients
        public required string RecipientRoles { get; set; } // Comma-separated: Manager, Safeguarding Officer, Director
        public string? SpecificRecipientUserIds { get; set; } // Comma-separated user IDs for specific recipients
        public bool NotifyFacilityManager { get; set; } = true;
        public bool NotifySafeguardingOfficer { get; set; } = true;
        public bool EscalateToDirestion { get; set; } = false;

        // Notification Settings
        public required string NotificationChannels { get; set; } // Email, SMS, InApp
        public int? NotificationDelaySeconds { get; set; } // Delay before sending (for aggregation)
        public bool AllowAggregation { get; set; } = false; // Combine multiple alerts into one
        public int? MaxNotificationsPerDay { get; set; } // Prevent alert fatigue

        // Rule Management
        public DateTime CreatedByUserId_Date { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedByUserId { get; set; }
        public int TimesTriggered { get; set; } = 0;
        public DateTime? LastTriggeredDate { get; set; }

        // Disable Temporarily
        public bool TemporarilySuspended { get; set; } = false;
        public DateTime? SuspensionUntilDate { get; set; }
    }
}
