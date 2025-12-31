using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Enums;

namespace DomumBackend.Application.Common.Interfaces
{
    /// <summary>
    /// Service interface for incident management operations
    /// Provides abstraction from database layer for CQRS commands/queries
    /// </summary>
    public interface IIncidentService
    {
        /// <summary>
        /// Create a new incident report
        /// </summary>
        Task<string> CreateIncidentAsync(
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
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Update an existing incident
        /// </summary>
        Task<int> UpdateIncidentAsync(
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
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Close/resolve an incident
        /// </summary>
        Task<int> CloseIncidentAsync(string id, string closingNotes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all incidents for a facility
        /// </summary>
        Task<List<IncidentResponseDTO>> GetIncidentsByFacilityAsync(string facilityId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get detailed information about a specific incident
        /// </summary>
        Task<IncidentDetailsResponseDTO> GetIncidentByIdAsync(string incidentId, CancellationToken cancellationToken = default);
    }
}
