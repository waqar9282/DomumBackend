namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Notification Preference - User notification settings
    /// Controls how and when users receive notifications
    /// </summary>
    public class NotificationPreference : BaseEntity
    {
        public required string UserId { get; set; }
        public required string FacilityId { get; set; }
        public Facility? Facility { get; set; }

        // Email Preferences
        public bool EmailNotificationsEnabled { get; set; } = true;
        public string? EmailAddress { get; set; }
        public bool UnsubscribedFromAllEmails { get; set; } = false;
        public string? UnsubscribedEmailCategories { get; set; } // Comma-separated categories

        // SMS Preferences
        public bool SMSNotificationsEnabled { get; set; } = false;
        public string? PhoneNumber { get; set; }
        public bool UnsubscribedFromAllSMS { get; set; } = false;

        // In-App Preferences
        public bool InAppNotificationsEnabled { get; set; } = true;
        public bool ShowNotificationBadge { get; set; } = true;
        public bool PlaySoundForNotifications { get; set; } = true;

        // Frequency Preferences
        public NotificationFrequency ImmediateAlertFrequency { get; set; } = NotificationFrequency.Immediate; // Critical alerts
        public NotificationFrequency HighPriorityFrequency { get; set; } = NotificationFrequency.Immediate;
        public NotificationFrequency MediumPriorityFrequency { get; set; } = NotificationFrequency.Daily;
        public NotificationFrequency LowPriorityFrequency { get; set; } = NotificationFrequency.Weekly;

        // Time Window
        public bool QuietHoursEnabled { get; set; } = false;
        public TimeSpan? QuietHoursStart { get; set; } // e.g., 22:00
        public TimeSpan? QuietHoursEnd { get; set; } // e.g., 08:00

        // Alert Type Preferences
        public bool NotifyOnIncidents { get; set; } = true;
        public bool NotifyOnHealthConcerns { get; set; } = true;
        public bool NotifyOnSafeguardingIssues { get; set; } = true;
        public bool NotifyOnAuditEvents { get; set; } = false;
        public bool NotifyOnReportGeneration { get; set; } = true;

        // Notification Content
        public bool IncludeDetailedContent { get; set; } = true;
        public bool IncludeActionableLinks { get; set; } = true;

        // Unsubscribe Token (for email unsubscribe links)
        public string? UnsubscribeToken { get; set; }

        // Metadata
        public DateTime PreferenceLastUpdatedDate { get; set; }
        public string? LastUpdatedByUserId { get; set; }
        public string? UpdateReason { get; set; }
    }

    public enum NotificationFrequency
    {
        Immediate = 0,
        Hourly = 1,
        Daily = 2,
        Weekly = 3,
        Monthly = 4,
        Never = 5
    }
}
