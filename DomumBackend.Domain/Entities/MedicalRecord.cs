using DomumBackend.Domain.Enums;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Medical Record - Tracks appointments, diagnoses, and medical treatments
    /// </summary>
    public class MedicalRecord : BaseEntity
    {
        public string FacilityId { get; set; }
        public string YoungPersonId { get; set; }
        public YoungPerson YoungPerson { get; set; }

        // Appointment Information
        public AppointmentType AppointmentType { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string HealthcareProfessional { get; set; } // Doctor name, dentist, etc.
        public string HealthcareFacility { get; set; } // Hospital, clinic name
        public AppointmentStatus Status { get; set; }

        // Medical Details
        public string Diagnosis { get; set; } // What condition was diagnosed
        public string Chief_Complaint { get; set; } // Reason for visit
        public string Symptoms { get; set; } // Symptoms presented
        public string FindingsObservations { get; set; } // Doctor's findings
        public string Treatment { get; set; } // Treatment plan prescribed
        public string Prescription { get; set; } // Medications prescribed
        public string ReferralTo { get; set; } // Referral to specialist if needed
        public string Notes { get; set; } // Additional clinical notes

        // Follow-up
        public DateTime? FollowUpDate { get; set; }
        public string FollowUpNotes { get; set; }

        // Staff Recording
        public string RecordedByUserId { get; set; } // Staff member who recorded
        public DateTime RecordedDate { get; set; }

        // Compliance
        public bool IsConfidential { get; set; }
        public string ConsentGiven { get; set; } // Yes/No/Pending
    }
}
