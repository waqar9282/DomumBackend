#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Domain.Enums;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class PhysicalActivityLogService : IPhysicalActivityLogService
    {
        private readonly ApplicationDbContext _context;

        public PhysicalActivityLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreatePhysicalActivityLogAsync(
            string facilityId, string youngPersonId, int activityType, DateTime activityDate,
            string activityDescription, string location, int durationMinutes, string intensity,
            string participants, string supervsingStaff, string performanceLevel, string effortLevel,
            string recordedByUserId, CancellationToken cancellationToken)
        {
            var log = new PhysicalActivityLog
            {
                FacilityId = facilityId,
                YoungPersonId = youngPersonId,
                ActivityType = (ActivityType)activityType,
                ActivityDate = activityDate,
                ActivityDescription = activityDescription,
                Location = location,
                DurationMinutes = durationMinutes,
                Intensity = intensity,
                Participants = participants,
                SupervisingStaff = supervsingStaff,
                PerformanceLevel = performanceLevel,
                Effort = effortLevel,
                RecordedByUserId = recordedByUserId
            };

            _context.PhysicalActivityLogs.Add(log);
            await _context.SaveChangesAsync(cancellationToken);

            return log.Id.ToString();
        }

        public async Task<int> UpdatePhysicalActivityLogAsync(
            string id, string activityDescription, string performanceLevel, string progressNotes,
            CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long logId))
                return 0;

            var log = await _context.PhysicalActivityLogs
                .FirstOrDefaultAsync(p => p.Id == logId, cancellationToken);

            if (log == null)
                return 0;

            log.ActivityDescription = activityDescription;
            log.PerformanceLevel = performanceLevel;
            log.ProgressNotes = progressNotes;

            _context.PhysicalActivityLogs.Update(log);
            await _context.SaveChangesAsync(cancellationToken);

            return 1;
        }

        public async Task<PhysicalActivityLogResponseDTO> GetPhysicalActivityLogByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long logId))
                return null;

            var log = await _context.PhysicalActivityLogs
                .Include(p => p.YoungPerson)
                .FirstOrDefaultAsync(p => p.Id == logId, cancellationToken);

            if (log == null)
                return null;

            return new PhysicalActivityLogResponseDTO
            {
                Id = log.Id.ToString(),
                FacilityId = log.FacilityId,
                YoungPersonId = log.YoungPersonId,
                YoungPersonName = log.YoungPerson?.FullName,
                ActivityType = (int)log.ActivityType,
                ActivityDate = log.ActivityDate,
                ActivityDescription = log.ActivityDescription,
                DurationMinutes = log.DurationMinutes,
                Intensity = log.Intensity
            };
        }

        public async Task<List<PhysicalActivityLogResponseDTO>> GetPhysicalActivityLogsByYoungPersonAsync(string youngPersonId, DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken)
        {
            var query = _context.PhysicalActivityLogs
                .Include(p => p.YoungPerson)
                .Where(p => p.YoungPersonId == youngPersonId);

            if (fromDate.HasValue)
                query = query.Where(p => p.ActivityDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(p => p.ActivityDate <= toDate.Value);

            var logs = await query
                .OrderByDescending(p => p.ActivityDate)
                .ToListAsync(cancellationToken);

            return logs.Select(p => new PhysicalActivityLogResponseDTO
            {
                Id = p.Id.ToString(),
                FacilityId = p.FacilityId,
                YoungPersonId = p.YoungPersonId,
                YoungPersonName = p.YoungPerson?.FullName,
                ActivityType = (int)p.ActivityType,
                ActivityDate = p.ActivityDate,
                ActivityDescription = p.ActivityDescription,
                DurationMinutes = p.DurationMinutes,
                Intensity = p.Intensity
            }).ToList();
        }

        public async Task<List<PhysicalActivityLogResponseDTO>> GetPhysicalActivityLogsByTypeAsync(string youngPersonId, int activityType, CancellationToken cancellationToken)
        {
            var logs = await _context.PhysicalActivityLogs
                .Include(p => p.YoungPerson)
                .Where(p => p.YoungPersonId == youngPersonId && p.ActivityType == (ActivityType)activityType)
                .OrderByDescending(p => p.ActivityDate)
                .ToListAsync(cancellationToken);

            return logs.Select(p => new PhysicalActivityLogResponseDTO
            {
                Id = p.Id.ToString(),
                FacilityId = p.FacilityId,
                YoungPersonId = p.YoungPersonId,
                YoungPersonName = p.YoungPerson?.FullName,
                ActivityType = (int)p.ActivityType,
                ActivityDate = p.ActivityDate,
                ActivityDescription = p.ActivityDescription,
                DurationMinutes = p.DurationMinutes,
                Intensity = p.Intensity
            }).ToList();
        }
    }
}
