using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using MediatR;

namespace DomumBackend.Application.Commands.Notifications
{
    // ===== NOTIFICATION COMMANDS =====

    public class SendNotificationCommand : IRequest<NotificationResponseDTO>
    {
        public required string FacilityId { get; set; }
        public required string RecipientUserId { get; set; }
        public required string Subject { get; set; }
        public required string Message { get; set; }
        public NotificationType NotificationType { get; set; }
        public NotificationPriority Priority { get; set; } = NotificationPriority.Medium;
        public string? TriggerType { get; set; }
        public string? TriggerEntityId { get; set; }
    }

    public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, NotificationResponseDTO>
    {
        private readonly INotificationService _notificationService;

        public SendNotificationCommandHandler(INotificationService notificationService)
            => _notificationService = notificationService;

        public async Task<NotificationResponseDTO> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.SendNotificationAsync(
                request.FacilityId,
                request.RecipientUserId,
                request.Subject,
                request.Message,
                request.NotificationType,
                request.Priority,
                request.TriggerType,
                request.TriggerEntityId,
                cancellationToken);
        }
    }

    public class SendBulkNotificationsCommand : IRequest<BulkNotificationResponseDTO>
    {
        public required string FacilityId { get; set; }
        public required List<string> RecipientUserIds { get; set; }
        public required string Subject { get; set; }
        public required string Message { get; set; }
        public NotificationType NotificationType { get; set; }
        public NotificationPriority Priority { get; set; } = NotificationPriority.Medium;
    }

    public class SendBulkNotificationsCommandHandler : IRequestHandler<SendBulkNotificationsCommand, BulkNotificationResponseDTO>
    {
        private readonly INotificationService _notificationService;

        public SendBulkNotificationsCommandHandler(INotificationService notificationService)
            => _notificationService = notificationService;

        public async Task<BulkNotificationResponseDTO> Handle(SendBulkNotificationsCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.SendBulkNotificationsAsync(
                request.FacilityId,
                request.RecipientUserIds,
                request.Subject,
                request.Message,
                request.NotificationType,
                request.Priority,
                cancellationToken);
        }
    }

    public class ScheduleNotificationCommand : IRequest<NotificationResponseDTO>
    {
        public required long NotificationId { get; set; }
        public required DateTime ScheduledDate { get; set; }
    }

    public class ScheduleNotificationCommandHandler : IRequestHandler<ScheduleNotificationCommand, NotificationResponseDTO>
    {
        private readonly INotificationService _notificationService;

        public ScheduleNotificationCommandHandler(INotificationService notificationService)
            => _notificationService = notificationService;

        public async Task<NotificationResponseDTO> Handle(ScheduleNotificationCommand request, CancellationToken cancellationToken)
        {
            var success = await _notificationService.SendScheduledNotificationAsync(
                request.NotificationId,
                request.ScheduledDate,
                cancellationToken);

            return new NotificationResponseDTO
            {
                NotificationId = request.NotificationId,
                Success = success,
                Status = success ? NotificationStatus.Pending : NotificationStatus.Failed
            };
        }
    }

    public class MarkNotificationAsReadCommand : IRequest<bool>
    {
        public required long NotificationId { get; set; }
    }

    public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, bool>
    {
        private readonly INotificationService _notificationService;

        public MarkNotificationAsReadCommandHandler(INotificationService notificationService)
            => _notificationService = notificationService;

        public async Task<bool> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.MarkNotificationAsReadAsync(request.NotificationId, cancellationToken);
        }
    }

    public class MarkAllNotificationsAsReadCommand : IRequest<bool>
    {
        public required string UserId { get; set; }
        public required string FacilityId { get; set; }
    }

    public class MarkAllNotificationsAsReadCommandHandler : IRequestHandler<MarkAllNotificationsAsReadCommand, bool>
    {
        private readonly INotificationService _notificationService;

        public MarkAllNotificationsAsReadCommandHandler(INotificationService notificationService)
            => _notificationService = notificationService;

        public async Task<bool> Handle(MarkAllNotificationsAsReadCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.MarkAllNotificationsAsReadAsync(request.UserId, request.FacilityId, cancellationToken);
        }
    }

    public class DeleteNotificationCommand : IRequest<bool>
    {
        public required long NotificationId { get; set; }
    }

    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly INotificationService _notificationService;

        public DeleteNotificationCommandHandler(INotificationService notificationService)
            => _notificationService = notificationService;

        public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.DeleteNotificationAsync(request.NotificationId, cancellationToken);
        }
    }

    public class RetryFailedNotificationsCommand : IRequest<int>
    {
        public required string FacilityId { get; set; }
        public int MaxRetries { get; set; } = 3;
    }

    public class RetryFailedNotificationsCommandHandler : IRequestHandler<RetryFailedNotificationsCommand, int>
    {
        private readonly INotificationService _notificationService;

        public RetryFailedNotificationsCommandHandler(INotificationService notificationService)
            => _notificationService = notificationService;

        public async Task<int> Handle(RetryFailedNotificationsCommand request, CancellationToken cancellationToken)
        {
            return await _notificationService.RetryFailedNotificationsAsync(request.FacilityId, request.MaxRetries, cancellationToken);
        }
    }
}
