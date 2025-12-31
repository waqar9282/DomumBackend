using DomumBackend.Domain.Enums;
using System;
using System.Collections.Generic;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Incident/Accident reporting system
    /// Captures safety events, injuries, behavioral incidents, and unusual occurrences
    /// Critical for child safeguarding and regulatory compliance
    /// </summary>
    public class Incident : BaseEntity
    {
        /// <summary>
        /// Unique identifier for the incident
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The facility where the incident occurred
        /// </summary>
        public string? FacilityId { get; set; }
        public Facility? Facility { get; set; }

        /// <summary>
        /// The young person involved in the incident (if applicable)
        /// Nullable because incident may not directly relate to a specific young person
        /// </summary>
        public string? YoungPersonId { get; set; }
        public YoungPerson? YoungPerson { get; set; }

        /// <summary>
        /// Type of incident (Injury, Behavioral, Medical Emergency, etc.)
        /// </summary>
        public IncidentType IncidentType { get; set; }

        /// <summary>
        /// Severity level (Minor, Moderate, Serious, Critical)
        /// </summary>
        public IncidentSeverity Severity { get; set; }

        /// <summary>
        /// Detailed description of what happened
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Date and time the incident occurred
        /// </summary>
        public DateTime IncidentDate { get; set; }

        /// <summary>
        /// Date and time the incident was reported
        /// </summary>
        public DateTime ReportedDate { get; set; }

        /// <summary>
        /// User ID of staff member who reported the incident
        /// </summary>
        public string? ReportedByUserId { get; set; }

        /// <summary>
        /// Location within the facility where incident occurred
        /// Examples: "Kitchen", "Bedroom 3", "Garden", "Bathroom"
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Other young people involved (comma-separated IDs or names)
        /// </summary>
        public string? OtherPersonsInvolved { get; set; }

        /// <summary>
        /// Staff members present at the time
        /// </summary>
        public string? StaffPresent { get; set; }

        /// <summary>
        /// Any injuries sustained (description)
        /// </summary>
        public string? InjuriesDescription { get; set; }

        /// <summary>
        /// First aid/medical treatment provided
        /// </summary>
        public string? TreatmentProvided { get; set; }

        /// <summary>
        /// Was external medical attention needed?
        /// </summary>
        public bool MedicalAttentionRequired { get; set; }

        /// <summary>
        /// If yes, where was the person treated? (Hospital, Clinic, etc.)
        /// </summary>
        public string? MedicalTreatmentLocation { get; set; }

        /// <summary>
        /// Witness statements or additional details
        /// </summary>
        public string? WitnessStatements { get; set; }

        /// <summary>
        /// Root cause analysis
        /// </summary>
        public string? RootCause { get; set; }

        /// <summary>
        /// Corrective actions taken to prevent recurrence
        /// </summary>
        public string? CorrectiveActions { get; set; }

        /// <summary>
        /// Was this a notifiable incident? (OFSTED, safeguarding board, etc.)
        /// </summary>
        public bool IsNotifiable { get; set; }

        /// <summary>
        /// Which authority was notified (OFSTED, Police, MASH, etc.)
        /// </summary>
        public string? NotifiedAuthority { get; set; }

        /// <summary>
        /// Status of the incident (Open, Under Investigation, Closed)
        /// </summary>
        public IncidentStatus Status { get; set; } = IncidentStatus.Open;

        /// <summary>
        /// Date the incident was closed/resolved
        /// </summary>
        public DateTime? ClosedDate { get; set; }

        /// <summary>
        /// Closing notes
        /// </summary>
        public string? ClosingNotes { get; set; }

        /// <summary>
        /// Reference number for tracking (e.g., INC-2025-001)
        /// </summary>
        public string? ReferenceNumber { get; set; }

        /// <summary>
        /// Is this incident marked as confidential?
        /// </summary>
        public bool IsConfidential { get; set; }

        /// <summary>
        /// Linked safeguarding concerns
        /// </summary>
        public ICollection<SafeguardingConcern> LinkedConcerns { get; set; } = new List<SafeguardingConcern>();
    }
}
