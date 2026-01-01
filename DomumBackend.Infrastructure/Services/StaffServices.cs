namespace DomumBackend.Infrastructure.Services
{
    using DomumBackend.Application.Common.Interfaces;
    using DomumBackend.Application.DTOs;
    using DomumBackend.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using DomumBackend.Infrastructure.Data;

    public class StaffManagementService : IStaffManagementService
    {
        private readonly ApplicationDbContext _context;

        public StaffManagementService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Staff CRUD Operations
        public async Task<StaffDTO> CreateStaffAsync(string facilityId, StaffCreateDTO request)
        {
            var staff = new Staff
            {
                UserId = request.UserId,
                FacilityId = facilityId,
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber ?? "",
                JobTitle = request.JobTitle,
                Department = request.Department ?? "",
                EmploymentStatus = request.EmploymentStatus,
                StartDate = request.StartDate,
                Roles = request.Roles ?? new(),
                Qualifications = request.Qualifications ?? "",
                DBSCheckStatus = request.DBSCheckStatus ?? "NotStarted",
                DBSCheckExpiryDate = request.DBSCheckExpiryDate,
                EmergencyContactName = request.EmergencyContactName ?? "",
                EmergencyContactPhone = request.EmergencyContactPhone ?? "",
                Address = request.Address ?? "",
                Postcode = request.Postcode ?? ""
            };

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
            return MapToDTO(staff);
        }

        public async Task<StaffDTO> GetStaffByIdAsync(long id)
        {
            var staff = await _context.Staff.FindAsync(id);
            return staff != null ? MapToDTO(staff) : throw new InvalidOperationException($"Staff {id} not found");
        }

        public async Task<StaffDTO> GetStaffByUserIdAsync(string facilityId, string userId)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.FacilityId == facilityId && s.UserId == userId);
            return staff != null ? MapToDTO(staff) : throw new InvalidOperationException($"Staff with UserId {userId} not found");
        }

        public async Task<List<StaffDTO>> GetAllStaffAsync(string facilityId)
        {
            var staff = await _context.Staff.Where(s => s.FacilityId == facilityId).ToListAsync();
            return staff.Select(MapToDTO).ToList();
        }

        public async Task<List<StaffDTO>> GetStaffByStatusAsync(string facilityId, string status)
        {
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId && s.EmploymentStatus == status)
                .ToListAsync();
            return staff.Select(MapToDTO).ToList();
        }

        public async Task<List<StaffDTO>> GetStaffByDepartmentAsync(string facilityId, string department)
        {
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId && s.Department == department)
                .ToListAsync();
            return staff.Select(MapToDTO).ToList();
        }

        public async Task<List<StaffDTO>> GetStaffByRoleAsync(string facilityId, string role)
        {
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId && s.Roles.Contains(role))
                .ToListAsync();
            return staff.Select(MapToDTO).ToList();
        }

        public async Task<StaffDTO> UpdateStaffAsync(long id, StaffUpdateDTO request)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) throw new InvalidOperationException($"Staff {id} not found");

            if (!string.IsNullOrEmpty(request.FullName)) staff.FullName = request.FullName;
            if (!string.IsNullOrEmpty(request.Email)) staff.Email = request.Email;
            if (!string.IsNullOrEmpty(request.PhoneNumber)) staff.PhoneNumber = request.PhoneNumber;
            if (!string.IsNullOrEmpty(request.JobTitle)) staff.JobTitle = request.JobTitle;
            if (!string.IsNullOrEmpty(request.Department)) staff.Department = request.Department;
            if (!string.IsNullOrEmpty(request.EmploymentStatus)) staff.EmploymentStatus = request.EmploymentStatus;
            if (request.Roles != null && request.Roles.Any()) staff.Roles = request.Roles;
            if (!string.IsNullOrEmpty(request.Qualifications)) staff.Qualifications = request.Qualifications;
            if (!string.IsNullOrEmpty(request.DBSCheckStatus)) staff.DBSCheckStatus = request.DBSCheckStatus;
            if (request.DBSCheckExpiryDate.HasValue) staff.DBSCheckExpiryDate = request.DBSCheckExpiryDate;
            if (request.SafeguardingTrainingDate.HasValue) staff.SafeguardingTrainingDate = request.SafeguardingTrainingDate;
            if (request.FirstAidCertExpiryDate.HasValue) staff.FirstAidCertExpiryDate = request.FirstAidCertExpiryDate;
            if (request.FireSafetyTrainingDate.HasValue) staff.FireSafetyTrainingDate = request.FireSafetyTrainingDate;
            if (!string.IsNullOrEmpty(request.SupervisionFrequency)) staff.SupervisionFrequency = request.SupervisionFrequency;
            if (request.LastSupervisionDate.HasValue) staff.LastSupervisionDate = request.LastSupervisionDate;
            staff.IsActive = request.IsActive;
            if (!string.IsNullOrEmpty(request.Notes)) staff.Notes = request.Notes;
            if (!string.IsNullOrEmpty(request.EmergencyContactName)) staff.EmergencyContactName = request.EmergencyContactName;
            if (!string.IsNullOrEmpty(request.EmergencyContactPhone)) staff.EmergencyContactPhone = request.EmergencyContactPhone;
            if (!string.IsNullOrEmpty(request.Address)) staff.Address = request.Address;
            if (!string.IsNullOrEmpty(request.Postcode)) staff.Postcode = request.Postcode;
            if (request.PerformanceRating.HasValue) staff.PerformanceRating = request.PerformanceRating;
            if (request.LastAppraisalDate.HasValue) staff.LastAppraisalDate = request.LastAppraisalDate;
            if (!string.IsNullOrEmpty(request.ProfessionalMemberships)) staff.ProfessionalMemberships = request.ProfessionalMemberships;

            await _context.SaveChangesAsync();
            return MapToDTO(staff);
        }

        public async Task DeactivateStaffAsync(long id, string reason)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) throw new InvalidOperationException($"Staff {id} not found");

            staff.IsActive = false;
            staff.EmploymentStatus = "Inactive";
            staff.EndDate = DateTime.UtcNow;
            staff.Notes = $"Deactivated: {reason}";

            await _context.SaveChangesAsync();
        }

        public async Task DeleteStaffAsync(long id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) throw new InvalidOperationException($"Staff {id} not found");

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
        }

        // Staff Allocation Operations
        public async Task<StaffAllocationDTO> CreateAllocationAsync(string facilityId, StaffAllocationCreateDTO request)
        {
            var allocation = new StaffAllocation
            {
                StaffId = request.StaffId,
                YoungPersonId = request.YoungPersonId,
                FacilityId = facilityId,
                AllocationType = request.AllocationType,
                StartDate = request.StartDate,
                Reason = request.Reason ?? "",
                HoursPerWeek = request.HoursPerWeek,
                ShiftPattern = request.ShiftPattern ?? "",
                SpecializationAreas = request.SpecializationAreas ?? "",
                Notes = request.Notes ?? "",
                CreatedByUserId = "system"
            };

            _context.StaffAllocations.Add(allocation);
            await _context.SaveChangesAsync();
            return await MapToDTOAsync(allocation);
        }

        public async Task<StaffAllocationDTO> GetAllocationByIdAsync(long id)
        {
            var allocation = await _context.StaffAllocations.FindAsync(id);
            return allocation != null ? await MapToDTOAsync(allocation) : throw new InvalidOperationException($"Allocation {id} not found");
        }

        public async Task<List<StaffAllocationDTO>> GetAllocationsByStaffAsync(long staffId)
        {
            var allocations = await _context.StaffAllocations
                .Where(a => a.StaffId == staffId)
                .ToListAsync();
            var dtos = new List<StaffAllocationDTO>();
            foreach (var a in allocations)
                dtos.Add(await MapToDTOAsync(a));
            return dtos;
        }

        public async Task<List<StaffAllocationDTO>> GetAllocationsByYoungPersonAsync(string youngPersonId)
        {
            var allocations = await _context.StaffAllocations
                .Where(a => a.YoungPersonId == youngPersonId)
                .ToListAsync();
            var dtos = new List<StaffAllocationDTO>();
            foreach (var a in allocations)
                dtos.Add(await MapToDTOAsync(a));
            return dtos;
        }

        public async Task<List<StaffAllocationDTO>> GetAllocationsByFacilityAsync(string facilityId)
        {
            var allocations = await _context.StaffAllocations
                .Where(a => a.FacilityId == facilityId)
                .ToListAsync();
            var dtos = new List<StaffAllocationDTO>();
            foreach (var a in allocations)
                dtos.Add(await MapToDTOAsync(a));
            return dtos;
        }

        public async Task<List<StaffAllocationDTO>> GetActiveAllocationsAsync(string facilityId)
        {
            var allocations = await _context.StaffAllocations
                .Where(a => a.FacilityId == facilityId && a.IsActive)
                .ToListAsync();
            var dtos = new List<StaffAllocationDTO>();
            foreach (var a in allocations)
                dtos.Add(await MapToDTOAsync(a));
            return dtos;
        }

        public async Task<StaffAllocationDTO> UpdateAllocationAsync(long id, StaffAllocationUpdateDTO request)
        {
            var allocation = await _context.StaffAllocations.FindAsync(id);
            if (allocation == null) throw new InvalidOperationException($"Allocation {id} not found");

            if (!string.IsNullOrEmpty(request.AllocationType)) allocation.AllocationType = request.AllocationType;
            if (request.EndDate.HasValue) allocation.EndDate = request.EndDate;
            if (!string.IsNullOrEmpty(request.Reason)) allocation.Reason = request.Reason;
            allocation.IsActive = request.IsActive;
            if (request.HoursPerWeek > 0) allocation.HoursPerWeek = request.HoursPerWeek;
            if (!string.IsNullOrEmpty(request.ShiftPattern)) allocation.ShiftPattern = request.ShiftPattern;
            if (!string.IsNullOrEmpty(request.SpecializationAreas)) allocation.SpecializationAreas = request.SpecializationAreas;
            if (!string.IsNullOrEmpty(request.Notes)) allocation.Notes = request.Notes;
            if (!string.IsNullOrEmpty(request.PerformanceNotes)) allocation.PerformanceNotes = request.PerformanceNotes;

            await _context.SaveChangesAsync();
            return await MapToDTOAsync(allocation);
        }

        public async Task<StaffAllocationDTO> EndAllocationAsync(long id, string reason)
        {
            var allocation = await _context.StaffAllocations.FindAsync(id);
            if (allocation == null) throw new InvalidOperationException($"Allocation {id} not found");

            allocation.EndDate = DateTime.UtcNow;
            allocation.IsActive = false;
            allocation.Notes = $"Ended: {reason}";

            await _context.SaveChangesAsync();
            return await MapToDTOAsync(allocation);
        }

        public async Task DeleteAllocationAsync(long id)
        {
            var allocation = await _context.StaffAllocations.FindAsync(id);
            if (allocation == null) throw new InvalidOperationException($"Allocation {id} not found");

            _context.StaffAllocations.Remove(allocation);
            await _context.SaveChangesAsync();
        }

        // Staff Search & Filtering
        public async Task<List<StaffDTO>> SearchStaffAsync(string facilityId, string searchTerm)
        {
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId &&
                    (s.FullName.Contains(searchTerm) || s.Email.Contains(searchTerm) || s.JobTitle.Contains(searchTerm)))
                .ToListAsync();
            return staff.Select(MapToDTO).ToList();
        }

        public async Task<List<StaffDTO>> GetStaffWithExpiringCertificationsAsync(string facilityId, int daysThreshold = 30)
        {
            var cutoffDate = DateTime.UtcNow.AddDays(daysThreshold);
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId && s.IsActive &&
                    ((s.DBSCheckExpiryDate.HasValue && s.DBSCheckExpiryDate < cutoffDate) ||
                     (s.FirstAidCertExpiryDate.HasValue && s.FirstAidCertExpiryDate < cutoffDate)))
                .ToListAsync();
            return staff.Select(MapToDTO).ToList();
        }

        public async Task<List<StaffDTO>> GetStaffDueForSupervisionAsync(string facilityId)
        {
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId && s.IsActive &&
                    (s.LastSupervisionDate == null || s.LastSupervisionDate < thirtyDaysAgo))
                .ToListAsync();
            return staff.Select(MapToDTO).ToList();
        }

        public async Task<List<StaffDTO>> GetStaffDueForAppraisalAsync(string facilityId)
        {
            var oneYearAgo = DateTime.UtcNow.AddYears(-1);
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId && s.IsActive &&
                    (s.LastAppraisalDate == null || s.LastAppraisalDate < oneYearAgo))
                .ToListAsync();
            return staff.Select(MapToDTO).ToList();
        }

        // Staff Performance & Reporting
        public async Task<StaffPerformanceSummaryDTO> GetStaffPerformanceSummaryAsync(long staffId)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null) throw new InvalidOperationException($"Staff {staffId} not found");

            var allocations = await _context.StaffAllocations.Where(a => a.StaffId == staffId).ToListAsync();
            var activeAllocations = allocations.Where(a => a.IsActive).ToList();
            var totalHours = allocations.Sum(a => a.HoursPerWeek);

            var expiringCerts = new List<string>();
            if (staff.DBSCheckExpiryDate.HasValue && staff.DBSCheckExpiryDate < DateTime.UtcNow.AddDays(30))
                expiringCerts.Add("DBS");
            if (staff.FirstAidCertExpiryDate.HasValue && staff.FirstAidCertExpiryDate < DateTime.UtcNow.AddDays(30))
                expiringCerts.Add("First Aid");

            return new StaffPerformanceSummaryDTO
            {
                StaffId = staffId,
                StaffName = staff.FullName,
                AllocationsCount = allocations.Count,
                ActiveAllocationsCount = activeAllocations.Count,
                AverageHoursPerWeek = totalHours > 0 ? (double)(totalHours / Math.Max(1, allocations.Count)) : 0,
                PerformanceRating = staff.PerformanceRating,
                LastAppraisalDate = staff.LastAppraisalDate,
                TrainingsCompleted = new[] { staff.SafeguardingTrainingDate, staff.FirstAidCertExpiryDate, staff.FireSafetyTrainingDate }
                    .Count(d => d.HasValue),
                CertificationsExpiring = expiringCerts,
                EmploymentStatus = staff.EmploymentStatus,
                IsActive = staff.IsActive
            };
        }

        public async Task<FacilityStaffSummaryDTO> GetFacilityStaffSummaryAsync(string facilityId)
        {
            var facility = await _context.Facilities.FindAsync(facilityId);
            var staff = await _context.Staff.Where(s => s.FacilityId == facilityId).ToListAsync();
            var allocations = await _context.StaffAllocations.Where(a => a.FacilityId == facilityId).ToListAsync();

            var activeStaff = staff.Where(s => s.IsActive).ToList();
            var inactiveStaff = staff.Where(s => !s.IsActive).ToList();

            var staffByRole = new Dictionary<string, int>();
            var staffByDept = new Dictionary<string, int>();

            foreach (var s in staff)
            {
                foreach (var role in s.Roles)
                {
                    if (staffByRole.ContainsKey(role))
                        staffByRole[role]++;
                    else
                        staffByRole[role] = 1;
                }

                if (!string.IsNullOrEmpty(s.Department))
                {
                    if (staffByDept.ContainsKey(s.Department))
                        staffByDept[s.Department]++;
                    else
                        staffByDept[s.Department] = 1;
                }
            }

            return new FacilityStaffSummaryDTO
            {
                FacilityId = facilityId,
                FacilityName = facility?.Name ?? "Unknown",
                TotalStaffCount = staff.Count,
                ActiveStaffCount = activeStaff.Count,
                InactiveStaffCount = inactiveStaff.Count,
                Staff = staff.Select(MapToDTO).ToList(),
                TotalAllocations = allocations.Count,
                ActiveAllocations = allocations.Count(a => a.IsActive),
                StaffByRole = staffByRole,
                StaffByDepartment = staffByDept
            };
        }

        public async Task<List<StaffDTO>> GetTopPerformersAsync(string facilityId, int count = 10)
        {
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId && s.IsActive && s.PerformanceRating.HasValue)
                .OrderByDescending(s => s.PerformanceRating)
                .Take(count)
                .ToListAsync();
            return staff.Select(MapToDTO).ToList();
        }

        public async Task<Dictionary<string, int>> GetStaffDistributionByRoleAsync(string facilityId)
        {
            var staff = await _context.Staff.Where(s => s.FacilityId == facilityId).ToListAsync();
            var distribution = new Dictionary<string, int>();

            foreach (var s in staff)
            {
                foreach (var role in s.Roles)
                {
                    if (distribution.ContainsKey(role))
                        distribution[role]++;
                    else
                        distribution[role] = 1;
                }
            }

            return distribution;
        }

        public async Task<Dictionary<string, int>> GetStaffDistributionByDepartmentAsync(string facilityId)
        {
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId)
                .GroupBy(s => s.Department)
                .Select(g => new { Department = g.Key, Count = g.Count() })
                .ToListAsync();

            return staff.ToDictionary(x => x.Department ?? "Unassigned", x => x.Count);
        }

        // Staff Training & Certification
        public async Task<bool> UpdateDBSCheckStatusAsync(long staffId, string status, DateTime? expiryDate)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null) return false;

            staff.DBSCheckStatus = status;
            staff.DBSCheckExpiryDate = expiryDate;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSafeguardingTrainingDateAsync(long staffId, DateTime completionDate)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null) return false;

            staff.SafeguardingTrainingDate = completionDate;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateFirstAidCertificationAsync(long staffId, DateTime expiryDate)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null) return false;

            staff.FirstAidCertExpiryDate = expiryDate;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateFireSafetyTrainingAsync(long staffId, DateTime completionDate)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null) return false;

            staff.FireSafetyTrainingDate = completionDate;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<StaffDTO>> GetStaffWithValidDBSAsync(string facilityId)
        {
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId && s.IsActive &&
                    s.DBSCheckStatus == "Cleared" &&
                    (s.DBSCheckExpiryDate == null || s.DBSCheckExpiryDate > DateTime.UtcNow))
                .ToListAsync();
            return staff.Select(MapToDTO).ToList();
        }

        public async Task<List<StaffDTO>> GetStaffWithExpiredDBSAsync(string facilityId)
        {
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId && s.IsActive &&
                    s.DBSCheckExpiryDate.HasValue && s.DBSCheckExpiryDate <= DateTime.UtcNow)
                .ToListAsync();
            return staff.Select(MapToDTO).ToList();
        }

        // Supervision & Appraisal
        public async Task<bool> RecordSupervisionAsync(long staffId, DateTime supervisionDate, string notes)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null) return false;

            staff.LastSupervisionDate = supervisionDate;
            if (!string.IsNullOrEmpty(notes))
                staff.Notes = notes;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RecordAppraisalAsync(long staffId, int rating, DateTime appraisalDate, string notes)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null) return false;

            staff.PerformanceRating = rating;
            staff.LastAppraisalDate = appraisalDate;
            if (!string.IsNullOrEmpty(notes))
                staff.Notes = notes;
            await _context.SaveChangesAsync();
            return true;
        }

        // Bulk Operations
        public async Task<List<StaffDTO>> ImportStaffAsync(string facilityId, List<StaffCreateDTO> staffList)
        {
            var results = new List<StaffDTO>();
            foreach (var staffDto in staffList)
            {
                try
                {
                    var created = await CreateStaffAsync(facilityId, staffDto);
                    results.Add(created);
                }
                catch { /* skip failed imports */ }
            }
            return results;
        }

        public async Task<bool> BulkUpdateStatusAsync(string facilityId, List<long> staffIds, string newStatus)
        {
            var staff = await _context.Staff
                .Where(s => s.FacilityId == facilityId && staffIds.Contains(s.Id))
                .ToListAsync();

            foreach (var s in staff)
                s.EmploymentStatus = newStatus;

            await _context.SaveChangesAsync();
            return true;
        }

        private StaffDTO MapToDTO(Staff staff) =>
            new StaffDTO
            {
                Id = staff.Id,
                UserId = staff.UserId,
                FacilityId = staff.FacilityId,
                FullName = staff.FullName,
                Email = staff.Email,
                PhoneNumber = staff.PhoneNumber,
                JobTitle = staff.JobTitle,
                Department = staff.Department,
                EmploymentStatus = staff.EmploymentStatus,
                StartDate = staff.StartDate,
                EndDate = staff.EndDate,
                Roles = staff.Roles,
                Qualifications = staff.Qualifications,
                DBSCheckStatus = staff.DBSCheckStatus,
                DBSCheckExpiryDate = staff.DBSCheckExpiryDate,
                SafeguardingTrainingDate = staff.SafeguardingTrainingDate,
                FirstAidCertExpiryDate = staff.FirstAidCertExpiryDate,
                FireSafetyTrainingDate = staff.FireSafetyTrainingDate,
                SupervisionFrequency = staff.SupervisionFrequency,
                LastSupervisionDate = staff.LastSupervisionDate,
                IsActive = staff.IsActive,
                Notes = staff.Notes,
                EmergencyContactName = staff.EmergencyContactName,
                EmergencyContactPhone = staff.EmergencyContactPhone,
                Address = staff.Address,
                Postcode = staff.Postcode,
                PerformanceRating = staff.PerformanceRating,
                LastAppraisalDate = staff.LastAppraisalDate,
                ProfessionalMemberships = staff.ProfessionalMemberships,
                CreatedDate = staff.CreatedDate,
                ModifiedDate = staff.ModifiedDate
            };

        private async Task<StaffAllocationDTO> MapToDTOAsync(StaffAllocation allocation)
        {
            var staff = await _context.Staff.FindAsync(allocation.StaffId);
            var youngPerson = await _context.YoungPeople.FindAsync(allocation.YoungPersonId);

            return new StaffAllocationDTO
            {
                Id = allocation.Id,
                StaffId = allocation.StaffId,
                StaffName = staff?.FullName ?? "Unknown",
                YoungPersonId = allocation.YoungPersonId,
                YoungPersonName = youngPerson?.FullName ?? "Unknown",
                FacilityId = allocation.FacilityId,
                AllocationType = allocation.AllocationType,
                StartDate = allocation.StartDate,
                EndDate = allocation.EndDate,
                Reason = allocation.Reason,
                IsActive = allocation.IsActive,
                HoursPerWeek = allocation.HoursPerWeek,
                ShiftPattern = allocation.ShiftPattern,
                SpecializationAreas = allocation.SpecializationAreas,
                Notes = allocation.Notes,
                PerformanceNotes = allocation.PerformanceNotes,
                CreatedDate = allocation.CreatedDate,
                ModifiedDate = allocation.ModifiedDate
            };
        }
    }

    /// <summary>
    /// Staff Allocation Service - Dedicated allocation operations
    /// </summary>
    public class StaffAllocationService : IStaffAllocationService
    {
        private readonly ApplicationDbContext _context;

        public StaffAllocationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StaffAllocationDTO> CreateAsync(string facilityId, StaffAllocationCreateDTO request)
        {
            var allocation = new StaffAllocation
            {
                StaffId = request.StaffId,
                YoungPersonId = request.YoungPersonId,
                FacilityId = facilityId,
                AllocationType = request.AllocationType,
                StartDate = request.StartDate,
                Reason = request.Reason ?? "",
                HoursPerWeek = request.HoursPerWeek,
                ShiftPattern = request.ShiftPattern ?? "",
                SpecializationAreas = request.SpecializationAreas ?? "",
                Notes = request.Notes ?? "",
                CreatedByUserId = "system"
            };

            _context.StaffAllocations.Add(allocation);
            await _context.SaveChangesAsync();
            return await MapToDTOAsync(allocation);
        }

        public async Task<StaffAllocationDTO> GetByIdAsync(long id)
        {
            var allocation = await _context.StaffAllocations.FindAsync(id);
            return allocation != null ? await MapToDTOAsync(allocation) : throw new InvalidOperationException($"Allocation {id} not found");
        }

        public async Task<List<StaffAllocationDTO>> GetByStaffAsync(long staffId)
        {
            var allocations = await _context.StaffAllocations.Where(a => a.StaffId == staffId).ToListAsync();
            var dtos = new List<StaffAllocationDTO>();
            foreach (var a in allocations)
                dtos.Add(await MapToDTOAsync(a));
            return dtos;
        }

        public async Task<List<StaffAllocationDTO>> GetByYoungPersonAsync(string youngPersonId)
        {
            var allocations = await _context.StaffAllocations.Where(a => a.YoungPersonId == youngPersonId).ToListAsync();
            var dtos = new List<StaffAllocationDTO>();
            foreach (var a in allocations)
                dtos.Add(await MapToDTOAsync(a));
            return dtos;
        }

        public async Task<List<StaffAllocationDTO>> GetByFacilityAsync(string facilityId)
        {
            var allocations = await _context.StaffAllocations.Where(a => a.FacilityId == facilityId).ToListAsync();
            var dtos = new List<StaffAllocationDTO>();
            foreach (var a in allocations)
                dtos.Add(await MapToDTOAsync(a));
            return dtos;
        }

        public async Task<List<StaffAllocationDTO>> GetActiveAsync(string facilityId)
        {
            var allocations = await _context.StaffAllocations.Where(a => a.FacilityId == facilityId && a.IsActive).ToListAsync();
            var dtos = new List<StaffAllocationDTO>();
            foreach (var a in allocations)
                dtos.Add(await MapToDTOAsync(a));
            return dtos;
        }

        public async Task<StaffAllocationDTO> UpdateAsync(long id, StaffAllocationUpdateDTO request)
        {
            var allocation = await _context.StaffAllocations.FindAsync(id);
            if (allocation == null) throw new InvalidOperationException($"Allocation {id} not found");

            if (!string.IsNullOrEmpty(request.AllocationType)) allocation.AllocationType = request.AllocationType;
            if (request.EndDate.HasValue) allocation.EndDate = request.EndDate;
            if (!string.IsNullOrEmpty(request.Reason)) allocation.Reason = request.Reason;
            allocation.IsActive = request.IsActive;
            if (request.HoursPerWeek > 0) allocation.HoursPerWeek = request.HoursPerWeek;
            if (!string.IsNullOrEmpty(request.ShiftPattern)) allocation.ShiftPattern = request.ShiftPattern;
            if (!string.IsNullOrEmpty(request.SpecializationAreas)) allocation.SpecializationAreas = request.SpecializationAreas;
            if (!string.IsNullOrEmpty(request.Notes)) allocation.Notes = request.Notes;
            if (!string.IsNullOrEmpty(request.PerformanceNotes)) allocation.PerformanceNotes = request.PerformanceNotes;

            await _context.SaveChangesAsync();
            return await MapToDTOAsync(allocation);
        }

        public async Task<StaffAllocationDTO> EndAsync(long id, string reason)
        {
            var allocation = await _context.StaffAllocations.FindAsync(id);
            if (allocation == null) throw new InvalidOperationException($"Allocation {id} not found");

            allocation.EndDate = DateTime.UtcNow;
            allocation.IsActive = false;
            allocation.Notes = $"Ended: {reason}";

            await _context.SaveChangesAsync();
            return await MapToDTOAsync(allocation);
        }

        public async Task DeleteAsync(long id)
        {
            var allocation = await _context.StaffAllocations.FindAsync(id);
            if (allocation == null) throw new InvalidOperationException($"Allocation {id} not found");

            _context.StaffAllocations.Remove(allocation);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StaffAllocationDTO>> GetByShiftPatternAsync(string facilityId, string shiftPattern)
        {
            var allocations = await _context.StaffAllocations
                .Where(a => a.FacilityId == facilityId && a.ShiftPattern == shiftPattern && a.IsActive)
                .ToListAsync();
            var dtos = new List<StaffAllocationDTO>();
            foreach (var a in allocations)
                dtos.Add(await MapToDTOAsync(a));
            return dtos;
        }

        public async Task<int> GetTotalHoursPerWeekAsync(long staffId)
        {
            var totalHours = await _context.StaffAllocations
                .Where(a => a.StaffId == staffId && a.IsActive)
                .SumAsync(a => (int)a.HoursPerWeek);
            return totalHours;
        }

        public async Task<List<StaffAllocationDTO>> GetOverallocatedStaffAsync(string facilityId, decimal maxHours = 40)
        {
            var allocations = await _context.StaffAllocations
                .Where(a => a.FacilityId == facilityId && a.IsActive)
                .GroupBy(a => a.StaffId)
                .Where(g => g.Sum(a => a.HoursPerWeek) > maxHours)
                .SelectMany(g => g)
                .ToListAsync();

            var dtos = new List<StaffAllocationDTO>();
            foreach (var a in allocations)
                dtos.Add(await MapToDTOAsync(a));
            return dtos;
        }

        private async Task<StaffAllocationDTO> MapToDTOAsync(StaffAllocation allocation)
        {
            var staff = await _context.Staff.FindAsync(allocation.StaffId);
            var youngPerson = await _context.YoungPeople.FindAsync(allocation.YoungPersonId);

            return new StaffAllocationDTO
            {
                Id = allocation.Id,
                StaffId = allocation.StaffId,
                StaffName = staff?.FullName ?? "Unknown",
                YoungPersonId = allocation.YoungPersonId,
                YoungPersonName = youngPerson?.FullName ?? "Unknown",
                FacilityId = allocation.FacilityId,
                AllocationType = allocation.AllocationType,
                StartDate = allocation.StartDate,
                EndDate = allocation.EndDate,
                Reason = allocation.Reason,
                IsActive = allocation.IsActive,
                HoursPerWeek = allocation.HoursPerWeek,
                ShiftPattern = allocation.ShiftPattern,
                SpecializationAreas = allocation.SpecializationAreas,
                Notes = allocation.Notes,
                PerformanceNotes = allocation.PerformanceNotes,
                CreatedDate = allocation.CreatedDate,
                ModifiedDate = allocation.ModifiedDate
            };
        }
    }
}
