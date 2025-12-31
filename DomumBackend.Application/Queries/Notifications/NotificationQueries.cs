using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Queries.Notifications
{
    // ===== NOTIFICATION QUERIES =====

    public class GetUserNotificationsQuery : IRequest<List<NotificationDTO>>
    {
        public required string UserId { get; set; }
        public required string FacilityId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, List<NotificationDTO>>
    {
        private readonly INotificationService _notificationService;

        public GetUserNotificationsQueryHandler(INotificationService notificationService)
            => _notificationService = notificationService;

        public async Task<List<NotificationDTO>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
        {
            return await _notificationService.GetNotificationsByUserAsync(
                request.UserId,
                request.FacilityId,
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
    }

    public class GetUnreadNotificationsQuery : IRequest<List<NotificationDTO>>
    {
        public required string UserId { get; set; }
        public required string FacilityId { get; set; }
    }

    public class GetUnreadNotificationsQueryHandler : IRequestHandler<GetUnreadNotificationsQuery, List<NotificationDTO>>
    {
        private readonly INotificationService _notificationService;

        public GetUnreadNotificationsQueryHandler(INotificationService notificationService)
            => _notificationService = notificationService;

        public async Task<List<NotificationDTO>> Handle(GetUnreadNotificationsQuery request, CancellationToken cancellationToken)
        {
            return await _notificationService.GetUnreadNotificationsAsync(request.UserId, request.FacilityId, cancellationToken);
        }
    }

    // ===== ALERT RULE QUERIES =====

    public class GetAlertRulesByFacilityQuery : IRequest<List<AlertRuleDTO>>
    {
        public required string FacilityId { get; set; }
        public bool ActiveOnly { get; set; } = true;
    }

    public class GetAlertRulesByFacilityQueryHandler : IRequestHandler<GetAlertRulesByFacilityQuery, List<AlertRuleDTO>>
    {
        private readonly IAlertRuleService _alertRuleService;

        public GetAlertRulesByFacilityQueryHandler(IAlertRuleService alertRuleService)
            => _alertRuleService = alertRuleService;

        public async Task<List<AlertRuleDTO>> Handle(GetAlertRulesByFacilityQuery request, CancellationToken cancellationToken)
        {
            return await _alertRuleService.GetAlertRulesByFacilityAsync(request.FacilityId, request.ActiveOnly, cancellationToken);
        }
    }

    public class GetAlertRuleByIdQuery : IRequest<AlertRuleDTO>
    {
        public required long AlertRuleId { get; set; }
    }

    public class GetAlertRuleByIdQueryHandler : IRequestHandler<GetAlertRuleByIdQuery, AlertRuleDTO>
    {
        private readonly IAlertRuleService _alertRuleService;

        public GetAlertRuleByIdQueryHandler(IAlertRuleService alertRuleService)
            => _alertRuleService = alertRuleService;

        public async Task<AlertRuleDTO> Handle(GetAlertRuleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _alertRuleService.GetAlertRuleByIdAsync(request.AlertRuleId, cancellationToken);
        }
    }

    // ===== NOTIFICATION PREFERENCE QUERIES =====

    public class GetUserPreferencesQuery : IRequest<NotificationPreferenceDTO>
    {
        public required string UserId { get; set; }
        public required string FacilityId { get; set; }
    }

    public class GetUserPreferencesQueryHandler : IRequestHandler<GetUserPreferencesQuery, NotificationPreferenceDTO>
    {
        private readonly INotificationPreferenceService _preferenceService;

        public GetUserPreferencesQueryHandler(INotificationPreferenceService preferenceService)
            => _preferenceService = preferenceService;

        public async Task<NotificationPreferenceDTO> Handle(GetUserPreferencesQuery request, CancellationToken cancellationToken)
        {
            return await _preferenceService.GetUserPreferencesAsync(request.UserId, request.FacilityId, cancellationToken);
        }
    }

    // ===== NOTIFICATION TEMPLATE QUERIES =====

    public class GetTemplateByKeyQuery : IRequest<NotificationTemplateDTO>
    {
        public required string FacilityId { get; set; }
        public required string TemplateKey { get; set; }
    }

    public class GetTemplateByKeyQueryHandler : IRequestHandler<GetTemplateByKeyQuery, NotificationTemplateDTO>
    {
        private readonly INotificationTemplateService _templateService;

        public GetTemplateByKeyQueryHandler(INotificationTemplateService templateService)
            => _templateService = templateService;

        public async Task<NotificationTemplateDTO> Handle(GetTemplateByKeyQuery request, CancellationToken cancellationToken)
        {
            return await _templateService.GetTemplateByKeyAsync(request.FacilityId, request.TemplateKey, cancellationToken);
        }
    }

    public class GetTemplatesByFacilityQuery : IRequest<List<NotificationTemplateDTO>>
    {
        public required string FacilityId { get; set; }
        public string? Category { get; set; }
    }

    public class GetTemplatesByFacilityQueryHandler : IRequestHandler<GetTemplatesByFacilityQuery, List<NotificationTemplateDTO>>
    {
        private readonly INotificationTemplateService _templateService;

        public GetTemplatesByFacilityQueryHandler(INotificationTemplateService templateService)
            => _templateService = templateService;

        public async Task<List<NotificationTemplateDTO>> Handle(GetTemplatesByFacilityQuery request, CancellationToken cancellationToken)
        {
            return await _templateService.GetTemplatesByFacilityAsync(request.FacilityId, request.Category, cancellationToken);
        }
    }
}
