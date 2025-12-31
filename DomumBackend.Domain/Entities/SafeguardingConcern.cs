using System;
using System.Collections.Generic;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Child Protection/Safeguarding concerns
    /// Separate from incidents - these are formal safeguarding referrals
    /// Triggers notification to Designated Safeguarding Lead, social workers, etc.
    /// </summary>
    public class SafeguardingConcern : BaseEntity
    {
        public new string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The facility where the concern was raised
        /// </summary>
        public string? FacilityId { get; set; }
        public Facility? Facility { get; set; }

        /// <summary>
        /// Young person the concern relates to
        /// </summary>
        public string? YoungPersonId { get; set; }
        public YoungPerson? YoungPerson { get; set; }

        /// <summary>
        /// Type of concern: Physical Abuse, Emotional Abuse, Sexual Abuse, Neglect, Exploitation
        /// </summary>
        public SafeguardingConcernType ConcernType { get; set; }

        /// <summary>
        /// Detailed description of the concern
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Date the concern was identified
        /// </summary>
        public DateTime ConcernDate { get; set; }

        /// <summary>
        /// Date the concern was reported/logged
        /// </summary>
        public DateTime ReportedDate { get; set; }

        /// <summary>
        /// User who reported the concern
        /// </summary>
        public string? ReportedByUserId { get; set; }

        /// <summary>
        /// Person at the center of the concern (if not the young person themselves)
        /// Examples: "Staff member John Smith", "Family member"
        /// </summary>
        public string? ConcernAboutPerson { get; set; }

        /// <summary>
        /// Assigned to which staff member for investigation
        /// </summary>
        public string? AssignedToUserId { get; set; }

        /// <summary>
        /// Designated Safeguarding Lead who was notified
        /// </summary>
        public string? DSLNotifiedUserId { get; set; }

        /// <summary>
        /// Date DSL was notified
        /// </summary>
        public DateTime? DSLNotifiedDate { get; set; }

        /// <summary>
        /// Was this reported to external authorities? (MASH, Social Services, Police)
        /// </summary>
        public bool ReportedToExternalAuthority { get; set; }

        /// <summary>
        /// Which authority was notified (MASH, Police, Social Services, etc.)
        /// </summary>
        public string? ExternalAuthorityContacted { get; set; }

        /// <summary>
        /// Reference number from external authority
        /// </summary>
        public string? ExternalReferenceNumber { get; set; }

        /// <summary>
        /// Current status of the concern
        /// </summary>
        public SafeguardingConcernStatus Status { get; set; } = SafeguardingConcernStatus.Reported;

        /// <summary>
        /// Detailed investigation notes
        /// </summary>
        public string? InvestigationNotes { get; set; }

        /// <summary>
        /// Outcome of investigation
        /// </summary>
        public string? Outcome { get; set; }

        /// <summary>
        /// Actions taken as a result
        /// </summary>
        public string? ActionsTaken { get; set; }

        /// <summary>
        /// Date the concern was closed
        /// </summary>
        public DateTime? ClosedDate { get; set; }

        /// <summary>
        /// Evidence or attachments (file paths or IDs)
        /// </summary>
        public string? Evidence { get; set; }

        /// <summary>
        /// Is this concern confidential/sensitive?
        /// </summary>
        public bool IsConfidential { get; set; }

        /// <summary>
        /// Linked incidents
        /// </summary>
        public ICollection<Incident> LinkedIncidents { get; set; } = new List<Incident>();
    }

    public enum SafeguardingConcernType
    {
        PhysicalAbuse = 0,
        EmotionalAbuse = 1,
        SexualAbuse = 2,
        Neglect = 3,
        FinancialAbuse = 4,
        Exploitation = 5,
        ModernSlavery = 6,
        Other = 99
    }

    public enum SafeguardingConcernStatus
    {
        Reported = 0,
        UnderInvestigation = 1,
        EscalatedToAuthority = 2,
        Resolved = 3,
        Closed = 4,
        Withdrawn = 5
    }
}
