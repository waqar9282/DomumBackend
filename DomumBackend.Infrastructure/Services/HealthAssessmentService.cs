#nullable disable
using DomumBackend.Application.Common.Interfaces;
using DomumBackend.Application.DTOs;
using DomumBackend.Domain.Entities;
using DomumBackend.Domain.Enums;
using DomumBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DomumBackend.Infrastructure.Services
{
    public class HealthAssessmentService : IHealthAssessmentService
    {
        private readonly ApplicationDbContext _context;

        public HealthAssessmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateHealthAssessmentAsync(
            string facilityId, string youngPersonId, int assessmentType, DateTime assessmentDate,
            string assessedByProfessional, string assessmentLocation, string assessmentFindings,
            string recommendations, string referralRequired, string recordedByUserId,
            bool isConfidential, CancellationToken cancellationToken)
        {
            var assessment = new HealthAssessment
            {
                FacilityId = facilityId,
                YoungPersonId = youngPersonId,
                AssessmentType = (HealthAssessmentType)assessmentType,
                AssessmentDate = assessmentDate,
                AssessedByProfessional = assessedByProfessional,
                AssessmentLocation = assessmentLocation,
                AssessmentFindings = assessmentFindings,
                Recommendations = recommendations,
                ReferralRequired = referralRequired,
                RecordedByUserId = recordedByUserId,
                IsConfidential = isConfidential
            };

            _context.HealthAssessments.Add(assessment);
            await _context.SaveChangesAsync(cancellationToken);

            return assessment.Id.ToString();
        }

        public async Task<int> UpdateHealthAssessmentAsync(
            string id, string assessmentFindings, string recommendations, string referralRequired,
            DateTime? nextAssessmentDate, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long assessmentId))
                return 0;

            var assessment = await _context.HealthAssessments
                .FirstOrDefaultAsync(h => h.Id == assessmentId, cancellationToken);

            if (assessment == null)
                return 0;

            assessment.AssessmentFindings = assessmentFindings;
            assessment.Recommendations = recommendations;
            assessment.ReferralRequired = referralRequired;
            assessment.NextAssessmentDate = nextAssessmentDate;

            _context.HealthAssessments.Update(assessment);
            await _context.SaveChangesAsync(cancellationToken);

            return 1;
        }

        public async Task<HealthAssessmentResponseDTO> GetHealthAssessmentByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!long.TryParse(id, out long assessmentId))
                return null;

            var assessment = await _context.HealthAssessments
                .Include(h => h.YoungPerson)
                .FirstOrDefaultAsync(h => h.Id == assessmentId, cancellationToken);

            if (assessment == null)
                return null;

            return new HealthAssessmentResponseDTO
            {
                Id = assessment.Id.ToString(),
                FacilityId = assessment.FacilityId,
                YoungPersonId = assessment.YoungPersonId,
                YoungPersonName = assessment.YoungPerson?.FullName,
                AssessmentType = (int)assessment.AssessmentType,
                AssessmentDate = assessment.AssessmentDate,
                AssessedByProfessional = assessment.AssessedByProfessional,
                AssessmentFindings = assessment.AssessmentFindings,
                Recommendations = assessment.Recommendations,
                ReferralRequired = string.IsNullOrEmpty(assessment.ReferralRequired) ? false : true,
                NextAssessmentDate = assessment.NextAssessmentDate,
                CreatedDate = assessment.CreatedDate
            };
        }

        public async Task<List<HealthAssessmentResponseDTO>> GetHealthAssessmentsByYoungPersonAsync(string youngPersonId, CancellationToken cancellationToken)
        {
            var assessments = await _context.HealthAssessments
                .Include(h => h.YoungPerson)
                .Where(h => h.YoungPersonId == youngPersonId)
                .OrderByDescending(h => h.AssessmentDate)
                .ToListAsync(cancellationToken);

            return assessments.Select(a => new HealthAssessmentResponseDTO
            {
                Id = a.Id.ToString(),
                FacilityId = a.FacilityId,
                YoungPersonId = a.YoungPersonId,
                YoungPersonName = a.YoungPerson?.FullName,
                AssessmentType = (int)a.AssessmentType,
                AssessmentDate = a.AssessmentDate,
                AssessedByProfessional = a.AssessedByProfessional,
                AssessmentFindings = a.AssessmentFindings,
                Recommendations = a.Recommendations,
                ReferralRequired = string.IsNullOrEmpty(a.ReferralRequired) ? false : true,
                NextAssessmentDate = a.NextAssessmentDate,
                CreatedDate = a.CreatedDate
            }).ToList();
        }

        public async Task<List<HealthAssessmentResponseDTO>> GetHealthAssessmentsByTypeAsync(string youngPersonId, int assessmentType, CancellationToken cancellationToken)
        {
            var assessments = await _context.HealthAssessments
                .Include(h => h.YoungPerson)
                .Where(h => h.YoungPersonId == youngPersonId && h.AssessmentType == (HealthAssessmentType)assessmentType)
                .OrderByDescending(h => h.AssessmentDate)
                .ToListAsync(cancellationToken);

            return assessments.Select(a => new HealthAssessmentResponseDTO
            {
                Id = a.Id.ToString(),
                FacilityId = a.FacilityId,
                YoungPersonId = a.YoungPersonId,
                YoungPersonName = a.YoungPerson?.FullName,
                AssessmentType = (int)a.AssessmentType,
                AssessmentDate = a.AssessmentDate,
                AssessedByProfessional = a.AssessedByProfessional,
                AssessmentFindings = a.AssessmentFindings,
                Recommendations = a.Recommendations,
                ReferralRequired = string.IsNullOrEmpty(a.ReferralRequired) ? false : true,
                NextAssessmentDate = a.NextAssessmentDate,
                CreatedDate = a.CreatedDate
            }).ToList();
        }
    }
}
