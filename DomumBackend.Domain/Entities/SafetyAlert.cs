using System;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Safety alerts for ongoing risks or concerns
    /// Different from incidents - these are proactive alerts about potential dangers
    /// Examples: "Young person has elopement risk", "Medication interaction warning"
    /// </summary>
    public class SafetyAlert : BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The facility where the alert applies
        /// </summary>
        public string? FacilityId { get; set; }
        public Facility? Facility { get; set; }

        /// <summary>
        /// The young person the alert relates to
        /// </summary>
        public string? YoungPersonId { get; set; }
        public YoungPerson? YoungPerson { get; set; }

        /// <summary>
        /// Alert title/category
        /// Examples: "Elopement Risk", "Self-Harm Risk", "Medication Allergy", "Behavioral Trigger"
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Detailed description of the risk
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Risk level: Low, Medium, High, Critical
        /// </summary>
        public SafetyAlertLevel AlertLevel { get; set; }

        /// <summary>
        /// Recommended preventive actions
        /// </summary>
        public string? PreventiveActions { get; set; }

        /// <summary>
        /// What to do if the risk occurs
        /// </summary>
        public string? ResponseActions { get; set; }

        /// <summary>
        /// User who created the alert
        /// </summary>
        public string? CreatedByUserId { get; set; }

        /// <summary>
        /// When the alert was created
        /// </summary>
        public DateTime AlertDate { get; set; }

        /// <summary>
        /// When should this alert be reviewed?
        /// </summary>
        public DateTime NextReviewDate { get; set; }

        /// <summary>
        /// Is this alert currently active?
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// When was the alert deactivated (if inactive)
        /// </summary>
        public DateTime? DeactivatedDate { get; set; }

        /// <summary>
        /// Why was the alert deactivated
        /// </summary>
        public string? DeactivationReason { get; set; }

        /// <summary>
        /// Should all staff be notified of this alert?
        /// </summary>
        public bool NotifyAllStaff { get; set; }

        /// <summary>
        /// How urgent is this alert? (None, Immediate, Weekly, Monthly review)
        /// </summary>
        public AlertUrgency Urgency { get; set; }
    }

    public enum SafetyAlertLevel
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Critical = 3
    }

    public enum AlertUrgency
    {
        None = 0,
        Immediate = 1,
        Daily = 2,
        Weekly = 3,
        Monthly = 4
    }
}
