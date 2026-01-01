namespace DomumBackend.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StaffManagementController : ControllerBase
{
    private readonly IStaffManagementService _staffService;
    private readonly IStaffAllocationService _allocationService;

    public StaffManagementController(
        IStaffManagementService staffService,
        IStaffAllocationService allocationService)
    {
        _staffService = staffService;
        _allocationService = allocationService;
    }

    private string GetFacilityId()
    {
        var facilityId = User.FindFirst("FacilityId");
        return facilityId?.Value ?? string.Empty;
    }

    #region Staff Management

    [HttpPost("staff")]
    public async Task<IActionResult> CreateStaff([FromBody] StaffCreateDTO request)
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.CreateStaffAsync(facilityId, request);
        return CreatedAtAction(nameof(GetStaffById), new { id = result.Id }, result);
    }

    [HttpGet("staff/{id}")]
    public async Task<IActionResult> GetStaffById(long id)
    {
        var result = await _staffService.GetStaffByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("staff/user/{userId}")]
    public async Task<IActionResult> GetStaffByUserId(string userId)
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetStaffByUserIdAsync(facilityId, userId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("staff")]
    public async Task<IActionResult> GetAllStaff()
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetAllStaffAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("staff/status/{status}")]
    public async Task<IActionResult> GetStaffByStatus(string status)
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetStaffByStatusAsync(facilityId, status);
        return Ok(result);
    }

    [HttpGet("staff/department/{department}")]
    public async Task<IActionResult> GetStaffByDepartment(string department)
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetStaffByDepartmentAsync(facilityId, department);
        return Ok(result);
    }

    [HttpGet("staff/role/{role}")]
    public async Task<IActionResult> GetStaffByRole(string role)
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetStaffByRoleAsync(facilityId, role);
        return Ok(result);
    }

    [HttpPut("staff/{id}")]
    public async Task<IActionResult> UpdateStaff(long id, [FromBody] StaffUpdateDTO request)
    {
        var result = await _staffService.UpdateStaffAsync(id, request);
        return Ok(result);
    }

    [HttpPut("staff/{id}/deactivate")]
    public async Task<IActionResult> DeactivateStaff(long id, [FromBody] string reason)
    {
        await _staffService.DeactivateStaffAsync(id, reason);
        return NoContent();
    }

    [HttpDelete("staff/{id}")]
    public async Task<IActionResult> DeleteStaff(long id)
    {
        await _staffService.DeleteStaffAsync(id);
        return NoContent();
    }

    [HttpGet("staff/search")]
    public async Task<IActionResult> SearchStaff([FromQuery] string searchTerm)
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.SearchStaffAsync(facilityId, searchTerm);
        return Ok(result);
    }

    [HttpGet("staff/expiring-certifications")]
    public async Task<IActionResult> GetStaffWithExpiringCertifications([FromQuery] int daysThreshold = 30)
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetStaffWithExpiringCertificationsAsync(facilityId, daysThreshold);
        return Ok(result);
    }

    [HttpGet("staff/due-for-supervision")]
    public async Task<IActionResult> GetStaffDueForSupervision()
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetStaffDueForSupervisionAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("staff/due-for-appraisal")]
    public async Task<IActionResult> GetStaffDueForAppraisal()
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetStaffDueForAppraisalAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("staff/{id}/performance-summary")]
    public async Task<IActionResult> GetStaffPerformanceSummary(long id)
    {
        var result = await _staffService.GetStaffPerformanceSummaryAsync(id);
        return Ok(result);
    }

    [HttpGet("facility-summary")]
    public async Task<IActionResult> GetFacilityStaffSummary()
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetFacilityStaffSummaryAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("staff/top-performers")]
    public async Task<IActionResult> GetTopPerformers([FromQuery] int count = 10)
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetTopPerformersAsync(facilityId, count);
        return Ok(result);
    }

    [HttpGet("staff/distribution/role")]
    public async Task<IActionResult> GetStaffDistributionByRole()
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetStaffDistributionByRoleAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("staff/distribution/department")]
    public async Task<IActionResult> GetStaffDistributionByDepartment()
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetStaffDistributionByDepartmentAsync(facilityId);
        return Ok(result);
    }

    [HttpPut("staff/{id}/dbs")]
    public async Task<IActionResult> UpdateDBSCheckStatus(long id, [FromBody] dynamic request)
    {
        await _staffService.UpdateDBSCheckStatusAsync(id, request.status, request.expiryDate);
        return Ok();
    }

    [HttpPut("staff/{id}/safeguarding-training")]
    public async Task<IActionResult> UpdateSafeguardingTraining(long id, [FromBody] DateTime completionDate)
    {
        await _staffService.UpdateSafeguardingTrainingDateAsync(id, completionDate);
        return Ok();
    }

    [HttpPut("staff/{id}/first-aid")]
    public async Task<IActionResult> UpdateFirstAidCertification(long id, [FromBody] DateTime expiryDate)
    {
        await _staffService.UpdateFirstAidCertificationAsync(id, expiryDate);
        return Ok();
    }

    [HttpPut("staff/{id}/fire-safety-training")]
    public async Task<IActionResult> UpdateFireSafetyTraining(long id, [FromBody] DateTime completionDate)
    {
        await _staffService.UpdateFireSafetyTrainingAsync(id, completionDate);
        return Ok();
    }

    [HttpGet("staff/valid-dbs")]
    public async Task<IActionResult> GetStaffWithValidDBS()
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetStaffWithValidDBSAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("staff/expired-dbs")]
    public async Task<IActionResult> GetStaffWithExpiredDBS()
    {
        var facilityId = GetFacilityId();
        var result = await _staffService.GetStaffWithExpiredDBSAsync(facilityId);
        return Ok(result);
    }

    [HttpPost("staff/{id}/record-supervision")]
    public async Task<IActionResult> RecordSupervision(long id, [FromBody] dynamic request)
    {
        await _staffService.RecordSupervisionAsync(id, request.supervisionDate, request.notes);
        return Ok();
    }

    [HttpPost("staff/{id}/record-appraisal")]
    public async Task<IActionResult> RecordAppraisal(long id, [FromBody] dynamic request)
    {
        await _staffService.RecordAppraisalAsync(id, request.rating, request.appraisalDate, request.notes);
        return Ok();
    }

    #endregion

    #region Staff Allocations

    [HttpPost("allocations")]
    public async Task<IActionResult> CreateAllocation([FromBody] StaffAllocationCreateDTO request)
    {
        var facilityId = GetFacilityId();
        var result = await _allocationService.CreateAsync(facilityId, request);
        return CreatedAtAction(nameof(GetAllocationById), new { id = result.Id }, result);
    }

    [HttpGet("allocations/{id}")]
    public async Task<IActionResult> GetAllocationById(long id)
    {
        var result = await _allocationService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("allocations/staff/{staffId}")]
    public async Task<IActionResult> GetAllocationsByStaff(long staffId)
    {
        var result = await _allocationService.GetByStaffAsync(staffId);
        return Ok(result);
    }

    [HttpGet("allocations/young-person/{youngPersonId}")]
    public async Task<IActionResult> GetAllocationsByYoungPerson(string youngPersonId)
    {
        var result = await _allocationService.GetByYoungPersonAsync(youngPersonId);
        return Ok(result);
    }

    [HttpGet("allocations")]
    public async Task<IActionResult> GetAllocationsByFacility()
    {
        var facilityId = GetFacilityId();
        var result = await _allocationService.GetByFacilityAsync(facilityId);
        return Ok(result);
    }

    [HttpGet("allocations/active")]
    public async Task<IActionResult> GetActiveAllocations()
    {
        var facilityId = GetFacilityId();
        var result = await _allocationService.GetActiveAsync(facilityId);
        return Ok(result);
    }

    [HttpPut("allocations/{id}")]
    public async Task<IActionResult> UpdateAllocation(long id, [FromBody] StaffAllocationUpdateDTO request)
    {
        var result = await _allocationService.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpPut("allocations/{id}/end")]
    public async Task<IActionResult> EndAllocation(long id, [FromBody] string reason)
    {
        var result = await _allocationService.EndAsync(id, reason);
        return Ok(result);
    }

    [HttpDelete("allocations/{id}")]
    public async Task<IActionResult> DeleteAllocation(long id)
    {
        await _allocationService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("allocations/shift/{shiftPattern}")]
    public async Task<IActionResult> GetAllocationsByShiftPattern(string shiftPattern)
    {
        var facilityId = GetFacilityId();
        var result = await _allocationService.GetByShiftPatternAsync(facilityId, shiftPattern);
        return Ok(result);
    }

    [HttpGet("allocations/staff/{staffId}/hours")]
    public async Task<IActionResult> GetTotalHoursPerWeek(long staffId)
    {
        var result = await _allocationService.GetTotalHoursPerWeekAsync(staffId);
        return Ok(new { staffId, totalHoursPerWeek = result });
    }

    [HttpGet("allocations/overallocated")]
    public async Task<IActionResult> GetOverallocatedStaff([FromQuery] decimal maxHours = 40)
    {
        var facilityId = GetFacilityId();
        var result = await _allocationService.GetOverallocatedStaffAsync(facilityId, maxHours);
        return Ok(result);
    }

    #endregion
}
