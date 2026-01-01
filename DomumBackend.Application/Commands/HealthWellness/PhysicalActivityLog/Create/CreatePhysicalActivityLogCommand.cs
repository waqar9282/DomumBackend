using DomumBackend.Application.Common.Interfaces;
using MediatR;

namespace DomumBackend.Application.Commands.HealthWellness.PhysicalActivityLog
{
    public class CreatePhysicalActivityLogCommand : IRequest<string>
    {
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public int ActivityType { get; set; }
        public DateTime ActivityDate { get; set; }
        public string? ActivityDescription { get; set; }
        public string? Location { get; set; }
        public int DurationMinutes { get; set; }
        public string? Intensity { get; set; }
        public string? Participants { get; set; }
        public string? SupervisingStaff { get; set; }
        public string? PerformanceLevel { get; set; }
        public string? EffortLevel { get; set; }
        public string? RecordedByUserId { get; set; }
    }

    public class CreatePhysicalActivityLogCommandHandler : IRequestHandler<CreatePhysicalActivityLogCommand, string>
    {
        private readonly IPhysicalActivityLogService _service;
        public CreatePhysicalActivityLogCommandHandler(IPhysicalActivityLogService service) => _service = service;
        public async Task<string> Handle(CreatePhysicalActivityLogCommand r, CancellationToken ct)
            => await _service.CreatePhysicalActivityLogAsync(r.FacilityId!, r.YoungPersonId!, r.ActivityType,
                r.ActivityDate, r.ActivityDescription!, r.Location!, r.DurationMinutes, r.Intensity!,
                r.Participants!, r.SupervisingStaff!, r.PerformanceLevel!, r.EffortLevel!, r.RecordedByUserId!, ct);
    }

    public class UpdatePhysicalActivityLogCommand : IRequest<int>
    {
        public string? Id { get; set; }
        public string? ActivityDescription { get; set; }
        public string? PerformanceLevel { get; set; }
        public string? ProgressNotes { get; set; }
    }

    public class UpdatePhysicalActivityLogCommandHandler : IRequestHandler<UpdatePhysicalActivityLogCommand, int>
    {
        private readonly IPhysicalActivityLogService _service;
        public UpdatePhysicalActivityLogCommandHandler(IPhysicalActivityLogService service) => _service = service;
        public async Task<int> Handle(UpdatePhysicalActivityLogCommand r, CancellationToken ct)
            => await _service.UpdatePhysicalActivityLogAsync(r.Id!, r.ActivityDescription!, r.PerformanceLevel!, r.ProgressNotes!, ct);
    }
}

