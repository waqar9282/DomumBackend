#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class NutritionLogService : INutritionLogService
    {
        private readonly ApplicationDbContext _context;

        public NutritionLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateNutritionLogAsync(
            string facilityId, string youngPersonId, DateTime logDate, string mealType,
            string foodDescription, decimal? portionSize, string appetite, string notes,
            string recordedByUserId, CancellationToken cancellationToken)
        {
            var log = new NutritionLog
            {
                FacilityId = facilityId,
                YoungPersonId = youngPersonId,
                LogDate = logDate,
                MealType = mealType,
                FoodDescription = foodDescription,
                PortionSize = portionSize,
                Appetite = appetite,
                Notes = notes,
                RecordedByUserId = recordedByUserId
            };

            _context.NutritionLogs.Add(log);
            await _context.SaveChangesAsync(cancellationToken);

            return log.Id.ToString();
        }

        public async Task<int> UpdateNutritionLogAsync(
            string id, string foodDescription, string appetite, string notes, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long logId))
                return 0;

            var log = await _context.NutritionLogs
                .FirstOrDefaultAsync(n => n.Id == logId, cancellationToken);

            if (log == null)
                return 0;

            log.FoodDescription = foodDescription;
            log.Appetite = appetite;
            log.Notes = notes;

            _context.NutritionLogs.Update(log);
            await _context.SaveChangesAsync(cancellationToken);

            return 1;
        }

        public async Task<NutritionLogResponseDTO> GetNutritionLogByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long logId))
                return null;

            var log = await _context.NutritionLogs
                .Include(n => n.YoungPerson)
                .FirstOrDefaultAsync(n => n.Id == logId, cancellationToken);

            if (log == null)
                return null;

            return new NutritionLogResponseDTO
            {
                Id = log.Id.ToString(),
                FacilityId = log.FacilityId,
                YoungPersonId = log.YoungPersonId,
                YoungPersonName = log.YoungPerson?.FullName,
                LogDate = log.LogDate,
                MealType = log.MealType,
                FoodDescription = log.FoodDescription,
                PortionSize = log.PortionSize,
                Appetite = log.Appetite
            };
        }

        public async Task<List<NutritionLogResponseDTO>> GetNutritionLogsByYoungPersonAsync(string youngPersonId, DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken)
        {
            var query = _context.NutritionLogs
                .Include(n => n.YoungPerson)
                .Where(n => n.YoungPersonId == youngPersonId);

            if (fromDate.HasValue)
                query = query.Where(n => n.LogDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(n => n.LogDate <= toDate.Value);

            var logs = await query
                .OrderByDescending(n => n.LogDate)
                .ToListAsync(cancellationToken);

            return logs.Select(n => new NutritionLogResponseDTO
            {
                Id = n.Id.ToString(),
                FacilityId = n.FacilityId,
                YoungPersonId = n.YoungPersonId,
                YoungPersonName = n.YoungPerson?.FullName,
                LogDate = n.LogDate,
                MealType = n.MealType,
                FoodDescription = n.FoodDescription,
                PortionSize = n.PortionSize,
                Appetite = n.Appetite
            }).ToList();
        }

        public async Task<List<NutritionLogResponseDTO>> GetNutritionLogsByFacilityAsync(string facilityId, DateTime logDate, CancellationToken cancellationToken)
        {
            var logs = await _context.NutritionLogs
                .Include(n => n.YoungPerson)
                .Where(n => n.FacilityId == facilityId && n.LogDate.Date == logDate.Date)
                .OrderByDescending(n => n.LogDate)
                .ToListAsync(cancellationToken);

            return logs.Select(n => new NutritionLogResponseDTO
            {
                Id = n.Id.ToString(),
                FacilityId = n.FacilityId,
                YoungPersonId = n.YoungPersonId,
                YoungPersonName = n.YoungPerson?.FullName,
                LogDate = n.LogDate,
                MealType = n.MealType,
                FoodDescription = n.FoodDescription,
                PortionSize = n.PortionSize,
                Appetite = n.Appetite
            }).ToList();
        }
    }
}
