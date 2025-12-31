using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Domain.Enums;
using MediatR;

namespace DomumBackend.Application.Commands.Incident
{
    /// <summary>
    /// Command to update an existing incident report
    /// </summary>
    public class UpdateIncidentCommand : IRequest<int>
    {
        public required string Id { get; set; }
        public required string Description { get; set; }
        public IncidentSeverity Severity { get; set; }
        public string? InjuriesDescription { get; set; }
        public string? TreatmentProvided { get; set; }
        public bool MedicalAttentionRequired { get; set; }
        public string? MedicalTreatmentLocation { get; set; }
        public string? RootCause { get; set; }
        public string? CorrectiveActions { get; set; }
        public bool IsNotifiable { get; set; }
        public string? NotifiedAuthority { get; set; }
    }

    public class UpdateIncidentCommandHandler : IRequestHandler<UpdateIncidentCommand, int>
    {
        private readonly IIncidentService _incidentService;

        public UpdateIncidentCommandHandler(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        public async Task<int> Handle(UpdateIncidentCommand request, CancellationToken cancellationToken)
        {
            return await _incidentService.UpdateIncidentAsync(
                request.Id,
                request.Description,
                request.Severity,
                request.InjuriesDescription ?? "",
                request.TreatmentProvided ?? "",
                request.MedicalAttentionRequired,
                request.MedicalTreatmentLocation ?? "",
                request.RootCause ?? "",
                request.CorrectiveActions ?? "",
                request.IsNotifiable,
                request.NotifiedAuthority ?? "",
                cancellationToken);
        }
    }
}
