using DomumBackend.Application.DTOs;

namespace DomumBackend.Application.Common.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<string> CreateMedicalRecordAsync(
            string facilityId, string youngPersonId, int appointmentType, DateTime appointmentDate,
            string healthcareProfessional, string healthcareFacility, int status, string diagnosis,
            string chief_complaint, string symptoms, string findingsObservations, string treatment,
            string prescription, string referralTo, string notes, string recordedByUserId,
            bool isConfidential, CancellationToken cancellationToken);

        Task<int> UpdateMedicalRecordAsync(
            string id, int status, string findingsObservations, string treatment, string prescription,
            string referralTo, DateTime? followUpDate, string followUpNotes, CancellationToken cancellationToken);

        Task<MedicalRecordResponseDTO> GetMedicalRecordByIdAsync(string id, CancellationToken cancellationToken);
        Task<List<MedicalRecordResponseDTO>> GetMedicalRecordsByYoungPersonAsync(string youngPersonId, CancellationToken cancellationToken);
        Task<List<MedicalRecordResponseDTO>> GetMedicalRecordsByFacilityAsync(string facilityId, CancellationToken cancellationToken);
    }

    public interface IMedicationService
    {
        Task<string> CreateMedicationAsync(
            string facilityId, string youngPersonId, string medicationName, string activeIngredient,
            string dosage, string dosageUnit, int quantityPerDose, DateTime prescriptionDate,
            DateTime startDate, int durationDays, string frequency, string route, string prescribedByDoctor,
            string reasonForMedication, bool isAdministeredByStaff, string recordedByUserId,
            CancellationToken cancellationToken);

        Task<int> UpdateMedicationAsync(
            string id, int status, string reportedSideEffects, string notes, CancellationToken cancellationToken);

        Task<int> DiscontinueMedicationAsync(string id, string reason, CancellationToken cancellationToken);
        Task<MedicationResponseDTO> GetMedicationByIdAsync(string id, CancellationToken cancellationToken);
        Task<List<MedicationResponseDTO>> GetMedicationsByYoungPersonAsync(string youngPersonId, CancellationToken cancellationToken);
        Task<List<MedicationResponseDTO>> GetActiveMedicationsByYoungPersonAsync(string youngPersonId, CancellationToken cancellationToken);
    }

    public interface IHealthAssessmentService
    {
        Task<string> CreateHealthAssessmentAsync(
            string facilityId, string youngPersonId, int assessmentType, DateTime assessmentDate,
            string assessedByProfessional, string assessmentLocation, string assessmentFindings,
            string recommendations, string referralRequired, string recordedByUserId,
            bool isConfidential, CancellationToken cancellationToken);

        Task<int> UpdateHealthAssessmentAsync(
            string id, string assessmentFindings, string recommendations, string referralRequired,
            DateTime? nextAssessmentDate, CancellationToken cancellationToken);

        Task<HealthAssessmentResponseDTO> GetHealthAssessmentByIdAsync(string id, CancellationToken cancellationToken);
        Task<List<HealthAssessmentResponseDTO>> GetHealthAssessmentsByYoungPersonAsync(string youngPersonId, CancellationToken cancellationToken);
        Task<List<HealthAssessmentResponseDTO>> GetHealthAssessmentsByTypeAsync(string youngPersonId, int assessmentType, CancellationToken cancellationToken);
    }

    public interface INutritionLogService
    {
        Task<string> CreateNutritionLogAsync(
            string facilityId, string youngPersonId, DateTime logDate, string mealType,
            string foodDescription, decimal? portionSize, string appetite, string notes,
            string recordedByUserId, CancellationToken cancellationToken);

        Task<int> UpdateNutritionLogAsync(
            string id, string foodDescription, string appetite, string notes, CancellationToken cancellationToken);

        Task<NutritionLogResponseDTO> GetNutritionLogByIdAsync(string id, CancellationToken cancellationToken);
        Task<List<NutritionLogResponseDTO>> GetNutritionLogsByYoungPersonAsync(string youngPersonId, DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken);
        Task<List<NutritionLogResponseDTO>> GetNutritionLogsByFacilityAsync(string facilityId, DateTime logDate, CancellationToken cancellationToken);
    }

    public interface IPhysicalActivityLogService
    {
        Task<string> CreatePhysicalActivityLogAsync(
            string facilityId, string youngPersonId, int activityType, DateTime activityDate,
            string activityDescription, string location, int durationMinutes, string intensity,
            string participants, string supervsingStaff, string performanceLevel, string effortLevel,
            string recordedByUserId, CancellationToken cancellationToken);

        Task<int> UpdatePhysicalActivityLogAsync(
            string id, string activityDescription, string performanceLevel, string progressNotes,
            CancellationToken cancellationToken);

        Task<PhysicalActivityLogResponseDTO> GetPhysicalActivityLogByIdAsync(string id, CancellationToken cancellationToken);
        Task<List<PhysicalActivityLogResponseDTO>> GetPhysicalActivityLogsByYoungPersonAsync(string youngPersonId, DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken);
        Task<List<PhysicalActivityLogResponseDTO>> GetPhysicalActivityLogsByTypeAsync(string youngPersonId, int activityType, CancellationToken cancellationToken);
    }

    public interface IMentalHealthCheckInService
    {
        Task<string> CreateMentalHealthCheckInAsync(
            string facilityId, string youngPersonId, int currentMood, string moodDescription,
            string emotionalState, int? moodScore, string sleepQuality, string energyLevel,
            string currentConcerns, string coping, string supportSources, bool hasSuicidalThoughts,
            bool isSelfHarming, string staffObservations, string checkInByUserId, bool isConfidential,
            CancellationToken cancellationToken);

        Task<int> UpdateMentalHealthCheckInAsync(
            string id, int currentMood, string staffObservations, string actionsRequired,
            bool referralRequired, string referralDetails, CancellationToken cancellationToken);

        Task<MentalHealthCheckInResponseDTO> GetMentalHealthCheckInByIdAsync(string id, CancellationToken cancellationToken);
        Task<List<MentalHealthCheckInResponseDTO>> GetMentalHealthCheckInsByYoungPersonAsync(string youngPersonId, DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken);
        Task<List<MentalHealthCheckInResponseDTO>> GetRecentMentalHealthCheckInsAsync(string facilityId, int days = 7, CancellationToken cancellationToken = default);
    }
}
