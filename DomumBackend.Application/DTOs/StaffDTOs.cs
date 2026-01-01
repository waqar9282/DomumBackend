namespace DomumBackend.Application.DTOs
{
    /// <summary>
    /// Staff Data Transfer Object
    /// </summary>
    public class StaffDTO
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string FacilityId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string EmploymentStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> Roles { get; set; } = new();
        public string Qualifications { get; set; }
        public string DBSCheckStatus { get; set; }
        public DateTime? DBSCheckExpiryDate { get; set; }
        public DateTime? SafeguardingTrainingDate { get; set; }
        public DateTime? FirstAidCertExpiryDate { get; set; }
        public DateTime? FireSafetyTrainingDate { get; set; }
        public string SupervisionFrequency { get; set; }
        public DateTime? LastSupervisionDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public int? PerformanceRating { get; set; }
        public DateTime? LastAppraisalDate { get; set; }
        public string ProfessionalMemberships { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    /// <summary>
    /// Staff Create Request DTO
    /// </summary>
    public class StaffCreateDTO
    {
        public required string UserId { get; set; }
        public required string FacilityId { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public string PhoneNumber { get; set; }
        public required string JobTitle { get; set; }
        public string Department { get; set; }
        public required string EmploymentStatus { get; set; }
        public required DateTime StartDate { get; set; }
        public List<string> Roles { get; set; } = new();
        public string Qualifications { get; set; }
        public string DBSCheckStatus { get; set; }
        public DateTime? DBSCheckExpiryDate { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
    }

    /// <summary>
    /// Staff Update Request DTO
    /// </summary>
    public class StaffUpdateDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string EmploymentStatus { get; set; }
        public List<string> Roles { get; set; }
        public string Qualifications { get; set; }
        public string DBSCheckStatus { get; set; }
        public DateTime? DBSCheckExpiryDate { get; set; }
        public DateTime? SafeguardingTrainingDate { get; set; }
        public DateTime? FirstAidCertExpiryDate { get; set; }
        public DateTime? FireSafetyTrainingDate { get; set; }
        public string SupervisionFrequency { get; set; }
        public DateTime? LastSupervisionDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public int? PerformanceRating { get; set; }
        public DateTime? LastAppraisalDate { get; set; }
        public string ProfessionalMemberships { get; set; }
    }

    /// <summary>
    /// Staff Allocation Data Transfer Object
    /// </summary>
    public class StaffAllocationDTO
    {
        public long Id { get; set; }
        public long StaffId { get; set; }
        public string StaffName { get; set; }
        public string YoungPersonId { get; set; }
        public string YoungPersonName { get; set; }
        public string FacilityId { get; set; }
        public string AllocationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public decimal HoursPerWeek { get; set; }
        public string ShiftPattern { get; set; }
        public string SpecializationAreas { get; set; }
        public string Notes { get; set; }
        public string PerformanceNotes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    /// <summary>
    /// Staff Allocation Create Request DTO
    /// </summary>
    public class StaffAllocationCreateDTO
    {
        public required long StaffId { get; set; }
        public required string YoungPersonId { get; set; }
        public required string FacilityId { get; set; }
        public required string AllocationType { get; set; }
        public required DateTime StartDate { get; set; }
        public string Reason { get; set; }
        public decimal HoursPerWeek { get; set; }
        public string ShiftPattern { get; set; }
        public string SpecializationAreas { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// Staff Allocation Update Request DTO
    /// </summary>
    public class StaffAllocationUpdateDTO
    {
        public string AllocationType { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public decimal HoursPerWeek { get; set; }
        public string ShiftPattern { get; set; }
        public string SpecializationAreas { get; set; }
        public string Notes { get; set; }
        public string PerformanceNotes { get; set; }
    }

    /// <summary>
    /// Staff Performance Summary DTO
    /// </summary>
    public class StaffPerformanceSummaryDTO
    {
        public long StaffId { get; set; }
        public string StaffName { get; set; }
        public int AllocationsCount { get; set; }
        public int ActiveAllocationsCount { get; set; }
        public double AverageHoursPerWeek { get; set; }
        public int? PerformanceRating { get; set; }
        public DateTime? LastAppraisalDate { get; set; }
        public int TrainingsCompleted { get; set; }
        public List<string> CertificationsExpiring { get; set; } = new();
        public string EmploymentStatus { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Facility Staff Summary DTO
    /// </summary>
    public class FacilityStaffSummaryDTO
    {
        public string FacilityId { get; set; }
        public string FacilityName { get; set; }
        public int TotalStaffCount { get; set; }
        public int ActiveStaffCount { get; set; }
        public int InactiveStaffCount { get; set; }
        public List<StaffDTO> Staff { get; set; } = new();
        public int TotalAllocations { get; set; }
        public int ActiveAllocations { get; set; }
        public Dictionary<string, int> StaffByRole { get; set; } = new();
        public Dictionary<string, int> StaffByDepartment { get; set; } = new();
    }
}
