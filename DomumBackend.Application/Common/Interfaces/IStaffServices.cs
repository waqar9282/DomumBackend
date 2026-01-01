namespace DomumBackend.Application.Common.Interfaces
{
    using DomumBackend.Application.DTOs;

    /// <summary>
    /// Staff Management Service Interface
    /// </summary>
    public interface IStaffManagementService
    {
        // Staff CRUD Operations
        Task<StaffDTO> CreateStaffAsync(string facilityId, StaffCreateDTO request);
        Task<StaffDTO> GetStaffByIdAsync(long id);
        Task<StaffDTO> GetStaffByUserIdAsync(string facilityId, string userId);
        Task<List<StaffDTO>> GetAllStaffAsync(string facilityId);
        Task<List<StaffDTO>> GetStaffByStatusAsync(string facilityId, string status);
        Task<List<StaffDTO>> GetStaffByDepartmentAsync(string facilityId, string department);
        Task<List<StaffDTO>> GetStaffByRoleAsync(string facilityId, string role);
        Task<StaffDTO> UpdateStaffAsync(long id, StaffUpdateDTO request);
        Task DeactivateStaffAsync(long id, string reason);
        Task DeleteStaffAsync(long id);

        // Staff Allocation Operations
        Task<StaffAllocationDTO> CreateAllocationAsync(string facilityId, StaffAllocationCreateDTO request);
        Task<StaffAllocationDTO> GetAllocationByIdAsync(long id);
        Task<List<StaffAllocationDTO>> GetAllocationsByStaffAsync(long staffId);
        Task<List<StaffAllocationDTO>> GetAllocationsByYoungPersonAsync(string youngPersonId);
        Task<List<StaffAllocationDTO>> GetAllocationsByFacilityAsync(string facilityId);
        Task<List<StaffAllocationDTO>> GetActiveAllocationsAsync(string facilityId);
        Task<StaffAllocationDTO> UpdateAllocationAsync(long id, StaffAllocationUpdateDTO request);
        Task<StaffAllocationDTO> EndAllocationAsync(long id, string reason);
        Task DeleteAllocationAsync(long id);

        // Staff Search & Filtering
        Task<List<StaffDTO>> SearchStaffAsync(string facilityId, string searchTerm);
        Task<List<StaffDTO>> GetStaffWithExpiringCertificationsAsync(string facilityId, int daysThreshold = 30);
        Task<List<StaffDTO>> GetStaffDueForSupervisionAsync(string facilityId);
        Task<List<StaffDTO>> GetStaffDueForAppraisalAsync(string facilityId);

        // Staff Performance & Reporting
        Task<StaffPerformanceSummaryDTO> GetStaffPerformanceSummaryAsync(long staffId);
        Task<FacilityStaffSummaryDTO> GetFacilityStaffSummaryAsync(string facilityId);
        Task<List<StaffDTO>> GetTopPerformersAsync(string facilityId, int count = 10);
        Task<Dictionary<string, int>> GetStaffDistributionByRoleAsync(string facilityId);
        Task<Dictionary<string, int>> GetStaffDistributionByDepartmentAsync(string facilityId);

        // Staff Training & Certification
        Task<bool> UpdateDBSCheckStatusAsync(long staffId, string status, DateTime? expiryDate);
        Task<bool> UpdateSafeguardingTrainingDateAsync(long staffId, DateTime completionDate);
        Task<bool> UpdateFirstAidCertificationAsync(long staffId, DateTime expiryDate);
        Task<bool> UpdateFireSafetyTrainingAsync(long staffId, DateTime completionDate);
        Task<List<StaffDTO>> GetStaffWithValidDBSAsync(string facilityId);
        Task<List<StaffDTO>> GetStaffWithExpiredDBSAsync(string facilityId);

        // Supervision & Appraisal
        Task<bool> RecordSupervisionAsync(long staffId, DateTime supervisionDate, string notes);
        Task<bool> RecordAppraisalAsync(long staffId, int rating, DateTime appraisalDate, string notes);

        // Bulk Operations
        Task<List<StaffDTO>> ImportStaffAsync(string facilityId, List<StaffCreateDTO> staffList);
        Task<bool> BulkUpdateStatusAsync(string facilityId, List<long> staffIds, string newStatus);
    }

    /// <summary>
    /// Staff Allocation Service Interface
    /// </summary>
    public interface IStaffAllocationService
    {
        Task<StaffAllocationDTO> CreateAsync(string facilityId, StaffAllocationCreateDTO request);
        Task<StaffAllocationDTO> GetByIdAsync(long id);
        Task<List<StaffAllocationDTO>> GetByStaffAsync(long staffId);
        Task<List<StaffAllocationDTO>> GetByYoungPersonAsync(string youngPersonId);
        Task<List<StaffAllocationDTO>> GetByFacilityAsync(string facilityId);
        Task<List<StaffAllocationDTO>> GetActiveAsync(string facilityId);
        Task<StaffAllocationDTO> UpdateAsync(long id, StaffAllocationUpdateDTO request);
        Task<StaffAllocationDTO> EndAsync(long id, string reason);
        Task DeleteAsync(long id);
        Task<List<StaffAllocationDTO>> GetByShiftPatternAsync(string facilityId, string shiftPattern);
        Task<int> GetTotalHoursPerWeekAsync(long staffId);
        Task<List<StaffAllocationDTO>> GetOverallocatedStaffAsync(string facilityId, decimal maxHours = 40);
    }
}
