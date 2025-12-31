using DomumBackend.Domain.Enums;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Notification - Stores notification records for users
    /// Tracks all notifications sent to users (email, SMS, in-app)
    /// </summary>
    public class Notification : BaseEntity
    {
        public required string FacilityId { get; set; }
        public Facility? Facility { get; set; }

        // Recipient Information
        public required string RecipientUserId { get; set; }
        public required string RecipientEmail { get; set; }
        public string? RecipientPhoneNumber { get; set; }

        // Notification Details
        public required string Subject { get; set; }
        public required string Message { get; set; }
        public string? HtmlContent { get; set; }
        public NotificationType NotificationType { get; set; } // Email, SMS, InApp
        public NotificationPriority Priority { get; set; } // Low, Medium, High, Critical

        // Trigger Information
        public string? TriggerType { get; set; } // IncidentReport, HealthAlert, SafeguardingConcern, etc.
        public string? TriggerEntityId { get; set; } // ID of the entity that triggered the notification

        // Status Tracking
        public NotificationStatus Status { get; set; } = NotificationStatus.Pending; // Pending, Sent, Failed, Bounced
        public DateTime? SentDate { get; set; }
        public DateTime? ReadDate { get; set; }
        public int RetryCount { get; set; } = 0;
        public string? LastErrorMessage { get; set; }

        // Metadata
        public DateTime CreatedDate_Scheduled { get; set; }
        public DateTime? ScheduledSendDate { get; set; }
        public bool IsScheduled { get; set; } = false;
        public string? Tags { get; set; } // Comma-separated tags for filtering
    }

    public enum NotificationType
    {
        Email = 0,
        SMS = 1,
        InApp = 2,
        PushNotification = 3
    }

    public enum NotificationPriority
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Critical = 3
    }

    public enum NotificationStatus
    {
        Pending = 0,
        Sent = 1,
        Failed = 2,
        Bounced = 3,
        Unsubscribed = 4,
        Draft = 5
    }
}
