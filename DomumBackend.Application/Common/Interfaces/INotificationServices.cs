using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;

namespace DomumBackend.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResponseDTO> SendNotificationAsync(
            string facilityId,
            string recipientUserId,
            string subject,
            string message,
            NotificationType notificationType,
            NotificationPriority priority = NotificationPriority.Medium,
            string? triggerType = null,
            string? triggerEntityId = null,
            CancellationToken cancellationToken = default);

        Task<BulkNotificationResponseDTO> SendBulkNotificationsAsync(
            string facilityId,
            List<string> recipientUserIds,
            string subject,
            string message,
            NotificationType notificationType,
            NotificationPriority priority = NotificationPriority.Medium,
            CancellationToken cancellationToken = default);

        Task<bool> SendScheduledNotificationAsync(
            long notificationId,
            DateTime scheduledDate,
            CancellationToken cancellationToken = default);

        Task<List<NotificationDTO>> GetNotificationsByUserAsync(
            string userId,
            string facilityId,
            int pageNumber = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default);

        Task<List<NotificationDTO>> GetUnreadNotificationsAsync(
            string userId,
            string facilityId,
            CancellationToken cancellationToken = default);

        Task<bool> MarkNotificationAsReadAsync(
            long notificationId,
            CancellationToken cancellationToken = default);

        Task<bool> MarkAllNotificationsAsReadAsync(
            string userId,
            string facilityId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteNotificationAsync(
            long notificationId,
            CancellationToken cancellationToken = default);

        Task<int> RetryFailedNotificationsAsync(
            string facilityId,
            int maxRetries = 3,
            CancellationToken cancellationToken = default);
    }

    public interface IAlertRuleService
    {
        Task<AlertRuleDTO> CreateAlertRuleAsync(
            string facilityId,
            string ruleName,
            string alertType,
            string triggerCondition,
            int thresholdValue,
            int timeframeMinutes,
            string recipientRoles,
            string notificationChannels,
            string createdByUserId,
            CancellationToken cancellationToken = default);

        Task<AlertRuleDTO> UpdateAlertRuleAsync(
            long alertRuleId,
            string ruleName,
            string triggerCondition,
            int thresholdValue,
            int timeframeMinutes,
            bool isActive,
            string updatedByUserId,
            CancellationToken cancellationToken = default);

        Task<List<AlertRuleDTO>> GetAlertRulesByFacilityAsync(
            string facilityId,
            bool activeOnly = true,
            CancellationToken cancellationToken = default);

        Task<AlertRuleDTO> GetAlertRuleByIdAsync(
            long alertRuleId,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAlertRuleAsync(
            long alertRuleId,
            CancellationToken cancellationToken = default);

        Task<AlertTriggeredResponseDTO> TriggerAlertRuleAsync(
            long alertRuleId,
            string? triggerEntityId = null,
            string? triggerDetails = null,
            CancellationToken cancellationToken = default);

        Task<bool> SuspendAlertRuleAsync(
            long alertRuleId,
            DateTime suspensionUntilDate,
            CancellationToken cancellationToken = default);

        Task<List<AlertRuleDTO>> CheckAndTriggerApplicableAlertsAsync(
            string facilityId,
            string triggerType,
            string triggerEntityId,
            CancellationToken cancellationToken = default);
    }

    public interface INotificationPreferenceService
    {
        Task<NotificationPreferenceDTO> GetUserPreferencesAsync(
            string userId,
            string facilityId,
            CancellationToken cancellationToken = default);

        Task<NotificationPreferenceDTO> UpdateUserPreferencesAsync(
            string userId,
            string facilityId,
            NotificationPreferenceDTO preferences,
            string updatedByUserId,
            CancellationToken cancellationToken = default);

        Task<bool> SetEmailNotificationsAsync(
            string userId,
            string facilityId,
            bool enabled,
            string? emailAddress = null,
            CancellationToken cancellationToken = default);

        Task<bool> SetSMSNotificationsAsync(
            string userId,
            string facilityId,
            bool enabled,
            string? phoneNumber = null,
            CancellationToken cancellationToken = default);

        Task<bool> UnsubscribeFromCategoryAsync(
            string userId,
            string facilityId,
            string category,
            CancellationToken cancellationToken = default);

        Task<bool> UnsubscribeFromAllAsync(
            string userId,
            string unsubscribeToken,
            CancellationToken cancellationToken = default);

        Task<NotificationPreferenceDTO> CreateDefaultPreferencesAsync(
            string userId,
            string facilityId,
            CancellationToken cancellationToken = default);

        Task<List<string>> GetRecipientsForAlertAsync(
            string facilityId,
            string alertType,
            NotificationFrequency minimumFrequency = NotificationFrequency.Immediate,
            CancellationToken cancellationToken = default);
    }

    public interface INotificationTemplateService
    {
        Task<NotificationTemplateDTO> CreateTemplateAsync(
            string facilityId,
            string templateName,
            string templateKey,
            string category,
            string notificationChannel,
            string? emailSubject = null,
            string? emailBody = null,
            string? emailHtml = null,
            string? smsBody = null,
            string? supportedVariables = null,
            string? createdByUserId = null,
            CancellationToken cancellationToken = default);

        Task<NotificationTemplateDTO> UpdateTemplateAsync(
            long templateId,
            string templateName,
            string templateKey,
            string? emailSubject = null,
            string? emailBody = null,
            string? emailHtml = null,
            bool isActive = true,
            string? updatedByUserId = null,
            CancellationToken cancellationToken = default);

        Task<NotificationTemplateDTO> GetTemplateByKeyAsync(
            string facilityId,
            string templateKey,
            CancellationToken cancellationToken = default);

        Task<List<NotificationTemplateDTO>> GetTemplatesByFacilityAsync(
            string facilityId,
            string? category = null,
            CancellationToken cancellationToken = default);

        Task<string> RenderTemplateAsync(
            long templateId,
            Dictionary<string, string> variables,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteTemplateAsync(
            long templateId,
            CancellationToken cancellationToken = default);

        Task<bool> SetDefaultTemplateAsync(
            long templateId,
            CancellationToken cancellationToken = default);

        Task<NotificationTemplateDTO> CreateVersionAsync(
            long originalTemplateId,
            string modifiedContent,
            string updatedByUserId,
            CancellationToken cancellationToken = default);
    }
}
