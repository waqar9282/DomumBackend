using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using MediatR;

namespace DomumBackend.Application.Commands.Notifications
{
    // ===== NOTIFICATION TEMPLATE COMMANDS =====

    public class CreateNotificationTemplateCommand : IRequest<NotificationTemplateDTO>
    {
        public required string FacilityId { get; set; }
        public required string TemplateName { get; set; }
        public required string TemplateKey { get; set; }
        public required string Category { get; set; }
        public required string NotificationChannel { get; set; }
        public string? EmailSubjectTemplate { get; set; }
        public string? EmailBodyTemplate { get; set; }
        public string? EmailHtmlTemplate { get; set; }
        public string? SMSBodyTemplate { get; set; }
        public string? SupportedVariables { get; set; }
        public required string CreatedByUserId { get; set; }
    }

    public class CreateNotificationTemplateCommandHandler : IRequestHandler<CreateNotificationTemplateCommand, NotificationTemplateDTO>
    {
        private readonly INotificationTemplateService _templateService;

        public CreateNotificationTemplateCommandHandler(INotificationTemplateService templateService)
            => _templateService = templateService;

        public async Task<NotificationTemplateDTO> Handle(CreateNotificationTemplateCommand request, CancellationToken cancellationToken)
        {
            return await _templateService.CreateTemplateAsync(
                request.FacilityId,
                request.TemplateName,
                request.TemplateKey,
                request.Category,
                request.NotificationChannel,
                request.EmailSubjectTemplate,
                request.EmailBodyTemplate,
                request.EmailHtmlTemplate,
                request.SMSBodyTemplate,
                request.SupportedVariables,
                request.CreatedByUserId,
                cancellationToken);
        }
    }

    public class UpdateNotificationTemplateCommand : IRequest<NotificationTemplateDTO>
    {
        public required long TemplateId { get; set; }
        public required string TemplateName { get; set; }
        public required string TemplateKey { get; set; }
        public string? EmailSubjectTemplate { get; set; }
        public string? EmailBodyTemplate { get; set; }
        public string? EmailHtmlTemplate { get; set; }
        public bool IsActive { get; set; }
        public required string UpdatedByUserId { get; set; }
    }

    public class UpdateNotificationTemplateCommandHandler : IRequestHandler<UpdateNotificationTemplateCommand, NotificationTemplateDTO>
    {
        private readonly INotificationTemplateService _templateService;

        public UpdateNotificationTemplateCommandHandler(INotificationTemplateService templateService)
            => _templateService = templateService;

        public async Task<NotificationTemplateDTO> Handle(UpdateNotificationTemplateCommand request, CancellationToken cancellationToken)
        {
            return await _templateService.UpdateTemplateAsync(
                request.TemplateId,
                request.TemplateName,
                request.TemplateKey,
                request.EmailSubjectTemplate,
                request.EmailBodyTemplate,
                request.EmailHtmlTemplate,
                request.IsActive,
                request.UpdatedByUserId,
                cancellationToken);
        }
    }

    public class DeleteNotificationTemplateCommand : IRequest<bool>
    {
        public required long TemplateId { get; set; }
    }

    public class DeleteNotificationTemplateCommandHandler : IRequestHandler<DeleteNotificationTemplateCommand, bool>
    {
        private readonly INotificationTemplateService _templateService;

        public DeleteNotificationTemplateCommandHandler(INotificationTemplateService templateService)
            => _templateService = templateService;

        public async Task<bool> Handle(DeleteNotificationTemplateCommand request, CancellationToken cancellationToken)
        {
            return await _templateService.DeleteTemplateAsync(request.TemplateId, cancellationToken);
        }
    }

    public class SetDefaultNotificationTemplateCommand : IRequest<bool>
    {
        public required long TemplateId { get; set; }
    }

    public class SetDefaultNotificationTemplateCommandHandler : IRequestHandler<SetDefaultNotificationTemplateCommand, bool>
    {
        private readonly INotificationTemplateService _templateService;

        public SetDefaultNotificationTemplateCommandHandler(INotificationTemplateService templateService)
            => _templateService = templateService;

        public async Task<bool> Handle(SetDefaultNotificationTemplateCommand request, CancellationToken cancellationToken)
        {
            return await _templateService.SetDefaultTemplateAsync(request.TemplateId, cancellationToken);
        }
    }

    public class CreateTemplateVersionCommand : IRequest<NotificationTemplateDTO>
    {
        public required long OriginalTemplateId { get; set; }
        public required string ModifiedContent { get; set; }
        public required string UpdatedByUserId { get; set; }
    }

    public class CreateTemplateVersionCommandHandler : IRequestHandler<CreateTemplateVersionCommand, NotificationTemplateDTO>
    {
        private readonly INotificationTemplateService _templateService;

        public CreateTemplateVersionCommandHandler(INotificationTemplateService templateService)
            => _templateService = templateService;

        public async Task<NotificationTemplateDTO> Handle(CreateTemplateVersionCommand request, CancellationToken cancellationToken)
        {
            return await _templateService.CreateVersionAsync(
                request.OriginalTemplateId,
                request.ModifiedContent,
                request.UpdatedByUserId,
                cancellationToken);
        }
    }
}
