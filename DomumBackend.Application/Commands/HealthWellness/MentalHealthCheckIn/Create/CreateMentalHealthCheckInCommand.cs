using DomumBackend.Application.Common.Interfaces;
using MediatR;

namespace DomumBackend.Application.Commands.HealthWellness.MentalHealthCheckIn
{
    public class CreateMentalHealthCheckInCommand : IRequest<string>
    {
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public int CurrentMood { get; set; }
        public string? MoodDescription { get; set; }
        public string? EmotionalState { get; set; }
        public int? MoodScore { get; set; }
        public string? SleepQuality { get; set; }
        public string? EnergyLevel { get; set; }
        public string? CurrentConcerns { get; set; }
        public string? Coping { get; set; }
        public string? SupportSources { get; set; }
        public bool HasSuicidalThoughts { get; set; }
        public bool IsSelfHarming { get; set; }
        public string? StaffObservations { get; set; }
        public string? CheckInByUserId { get; set; }
        public bool IsConfidential { get; set; }
    }

    public class CreateMentalHealthCheckInCommandHandler : IRequestHandler<CreateMentalHealthCheckInCommand, string>
    {
        private readonly IMentalHealthCheckInService _service;
        public CreateMentalHealthCheckInCommandHandler(IMentalHealthCheckInService service) => _service = service;
        public async Task<string> Handle(CreateMentalHealthCheckInCommand r, CancellationToken ct)
            => await _service.CreateMentalHealthCheckInAsync(r.FacilityId!, r.YoungPersonId!, r.CurrentMood,
                r.MoodDescription!, r.EmotionalState!, r.MoodScore, r.SleepQuality!, r.EnergyLevel!,
                r.CurrentConcerns!, r.Coping!, r.SupportSources!, r.HasSuicidalThoughts, r.IsSelfHarming,
                r.StaffObservations!, r.CheckInByUserId!, r.IsConfidential, ct);
    }

    public class UpdateMentalHealthCheckInCommand : IRequest<int>
    {
        public string? Id { get; set; }
        public int CurrentMood { get; set; }
        public string? StaffObservations { get; set; }
        public string? ActionsRequired { get; set; }
        public bool ReferralRequired { get; set; }
        public string? ReferralDetails { get; set; }
    }

    public class UpdateMentalHealthCheckInCommandHandler : IRequestHandler<UpdateMentalHealthCheckInCommand, int>
    {
        private readonly IMentalHealthCheckInService _service;
        public UpdateMentalHealthCheckInCommandHandler(IMentalHealthCheckInService service) => _service = service;
        public async Task<int> Handle(UpdateMentalHealthCheckInCommand r, CancellationToken ct)
            => await _service.UpdateMentalHealthCheckInAsync(r.Id!, r.CurrentMood, r.StaffObservations!,
                r.ActionsRequired!, r.ReferralRequired, r.ReferralDetails!, ct);
    }
}

