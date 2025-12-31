#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateMedicalRecordAsync(
            string facilityId, string youngPersonId, int appointmentType, DateTime appointmentDate,
            string healthcareProfessional, string healthcareFacility, int status, string diagnosis,
            string chief_complaint, string symptoms, string findingsObservations, string treatment,
            string prescription, string referralTo, string notes, string recordedByUserId,
            bool isConfidential, CancellationToken cancellationToken)
        {
            var record = new MedicalRecord
            {
                FacilityId = facilityId,
                YoungPersonId = youngPersonId,
                AppointmentType = (Domain.Enums.AppointmentType)appointmentType,
                AppointmentDate = appointmentDate,
                RecordedDate = DateTime.UtcNow,
                HealthcareProfessional = healthcareProfessional,
                HealthcareFacility = healthcareFacility,
                Status = (Domain.Enums.AppointmentStatus)status,
                Diagnosis = diagnosis,
                Chief_Complaint = chief_complaint,
                Symptoms = symptoms,
                FindingsObservations = findingsObservations,
                Treatment = treatment,
                Prescription = prescription,
                ReferralTo = referralTo,
                Notes = notes,
                RecordedByUserId = recordedByUserId,
                IsConfidential = isConfidential,
                CreatedDate = DateTime.UtcNow
            };

            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync(cancellationToken);
            return record.Id.ToString();
        }

        public async Task<int> UpdateMedicalRecordAsync(
            string id, int status, string findingsObservations, string treatment, string prescription,
            string referralTo, DateTime? followUpDate, string followUpNotes, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long recordId))
                return 0;

            var record = await _context.MedicalRecords.FirstOrDefaultAsync(m => m.Id == recordId, cancellationToken);
            if (record == null)
                return 0;

            record.Status = (Domain.Enums.AppointmentStatus)status;
            record.FindingsObservations = findingsObservations;
            record.Treatment = treatment;
            record.Prescription = prescription;
            record.ReferralTo = referralTo;
            record.FollowUpDate = followUpDate;
            record.FollowUpNotes = followUpNotes;

            _context.MedicalRecords.Update(record);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<MedicalRecordResponseDTO> GetMedicalRecordByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long recordId))
                return null;

            var record = await _context.MedicalRecords
                .Include(m => m.YoungPerson)
                .FirstOrDefaultAsync(m => m.Id == recordId, cancellationToken);

            if (record == null)
                return null;

            return new MedicalRecordResponseDTO
            {
                Id = record.Id.ToString(),
                FacilityId = record.FacilityId,
                YoungPersonId = record.YoungPersonId,
                YoungPersonName = record.YoungPerson?.FullName,
                AppointmentType = (int)record.AppointmentType,
                AppointmentTypeDescription = record.AppointmentType.ToString(),
                AppointmentDate = record.AppointmentDate,
                HealthcareProfessional = record.HealthcareProfessional,
                HealthcareFacility = record.HealthcareFacility,
                Diagnosis = record.Diagnosis,
                Chief_Complaint = record.Chief_Complaint,
                Treatment = record.Treatment,
                Status = (int)record.Status,
                StatusDescription = record.Status.ToString(),
                FollowUpDate = record.FollowUpDate,
                CreatedDate = record.CreatedDate
            };
        }

        public async Task<List<MedicalRecordResponseDTO>> GetMedicalRecordsByYoungPersonAsync(string youngPersonId, CancellationToken cancellationToken)
        {
            return await _context.MedicalRecords
                .Where(m => m.YoungPersonId == youngPersonId)
                .Include(m => m.YoungPerson)
                .OrderByDescending(m => m.AppointmentDate)
                .Select(m => new MedicalRecordResponseDTO
                {
                    Id = m.Id.ToString(),
                    FacilityId = m.FacilityId,
                    YoungPersonId = m.YoungPersonId,
                    YoungPersonName = m.YoungPerson.FullName,
                    AppointmentType = (int)m.AppointmentType,
                    AppointmentTypeDescription = m.AppointmentType.ToString(),
                    AppointmentDate = m.AppointmentDate,
                    HealthcareProfessional = m.HealthcareProfessional,
                    HealthcareFacility = m.HealthcareFacility,
                    Diagnosis = m.Diagnosis,
                    Chief_Complaint = m.Chief_Complaint,
                    Treatment = m.Treatment,
                    Status = (int)m.Status,
                    StatusDescription = m.Status.ToString(),
                    FollowUpDate = m.FollowUpDate,
                    CreatedDate = m.CreatedDate
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<List<MedicalRecordResponseDTO>> GetMedicalRecordsByFacilityAsync(string facilityId, CancellationToken cancellationToken)
        {
            return await _context.MedicalRecords
                .Where(m => m.FacilityId == facilityId)
                .Include(m => m.YoungPerson)
                .OrderByDescending(m => m.AppointmentDate)
                .Select(m => new MedicalRecordResponseDTO
                {
                    Id = m.Id.ToString(),
                    FacilityId = m.FacilityId,
                    YoungPersonId = m.YoungPersonId,
                    YoungPersonName = m.YoungPerson.FullName,
                    AppointmentType = (int)m.AppointmentType,
                    AppointmentTypeDescription = m.AppointmentType.ToString(),
                    AppointmentDate = m.AppointmentDate,
                    HealthcareProfessional = m.HealthcareProfessional,
                    HealthcareFacility = m.HealthcareFacility,
                    Diagnosis = m.Diagnosis,
                    Chief_Complaint = m.Chief_Complaint,
                    Treatment = m.Treatment,
                    Status = (int)m.Status,
                    StatusDescription = m.Status.ToString(),
                    FollowUpDate = m.FollowUpDate,
                    CreatedDate = m.CreatedDate
                })
                .ToListAsync(cancellationToken);
        }
    }
}
