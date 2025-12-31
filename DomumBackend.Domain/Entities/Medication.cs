using DomumBackend.Domain.Enums;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Medication - Tracks prescribed medications, dosages, and schedules
    /// </summary>
    public class Medication : BaseEntity
    {
        public required string FacilityId { get; set; }
        public required string YoungPersonId { get; set; }
        public YoungPerson? YoungPerson { get; set; }

        // Medication Details
        public required string MedicationName { get; set; } // e.g., Amoxicillin
        public required string ActiveIngredient { get; set; } // Chemical compound
        public required string Dosage { get; set; } // e.g., 500mg
        public required string DosageUnit { get; set; } // mg, ml, tablets, etc.
        public int QuantityPerDose { get; set; } // How many tablets/ml per dose

        // Prescription Details
        public DateTime PrescriptionDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // When medication ends
        public int DurationDays { get; set; } // Course length
        public required string Frequency { get; set; } // e.g., Twice daily, 3x daily
        public required string Route { get; set; } // Oral, Injection, Topical, etc.

        // Prescriber Information
        public required string PrescribedByDoctor { get; set; }
        public string? DoctorQualification { get; set; }
        public required string ReasonForMedication { get; set; } // Diagnosis/condition

        // Administration Tracking
        public bool IsAdministeredByStaff { get; set; } // true = staff administered, false = self-administered
        public MedicationStatus Status { get; set; }
        public string? DiscontinuationReason { get; set; } // If discontinued

        // Side Effects & Allergies
        public string? KnownAllergies { get; set; }
        public string? PotentialSideEffects { get; set; }
        public string? ReportedSideEffects { get; set; } // Observed side effects
        public string? ContraindicatedWith { get; set; } // Other drugs it conflicts with

        // Storage & Safety
        public string? StorageLocation { get; set; } // Where medication is kept
        public bool RequiresRefrigeration { get; set; }
        public string? SpecialHandling { get; set; }

        // Compliance & Audit
        public required string RecordedByUserId { get; set; }
        public DateTime RecordedDate { get; set; }
        public bool IsConfidential { get; set; }
    }
}
