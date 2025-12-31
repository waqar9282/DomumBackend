using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Enums;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    /// <summary>
    /// Implementation of incident management service
    /// Handles all incident-related database operations
    /// </summary>
    public class IncidentService : IIncidentService
    {
        private readonly ApplicationDbContext _context;

        public IncidentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateIncidentAsync(
            string facilityId,
            string youngPersonId,
            IncidentType incidentType,
            IncidentSeverity severity,
            string description,
            DateTime incidentDate,
            string reportedByUserId,
            string location,
            string otherPersonsInvolved,
            string staffPresent,
            string injuriesDescription,
            string treatmentProvided,
            bool medicalAttentionRequired,
            string medicalTreatmentLocation,
            string witnessStatements,
            string rootCause,
            string correctiveActions,
            bool isNotifiable,
            string notifiedAuthority,
            bool isConfidential,
            CancellationToken cancellationToken = default)
        {
            var incident = new Domain.Entities.Incident
            {
                Id = Guid.NewGuid().ToString(),
                FacilityId = facilityId,
                YoungPersonId = youngPersonId,
                IncidentType = incidentType,
                Severity = severity,
                Description = description,
                IncidentDate = incidentDate,
                ReportedDate = DateTime.UtcNow,
                ReportedByUserId = reportedByUserId,
                Location = location,
                OtherPersonsInvolved = otherPersonsInvolved,
                StaffPresent = staffPresent,
                InjuriesDescription = injuriesDescription,
                TreatmentProvided = treatmentProvided,
                MedicalAttentionRequired = medicalAttentionRequired,
                MedicalTreatmentLocation = medicalTreatmentLocation,
                WitnessStatements = witnessStatements,
                RootCause = rootCause,
                CorrectiveActions = correctiveActions,
                IsNotifiable = isNotifiable,
                NotifiedAuthority = notifiedAuthority,
                IsConfidential = isConfidential,
                ReferenceNumber = GenerateReferenceNumber()
            };

            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync(cancellationToken);

            return incident.Id;
        }

        public async Task<int> UpdateIncidentAsync(
            string id,
            string description,
            IncidentSeverity severity,
            string injuriesDescription,
            string treatmentProvided,
            bool medicalAttentionRequired,
            string medicalTreatmentLocation,
            string rootCause,
            string correctiveActions,
            bool isNotifiable,
            string notifiedAuthority,
            CancellationToken cancellationToken = default)
        {
            var incident = await _context.Incidents.FindAsync(new object[] { id }, cancellationToken);
            if (incident == null)
                return 0;

            incident.Description = description;
            incident.Severity = severity;
            incident.InjuriesDescription = injuriesDescription;
            incident.TreatmentProvided = treatmentProvided;
            incident.MedicalAttentionRequired = medicalAttentionRequired;
            incident.MedicalTreatmentLocation = medicalTreatmentLocation;
            incident.RootCause = rootCause;
            incident.CorrectiveActions = correctiveActions;
            incident.IsNotifiable = isNotifiable;
            incident.NotifiedAuthority = notifiedAuthority;

            _context.Incidents.Update(incident);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CloseIncidentAsync(string id, string closingNotes, CancellationToken cancellationToken = default)
        {
            var incident = await _context.Incidents.FindAsync(new object[] { id }, cancellationToken);
            if (incident == null)
                return 0;

            incident.Status = IncidentStatus.Closed;
            incident.ClosedDate = DateTime.UtcNow;
            incident.ClosingNotes = closingNotes;

            _context.Incidents.Update(incident);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<IncidentResponseDTO>> GetIncidentsByFacilityAsync(string facilityId, CancellationToken cancellationToken = default)
        {
            return await _context.Incidents
                .Where(i => i.FacilityId == facilityId)
                .Include(i => i.YoungPerson)
                .OrderByDescending(i => i.IncidentDate)
                .Select(i => new IncidentResponseDTO
                {
                    Id = i.Id,
                    FacilityId = i.FacilityId,
                    YoungPersonId = i.YoungPersonId,
                    YoungPersonName = i.YoungPerson.FullName,
                    IncidentType = (int)i.IncidentType,
                    IncidentTypeDescription = i.IncidentType.ToString(),
                    Severity = (int)i.Severity,
                    SeverityDescription = i.Severity.ToString(),
                    Description = i.Description,
                    IncidentDate = i.IncidentDate,
                    ReportedDate = i.ReportedDate,
                    ReportedByUserId = i.ReportedByUserId,
                    Location = i.Location,
                    InjuriesDescription = i.InjuriesDescription,
                    MedicalAttentionRequired = i.MedicalAttentionRequired,
                    Status = i.Status.ToString(),
                    ReferenceNumber = i.ReferenceNumber,
                    IsConfidential = i.IsConfidential,
                    ClosedDate = i.ClosedDate
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IncidentDetailsResponseDTO> GetIncidentByIdAsync(string incidentId, CancellationToken cancellationToken = default)
        {
            var incident = await _context.Incidents
                .Include(i => i.YoungPerson)
                .FirstOrDefaultAsync(i => i.Id == incidentId, cancellationToken);

            if (incident == null)
                return null;

            return new IncidentDetailsResponseDTO
            {
                Id = incident.Id,
                FacilityId = incident.FacilityId,
                YoungPersonId = incident.YoungPersonId,
                YoungPersonName = incident.YoungPerson?.FullName,
                IncidentType = (int)incident.IncidentType,
                Severity = (int)incident.Severity,
                Description = incident.Description,
                IncidentDate = incident.IncidentDate,
                ReportedDate = incident.ReportedDate,
                ReportedByUserId = incident.ReportedByUserId,
                Location = incident.Location,
                OtherPersonsInvolved = incident.OtherPersonsInvolved,
                StaffPresent = incident.StaffPresent,
                InjuriesDescription = incident.InjuriesDescription,
                TreatmentProvided = incident.TreatmentProvided,
                MedicalAttentionRequired = incident.MedicalAttentionRequired,
                MedicalTreatmentLocation = incident.MedicalTreatmentLocation,
                WitnessStatements = incident.WitnessStatements,
                RootCause = incident.RootCause,
                CorrectiveActions = incident.CorrectiveActions,
                IsNotifiable = incident.IsNotifiable,
                NotifiedAuthority = incident.NotifiedAuthority,
                Status = (int)incident.Status,
                ClosedDate = incident.ClosedDate,
                ClosingNotes = incident.ClosingNotes,
                ReferenceNumber = incident.ReferenceNumber,
                IsConfidential = incident.IsConfidential,
                CreatedDate = incident.CreatedDate
            };
        }

        private string GenerateReferenceNumber()
        {
            // Format: INC-2025-001234 (INC-YYYY-sequential)
            return $"INC-{DateTime.UtcNow.Year}-{Guid.NewGuid().GetHashCode().ToString().Substring(0, 6)}";
        }
    }
}
