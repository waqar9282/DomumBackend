using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Commands.Notifications
{
    // ===== ALERT RULE COMMANDS =====

    public class CreateAlertRuleCommand : IRequest<AlertRuleDTO>
    {
        public required string FacilityId { get; set; }
        public required string RuleName { get; set; }
        public required string AlertType { get; set; }
        public required string TriggerCondition { get; set; }
        public int ThresholdValue { get; set; }
        public int TimeframeMinutes { get; set; }
        public required string RecipientRoles { get; set; }
        public required string NotificationChannels { get; set; }
        public required string CreatedByUserId { get; set; }
    }

    public class CreateAlertRuleCommandHandler : IRequestHandler<CreateAlertRuleCommand, AlertRuleDTO>
    {
        private readonly IAlertRuleService _alertRuleService;

        public CreateAlertRuleCommandHandler(IAlertRuleService alertRuleService)
            => _alertRuleService = alertRuleService;

        public async Task<AlertRuleDTO> Handle(CreateAlertRuleCommand request, CancellationToken cancellationToken)
        {
            return await _alertRuleService.CreateAlertRuleAsync(
                request.FacilityId,
                request.RuleName,
                request.AlertType,
                request.TriggerCondition,
                request.ThresholdValue,
                request.TimeframeMinutes,
                request.RecipientRoles,
                request.NotificationChannels,
                request.CreatedByUserId,
                cancellationToken);
        }
    }

    public class UpdateAlertRuleCommand : IRequest<AlertRuleDTO>
    {
        public required long AlertRuleId { get; set; }
        public required string RuleName { get; set; }
        public required string TriggerCondition { get; set; }
        public int ThresholdValue { get; set; }
        public int TimeframeMinutes { get; set; }
        public bool IsActive { get; set; }
        public required string UpdatedByUserId { get; set; }
    }

    public class UpdateAlertRuleCommandHandler : IRequestHandler<UpdateAlertRuleCommand, AlertRuleDTO>
    {
        private readonly IAlertRuleService _alertRuleService;

        public UpdateAlertRuleCommandHandler(IAlertRuleService alertRuleService)
            => _alertRuleService = alertRuleService;

        public async Task<AlertRuleDTO> Handle(UpdateAlertRuleCommand request, CancellationToken cancellationToken)
        {
            return await _alertRuleService.UpdateAlertRuleAsync(
                request.AlertRuleId,
                request.RuleName,
                request.TriggerCondition,
                request.ThresholdValue,
                request.TimeframeMinutes,
                request.IsActive,
                request.UpdatedByUserId,
                cancellationToken);
        }
    }

    public class DeleteAlertRuleCommand : IRequest<bool>
    {
        public required long AlertRuleId { get; set; }
    }

    public class DeleteAlertRuleCommandHandler : IRequestHandler<DeleteAlertRuleCommand, bool>
    {
        private readonly IAlertRuleService _alertRuleService;

        public DeleteAlertRuleCommandHandler(IAlertRuleService alertRuleService)
            => _alertRuleService = alertRuleService;

        public async Task<bool> Handle(DeleteAlertRuleCommand request, CancellationToken cancellationToken)
        {
            return await _alertRuleService.DeleteAlertRuleAsync(request.AlertRuleId, cancellationToken);
        }
    }

    public class TriggerAlertRuleCommand : IRequest<AlertTriggeredResponseDTO>
    {
        public required long AlertRuleId { get; set; }
        public string? TriggerEntityId { get; set; }
        public string? TriggerDetails { get; set; }
    }

    public class TriggerAlertRuleCommandHandler : IRequestHandler<TriggerAlertRuleCommand, AlertTriggeredResponseDTO>
    {
        private readonly IAlertRuleService _alertRuleService;

        public TriggerAlertRuleCommandHandler(IAlertRuleService alertRuleService)
            => _alertRuleService = alertRuleService;

        public async Task<AlertTriggeredResponseDTO> Handle(TriggerAlertRuleCommand request, CancellationToken cancellationToken)
        {
            return await _alertRuleService.TriggerAlertRuleAsync(
                request.AlertRuleId,
                request.TriggerEntityId,
                request.TriggerDetails,
                cancellationToken);
        }
    }

    public class SuspendAlertRuleCommand : IRequest<bool>
    {
        public required long AlertRuleId { get; set; }
        public required DateTime SuspensionUntilDate { get; set; }
    }

    public class SuspendAlertRuleCommandHandler : IRequestHandler<SuspendAlertRuleCommand, bool>
    {
        private readonly IAlertRuleService _alertRuleService;

        public SuspendAlertRuleCommandHandler(IAlertRuleService alertRuleService)
            => _alertRuleService = alertRuleService;

        public async Task<bool> Handle(SuspendAlertRuleCommand request, CancellationToken cancellationToken)
        {
            return await _alertRuleService.SuspendAlertRuleAsync(request.AlertRuleId, request.SuspensionUntilDate, cancellationToken);
        }
    }
}
