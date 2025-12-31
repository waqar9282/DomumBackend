#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly ApplicationDbContext _context;

        public MedicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateMedicationAsync(
            string facilityId, string youngPersonId, string medicationName, string activeIngredient,
            string dosage, string dosageUnit, int quantityPerDose, DateTime prescriptionDate,
            DateTime startDate, int durationDays, string frequency, string route, string prescribedByDoctor,
            string reasonForMedication, bool isAdministeredByStaff, string recordedByUserId,
            CancellationToken cancellationToken)
        {
            var medication = new Medication
            {
                FacilityId = facilityId,
                YoungPersonId = youngPersonId,
                MedicationName = medicationName,
                ActiveIngredient = activeIngredient,
                Dosage = dosage,
                DosageUnit = dosageUnit,
                QuantityPerDose = quantityPerDose,
                PrescriptionDate = prescriptionDate,
                StartDate = startDate,
                DurationDays = durationDays,
                Frequency = frequency,
                Route = route,
                PrescribedByDoctor = prescribedByDoctor,
                ReasonForMedication = reasonForMedication,
                IsAdministeredByStaff = isAdministeredByStaff,
                Status = Domain.Enums.MedicationStatus.Active,
                RecordedByUserId = recordedByUserId,
                RecordedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            _context.Medications.Add(medication);
            await _context.SaveChangesAsync(cancellationToken);
            return medication.Id.ToString();
        }

        public async Task<int> UpdateMedicationAsync(string id, int status, string reportedSideEffects, string notes, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long medId))
                return 0;

            var medication = await _context.Medications.FirstOrDefaultAsync(m => m.Id == medId, cancellationToken);
            if (medication == null)
                return 0;

            medication.Status = (Domain.Enums.MedicationStatus)status;
            medication.ReportedSideEffects = reportedSideEffects;

            _context.Medications.Update(medication);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DiscontinueMedicationAsync(string id, string reason, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long medId))
                return 0;

            var medication = await _context.Medications.FirstOrDefaultAsync(m => m.Id == medId, cancellationToken);
            if (medication == null)
                return 0;

            medication.Status = Domain.Enums.MedicationStatus.Discontinued;
            medication.DiscontinuationReason = reason;
            medication.EndDate = DateTime.UtcNow;

            _context.Medications.Update(medication);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<MedicationResponseDTO> GetMedicationByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long medId))
                return null;

            var med = await _context.Medications
                .Include(m => m.YoungPerson)
                .FirstOrDefaultAsync(m => m.Id == medId, cancellationToken);

            if (med == null)
                return null;

            return new MedicationResponseDTO
            {
                Id = med.Id.ToString(),
                FacilityId = med.FacilityId,
                YoungPersonId = med.YoungPersonId,
                YoungPersonName = med.YoungPerson?.FullName,
                MedicationName = med.MedicationName,
                Dosage = med.Dosage,
                Frequency = med.Frequency,
                StartDate = med.StartDate,
                EndDate = med.EndDate,
                Status = (int)med.Status,
                StatusDescription = med.Status.ToString(),
                PrescribedByDoctor = med.PrescribedByDoctor,
                ReasonForMedication = med.ReasonForMedication,
                IsAdministeredByStaff = med.IsAdministeredByStaff,
                CreatedDate = med.CreatedDate
            };
        }

        public async Task<List<MedicationResponseDTO>> GetMedicationsByYoungPersonAsync(string youngPersonId, CancellationToken cancellationToken)
        {
            return await _context.Medications
                .Where(m => m.YoungPersonId == youngPersonId)
                .Include(m => m.YoungPerson)
                .OrderByDescending(m => m.StartDate)
                .Select(m => new MedicationResponseDTO
                {
                    Id = m.Id.ToString(),
                    FacilityId = m.FacilityId,
                    YoungPersonId = m.YoungPersonId,
                    YoungPersonName = m.YoungPerson.FullName,
                    MedicationName = m.MedicationName,
                    Dosage = m.Dosage,
                    Frequency = m.Frequency,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Status = (int)m.Status,
                    StatusDescription = m.Status.ToString(),
                    PrescribedByDoctor = m.PrescribedByDoctor,
                    ReasonForMedication = m.ReasonForMedication,
                    IsAdministeredByStaff = m.IsAdministeredByStaff,
                    CreatedDate = m.CreatedDate
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<List<MedicationResponseDTO>> GetActiveMedicationsByYoungPersonAsync(string youngPersonId, CancellationToken cancellationToken)
        {
            return await _context.Medications
                .Where(m => m.YoungPersonId == youngPersonId && m.Status == Domain.Enums.MedicationStatus.Active)
                .Include(m => m.YoungPerson)
                .OrderByDescending(m => m.StartDate)
                .Select(m => new MedicationResponseDTO
                {
                    Id = m.Id.ToString(),
                    FacilityId = m.FacilityId,
                    YoungPersonId = m.YoungPersonId,
                    YoungPersonName = m.YoungPerson.FullName,
                    MedicationName = m.MedicationName,
                    Dosage = m.Dosage,
                    Frequency = m.Frequency,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Status = (int)m.Status,
                    StatusDescription = m.Status.ToString(),
                    PrescribedByDoctor = m.PrescribedByDoctor,
                    ReasonForMedication = m.ReasonForMedication,
                    IsAdministeredByStaff = m.IsAdministeredByStaff,
                    CreatedDate = m.CreatedDate
                })
                .ToListAsync(cancellationToken);
        }
    }
}
