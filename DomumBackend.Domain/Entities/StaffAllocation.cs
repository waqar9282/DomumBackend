namespace DomumBackend.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// StaffAllocation - Tracks staff assignments to young people
    /// </summary>
    public class StaffAllocation : BaseEntity
    {
        /// <summary>
        /// Staff member ID
        /// </summary>
        public required long StaffId { get; set; }

        public Staff Staff { get; set; }

        /// <summary>
        /// Young person ID (string type to match YoungPerson.Id)
        /// </summary>
        public required string YoungPersonId { get; set; }

        public YoungPerson YoungPerson { get; set; }

        /// <summary>
        /// Facility ID
        /// </summary>
        public required string FacilityId { get; set; }

        public Facility Facility { get; set; }

        /// <summary>
        /// Allocation type (Primary, Secondary, Backup, Specialist)
        /// </summary>
        public required string AllocationType { get; set; }

        /// <summary>
        /// Start date of allocation
        /// </summary>
        public required DateTime StartDate { get; set; }

        /// <summary>
        /// End date of allocation (null if ongoing)
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Reason for allocation
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Is allocation active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Hours per week assigned
        /// </summary>
        public decimal HoursPerWeek { get; set; }

        /// <summary>
        /// Shift pattern (e.g., "Morning", "Afternoon", "Night", "Flexible")
        /// </summary>
        public string ShiftPattern { get; set; }

        /// <summary>
        /// Specialization areas (CSV)
        /// </summary>
        public string SpecializationAreas { get; set; }

        /// <summary>
        /// Notes about allocation
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// User ID who created allocation
        /// </summary>
        public string CreatedByUserId { get; set; }

        /// <summary>
        /// User ID who last modified allocation
        /// </summary>
        public string? ModifiedByUserId { get; set; }

        /// <summary>
        /// Performance notes for this allocation
        /// </summary>
        public string PerformanceNotes { get; set; }

        /// <summary>
        /// Allocation metrics
        /// </summary>
        [NotMapped]
        public Dictionary<string, object> Metrics { get; set; } = new();

        /// <summary>
        /// Allocation history
        /// </summary>
        [NotMapped]
        public List<Dictionary<string, object>> HistoryChanges { get; set; } = new();

        /// <summary>
        /// Extended metadata
        /// </summary>
        [NotMapped]
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}
