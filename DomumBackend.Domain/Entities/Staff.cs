namespace DomumBackend.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Staff - Represents staff members working at a facility
    /// </summary>
    public class Staff : BaseEntity
    {
        /// <summary>
        /// Associated user ID (from Identity system)
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// Facility ID where staff works
        /// </summary>
        public required string FacilityId { get; set; }

        public Facility Facility { get; set; }

        /// <summary>
        /// Staff member's full name
        /// </summary>
        public required string FullName { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Job title (e.g., "Care Worker", "Manager", "Deputy Manager")
        /// </summary>
        public required string JobTitle { get; set; }

        /// <summary>
        /// Department (e.g., "Care", "Administration", "Health & Safety")
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Employment status (Active, Inactive, OnLeave, Suspended, Terminated)
        /// </summary>
        public required string EmploymentStatus { get; set; }

        /// <summary>
        /// Start date at facility
        /// </summary>
        public required DateTime StartDate { get; set; }

        /// <summary>
        /// End date (null if still employed)
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Staff member's primary roles
        /// </summary>
        public List<string> Roles { get; set; } = new();

        /// <summary>
        /// Qualifications/certifications (CSV or serialized list)
        /// </summary>
        public string Qualifications { get; set; }

        /// <summary>
        /// DBS Check status (NotStarted, Pending, Cleared, Suspended)
        /// </summary>
        public string DBSCheckStatus { get; set; }

        /// <summary>
        /// DBS Check expiry date
        /// </summary>
        public DateTime? DBSCheckExpiryDate { get; set; }

        /// <summary>
        /// Safeguarding training completion date
        /// </summary>
        public DateTime? SafeguardingTrainingDate { get; set; }

        /// <summary>
        /// First aid certification expiry date
        /// </summary>
        public DateTime? FirstAidCertExpiryDate { get; set; }

        /// <summary>
        /// Fire safety training completion date
        /// </summary>
        public DateTime? FireSafetyTrainingDate { get; set; }

        /// <summary>
        /// Supervision frequency (Weekly, BiWeekly, Monthly)
        /// </summary>
        public string SupervisionFrequency { get; set; }

        /// <summary>
        /// Last supervision date
        /// </summary>
        public DateTime? LastSupervisionDate { get; set; }

        /// <summary>
        /// Staff member is active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Notes/comments about staff member
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Emergency contact name
        /// </summary>
        public string EmergencyContactName { get; set; }

        /// <summary>
        /// Emergency contact phone
        /// </summary>
        public string EmergencyContactPhone { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Postcode
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// Staff member's performance rating (1-5)
        /// </summary>
        public int? PerformanceRating { get; set; }

        /// <summary>
        /// Last annual appraisal date
        /// </summary>
        public DateTime? LastAppraisalDate { get; set; }

        /// <summary>
        /// Professional memberships
        /// </summary>
        public string ProfessionalMemberships { get; set; }

        /// <summary>
        /// Background checks performed
        /// </summary>
        [NotMapped]
        public Dictionary<string, string> BackgroundChecks { get; set; } = new();

        /// <summary>
        /// Training records
        /// </summary>
        [NotMapped]
        public List<Dictionary<string, object>> TrainingRecords { get; set; } = new();

        /// <summary>
        /// Disciplinary history
        /// </summary>
        [NotMapped]
        public List<Dictionary<string, object>> DisciplinaryHistory { get; set; } = new();

        /// <summary>
        /// Staff allocation history
        /// </summary>
        [NotMapped]
        public List<Dictionary<string, object>> AllocationHistory { get; set; } = new();

        /// <summary>
        /// Metadata for extended information
        /// </summary>
        [NotMapped]
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}
