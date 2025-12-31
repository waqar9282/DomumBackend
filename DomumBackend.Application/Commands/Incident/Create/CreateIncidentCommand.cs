using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Domain.Enums;
using MediatR;

namespace DomumBackend.Application.Commands.Incident
{
    /// <summary>
    /// Command to create a new incident report
    /// </summary>
    public class CreateIncidentCommand : IRequest<string>
    {
        public required string FacilityId { get; set; }
        public required string YoungPersonId { get; set; }
        public IncidentType IncidentType { get; set; }
        public IncidentSeverity Severity { get; set; }
        public required string Description { get; set; }
        public DateTime IncidentDate { get; set; }
        public required string ReportedByUserId { get; set; }
        public required string Location { get; set; }
        public string? OtherPersonsInvolved { get; set; }
        public string? StaffPresent { get; set; }
        public string? InjuriesDescription { get; set; }
        public string? TreatmentProvided { get; set; }
        public bool MedicalAttentionRequired { get; set; }
        public string? MedicalTreatmentLocation { get; set; }
        public string? WitnessStatements { get; set; }
        public string? RootCause { get; set; }
        public string? CorrectiveActions { get; set; }
        public bool IsNotifiable { get; set; }
        public string? NotifiedAuthority { get; set; }
        public bool IsConfidential { get; set; }
    }

    public class CreateIncidentCommandHandler : IRequestHandler<CreateIncidentCommand, string>
    {
        private readonly IIncidentService _incidentService;

        public CreateIncidentCommandHandler(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        public async Task<string> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
        {
            return await _incidentService.CreateIncidentAsync(
                request.FacilityId,
                request.YoungPersonId,
                request.IncidentType,
                request.Severity,
                request.Description,
                request.IncidentDate,
                request.ReportedByUserId,
                request.Location,
                request.OtherPersonsInvolved ?? "",
                request.StaffPresent ?? "",
                request.InjuriesDescription ?? "",
                request.TreatmentProvided ?? "",
                request.MedicalAttentionRequired,
                request.MedicalTreatmentLocation ?? "",
                request.WitnessStatements ?? "",
                request.RootCause ?? "",
                request.CorrectiveActions ?? "",
                request.IsNotifiable,
                request.NotifiedAuthority ?? "",
                request.IsConfidential,
                cancellationToken);
        }
    }
}
