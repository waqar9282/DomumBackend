using DomumBackend.Application.Common.Interfaces;
using MediatR;

namespace DomumBackend.Application.Commands.HealthWellness.MedicalRecord
{
    public class CreateMedicalRecordCommand : IRequest<string>
    {
        public string FacilityId { get; set; }
        public string YoungPersonId { get; set; }
        public int AppointmentType { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string HealthcareProfessional { get; set; }
        public string HealthcareFacility { get; set; }
        public int Status { get; set; }
        public string Diagnosis { get; set; }
        public string Chief_Complaint { get; set; }
        public string Symptoms { get; set; }
        public string FindingsObservations { get; set; }
        public string Treatment { get; set; }
        public string Prescription { get; set; }
        public string ReferralTo { get; set; }
        public string Notes { get; set; }
        public string RecordedByUserId { get; set; }
        public bool IsConfidential { get; set; }
    }

    public class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, string>
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public CreateMedicalRecordCommandHandler(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        public async Task<string> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            return await _medicalRecordService.CreateMedicalRecordAsync(
                request.FacilityId,
                request.YoungPersonId,
                request.AppointmentType,
                request.AppointmentDate,
                request.HealthcareProfessional,
                request.HealthcareFacility,
                request.Status,
                request.Diagnosis,
                request.Chief_Complaint,
                request.Symptoms,
                request.FindingsObservations,
                request.Treatment,
                request.Prescription,
                request.ReferralTo,
                request.Notes,
                request.RecordedByUserId,
                request.IsConfidential,
                cancellationToken);
        }
    }
}
