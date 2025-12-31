#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
            => _context = context;

        public async Task<NotificationResponseDTO> SendNotificationAsync(
            string facilityId,
            string recipientUserId,
            string subject,
            string message,
            NotificationType notificationType,
            NotificationPriority priority = NotificationPriority.Medium,
            string triggerType = null,
            string triggerEntityId = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var notification = new Notification
                {
                    FacilityId = facilityId,
                    RecipientUserId = recipientUserId,
                    RecipientEmail = recipientUserId, // Will be replaced with actual email lookup
                    Subject = subject,
                    Message = message,
                    NotificationType = notificationType,
                    Priority = priority,
                    TriggerType = triggerType,
                    TriggerEntityId = triggerEntityId,
                    Status = NotificationStatus.Sent,
                    SentDate = DateTime.UtcNow,
                    CreatedDate_Scheduled = DateTime.UtcNow
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync(cancellationToken);

                return new NotificationResponseDTO
                {
                    NotificationId = notification.Id,
                    Status = NotificationStatus.Sent,
                    Message = "Notification sent successfully",
                    SentDate = notification.SentDate,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new NotificationResponseDTO
                {
                    Success = false,
                    Status = NotificationStatus.Failed,
                    Message = ex.Message
                };
            }
        }

        public async Task<BulkNotificationResponseDTO> SendBulkNotificationsAsync(
            string facilityId,
            List<string> recipientUserIds,
            string subject,
            string message,
            NotificationType notificationType,
            NotificationPriority priority = NotificationPriority.Medium,
            CancellationToken cancellationToken = default)
        {
            var results = new List<NotificationResponseDTO>();
            int successCount = 0;

            foreach (var userId in recipientUserIds)
            {
                var result = await SendNotificationAsync(
                    facilityId,
                    userId,
                    subject,
                    message,
                    notificationType,
                    priority,
                    cancellationToken: cancellationToken);

                results.Add(result);
                if (result.Success) successCount++;
            }

            return new BulkNotificationResponseDTO
            {
                TotalRequested = recipientUserIds.Count,
                SuccessfullySent = successCount,
                Failed = recipientUserIds.Count - successCount,
                Results = results
            };
        }

        public async Task<bool> SendScheduledNotificationAsync(
            long notificationId,
            DateTime scheduledDate,
            CancellationToken cancellationToken = default)
        {
            var notification = await _context.Notifications.FindAsync(new object[] { notificationId }, cancellationToken);
            if (notification == null)
                return false;

            notification.ScheduledSendDate = scheduledDate;
            notification.IsScheduled = true;
            notification.Status = NotificationStatus.Pending;

            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<NotificationDTO>> GetNotificationsByUserAsync(
            string userId,
            string facilityId,
            int pageNumber = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var notifications = await _context.Notifications
                .Where(n => n.RecipientUserId == userId && n.FacilityId == facilityId)
                .OrderByDescending(n => n.CreatedDate_Scheduled)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return notifications.Select(n => new NotificationDTO
            {
                Id = n.Id,
                FacilityId = n.FacilityId,
                RecipientUserId = n.RecipientUserId,
                Subject = n.Subject,
                Message = n.Message,
                NotificationType = n.NotificationType,
                Priority = n.Priority,
                Status = n.Status,
                SentDate = n.SentDate,
                ReadDate = n.ReadDate,
                RetryCount = n.RetryCount
            }).ToList();
        }

        public async Task<List<NotificationDTO>> GetUnreadNotificationsAsync(
            string userId,
            string facilityId,
            CancellationToken cancellationToken = default)
        {
            var notifications = await _context.Notifications
                .Where(n => n.RecipientUserId == userId && 
                           n.FacilityId == facilityId &&
                           n.ReadDate == null &&
                           n.Status == NotificationStatus.Sent)
                .OrderByDescending(n => n.SentDate)
                .ToListAsync(cancellationToken);

            return notifications.Select(n => new NotificationDTO
            {
                Id = n.Id,
                FacilityId = n.FacilityId,
                RecipientUserId = n.RecipientUserId,
                Subject = n.Subject,
                Message = n.Message,
                NotificationType = n.NotificationType,
                Priority = n.Priority,
                Status = n.Status,
                SentDate = n.SentDate,
                ReadDate = n.ReadDate,
                RetryCount = n.RetryCount
            }).ToList();
        }

        public async Task<bool> MarkNotificationAsReadAsync(
            long notificationId,
            CancellationToken cancellationToken = default)
        {
            var notification = await _context.Notifications.FindAsync(new object[] { notificationId }, cancellationToken);
            if (notification == null)
                return false;

            notification.ReadDate = DateTime.UtcNow;
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> MarkAllNotificationsAsReadAsync(
            string userId,
            string facilityId,
            CancellationToken cancellationToken = default)
        {
            var notifications = await _context.Notifications
                .Where(n => n.RecipientUserId == userId && 
                           n.FacilityId == facilityId &&
                           n.ReadDate == null)
                .ToListAsync(cancellationToken);

            foreach (var notification in notifications)
            {
                notification.ReadDate = DateTime.UtcNow;
            }

            _context.Notifications.UpdateRange(notifications);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteNotificationAsync(
            long notificationId,
            CancellationToken cancellationToken = default)
        {
            var notification = await _context.Notifications.FindAsync(new object[] { notificationId }, cancellationToken);
            if (notification == null)
                return false;

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<int> RetryFailedNotificationsAsync(
            string facilityId,
            int maxRetries = 3,
            CancellationToken cancellationToken = default)
        {
            var failedNotifications = await _context.Notifications
                .Where(n => n.FacilityId == facilityId &&
                           n.Status == NotificationStatus.Failed &&
                           n.RetryCount < maxRetries)
                .ToListAsync(cancellationToken);

            foreach (var notification in failedNotifications)
            {
                notification.RetryCount++;
                notification.Status = NotificationStatus.Sent;
                notification.SentDate = DateTime.UtcNow;
                notification.LastErrorMessage = null;
            }

            _context.Notifications.UpdateRange(failedNotifications);
            await _context.SaveChangesAsync(cancellationToken);
            return failedNotifications.Count;
        }
    }
}
