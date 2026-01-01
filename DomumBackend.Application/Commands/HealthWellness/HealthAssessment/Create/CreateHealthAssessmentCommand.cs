using DomumBackend.Application.Common.Interfaces;
using MediatR;

namespace DomumBackend.Application.Commands.HealthWellness.HealthAssessment
{
    public class CreateHealthAssessmentCommand : IRequest<string>
    {
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public int AssessmentType { get; set; }
        public DateTime AssessmentDate { get; set; }
        public string? AssessedByProfessional { get; set; }
        public string? AssessmentLocation { get; set; }
        public string? AssessmentFindings { get; set; }
        public string? Recommendations { get; set; }
        public string? ReferralRequired { get; set; }
        public string? RecordedByUserId { get; set; }
        public bool IsConfidential { get; set; }
    }

    public class CreateHealthAssessmentCommandHandler : IRequestHandler<CreateHealthAssessmentCommand, string>
    {
        private readonly IHealthAssessmentService _service;
        public CreateHealthAssessmentCommandHandler(IHealthAssessmentService service) => _service = service;
        public async Task<string> Handle(CreateHealthAssessmentCommand r, CancellationToken ct)
            => await _service.CreateHealthAssessmentAsync(r.FacilityId, r.YoungPersonId, r.AssessmentType,
                r.AssessmentDate, r.AssessedByProfessional, r.AssessmentLocation, r.AssessmentFindings,
                r.Recommendations, r.ReferralRequired, r.RecordedByUserId, r.IsConfidential, ct);
    }

    public class UpdateHealthAssessmentCommand : IRequest<int>
    {
        public string? Id { get; set; }
        public string? AssessmentFindings { get; set; }
        public string? Recommendations { get; set; }
        public string? ReferralRequired { get; set; }
        public DateTime? NextAssessmentDate { get; set; }
    }

    public class UpdateHealthAssessmentCommandHandler : IRequestHandler<UpdateHealthAssessmentCommand, int>
    {
        private readonly IHealthAssessmentService _service;
        public UpdateHealthAssessmentCommandHandler(IHealthAssessmentService service) => _service = service;
        public async Task<int> Handle(UpdateHealthAssessmentCommand r, CancellationToken ct)
            => await _service.UpdateHealthAssessmentAsync(r.Id, r.AssessmentFindings, r.Recommendations,
                r.ReferralRequired, r.NextAssessmentDate, ct);
    }
}

