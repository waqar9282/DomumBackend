using DomumBackend.Domain.Entities;

namespace DomumBackend.Application.DTOs
{
    public class NotificationDTO
    {
        public long Id { get; set; }
        public required string FacilityId { get; set; }
        public required string RecipientUserId { get; set; }
        public required string Subject { get; set; }
        public required string Message { get; set; }
        public NotificationType NotificationType { get; set; }
        public NotificationPriority Priority { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime? SentDate { get; set; }
        public DateTime? ReadDate { get; set; }
        public int RetryCount { get; set; }
    }

    public class AlertRuleDTO
    {
        public long Id { get; set; }
        public required string FacilityId { get; set; }
        public required string RuleName { get; set; }
        public required string AlertType { get; set; }
        public bool IsActive { get; set; }
        public int ThresholdValue { get; set; }
        public int TimeframeMinutes { get; set; }
        public required string NotificationChannels { get; set; }
        public int TimesTriggered { get; set; }
        public DateTime? LastTriggeredDate { get; set; }
    }

    public class NotificationPreferenceDTO
    {
        public long Id { get; set; }
        public required string UserId { get; set; }
        public required string FacilityId { get; set; }
        public bool EmailNotificationsEnabled { get; set; }
        public string? EmailAddress { get; set; }
        public bool SMSNotificationsEnabled { get; set; }
        public string? PhoneNumber { get; set; }
        public bool InAppNotificationsEnabled { get; set; }
        public NotificationFrequency HighPriorityFrequency { get; set; }
        public bool QuietHoursEnabled { get; set; }
        public TimeSpan? QuietHoursStart { get; set; }
        public TimeSpan? QuietHoursEnd { get; set; }
    }

    public class NotificationTemplateDTO
    {
        public long Id { get; set; }
        public required string TemplateName { get; set; }
        public required string TemplateKey { get; set; }
        public required string Category { get; set; }
        public required string NotificationChannel { get; set; }
        public bool IsActive { get; set; }
        public int VersionNumber { get; set; }
        public bool IsDefault { get; set; }
        public int TimesUsed { get; set; }
    }

    public class NotificationResponseDTO
    {
        public long NotificationId { get; set; }
        public NotificationStatus Status { get; set; }
        public string? Message { get; set; }
        public DateTime? SentDate { get; set; }
        public int RetryCount { get; set; }
        public bool Success { get; set; }
    }

    public class BulkNotificationResponseDTO
    {
        public int TotalRequested { get; set; }
        public int SuccessfullySent { get; set; }
        public int Failed { get; set; }
        public List<NotificationResponseDTO>? Results { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class AlertTriggeredResponseDTO
    {
        public long AlertRuleId { get; set; }
        public required string AlertType { get; set; }
        public int NotificationsSent { get; set; }
        public List<string>? RecipientUserIds { get; set; }
        public DateTime TriggeredDate { get; set; }
        public string? TriggerDetails { get; set; }
    }
}
