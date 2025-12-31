using System;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Comprehensive audit trail for all entity changes
    /// Captures WHO changed WHAT, WHEN, WHY, and the OLD vs NEW values
    /// Essential for regulatory compliance and accountability
    /// </summary>
    public class AuditLog : BaseEntity
    {
        /// <summary>
        /// User who made the change
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Name of the entity that was changed (e.g., "YoungPerson", "Medication")
        /// </summary>
        public string? EntityName { get; set; }

        /// <summary>
        /// ID of the entity that was changed
        /// </summary>
        public string? EntityId { get; set; }

        /// <summary>
        /// Type of action: Create, Update, Delete, View (for sensitive data access)
        /// </summary>
        public string? Action { get; set; }

        /// <summary>
        /// JSON representation of the entity state BEFORE the change
        /// </summary>
        public string? OldValue { get; set; }

        /// <summary>
        /// JSON representation of the entity state AFTER the change
        /// </summary>
        public string? NewValue { get; set; }

        /// <summary>
        /// Timestamp when the change occurred
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Reason for the change (optional but recommended)
        /// Examples: "Updated due to review", "Correction of data entry error"
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// IP Address of the user making the change (for security audit)
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// The changed property name (for granular tracking)
        /// e.g., "HealthIssues", "Medication"
        /// </summary>
        public string? ChangedProperty { get; set; }

        public AuditLog()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
