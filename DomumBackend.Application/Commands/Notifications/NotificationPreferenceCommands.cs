using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Commands.Notifications
{
    // ===== NOTIFICATION PREFERENCE COMMANDS =====

    public class UpdateNotificationPreferencesCommand : IRequest<NotificationPreferenceDTO>
    {
        public required string UserId { get; set; }
        public required string FacilityId { get; set; }
        public NotificationPreferenceDTO? Preferences { get; set; }
        public required string UpdatedByUserId { get; set; }
    }

    public class UpdateNotificationPreferencesCommandHandler : IRequestHandler<UpdateNotificationPreferencesCommand, NotificationPreferenceDTO>
    {
        private readonly INotificationPreferenceService _preferenceService;

        public UpdateNotificationPreferencesCommandHandler(INotificationPreferenceService preferenceService)
            => _preferenceService = preferenceService;

        public async Task<NotificationPreferenceDTO> Handle(UpdateNotificationPreferencesCommand request, CancellationToken cancellationToken)
        {
            return await _preferenceService.UpdateUserPreferencesAsync(
                request.UserId,
                request.FacilityId,
                request.Preferences ?? new NotificationPreferenceDTO { UserId = request.UserId, FacilityId = request.FacilityId },
                request.UpdatedByUserId,
                cancellationToken);
        }
    }

    public class SetEmailNotificationsCommand : IRequest<bool>
    {
        public required string UserId { get; set; }
        public required string FacilityId { get; set; }
        public bool Enabled { get; set; }
        public string? EmailAddress { get; set; }
    }

    public class SetEmailNotificationsCommandHandler : IRequestHandler<SetEmailNotificationsCommand, bool>
    {
        private readonly INotificationPreferenceService _preferenceService;

        public SetEmailNotificationsCommandHandler(INotificationPreferenceService preferenceService)
            => _preferenceService = preferenceService;

        public async Task<bool> Handle(SetEmailNotificationsCommand request, CancellationToken cancellationToken)
        {
            return await _preferenceService.SetEmailNotificationsAsync(
                request.UserId,
                request.FacilityId,
                request.Enabled,
                request.EmailAddress,
                cancellationToken);
        }
    }

    public class SetSMSNotificationsCommand : IRequest<bool>
    {
        public required string UserId { get; set; }
        public required string FacilityId { get; set; }
        public bool Enabled { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class SetSMSNotificationsCommandHandler : IRequestHandler<SetSMSNotificationsCommand, bool>
    {
        private readonly INotificationPreferenceService _preferenceService;

        public SetSMSNotificationsCommandHandler(INotificationPreferenceService preferenceService)
            => _preferenceService = preferenceService;

        public async Task<bool> Handle(SetSMSNotificationsCommand request, CancellationToken cancellationToken)
        {
            return await _preferenceService.SetSMSNotificationsAsync(
                request.UserId,
                request.FacilityId,
                request.Enabled,
                request.PhoneNumber,
                cancellationToken);
        }
    }

    public class UnsubscribeFromCategoryCommand : IRequest<bool>
    {
        public required string UserId { get; set; }
        public required string FacilityId { get; set; }
        public required string Category { get; set; }
    }

    public class UnsubscribeFromCategoryCommandHandler : IRequestHandler<UnsubscribeFromCategoryCommand, bool>
    {
        private readonly INotificationPreferenceService _preferenceService;

        public UnsubscribeFromCategoryCommandHandler(INotificationPreferenceService preferenceService)
            => _preferenceService = preferenceService;

        public async Task<bool> Handle(UnsubscribeFromCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _preferenceService.UnsubscribeFromCategoryAsync(
                request.UserId,
                request.FacilityId,
                request.Category,
                cancellationToken);
        }
    }

    public class UnsubscribeFromAllCommand : IRequest<bool>
    {
        public required string UserId { get; set; }
        public required string UnsubscribeToken { get; set; }
    }

    public class UnsubscribeFromAllCommandHandler : IRequestHandler<UnsubscribeFromAllCommand, bool>
    {
        private readonly INotificationPreferenceService _preferenceService;

        public UnsubscribeFromAllCommandHandler(INotificationPreferenceService preferenceService)
            => _preferenceService = preferenceService;

        public async Task<bool> Handle(UnsubscribeFromAllCommand request, CancellationToken cancellationToken)
        {
            return await _preferenceService.UnsubscribeFromAllAsync(request.UserId, request.UnsubscribeToken, cancellationToken);
        }
    }
}
