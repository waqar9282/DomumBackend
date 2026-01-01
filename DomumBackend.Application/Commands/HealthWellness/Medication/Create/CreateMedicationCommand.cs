using DomumBackend.Application.Common.Interfaces;
using MediatR;

namespace DomumBackend.Application.Commands.HealthWellness.Medication
{
    public class CreateMedicationCommand : IRequest<string>
    {
        public string? FacilityId { get; set; }
        public string? YoungPersonId { get; set; }
        public string? MedicationName { get; set; }
        public string? ActiveIngredient { get; set; }
        public string? Dosage { get; set; }
        public string? DosageUnit { get; set; }
        public int QuantityPerDose { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationDays { get; set; }
        public string? Frequency { get; set; }
        public string? Route { get; set; }
        public string? PrescribedByDoctor { get; set; }
        public string? ReasonForMedication { get; set; }
        public bool IsAdministeredByStaff { get; set; }
        public string? RecordedByUserId { get; set; }
    }

    public class CreateMedicationCommandHandler : IRequestHandler<CreateMedicationCommand, string>
    {
        private readonly IMedicationService _medicationService;

        public CreateMedicationCommandHandler(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        public async Task<string> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
        {
            return await _medicationService.CreateMedicationAsync(
                request.FacilityId!,
                request.YoungPersonId!,
                request.MedicationName!,
                request.ActiveIngredient!,
                request.Dosage!,
                request.DosageUnit!,
                request.QuantityPerDose,
                request.PrescriptionDate,
                request.StartDate,
                request.DurationDays,
                request.Frequency!,
                request.Route!,
                request.PrescribedByDoctor!,
                request.ReasonForMedication!,
                request.IsAdministeredByStaff,
                request.RecordedByUserId!,
                cancellationToken);
        }
    }

    public class UpdateMedicationCommand : IRequest<int>
    {
        public string? Id { get; set; }
        public int Status { get; set; }
        public string? ReportedSideEffects { get; set; }
        public string? Notes { get; set; }
    }

    public class UpdateMedicationCommandHandler : IRequestHandler<UpdateMedicationCommand, int>
    {
        private readonly IMedicationService _medicationService;

        public UpdateMedicationCommandHandler(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        public async Task<int> Handle(UpdateMedicationCommand request, CancellationToken cancellationToken)
        {
            return await _medicationService.UpdateMedicationAsync(
                request.Id!,
                request.Status,
                request.ReportedSideEffects!,
                request.Notes!,
                cancellationToken);
        }
    }

    public class DiscontinueMedicationCommand : IRequest<int>
    {
        public string? Id { get; set; }
        public string? Reason { get; set; }
    }

    public class DiscontinueMedicationCommandHandler : IRequestHandler<DiscontinueMedicationCommand, int>
    {
        private readonly IMedicationService _medicationService;

        public DiscontinueMedicationCommandHandler(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        public async Task<int> Handle(DiscontinueMedicationCommand request, CancellationToken cancellationToken)
        {
            return await _medicationService.DiscontinueMedicationAsync(request.Id!, request.Reason!, cancellationToken);
        }
    }
}

