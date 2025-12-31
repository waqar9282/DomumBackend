using DomumBackend.Application.Common.Interfaces;
using MediatR;

namespace DomumBackend.Application.Commands.HealthWellness.MedicalRecord
{
    public class UpdateMedicalRecordCommand : IRequest<int>
    {
        public string Id { get; set; }
        public int Status { get; set; }
        public string FindingsObservations { get; set; }
        public string Treatment { get; set; }
        public string Prescription { get; set; }
        public string ReferralTo { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string FollowUpNotes { get; set; }
    }

    public class UpdateMedicalRecordCommandHandler : IRequestHandler<UpdateMedicalRecordCommand, int>
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public UpdateMedicalRecordCommandHandler(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        public async Task<int> Handle(UpdateMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            return await _medicalRecordService.UpdateMedicalRecordAsync(
                request.Id,
                request.Status,
                request.FindingsObservations,
                request.Treatment,
                request.Prescription,
                request.ReferralTo,
                request.FollowUpDate,
                request.FollowUpNotes,
                cancellationToken);
        }
    }
}
