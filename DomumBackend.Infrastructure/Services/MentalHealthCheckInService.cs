#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Domain.Enums;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class MentalHealthCheckInService : IMentalHealthCheckInService
    {
        private readonly ApplicationDbContext _context;

        public MentalHealthCheckInService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateMentalHealthCheckInAsync(
            string facilityId, string youngPersonId, int currentMood, string moodDescription,
            string emotionalState, int? moodScore, string sleepQuality, string energyLevel,
            string currentConcerns, string coping, string supportSources, bool hasSuicidalThoughts,
            bool isSelfHarming, string staffObservations, string checkInByUserId, bool isConfidential,
            CancellationToken cancellationToken)
        {
            var checkIn = new MentalHealthCheckIn
            {
                FacilityId = facilityId,
                YoungPersonId = youngPersonId,
                CurrentMood = (MoodLevel)currentMood,
                MoodDescription = moodDescription,
                EmotionalState = emotionalState,
                MoodScore = moodScore,
                SleepQuality = sleepQuality,
                EnergyLevel = energyLevel,
                CurrentConcerns = currentConcerns,
                CopingStrategies = coping,
                SupportSources = supportSources,
                HasSuicidalThoughts = hasSuicidalThoughts,
                IsSelfHarming = isSelfHarming,
                StaffObservations = staffObservations,
                CheckInByUserId = checkInByUserId,
                IsConfidential = isConfidential
            };

            _context.MentalHealthCheckIns.Add(checkIn);
            await _context.SaveChangesAsync(cancellationToken);

            return checkIn.Id.ToString();
        }

        public async Task<int> UpdateMentalHealthCheckInAsync(
            string id, int currentMood, string staffObservations, string actionsRequired,
            bool referralRequired, string referralDetails, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long checkInId))
                return 0;

            var checkIn = await _context.MentalHealthCheckIns
                .FirstOrDefaultAsync(m => m.Id == checkInId, cancellationToken);

            if (checkIn == null)
                return 0;

            checkIn.CurrentMood = (MoodLevel)currentMood;
            checkIn.StaffObservations = staffObservations;
            checkIn.ActionsRequired = actionsRequired;
            checkIn.ReferralToMentalHealthRequired = referralRequired;
            checkIn.ReferralDetails = referralDetails;

            _context.MentalHealthCheckIns.Update(checkIn);
            await _context.SaveChangesAsync(cancellationToken);

            return 1;
        }

        public async Task<MentalHealthCheckInResponseDTO> GetMentalHealthCheckInByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long checkInId))
                return null;

            var checkIn = await _context.MentalHealthCheckIns
                .Include(m => m.YoungPerson)
                .FirstOrDefaultAsync(m => m.Id == checkInId, cancellationToken);

            if (checkIn == null)
                return null;

            return new MentalHealthCheckInResponseDTO
            {
                Id = checkIn.Id.ToString(),
                FacilityId = checkIn.FacilityId,
                YoungPersonId = checkIn.YoungPersonId,
                YoungPersonName = checkIn.YoungPerson?.FullName,
                CurrentMood = (int)checkIn.CurrentMood,
                MoodDescription = checkIn.MoodDescription,
                CheckInDate = checkIn.CheckInDate,
                HasSuicidalThoughts = checkIn.HasSuicidalThoughts,
                IsSelfHarming = checkIn.IsSelfHarming
            };
        }

        public async Task<List<MentalHealthCheckInResponseDTO>> GetMentalHealthCheckInsByYoungPersonAsync(string youngPersonId, DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken)
        {
            var query = _context.MentalHealthCheckIns
                .Include(m => m.YoungPerson)
                .Where(m => m.YoungPersonId == youngPersonId);

            if (fromDate.HasValue)
                query = query.Where(m => m.CheckInDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(m => m.CheckInDate <= toDate.Value);

            var checkIns = await query
                .OrderByDescending(m => m.CheckInDate)
                .ToListAsync(cancellationToken);

            return checkIns.Select(m => new MentalHealthCheckInResponseDTO
            {
                Id = m.Id.ToString(),
                FacilityId = m.FacilityId,
                YoungPersonId = m.YoungPersonId,
                YoungPersonName = m.YoungPerson?.FullName,
                CurrentMood = (int)m.CurrentMood,
                MoodDescription = m.MoodDescription,
                CheckInDate = m.CheckInDate,
                HasSuicidalThoughts = m.HasSuicidalThoughts,
                IsSelfHarming = m.IsSelfHarming
            }).ToList();
        }

        public async Task<List<MentalHealthCheckInResponseDTO>> GetRecentMentalHealthCheckInsAsync(string facilityId, int days = 7, CancellationToken cancellationToken = default)
        {
            var fromDate = DateTime.UtcNow.AddDays(-days);

            var checkIns = await _context.MentalHealthCheckIns
                .Include(m => m.YoungPerson)
                .Where(m => m.FacilityId == facilityId && m.CheckInDate >= fromDate)
                .OrderByDescending(m => m.CheckInDate)
                .ToListAsync(cancellationToken);

            return checkIns.Select(m => new MentalHealthCheckInResponseDTO
            {
                Id = m.Id.ToString(),
                FacilityId = m.FacilityId,
                YoungPersonId = m.YoungPersonId,
                YoungPersonName = m.YoungPerson?.FullName,
                CurrentMood = (int)m.CurrentMood,
                MoodDescription = m.MoodDescription,
                CheckInDate = m.CheckInDate,
                HasSuicidalThoughts = m.HasSuicidalThoughts,
                IsSelfHarming = m.IsSelfHarming
            }).ToList();
        }
    }
}
